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

public class NoReSet_160519000046
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
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_KHDA_wcj");
        param.Add("@YYID", guid);
        param.Add("@YYname", ht_forUI["YYname"].ToString());

        param.Add("@yhb_city_Promary_diquxian", ht_forUI["yhb_city_Promary_YYshengshiqu"].ToString());
        param.Add("@yhb_city_City_diquxian", ht_forUI["yhb_city_City_YYshengshiqu"].ToString());
        param.Add("@yhb_city_Qu_diquxian", ht_forUI["yhb_city_Qu_YYshengshiqu"].ToString());

        param.Add("@YYdizhi", ht_forUI["YYdizhi"].ToString());
        param.Add("@YYdianhua", ht_forUI["YYdianhua"].ToString());
        param.Add("@YYchuanzhen", ht_forUI["YYchuanzhen"].ToString());
        param.Add("@YYkaipiao", ht_forUI["YYkaipiao"].ToString());
        param.Add("@YYerp", ht_forUI["YYerp"].ToString());

        param.Add("@YYquyudaima", ht_forUI["YYquyudaima"].ToString());
        param.Add("@YYzuobiao", ht_forUI["YYzuobiao"].ToString());

        param.Add("@YYssbumen", ht_forUI["YYssbumen"].ToString());
        param.Add("@YYfuwufuzeren", ht_forUI["YYfuwufuzeren"].ToString());

        param.Add("@YYbeizhu", ht_forUI["YYbeizhu"].ToString());

        alsql.Add("INSERT INTO  ZZZ_KHDA_wcj(YYID, YYname, yhb_city_Promary_diquxian, yhb_city_City_diquxian, yhb_city_Qu_diquxian, YYdizhi,     YYdianhua, YYchuanzhen, YYkaipiao, YYerp, YYquyudaima, YYzuobiao,  YYbeizhu,YYssbumen,YYfuwufuzeren) VALUES(@YYID, @YYname, @yhb_city_Promary_diquxian, @yhb_city_City_diquxian, @yhb_city_Qu_diquxian, @YYdizhi,     @YYdianhua, @YYchuanzhen, @YYkaipiao, @YYerp, @YYquyudaima, @YYzuobiao , @YYbeizhu,@YYssbumen,@YYfuwufuzeren)");


        //对于未成交客户的新增，屏蔽子表的处理
        bool gosub = false;
        if (gosub)
        {

            //遍历子表， 插入 
            string zibiao_lxr_id = "grid-table-subtable-160519000795";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_lxr_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_lxr_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", guid); //隶属主表id
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                param.Add("@sub_" + "KID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_KHLXR_wcj"));

                param.Add("@sub_" + "KKS" + "_" + i, subdt.Rows[i]["科室"].ToString());
                param.Add("@sub_" + "Klianiren" + "_" + i, subdt.Rows[i]["姓名"].ToString());
                param.Add("@sub_" + "Kzhicheng" + "_" + i, subdt.Rows[i]["职位"].ToString());
                param.Add("@sub_" + "Kxingbie" + "_" + i, subdt.Rows[i]["性别"].ToString());
                param.Add("@sub_" + "Kdianhua" + "_" + i, subdt.Rows[i]["电话"].ToString());
                param.Add("@sub_" + "Kbeizhu" + "_" + i, subdt.Rows[i]["备注"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_KHLXR_wcj (  KID, K_YYID, KKS, Klianiren, Kzhicheng, Kxingbie, Kdianhua, Kbeizhu ) VALUES(@sub_" + "KID" + "_" + i + ", @sub_MainID,@sub_" + "KKS" + "_" + i + ", @sub_" + "Klianiren" + "_" + i + ", @sub_" + "Kzhicheng" + "_" + i + ", @sub_" + "Kxingbie" + "_" + i + ", @sub_" + "Kdianhua" + "_" + i + ", @sub_" + "Kbeizhu" + "_" + i + "  )";
                alsql.Add(INSERTsql);
            }

        }

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
        param.Add("@YYID", ht_forUI["idforedit"].ToString());
        param.Add("@YYname", ht_forUI["YYname"].ToString());

        param.Add("@yhb_city_Promary_diquxian", ht_forUI["yhb_city_Promary_YYshengshiqu"].ToString());
        param.Add("@yhb_city_City_diquxian", ht_forUI["yhb_city_City_YYshengshiqu"].ToString());
        param.Add("@yhb_city_Qu_diquxian", ht_forUI["yhb_city_Qu_YYshengshiqu"].ToString());

        param.Add("@YYdizhi", ht_forUI["YYdizhi"].ToString());
        param.Add("@YYdianhua", ht_forUI["YYdianhua"].ToString());
        param.Add("@YYchuanzhen", ht_forUI["YYchuanzhen"].ToString());
        param.Add("@YYkaipiao", ht_forUI["YYkaipiao"].ToString());
        param.Add("@YYerp", ht_forUI["YYerp"].ToString());

        param.Add("@YYquyudaima", ht_forUI["YYquyudaima"].ToString());
        param.Add("@YYzuobiao", ht_forUI["YYzuobiao"].ToString());

        param.Add("@YYssbumen", ht_forUI["YYssbumen"].ToString());
        param.Add("@YYfuwufuzeren", ht_forUI["YYfuwufuzeren"].ToString());

        param.Add("@YYbeizhu", ht_forUI["YYbeizhu"].ToString());

        alsql.Add("UPDATE ZZZ_KHDA_wcj SET YYname=@YYname, yhb_city_Promary_diquxian=@yhb_city_Promary_diquxian, yhb_city_City_diquxian=@yhb_city_City_diquxian, yhb_city_Qu_diquxian=@yhb_city_Qu_diquxian, YYdizhi=@YYdizhi,     YYdianhua=@YYdianhua, YYchuanzhen=@YYchuanzhen, YYkaipiao=@YYkaipiao, YYerp=@YYerp, YYquyudaima=@YYquyudaima, YYzuobiao=@YYzuobiao,  YYbeizhu=@YYbeizhu, YYssbumen=@YYssbumen,YYfuwufuzeren=@YYfuwufuzeren where YYID=@YYID ");



        //遍历子表，先删除，再插入，已有主键的不重新生成。
        string zibiao_lxr_id = "grid-table-subtable-160519000795";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_lxr_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_lxr_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete ZZZ_KHLXR_wcj where  K_YYID = @sub_" + "MainID");
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "KID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_KHLXR_wcj"));
            }
            else
            {
                param.Add("@sub_" + "KID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
            }

            param.Add("@sub_" + "KKS" + "_" + i, subdt.Rows[i]["科室"].ToString());
            param.Add("@sub_" + "Klianiren" + "_" + i, subdt.Rows[i]["姓名"].ToString());
            param.Add("@sub_" + "Kzhicheng" + "_" + i, subdt.Rows[i]["职位"].ToString());
            param.Add("@sub_" + "Kxingbie" + "_" + i, subdt.Rows[i]["性别"].ToString());
            param.Add("@sub_" + "Kdianhua" + "_" + i, subdt.Rows[i]["电话"].ToString());
            param.Add("@sub_" + "Kbeizhu" + "_" + i, subdt.Rows[i]["备注"].ToString());


            string INSERTsql = "INSERT INTO ZZZ_KHLXR_wcj (  KID, K_YYID, KKS, Klianiren, Kzhicheng, Kxingbie, Kdianhua, Kbeizhu ) VALUES(@sub_" + "KID" + "_" + i + ", @sub_MainID,@sub_" + "KKS" + "_" + i + ", @sub_" + "Klianiren" + "_" + i + ", @sub_" + "Kzhicheng" + "_" + i + ", @sub_" + "Kxingbie" + "_" + i + ", @sub_" + "Kdianhua" + "_" + i + ", @sub_" + "Kbeizhu" + "_" + i + "  )";
            alsql.Add(INSERTsql);
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
        param.Add("@YYID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *,yhb_city_Promary_diquxian as yhb_city_Promary_YYshengshiqu,yhb_city_City_diquxian as yhb_city_City_YYshengshiqu, yhb_city_Qu_diquxian as yhb_city_Qu_YYshengshiqu from View_ZZZ_KHDA_wcj where YYID=@YYID", "数据记录", param);

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
