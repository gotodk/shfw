using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_sys_demo01
{
 
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_DEL(DataTable parameter_forUI)
        {
            //接收转换参数
            Hashtable ht_forUI = new Hashtable();
            for (int i = 0; i < parameter_forUI.Rows.Count; i++)
            {
                ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
            }

 
            //存在有效目标才删除
            if (ht_forUI.Contains("ajaxrun") && ht_forUI["ajaxrun"].ToString() == "del" && ht_forUI.Contains("oper") && ht_forUI["oper"].ToString() == "del" && ht_forUI.Contains("id") && ht_forUI["id"].ToString().Trim() != "")
            {
                //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

                I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

                Hashtable param = new Hashtable();

                Hashtable return_ht = new Hashtable();
                ArrayList alsql = new ArrayList();


                //删除数据表里的数据 
                string[] delids = ht_forUI["id"].ToString().Split(',');
                for (int d = 0; d < delids.Length; d++)
                {
                    param.Add("@SID_" + d, delids[d]);

                    alsql.Add("delete demouser  where SID=@SID_" + d);
                }


                return_ht = I_DBL.RunParam_SQL(alsql, param);


                if ((bool)(return_ht["return_float"]))
                {

                    ;
                }
                else
                {
                    ;
                }
            }



            return "";
        }


    }
 
