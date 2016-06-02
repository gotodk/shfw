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

public class NoReSet_160602000052
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
    public string check_zhuangtai(string rid)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@rid", rid);

        return_ht = I_DBL.RunParam_SQL("select top 1 rzhuangtai from ZZZ_C_record where rid=@rid", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                return redb.Rows[0]["rzhuangtai"].ToString();
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

        return null;
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
        param.Add("@rid", ht_forUI["idforedit"].ToString());


        if (ht_forUI["ywlx_yincang"].ToString() == "bianjicaogao")
        {

            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "草稿")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有草稿状态才允许编辑。";
                return dsreturn;
            }

            //只有草稿可以编辑

            param.Add("@rbeizhu", ht_forUI["rbeizhu"].ToString());
            alsql.Add("UPDATE  ZZZ_C_record SET rbeizhu=@rbeizhu  where rid =@rid ");


            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-160602000842";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_C_record_sub where  rid = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                param.Add("@sub_" + "subid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_C_record_sub"));

                string chuku_kw = "";
                string ruku_kw = "";
                if (ht_forUI["rdb"].ToString() == "调拨单" || ht_forUI["rdb"].ToString() == "调整单")
                {
                    chuku_kw = subdt.Rows[i]["出库库位"].ToString();
                    ruku_kw = subdt.Rows[i]["入库库位"].ToString();
                }
                if (ht_forUI["rdb"].ToString() == "申请单")
                {
                    chuku_kw = "";
                    ruku_kw = subdt.Rows[i]["入库库位"].ToString();
                }
                if (ht_forUI["rdb"].ToString() == "退回单")
                {
                    chuku_kw = subdt.Rows[i]["出库库位"].ToString();
                    ruku_kw = "";
                }

                param.Add("@sub_" + "r_chu" + "_" + i, chuku_kw);
                param.Add("@sub_" + "r_ru" + "_" + i, ruku_kw);
                param.Add("@sub_" + "r_cpbh" + "_" + i, subdt.Rows[i]["调整零件"].ToString());
                param.Add("@sub_" + "r_shuliang" + "_" + i, subdt.Rows[i]["调整数量"].ToString());
                param.Add("@sub_" + "r_danwei" + "_" + i, subdt.Rows[i]["单位"].ToString());
                param.Add("@sub_" + "r_pihao" + "_" + i, subdt.Rows[i]["批号"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_C_record_sub (subid, rid, r_chu, r_ru, r_cpbh, r_shuliang, r_danwei ,r_pihao) VALUES(@sub_" + "subid" + "_" + i + ", @sub_MainID, @sub_" + "r_chu" + "_" + i + ", @sub_" + "r_ru" + "_" + i + ", @sub_" + "r_cpbh" + "_" + i + ", @sub_" + "r_shuliang" + "_" + i + ", @sub_" + "r_danwei" + "_" + i + " , @sub_" + "r_pihao" + "_" + i + ")";
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
            param.Add("@rwuliudan", ht_forUI["rwuliudan"].ToString());
            param.Add("@rwuliugongsi", ht_forUI["rwuliugongsi"].ToString());
            param.Add("@rfahuoren", ht_forUI["yhbsp_session_uer_UAid"].ToString());

            param.Add("@rjisongdizhi", ht_forUI["rjisongdizhi"].ToString());
            param.Add("@rlianxifangshi", ht_forUI["rlianxifangshi"].ToString());
            param.Add("@rshoujianren", ht_forUI["rshoujianren"].ToString());

            alsql.Add("UPDATE  ZZZ_C_record SET rwuliudan=@rwuliudan,rwuliugongsi=@rwuliugongsi,rfahuoren=@rfahuoren,rfahuoshijian=getdate(),rjisongdizhi=@rjisongdizhi,rlianxifangshi=@rlianxifangshi,rshoujianren=@rshoujianren,rzhuangtai='在途'  where rid =@rid ");
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
                    param.Add("@rshr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
                    alsql.Add("UPDATE ZZZ_C_record SET  rzhuangtai='审核',rshr=@rshr,rshenheshijian=getdate()  where rzhuangtai='提交' and rid =@rid");
                }
                if (ht_forUI["shenhe_yincang"].ToString() == "驳回")
                {
                    alsql.Add("UPDATE ZZZ_C_record SET  rzhuangtai='草稿'  where rzhuangtai='提交' and rid =@rid");
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
        param.Add("@rid", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *  from View_ZZZ_C_record_ex where rid=@rid", "数据记录", param);

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
