using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
/// <summary>
/// FUPpublic 的摘要说明
/// </summary>
public class FUPpublic
{
    public FUPpublic()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 返回公共变量定义
    /// </summary>
    /// <param name="req">请求</param>
    /// <param name="dsFPZ">配置</param>
    /// <returns></returns>
    public static Hashtable initPP(HttpRequest req, DataSet dsFPZ)
    {
        Hashtable htPP = new Hashtable();
        htPP["title_f"] = "";
        htPP["form_hide"] = "";
        htPP["reloaddb_type"] = "reset";

        //判定是新增还是保存
        if (req["fff"] != null && req["fff"].ToString() == "1")
        {
            htPP["title_f"] = "修改--" + dsFPZ.Tables["表单配置主表"].Rows[0]["Fname"].ToString() + "(" + req["idforedit"].ToString() + ")";
            htPP["form_hide"] = "hide";
            htPP["reloaddb_type"] = "button";
        }
        else
        {
            htPP["title_f"] = "新增--" + dsFPZ.Tables["表单配置主表"].Rows[0]["Fname"].ToString();
        }

        htPP["othercheck"] = new Hashtable();
        return htPP;
    }

    /// <summary>
    /// 返回公共变量定义
    /// </summary>
    /// <param name="req">请求</param>
    /// <param name="dsFPZ">配置</param>
    /// <returns></returns>
    public static Hashtable initPP_list(HttpRequest req, DataSet dsFPZ)
    {
        Hashtable htPP = new Hashtable();


        return htPP;

    }
}