
using FMPublicClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    public string homeurl = "demo_home.aspx";
    public string inputzhanghao = "";
    public string sCorpID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        sCorpID = ConfigurationManager.AppSettings["wx_CorpID"].ToString();

        denglu_title.InnerHtml = ConfigurationManager.AppSettings["SYSname"];

        if (Request["u"] != null && Request["u"].ToString() != "")
        {
            homeurl = StringOP.uncMe(Request["u"].ToString(),"mima");
        }


        //处理退出
        if (Request["f"] != null && Request["f"].ToString() == "exit")
        {
            Session.RemoveAll();


        }

        //特殊的无权登录提示
        if (Request["meiyouquanxian"] != null && Request["meiyouquanxian"].ToString() == "yes")
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "keys", "alert('此账号无权登录后台内部管理！');", true);  
        }

        if (Request.Cookies["user_Uloginname_onlyforinput"] != null)
        {
            inputzhanghao = Server.UrlDecode(Request.Cookies["user_Uloginname_onlyforinput"].Value); //输入框记忆
        }

 
    }
}