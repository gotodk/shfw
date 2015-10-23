using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 统一名称设置或获取登陆后记录的session
/// </summary>
public static class UserSession
{
    public static string 唯一键
    {
        get { return (HttpContext.Current.Session["user_UAid"] == null) ? "" : HttpContext.Current.Session["user_UAid"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UAid"] = value; }
    }
    public static string 登录名
    {
        get { return (HttpContext.Current.Session["user_Uloginname"] == null) ? "" : HttpContext.Current.Session["user_Uloginname"].ToString().Trim(); }
        set {HttpContext.Current.Session["user_Uloginname"] = value;}
    }
    public static string 是否超管
    {
        get { return (HttpContext.Current.Session["user_SuperUser"] == null || HttpContext.Current.Session["user_SuperUser"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_SuperUser"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_SuperUser"] = value; }
    }
    public static string 最终权值_后台菜单权限
    {
        get { return (HttpContext.Current.Session["user_UfinalUnumber1"] == null || HttpContext.Current.Session["user_UfinalUnumber1"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_UfinalUnumber1"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UfinalUnumber1"] = value; }
    }
    public static string 最终权值_前台导航权限
    {
        get { return (HttpContext.Current.Session["user_UfinalUnumber2"] == null || HttpContext.Current.Session["user_UfinalUnumber2"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_UfinalUnumber2"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UfinalUnumber2"] = value; }
    }
    public static string 最终权值_全局独立权限
    {
        get { return (HttpContext.Current.Session["user_UfinalUnumber3"] == null || HttpContext.Current.Session["user_UfinalUnumber3"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_UfinalUnumber3"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UfinalUnumber3"] = value; }
    }
    public static string 最终权值_特殊权限
    {
        get { return (HttpContext.Current.Session["user_UfinalUnumber4"] == null || HttpContext.Current.Session["user_UfinalUnumber4"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_UfinalUnumber4"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UfinalUnumber4"] = value; }
    }
    public static string 最终权值_备用权限
    {
        get { return (HttpContext.Current.Session["user_UfinalUnumber5"] == null || HttpContext.Current.Session["user_UfinalUnumber5"].ToString().Trim() == "") ? "0" : HttpContext.Current.Session["user_UfinalUnumber5"].ToString().Trim(); }
        set { HttpContext.Current.Session["user_UfinalUnumber5"] = value; }
    }
}