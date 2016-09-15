using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetAR_tsthcs_160914000114
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
            tbname_str = tbname_str.Replace("{日期区间显示}", " '" + dic_mysearchtop["tsthcs_shijian1"] + "' as rq1, '" + dic_mysearchtop["tsthcs_shijian2"] + "' as rq2 ");
            tbname_str = tbname_str.Replace("{过滤日期区间}", " riqi>='" + dic_mysearchtop["tsthcs_shijian1"] + "' and riqi<='" + dic_mysearchtop["tsthcs_shijian2"] + "' ");
         
        }
        else
        {
            //不处理，使其报错
            //tbname_str = tbname_str.Replace("{日期区间显示}", " '2016-09-14' as rq1, '2016-09-14' as rq2 ");
            //tbname_str = tbname_str.Replace("{过滤日期区间}", " riqi>='2016-09-14' and riqi<='2016-09-14' ");
        }
     

        ds_page.Tables[0].Rows[0]["search_tbname"] = tbname_str;
        return ds_page;
    }


}
 
