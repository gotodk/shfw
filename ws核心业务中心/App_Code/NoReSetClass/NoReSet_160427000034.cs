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

public class NoReSet_160427000034
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
        string guid = ht_forUI["HID"].ToString();
        if (guid == "临时自动生成")
        {
            guid = "T"+CombGuid.GetMewIdFormSequence("ZZZ_HTGL");
        }
        param.Add("@HID", guid);
        param.Add("@H_YYID", ht_forUI["H_YYID"].ToString());
        param.Add("@Eleixing", ht_forUI["Eleixing"].ToString());
        param.Add("@Hqianshuren", ht_forUI["Hqianshuren"].ToString());
        param.Add("@Hzongjia", ht_forUI["Hzongjia"].ToString());
        param.Add("@Hqianshuriqi", ht_forUI["Hqianshuriqi"].ToString());
        param.Add("@Hfukuanriqi", ht_forUI["Hfukuanriqi"].ToString());
        param.Add("@Hshouuanjin", ht_forUI["Hshouuanjin"].ToString());

        param.Add("@Hfukuanzhouqi", ht_forUI["Hfukuanzhouqi"].ToString());
        param.Add("@Hshifouzhiqu", ht_forUI["Hshifouzhiqu"].ToString());
        param.Add("@Hbeizhu", ht_forUI["Hbeizhu"].ToString());

        alsql.Add("INSERT INTO ZZZ_HTGL(HID, H_YYID, Eleixing, Hqianshuren, Hzongjia, Hqianshuriqi, Hfukuanriqi, Hshouuanjin, Hfukuanzhouqi,   Hshifouzhiqu, Hbeizhu) VALUES(@HID, @H_YYID, @Eleixing, @Hqianshuren, @Hzongjia, @Hqianshuriqi, @Hfukuanriqi, @Hshouuanjin, @Hfukuanzhouqi,   @Hshifouzhiqu, @Hbeizhu)");

 
        //遍历子表， 插入(设备信息)
        string zibiao_gts_id = "grid-table-subtable-160427000636";
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
            param.Add("@sub_" + "id" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_HTGL_SB"));

            param.Add("@sub_" + "HSB_SID" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
            param.Add("@sub_" + "HSBbegin" + "_" + i, subdt.Rows[i]["生效日期"].ToString());
            param.Add("@sub_" + "HSBend" + "_" + i, subdt.Rows[i]["失效日期"].ToString());
            param.Add("@sub_" + "HSBjiage" + "_" + i, subdt.Rows[i]["金额"].ToString());

            param.Add("@sub_" + "HSB_SBID" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
            param.Add("@sub_" + "HSB_SBmingcheng" + "_" + i, subdt.Rows[i]["设备名称"].ToString());
            param.Add("@sub_" + "HSB_SBxinghao" + "_" + i, subdt.Rows[i]["设备规格"].ToString());



            string INSERTsql = "INSERT INTO ZZZ_HTGL_SB ( HSBID, HSB_HID,  HSB_SID, HSB_SBID,HSB_SBmingcheng, HSB_SBxinghao, HSBbegin,  HSBend, HSBjiage  ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_"+ "HSB_SID" + "_" + i + ", @sub_" + "HSB_SBID" + "_" + i + ", @sub_" + "HSB_SBmingcheng" + "_" + i + ", @sub_" + "HSB_SBxinghao" + "_" + i + ", @sub_" + "HSBbegin" + "_" + i + ", @sub_" + "HSBend" + "_" + i + ", @sub_" + "HSBjiage" + "_" + i + "  )";
            alsql.Add(INSERTsql);
        }



        //遍历子表， 插入 (付款信息)
        string zibiao_cw_id = "grid-table-subtable-160902000184";
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
            param.Add("@sub_" + "FKID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_HTGL_FKQK"));

            param.Add("@sub_" + "FK_shuoming" + "_" + i, subdt_cw.Rows[i]["付款说明"].ToString());
            param.Add("@sub_" + "FK_riqi" + "_" + i, subdt_cw.Rows[i]["付款日期"].ToString());
            param.Add("@sub_" + "FK_bili" + "_" + i, subdt_cw.Rows[i]["付款比例"].ToString());
            param.Add("@sub_" + "FK_jine" + "_" + i, subdt_cw.Rows[i]["付款金额"].ToString());
            param.Add("@sub_" + "FK_beizhu" + "_" + i, subdt_cw.Rows[i]["备注"].ToString());

            string INSERTsql = "INSERT INTO ZZZ_HTGL_FKQK ( FKID, FK_HID, FK_shuoming, FK_riqi, FK_bili, FK_jine, FK_beizhu) VALUES(@sub_" + "FKID" + "_" + i + ", @sub_MainID, @sub_" + "FK_shuoming" + "_" + i + ", @sub_" + "FK_riqi" + "_" + i + ", @sub_" + "FK_bili" + "_" + i + ", @sub_" + "FK_jine" + "_" + i + ", @sub_" + "FK_beizhu" + "_" + i + " )";
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
        param.Add("@HID", ht_forUI["idforedit"].ToString());
        param.Add("@H_YYID", ht_forUI["H_YYID"].ToString());
        param.Add("@Eleixing", ht_forUI["Eleixing"].ToString());
        param.Add("@Hqianshuren", ht_forUI["Hqianshuren"].ToString());
        param.Add("@Hzongjia", ht_forUI["Hzongjia"].ToString());
        param.Add("@Hqianshuriqi", ht_forUI["Hqianshuriqi"].ToString());
        param.Add("@Hfukuanriqi", ht_forUI["Hfukuanriqi"].ToString());
        param.Add("@Hshouuanjin", ht_forUI["Hshouuanjin"].ToString());

        param.Add("@Hfukuanzhouqi", ht_forUI["Hfukuanzhouqi"].ToString());
        param.Add("@Hshifouzhiqu", ht_forUI["Hshifouzhiqu"].ToString());
        param.Add("@Hbeizhu", ht_forUI["Hbeizhu"].ToString());

        alsql.Add("UPDATE ZZZ_HTGL SET   H_YYID=@H_YYID, Eleixing=@Eleixing, Hqianshuren=@Hqianshuren, Hzongjia=@Hzongjia, Hqianshuriqi=@Hqianshuriqi, Hfukuanriqi=@Hfukuanriqi, Hshouuanjin=@Hshouuanjin, Hfukuanzhouqi=@Hfukuanzhouqi,   Hshifouzhiqu=@Hshifouzhiqu, Hbeizhu=@Hbeizhu where HID=@HID ");


        //修改合同编号
        if (ht_forUI["idforedit"].ToString().Trim() != ht_forUI["HID"].ToString().Trim())
        {
            param.Add("@new_HID", ht_forUI["HID"].ToString());
            param.Add("@old_HID", ht_forUI["idforedit"].ToString());
            alsql.Add("update ZZZ_HTGL  set HID=@new_HID where HID=@old_HID");
            alsql.Add("update ZZZ_HTGL_SB  set HSB_HID=@new_HID where HSB_HID=@old_HID");
            alsql.Add("update ZZZ_HTGL_FKQK  set FK_HID=@new_HID where FK_HID=@old_HID");

        }


        //未修改合同编号时，才保存子表
        if (ht_forUI["idforedit"].ToString().Trim() == ht_forUI["HID"].ToString().Trim())
        {

            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-160427000636";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete ZZZ_HTGL_SB where  HSB_HID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "id" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_HTGL_SB"));
                }
                else
                {
                    param.Add("@sub_" + "id" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }


                param.Add("@sub_" + "HSB_SID" + "_" + i, subdt.Rows[i]["设备序列号"].ToString());
                param.Add("@sub_" + "HSBbegin" + "_" + i, subdt.Rows[i]["生效日期"].ToString());
                param.Add("@sub_" + "HSBend" + "_" + i, subdt.Rows[i]["失效日期"].ToString());
                param.Add("@sub_" + "HSBjiage" + "_" + i, subdt.Rows[i]["金额"].ToString());

                param.Add("@sub_" + "HSB_SBID" + "_" + i, subdt.Rows[i]["物料编码"].ToString());
                param.Add("@sub_" + "HSB_SBmingcheng" + "_" + i, subdt.Rows[i]["设备名称"].ToString());
                param.Add("@sub_" + "HSB_SBxinghao" + "_" + i, subdt.Rows[i]["设备规格"].ToString());


                string INSERTsql = "INSERT INTO ZZZ_HTGL_SB ( HSBID, HSB_HID,  HSB_SID, HSB_SBID,HSB_SBmingcheng, HSB_SBxinghao,  HSBbegin,  HSBend, HSBjiage  ) VALUES(@sub_" + "id" + "_" + i + ", @sub_MainID, @sub_" + "HSB_SID" + "_" + i + ", @sub_" + "HSB_SBID" + "_" + i + ", @sub_" + "HSB_SBmingcheng" + "_" + i + ", @sub_" + "HSB_SBxinghao" + "_" + i + ", @sub_" + "HSBbegin" + "_" + i + ", @sub_" + "HSBend" + "_" + i + ", @sub_" + "HSBjiage" + "_" + i + "  )";
                alsql.Add(INSERTsql);
            }



            //遍历子表， 插入 (付款信息)
            string zibiao_cw_id = "grid-table-subtable-160902000184";
            DataTable subdt_cw = jsontodatatable.ToDataTable(ht_forUI[zibiao_cw_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_cw_id + "_fcjsq"].ToString() != subdt_cw.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }

            //param.Add("@sub_" + "MainID_cw", guid); //隶属主表id
            alsql.Add("delete ZZZ_HTGL_FKQK where  FK_HID = @sub_" + "MainID");
            for (int i = 0; i < subdt_cw.Rows.Count; i++)
            {
            
                if (subdt_cw.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "FKID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_HTGL_FKQK"));
                }
                else
                {
                    param.Add("@sub_" + "FKID" + "_" + i, subdt_cw.Rows[i]["隐藏编号"].ToString());
                }
                param.Add("@sub_" + "FK_shuoming" + "_" + i, subdt_cw.Rows[i]["付款说明"].ToString());
                param.Add("@sub_" + "FK_riqi" + "_" + i, subdt_cw.Rows[i]["付款日期"].ToString());
                param.Add("@sub_" + "FK_bili" + "_" + i, subdt_cw.Rows[i]["付款比例"].ToString());
                param.Add("@sub_" + "FK_jine" + "_" + i, subdt_cw.Rows[i]["付款金额"].ToString());
                param.Add("@sub_" + "FK_beizhu" + "_" + i, subdt_cw.Rows[i]["备注"].ToString());

                string INSERTsql = "INSERT INTO ZZZ_HTGL_FKQK ( FKID, FK_HID, FK_shuoming, FK_riqi, FK_bili, FK_jine, FK_beizhu) VALUES(@sub_" + "FKID" + "_" + i + ", @sub_MainID, @sub_" + "FK_shuoming" + "_" + i + ", @sub_" + "FK_riqi" + "_" + i + ", @sub_" + "FK_bili" + "_" + i + ", @sub_" + "FK_jine" + "_" + i + ", @sub_" + "FK_beizhu" + "_" + i + " )";
                alsql.Add(INSERTsql);
            }


        }

        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改成功！{" + ht_forUI["HID"].ToString() + "}";
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
        param.Add("@HID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_HTGL_list where HID=@HID", "数据记录", param);

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
