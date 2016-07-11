using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class qyapi_dlhd : System.Web.UI.Page
{
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

        if (Request["code"] != null)
        {
            try {
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
                HTMLAnalyzeClass HAC = new HTMLAnalyzeClass();
                string wxusername = HAC.My_Cut_Str(endstr, "UserId\":\"", "\"", 1, false)[0].ToString();

                //尝试找到对应账号和密码，如果找到，自动跳转到自动登录界面
                //调用框架免代理通用接口删，公用一下删除接口
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
                Response.Redirect("/adminht/login.aspx?aulgogo=1&aulcscs="+ jm + "");

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