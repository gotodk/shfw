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

public class NoReSet_160713000058
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
    /// 获取销售发货子表数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private DataTable get_fhzb(string FCID)
    {


        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select * from ZZZ_xiaoshoufahuo_sb   where FCS_FCID=@FCID ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
            return redb;
        }
        else
        {
            return null;
        }


        return null;
    }

    /// <summary>
    /// 检查单据状态
    /// </summary>
    /// <returns></returns>
    public string check_zhuangtai(string FCID)
    {
 
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select top 1 FCzhuangtai from ZZZ_xiaoshoufahuo where FCID=@FCID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                return redb.Rows[0]["FCzhuangtai"].ToString();
            }
        }
        else
        {
            return "";
        }
 
    }

    /// <summary>
    /// 根据销售发货单号，生成一个报修申请
    /// </summary>
    /// <param name="FCID"></param>
    /// <returns></returns>
    public string smaz_go(string FCID)
    {
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@FCID", FCID);

        return_ht = I_DBL.RunParam_SQL("select  FCID,FC_YYID,FCsmaz,FCzhuangtai,FClianxifangshi,FCshoujianren,FCshenqingren, (select top 1 YYfuwufuzeren from ZZZ_KHDA where YYID=FC_YYID) as ss_fwfzr from ZZZ_xiaoshoufahuo where FCID=@FCID and FCsmaz='是' and FCzhuangtai='在途' ; select FCSID,FCS_FCID,FCSbh,FClb,mingcheng,xinghao from View_ZZZ_xiaoshoufahuo_sb_ex where FCS_FCID=@FCID and FClb='设备'; ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
            DataTable redb_other = ((DataSet)return_ht["return_ds"]).Tables[1].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                NoReSet_160605000054 nrs = new NoReSet_160605000054();
       
                //放入参数
                DataTable parameter_forUI = new DataTable();
                parameter_forUI.TableName = "参数集合";
                parameter_forUI.Columns.Add("参数名");
                parameter_forUI.Columns.Add("参数值");
                parameter_forUI.Columns.Add("参数类型");//实际没用，调试看数据方便用的
 
                parameter_forUI.Rows.Add(new string[] { "B_YYID", redb.Rows[0]["FC_YYID"].ToString(), "FormString" });
                parameter_forUI.Rows.Add(new string[] { "Blianxiren", redb.Rows[0]["FCshoujianren"].ToString(), "FormString" });
                parameter_forUI.Rows.Add(new string[] { "Bdianhua", redb.Rows[0]["FClianxifangshi"].ToString(), "FormString" });
                parameter_forUI.Rows.Add(new string[] { "Bfwlx", "安装", "FormString" });

                string sb_str = "";
                for (int p = 0; p < redb_other.Rows.Count; p++)
                {
                    sb_str = sb_str + redb_other.Rows[p]["FCSbh"].ToString() + "，" + redb_other.Rows[p]["mingcheng"].ToString() + "，" + redb_other.Rows[p]["xinghao"].ToString() + "---";
                }

                parameter_forUI.Rows.Add(new string[] { "Bmiaoshu", "此单自动生成，安装设备包括：" + sb_str, "FormString" });
                parameter_forUI.Rows.Add(new string[] { "yhbsp_session_uer_UAid", redb.Rows[0]["FCshenqingren"].ToString(), "FormString" });
                parameter_forUI.Rows.Add(new string[] { "Bfwfzr", redb.Rows[0]["ss_fwfzr"].ToString(), "FormString" });
 

                DataSet dsre = nrs.NRS_ADD(parameter_forUI);
                if (dsre.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "ok")
                { return "，并且同时生成了安装类型的一个报修申请。"; }
                else
                {
                    return "，自动生成报修申请失败了。";
                }
            }
        }
        else
        {
            return "";
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
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo");
        param.Add("@FCID", guid);
        param.Add("@FC_YYID", ht_forUI["FC_YYID"].ToString());
        param.Add("@FCsmaz", ht_forUI["FCsmaz"].ToString());
        param.Add("@FCbumen", ht_forUI["FCbumen"].ToString());
        param.Add("@FC_erp_danbie", ht_forUI["FC_erp_danbie"].ToString());
        param.Add("@FCshenqingren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@FCbeizhu", ht_forUI["FCbeizhu"].ToString());

        param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
        param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
        param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

        param.Add("@Fdailishang", ht_forUI["Fdailishang"].ToString());
        param.Add("@Fdailishang_name", ht_forUI["Fdailishang_name"].ToString());

        alsql.Add("INSERT INTO ZZZ_xiaoshoufahuo(FCID,FC_YYID,FCsmaz,FCbumen,FC_erp_danbie,FCbeizhu,FCshenqingren,FCshenqingshijian,FCzhuangtai,FCjisongdizhi,FClianxifangshi,FCshoujianren,Fdailishang,Fdailishang_name) VALUES(@FCID,@FC_YYID,@FCsmaz,@FCbumen,@FC_erp_danbie,@FCbeizhu,@FCshenqingren,getdate(),'草稿',@FCjisongdizhi,@FClianxifangshi,@FCshoujianren,@Fdailishang,@Fdailishang_name)");


        //遍历子表， 插入 
        string zibiao_gts_id = "grid-table-subtable-160713000991";
        DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
        //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
        if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
            return dsreturn;
        }

        param.Add("@sub_" + "MainID", guid); //隶属主表id

        Double zje = 0.00;

        for (int i = 0; i < subdt.Rows.Count; i++)
        {
            param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo_sb"));

            param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
            param.Add("@sub_" + "FClb" + "_" + i, subdt.Rows[i]["物料类别"].ToString());
            param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["发货数量"].ToString());
            param.Add("@sub_" + "FCbxqx" + "_" + i, subdt.Rows[i]["保修期限"].ToString());
            param.Add("@sub_" + "FCdanjia" + "_" + i, subdt.Rows[i]["单价"].ToString());
            param.Add("@sub_" + "FCjine" + "_" + i, subdt.Rows[i]["金额"].ToString());
            param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());

            zje = zje + Convert.ToDouble(subdt.Rows[i]["金额"].ToString());

            string INSERTsql = "INSERT INTO ZZZ_xiaoshoufahuo_sb ( FCSID, FCS_FCID, FCSbh,FClb, FCSsl,FCbxqx,FCdanjia,FCjine, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ",@sub_" + "FClb" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + " , @sub_" + "FCbxqx" + "_" + i + "  , @sub_" + "FCdanjia" + "_" + i + "  , @sub_" + "FCjine" + "_" + i + " , @sub_" + "FCSbz" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }

        //更新总金额
        string upsql = "UPDATE  ZZZ_xiaoshoufahuo SET FCzje=(select isnull(sum(FCjine),0) from ZZZ_xiaoshoufahuo_sb where FCS_FCID=@FCID) where  FCID=@FCID ";
        alsql.Add(upsql);

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存草稿成功！注意您需要提交后才能生效！{" + guid + "}";
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
        param.Add("@FCID", ht_forUI["idforedit"].ToString());


        if (ht_forUI["ywlx_yincang"].ToString() == "bianjicaogao")
        {

            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "草稿")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有草稿状态才允许编辑。";
                return dsreturn;
            }

            //只有草稿可以编辑

            param.Add("@FC_YYID", ht_forUI["FC_YYID"].ToString());
            param.Add("@FCsmaz", ht_forUI["FCsmaz"].ToString());
            param.Add("@FCbumen", ht_forUI["FCbumen"].ToString());
            param.Add("@FC_erp_danbie", ht_forUI["FC_erp_danbie"].ToString());
            param.Add("@FCbeizhu", ht_forUI["FCbeizhu"].ToString());

            param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
            param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
            param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

            param.Add("@Fdailishang", ht_forUI["Fdailishang"].ToString());
            param.Add("@Fdailishang_name", ht_forUI["Fdailishang_name"].ToString());

            alsql.Add("UPDATE  ZZZ_xiaoshoufahuo SET FC_YYID=@FC_YYID,FCsmaz=@FCsmaz,FCbumen=@FCbumen,FC_erp_danbie=@FC_erp_danbie,FCbeizhu=@FCbeizhu,FCjisongdizhi=@FCjisongdizhi,FClianxifangshi=@FClianxifangshi,FCshoujianren=@FCshoujianren,Fdailishang=@Fdailishang,Fdailishang_name=@Fdailishang_name  where FCID =@FCID ");


            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-160713000991";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_xiaoshoufahuo_sb where  FCS_FCID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_xiaoshoufahuo_sb"));
                }
                else
                {
                    param.Add("@sub_" + "FCSID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }

                param.Add("@sub_" + "FCSbh" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
                param.Add("@sub_" + "FClb" + "_" + i, subdt.Rows[i]["物料类别"].ToString());
                param.Add("@sub_" + "FCSsl" + "_" + i, subdt.Rows[i]["发货数量"].ToString());
                param.Add("@sub_" + "FCbxqx" + "_" + i, subdt.Rows[i]["保修期限"].ToString());
                param.Add("@sub_" + "FCdanjia" + "_" + i, subdt.Rows[i]["单价"].ToString());
                param.Add("@sub_" + "FCjine" + "_" + i, subdt.Rows[i]["金额"].ToString());
                param.Add("@sub_" + "FCSbz" + "_" + i, subdt.Rows[i]["备注"].ToString());



                string INSERTsql = "INSERT INTO ZZZ_xiaoshoufahuo_sb ( FCSID, FCS_FCID, FCSbh,FClb, FCSsl,FCbxqx,FCdanjia,FCjine, FCSbz ) VALUES(@sub_" + "FCSID" + "_" + i + ", @sub_MainID, @sub_" + "FCSbh" + "_" + i + ",@sub_" + "FClb" + "_" + i + ", @sub_" + "FCSsl" + "_" + i + " , @sub_" + "FCbxqx" + "_" + i + "  , @sub_" + "FCdanjia" + "_" + i + "  , @sub_" + "FCjine" + "_" + i + " , @sub_" + "FCSbz" + "_" + i + "  )";
                alsql.Add(INSERTsql);
            }
            //更新总金额
            string upsql = "UPDATE  ZZZ_xiaoshoufahuo SET FCzje=(select isnull(sum(FCjine),0) from ZZZ_xiaoshoufahuo_sb where FCS_FCID=@FCID) where  FCID=@FCID ";
            alsql.Add(upsql);

        }

        string tishimsg_sbda = "";
        string tishimsg_fxdd = "";
        if (ht_forUI["ywlx_yincang"].ToString() == "fahuo")
        {
            if (check_zhuangtai(ht_forUI["idforedit"].ToString().Trim()) != "审核")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有审核状态才允许录入发货信息。";
                return dsreturn;
            }

            if (ht_forUI["FCjisongdizhi"].ToString().Trim() == "" || ht_forUI["FClianxifangshi"].ToString().Trim() == "" || ht_forUI["FCshoujianren"].ToString().Trim() == "")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，收货人的姓名地址联系方式必填。";
                return dsreturn;
            }

            //只更新发货信息，不更新子表
            param.Add("@FCwuliudan", ht_forUI["FCwuliudan"].ToString());
            param.Add("@FCwuliugongsi", ht_forUI["FCwuliugongsi"].ToString());
            param.Add("@FCfahuoren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
 
            param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
            param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
            param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

            param.Add("@FCfahuobeizhu", ht_forUI["FCfahuobeizhu"].ToString());

            param.Add("@FCglxsdd", ht_forUI["FCglxsdd"].ToString());

            alsql.Add("UPDATE  ZZZ_xiaoshoufahuo SET FCwuliudan=@FCwuliudan,FCwuliugongsi=@FCwuliugongsi,FCfahuoren=@FCfahuoren,FCfahuoshijian=getdate(),FCjisongdizhi=@FCjisongdizhi,FClianxifangshi=@FClianxifangshi,FCshoujianren=@FCshoujianren,FCzhuangtai='在途',FCfahuobeizhu=@FCfahuobeizhu  where FCID =@FCID ");
            //同步在联系人中增加一条联系人，重名的不插入
            string KID = CombGuid.GetMewIdFormSequence("ZZZ_KHLXR");
            param.Add("@KID", KID);
            param.Add("@K_YYID", ht_forUI["FC_YYID"].ToString());
            param.Add("@Fdailishang_name", ht_forUI["Fdailishang_name"].ToString());
            if (ht_forUI["Fdailishang_name"].ToString() == "")
            {
                param.Add("@Scaigouqudao", "直销");
            }
            else
            {
                param.Add("@Scaigouqudao", "分销");
            }
        


            param.Add("@KKS", ht_forUI["FCbumen"].ToString());
            alsql.Add("if not exists(select top 1 KID from ZZZ_KHLXR where Klianiren=@FCshoujianren) begin insert into ZZZ_KHLXR (KID,K_YYID,KKS,Klianiren,Kzhicheng,Kxingbie,Kdianhua,Kbeizhu) values(@KID,@K_YYID,@KKS,@FCshoujianren,'','男',@FClianxifangshi,@FCjisongdizhi) end ");






           
            //遍历零配件子表，也反写零件价格。 并且在设备档案表零件子表中插入一条信息
            //遍历子表， 准备反写 (零件信息)
            string zibiao_lj_id = "grid-table-subtable-160713000991";
            DataTable subdt_lj = jsontodatatable.ToDataTable(ht_forUI[zibiao_lj_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_lj_id + "_fcjsq"].ToString() != subdt_lj.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            for (int i = 0; i < subdt_lj.Rows.Count; i++)
            {

                if (subdt_lj.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    //新增的不反写
                }
                else
                {
                    //原有子表编号的，更新
                    param.Add("@sub_" + "FCSID" + "_" + i, subdt_lj.Rows[i]["隐藏编号"].ToString());

                    param.Add("@sub_" + "FCbxqx" + "_" + i, subdt_lj.Rows[i]["保修期限"].ToString());
                    int ljday = Convert.ToInt32(subdt_lj.Rows[i]["保修期限"].ToString()) + 14;
                    param.Add("@sub_" + "FCdanjia" + "_" + i, subdt_lj.Rows[i]["单价"].ToString());
                    param.Add("@sub_" + "FCjine" + "_" + i, subdt_lj.Rows[i]["金额"].ToString());
                    param.Add("@sub_" + "FCscsbxlh" + "_" + i, subdt_lj.Rows[i]["设备档案序列号"].ToString());
 
               

                    param.Add("@sub_" + "FCSbh" + "_" + i, subdt_lj.Rows[i]["物料编码"].ToString());
                    param.Add("@sub_" + "FClb" + "_" + i, subdt_lj.Rows[i]["物料类别"].ToString());

                    param.Add("@sub_" + "Smingcheng" + "_" + i, subdt_lj.Rows[i]["物料名称"].ToString());
                    param.Add("@sub_" + "Sxinghao" + "_" + i, subdt_lj.Rows[i]["规格型号"].ToString());

                    string upupsql = "update ZZZ_xiaoshoufahuo_sb set FCbxqx=@sub_" + "FCbxqx" + "_" + i + " ,FCdanjia=@sub_" + "FCdanjia" + "_" + i + " ,FCjine=@sub_" + "FCjine" + "_" + i + " ,FCscsbxlh=@sub_" + "FCscsbxlh" + "_" + i + " where FCSID=@sub_" + "FCSID" + "_" + i;
                    alsql.Add(upupsql);

                    //反写销售订单已发货数量
                    string ddly = ht_forUI["FCglxsdd"].ToString().Trim();
                    if (ddly != "" && ddly != "--" && ddly != "未关联") {
                        //
                        string fanxiesql = "update ZZZ_xiaoshoudingdan_sb set FCyfhsl=FCyfhsl+1 where FCS_FCID=@FCglxsdd  and FCSbh=@sub_" + "FCSbh" + "_" + i + " and FClb=@sub_" + "FClb" + "_" + i + "";
                        alsql.Add(fanxiesql);
                        tishimsg_fxdd = "并且反写了销售订单中的已发货数量。";
                    }

                    //同时根据序列号生成设备档案
                    if (subdt_lj.Rows[i]["设备档案序列号"].ToString().Trim() != "" && subdt_lj.Rows[i]["物料类别"].ToString().Trim() == "设备")
                    {
                        //
                        string xlhsql = "INSERT INTO ZZZ_WFSB (SID,S_YYID,Skeshi,S_SBID,Smingcheng,Sxinghao,Schuchangriqi,Sbaoxiuqixian,Sdailishang,Scaigouqudao,Sxiaoshoujiage,Sbaoxiudaoqi,Sbaoyangdaoqi) VALUES (@sub_" + "FCscsbxlh" + "_" + i + ", @K_YYID, @KKS,@sub_" + "FCSbh" + "_" + i + ", @sub_" + "Smingcheng" + "_" + i + ", @sub_" + "Sxinghao" + "_" + i + ", getdate(),@sub_" + "FCbxqx" + "_" + i + ",@Fdailishang_name,@Scaigouqudao,@sub_" + "FCdanjia" + "_" + i + ",DATEADD(DAY,"+ ljday + ",GETDATE()),DATEADD(DAY," + ljday + ",GETDATE()))";
                        alsql.Add(xlhsql);
                        tishimsg_sbda = "并且依据填写的序列号同时生成了设备档案。";
                    }
 

                }
            }



        }



        string tishimsg = "";
        string djzt = check_zhuangtai(ht_forUI["idforedit"].ToString().Trim());
        if (ht_forUI["ywlx_yincang"].ToString() == "shenhe" && (!ht_forUI.Contains("shenhe_yincang") || ht_forUI["shenhe_yincang"].ToString() != "关闭"))
        {
            if (djzt != "提交")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有提交状态才允许进行审核或驳回。";
                return dsreturn;
            }

            if (ht_forUI.Contains("shenhe_yincang"))
            {
                //判断是审核还是驳回
                if (ht_forUI["shenhe_yincang"].ToString() == "审核通过")
                {
                    param.Add("@FCshenheren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
                    param.Add("@FCjisongdizhi", ht_forUI["FCjisongdizhi"].ToString());
                    param.Add("@FClianxifangshi", ht_forUI["FClianxifangshi"].ToString());
                    param.Add("@FCfahuobeizhu", ht_forUI["FCfahuobeizhu"].ToString());
                    param.Add("@FCshoujianren", ht_forUI["FCshoujianren"].ToString());

                    alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='审核',FCshenheren=@FCshenheren,FCshenheshijian=getdate(),FCjisongdizhi=@FCjisongdizhi, FClianxifangshi=@FClianxifangshi, FCfahuobeizhu=@FCfahuobeizhu, FCshoujianren=@FCshoujianren where FCzhuangtai='提交' and FCID =@FCID");

                    //审核后，取出设备类型的子表，分析拆分成数量1
                    DataTable dtz = get_fhzb(ht_forUI["idforedit"].ToString());
                    //如果是设备，并且数量大于1，进行拆分处理
                    for (int p = 0; p < dtz.Rows.Count; p++)
                    {
                        
                        string FCSID = dtz.Rows[p]["FCSID"].ToString();
                        string FCS_FCID = dtz.Rows[p]["FCS_FCID"].ToString();
                        string FCSbh = dtz.Rows[p]["FCSbh"].ToString();
                        string FClb = dtz.Rows[p]["FClb"].ToString();
                        double FCSsl =  Convert.ToDouble(dtz.Rows[p]["FCSsl"]);
                        string FCbxqx = dtz.Rows[p]["FCbxqx"].ToString();
                        double FCdanjia = Convert.ToDouble(dtz.Rows[p]["FCdanjia"]);
                        double FCjine = Convert.ToDouble(dtz.Rows[p]["FCjine"]);
                        string FCSbz = dtz.Rows[p]["FCSbz"].ToString();
                        string FCbeuserd = dtz.Rows[p]["FCbeuserd"].ToString();
                        if (FClb == "设备" && FCSsl > 1)
                        {
                            //更新这个数据，把数量变成1，把金额改成单价
                            alsql.Add("UPDATE ZZZ_xiaoshoufahuo_sb SET  FCSsl=1, FCjine=FCdanjia where FCSID ='" + FCSID + "'");

                            //循环原数量-1次，插入拆分后的数据。从修改后的单号提取插入即可。
                            for (int c = 1; c < FCSsl; c++)
                            {
                                alsql.Add("Insert Into ZZZ_xiaoshoufahuo_sb(FCSID, FCS_FCID, FCSbh, FClb, FCSsl, FCbxqx, FCdanjia, FCjine, FCSbz, FCbeuserd) select FCSID+'-" + c.ToString()+ "' as FCSID, FCS_FCID, FCSbh, FClb, FCSsl, FCbxqx, FCdanjia, FCjine, FCSbz, FCbeuserd from ZZZ_xiaoshoufahuo_sb   where FCSID='" + FCSID + "'");
                            }
                            tishimsg = "并且发货数量大于1的物料子表已被自动拆分。";
                        }
                    }
                }
                if (ht_forUI["shenhe_yincang"].ToString() == "驳回")
                {
                    alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='草稿'  where FCzhuangtai='提交' and FCID =@FCID");
                }
            }
            else
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，审核选项必须选择。";
                return dsreturn;
            }
           
        }
        if (ht_forUI["ywlx_yincang"].ToString() == "shenhe" && ht_forUI.Contains("shenhe_yincang") && ht_forUI["shenhe_yincang"].ToString() == "关闭")
        {
            if (djzt != "提交" && djzt != "审核")
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，只有提交状态或审核状态才允许关闭。";
                return dsreturn;
            }
            alsql.Add("UPDATE ZZZ_xiaoshoufahuo SET  FCzhuangtai='关闭'  where FCzhuangtai in ('提交','审核') and isnull(Len(FC_erp_danhao),0) < 5 and FCID =@FCID");
        }


            return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {
            string msg0001 = "";
            if (ht_forUI["ywlx_yincang"].ToString() == "fahuo")
            {
                //如果是上门安装类型，自动生成一个报修申请
                msg0001 = smaz_go(ht_forUI["idforedit"].ToString().Trim());
            }

                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！"+ tishimsg + tishimsg_sbda + msg0001 + "{" + ht_forUI["idforedit"].ToString() + "}";

            
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
        param.Add("@FCID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_xiaoshoufahuo_ex where FCID=@FCID", "数据记录", param);

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
