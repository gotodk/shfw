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

public class NoReSet_sys_demo_0001
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
        string guid = CombGuid.GetMewIdFormSequence("FUP_FormsDemoDB");
        param.Add("@id", guid);
        param.Add("@fieldtest", ht_forUI["fieldtest"].ToString());



        alsql.Add("INSERT INTO FUP_FormsDemoDB(id ,fieldtest) VALUES(@id ,@fieldtest )");

 
        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-sys_ddmo_0002";
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
            param.Add("@sub_" + "id" + "_" + i, CombGuid.GetMewIdFormSequence("demouser_sub_test"));

            param.Add("@sub_" + "Sname" + "_" + i, subdt.Rows[i]["姓名"].ToString());
            param.Add("@sub_" + "Scity" + "_" + i, subdt.Rows[i]["城市"].ToString());
            param.Add("@sub_" + "Sint" + "_" + i, subdt.Rows[i]["整数"].ToString());
            param.Add("@sub_" + "Sdecimal" + "_" + i, subdt.Rows[i]["小数"].ToString());
            param.Add("@sub_" + "CreateTime" + "_" + i, subdt.Rows[i]["添加日期"].ToString());


            string INSERTsql = "INSERT INTO demouser_sub_test ( id, SID, Sname,   Scity,  Sint, Sdecimal,CreateTime ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_Sname_" + i + ", @sub_Scity_" + i + ", @sub_Sint_" + i + ", @sub_Sdecimal_" + i + ", @sub_CreateTime_" + i + "  )";
            alsql.Add(INSERTsql);
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
        param.Add("@id", ht_forUI["idforedit"].ToString());
        param.Add("@fieldtest", ht_forUI["fieldtest"].ToString());

        alsql.Add("UPDATE FUP_FormsDemoDB SET fieldtest = @fieldtest where id=@id ");


        //遍历子表，先删除，再插入，已有主键的不重新生成。
        string zibiao_gts_id = "grid-table-subtable-sys_ddmo_0002";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete demouser_sub_test where  SID = @sub_" + "MainID");
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "id" + "_" + i,   CombGuid.GetMewIdFormSequence("demouser_sub_test"));
            }
            else
            {
                param.Add("@sub_" + "id" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
            }


            param.Add("@sub_" + "Sname" + "_" + i, subdt.Rows[i]["姓名"].ToString());
            param.Add("@sub_" + "Scity" + "_" + i, subdt.Rows[i]["城市"].ToString());
            param.Add("@sub_" + "Sint" + "_" + i, subdt.Rows[i]["整数"].ToString());
            param.Add("@sub_" + "Sdecimal" + "_" + i, subdt.Rows[i]["小数"].ToString());
            param.Add("@sub_" + "CreateTime" + "_" + i, subdt.Rows[i]["添加日期"].ToString());


            string INSERTsql = "INSERT INTO demouser_sub_test ( id, SID, Sname,   Scity,  Sint, Sdecimal,CreateTime ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_Sname_" + i + ", @sub_Scity_" + i + ", @sub_Sint_" + i + ", @sub_Sdecimal_" + i + ", @sub_CreateTime_" + i + "  )";
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
        param.Add("@id", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 id, fieldtest, mima, xialakuang, xingbie, quanxian, xialakuangduoxuan, zhengshushuliang, erweixiao,  Convert(varchar(10),yigeriqi,120) as yigeriqi,   Convert(varchar(10),riqiqujian1,120) as riqiqujian1,   Convert(varchar(10),riqiqujian2,120) as riqiqujian2, beizhu, bianjiqi, yhb_city_Promary_diquxian,yhb_city_City_diquxian,yhb_city_Qu_diquxian, zhanghao from FUP_FormsDemoDB where id=@id", "数据记录", param);

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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，修改失败：" + return_ht["return_errmsg"].ToString();
        }


        return dsreturn;
    }


}
