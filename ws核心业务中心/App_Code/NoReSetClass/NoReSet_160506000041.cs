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

public class NoReSet_160506000041
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
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_workplan");
        param.Add("@GID", guid);
        param.Add("@G_UAID", ht_forUI["G_UAID"].ToString());
        param.Add("@Gbiaoti", ht_forUI["Gbiaoti"].ToString());

        param.Add("@Gtime1", ht_forUI["Gtime1"].ToString());
        param.Add("@Gtime2", ht_forUI["Gtime2"].ToString());

        param.Add("@Gsheng", ht_forUI["yhb_city_Promary_shengshiqu"].ToString());
        param.Add("@Gchengshi", ht_forUI["yhb_city_City_shengshiqu"].ToString());
        param.Add("@Gquyu", ht_forUI["yhb_city_Qu_shengshiqu"].ToString());
         
        param.Add("@Gdidian", ht_forUI["Gdidian"].ToString());
        param.Add("@Gkqleixing", ht_forUI["Gkqleixing"].ToString());
        param.Add("@Grwleixing", ht_forUI["Grwleixing"].ToString());
        param.Add("@Gneirong", ht_forUI["Gneirong"].ToString());
        param.Add("@Gjieguo", ht_forUI["Gjieguo"].ToString());
       


        alsql.Add("INSERT INTO ZZZ_workplan(GID, G_UAID, Gbiaoti, Gtime1, Gtime2, Gsheng, Gchengshi, Gquyu, Gdidian, Gkqleixing, Grwleixing, Gneirong, Gjieguo ) VALUES(@GID, @G_UAID, @Gbiaoti, @Gtime1, @Gtime2, @Gsheng, @Gchengshi, @Gquyu, @Gdidian, @Gkqleixing, @Grwleixing, @Gneirong, @Gjieguo)");


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
        param.Add("@GID", ht_forUI["idforedit"].ToString());

        param.Add("@G_UAID", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@Gbiaoti", ht_forUI["Gbiaoti"].ToString());

        param.Add("@Gtime1", ht_forUI["Gtime1"].ToString());
        param.Add("@Gtime2", ht_forUI["Gtime2"].ToString());

        param.Add("@Gsheng", ht_forUI["yhb_city_Promary_shengshiqu"].ToString());
        param.Add("@Gchengshi", ht_forUI["yhb_city_City_shengshiqu"].ToString());
        param.Add("@Gquyu", ht_forUI["yhb_city_Qu_shengshiqu"].ToString());

        param.Add("@Gdidian", ht_forUI["Gdidian"].ToString());
        param.Add("@Gkqleixing", ht_forUI["Gkqleixing"].ToString());
        param.Add("@Grwleixing", ht_forUI["Grwleixing"].ToString());
        param.Add("@Gneirong", ht_forUI["Gneirong"].ToString());
        param.Add("@Gjieguo", ht_forUI["Gjieguo"].ToString());

        alsql.Add("UPDATE  ZZZ_workplan SET G_UAID=@G_UAID, Gbiaoti=@Gbiaoti, Gtime1=@Gtime1, Gtime2=@Gtime2, Gsheng=@Gsheng, Gchengshi=@Gchengshi, Gquyu=@Gquyu, Gdidian=@Gdidian, Gkqleixing=@Gkqleixing, Grwleixing=@Grwleixing, Gneirong=@Gneirong, Gjieguo=@Gjieguo where GID =@GID ");



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
        param.Add("@GID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 *,Gsheng as yhb_city_Promary_shengshiqu,Gchengshi as yhb_city_City_shengshiqu, Gquyu as yhb_city_Qu_shengshiqu from View_ZZZ_workplan_list where GID=@GID", "数据记录", param);

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
