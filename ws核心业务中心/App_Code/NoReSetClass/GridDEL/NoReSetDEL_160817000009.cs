using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_160817000009
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
   
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {

                    param.Add("@FCID_" + d, ids[d]);


                    alsql.Add("UPDATE ZZZ_fanchang SET  FCwx_zt='提交'  where FCwx_zt='草稿' and FCID =@FCID_" + d);
 


                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {
                return "提交维修情况完成！";
            }

        }



        return "提交维修情况失败，发生错误";
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
 
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    
                    param.Add("@FCID_" + d, ids[d]);
                    alsql.Add("UPDATE ZZZ_fanchang SET  FCwx_zt='审核',FCwx_shsj=getdate()  where FCwx_zt='提交' and FCID =@FCID_" + d);
 
                

                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {
                return "维修单审核完成！";
            }

        }



        return "维修单审核失败，发生错误";
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
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    param.Add("@FCID_" + d, ids[d]);
                    alsql.Add("UPDATE ZZZ_fanchang SET  FCwx_zt='草稿',FCwx_shsj=getdate()  where FCwx_zt='提交' and FCID =@FCID_" + d);
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "维修单驳回完成！";
            }

        }



        return "维修单驳回失败，发生错误";
    }


    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_zuizhongchuli(DataTable parameter_forUI)
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
            param.Add("@FCwx_zzcl", ht_forUI["ddtj_FCwx_zzcl"].ToString());
            param.Add("@FCwx_hqd", ht_forUI["ddtj_FCwx_hqd"].ToString());
            param.Add("@FCwx_sfcd", ht_forUI["ddtj_FCwx_sfcd"].ToString());
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    param.Add("@FCID_" + d, ids[d]);
                    if (ht_forUI["ddtj_FCwx_sfcd"].ToString() == "是")
                    {

                    }
                    alsql.Add("UPDATE ZZZ_fanchang SET  FCwx_sfcd=@FCwx_sfcd,FCwx_zzcl=@FCwx_zzcl,FCwx_hqd=@FCwx_hqd where FCwx_zt='审核' and (FCwx_sfcd is null or FCwx_sfcd= '' or FCwx_sfcd= '否') and FCID =@FCID_" + d);
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "维修单存档完成！";
            }

        }



        return "维修单存档失败，发生错误";
    }



}

