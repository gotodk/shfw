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

public class NoReSet_160429000036
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
        //SBID, SBmingcheng, SBxinghao, SBdanwei, SBchengbenjia, SBbaoxiuqixian, SBbaoyangzhouqi,  SBchanpinshouming, SBxiaoshoujiage, SBshengchanchang, SBerpbianma, SBzhuangtai, SBbeizhu
        //string guid = ht_forUI["SBID"].ToString();
 
        string guid = CombGuid.GetNewCombGuid("S");
        if (ht_forUI["SBID"].ToString().Trim() == "" || ht_forUI["SBID"].ToString() == "自动生成")
        {
            guid = CombGuid.GetMewIdFormSequence("ZZZ_SBLXBASE");
        }
        else
        {
            guid = ht_forUI["SBID"].ToString().Trim();
        }

        param.Add("@SBID", guid);
        param.Add("@SBmingcheng", ht_forUI["SBmingcheng"].ToString());
        param.Add("@SBxinghao", ht_forUI["SBxinghao"].ToString());
        param.Add("@SBdanwei", ht_forUI["SBdanwei"].ToString());
        param.Add("@SBchengbenjia", ht_forUI["SBchengbenjia"].ToString());
        param.Add("@SBbaoxiuqixian", ht_forUI["SBbaoxiuqixian"].ToString());
        param.Add("@SBbaoyangzhouqi", ht_forUI["SBbaoyangzhouqi"].ToString());
        param.Add("@SBchanpinshouming", ht_forUI["SBchanpinshouming"].ToString());
        param.Add("@SBxiaoshoujiage", ht_forUI["SBxiaoshoujiage"].ToString());
        param.Add("@SBshengchanchang", ht_forUI["SBshengchanchang"].ToString());
        param.Add("@SBerpbianma", ht_forUI["SBerpbianma"].ToString());
        param.Add("@SBzhuangtai", ht_forUI["SBzhuangtai"].ToString());
        param.Add("@SBbeizhu", ht_forUI["SBbeizhu"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@SBfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@SBfujian", "");
        }

        alsql.Add("INSERT INTO ZZZ_SBLXBASE(SBID, SBmingcheng, SBxinghao, SBdanwei, SBchengbenjia, SBbaoxiuqixian, SBbaoyangzhouqi,  SBchanpinshouming, SBxiaoshoujiage, SBshengchanchang, SBerpbianma, SBzhuangtai, SBbeizhu,SBfujian ) VALUES(@SBID, @SBmingcheng, @SBxinghao, @SBdanwei, @SBchengbenjia, @SBbaoxiuqixian, @SBbaoyangzhouqi,  @SBchanpinshouming, @SBxiaoshoujiage, @SBshengchanchang, @SBerpbianma, @SBzhuangtai, @SBbeizhu,@SBfujian)");

 
        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160429000669";
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
            param.Add("@sub_" + "id" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_SBLXBASE_BB"));

            param.Add("@sub_" + "BBBbanben" + "_" + i, subdt.Rows[i]["版本号"].ToString());
            param.Add("@sub_" + "BBBshuoming" + "_" + i, subdt.Rows[i]["版本说明"].ToString());
 
    


            string INSERTsql = "INSERT INTO ZZZ_SBLXBASE_BB ( BBBID, BBB_SBID,  BBBbanben,   BBBshuoming ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_"+ "BBBbanben" + "_" + i + ", @sub_" + "BBBshuoming" + "_" + i + "    )";
            alsql.Add(INSERTsql);
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
        param.Add("@SBID", ht_forUI["idforedit"].ToString());
        param.Add("@SBmingcheng", ht_forUI["SBmingcheng"].ToString());
        param.Add("@SBxinghao", ht_forUI["SBxinghao"].ToString());
        param.Add("@SBdanwei", ht_forUI["SBdanwei"].ToString());
        param.Add("@SBchengbenjia", ht_forUI["SBchengbenjia"].ToString());
        param.Add("@SBbaoxiuqixian", ht_forUI["SBbaoxiuqixian"].ToString());
        param.Add("@SBbaoyangzhouqi", ht_forUI["SBbaoyangzhouqi"].ToString());
        param.Add("@SBchanpinshouming", ht_forUI["SBchanpinshouming"].ToString());
        param.Add("@SBxiaoshoujiage", ht_forUI["SBxiaoshoujiage"].ToString());
        param.Add("@SBshengchanchang", ht_forUI["SBshengchanchang"].ToString());
        param.Add("@SBerpbianma", ht_forUI["SBerpbianma"].ToString());
        param.Add("@SBzhuangtai", ht_forUI["SBzhuangtai"].ToString());
        param.Add("@SBbeizhu", ht_forUI["SBbeizhu"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@SBfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@SBfujian", "");
        }

        alsql.Add("UPDATE ZZZ_SBLXBASE SET SBmingcheng=@SBmingcheng, SBxinghao=@SBxinghao, SBdanwei=@SBdanwei, SBchengbenjia=@SBchengbenjia, SBbaoxiuqixian=@SBbaoxiuqixian, SBbaoyangzhouqi=@SBbaoyangzhouqi,  SBchanpinshouming=@SBchanpinshouming, SBxiaoshoujiage=@SBxiaoshoujiage, SBshengchanchang=@SBshengchanchang, SBerpbianma=@SBerpbianma, SBzhuangtai=@SBzhuangtai, SBbeizhu=@SBbeizhu,SBfujian=@SBfujian  where SBID=@SBID ");


        //遍历子表，先删除，再插入，已有主键的不重新生成。
        string zibiao_gts_id = "grid-table-subtable-160429000669";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete ZZZ_SBLXBASE_BB where  BBB_SBID = @sub_" + "MainID");
        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "id" + "_" + i,   CombGuid.GetMewIdFormSequence("ZZZ_SBLXBASE_BB"));
            }
            else
            {
                param.Add("@sub_" + "id" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
            }

            param.Add("@sub_" + "BBBbanben" + "_" + i, subdt.Rows[i]["版本号"].ToString());
            param.Add("@sub_" + "BBBshuoming" + "_" + i, subdt.Rows[i]["版本说明"].ToString());


            string INSERTsql = "INSERT INTO ZZZ_SBLXBASE_BB ( BBBID, BBB_SBID,  BBBbanben,   BBBshuoming ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_" + "BBBbanben" + "_" + i + ", @sub_" + "BBBshuoming" + "_" + i + "    )";
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
        param.Add("@SBID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from ZZZ_SBLXBASE where SBID=@SBID", "数据记录", param);

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
            if (redb.Rows[0]["SBfujian"].ToString() != "")
            {
                //Ttupianpath
                DataTable dttu = new DataTable("图片记录");
                dttu.Columns.Add("Ttupianpath");
                string[] arr_tu = redb.Rows[0]["SBfujian"].ToString().Split(',');
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
