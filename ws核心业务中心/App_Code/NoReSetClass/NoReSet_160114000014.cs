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

public class NoReSet_160114000014
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
        //用登录账号作为内码
        string guid = ht_forUI["Uloginname"].ToString();
        System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
        bool regre = reg1.IsMatch(guid);
        if (!regre)
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "错误：登录账号必须全是数字，以工号作为登录账号！";
            return dsreturn;
        }
        //string guid = CombGuid.GetNewCombGuid("U");
        param.Add("@UAid", guid);
        param.Add("@Uloginname", ht_forUI["Uloginname"].ToString());

        //对密码进行加密
        string mima_enc = StringOP.encMe(ht_forUI["Uloginpassword"].ToString().Trim(), "mima");
        param.Add("@Uloginpassword", mima_enc);

        param.Add("@xingming", ht_forUI["xingming"].ToString());
        param.Add("@zhuangtai", ht_forUI["zhuangtai"].ToString());
        param.Add("@zhiwei", ht_forUI["zhiwei"].ToString());
        param.Add("@xingbie", ht_forUI["xingbie"].ToString());
 
     
        param.Add("@beizhu", ht_forUI["beizhu"].ToString());

        param.Add("@gongzuodi", ht_forUI["gongzuodi"].ToString());
        param.Add("@suoshuquyu", ht_forUI["suoshuquyu"].ToString());
        param.Add("@shoujihao", ht_forUI["shoujihao"].ToString());
        param.Add("@gudingdianhua", ht_forUI["gudingdianhua"].ToString());
        param.Add("@youxiang", ht_forUI["youxiang"].ToString());
        param.Add("@lingdao", ht_forUI["lingdao"].ToString());

        if (ht_forUI["zhuangtai"].ToString() == "离职")
        { param.Add("@Uattrcode", "1"); }
        else
        { param.Add("@Uattrcode", "-1"); }


        alsql.Add("INSERT INTO  auth_users_auths(UAid ,Uloginname,Uloginpassword,Uattrcode) VALUES(@UAid ,@Uloginname,@Uloginpassword,@Uattrcode )");

        alsql.Add("INSERT INTO  ZZZ_userinfo(UAid ,xingming,zhuangtai,zhiwei,xingbie,beizhu,gongzuodi,suoshuquyu,shoujihao,gudingdianhua,youxiang,lingdao) VALUES(@UAid ,@xingming,@zhuangtai,@zhiwei,@xingbie,@beizhu,@gongzuodi,@suoshuquyu,@shoujihao,@gudingdianhua,@youxiang,@lingdao)");

        //设置初始权限组
        if (ht_forUI.Contains("morenqanxianshezhi") && ht_forUI["morenqanxianshezhi"].ToString() != "")
        {
            param.Add("@morenqanxianshezhi", ht_forUI["morenqanxianshezhi"].ToString());
            alsql.Add("update auth_users_auths set Uingroups=@morenqanxianshezhi where UAid=@UAid");
        }

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！{" + guid + "}";
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
        param.Add("@UAid", ht_forUI["idforedit"].ToString());
        param.Add("@Uloginname", ht_forUI["Uloginname"].ToString());
        param.Add("@Uloginpassword", StringOP.encMe(ht_forUI["Uloginpassword"].ToString(), "mima"));

        param.Add("@xingming", ht_forUI["xingming"].ToString());
        param.Add("@zhuangtai", ht_forUI["zhuangtai"].ToString());
        param.Add("@zhiwei", ht_forUI["zhiwei"].ToString());
        param.Add("@xingbie", ht_forUI["xingbie"].ToString());


        param.Add("@beizhu", ht_forUI["beizhu"].ToString());

        param.Add("@gongzuodi", ht_forUI["gongzuodi"].ToString());
        param.Add("@suoshuquyu", ht_forUI["suoshuquyu"].ToString());
        param.Add("@shoujihao", ht_forUI["shoujihao"].ToString());
        param.Add("@gudingdianhua", ht_forUI["gudingdianhua"].ToString());
        param.Add("@youxiang", ht_forUI["youxiang"].ToString());
        param.Add("@lingdao", ht_forUI["lingdao"].ToString());

        if (ht_forUI["zhuangtai"].ToString() == "离职")
        { param.Add("@Uattrcode", "1"); }
        else
        { param.Add("@Uattrcode", "-1"); }

        alsql.Add("UPDATE ZZZ_userinfo SET xingming=@xingming,zhuangtai=@zhuangtai,zhiwei=@zhiwei,xingbie=@xingbie,beizhu=@beizhu,gongzuodi=@gongzuodi,suoshuquyu=@suoshuquyu,shoujihao=@shoujihao,gudingdianhua=@gudingdianhua,youxiang=@youxiang,lingdao=@lingdao where UAid=@UAid ");
        alsql.Add("UPDATE auth_users_auths SET Uloginname=@Uloginname,Uloginpassword=@Uloginpassword,Uattrcode=@Uattrcode  where UAid=@UAid ");

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
        param.Add("@UAid", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 * from view_ZZZ_userinfo_ex where UAid=@UAid", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有找到指定数据!";
                return dsreturn;
            }
            redb.Rows[0]["Uloginpassword"] = StringOP.uncMe(redb.Rows[0]["Uloginpassword"].ToString(),"mima");
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
