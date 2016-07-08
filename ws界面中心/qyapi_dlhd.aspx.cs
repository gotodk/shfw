using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class qyapi_dlhd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        string msg_signature = Request["msg_signature"].ToString();
        string timestamp = Request["timestamp"].ToString();
        string nonce = Request["nonce"].ToString();
        string echostr = Request["echostr"].ToString();
 
        //Response.Write(echostr);


        //公众平台上开发者设置的token, corpID, EncodingAESKey
        string sToken = "QDG6eK";
        string sCorpID = "wx5823bf96d3bd56c7";
        string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";

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