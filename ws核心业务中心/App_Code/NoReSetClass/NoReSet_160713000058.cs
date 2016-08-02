using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Web.Script.Serialization;

public class NoReSet_160713000058
{
 

    /// <summary>
    /// 初始化返回值数据集,执行结果只有两种ok和err(大多数情况是这个标准)
    /// </summary>
    /// <returns></returns>
    private DataSet initReturnDataSet()
    {
        DataSet ds = new DataSet();
        DataTable auto2 = new DataTable();
        auto2.TableName = "返回值单条";
        auto2.Columns.Add("执行结果");
        auto2.Columns.Add("提示文本");
        auto2.Columns.Add("附件信息1");
        auto2.Columns.Add("附件信息2");
        auto2.Columns.Add("附件信息3");
        auto2.Columns.Add("附件信息4");
        auto2.Columns.Add("附件信息5");
        ds.Tables.Add(auto2);
        return ds;
    }



    /// <summary>
    /// 检查单据状态
    /// </summary>
    /// <returns></returns>
    public string check_zhuangtai(string FCID)
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
    /// 增加数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_ADD(DataTable parameter_forUI)
    {

        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }
        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });
        //参数合法性各种验证，这里要根据具体业务逻辑处理

        //开始真正的处理，根据业务逻辑操作数据库
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo");
        param.Add("@FCID", guid);
        param.Add("@FC_YYID", ht_forUI["FC_YYID"].ToString());
        param.Add("@FCsmaz", ht_forUI["FCsmaz"].ToString());
        param.Add("@FCbumen", ht_forUI["FCbumen"].ToString());
        param.Add("@FC_erp_danbie", ht_forUI["FC_erp_danbie"].ToString());
        param.Add("@FCshenqingren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@FCbeizhu", ht_forUI["FCbeizhu"].ToString());

        param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
        param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
        param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

        alsql.Add("INSERT INTO ZZZ_xiaoshoufahuo(FCID,FC_YYID,FCsmaz,FCbumen,FC_erp_danbie,FCbeizhu,FCshenqingren,FCshenqingshijian,FCzhuangtai,FCjisongdizhi,FClianxifangshi,FCshoujianren) VALUES(@FCID,@FC_YYID,@FCsmaz,@FCbumen,@FC_erp_danbie,@FCbeizhu,@FCshenqingren,getdate(),'草稿',@FCjisongdizhi,@FClianxifangshi,@FCshoujianren)");


        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160713000991";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        param.Add("@sub_" + "MainID", guid); //隶属主表id

        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo_sb"));

            param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
            param.Add("@sub_" + "FClb" + "_" + i, subdt.Rows[i]["物料类别"].ToString());
            param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["发货数量"].ToString());
            param.Add("@sub_" + "FCbxqx" + "_" + i, subdt.Rows[i]["保修期限"].ToString());
            param.Add("@sub_" + "FCdanjia" + "_" + i, subdt.Rows[i]["单价"].ToString());
            param.Add("@sub_" + "FCjine" + "_" + i, subdt.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());
       


            string INSERTsql = "INSERT INTO ZZZ_xiaoshoufahuo_sb ( FCSID, FCS_FCID, FCSbh,FClb, FCSsl,FCbxqx,FCdanjia,FCjine, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ",@sub_" + "FClb" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + " , @sub_" + "FCbxqx" + "_" + i + "  , @sub_" + "FCdanjia" + "_" + i + "  , @sub_" + "FCjine" + "_" + i + " , @sub_" + "FCSbz" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }


        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存草稿成功！注意您需要提交后才能生效！{" + guid + "}";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }
        return dsreturn;
    }

    /// <summary>
    /// 编辑数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略
        if (ht_forUI["idforedit"].ToString().Trim() == "")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有明确的修改目标！";
            return dsreturn;
        }


        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        param.Add("@FCID", ht_forUI["idforedit"].ToString());


        if (ht_forUI["ywlx_yincang"].ToString() == "bianjicaogao")
        {

            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "草稿")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有草稿状态才允许编辑。";
                return dsreturn;
            }

            //只有草稿可以编辑

            param.Add("@FC_YYID", ht_forUI["FC_YYID"].ToString());
            param.Add("@FCsmaz", ht_forUI["FCsmaz"].ToString());
            param.Add("@FCbumen", ht_forUI["FCbumen"].ToString());
            param.Add("@FC_erp_danbie", ht_forUI["FC_erp_danbie"].ToString());
            param.Add("@FCbeizhu", ht_forUI["FCbeizhu"].ToString());

            param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
            param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
            param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

            alsql.Add("UPDATE  ZZZ_xiaoshoufahuo SET FC_YYID=@FC_YYID,FCsmaz=@FCsmaz,FCbumen=@FCbumen,FC_erp_danbie=@FC_erp_danbie,FCbeizhu=@FCbeizhu,FCjisongdizhi=@FCjisongdizhi,FClianxifangshi=@FClianxifangshi,FCshoujianren=@FCshoujianren  where FCID =@FCID ");


            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-160713000991";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_xiaoshoufahuo_sb where  FCS_FCID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo_sb"));
                }
                else
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
                param.Add("@sub_" + "FClb" + "_" + i, subdt.Rows[i]["物料类别"].ToString());
                param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["发货数量"].ToString());
                param.Add("@sub_" + "FCbxqx" + "_" + i, subdt.Rows[i]["保修期限"].ToString());
                param.Add("@sub_" + "FCdanjia" + "_" + i, subdt.Rows[i]["单价"].ToString());
                param.Add("@sub_" + "FCjine" + "_" + i, subdt.Rows[i]["金额"].ToString());
                param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());



                string INSERTsql = "INSERT INTO ZZZ_xiaoshoufahuo_sb ( FCSID, FCS_FCID, FCSbh,FClb, FCSsl,FCbxqx,FCdanjia,FCjine, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ",@sub_" + "FClb" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + " , @sub_" + "FCbxqx" + "_" + i + "  , @sub_" + "FCdanjia" + "_" + i + "  , @sub_" + "FCjine" + "_" + i + " , @sub_" + "FCSbz" + "_" + i + "  )";
                alsql.Add(INSERTsql);
            }

        }
        if (ht_forUI["ywlx_yincang"].ToString() == "fahuo")
        {
            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "审核")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有审核状态才允许录入发货信息。";
                return dsreturn;
            }

            if (ht_forUI["FCjisongdizhi"].ToString().Trim() == "" || ht_forUI["FClianxifangshi"].ToString().Trim() == "" || ht_forUI["FCshoujianren"].ToString().Trim() == "")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，收货人的姓名地址联系方式必填。";
                return dsreturn;
            }

            //只更新发货信息，不更新子表
            param.Add("@FCwuliudan", ht_forUI["FCwuliudan"].ToString());
            param.Add("@FCwuliugongsi", ht_forUI["FCwuliugongsi"].ToString());
            param.Add("@FCfahuoren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
 
            param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
            param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
            param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

            param.Add("@FCfahuobeizhu", ht_forUI["FCfahuobeizhu"].ToString());

            alsql.Add("UPDATE  ZZZ_xiaoshoufahuo SET FCwuliudan=@FCwuliudan,FCwuliugongsi=@FCwuliugongsi,FCfahuoren=@FCfahuoren,FCfahuoshijian=getdate(),FCjisongdizhi=@FCjisongdizhi,FClianxifangshi=@FClianxifangshi,FCshoujianren=@FCshoujianren,FCzhuangtai='在途',FCfahuobeizhu=@FCfahuobeizhu  where FCID =@FCID ");
            //同步在联系人中增加一条联系人，重名的不插入
            string KID = CombGuid.GetMewIdFormSequence("ZZZ_KHLXR");
            param.Add("@KID", KID);
            param.Add("@K_YYID", ht_forUI["FC_YYID"].ToString());
            param.Add("@KKS", ht_forUI["FCbumen"].ToString());
            alsql.Add("if not exists(select top 1 KID from ZZZ_KHLXR where Klianiren=@FCshoujianren) begin insert into ZZZ_KHLXR (KID,K_YYID,KKS,Klianiren,Kzhicheng,Kxingbie,Kdianhua,Kbeizhu) values(@KID,@K_YYID,@KKS,@FCshoujianren,'','男',@FClianxifangshi,@FCjisongdizhi) end ");







            //遍历零配件子表，也反写零件价格。 并且在设备档案表零件子表中插入一条信息
            //遍历子表， 准备反写 (零件信息)
            string zibiao_lj_id = "grid-table-subtable-160713000991";
            DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            for (int i = 0; i < subdt_lj.Rows.Count; i++)
            {

                if (subdt_lj.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    //新增的不反写
                }
                else
                {
                    //原有子表编号的，更新
                    param.Add("@sub_" + "FCSID" + "_" + i, subdt_lj.Rows[i]["隐藏编号"].ToString());

                    param.Add("@sub_" + "FCbxqx" + "_" + i, subdt_lj.Rows[i]["保修期限"].ToString());
                    param.Add("@sub_" + "FCdanjia" + "_" + i, subdt_lj.Rows[i]["单价"].ToString());
                    param.Add("@sub_" + "FCjine" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());

                    string upupsql = "update ZZZ_xiaoshoufahuo_sb set FCbxqx=@sub_" + "FCbxqx" + "_" + i + " ,FCdanjia=@sub_" + "FCdanjia" + "_" + i + " ,FCjine=@sub_" + "FCjine" + "_" + i + " where FCSID=@sub_" + "FCSID" + "_" + i;
                    alsql.Add(upupsql);
 

                }
            }



        }




        if (ht_forUI["ywlx_yincang"].ToString() == "shenhe")
        {
            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "提交")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有提交状态才允许进行审核或驳回。";
                return dsreturn;
            }

            if (ht_forUI.Contains("shenhe_yincang"))
            {
                //判断是审核还是驳回
                if (ht_forUI["shenhe_yincang"].ToString() == "审核通过")
                {
                    param.Add("@FCshenheren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
                    alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='审核',FCshenheren=@FCshenheren,FCshenheshijian=getdate()  where FCzhuangtai='提交' and FCID =@FCID");
                }
                if (ht_forUI["shenhe_yincang"].ToString() == "驳回")
                {
                    alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='草稿'  where FCzhuangtai='提交' and FCID =@FCID");
                }
            }
            else
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，审核选项必须选择。";
                return dsreturn;
            }
           
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，修改失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }

    /// <summary>
    /// 编辑数据前获取数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_xiaoshoufahuo_ex where FCID=@FCID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有找到指定数据!";
                return dsreturn;
            }

            dsreturn.Tables.Add(redb);




            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，获取失败：" + return_ht["return_errmsg"].ToString();
        }


        return dsreturn;
    }


}
