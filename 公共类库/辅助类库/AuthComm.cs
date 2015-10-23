using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// AuthComm 的摘要说明
/// </summary>
public static class AuthComm
{
 


    public static Int64 toDenaryIp(string ip)
    {
        Int64 _Int64 = 0;
        string _ip = ip;
        if (_ip.LastIndexOf(".") > -1)
        {
            string[] _iparray = _ip.Split('.');

            _Int64 = Int64.Parse(_iparray.GetValue(0).ToString()) * 256 * 256 * 256 + Int64.Parse(_iparray.GetValue(1).ToString()) * 256 * 256 + Int64.Parse(_iparray.GetValue(2).ToString()) * 256 + Int64.Parse(_iparray.GetValue(3).ToString()) - 1;
        }
        return _Int64;
    }

    /// <summary>
    /// /ip十进制
    /// </summary>
    public static Int64 DenaryIp
    {
        get
        {
            Int64 _Int64 = 0;

            string _ip = IP;
            if (_ip.LastIndexOf(".") > -1)
            {
                string[] _iparray = _ip.Split('.');

                _Int64 = Int64.Parse(_iparray.GetValue(0).ToString()) * 256 * 256 * 256 + Int64.Parse(_iparray.GetValue(1).ToString()) * 256 * 256 + Int64.Parse(_iparray.GetValue(2).ToString()) * 256 + Int64.Parse(_iparray.GetValue(3).ToString()) - 1;
            }
            return _Int64;
        }
    }

    public static string IP
    {
        get
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if ( result != null && result != String.Empty )
            {
               //可能有代理
                if ( result.IndexOf ( "." ) == -1 ) //没有"."肯定是非IPv4格式
                    result = null;
                else
                {
                    if ( result.IndexOf ( "," ) != -1 )
                    {
                         //有","，估计多个代理。取第一个不是内网的IP。
                        result = result.Replace ( " ", "" ).Replace ( "", "" );
                        string[] temparyip = result.Split ( ",;".ToCharArray() );
                        for ( int i = 0; i < temparyip.Length; i++ )
                        {
                            if ( IsIPAddress ( temparyip[i] )
                                    && temparyip[i].Substring ( 0, 3 ) != "10."
                                    && temparyip[i].Substring ( 0, 7 ) != "192.168"
                                    && temparyip[i].Substring ( 0, 7 ) != "172.16." )
                            {
                                return temparyip[i]; //找到不是内网的地址
                            }
                        }
                    }
                    else if ( IsIPAddress ( result ) ) //代理即是IP格式
                        return result;
                    else
                        result = null; //代理中的内容 非IP，取IP
                }

            }

            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

           
            if ( null == result || result == String.Empty )
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if ( result == null || result == String.Empty )
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
    }

    //是否ip格式
    public static bool IsIPAddress(string str1)
    {
     
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

        string regformat = @"^\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}$";

        Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }

    /// <summary>
    /// 检查权限(仅判定，不执行操作,外部不需要调用)
    /// </summary>
    /// <param name="userAuthNumber">用户权限</param>
    /// <param name="enumNumber">枚举的要判定值</param>
    /// <param name="issuperuser">是否无限制的超级管理员(1为是，0为否)</param>
    /// <returns></returns>
    private static bool checkAuth(string userAuthNumber, string enumNumber,string issuperuser)
    {
        //超级用户就直接返回
        if (issuperuser == "1")
        {
            return true;
        }
        //用户权限
        BigInteger qx = BigInteger.Parse(userAuthNumber);
        //枚举的要判定值
        BigInteger eun = BigInteger.Parse(enumNumber);
        //判定是否具备权限
        if ((qx & eun) == eun)
        {
           return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 通过session检查权限(管理后台)
    /// </summary>
    /// <param name="enumNumber">要检查的权限权值枚举值(在配置中查阅)</param>
    /// <param name="SUfinal_Number">当前用户某个类型的最终权值</param>
    /// <param name="Do_Redirect">没有权限时，是否跳转到无权提示页面.true为跳转，false为不跳转。 一般情况用true，需要自己特殊处理代码的才用false,因为false不跳转等于没有控制权限只是检测了结果</param>
    /// <returns>是否具备指定权限</returns>
    public static bool chekcAuth_fromsession(string enumNumber, string SUfinal_Number, bool Do_Redirect)
    {
 
    
        //检查唯一编号是否存在
        if (UserSession.唯一键 == "")
        {
            HttpContext.Current.Response.Redirect("/adminht/login.aspx?u=" + HttpContext.Current.Request.Url.PathAndQuery);
            return false;
        }
        //检查是否具备权限
        if (!AuthComm.checkAuth(SUfinal_Number, enumNumber, UserSession.是否超管))
        {
            if (Do_Redirect)
            {
                HttpContext.Current.Response.Redirect("/adminht/auth_noauth.aspx");
            }

            return false;
        }
        else
        {
            return true;
        }
    }


    /// <summary>
    /// 通过session检查权限(用户后台)
    /// </summary>
    /// <param name="enumNumber">要检查的权限权值枚举值(在配置中查阅)</param>
    /// <param name="SUfinal_Number">当前用户某个类型的最终权值</param>
    /// <param name="Do_Redirect">没有权限时，是否跳转到无权提示页面.true为跳转，false为不跳转。 一般情况用true，需要自己特殊处理代码的才用false,因为false不跳转等于没有控制权限只是检测了结果</param>
    /// <returns>是否具备指定权限</returns>
    public static bool chekcAuth_fromsession_userht(string enumNumber, string SUfinal_Number, bool Do_Redirect)
    {


        //检查唯一编号是否存在
        if (UserSession.唯一键 == "")
        {
            HttpContext.Current.Response.Redirect("/userht/login.aspx?u=" + HttpContext.Current.Request.Url.PathAndQuery);
            return false;
        }
        //检查是否具备权限
        if (!AuthComm.checkAuth(SUfinal_Number, enumNumber, UserSession.是否超管))
        {
            if (Do_Redirect)
            {
                HttpContext.Current.Response.Redirect("/userht/auth_noauth.aspx");
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 从Unmber值中分解出已赋予的权限枚举
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string,string> GetEnumFormUnumber(string Unumber)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        BigInteger qx = BigInteger.Parse(Unumber);
        Int32 shuliang = 0;
        while (qx > 0 && shuliang<6500)
        {
            BigInteger checkthis = BigInteger.Pow(2, shuliang);
            shuliang++;
            if (checkAuth(Unumber, checkthis.ToString(),"0"))
            {
                dic.Add(shuliang.ToString(), checkthis.ToString());//加入有效值
                qx = qx & ~checkthis;//踢掉权限
            }
        }
        return dic;
    }
}