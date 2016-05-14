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

public class NoReSet_160513000045
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

        return_ht = I_DBL.RunParam_SQL("select top 1 FCzhuangtai from ZZZ_fanchang where FCID=@FCID", "数据记录", param);

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
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_fanchang");
        param.Add("@FCID", guid);
        param.Add("@FC_YYID", ht_forUI["FC_YYID"].ToString());
        param.Add("@FCshenqingren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
 
       


        alsql.Add("INSERT INTO ZZZ_fanchang(FCID,FC_YYID,FCshenqingren,FCshenqingshijian,FCzhuangtai) VALUES(@FCID,@FC_YYID,@FCshenqingren,getdate(),'草稿')");


        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160514000780";
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
            param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_fanchang_sb"));

            param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
            param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["返厂数量"].ToString());
            param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());
       


            string INSERTsql = "INSERT INTO ZZZ_fanchang_sb ( FCSID, FCS_FCID, FCSbh, FCSsl, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + ", @sub_" + "FCSbz" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }


        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存草稿成功！";
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
            alsql.Add("UPDATE  ZZZ_fanchang SET FC_YYID=@FC_YYID  where FCID =@FCID ");


            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-160514000780";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_fanchang_sb where  FCS_FCID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_fanchang_sb"));
                }
                else
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
                param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["返厂数量"].ToString());
                param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());


                string INSERTsql = "INSERT INTO ZZZ_fanchang_sb ( FCSID, FCS_FCID, FCSbh, FCSsl, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + ", @sub_" + "FCSbz" + "_" + i + "  )";
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

            //只更新发货信息，不更新子表
            param.Add("@FCwuliudan", ht_forUI["FCwuliudan"].ToString());
            param.Add("@FCwuliugongsi", ht_forUI["FCwuliugongsi"].ToString());
            param.Add("@FCfahuoren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
 
            param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
            param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
            param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

            alsql.Add("UPDATE  ZZZ_fanchang SET FCwuliudan=@FCwuliudan,FCwuliugongsi=@FCwuliugongsi,FCfahuoren=@FCfahuoren,FCfahuoshijian=getdate(),FCjisongdizhi=@FCjisongdizhi,FClianxifangshi=@FClianxifangshi,FCshoujianren=@FCshoujianren,FCzhuangtai='在途'  where FCID =@FCID ");
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

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_fanchang_ex where FCID=@FCID", "数据记录", param);

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
