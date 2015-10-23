using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 统一名称设置或获取登陆后记录的session（扩展session，用于业务。）
/// </summary>
public static class UserSessionEX
{
    public static string 用户类型
    {
        get { return (HttpContext.Current.Session["user_btype"] == null) ? "" : HttpContext.Current.Session["user_btype"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_btype"] = value; }
    }

}