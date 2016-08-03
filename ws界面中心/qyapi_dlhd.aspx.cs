using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMipcClass;
using System.Text;
using System.IO;
using System.Data;

public partial class qyapi_dlhd : System.Web.UI.Page
{

    /// <summary>
    /// 提交post消息
    /// </summary>
    /// <param name="postUrl"></param>
    /// <param name="paramData"></param>
    /// <param name="dataEncode"></param>
    /// <returns></returns>
    private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
    {
        string ret = string.Empty;
        try
        {
            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";

            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            return "err:" + ex.ToString();
        }
        return ret;
    }

    protected void Page_Load(object sender, EventArgs e)
    {






        //Response.Write(echostr);


        //公众平台上开发者设置的token, corpID, EncodingAESKey
        //string sToken = "QDG6eK";
        string sToken = ConfigurationManager.AppSettings["wx_Token"].ToString();
        //string sCorpID = "wxdb0c8553d3bf3ad5";
        string sCorpID = ConfigurationManager.AppSettings["wx_CorpID"].ToString();
        //string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";
        string sEncodingAESKey = ConfigurationManager.AppSettings["wx_EncodingAESKey"].ToString();
        //string wx_corpsecret = "Mte0XxwwFPy9qbcztpE9CCsbuApg6eeSmljzghtax1H7wg2jFbSH_w3h-TbeXJjq";
        string wx_corpsecret = ConfigurationManager.AppSettings["wx_corpsecret"].ToString();

        string wx_agentid = ConfigurationManager.AppSettings["wx_agentid"].ToString();

        if (Request["sendmsgf"] != null)
        {
            //发送微信消息
            if (Request["sendmsgf"].ToString() == "test")
            {
                WebClient client = new WebClient();

                //获取access_token
                string content = client.DownloadString("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + sCorpID + "&corpsecret=" + wx_corpsecret + "");
                string access_token = content.Split(',')[0].Split(':')[1].Replace("\"", "").Replace(" ", "");

                string msg_json = "{";
                msg_json = msg_json + "\"touser\": \"[[UserID]]\",";
                //msg_json = msg_json + "\"toparty\": \"PartyID1|PartyID2\",";
                //msg_json = msg_json + "\"totag\": \"TagID1|TagID2\",";
                msg_json = msg_json + "\"msgtype\": \"text\",";
                msg_json = msg_json + "\"agentid\": "+ wx_agentid + ",";
                msg_json = msg_json + "\"text\": {\"content\": \"[[MsgContent]]\"},";
                msg_json = msg_json + "\"safe\":\"0\"}";

                msg_json = msg_json.Replace("[[UserID]]", "gotodk");
                msg_json = msg_json.Replace("[[MsgContent]]", "测试消息22");
                string restr = PostWebRequest("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + access_token, msg_json, Encoding.UTF8);

                Response.Write(restr);
            }
            if (Request["sendmsgf"].ToString() == "send")
            {
                WebClient client = new WebClient();

                //获取access_token
                string content = client.DownloadString("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + sCorpID + "&corpsecret=" + wx_corpsecret + "");
                string access_token = content.Split(',')[0].Split(':')[1].Replace("\"", "").Replace(" ", "");

                string msg_json = "{";
                msg_json = msg_json + "\"touser\": \"[[UserID]]\",";
                //msg_json = msg_json + "\"toparty\": \"PartyID1|PartyID2\",";
                //msg_json = msg_json + "\"totag\": \"TagID1|TagID2\",";
                msg_json = msg_json + "\"msgtype\": \"text\",";
                msg_json = msg_json + "\"agentid\": " + wx_agentid + ",";
                msg_json = msg_json + "\"text\": {\"content\": \"[[MsgContent]]\"},";
                msg_json = msg_json + "\"safe\":\"0\"}";

                //连接数据库，获取要发送的消息列表，获取的同时要更新成微信已发送，不管是否发送成功。
                string restr = "微信消息发送结果：";
                DataSet dsmsg = new DataSet();
                object[] re_dsi_wx = IPC.Call("获取待发送微信消息的提醒", new object[] { "所有未发送" });
                if (re_dsi_wx[0].ToString() == "ok")
                {
                    //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                    dsmsg = (DataSet)(re_dsi_wx[1]);
                    if (dsmsg.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "ok")
                    {
                        for (int i = 0; i < dsmsg.Tables["待发数据"].Rows.Count; i++)
                        {
                            string msg_json_init = msg_json;
                            msg_json_init = msg_json_init.Replace("[[UserID]]", dsmsg.Tables["待发数据"].Rows[i]["Uloginname"].ToString());
                            msg_json_init = msg_json_init.Replace("[[MsgContent]]", dsmsg.Tables["待发数据"].Rows[i]["msgtitle"].ToString());
                            restr = restr + Environment.NewLine + "---"  + PostWebRequest("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + access_token, msg_json_init, Encoding.UTF8);
                        }
                    }
                }
                else
                {
                    string err = "调用错误" + re_dsi_wx[1].ToString();
                    Response.Write(err);
                    return;
                }

                

                Response.Write(restr);
            }
        }
        if (Request["code"] != null)
        {
            try
            {
                string code = Request["code"].ToString();
                //Response.Write(code);

                WebClient client = new WebClient();

                //获取access_token
                string content = client.DownloadString("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + sCorpID + "&corpsecret=" + wx_corpsecret + "");
                string access_token = content.Split(',')[0].Split(':')[1].Replace("\"", "").Replace(" ", "");
                //Response.Write(access_token);

                //client.Encoding = Encoding.UTF8;
                string address = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + access_token + "&code=" + code;
                //Response.Redirect(address);
                string endstr = client.DownloadString(address);
                Response.Write(endstr);
                HTMLAnalyzeClass HAC = new HTMLAnalyzeClass();
                string wxusername = HAC.My_Cut_Str(endstr, "UserId\":\"", "\"", 1, false)[0].ToString();

                //尝试找到对应账号和密码，如果找到，自动跳转到自动登录界面
                string jm = "";
                object[] re_dsi_wx = IPC.Call("获取微信自动登录参数", new object[] { wxusername });
                if (re_dsi_wx[0].ToString() == "ok")
                {
                    //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                    jm = re_dsi_wx[1].ToString();
                }
                else
                {
                    string err = "调用错误" + re_dsi_wx[1].ToString();
                    jm = "";
                }

                //string zhanghao = wxusername;
                //string mima = "48d757d7d2c387c0f25d7bece01768dd";
                //string jm = zhanghao+"|"+ mima;
                Response.Redirect("/adminht/login.aspx?aulgogo=1&aulcscs=" + jm + "");

                //Response.Write(wxusername);
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }


        }




        //仅用于验证
        if (Request["echostr"] != null)
        {
            string msg_signature = Request["msg_signature"].ToString();
            string timestamp = Request["timestamp"].ToString();
            string nonce = Request["nonce"].ToString();
            string echostr = Request["echostr"].ToString();

            /*
------------使用示例一：验证回调URL---------------
*企业开启回调模式时，企业号会向验证url发送一个get请求 
假设点击验证时，企业收到类似请求：
* GET /cgi-bin/wxpush?msg_signature=5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3&timestamp=1409659589&nonce=263014780&echostr=P9nAzCzyDtyTWESHep1vC5X9xho%2FqYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp%2B4RPcs8TgAE7OaBO%2BFZXvnaqQ%3D%3D 
* HTTP/1.1 Host: qy.weixin.qq.com

* 接收到该请求时，企业应			1.解析出Get请求的参数，包括消息体签名(msg_signature)，时间戳(timestamp)，随机数字串(nonce)以及公众平台推送过来的随机加密字符串(echostr),
这一步注意作URL解码。
2.验证消息体签名的正确性 
3.解密出echostr原文，将原文当作Get请求的response，返回给公众平台
第2，3步可以用公众平台提供的库函数VerifyURL来实现。
*/

            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            string sVerifyMsgSig = msg_signature;
            //string sVerifyMsgSig = "5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3";
            string sVerifyTimeStamp = timestamp;
            //string sVerifyTimeStamp = "1409659589";
            string sVerifyNonce = nonce;
            //string sVerifyNonce = "263014780";
            string sVerifyEchoStr = echostr;
            //string sVerifyEchoStr = "P9nAzCzyDtyTWESHep1vC5X9xho/qYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp+4RPcs8TgAE7OaBO+FZXvnaqQ==";
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret != 0)
            {
                System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
                return;
            }
            //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。
            Response.Write(sEchoStr);
        }







    }
}