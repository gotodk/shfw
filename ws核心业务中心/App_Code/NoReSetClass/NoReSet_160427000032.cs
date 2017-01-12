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

public class NoReSet_160427000032
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
        //LID, Lmingcheng, Lguige, Ldanwei, Lpinyin, Lchengbenjia, Lzuidijia, Lshoujia, Lbaoyangzhouqi, Lbaoxiuqi
        string guid = CombGuid.GetNewCombGuid("L");
        if (ht_forUI["LID"].ToString().Trim() == "" || ht_forUI["LID"].ToString() == "自动生成")
        {
            guid = 'L' + CombGuid.GetMewIdFormSequence("ZZZ_WFLJ");
        }
        else
        {
            guid = ht_forUI["LID"].ToString().Trim();
        }

  
        param.Add("@LID", guid);
        param.Add("@Lmingcheng", ht_forUI["Lmingcheng"].ToString());
        param.Add("@Lguige", ht_forUI["Lguige"].ToString());
        param.Add("@Ldanwei", ht_forUI["Ldanwei"].ToString());

        param.Add("@Lpinyin", ht_forUI["Lpinyin"].ToString());
        param.Add("@Lchengbenjia", ht_forUI["Lchengbenjia"].ToString());
        param.Add("@Lzuidijia", ht_forUI["Lzuidijia"].ToString());
        param.Add("@Lshoujia", ht_forUI["Lshoujia"].ToString());

        param.Add("@Lerpbianhao", ht_forUI["Lerpbianhao"].ToString());
        param.Add("@Lbaoxiuqi", ht_forUI["Lbaoxiuqi"].ToString());
        param.Add("@Lzhuangtai", ht_forUI["Lzhuangtai"].ToString());

        param.Add("@u", ht_forUI["yhbsp_session_uer_UAid"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Lfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Lfujian", "");
        }

        alsql.Add("INSERT INTO   ZZZ_WFLJ(LID, Lmingcheng, Lguige, Ldanwei, Lpinyin, Lchengbenjia, Lzuidijia, Lshoujia, Lerpbianhao, Lbaoxiuqi,Lzhuangtai,Lfujian,Lupuser,Luptime) VALUES(@LID, @Lmingcheng, @Lguige, @Ldanwei, @Lpinyin, @Lchengbenjia, @Lzuidijia, @Lshoujia, @Lerpbianhao, @Lbaoxiuqi,@Lzhuangtai,@Lfujian,(select top 1 xingming from ZZZ_userinfo where UAid=@u),getdate())");

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
        param.Add("@LID", ht_forUI["idforedit"].ToString());
 
        param.Add("@Lmingcheng", ht_forUI["Lmingcheng"].ToString());
        param.Add("@Lguige", ht_forUI["Lguige"].ToString());
        param.Add("@Ldanwei", ht_forUI["Ldanwei"].ToString());

        param.Add("@Lpinyin", ht_forUI["Lpinyin"].ToString());
        param.Add("@Lchengbenjia", ht_forUI["Lchengbenjia"].ToString());
        param.Add("@Lzuidijia", ht_forUI["Lzuidijia"].ToString());
        param.Add("@Lshoujia", ht_forUI["Lshoujia"].ToString());

        param.Add("@Lerpbianhao", ht_forUI["Lerpbianhao"].ToString());
        param.Add("@Lbaoxiuqi", ht_forUI["Lbaoxiuqi"].ToString());
        param.Add("@Lzhuangtai", ht_forUI["Lzhuangtai"].ToString());

        param.Add("@u", ht_forUI["yhbsp_session_uer_UAid"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Lfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Lfujian", "");
        }

        alsql.Add("UPDATE ZZZ_WFLJ SET Lmingcheng=@Lmingcheng, Lguige=@Lguige, Ldanwei=@Ldanwei, Lpinyin=@Lpinyin, Lchengbenjia=@Lchengbenjia, Lzuidijia=@Lzuidijia, Lshoujia=@Lshoujia, Lerpbianhao=@Lerpbianhao, Lbaoxiuqi=@Lbaoxiuqi,Lzhuangtai=@Lzhuangtai,Lfujian=@Lfujian ,Lupuser=(select top 1 xingming from ZZZ_userinfo where UAid=@u),Luptime=getdate() where LID=@LID ");
   

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
        param.Add("@LID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *  from ZZZ_WFLJ where LID=@LID", "数据记录", param);

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
            if (redb.Rows[0]["Lfujian"].ToString() != "")
            {
                //Ttupianpath
                DataTable dttu = new DataTable("图片记录");
                dttu.Columns.Add("Ttupianpath");
                string[] arr_tu = redb.Rows[0]["Lfujian"].ToString().Split(',');
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
