using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Threading;

public class NoReSetDEL_160907000013
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

                
                alsql.Add("delete ZZZ_xiaoshoudingdan_sb  where FCS_FCID=@FCID_" + d + " and (select top 1 FCzhuangtai from ZZZ_xiaoshoudingdan where FCID=@FCID_" + d + " and FCzhuangtai='草稿'  )='草稿'");
                alsql.Add("delete ZZZ_xiaoshoudingdan  where FCID=@FCID_" + d + " and FCzhuangtai='草稿'");
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

        return_ht = I_DBL.RunParam_SQL("select count(FCSID) as sl from ZZZ_xiaoshoudingdan_sb where FCS_FCID=@FCS_FCID", "数据记录", param);

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
    /// 获取订单数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private DataSet get_dd(string FCID)
    {
 

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select  top 1 * from View_ZZZ_xiaoshoudingdan_ex   where FCID=@FCID; select View_ZZZ_xiaoshoudingdan_sb_ex.*,View_ZZZ_wuliao_hb.baoxiuqixian from View_ZZZ_xiaoshoudingdan_sb_ex left join View_ZZZ_wuliao_hb on View_ZZZ_xiaoshoudingdan_sb_ex.FCSbh=View_ZZZ_wuliao_hb.bianhao and View_ZZZ_xiaoshoudingdan_sb_ex.FClb=View_ZZZ_wuliao_hb.lb where FCS_FCID=@FCID;  ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb1 = ((DataSet)return_ht["return_ds"]).Tables[0].Copy();
            DataTable redb2 = ((DataSet)return_ht["return_ds"]).Tables[1].Copy();
            DataSet dsre = new DataSet();
            redb1.TableName = "订单主表";
            redb2.TableName = "订单物料表";
            dsre.Tables.Add(redb1);
            dsre.Tables.Add(redb2);
            return dsre;
        }
        else
        {
            return null;
        }


        return null;
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

        return_ht = I_DBL.RunParam_SQL("select count(FCSID) as sl from ZZZ_xiaoshoudingdan_sb where FCS_FCID=@FCS_FCID and FCSsl<1", "数据记录", param);

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
                        alsql.Add("UPDATE ZZZ_xiaoshoudingdan SET  FCzhuangtai='提交',FCshenqingshijian=getdate()  where FCzhuangtai='草稿' and  FCID =@FCID_" + d);
                      
                        
                        
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



        return "提交订单失败，发生错误";
    }

    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_xiatui(DataTable parameter_forUI)
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


     
            string ids = ht_forUI["xuanzhongzhi"].ToString().Trim();

            //开始处理单据，生成语句。
            //提取销售订单数据
            DataSet dsdd = get_dd(ids);

            //状态为结单的不允许生成
            if (dsdd.Tables["订单主表"].Rows[0]["FCzhuangtai"].ToString() != "提交")
            {
                return "生成失败：订单为草稿或结单状态时，不允许生成销售发货单！";
            }

    
            //分析订单主表，生成插入语句
            //以可排序guid方式生成
            string guid = CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo");
            param.Add("@FCID", guid);
            param.Add("@FC_YYID", dsdd.Tables["订单主表"].Rows[0]["FC_YYID"].ToString());
            param.Add("@FCsmaz", "是");
            param.Add("@FCbumen", dsdd.Tables["订单主表"].Rows[0]["FCbumen"].ToString());
            param.Add("@FC_erp_danbie", "销售出库单");
            param.Add("@FCshenqingren", dsdd.Tables["订单主表"].Rows[0]["FCshenqingren"].ToString());
            param.Add("@FCbeizhu", dsdd.Tables["订单主表"].Rows[0]["FCbeizhu"].ToString());

            param.Add("@FCjisongdizhi", "");
            param.Add("@FClianxifangshi", "");
            param.Add("@FCshoujianren", "");

            param.Add("@FCglxsdd", dsdd.Tables["订单主表"].Rows[0]["FCID"].ToString());

            alsql.Add("INSERT INTO ZZZ_xiaoshoufahuo(FCID,FC_YYID,FCsmaz,FCbumen,FC_erp_danbie,FCbeizhu,FCshenqingren,FCshenqingshijian,FCzhuangtai,FCjisongdizhi,FClianxifangshi,FCshoujianren,FCglxsdd) VALUES(@FCID,@FC_YYID,@FCsmaz,@FCbumen,@FC_erp_danbie,@FCbeizhu,@FCshenqingren,getdate(),'草稿',@FCjisongdizhi,@FClianxifangshi,@FCshoujianren,@FCglxsdd)");

            //分析订单子表，生成插入语句
            param.Add("@sub_" + "MainID", guid); //隶属主表id
            for (int i = 0; i < dsdd.Tables["订单物料表"].Rows.Count; i++)
            {
                double FCSsl = Convert.ToDouble(dsdd.Tables["订单物料表"].Rows[i]["FCSsl"]);
                double FCyfhsl = Convert.ToDouble(dsdd.Tables["订单物料表"].Rows[i]["FCyfhsl"]);
                double FCdanjia = Convert.ToDouble(dsdd.Tables["订单物料表"].Rows[i]["FCdanjia"]);
                double FCjine = Convert.ToDouble(dsdd.Tables["订单物料表"].Rows[i]["FCjine"]);
                double dr_FCSsl = FCSsl - FCyfhsl;
                FCjine = dr_FCSsl * FCdanjia;
                param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo_sb"));

                param.Add("@sub_" + "FCSbh" + "_" + i, dsdd.Tables["订单物料表"].Rows[i]["FCSbh"].ToString());
                param.Add("@sub_" + "FClb" + "_" + i, dsdd.Tables["订单物料表"].Rows[i]["FClb"].ToString());
                param.Add("@sub_" + "FCSsl" + "_" + i, dr_FCSsl.ToString());
                param.Add("@sub_" + "FCbxqx" + "_" + i, dsdd.Tables["订单物料表"].Rows[i]["baoxiuqixian"].ToString());
                param.Add("@sub_" + "FCdanjia" + "_" + i, dsdd.Tables["订单物料表"].Rows[i]["FCdanjia"].ToString());
                param.Add("@sub_" + "FCjine" + "_" + i, FCjine.ToString());
                param.Add("@sub_" + "FCSbz" + "_" + i, dsdd.Tables["订单物料表"].Rows[i]["FCSbz"].ToString());



                string INSERTsql = "INSERT INTO ZZZ_xiaoshoufahuo_sb ( FCSID, FCS_FCID, FCSbh,FClb, FCSsl,FCbxqx,FCdanjia,FCjine, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ",@sub_" + "FClb" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + " , @sub_" + "FCbxqx" + "_" + i + "  , @sub_" + "FCdanjia" + "_" + i + "  , @sub_" + "FCjine" + "_" + i + " , @sub_" + "FCSbz" + "_" + i + "  )";
                alsql.Add(INSERTsql);



            }

            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "生成销售发货单完成，默认为草稿状态。<br/><a href='/adminht/corepage/xsfh/fc_shenqing.aspx?idforedit="+ guid + "&fff=1&ywlx=bianjicaogao&showinfo=1'>点击这里编辑生成的单据草稿！</a>";
            }

        }



        return "生成销售发货单失败，发生错误";
    }

}

