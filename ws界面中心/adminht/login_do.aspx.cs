using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login_do : System.Web.UI.Page
{
 

    protected void Page_Load(object sender, EventArgs e)
    {
  
        //处理ajax请求
        string ajaxrun = "";
        if (Request["ajaxrun"] == null || Request["ajaxrun"].ToString().Trim() == "")
        {
            return;
        }
        if (Request["jkname"] == null || Request["jkname"].ToString().Trim() == "")
        {
            return;
        }
        if (Request["zhanghao"] == null || Request["zhanghao"].ToString().Trim() == "" || Request["mima"] == null || Request["mima"].ToString().Trim() == "")
        {
            return;
        }
        string jkname = Request["jkname"].ToString();
        ajaxrun = Request["ajaxrun"].ToString();
        string zhanghao = Request["zhanghao"].ToString().Trim();
        string mima = Request["mima"].ToString().Trim();
    
        if (ajaxrun == "backlogin")
        { 

            //处理验证码
            string c = "0";
            HttpCookie aaa = Request.Cookies["vccs"];
            if (aaa == null)
            {
                c = "0";
                Response.Cookies["vccs"].Value = "0";
                Response.Cookies["vccs"].Expires = DateTime.Now.Add(new TimeSpan(6, 0, 0));
            }
            c = Request.Cookies["vccs"].Value;

           
            if (Convert.ToInt32(c) >= 5)//超过错误次数才对验证码进行校验
            {
                if (Request["yanzhengma"] == null || Request["yanzhengma"].ToString().Trim() == "" || Request["yanzhengma"].ToString().Trim().ToLower() != Session["ValidateCode"].ToString().ToLower())
                {
                    Response.Write("验证码错误！");
                    //搞乱验证码
                    Session["ValidateCode"] = Guid.NewGuid().ToString();
                    return;
                }
            }




            //调用执行方法获取数据

            object[] re_dsi = IPC.Call(jkname, new object[] { zhanghao, mima,AuthComm.IP });
            if (re_dsi[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];
                if (dsreturn.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "ok")
                {
                    UserSession.唯一键 = dsreturn.Tables["用户信息"].Rows[0]["UAid"].ToString();
                    UserSession.登录名 = dsreturn.Tables["用户信息"].Rows[0]["Uloginname"].ToString();
                    UserSession.是否超管 = dsreturn.Tables["用户信息"].Rows[0]["SuperUser"].ToString();
                    UserSession.最终权值_后台菜单权限 = dsreturn.Tables["用户信息"].Rows[0]["UfinalUnumber1"].ToString();
                    UserSession.最终权值_前台导航权限 = dsreturn.Tables["用户信息"].Rows[0]["UfinalUnumber2"].ToString();
                    UserSession.最终权值_全局独立权限 = dsreturn.Tables["用户信息"].Rows[0]["UfinalUnumber3"].ToString();
                    UserSession.最终权值_特殊权限 = dsreturn.Tables["用户信息"].Rows[0]["UfinalUnumber4"].ToString();
                    UserSession.最终权值_备用权限 = dsreturn.Tables["用户信息"].Rows[0]["UfinalUnumber5"].ToString();
                    Response.Cookies["user_Uloginname_onlyforinput"].Value = Server.UrlEncode(dsreturn.Tables["用户信息"].Rows[0]["Uloginname"].ToString());
                    Response.Cookies["user_Uloginname_onlyforinput"].Expires = DateTime.MaxValue;
                    Response.Write("ok");
                    return;
                }
                else
                {
                    if (dsreturn.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "err_olnypassworderr")
                    {
                        //密码错误次数

                        Response.Cookies["vccs"].Value = (Convert.ToInt32(c) + 1).ToString();
                        Response.Cookies["vccs"].Expires = DateTime.Now.Add(new TimeSpan(6, 0, 0));
              
                        
                    }
                    Response.Write(dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString());
                    return;
                }

            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串
                return;

            }








        }
    }
}