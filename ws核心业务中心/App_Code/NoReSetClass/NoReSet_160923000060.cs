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

public class NoReSet_160923000060
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

 

        //检查单据状态是否允许生成eas单据，只有审核状态才能生成，并返回发货单需要的数据
        if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "审核")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "生成失败，只有审核状态才允许生成。";
            return dsreturn;
        }
        //根据表单，生成接口需要的数据集

        DataSet dsmain = new DataSet();
        DataTable dbHead = new DataTable();
        dbHead.TableName = "dbHead";
        dbHead.Columns.Add("操作类型", typeof(string));
        dbHead.Columns.Add("本地单号", typeof(string));
        dbHead.Columns.Add("事务类型", typeof(string));
        dbHead.Columns.Add("成本中心", typeof(string));
        dbHead.Columns.Add("部门", typeof(string));
        dbHead.Columns.Add("制单人", typeof(string));
        dbHead.Columns.Add("销售方式", typeof(string));
        dbHead.Columns.Add("销售组织", typeof(string));
        dbHead.Columns.Add("币别", typeof(string));
        dbHead.Columns.Add("汇率", typeof(string));
        dbHead.Columns.Add("订货客户", typeof(string));
        dbHead.Columns.Add("付款类型", typeof(string));
        dbHead.Columns.Add("库存组织", typeof(string));
        dbHead.Columns.Add("控制单元", typeof(string));
        dbHead.Columns.Add("业务类型", typeof(string));
        dsmain.Tables.Add(dbHead);
        DataTable dbEntry = new DataTable();
        dbEntry.TableName = "dbEntry";
        dbEntry.Columns.Add("物料编码", typeof(string));
        dbEntry.Columns.Add("物料类别", typeof(string));
        dbEntry.Columns.Add("发货数量", typeof(string));
        dbEntry.Columns.Add("批号", typeof(string));
        dbEntry.Columns.Add("单价", typeof(string));
        dbEntry.Columns.Add("子表主键", typeof(string));
        dsmain.Tables.Add(dbEntry);

        dsmain.Tables["dbHead"].Rows.Add(new string[]{ "","", "", "", "", "", "", "", "", "", "", "", "", "", "" });
        dsmain.Tables["dbHead"].Rows[0]["操作类型"] = "0";
        dsmain.Tables["dbHead"].Rows[0]["本地单号"] = ht_forUI["idforedit"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["事务类型"] = ht_forUI["shiwuleixing"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["成本中心"] = ht_forUI["chengbenzhongxin"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["部门"] = ht_forUI["bumen"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["制单人"] = ht_forUI["zhidanren"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["销售方式"] = ht_forUI["xiaoshoufangshi"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["销售组织"] = ht_forUI["xiaoshouzuzhi"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["币别"] = ht_forUI["bibie"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["汇率"] = ht_forUI["huilv"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["订货客户"] = ht_forUI["FC_YYID"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["付款类型"] = ht_forUI["fukuanfangshi"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["库存组织"] = ht_forUI["kucunzuzhi"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["控制单元"] = ht_forUI["kongzhidanyuan"].ToString();
        dsmain.Tables["dbHead"].Rows[0]["业务类型"] = ht_forUI["yewuleixing"].ToString();

        

        string zibiao_gts_id = "grid-table-subtable-160923000244";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            dsmain.Tables["dbEntry"].Rows.Add(new string[] {  subdt.Rows[i]["物料编码"].ToString(), subdt.Rows[i]["物料类别"].ToString(), subdt.Rows[i]["发货数量"].ToString(), subdt.Rows[i]["设备档案序列号"].ToString(), subdt.Rows[i]["单价"].ToString(), subdt.Rows[i]["编号"].ToString() });
 
 
        }

        //调用接口生成表单
        DataSet dsre = new DataSet();
        object[] re_dsi = IPC.Call("EAS出库单小伟生成", new object[] { dsmain });
        if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
        {
             dsre = (DataSet)(re_dsi[1]);
            //            3、	返回数据DataSet包含三个DataTable(0, 1, 2)
            //4、	Datatable(0):一行一列，存储返回提示文本内容。
            //5、	Datatable(1)：单据头。
            //6、	Datatable(2)：单据分录，可能会变更分录“批号”。
            if (dsre.Tables[0].Rows[0][0].ToString().IndexOf("单据保存成功") < 0)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "错误err，接口调用成功，但接口返回：" + dsre.Tables[0].Rows[0][0].ToString();
                return dsreturn;
            }
            
            
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "错误err，接口调用失败。" + re_dsi[1].ToString();
            return dsreturn;
         }

        //获取接口执行结果，反写本地子表序列号字段





        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();

        //更新主表的一些信息
        string FCID = ht_forUI["idforedit"].ToString();
        string FC_erp_danbie = ht_forUI["FC_erp_danbie"].ToString();
        string FC_erp_danhao = dsre.Tables[1].Rows[0]["本地单号"].ToString();
        string sqlm = "update ZZZ_xiaoshoufahuo set  FC_erp_danbie='"+ FC_erp_danbie + "', FC_erp_danhao='"+ FC_erp_danhao + "' where FCID='" + FCID + "'";
        alsql.Add(sqlm);

        //循环返回的子表数据，更新序列号
        for (int i = 0; i < dsre.Tables[2].Rows.Count; i++)
        {
            param.Add("@sub_" + "FCscsbxlh" + "_" + i, dsre.Tables[2].Rows[i]["批号"].ToString());
            param.Add("@sub_" + "FCSID" + "_" + i, dsre.Tables[2].Rows[i]["子表主键"].ToString());
            string upupsql = "update ZZZ_xiaoshoufahuo_sb set FCscsbxlh="+ "@sub_" + "FCscsbxlh" + "_" + i + " where FCSID="+ "@sub_" + "FCscsbxlh" + "_" + i+"";
            alsql.Add(upupsql);
        }
        return_ht = I_DBL.RunParam_SQL(alsql, param);
        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "生成单据成功！";
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
