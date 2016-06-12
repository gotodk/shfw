﻿using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;
using System.Web.Script.Serialization;

public class NoReSet_160427000035
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
    /// 获取服务报告状态
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private string get_zhuangtai(string GID)
    {
  
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@GID", GID);

        return_ht = I_DBL.RunParam_SQL("select  top 1 Gzhuangtai from ZZZ_FWBG where GID=@GID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                return redb.Rows[0]["Gzhuangtai"].ToString();
            }
            else
            {
                return "未知状态";
            }
  
        }
        else
        {
            return "未知状态";
        }


       
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
        //GID, Gfwlx, Gtianxieren, Gbylx, G_BID, G_YYID, Gkeshi, Glianxiren, Gsbtime, Gkehuyaoqiu,  Gguzhang_a, Gguzhang_b, Gguocheng_a, Gguocheng_b, Gjiedan, Gzhuangtai,  Gkaigongtime, Gwangongtime, Gjishufuwufei, Ggongshi, Gneibujiesuan, Gzongjia, Gkehuyijian, Gbeizhu, Gfujian
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_FWBG");
        param.Add("@GID", guid);
        param.Add("@Gfwlx", ht_forUI["Gfwlx"].ToString());
        param.Add("@Gtianxieren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@Gbylx", ht_forUI["Gbylx"].ToString());
        param.Add("@G_BID", ht_forUI["G_BID"].ToString());
        param.Add("@G_YYID", ht_forUI["G_YYID"].ToString());
        param.Add("@Gkeshi", ht_forUI["Gkeshi"].ToString());
        param.Add("@Glianxiren", ht_forUI["Glianxiren"].ToString());

        param.Add("@Gsbtime", ht_forUI["Gsbtime"].ToString());

        param.Add("@Gkehuyaoqiu", ht_forUI["Gkehuyaoqiu"].ToString());
        param.Add("@Gguzhang_a", ht_forUI["Gguzhang_a"].ToString());
        param.Add("@Gguzhang_b", ht_forUI["Gguzhang_b"].ToString());
        param.Add("@Gguocheng_a", ht_forUI["Gguocheng_a"].ToString());
        param.Add("@Gguocheng_b", ht_forUI["Gguocheng_b"].ToString());
        param.Add("@Gjiedan", ht_forUI["Gjiedan"].ToString());
        param.Add("@Gzhuangtai", "保存");
        param.Add("@Gkaigongtime", ht_forUI["Gkaigongtime"].ToString());
        param.Add("@Gwangongtime", ht_forUI["Gwangongtime"].ToString());

        param.Add("@Gjishufuwufei", ht_forUI["Gjishufuwufei"].ToString());
        param.Add("@Ggongshi", ht_forUI["Ggongshi"].ToString());
        param.Add("@Gneibujiesuan", ht_forUI["Gneibujiesuan"].ToString());
        param.Add("@Gzongjia", ht_forUI["Gzongjia"].ToString());
        param.Add("@Gkehuyijian", ht_forUI["Gkehuyijian"].ToString());
        param.Add("@Gbeizhu", ht_forUI["Gbeizhu"].ToString());

        if (ht_forUI.Contains("allpath_file1"))
        { param.Add("@Gfujian", ht_forUI["allpath_file1"].ToString()); }
        else
        {
            param.Add("@Gfujian", "");
        }

        alsql.Add("INSERT INTO ZZZ_FWBG(GID, Gfwlx, Gtianxieren, Gbylx, G_BID, G_YYID, Gkeshi, Glianxiren, Gsbtime, Gkehuyaoqiu,  Gguzhang_a, Gguzhang_b, Gguocheng_a, Gguocheng_b, Gjiedan, Gzhuangtai,  Gkaigongtime, Gwangongtime, Gjishufuwufei, Ggongshi, Gneibujiesuan, Gzongjia, Gkehuyijian, Gbeizhu, Gfujian) VALUES(@GID, @Gfwlx, @Gtianxieren, @Gbylx, @G_BID, @G_YYID, @Gkeshi, @Glianxiren, @Gsbtime, @Gkehuyaoqiu,  @Gguzhang_a, @Gguzhang_b, @Gguocheng_a, @Gguocheng_b, @Gjiedan, @Gzhuangtai,  @Gkaigongtime, @Gwangongtime, @Gjishufuwufei, @Ggongshi, @Gneibujiesuan, @Gzongjia, @Gkehuyijian, @Gbeizhu, @Gfujian )");

 
      
        //遍历子表， 插入 (设备信息)
        string zibiao_gts_id = "grid-table-subtable-160427000664";
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
            param.Add("@sub_" + "sbid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_shebei"));

            param.Add("@sub_" + "sb_SID" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
            param.Add("@sub_" + "sbmingcheng" + "_" + i, subdt.Rows[i]["设备名称"].ToString());
            param.Add("@sub_" + "sbguige" + "_" + i, subdt.Rows[i]["设备规格"].ToString());
            param.Add("@sub_" + "sberpbianhao" + "_" + i, subdt.Rows[i]["ERP编号"].ToString());
            param.Add("@sub_" + "sbyzsj" + "_" + i, subdt.Rows[i]["运转时间"].ToString());
            param.Add("@sub_" + "sbbeizhu" + "_" + i, subdt.Rows[i]["备注"].ToString());

            string INSERTsql = "INSERT INTO ZZZ_FWBG_shebei (sbid, sb_GID, sb_SID, sbmingcheng, sbguige, sberpbianhao, sbyzsj, sbbeizhu) VALUES(@sub_" + "sbid" + "_" + i + ", @sub_MainID, @sub_"+ "sb_SID" + "_" + i + ", @sub_" + "sbmingcheng" + "_" + i + ", @sub_" + "sbguige" + "_" + i + ", @sub_" + "sberpbianhao" + "_" + i + ", @sub_" + "sbyzsj" + "_" + i + ", @sub_" + "sbbeizhu" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }




        //遍历子表， 插入 (预制错误信息)
        string zibiao_cw_id = "grid-table-subtable-160427000665";
        DataTable subdt_cw = jsontodatatable.ToDataTable(ht_forUI[zibiao_cw_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_cw_id + "_fcjsq"].ToString() != subdt_cw.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        //param.Add("@sub_" + "MainID_cw", guid); //隶属主表id

        for (int i = 0; i < subdt_cw.Rows.Count; i++)
        {
            param.Add("@sub_" + "bjid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_baojing"));

            param.Add("@sub_" + "bj_EID" + "_" + i, subdt_cw.Rows[i]["错误编号"].ToString());
            param.Add("@sub_" + "bjmingcheng" + "_" + i, subdt_cw.Rows[i]["错误名称"].ToString());
            param.Add("@sub_" + "bjleixing" + "_" + i, subdt_cw.Rows[i]["错误类型"].ToString());
            param.Add("@sub_" + "bjbeizhu" + "_" + i, subdt_cw.Rows[i]["备注"].ToString());
           
            string INSERTsql = "INSERT INTO ZZZ_FWBG_baojing ( bjid, bj_GID, bj_EID, bjmingcheng, bjleixing, bjbeizhu) VALUES(@sub_" + "bjid" + "_" + i + ", @sub_MainID, @sub_" + "bj_EID" + "_" + i + ", @sub_" + "bjmingcheng" + "_" + i + ", @sub_" + "bjleixing" + "_" + i + ", @sub_" + "bjbeizhu" + "_" + i + " )";
            alsql.Add(INSERTsql);
        }




        //遍历子表， 插入 (零件信息)
        string zibiao_lj_id = "grid-table-subtable-160427000666";
        DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        //param.Add("@sub_" + "MainID_cw", guid); //隶属主表id

        for (int i = 0; i < subdt_lj.Rows.Count; i++)
        {
            param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_lingjian"));

            param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["零件编号"].ToString());
            param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["零件名称"].ToString());
            param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["规格型号"].ToString());
            param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["零件单位"].ToString());
            param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["实际售价"].ToString());
            param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["零售价"].ToString());
            param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["零件数量"].ToString());
            param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["保修截止日期"].ToString());
            param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["批号"].ToString());
            param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["备注"].ToString());
  
            string INSERTsql = "INSERT INTO ZZZ_FWBG_lingjian (  ljid, lj_GID, lj_LID, ljmingcheng, ljxinghao, ljdanwei, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
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
        param.Add("@GID", ht_forUI["idforedit"].ToString());


        if (ht_forUI["yc_czlx"].ToString() == "xiugai")
        {

            //验证状态，只有保存或驳回状态的才允许修改。
            string zt = get_zhuangtai(ht_forUI["idforedit"].ToString());
            if (zt != "保存" && zt != "驳回")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "只有“保存”或“驳回”状态才允许修改！";
                return dsreturn;
            }

            param.Add("@Gfwlx", ht_forUI["Gfwlx"].ToString());

            param.Add("@Gbylx", ht_forUI["Gbylx"].ToString());
            param.Add("@G_BID", ht_forUI["G_BID"].ToString());
            param.Add("@G_YYID", ht_forUI["G_YYID"].ToString());
            param.Add("@Gkeshi", ht_forUI["Gkeshi"].ToString());
            param.Add("@Glianxiren", ht_forUI["Glianxiren"].ToString());

            param.Add("@Gsbtime", ht_forUI["Gsbtime"].ToString());

            param.Add("@Gkehuyaoqiu", ht_forUI["Gkehuyaoqiu"].ToString());
            param.Add("@Gguzhang_a", ht_forUI["Gguzhang_a"].ToString());
            param.Add("@Gguzhang_b", ht_forUI["Gguzhang_b"].ToString());
            param.Add("@Gguocheng_a", ht_forUI["Gguocheng_a"].ToString());
            param.Add("@Gguocheng_b", ht_forUI["Gguocheng_b"].ToString());
            param.Add("@Gjiedan", ht_forUI["Gjiedan"].ToString());
            param.Add("@Gzhuangtai", "保存");
            param.Add("@Gkaigongtime", ht_forUI["Gkaigongtime"].ToString());
            param.Add("@Gwangongtime", ht_forUI["Gwangongtime"].ToString());

            param.Add("@Gjishufuwufei", ht_forUI["Gjishufuwufei"].ToString());
            param.Add("@Ggongshi", ht_forUI["Ggongshi"].ToString());
            param.Add("@Gneibujiesuan", ht_forUI["Gneibujiesuan"].ToString());
            param.Add("@Gzongjia", ht_forUI["Gzongjia"].ToString());
            param.Add("@Gkehuyijian", ht_forUI["Gkehuyijian"].ToString());
            param.Add("@Gbeizhu", ht_forUI["Gbeizhu"].ToString());

            if (ht_forUI.Contains("allpath_file1"))
            { param.Add("@Gfujian", ht_forUI["allpath_file1"].ToString()); }
            else
            {
                param.Add("@Gfujian", "");
            }

            alsql.Add("UPDATE ZZZ_FWBG SET Gfwlx=@Gfwlx, Gbylx=@Gbylx, G_BID=@G_BID, G_YYID=@G_YYID, Gkeshi=@Gkeshi, Glianxiren=@Glianxiren, Gsbtime=@Gsbtime, Gkehuyaoqiu=@Gkehuyaoqiu,  Gguzhang_a=@Gguzhang_a, Gguzhang_b=@Gguzhang_b, Gguocheng_a=@Gguocheng_a, Gguocheng_b=@Gguocheng_b, Gjiedan=@Gjiedan, Gzhuangtai=@Gzhuangtai,  Gkaigongtime=@Gkaigongtime, Gwangongtime=@Gwangongtime, Gjishufuwufei=@Gjishufuwufei, Ggongshi=@Ggongshi, Gneibujiesuan=@Gneibujiesuan, Gzongjia=@Gzongjia, Gkehuyijian=@Gkehuyijian, Gbeizhu=@Gbeizhu, Gfujian=@Gfujian where GID=@GID ");

          




            //遍历子表， 插入 (设备信息)
            string zibiao_gts_id = "grid-table-subtable-160427000664";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }

            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_FWBG_shebei where  sb_GID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
        
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "sbid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_shebei"));
                }
                else
                {
                    param.Add("@sub_" + "sbid" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "sb_SID" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
                param.Add("@sub_" + "sbmingcheng" + "_" + i, subdt.Rows[i]["设备名称"].ToString());
                param.Add("@sub_" + "sbguige" + "_" + i, subdt.Rows[i]["设备规格"].ToString());
                param.Add("@sub_" + "sberpbianhao" + "_" + i, subdt.Rows[i]["ERP编号"].ToString());
                param.Add("@sub_" + "sbyzsj" + "_" + i, subdt.Rows[i]["运转时间"].ToString());
                param.Add("@sub_" + "sbbeizhu" + "_" + i, subdt.Rows[i]["备注"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_FWBG_shebei (sbid, sb_GID, sb_SID, sbmingcheng, sbguige, sberpbianhao, sbyzsj, sbbeizhu) VALUES(@sub_" + "sbid" + "_" + i + ", @sub_MainID, @sub_" + "sb_SID" + "_" + i + ", @sub_" + "sbmingcheng" + "_" + i + ", @sub_" + "sbguige" + "_" + i + ", @sub_" + "sberpbianhao" + "_" + i + ", @sub_" + "sbyzsj" + "_" + i + ", @sub_" + "sbbeizhu" + "_" + i + "  )";
                alsql.Add(INSERTsql);
            }




            //遍历子表， 插入 (预制错误信息)
            string zibiao_cw_id = "grid-table-subtable-160427000665";
            DataTable subdt_cw = jsontodatatable.ToDataTable(ht_forUI[zibiao_cw_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_cw_id + "_fcjsq"].ToString() != subdt_cw.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }

            //param.Add("@sub_" + "MainID_cw", guid); //隶属主表id
            alsql.Add("delete ZZZ_FWBG_baojing where  bj_GID = @sub_" + "MainID");
            for (int i = 0; i < subdt_cw.Rows.Count; i++)
            {
          
                if (subdt_cw.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "bjid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_baojing"));
                }
                else
                {
                    param.Add("@sub_" + "bjid" + "_" + i, subdt_cw.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "bj_EID" + "_" + i, subdt_cw.Rows[i]["错误编号"].ToString());
                param.Add("@sub_" + "bjmingcheng" + "_" + i, subdt_cw.Rows[i]["错误名称"].ToString());
                param.Add("@sub_" + "bjleixing" + "_" + i, subdt_cw.Rows[i]["错误类型"].ToString());
                param.Add("@sub_" + "bjbeizhu" + "_" + i, subdt_cw.Rows[i]["备注"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_FWBG_baojing ( bjid, bj_GID, bj_EID, bjmingcheng, bjleixing, bjbeizhu) VALUES(@sub_" + "bjid" + "_" + i + ", @sub_MainID, @sub_" + "bj_EID" + "_" + i + ", @sub_" + "bjmingcheng" + "_" + i + ", @sub_" + "bjleixing" + "_" + i + ", @sub_" + "bjbeizhu" + "_" + i + " )";
                alsql.Add(INSERTsql);
            }




            //遍历子表， 插入 (零件信息)
            string zibiao_lj_id = "grid-table-subtable-160427000666";
            DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }

            //param.Add("@sub_" + "MainID_cw", guid); //隶属主表id
            alsql.Add("delete ZZZ_FWBG_lingjian where  lj_GID = @sub_" + "MainID");
            for (int i = 0; i < subdt_lj.Rows.Count; i++)
            {
       
                if (subdt_lj.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "ljid" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_FWBG_lingjian"));
                }
                else
                {
                    param.Add("@sub_" + "ljid" + "_" + i, subdt_lj.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "lj_LID" + "_" + i, subdt_lj.Rows[i]["零件编号"].ToString());
                param.Add("@sub_" + "ljmingcheng" + "_" + i, subdt_lj.Rows[i]["零件名称"].ToString());
                param.Add("@sub_" + "ljxinghao" + "_" + i, subdt_lj.Rows[i]["规格型号"].ToString());
                param.Add("@sub_" + "ljdanwei" + "_" + i, subdt_lj.Rows[i]["零件单位"].ToString());
                param.Add("@sub_" + "ljsjsj" + "_" + i, subdt_lj.Rows[i]["实际售价"].ToString());
                param.Add("@sub_" + "ljlsj" + "_" + i, subdt_lj.Rows[i]["零售价"].ToString());
                param.Add("@sub_" + "ljshuliang" + "_" + i, subdt_lj.Rows[i]["零件数量"].ToString());
                param.Add("@sub_" + "ljzje" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());
                param.Add("@sub_" + "ljbaoxiujiezhi" + "_" + i, subdt_lj.Rows[i]["保修截止日期"].ToString());
                param.Add("@sub_" + "ljpihao" + "_" + i, subdt_lj.Rows[i]["批号"].ToString());
                param.Add("@sub_" + "ljbeizhu" + "_" + i, subdt_lj.Rows[i]["备注"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_FWBG_lingjian (  ljid, lj_GID, lj_LID, ljmingcheng, ljxinghao, ljdanwei, ljsjsj, ljlsj, ljshuliang, ljzje, ljbaoxiujiezhi, ljpihao,   ljbeizhu) VALUES(@sub_" + "ljid" + "_" + i + ", @sub_MainID, @sub_" + "lj_LID" + "_" + i + ", @sub_" + "ljmingcheng" + "_" + i + ", @sub_" + "ljxinghao" + "_" + i + ", @sub_" + "ljdanwei" + "_" + i + ", @sub_" + "ljsjsj" + "_" + i + ", @sub_" + "ljlsj" + "_" + i + ", @sub_" + "ljshuliang" + "_" + i + ", @sub_" + "ljzje" + "_" + i + ", @sub_" + "ljbaoxiujiezhi" + "_" + i + ", @sub_" + "ljpihao" + "_" + i + ", @sub_" + "ljbeizhu" + "_" + i + " )";
                alsql.Add(INSERTsql);
            }





        }

        if (ht_forUI["yc_czlx"].ToString() == "shenhe")
        {

            //只有选择了审核选项，才允许审核
            if (!ht_forUI.Contains("Gshenpixuanxiang"))
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "错误：未选择“审核通过”或“驳回报告”";
                return dsreturn;
            }
            //验证状态，只有“提交”状态才允许审核
            string zt = get_zhuangtai(ht_forUI["idforedit"].ToString());
            if (zt != "提交")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "只有“提交”状态才允许进行审核操作！";
                return dsreturn;
            }

            //进行审核;
            if (ht_forUI.Contains("Gshenpixuanxiang") && ht_forUI["Gshenpixuanxiang"].ToString() == "审核通过")
            {
                param.Add("@Gzhuangtai", "审核");
                param.Add("@Gspyj", ht_forUI["Gspyj"].ToString());
                param.Add("@Gtianxieren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
                alsql.Add("UPDATE ZZZ_FWBG SET Gzhuangtai=@Gzhuangtai,Gspyj=@Gspyj,Gtianxieren=@Gtianxieren,Gspshijian=getdate() where GID=@GID");
            }
            if (ht_forUI.Contains("Gshenpixuanxiang") && ht_forUI["Gshenpixuanxiang"].ToString() == "驳回报告")
            {
                param.Add("@Gzhuangtai", "驳回");
                param.Add("@Gspyj", ht_forUI["Gspyj"].ToString());
                param.Add("@Gtianxieren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
                alsql.Add("UPDATE ZZZ_FWBG SET Gzhuangtai=@Gzhuangtai,Gspyj=@Gspyj,Gtianxieren=@Gtianxieren,Gspshijian=getdate() where GID=@GID");
            }



            

            if (ht_forUI.Contains("Gshenpixuanxiang") && ht_forUI["Gshenpixuanxiang"].ToString() == "审核通过")
            {
                //如果结单，并且审核通过了，反写报修申请单状态
                alsql.Add("UPDATE ZZZ_BXSQ SET Bzhuangtai='已结单',Bwctime=getdate() where BID=(select  G_BID from ZZZ_FWBG where GID=@GID and Gjiedan='是') ");
                //如果结单，并且审核通过了，反写序列号表安装日期
                alsql.Add("UPDATE ZZZ_WFSB SET Sanzhuangriqi=getdate() where SID in (select  sb_SID from ZZZ_FWBG_shebei where sb_GID=@GID ) and (select Gjiedan from ZZZ_FWBG where GID=@GID) = '是' ");

                //如果结单，并且审核通过了，减少个人库存(怎么对应个人库存待研究，调取变更库存的sql语句)

            }
        }


        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            if (ht_forUI["yc_czlx"].ToString() == "shenhe")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "审核或驳回完成！";
            }
            else
            {
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！";
            }
                
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

        return_ht = I_DBL.RunParam_SQL("select  top 1 * from View_ZZZ_FWBG_ex where GID=@GID", "数据记录", param);

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
            if (redb.Rows[0]["Gfujian"].ToString() != "")
            {
                //Ttupianpath
                DataTable dttu = new DataTable("图片记录");
                dttu.Columns.Add("Ttupianpath");
                string[] arr_tu = redb.Rows[0]["Gfujian"].ToString().Split(',');
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
