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

public partial class MasterPageMain : System.Web.UI.MasterPage
{

    public string znxsl = "0";
    public string mytouiang = "/mytutu/defaulttouxiang.jpg";

    private DataSet GetInfo_znxtop()
    {


        object[] re_dsi = IPC.Call("获取站内信右上角提醒", new object[] { UserSession.唯一键 });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            return (DataSet)(re_dsi[1]);
        }
        return null;

    }

    protected void Page_Load(object sender, EventArgs e)
    {



        masterpageleftmenu1.OnNeedLoadData += new masterpageleftmenu.OnNeedDataHandler(MyWebControl_OnNeedLoadData);
        this.Page.Title = ConfigurationManager.AppSettings["SYSname"] + "";
        titleshowname.InnerHtml = ConfigurationManager.AppSettings["SYSname"] + "";
        mysmlogo.Src = ConfigurationManager.AppSettings["mylogo_s"];
        showusername.InnerHtml = UserSession.登录名;

        //重新显示右上角为员工姓名
        
        object[] re_dsi_uinf = IPC.Call("获取用户基础资料", new object[] { UserSession.唯一键 });
        if (re_dsi_uinf[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataTable dt = (DataTable)re_dsi_uinf[1];
            if (dt != null && dt.Columns.Contains("xingming"))
            {
                showusername.InnerHtml = dt.Rows[0]["xingming"].ToString() + '[' + showusername.InnerHtml + ']';
            }
        }

        //获取用户头像
        if (System.IO.File.Exists(Server.MapPath("/uploadfiles/faceup/" + UserSession.唯一键 + ".jpg")))
        {
            mytouiang = "/uploadfiles/faceup/" + UserSession.唯一键 + ".jpg";
        }
        
   

        DataSet ds_znx = GetInfo_znxtop();
        if (ds_znx != null)
        {
            repeater_znx.DataSource = ds_znx.Tables["站内信提醒"];
            repeater_znx.DataBind();
            znxsl = ds_znx.Tables["站内信提醒"].Rows.Count.ToString();
        }

    }
    protected override void OnInit(EventArgs e)
    {

        //登录状态判定
        if (UserSession.唯一键 == "")
        {
            Response.Redirect("/adminht/login.aspx?u=" + StringOP.encMe(Request.Url.PathAndQuery,"mima"));
            return;
        }
        if (!AuthComm.chekcAuth_fromsession("1", UserSession.最终权值_全局独立权限, false))
        {
            Response.Redirect("/adminht/login.aspx?f=exit&meiyouquanxian=yes");
            return;
        }

        base.OnInit(e);
    }  
    //绑定事件导航栏修改事件
    private void MyWebControl_OnNeedLoadData(ArrayList al_daohang, string ERRinfo)
    {
        //修改导航
        dongtaidaohang.InnerHtml = "<li><i class='ace-icon fa fa-home home-icon'></i><a href='/adminht/demo_home.aspx'>首页</a></li>";
        int alli = al_daohang.Count;
        for (int i = 1; i < al_daohang.Count; i++)
        {
            if (i == alli - 1)
            {
                dongtaidaohang.InnerHtml = dongtaidaohang.InnerHtml + "<li class='active'>" + al_daohang[i].ToString() + "</li>";
            }
            else
            {
                dongtaidaohang.InnerHtml = dongtaidaohang.InnerHtml + "<li><a href='javascript:void(0)'>" + al_daohang[i].ToString() + "</a></li>";
            }

        }

    }
}
