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

public class NoReSet_160425000031
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
        //SID, S_YYID, Skeshi, Sleixing, Sbanben, Schuchangriqi, Sanzhuangriqi, Sbaoxiudaoqi, Schenbenjia,   Sbaoxiuqixian, Sbaoyangzhouqi, Szhuangtai, Scaigouqudao, Sdailishang,   Sshouming, Sxiaoshoujiage
        param.Add("@SID", ht_forUI["SID"].ToString());
        param.Add("@S_YYID", ht_forUI["S_YYID"].ToString());
        param.Add("@Skeshi", ht_forUI["Skeshi"].ToString());
        param.Add("@S_SBID", ht_forUI["S_SBID"].ToString());
        param.Add("@Smingcheng", ht_forUI["Smingcheng"].ToString());
        param.Add("@Sxinghao", ht_forUI["Sxinghao"].ToString());

        param.Add("@Sbanben", ht_forUI["Sbanben"].ToString());
        param.Add("@Schuchangriqi", ht_forUI["Schuchangriqi"].ToString());
        param.Add("@Sanzhuangriqi", ht_forUI["Sanzhuangriqi"].ToString());
        param.Add("@Sbaoxiudaoqi", ht_forUI["Sbaoxiudaoqi"].ToString());
        param.Add("@Sbaoyangdaoqi", ht_forUI["Sbaoyangdaoqi"].ToString());
        param.Add("@Schenbenjia", ht_forUI["Schenbenjia"].ToString());
        param.Add("@Sbaoxiuqixian", ht_forUI["Sbaoxiuqixian"].ToString());
        param.Add("@Sbaoyangzhouqi", ht_forUI["Sbaoyangzhouqi"].ToString());
        param.Add("@Szhuangtai", ht_forUI["Szhuangtai"].ToString());
        param.Add("@Scaigouqudao", ht_forUI["Scaigouqudao"].ToString());
        param.Add("@Sdailishang", ht_forUI["Sdailishang"].ToString());
        param.Add("@Sshouming", ht_forUI["Sshouming"].ToString());
        param.Add("@Sxiaoshoujiage", ht_forUI["Sxiaoshoujiage"].ToString());

        alsql.Add("INSERT INTO   ZZZ_WFSB(SID, S_YYID, Skeshi, S_SBID,Smingcheng,Sxinghao, Sbanben, Schuchangriqi, Sanzhuangriqi, Sbaoxiudaoqi,Sbaoyangdaoqi, Schenbenjia,   Sbaoxiuqixian, Sbaoyangzhouqi, Szhuangtai, Scaigouqudao, Sdailishang,   Sshouming, Sxiaoshoujiage ) VALUES(@SID, @S_YYID, @Skeshi, @S_SBID,@Smingcheng,@Sxinghao, @Sbanben, @Schuchangriqi, @Sanzhuangriqi, @Sbaoxiudaoqi,@Sbaoyangdaoqi, @Schenbenjia,   @Sbaoxiuqixian, @Sbaoyangzhouqi, @Szhuangtai, @Scaigouqudao, @Sdailishang,   @Sshouming, @Sxiaoshoujiage)");



        //遍历子表， 插入 (零件信息)
        string zibiao_lj_id = "grid-table-subtable-160726000004";
        DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        param.Add("@sub_" + "MainID", ht_forUI["SID"].ToString()); //隶属主表id

        for (int i = 0; i < subdt_lj.Rows.Count; i++)
        {
            param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_WFSB_LJ"));

            param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["零件编号"].ToString());
            param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["零件名称"].ToString());
            param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["规格型号"].ToString());
            param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["零件单位"].ToString());
            param.Add("@sub_" + "ljweizhi" + "_" + i, subdt_lj.Rows[i]["位置标记"].ToString());
            param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["实际售价"].ToString());
            param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["零售价"].ToString());
            param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["零件数量"].ToString());
            param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["保修截止日期"].ToString());
            param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["批号"].ToString());
            param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["备注"].ToString());
 

            string INSERTsql = "INSERT INTO ZZZ_WFSB_LJ (  ljid, lj_SBID, lj_LID, ljmingcheng, ljxinghao, ljdanwei,ljweizhi, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ",@sub_" + "ljweizhi" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
            alsql.Add(INSERTsql);
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！{" + ht_forUI["SID"].ToString() + "}";
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
        param.Add("@SID", ht_forUI["idforedit"].ToString());
        param.Add("@S_YYID", ht_forUI["S_YYID"].ToString());
        param.Add("@Skeshi", ht_forUI["Skeshi"].ToString());
        param.Add("@S_SBID", ht_forUI["S_SBID"].ToString());
        param.Add("@Smingcheng", ht_forUI["Smingcheng"].ToString());
        param.Add("@Sxinghao", ht_forUI["Sxinghao"].ToString());

        param.Add("@Sbanben", ht_forUI["Sbanben"].ToString());
        param.Add("@Schuchangriqi", ht_forUI["Schuchangriqi"].ToString());
        param.Add("@Sanzhuangriqi", ht_forUI["Sanzhuangriqi"].ToString());
        param.Add("@Sbaoxiudaoqi", ht_forUI["Sbaoxiudaoqi"].ToString());
        param.Add("@Sbaoyangdaoqi", ht_forUI["Sbaoyangdaoqi"].ToString());
        param.Add("@Schenbenjia", ht_forUI["Schenbenjia"].ToString());
        param.Add("@Sbaoxiuqixian", ht_forUI["Sbaoxiuqixian"].ToString());
        param.Add("@Sbaoyangzhouqi", ht_forUI["Sbaoyangzhouqi"].ToString());
        param.Add("@Szhuangtai", ht_forUI["Szhuangtai"].ToString());
        param.Add("@Scaigouqudao", ht_forUI["Scaigouqudao"].ToString());
        param.Add("@Sdailishang", ht_forUI["Sdailishang"].ToString());
        param.Add("@Sshouming", ht_forUI["Sshouming"].ToString());
        param.Add("@Sxiaoshoujiage", ht_forUI["Sxiaoshoujiage"].ToString());


        alsql.Add("UPDATE ZZZ_WFSB SET S_YYID=@S_YYID, Skeshi=@Skeshi, S_SBID=@S_SBID,Smingcheng=@Smingcheng,Sxinghao=@Sxinghao, Sbanben=@Sbanben, Schuchangriqi=@Schuchangriqi, Sanzhuangriqi=@Sanzhuangriqi, Sbaoxiudaoqi=@Sbaoxiudaoqi,Sbaoyangdaoqi=@Sbaoyangdaoqi, Schenbenjia=@Schenbenjia,   Sbaoxiuqixian=@Sbaoxiuqixian, Sbaoyangzhouqi=@Sbaoyangzhouqi, Szhuangtai=@Szhuangtai, Scaigouqudao=@Scaigouqudao, Sdailishang=@Sdailishang,   Sshouming=@Sshouming, Sxiaoshoujiage=@Sxiaoshoujiage  where SID=@SID ");



        //遍历子表， 插入 (零件信息)
        string zibiao_lj_id = "grid-table-subtable-160726000004";
        DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }
 

        param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
        alsql.Add("delete ZZZ_WFSB_LJ where  lj_SBID = @sub_" + "MainID");
        for (int i = 0; i < subdt_lj.Rows.Count; i++)
        {

            if (subdt_lj.Rows[i]["隐藏编号"].ToString().Trim() == "")
            {
                param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_WFSB_LJ"));
            }
            else
            {
                param.Add("@sub_" + "ljid" + "_" + i, subdt_lj.Rows[i]["隐藏编号"].ToString());
            }

            param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["零件编号"].ToString());
            param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["零件名称"].ToString());
            param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["规格型号"].ToString());
            param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["零件单位"].ToString());
            param.Add("@sub_" + "ljweizhi" + "_" + i, subdt_lj.Rows[i]["位置标记"].ToString());
            param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["实际售价"].ToString());
            param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["零售价"].ToString());
            param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["零件数量"].ToString());
            param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["保修截止日期"].ToString());
            param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["批号"].ToString());
            param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["备注"].ToString());
          

            string INSERTsql = "INSERT INTO ZZZ_WFSB_LJ (  ljid, lj_SBID, lj_LID, ljmingcheng, ljxinghao, ljdanwei,ljweizhi, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ",@sub_" + "ljweizhi" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
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
        param.Add("@SID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *  from View_ZZZ_WFSB_list where SID=@SID", "数据记录", param);

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
