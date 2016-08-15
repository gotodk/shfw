using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_160713000062
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
                param.Add("@FCID_" + d, delids[d]);

                alsql.Add("delete ZZZ_xiaoshoufahuo  where FCID=@FCID_" + d + " and FCzhuangtai='草稿'");
                alsql.Add("delete ZZZ_xiaoshoufahuo_sb  where FCS_FCID=@FCID_" + d + " and (select FCzhuangtai from ZZZ_xiaoshoufahuo where FCID=@FCID_" + d + " and FCzhuangtai='草稿'  )='草稿'");
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
    /// 获取子表数据条数
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private int get_tiaoshu(string FCS_FCID)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCS_FCID", FCS_FCID);

        return_ht = I_DBL.RunParam_SQL("select count(FCSID) as sl from ZZZ_xiaoshoufahuo_sb where FCS_FCID=@FCS_FCID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                return  Convert.ToInt32( redb.Rows[0]["sl"]);
            }
            else
            {
                return 0;
            }

        }
        else
        {
            return 0;
        }



    }


    /// <summary>
    /// 获取子表存在0数量的数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private int get_lingshuliang(string FCS_FCID)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCS_FCID", FCS_FCID);

        return_ht = I_DBL.RunParam_SQL("select count(FCSID) as sl from ZZZ_xiaoshoufahuo_sb where FCS_FCID=@FCS_FCID and FCSsl<1", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                return Convert.ToInt32(redb.Rows[0]["sl"]);
            }
            else
            {
                return 999;
            }

        }
        else
        {
            return 999;
        }



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



            Hashtable hmsg = new Hashtable();
            //更新数据表里的数据 
            string[] ids = ht_forUI["xuanzhongzhi"].ToString().Split(',');
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    

                    //检查错误
                    if (get_tiaoshu(ids[d]) == 0)
                    {
                        hmsg[ids[d]] = "发货物料子表没有数据，应至少一条数据。";
                    }

                    //检查错误，销售发货子表中不能有0数量的数据。
                    if (get_lingshuliang(ids[d]) > 0)
                    {
                        hmsg[ids[d]] = "发货物料子表中存在发货数量为零的物料。";
                    }
                    if (!hmsg.Contains(ids[d]))
                    {
                        param.Add("@FCID_" + d, ids[d]);
                        alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='提交',FCshenqingshijian=getdate()  where FCzhuangtai='草稿' and  FCID =@FCID_" + d);
                    }

                    
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);

            if (hmsg.Count > 0)
            {
                string errmsg = "";
                foreach (DictionaryEntry de in hmsg)
                {
                    errmsg = errmsg + "单号[" + de.Key.ToString() + "]未处理：" + de.Value.ToString() + "<br/>";

                }
                return "处理结束，但存在问题：<br/>"+errmsg;
            }
          

            if ((bool)(return_ht["return_float"]))
            {
                return "提交完成。";
                
            }

        }



        return "提交申请失败，发生错误";
    }



}

