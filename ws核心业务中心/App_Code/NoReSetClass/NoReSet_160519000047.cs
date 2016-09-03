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

public class NoReSet_160519000047
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
        
        if(ht_forUI["dananleixing"].ToString() == "未成交")
        {
            string guid = CombGuid.GetMewIdFormSequence("ZZZ_KHLXR_wcj");
            param.Add("@KID", guid);
            param.Add("@K_YYID", ht_forUI["K_YYID"].ToString());
            param.Add("@KKS", ht_forUI["KKS"].ToString());
            param.Add("@Klianiren", ht_forUI["Klianiren"].ToString());
            param.Add("@Kzhicheng", ht_forUI["Kzhicheng"].ToString());
            param.Add("@Kxingbie", ht_forUI["Kxingbie"].ToString());
            param.Add("@Kdianhua", ht_forUI["Kdianhua"].ToString());
            param.Add("@Kbeizhu", ht_forUI["Kbeizhu"].ToString());
            alsql.Add("INSERT INTO   ZZZ_KHLXR_wcj(KID, K_YYID, KKS, Klianiren, Kzhicheng, Kxingbie, Kdianhua, Kbeizhu ) VALUES(@KID, @K_YYID, @KKS, @Klianiren, @Kzhicheng, @Kxingbie, @Kdianhua, @Kbeizhu)");
        }

        if (ht_forUI["dananleixing"].ToString() == "成交")
        {
            string guid = CombGuid.GetMewIdFormSequence("ZZZ_KHLXR");
            param.Add("@KID", guid);
            param.Add("@K_YYID", ht_forUI["K_YYID"].ToString());
            param.Add("@KKS", ht_forUI["KKS"].ToString());
            param.Add("@Klianiren", ht_forUI["Klianiren"].ToString());
            param.Add("@Kzhicheng", ht_forUI["Kzhicheng"].ToString());
            param.Add("@Kxingbie", ht_forUI["Kxingbie"].ToString());
            param.Add("@Kdianhua", ht_forUI["Kdianhua"].ToString());
            param.Add("@Kbeizhu", ht_forUI["Kbeizhu"].ToString());
            alsql.Add("INSERT INTO   ZZZ_KHLXR(KID, K_YYID, KKS, Klianiren, Kzhicheng, Kxingbie, Kdianhua, Kbeizhu ) VALUES(@KID, @K_YYID, @KKS, @Klianiren, @Kzhicheng, @Kxingbie, @Kdianhua, @Kbeizhu)");
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
         

        return null;
    }

    /// <summary>
    /// 编辑数据前获取数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
    {
        return null;
    }


}
