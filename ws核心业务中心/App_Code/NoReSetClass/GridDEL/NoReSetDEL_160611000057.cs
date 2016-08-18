using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_160611000057
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
                param.Add("@GID_" + d, delids[d]);

                
                alsql.Add("delete ZZZ_FWBG_shebei  where (select top 1 Gzhuangtai from ZZZ_FWBG where GID=@GID_" + d + " and Gzhuangtai='保存'  )='保存' and sb_GID=@GID_" + d);
                alsql.Add("delete ZZZ_FWBG_baojing  where (select top 1 Gzhuangtai from ZZZ_FWBG where GID=@GID_" + d + " and Gzhuangtai='保存'  )='保存' and  bj_GID=@GID_" + d);
                alsql.Add("delete ZZZ_FWBG_lingjian  where  (select top 1 Gzhuangtai from ZZZ_FWBG where GID=@GID_" + d + " and Gzhuangtai='保存'  )='保存' and lj_GID=@GID_" + d);
                alsql.Add("delete ZZZ_FWBG  where  Gzhuangtai='保存' and GID=@GID_" + d);
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




    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_tijiaoshenqing(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //存在有效目标才删除
        if (ht_forUI.Contains("zdyname") && ht_forUI["xuanzhongzhi"].ToString() != "")
        {
            if (ht_forUI["xuanzhongzhi"].ToString().Trim() == "")
            {
                return "未选中任何要操作的数据。";
            }
            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            Hashtable param = new Hashtable();

            Hashtable return_ht = new Hashtable();
            ArrayList alsql = new ArrayList();


            //删除数据表里的数据 
            string[] ids = ht_forUI["xuanzhongzhi"].ToString().Split(',');
            param.Add("@nowloginuser", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    param.Add("@GID_" + d, ids[d]);
                    alsql.Add("UPDATE ZZZ_FWBG SET  Gzhuangtai='提交',Gaddtime=getdate()  where Gzhuangtai in ('保存','驳回') and Gtianxieren=@nowloginuser and GID =@GID_" + d);
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "提交完成！";
            }

        }



        return "提交失败，发生错误";
    }


 

}

