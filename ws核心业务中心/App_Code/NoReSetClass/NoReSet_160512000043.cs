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
using System.Net;
using System.Configuration;

public class NoReSet_160512000043
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
    /// 获取当前数据库内容，用于时间对比字符串
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    private string get_dbtime(string QID)
    {
        string dbtime = "";
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@QID", QID);

        return_ht = I_DBL.RunParam_SQL("select * from View_ZZZ_HQ_YJ_ex where YJ_QID = @QID order by YJlysj asc", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            for (int i = 0; i < redb.Rows.Count; i++)
            {
                dbtime = dbtime + redb.Rows[i]["YJqsshijian"].ToString() + ",";
            }
            return dbtime;

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

        return null;
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


        param.Add("@YJ_QID", ht_forUI["idforedit"].ToString());

        param.Add("@YJqianhsuren", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@YJyijian", ht_forUI["YJyijian"].ToString());

        param.Add("@YJlaiyuan", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        string YJqianhsuren = "";
        if (ht_forUI.Contains("YJqianhsuren"))
        {
            YJqianhsuren = ht_forUI["YJqianhsuren"].ToString();
        }
        if (YJqianhsuren.Trim() != "")
        {
            //开始生成多个sql插入语句，转发会签人
            string[] YJqianhsuren_arr = YJqianhsuren.Split(',');
 
            for (int i = 0; i < YJqianhsuren_arr.Length; i++)
            {
                if(YJqianhsuren_arr[i].Trim() != "")
                {
                    //如果被转发人已在此会签单存在，则忽略

                    param.Add("@sub_" + "YJID" + "_" + i, CombGuid.GetMewIdFormSequence("ZZZ_HQ_YJ"));

                    param.Add("@sub_" + "YJqianhsuren" + "_" + i, YJqianhsuren_arr[i].Trim());
 
                    string INSERTsql = "if not exists(select YJID from ZZZ_HQ_YJ where YJ_QID=@YJ_QID and YJqianhsuren=@sub_" + "YJqianhsuren" + "_" + i + " )   begin   INSERT INTO  ZZZ_HQ_YJ ( YJID, YJ_QID, YJqianhsuren, YJzhuangtai, YJyijian, YJqsshijian, YJlaiyuan, YJlysj ) VALUES(@sub_" + "YJID" + "_" + i + ", @YJ_QID, @sub_" + "YJqianhsuren" + "_" + i + ", '待签',null,null, @YJlaiyuan,getdate()   )  ;    INSERT INTO  auth_znx(touser, msgtitle, msurl) VALUES(@sub_" + "YJqianhsuren" + "_" + i + ", '有新的会签需要您的参与，单号[' + @YJ_QID + ']', '/adminht/corepage/huiqian/cyhq.aspx?idforedit='+@YJ_QID+'&fff=1')    end";
 
                    alsql.Add(INSERTsql);
                   
                }
                
            }
        }


        //更新会签单意见字段
        alsql.Add("UPDATE ZZZ_HQ_YJ SET  YJzhuangtai='已签', YJyijian=  '[' + CONVERT(varchar(100), GETDATE(), 20) + ']-->：<br/>' + @YJyijian + '<br/>' + YJyijian ,YJqsshijian=getdate() where YJqianhsuren=@YJqianhsuren and YJ_QID=@YJ_QID ");

        //更新会签单状态,如果是结单人才更新
        if (ht_forUI.Contains("Qzhuangtai"))
        {
            //会签结单时， 如果界面时间跟数据库时间有差异， 提示意见已变化。。
            if (ht_forUI["Qzhuangtai"].ToString() == "已结单")
            {
                if (get_dbtime(ht_forUI["idforedit"].ToString()) != ht_forUI["ymtime_hidden"].ToString())
                {
                    dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                    dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "结单失败，打开页面后，签署意见有变更，请刷新重新审阅！";
                    return dsreturn;
                }
                
                param.Add("@Qzhuangtai", ht_forUI["Qzhuangtai"].ToString());
                alsql.Add("UPDATE ZZZ_HQ SET  Qzhuangtai=@Qzhuangtai,Qjiedanshijian=getdate()   where QID=@YJ_QID ");
            }

            

        }
       



        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {
            //强制调用一次微信发送扫描接口，发送微信消息
            try
            {
                WebClient client = new WebClient();
                string wx_httpurl = ConfigurationManager.AppSettings["wx_httpurl"].ToString();
                client.DownloadString("http://"+ wx_httpurl + "/qyapi_dlhd.aspx?sendmsgf=send");
            }
            catch { }
         

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存成功！";
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
        param.Add("@QID", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_HQ_ex where QID=@QID", "数据记录", param);

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
