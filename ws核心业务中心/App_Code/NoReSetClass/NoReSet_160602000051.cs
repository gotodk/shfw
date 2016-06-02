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

public class NoReSet_160602000051
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
    /// 增加数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_ADD(DataTable parameter_forUI)
    {

        //库存调整单即刻生效，状态为"已审核",审核人为登录账号。

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
        //rid, rdb,  rbzr,   rzhuangtai, rbeizhu
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_C_record");
        param.Add("@rid", guid);
        param.Add("@rdb", ht_forUI["rdb"].ToString());
        param.Add("@rbzr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@rshr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@rzhuangtai", "已审核");
        param.Add("@rbeizhu", ht_forUI["rbeizhu"].ToString());
        

        alsql.Add("INSERT INTO  ZZZ_C_record(rid, rdb,  rbzr, rshr ,rshenheshijian,  rzhuangtai, rbeizhu) VALUES(@rid, @rdb,  @rbzr, @rshr, getdate(),  @rzhuangtai, @rbeizhu)");

        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160602000837";
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

        //更新库存


        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！";
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
         



        return null;
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
