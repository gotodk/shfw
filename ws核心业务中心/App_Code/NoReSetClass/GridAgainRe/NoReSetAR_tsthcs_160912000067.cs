using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetAR_tsthcs_160912000067
{

    /// <summary>
    /// 二次处理数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_AR(DataSet ds_page, Dictionary<string, string> dic_mysearchtop, DataTable parameter_forUI)
    {

        //对ds_page进行二次处理
        string tbname_str = ds_page.Tables[0].Rows[0]["search_tbname"].ToString();
        if (dic_mysearchtop.ContainsKey("tsthcs_shijian1") && dic_mysearchtop.ContainsKey("tsthcs_shijian2"))
        {
            tbname_str = tbname_str.Replace("{服务报告时间条件}", " Gaddtime>='" + dic_mysearchtop["tsthcs_shijian1"] + " 00:00:00.000' and Gaddtime<='" + dic_mysearchtop["tsthcs_shijian2"] + " 23:59:59.999' ");
            tbname_str = tbname_str.Replace("{工作计划时间条件}", " Gaddtime>='" + dic_mysearchtop["tsthcs_shijian1"] + " 00:00:00.000' and Gaddtime<='" + dic_mysearchtop["tsthcs_shijian2"] + " 23:59:59.999' ");
            tbname_str = tbname_str.Replace("{考勤城市时间条件}", " Ktime>='" + dic_mysearchtop["tsthcs_shijian1"] + " 00:00:00.000' and Ktime<='" + dic_mysearchtop["tsthcs_shijian2"] + " 23:59:59.999' ");
        }
        else
        {
            tbname_str = tbname_str.Replace("{服务报告时间条件}", " 1=1 ");
            tbname_str = tbname_str.Replace("{工作计划时间条件}", " 1=1 ");
            tbname_str = tbname_str.Replace("{考勤城市时间条件}", " 1=1 ");
        }
     

        ds_page.Tables[0].Rows[0]["search_tbname"] = tbname_str;
        return ds_page;
    }


}
 
