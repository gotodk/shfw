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

public class NoReSet_160219000020
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
        //pid, formnumber, ddh, jhrq_begin, jhrq_end, jhsj, jhcssj, cpbh, cpmc, cpgg, jhzl, jhzldw, jhfl, jhfldw, bzr, shr,  zt_jihua, zt_shenhe, zt_xiada, beizhu, chucang, rucang, chejian, gongxu, banzu, caozuoyuan, baogongyuan,   zhijianyuan, shebei, zujian
        string guid = CombGuid.GetMewIdFormSequence("ZZZ_plan");
        param.Add("@pid", guid);
        param.Add("@formnumber", ht_forUI["formnumber"].ToString());
        param.Add("@ddh", ht_forUI["ddh"].ToString());
        param.Add("@jhrq_begin", ht_forUI["jhrq_gogo1"].ToString());
        param.Add("@jhrq_end", ht_forUI["jhrq_gogo2"].ToString());
        param.Add("@jhsj", ht_forUI["jhsj"].ToString());
        //param.Add("@jhcssj", ht_forUI["jhcssj"].ToString());
        param.Add("@cpbh", ht_forUI["cpbh"].ToString());
        param.Add("@cpmc", ht_forUI["cpmc"].ToString());
        //param.Add("@cpgg", ht_forUI["cpgg"].ToString());
        param.Add("@jhzl", ht_forUI["jhzl"].ToString());
        param.Add("@jhzldw", ht_forUI["jhzldw"].ToString());
        param.Add("@jhfl", ht_forUI["jhfl"].ToString());
        param.Add("@jhfldw", ht_forUI["jhfldw"].ToString()); 
        param.Add("@bzr", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@shr", "");
        param.Add("@zt_jihua", ht_forUI["zt_jihua"].ToString());
        param.Add("@zt_shenhe", ht_forUI["zt_shenhe"].ToString());
        param.Add("@zt_xiada", ht_forUI["zt_xiada"].ToString());
        param.Add("@beizhu", ht_forUI["beizhu"].ToString());
        param.Add("@chucang", ht_forUI["chucang"].ToString());
        param.Add("@rucang", ht_forUI["rucang"].ToString());
        param.Add("@chejian", ht_forUI["chejian"].ToString());
        param.Add("@gongxu", ht_forUI["gongxu"].ToString());
        param.Add("@banzu", ht_forUI["banzu"].ToString());
        param.Add("@caozuoyuan", ht_forUI["caozuoyuan"].ToString());
        param.Add("@baogongyuan", ht_forUI["baogongyuan"].ToString());
        param.Add("@zhijianyuan", ht_forUI["zhijianyuan"].ToString());
        param.Add("@shebei", ht_forUI["shebei"].ToString());
        param.Add("@zujian", ht_forUI["zujian"].ToString());
         

        alsql.Add("INSERT INTO  ZZZ_plan(pid, formnumber, ddh, jhrq_begin, jhrq_end, jhsj, cpbh, cpmc, jhzl, jhzldw, jhfl, jhfldw, bzr, shr,  zt_jihua, zt_shenhe, zt_xiada, beizhu, chucang, rucang, chejian, gongxu, banzu, caozuoyuan, baogongyuan,   zhijianyuan, shebei, zujian) VALUES(@pid, @formnumber, @ddh, @jhrq_begin, @jhrq_end, @jhsj, @cpbh, @cpmc,  @jhzl, @jhzldw, @jhfl, @jhfldw,@bzr, @shr, @zt_jihua, @zt_shenhe, @zt_xiada, @beizhu, @chucang, @rucang, @chejian, @gongxu, @banzu, @caozuoyuan, @baogongyuan,   @zhijianyuan, @shebei, @zujian)");
 

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新增成功！{"+ guid + "}";
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
   
        param.Add("@pid", ht_forUI["idforedit"].ToString());
        param.Add("@formnumber", ht_forUI["formnumber"].ToString());
        param.Add("@ddh", ht_forUI["ddh"].ToString());
        param.Add("@jhrq_begin", ht_forUI["jhrq_gogo1"].ToString());
        param.Add("@jhrq_end", ht_forUI["jhrq_gogo2"].ToString());
        param.Add("@jhsj", ht_forUI["jhsj"].ToString());
        param.Add("@cpbh", ht_forUI["cpbh"].ToString());
        param.Add("@cpmc", ht_forUI["cpmc"].ToString());
        //param.Add("@cpgg", ht_forUI["cpgg"].ToString());
        param.Add("@jhzl", ht_forUI["jhzl"].ToString());
        param.Add("@jhzldw", ht_forUI["jhzldw"].ToString());
        param.Add("@jhfl", ht_forUI["jhfl"].ToString());
        param.Add("@jhfldw", ht_forUI["jhfldw"].ToString());
        param.Add("@zt_jihua", ht_forUI["zt_jihua"].ToString());
        param.Add("@zt_shenhe", ht_forUI["zt_shenhe"].ToString());
        param.Add("@zt_xiada", ht_forUI["zt_xiada"].ToString());
        param.Add("@beizhu", ht_forUI["beizhu"].ToString());
        param.Add("@chucang", ht_forUI["chucang"].ToString());
        param.Add("@rucang", ht_forUI["rucang"].ToString());
        param.Add("@chejian", ht_forUI["chejian"].ToString());
        param.Add("@gongxu", ht_forUI["gongxu"].ToString());
        param.Add("@banzu", ht_forUI["banzu"].ToString());
        param.Add("@caozuoyuan", ht_forUI["caozuoyuan"].ToString());
        param.Add("@baogongyuan", ht_forUI["baogongyuan"].ToString());
        param.Add("@zhijianyuan", ht_forUI["zhijianyuan"].ToString());
        param.Add("@shebei", ht_forUI["shebei"].ToString());
        param.Add("@zujian", ht_forUI["zujian"].ToString());

        alsql.Add("UPDATE ZZZ_plan SET    formnumber=@formnumber, ddh=@ddh, jhrq_begin=@jhrq_begin, jhrq_end=@jhrq_end, jhsj=@jhsj,   cpbh=@cpbh, cpmc=@cpmc,   jhzl=@jhzl, jhzldw=@jhzldw, jhfl=@jhfl, jhfldw=@jhfldw,  zt_jihua=@zt_jihua, zt_shenhe=@zt_shenhe, zt_xiada=@zt_xiada, beizhu=@beizhu, chucang=@chucang, rucang=@rucang, chejian=@chejian, gongxu=@gongxu, banzu=@banzu, caozuoyuan=@caozuoyuan, baogongyuan=@baogongyuan,   zhijianyuan=@zhijianyuan, shebei=@shebei, zujian=@zujian   where pid=@pid ");

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
        param.Add("@pid", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *, jhrq_begin as jhrq_gogo1, jhrq_end as jhrq_gogo2 from ZZZ_plan where pid=@pid", "数据记录", param);

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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，修改失败：" + return_ht["return_errmsg"].ToString();
        }


        return dsreturn;
    }


}
