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

public class NoReSet_160817000007
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
    public string check_wxzt(string FCID)
    {

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);
        return_ht = I_DBL.RunParam_SQL("select top 1 FCwx_zt from ZZZ_fanchang where FCID=@FCID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                return redb.Rows[0]["FCwx_zt"].ToString();
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

        //验证权限

        //状态验证
        string wxzt = check_wxzt(ht_forUI["idforedit"].ToString());
        if (wxzt == "提交" || wxzt == "审核")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败：只有未提交和草稿状态允许编辑。";
            return dsreturn;
        }

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        param.Add("@FCID", ht_forUI["idforedit"].ToString());
        param.Add("@FCwx_yijian", ht_forUI["FCwx_yijian"].ToString());
        param.Add("@FCwx_cjr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@FCwx_zt", "草稿");
        param.Add("@FCwx_zwxfy", ht_forUI["FCwx_zwxfy"].ToString());

        alsql.Add("UPDATE ZZZ_fanchang SET FCwx_txsj = getdate(),FCwx_yijian=@FCwx_yijian,FCwx_zwxfy=@FCwx_zwxfy,FCwx_cjr=@FCwx_cjr+'('+(select top 1 isnull(xingming,'')  from ZZZ_userinfo where UAid=@FCwx_cjr )+')',FCwx_zt=@FCwx_zt where FCID=@FCID and  FCwx_zt in ('未填写','草稿')");


        //遍历子表，先删除，再插入，已有主键的不重新生成。
        string zibiao_gts_id = "grid-table-subtable-160817000101";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete ZZZ_fanchang_wxsypj where  FCW_FCID = @sub_" + "MainID");
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "FCWID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_fanchang_wxsypj"));
            }
            else
            {
                param.Add("@sub_" + "FCWID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
            }


            param.Add("@sub_" + "FCWbh" + "_" + i, subdt.Rows[i]["配件物料编码"].ToString());
            param.Add("@sub_" + "FCWmingcheng" + "_" + i, subdt.Rows[i]["配件物料名称"].ToString());
            param.Add("@sub_" + "FCWguige" + "_" + i, subdt.Rows[i]["配件物料规格"].ToString());
            param.Add("@sub_" + "FCWsl" + "_" + i, subdt.Rows[i]["配件数量"].ToString());
            param.Add("@sub_" + "FCWdj" + "_" + i, subdt.Rows[i]["单价"].ToString());
            param.Add("@sub_" + "FCWzj" + "_" + i, subdt.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "FCWbz" + "_" + i, subdt.Rows[i]["备注"].ToString());

            string INSERTsql = "INSERT INTO ZZZ_fanchang_wxsypj ( FCWID, FCW_FCID, FCWbh,   FCWmingcheng,  FCWguige, FCWsl,FCWdj,FCWzj,FCWbz ) VALUES(@sub_" + "FCWID" + "_" + i + ", @sub_MainID, @sub_" + "FCWbh" + "_" + i + ", @sub_" + "FCWmingcheng" + "_" + i + ", @sub_" + "FCWguige" + "_" + i + ", @sub_" + "FCWsl" + "_" + i + ", @sub_" + "FCWdj" + "_" + i + " , @sub_" + "FCWzj" + "_" + i + " , @sub_" + "FCWbz" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }


        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！{" + ht_forUI["idforedit"].ToString() + "}";
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
