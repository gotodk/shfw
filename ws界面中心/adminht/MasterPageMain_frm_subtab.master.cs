using FMipcClass;
using FMPublicClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPageMain_frm_subtab : System.Web.UI.MasterPage
{
 

    protected void Page_Load(object sender, EventArgs e)
    {
 

    }
    protected override void OnInit(EventArgs e)
    {

        //登录状态判定
        if (UserSession.唯一键 == "")
        {
            Response.Redirect("/adminht/login.aspx?u=" + StringOP.encMe(Request.Url.PathAndQuery, "mima"));
            return;
        }
        if (!AuthComm.chekcAuth_fromsession("1", UserSession.最终权值_全局独立权限, false))
        {
            Response.Redirect("/adminht/login.aspx?f=exit&meiyouquanxian=yes");
            return;
        }

        base.OnInit(e);
    }  
 
}
