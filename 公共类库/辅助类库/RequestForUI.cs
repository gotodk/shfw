using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// RequestForUI 的摘要说明
/// </summary>
public class RequestForUI
{
	public RequestForUI()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 打包请求
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    public static DataTable Get_parameter_forUI(HttpRequest Request)
    {
        //放入参数
        DataTable parameter_forUI = new DataTable();
        parameter_forUI.TableName = "参数集合";
        parameter_forUI.Columns.Add("参数名");
        parameter_forUI.Columns.Add("参数值");
        parameter_forUI.Columns.Add("参数类型");//实际没用，调试看数据方便用的

        for (int i = 0; i < Request.QueryString.Count; i++)
        {
            parameter_forUI.Rows.Add(new string[] { Request.QueryString.Keys[i].ToString(), Request.QueryString[i].ToString(), "QueryString" });
        }
        for (int i = 0; i < Request.Form.Count; i++)
        {
            parameter_forUI.Rows.Add(new string[] { Request.Form.Keys[i].ToString(), Request.Form[i].ToString(), "FormString" });
        }

        //强制把session中的用户唯一id也放进去，某些情况备用
        parameter_forUI.Rows.Add(new string[] { "yhbsp_session_uer_UAid", UserSession.唯一键, "自动添加" });
        parameter_forUI.Rows.Add(new string[] { "yhbsp_session_uer_btype", UserSessionEX.用户类型, "自动添加" });

        return parameter_forUI;
    }

    /// <summary>
    /// 返回一个分页html脚本
    /// </summary>
    /// <param name="baseurl">基础url</param>
    /// <param name="R_PageNumber">当前页码</param>
    /// <param name="PageCount">总页数</param>
    /// <param name="pnn">显示分页的数量标识</param>
    /// <param name="site">尺寸样式</param>
    /// <returns></returns>
    public static string pagerhtml(string baseurl, int R_PageNumber, int PageCount,int pnn,string site)
    {
        string htmlto = "<ul class='pagination " + site + " pull-right no-margin'>";
        if (R_PageNumber <= 1)
        {
            htmlto = htmlto + "<li class='prev disabled'><a><i class='ace-icon fa fa-angle-double-left'></i></a></li>";
        }
        else
        {

            htmlto = htmlto + "<li class='prev'><a href='" + baseurl + "&R_PageNumber=" + (R_PageNumber - 1).ToString() + "'><i class='ace-icon fa fa-angle-double-left'></i></a></li>";
        }

        for (int i = R_PageNumber - pnn; i <= R_PageNumber + pnn; i++)
        {
            if (i > 0 && i <= PageCount)
            {
                if (i == R_PageNumber)
                {
                    htmlto = htmlto + "<li class='active'><a>" + i.ToString() + "</a></li>";
                }
                else
                {
                    htmlto = htmlto + "<li><a href='" + baseurl + "&R_PageNumber=" + i.ToString() + "'>" + i.ToString() + "</a></li>";
                }

            }


        }

        if (R_PageNumber >= PageCount)
        {
            htmlto = htmlto + "<li class='next disabled'><a><i class='ace-icon fa fa-angle-double-right'></i></a></li>";
        }
        else
        {

            htmlto = htmlto + "<li class='next'><a href='" + baseurl + "&R_PageNumber=" + (R_PageNumber + 1).ToString() + "'><i class='ace-icon fa fa-angle-double-right'></i></a></li>";
        }

        htmlto = htmlto + "</ul>";
        return htmlto;
    }

    public static string pagerhtml_sp(string baseurl, int R_PageNumber, int PageCount)
    {
        string htmlto = "";
        if (R_PageNumber <= 1)
        {
            //
        }
        else
        {

            htmlto = htmlto + "<a href='" + baseurl + "&R_PageNumber=" + (R_PageNumber - 1).ToString() + "'><i class='ace-icon fa fa-arrow-left bigger-110'></i></a>";
        }

        htmlto = htmlto + "&nbsp;&nbsp;&nbsp;<span class='bigger-110'>[" + R_PageNumber + "]</span>&nbsp;&nbsp;&nbsp;";

        if (R_PageNumber >= PageCount)
        {
            //
        }
        else
        {

            htmlto = htmlto + "<a href='" + baseurl + "&R_PageNumber=" + (R_PageNumber + 1).ToString() + "'><i class='ace-icon fa fa-arrow-right bigger-110'></i></a>";
        }

        htmlto = htmlto + "</ul>";
        return htmlto;
    }
   
}