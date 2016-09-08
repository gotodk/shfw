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

                
                alsql.Add("delete ZZZ_xiaoshoufahuo_sb  where FCS_FCID=@FCID_" + d + " and (select top 1 FCzhuangtai from ZZZ_xiaoshoufahuo where FCID=@FCID_" + d + " and FCzhuangtai='草稿'  )='草稿'");
                alsql.Add("delete ZZZ_xiaoshoufahuo  where FCID=@FCID_" + d + " and FCzhuangtai='草稿'");
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
    /// 检查单据状态
    /// </summary>
    /// <returns></returns>
    private string check_zhuangtai(string FCID)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select top 1 FCzhuangtai from ZZZ_xiaoshoufahuo where FCID=@FCID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                return redb.Rows[0]["FCzhuangtai"].ToString();
            }
        }
        else
        {
            return "";
        }

    }

    /// <summary>
    /// 获取销售发货子表数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private DataTable get_fhzb(string FCID)
    {


        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select * from ZZZ_xiaoshoufahuo_sb   where FCS_FCID=@FCID ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
            return redb;
        }
        else
        {
            return null;
        }


        return null;
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
    /// 获取子表数据条数
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private int get_yewuyuanbumen(string FCshenheren)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCshenheren", FCshenheren);

        return_ht = I_DBL.RunParam_SQL("SELECT count(Uaid) FROM ZZZ_userinfo WHERE Uaid=@FCshenheren and suoshuquyu in ('122','121','88','120','89')", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0 && redb.Rows[0][0].ToString() == "1")
            {
                return 1;
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
            param.Add("@FCshenheren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
            int ywbmf = get_yewuyuanbumen(ht_forUI["yhbsp_session_uer_UAid"].ToString());
            for (int d = 0; d < ids.Length; d++)
            {

             
                if (ids[d].Trim() != "" && check_zhuangtai(ids[d]) == "草稿")
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

                        //验证状态是否符合审批条件

                        alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='提交',FCshenqingshijian=getdate()  where FCzhuangtai='草稿' and  FCID =@FCID_" + d);
                        //提交人是业务人员的，自动审核通过
                        if(ywbmf == 1)
                        {
                            alsql.Add(" UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='审核',FCshenheren=@FCshenheren,FCshenheshijian=getdate()  where FCzhuangtai='提交' and FCID =@FCID_" + d + " ");
                            //并且拆分子表
                            //审核后，取出设备类型的子表，分析拆分成数量1
                            DataTable dtz = get_fhzb(param["@FCID_" + d].ToString());
                            //如果是设备，并且数量大于1，进行拆分处理
                            for (int p = 0; p < dtz.Rows.Count; p++)
                            {

                                string FCSID = dtz.Rows[p]["FCSID"].ToString();
                                string FCS_FCID = dtz.Rows[p]["FCS_FCID"].ToString();
                                string FCSbh = dtz.Rows[p]["FCSbh"].ToString();
                                string FClb = dtz.Rows[p]["FClb"].ToString();
                                double FCSsl = Convert.ToDouble(dtz.Rows[p]["FCSsl"]);
                                string FCbxqx = dtz.Rows[p]["FCbxqx"].ToString();
                                double FCdanjia = Convert.ToDouble(dtz.Rows[p]["FCdanjia"]);
                                double FCjine = Convert.ToDouble(dtz.Rows[p]["FCjine"]);
                                string FCSbz = dtz.Rows[p]["FCSbz"].ToString();
                                string FCbeuserd = dtz.Rows[p]["FCbeuserd"].ToString();
                                if (FClb == "设备" && FCSsl > 1)
                                {
                                    //更新这个数据，把数量变成1，把金额改成单价
                                    alsql.Add("UPDATE ZZZ_xiaoshoufahuo_sb SET  FCSsl=1, FCjine=FCdanjia where FCSID ='" + FCSID + "'");

                                    //循环原数量-1次，插入拆分后的数据。从修改后的单号提取插入即可。
                                    for (int c = 1; c < FCSsl; c++)
                                    {
                                        alsql.Add("Insert Into ZZZ_xiaoshoufahuo_sb(FCSID, FCS_FCID, FCSbh, FClb, FCSsl, FCbxqx, FCdanjia, FCjine, FCSbz, FCbeuserd) select FCSID+'-" + c.ToString() + "' as FCSID, FCS_FCID, FCSbh, FClb, FCSsl, FCbxqx, FCdanjia, FCjine, FCSbz, FCbeuserd from ZZZ_xiaoshoufahuo_sb   where FCSID='" + FCSID + "'");
                                    }
                                }
                            }
                        }
                        
                        
                    }

                    
                }

            }

            alsql.Add("select ''");
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
                return "提交完成，若申请人是业务员，则已经自动审核通过。";

            }
            else
            {
                return return_ht["return_errmsg"].ToString();
            }

        }



        return "提交申请失败，发生错误";
    }



}

