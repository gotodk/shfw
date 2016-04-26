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

public class NoReSet_160414000030
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

        //检查时间限制
        string jcsj = jianchashijian(parameter_forUI);

        if (jcsj == "ok")
        {

        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = jcsj;
            return dsreturn;
        }

        //检查单号并生成单号
        string jcdh = jianchadanhao(parameter_forUI);

        //开始真正的处理，根据业务逻辑操作数据库
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = jcdh;
        param.Add("@XID", guid);
        param.Add("@Xdanhao", guid);
 
        param.Add("@Xyiyuan", ht_forUI["Xyiyuan"].ToString());
        param.Add("@Xnianfen", ht_forUI["Xnianfen"].ToString());
        param.Add("@Xyuefen", ht_forUI["Xyuefen"].ToString());
        param.Add("@Xtupian", ht_forUI["allpath_file1"].ToString());
 

        alsql.Add("INSERT INTO  ZZZ_M_XLD(XID,Xdanhao ,Xyiyuan,Xnianfen,Xyuefen,Xtupian) VALUES( @XID,@Xdanhao ,@Xyiyuan,@Xnianfen,@Xyuefen,@Xtupian )");
 

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
            //特殊提示
            if (return_ht["return_errmsg"].ToString().IndexOf("重复键") > 0)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "提交失败，请勿重复处理单据！";
            }
        }
        return dsreturn;
    }

    private string jianchadanhao(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@year", ht_forUI["Xnianfen"].ToString());
        param.Add("@month", ht_forUI["Xyuefen"].ToString());
        param.Add("@CZY", ht_forUI["zdy_CZY"].ToString());
        param.Add("@YiYuan", ht_forUI["Xyiyuan"].ToString());

        return_ht = I_DBL.RunProc_CMD("[jwms].[dbo].proc_www_FPTB ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                return redb.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }


        }
        else
        {
            return "";
        }
    }

    private string jianchashijian(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }
 
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@Xnianfen", ht_forUI["Xnianfen"].ToString());
        param.Add("@Xyuefen", ht_forUI["Xyuefen"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 * from View_M_MOB_view_Period where Fyear=@Xnianfen and Fmonth=@Xyuefen and  getdate()>=FDate1 and getdate()<= dateadd(day,1,FDate2)", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                return "ok";
            }
            else
            {
                return "不在合理时间内，不允许提交！";
            }

  
        }
        else
        {
            return "提交时间验证失败";
        }

 
 
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


        //检查时间限制
        string jcsj = jianchashijian(parameter_forUI);

        if (jcsj == "ok")
        {

        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = jcsj;
            return dsreturn;
        }

        //检查单号并生成单号
        string jcdh = jianchadanhao(parameter_forUI);



        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        param.Add("@XID", ht_forUI["idforedit"].ToString());
 
        param.Add("@Xyiyuan", ht_forUI["Xyiyuan"].ToString());
        param.Add("@Xnianfen", ht_forUI["Xnianfen"].ToString());
        param.Add("@Xyuefen", ht_forUI["Xyuefen"].ToString());
        param.Add("@Xtupian", ht_forUI["allpath_file1"].ToString());

        alsql.Add("UPDATE  ZZZ_M_XLD SET   Xyiyuan=@Xyiyuan,Xnianfen=@Xnianfen,Xyuefen=@Xyuefen,Xtupian=@Xtupian  where XID=@XID ");
     

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
        param.Add("@XID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *,FUserName as zdy_CZY from View_M_XLD_list where XID=@XID", "数据记录", param);

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

            //如果图片不是空值，把图片也弄个表加进来
            if (redb.Rows[0]["Xtupian"].ToString() != "")
            {
                //Ttupianpath
                DataTable dttu = new DataTable("图片记录");
                dttu.Columns.Add("Ttupianpath");
                string[] arr_tu = redb.Rows[0]["Xtupian"].ToString().Split(',');
                for (int t = 0; t < arr_tu.Length; t++)
                {
                    dttu.Rows.Add(arr_tu[t]);
                }
                dsreturn.Tables.Add(dttu.Copy());


            }

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
