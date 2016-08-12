using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_160506000037
{

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_DEL(DataTable parameter_forUI)
    {
       

        return "";
    }




    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_shenhe(DataTable parameter_forUI)
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
            param.Add("@Gshenheren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            if (ht_forUI.Contains("ddtj_Gshenheyijian"))
            {
                param.Add("@Gshenheyijian", ht_forUI["ddtj_Gshenheyijian"].ToString());
            }
            else
            {
                param.Add("@Gshenheyijian", "");
            }
            
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    param.Add("@GID_" + d, ids[d]);

                    alsql.Add("UPDATE ZZZ_workplan SET  Gzhuangtai='审核',Gshenheren=@Gshenheren,Gshenheshijian=getdate(),Gshenheyijian=@Gshenheyijian  where Gzhuangtai='提交' and GID =@GID_" + d);
                    //自动标注报修申请为已接收。
                    alsql.Add("UPDATE ZZZ_BXSQ SET  Bzhuangtai='已接收',Bjstime=getdate()  where Bzhuangtai='待处理' and BID =(select top 1 G_BID from ZZZ_workplan where GID =@GID_" + d + ")");
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "审核完成！";
            }

        }



        return "审核失败，发生错误";
    }




    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_bohui(DataTable parameter_forUI)
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
            param.Add("@Gshenheren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            if (ht_forUI.Contains("ddtj_Gshenheyijian"))
            {
                param.Add("@Gshenheyijian", ht_forUI["ddtj_Gshenheyijian"].ToString());
            }
            else
            {
                param.Add("@Gshenheyijian", "");
            }
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    param.Add("@GID_" + d, ids[d]);

                    alsql.Add("UPDATE ZZZ_workplan SET  Gzhuangtai='草稿',Gshenheren=@Gshenheren,Gshenheshijian=getdate(),Gshenheyijian=@Gshenheyijian  where Gzhuangtai='提交' and GID =@GID_" + d);
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "驳回完成！";
            }

        }



        return "驳回失败，发生错误";
    }



}

