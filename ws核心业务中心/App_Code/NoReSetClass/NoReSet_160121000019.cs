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
using ServiceStack.Text;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.IO;
using System.Xml;

public class NoReSet_160121000019
{

    public  DataSet ConvertXMLToDataSet(string xmlData)
    {
        StringReader stream = null;
        XmlTextReader reader = null;
        try
        {
            DataSet xmlDS = new DataSet();
            stream = new StringReader(xmlData);
            //从stream装载到XmlTextReader
            reader = new XmlTextReader(stream);
            xmlDS.ReadXml(reader);
            return xmlDS;
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (reader != null) reader.Close();
        }
    }


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
        //wmid, wmname, wmbeizhu, wmjsonstr

        string guid = CombGuid.GetNewCombGuid("WM");
        param.Add("@wmid", guid);
        param.Add("@wmname", ht_forUI["wmname"].ToString());
        param.Add("@wmbeizhu", ht_forUI["wmbeizhu"].ToString());
        param.Add("@wmjsonstr", ht_forUI["workprocess_area_json"].ToString());
        


        alsql.Add("INSERT INTO  ZZZ_workprocess_main(wmid, wmname, wmbeizhu, wmjsonstr) VALUES(@wmid, @wmname, @wmbeizhu, @wmjsonstr)");


        //解析流程图的json
    
        DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(ht_forUI["workprocess_area_json"].ToString());

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
 
        param.Add("@wmid", ht_forUI["idforedit"].ToString());
        param.Add("@wmname", ht_forUI["wmname"].ToString());
        param.Add("@wmbeizhu", ht_forUI["wmbeizhu"].ToString());
        param.Add("@wmjsonstr", ht_forUI["workprocess_area_json"].ToString());

        alsql.Add("UPDATE ZZZ_workprocess_main SET  wmname=@wmname, wmbeizhu=@wmbeizhu, wmjsonstr=@wmjsonstr   where wmid=@wmid ");

       
        //解析流程图的json
        XNode node = JsonConvert.DeserializeXNode(ht_forUI["workprocess_area_json"].ToString(), "Root");
        DataSet ds =  ConvertXMLToDataSet(node.ToString());
        alsql.Add("delete ZZZ_workprocess_sub    where wswmid=@wmid ");
        for (int w = 0; w < ds.Tables.Count; w++)
        {
            if (ds.Tables[w].TableName.IndexOf("workprocess_area_node_") >= 0)
            {
                string wsbiaoji = "普通";
                if (ds.Tables[w].Rows[0]["type"].ToString().IndexOf("start") == 0)
                {
                    wsbiaoji = "起点";
                }
                if (ds.Tables[w].Rows[0]["type"].ToString().IndexOf("end") == 0)
                {
                    wsbiaoji = "终点";
                }
                alsql.Add("INSERT INTO  ZZZ_workprocess_sub(wsid, wswmid, wsname, wsbiaoji, ws_gx_nodeid, ws_gx_ru, ws_gx_chu, ws_gx_xia,  ws_gx_shang, ws_gx_mod, wsbeizhu) VALUES('"+ ht_forUI["idforedit"].ToString() + "_" + ds.Tables[w].TableName + "', @wmid, '" + ds.Tables[w].TableName + "', '" + wsbiaoji + "', '" + ds.Tables[w].TableName + "', '', '', '',  '', '', '"+ ds.Tables[w].Rows[0]["name"].ToString() + "') ");
            }
            if (ds.Tables[w].TableName.IndexOf("workprocess_area_line_") >= 0)
            {
                string from = ds.Tables[w].Rows[0]["from"].ToString();
                string to = ds.Tables[w].Rows[0]["to"].ToString();
                alsql.Add("UPDATE  ZZZ_workprocess_sub set ws_gx_xia=ws_gx_xia + '" + ht_forUI["idforedit"].ToString() + "_" + to + "' + ',' where wsid= '" + ht_forUI["idforedit"].ToString() + "_" + from + "'");
                alsql.Add("UPDATE  ZZZ_workprocess_sub set ws_gx_shang=ws_gx_shang + '" + ht_forUI["idforedit"].ToString() + "_" + from + "' + ',' where wsid= '" + ht_forUI["idforedit"].ToString() + "_" + to + "'");
            }
        }


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
        param.Add("@wmid", ht_forUI["idforedit"].ToString());

        return_ht = I_DBL.RunParam_SQL("select  top 1 *,wmjsonstr as workprocess_area_json_foredit from ZZZ_workprocess_main where wmid=@wmid", "数据记录", param);

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
