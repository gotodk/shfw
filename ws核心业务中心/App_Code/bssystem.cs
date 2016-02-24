using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Web.Script.Serialization;
using System.Numerics;
using System.Reflection;

/// <summary>
/// 核心业务的相关处理接口
/// </summary>
[WebService(Namespace = "http://corebusiness.aftipc.com/", Description = "V1.00->xxx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class bssystem : System.Web.Services.WebService
{

    public bssystem()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    # region  基础前置方法

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
    /// 是否开启防篡改验证
    /// </summary>
    /// <returns></returns>
    private bool IsMD5check()
    {
        return true;
    }

    /// <summary>
    /// 测试该接口是否还活着(每个接口必备)
    /// </summary>
    /// <param name="temp">随便传</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "测试接口", Description = "测试该接口是否还活着(每个接口必备)")]
    public string onlinetest(string temp)
    {
        //根据不同的传入值，后续可以检查不同的东西，比如这个接口所连接的数据库，比如进程池，服务器空间等等。。。
        return "ok";
    }


    #endregion








    #region 演示demo代码







    /// <summary>
    /// 处理提交上来的demo添加表单
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "提交表单demo", Description = "界面框架中提交表单模板的处理演示(新增)")]
    public DataSet DemoAdd(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = CombGuid.GetMewIdFormSequence("test");
        param.Add("@SID", guid);
        param.Add("@Sname", ht_forUI["zhanghao"].ToString());
        param.Add("@Spassword", ht_forUI["mima"].ToString());
        param.Add("@Ssex", ht_forUI["xingbie"].ToString());
        param.Add("@Scity", ht_forUI["shengfen"].ToString());
        param.Add("@Sdiqu", ht_forUI["diqu"].ToString());
        param.Add("@Sint", ht_forUI["zhengshu"].ToString());
        param.Add("@Sdecimal", ht_forUI["erweixiao"].ToString());
        param.Add("@Stime", ht_forUI["yigeriqi"].ToString());
        param.Add("@Stime_begin", ht_forUI["riqiqujian1"].ToString());
        param.Add("@Stime_end", ht_forUI["riqiqujian2"].ToString());
        param.Add("@Smoney", ht_forUI["zhengshu"].ToString());
        param.Add("@Sshouji", ht_forUI["shouji"].ToString());


        string bianjiqi_html = StringOP.uncMe(ht_forUI["bianjiqi_html"].ToString(), "mima");//编辑器值完整html
        string bianjiqi_text = StringOP.uncMe(ht_forUI["bianjiqi_text"].ToString(), "mima");//编辑器值只保留文字
        param.Add("@Sbeizhu", bianjiqi_html);//把html代码放入备注，只是演示

        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("INSERT INTO demouser(SID ,Sname ,Spassword ,Ssex  ,Scity  ,Sdiqu  ,Sint  ,Sdecimal  ,Stime  ,Stime_begin  ,Stime_end ,Smoney ,Sshouji  ,Sbeizhu ) VALUES(@SID ,@Sname ,@Spassword ,@Ssex  ,@Scity  ,@Sdiqu  ,@Sint  ,@Sdecimal  ,@Stime  ,@Stime_begin  ,@Stime_end ,@Smoney ,@Sshouji  ,@Sbeizhu )");



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "演示表单提交成功！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }
    /// <summary>
    /// 处理提交上来的demo修改表单
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "修改表单demo", Description = "界面框架中提交表单模板的处理演示(修改)")]
    public DataSet DemoEdit(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@SID", ht_forUI["idforedit"].ToString());
        param.Add("@Sname", ht_forUI["zhanghao"].ToString());
        param.Add("@Spassword", ht_forUI["mima"].ToString());
        param.Add("@Ssex", ht_forUI["xingbie"].ToString());
        param.Add("@Scity", ht_forUI["shengfen"].ToString());
        param.Add("@Sdiqu", ht_forUI["diqu"].ToString());
        param.Add("@Sint", ht_forUI["zhengshu"].ToString());
        param.Add("@Sdecimal", ht_forUI["erweixiao"].ToString());
        param.Add("@Stime", ht_forUI["yigeriqi"].ToString());
        param.Add("@Stime_begin", ht_forUI["riqiqujian1"].ToString());
        param.Add("@Stime_end", ht_forUI["riqiqujian2"].ToString());
        param.Add("@Smoney", ht_forUI["zhengshu"].ToString());
        param.Add("@Sshouji", ht_forUI["shouji"].ToString());
        //param.Add("@Sbeizhu", ht_forUI["beizhu"].ToString());

        if (ht_forUI.Contains("bianjiqi_html"))
        {
            string bianjiqi_html = StringOP.uncMe(ht_forUI["bianjiqi_html"].ToString(), "mima");//编辑器值完整html
            string bianjiqi_text = StringOP.uncMe(ht_forUI["bianjiqi_text"].ToString(), "mima");//编辑器值只保留文字
            param.Add("@Sbeizhu", bianjiqi_html);//把html代码放入备注，只是演示
        }
        else
        {
            param.Add("@Sbeizhu", "");//把html代码放入备注，只是演示
        }







        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("UPDATE demouser SET  Sname=@Sname ,Spassword=@Spassword ,Ssex=@Ssex  ,Scity=@Scity  ,Sdiqu=@Sdiqu  ,Sint=@Sint  ,Sdecimal=@Sdecimal  ,Stime=@Stime  ,Stime_begin=@Stime_begin  ,Stime_end=@Stime_end ,Smoney=@Smoney ,Sshouji=@Sshouji  ,Sbeizhu=@Sbeizhu   where SID=@SID");




        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "演示表单修改成功！";
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
    /// 删除数据demo接口
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "删除数据demo", Description = "界面框架中删除数据的接口")]
    public string DemoDel(DataTable parameter_forUI)
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

        //存在有效目标才删除
        if (ht_forUI.Contains("ajaxrun") && ht_forUI["ajaxrun"].ToString() == "del" && ht_forUI.Contains("oper") && ht_forUI["oper"].ToString() == "del" && ht_forUI.Contains("id") && ht_forUI["id"].ToString().Trim() != "")
        {
            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            Hashtable param = new Hashtable();

            Hashtable return_ht = new Hashtable();
            ArrayList alsql = new ArrayList();


            //删除数据表里的数据 
            string[] delids = ht_forUI["id"].ToString().Split(',');
            for (int d = 0; d < delids.Length; d++)
            {
                param.Add("@SID_" + d, delids[d]);

                alsql.Add("delete demouser  where SID=@SID_" + d);
            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                ;
            }
            else
            {
                ;
            }
        }



        return "";
    }

    /// <summary>
    /// 获取某条数据，用于修改前的界面显示
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取数据demo", Description = "界面框架中获取数据接口演示")]
    public DataSet DemoGetInfo(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@SID", ht_forUI["idforedit"].ToString());




        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select  top 1 SID,Sname ,Spassword ,Ssex  ,Scity  ,Sdiqu  ,Sint  ,Sdecimal  ,Convert(varchar(10),Stime,120) as Stime  ,Convert(varchar(10),Stime_begin,120) as Stime_begin  ,Convert(varchar(10),Stime_end,120) as Stime_end ,Smoney ,Sshouji  ,Sbeizhu ,Stupian from demouser where SID=@SID", "数据记录", param);

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
            if (redb.Rows[0]["Stupian"].ToString() != "")
            {
                Hashtable return_ht_tu = new Hashtable();
                Hashtable param_tu = new Hashtable();
                param_tu.Add("@TStupian", redb.Rows[0]["Stupian"].ToString());
                return_ht_tu = I_DBL.RunParam_SQL("select  Ttupianpath from demoimages where TStupian=@TStupian", "图片记录", param_tu);

                if ((bool)(return_ht_tu["return_float"]))
                {
                    DataTable redb_tu = ((DataSet)return_ht_tu["return_ds"]).Tables["图片记录"].Copy();
                    if (redb_tu.Rows.Count > 0)
                    {
                        dsreturn.Tables.Add(redb_tu);
                    }
                }

            }








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



    /// <summary>
    /// 获取演示分页数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取分页数据demo", Description = "界面框架中获取分页数据接口演示")]
    public DataSet DemoGetPage(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        try
        {


            DataSet ds_page;

            string[] page = new string[2] { (Convert.ToInt32(ht_forUI["R_PageNumber"]) - 1).ToString(), ht_forUI["R_PageSize"].ToString() };
            //调用执行方法获取数据
            pagerdemo pd = new pagerdemo();
            ds_page = pd.SetPagerInit(page);

            DataSet ds_res = new DataSet();//待返回的结果集 

            //设置分页功能需要的参数
            //dt_page.Rows[0]["this_dblink"] = "";//一般不用填，默认webconfig中的mainsqlserver。有特殊情况需连接别的数据库时使用，填写webconfig中添加的数据库链接的“name”值。
            // ds_page.Tables[0].Rows[0]["GetCustomersDataPage_NAME"] = " GetCustomersDataPage2 ";  //使用的存储过程名称,不设置的时候默认为GetCustomersDataPage。
            ds_page.Tables[0].Rows[0]["serach_Row_str"] = " SID,Sname ,Spassword ,Ssex  ,Scity  ,Sdiqu  ,Sint  ,Sdecimal  ,Convert(varchar(10),Stime,120) as Stime  ,Convert(varchar(10),Stime_begin,120) as Stime_begin  ,Convert(varchar(10),Stime_end,120) as Stime_end ,Smoney ,Sshouji   ,'assets/images/gallery/thumb-6.jpg' as Stupian,CreateTime "; //检索字段(必须设置)
            ds_page.Tables[0].Rows[0]["search_tbname"] = " demouser  ";  //检索的表(必须设置)
            ds_page.Tables[0].Rows[0]["search_mainid"] = " SID ";  //所检索表的主键(必须设置)




            //处理发过来的表头搜索条件
            string extseearchstr = " ";
            Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
            if (ht_forUI.Contains("mysearchtop"))
            {
                jqsearch_sql js = new jqsearch_sql();
                js.getmysearchtop(ref dic_mysearchtop, ht_forUI["mysearchtop"].ToString());

                //生成条件
                if (dic_mysearchtop.ContainsKey("Sname"))
                {
                    extseearchstr = extseearchstr + " and Sname like '%" + dic_mysearchtop["Sname"] + "%'";
                }
                if (dic_mysearchtop.ContainsKey("time1"))
                {
                    extseearchstr = extseearchstr + " and CreateTime >= '" + dic_mysearchtop["time1"] + " 00:00:00.000'";
                }
                if (dic_mysearchtop.ContainsKey("time2"))
                {
                    extseearchstr = extseearchstr + " and CreateTime <= '" + dic_mysearchtop["time2"] + " 23:59:59.999'";
                }
            }

            if (ht_forUI.Contains("filters"))
            {
                //处理发过来的自带搜索功能的复杂条件
                JavaScriptSerializer js = new JavaScriptSerializer();
                JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

                jqsearch_sql jqsql = new jqsearch_sql();
                string spsp_sql = jqsql.getsql(jg);
                if (spsp_sql.Trim() != "")
                {
                    extseearchstr = extseearchstr + " and " + spsp_sql;
                }


            }


            //给条件赋值
            ds_page.Tables[0].Rows[0]["search_str_where"] = " 1=1 " + extseearchstr;  //检索条件(必须设置)

            //处理发过来的排序参数
            if (!ht_forUI.Contains("R_OrderBy") || (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString().Trim() == ""))
            {
                //设定默认排序
                ds_page.Tables[0].Rows[0]["search_paixu"] = " desc ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " CreateTime ";  //用于排序的字段(必须设置) 
            }
            else
            {
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + ht_forUI["R_Sort"].ToString() + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + ht_forUI["R_OrderBy"].ToString() + " ";  //用于排序的字段(必须设置)   
            }




            //调用执行方法获取数据
            return pd.GetPagerDB(ds_page);
        }
        catch (Exception ex)
        {
            return null;
        }






    }

    # endregion

    # region 登录验证处理

    /// <summary>
    /// 修改登录密码
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "修改登录密码", Description = "修改登录密码")]
    public DataSet AuthEditLoginPassword(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@UAid", ht_forUI["idforedit"].ToString());
        //对密码进行加密
        string mima_old_enc = StringOP.encMe(ht_forUI["mima_old"].ToString().Trim(), "mima");
        param.Add("@mima_old", mima_old_enc);
        string mima_new_enc = StringOP.encMe(ht_forUI["mima_new"].ToString().Trim(), "mima");
        param.Add("@mima_new", mima_new_enc);



        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("UPDATE auth_users_auths SET  Uloginpassword=@mima_new     where UAid=@UAid  and  Uloginpassword=@mima_old");

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            if (Convert.ToInt32(return_ht["return_other"]) > 0)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "新密码设置成功！";
            }
            else
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "修改失败，原始密码无法通过验证！";
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
    /// 后台管理登录验证
    /// </summary>
    /// <param name="zhanghao">账号</param>
    /// <param name="mima">密码</param>
    /// <param name="ip">ip地址</param>
    /// <returns></returns>
    [WebMethod(MessageName = "后台管理登录验证", Description = "后台管理登录验证")]
    public DataSet CheckLogin_Back(string zhanghao, string mima, string ip)
    {



        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        param.Add("@Uloginname", zhanghao);
        param.Add("@Uloginpassword", StringOP.encMe(mima.Trim(), "mima"));



        Hashtable return_ht = new Hashtable();

        //密码验证，其他相关用户状态验证要独立出去，不能混合在这个语句中
        return_ht = I_DBL.RunParam_SQL("select  top 1 UAid,Uloginname ,Uattrcode ,Unumber1  ,Unumber2  ,Unumber3,Unumber4,Unumber5,Uingroups,SuperUser,'0' as UfinalUnumber1,'0' as UfinalUnumber2,'0' as UfinalUnumber3,'0' as UfinalUnumber4,'0' as UfinalUnumber5 from auth_users_auths where Uloginname=@Uloginname and Uloginpassword=@Uloginpassword", "用户信息", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["用户信息"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err_olnypassworderr";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "账号或密码错误!";
                return dsreturn;
            }



            //重新处理权限，把最终的权值算出来赋上值，权限验证真正用的是这个
            //计算UfinalUnumber的新值
            BigInteger groupNum1 = 0;//组权限搞出来的值
            BigInteger groupNum2 = 0;
            BigInteger groupNum3 = 0;
            BigInteger groupNum4 = 0;
            BigInteger groupNum5 = 0;
            string tiaojian = redb.Rows[0]["Uingroups"].ToString();
            if (tiaojian.Trim() == "")
            { tiaojian = "0"; }
            Hashtable htyiju = I_DBL.RunProc("select Unumber1,Unumber2,Unumber3,Unumber4,Unumber5 from auth_group where SortID in (" + tiaojian + ")", "计算依据");
            if ((bool)(htyiju["return_float"]))
            {
                DataSet ds_yiju = ((DataSet)htyiju["return_ds"]).Copy();
                //累加所有组权限
                for (int i = 0; i < ds_yiju.Tables["计算依据"].Rows.Count; i++)
                {
                    groupNum1 = groupNum1 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber1"].ToString());
                    groupNum2 = groupNum2 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber2"].ToString());
                    groupNum3 = groupNum3 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber3"].ToString());
                    groupNum4 = groupNum4 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber4"].ToString());
                    groupNum5 = groupNum5 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber5"].ToString());
                }
                //把组权限累加到直接权限上产生最终权限
                redb.Rows[0]["UfinalUnumber1"] = BigInteger.Parse(redb.Rows[0]["Unumber1"].ToString()) | groupNum1;
                redb.Rows[0]["UfinalUnumber2"] = BigInteger.Parse(redb.Rows[0]["Unumber2"].ToString()) | groupNum2;
                redb.Rows[0]["UfinalUnumber3"] = BigInteger.Parse(redb.Rows[0]["Unumber3"].ToString()) | groupNum3;
                redb.Rows[0]["UfinalUnumber4"] = BigInteger.Parse(redb.Rows[0]["Unumber4"].ToString()) | groupNum4;
                redb.Rows[0]["UfinalUnumber5"] = BigInteger.Parse(redb.Rows[0]["Unumber5"].ToString()) | groupNum5;


                dsreturn.Tables.Add(redb);
                //记录登录ip
                ArrayList lasp = new ArrayList();
                lasp.Add("INSERT INTO auth_login_ip (Lid,LUAid,Lip) VALUES ('" + CombGuid.GetNewCombGuid("IP") + "','" + redb.Rows[0]["UAid"].ToString() + "','" + ip + "')");
                I_DBL.RunParam_SQL(lasp);
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
            }
            else
            {
                //枚得不到正常的计算依据，不再继续保存
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "获取组权限时出现问题！";
            }


        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：登录失败";
        }





        return dsreturn;
    }
    #  endregion

    # region 权限处理相关接口


    /// <summary>
    /// 获取功能菜单数据集(制定表名)
    /// </summary>
    /// <param name="sTable">表名</param>
    /// <param name="iSortID">欲调用的分类ID(0或者此ID不存在,即调用所有)</param>
    /// <param name="iCond">条件(1包含本级ID和所有下级ID,2包含本级ID和子ID,3不包含本级ID的所有下级ID,4不包含本级ID的子ID)</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取菜单数据", Description = "根据表名获取菜单数据，所有无限分类通用")]
    public DataTable GetMenuData_mytb(string sTable, int iSortID, int iCond)
    {
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        DataSet DataSet_this = new DataSet();
        Hashtable return_ht = new Hashtable();
        Hashtable putin_ht = new Hashtable();
        putin_ht["sField"] = "*";
        putin_ht["sTable"] = sTable;
        putin_ht["iSortID"] = iSortID;
        putin_ht["iCond"] = iCond;
        return_ht = I_DBL.RunProc_CMD("sp_Util_Sort_SELECT", "菜单表", putin_ht);
        if (Convert.ToBoolean(return_ht["return_float"]))
        {
            DataSet_this = (DataSet)return_ht["return_ds"];
            if (DataSet_this != null)
            {
                return DataSet_this.Tables[0];

            }
            else
            {
                return null;
            }
        }
        else
        {
            //显示错误
            //MessageBox.Show(return_ht["return_errmsg"].ToString(), "数据库操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;

        }
    }

    /// <summary>
    /// 菜单维护操作
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "菜单维护操作", Description = "操作菜单数据，修改不通用要单独加代码，其他通用")]
    public string EditMenuData(DataTable dt_request)
    {
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable putin_ht = new Hashtable();
        switch (dt_request.Rows[0]["buttonid"].ToString())
        {
            case "tianjia":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortParentID"] = dt_request.Rows[0]["SortID"].ToString();
                putin_ht["sSortName"] = dt_request.Rows[0]["add_SortName"].ToString().Replace("[", "").Replace("]", "").Replace("|", "").Replace(",", "").Replace("'", "");
                I_DBL.RunProc_CMD("sp_Util_Sort_INSERT", "操作", putin_ht);
                break;
            case "xiugai":
                putin_ht["@SortID"] = dt_request.Rows[0]["SortID"].ToString();
                putin_ht["@SortName"] = dt_request.Rows[0]["ee_SortName"].ToString().Replace("[", "").Replace("]", "").Replace("|", "").Replace(",", "").Replace("'", "");
                if (dt_request.Rows[0]["dbtbname"].ToString() == "auth_group")
                {
                    //权限组的处理
                    putin_ht["@Unumber1"] = dt_request.Rows[0]["ee_Unumber1_qx"].ToString();
                    putin_ht["@Unumber2"] = dt_request.Rows[0]["ee_Unumber2_qx"].ToString();
                    putin_ht["@Unumber3"] = dt_request.Rows[0]["ee_Unumber3_qx"].ToString();
                    putin_ht["@Unumber4"] = dt_request.Rows[0]["ee_Unumber4_qx"].ToString();
                    putin_ht["@Unumber5"] = dt_request.Rows[0]["ee_Unumber5_qx"].ToString();
                    ArrayList alsql = new ArrayList();
                    alsql.Add("UPDATE " + dt_request.Rows[0]["dbtbname"].ToString() + " SET  SortName=@SortName,Unumber1=@Unumber1,Unumber2=@Unumber2,Unumber3=@Unumber3,Unumber4=@Unumber4,Unumber5=@Unumber5   where SortID=@SortID");
                    I_DBL.RunParam_SQL(alsql, putin_ht);
                }
                else
                {
                    //一般菜单
                    putin_ht["@m_url"] = dt_request.Rows[0]["ee_m_url"].ToString();
                    putin_ht["@m_tip"] = dt_request.Rows[0]["ee_m_tip"].ToString();
                    putin_ht["@m_tag"] = dt_request.Rows[0]["ee_m_tag"].ToString();
                    putin_ht["@m_ico"] = dt_request.Rows[0]["ee_m_ico"].ToString();
                    if (dt_request.Rows[0]["ee_m_show0"].ToString().ToLower() == "true")
                    {
                        putin_ht["@m_show"] = "隐藏";
                    }
                    else
                    {
                        putin_ht["@m_show"] = "不隐藏";
                    }

                    ArrayList alsql = new ArrayList();
                    alsql.Add("UPDATE " + dt_request.Rows[0]["dbtbname"].ToString() + " SET  SortName=@SortName ,m_url=@m_url,m_tip=@m_tip,m_tag=@m_tag,m_ico=@m_ico,m_show=@m_show   where SortID=@SortID");
                    I_DBL.RunParam_SQL(alsql, putin_ht);
                }
                break;
            case "yidong":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortID"] = dt_request.Rows[0]["SortID"].ToString();
                putin_ht["iSortParent"] = dt_request.Rows[0]["move_SortParentID"].ToString();
                //判定是否可以移动，不能调整到自己的子类
                if (putin_ht["iSortID"].ToString() != putin_ht["iSortParent"].ToString())
                {
                    Hashtable ht = I_DBL.RunProc("select SortID,SortParentPath from auth_menu_b where SortID = " + putin_ht["iSortID"].ToString() + " and CHARINDEX(','+CAST('" + putin_ht["iSortParent"].ToString() + "' AS varchar(10))+',',SortParentPath)>0", "判定");
                    if (Convert.ToBoolean(ht["return_float"]))
                    {
                        DataSet ds = (DataSet)ht["return_ds"];
                        if (ds != null && ds.Tables["判定"].Rows.Count < 1)
                        {
                            I_DBL.RunProc_CMD("sp_Util_Sort_MoveSort", "操作", putin_ht);

                        }
                    }
                }

                break;
            case "shang":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortID"] = dt_request.Rows[0]["SortID"].ToString();
                putin_ht["iSortOrder"] = "1";
                I_DBL.RunProc_CMD("sp_Util_Sort_MoveOrder", "操作", putin_ht);
                break;
            case "xia":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortID"] = dt_request.Rows[0]["SortID"].ToString();
                putin_ht["iSortOrder"] = "-1";
                I_DBL.RunProc_CMD("sp_Util_Sort_MoveOrder", "操作", putin_ht);
                break;
            case "shanchu":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortID"] = dt_request.Rows[0]["SortID"].ToString();
                I_DBL.RunProc_CMD("sp_Util_Sort_DELETE", "操作", putin_ht);
                break;
            case "zhengli":
                putin_ht["sTable"] = dt_request.Rows[0]["dbtbname"].ToString();
                putin_ht["iSortID"] = dt_request.Rows[0]["SortID"].ToString();
                I_DBL.RunProc_CMD("sp_Util_Sort_MoveRevise", "操作", putin_ht);
                break;
            default:
                break;
        }
        return "ok";
    }

    /// <summary>
    /// 获取单条菜单数据
    /// </summary>
    /// <param name="tbname">表名</param>
    /// <param name="sortid">sortid</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取单条菜单数据", Description = "获取某个特定id的菜单数据，所有无限分类通用")]
    public DataSet GetMenuData_OneInfo(string tbname, string sortid)
    {


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        param.Add("@sortid", sortid);



        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select  top 1 * from " + tbname + " where SortID=@sortid", "数据记录", param);

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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }


    /// <summary>
    /// 编辑一条权限枚值举值
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "编辑一条权限枚值举值", Description = "编辑一条权限枚值举值")]
    public DataSet AuthEnumEdit(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@ANID", ht_forUI["idforedit"].ToString());
        param.Add("@ANBaseName", ht_forUI["ANBaseName"].ToString());
        param.Add("@ANextendID", ht_forUI["ANextendID"].ToString());
        param.Add("@ANused", ht_forUI["ANused"].ToString());



        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("UPDATE auth_enum_number SET  ANBaseName=@ANBaseName,ANextendID=@ANextendID ,ANused=@ANused   where ANID=@ANID");

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "权限枚举修改成功！";
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
    /// 获取某个权限权值枚举
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取一条权值枚举", Description = "获取某个权限权值枚举")]
    public DataSet GetAuthEnumInfo(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@ANID", ht_forUI["idforedit"].ToString());




        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select  top 1 ANID,ANClass ,ANClassName ,ANBaseName  ,ANBaseNumber ,ANextendID ,ANused from auth_enum_number where ANID=@ANID", "数据记录", param);

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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }


    /// <summary>
    /// 获取所有已启用的权限枚举
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取所有已启用的权限枚举", Description = "获取所有已启用的权限枚举")]
    public DataSet GetAuthEnumAllUsed(string temp)
    {

        //初始化返回值
        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });
        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable return_ht = new Hashtable();
        string yc_sql = " ";
        if (temp == "隐藏开发专用")
        {
            yc_sql = " and ANBaseName <> '开发专用' ";
        }
        return_ht = I_DBL.RunProc("select * from auth_enum_number where ANClass='Unumber1' and ANused=1 "+ yc_sql + "  order by ANBaseName  ; select * from auth_enum_number where ANClass='Unumber2' and ANused=1 order by ANBaseName  ; select * from auth_enum_number where ANClass='Unumber3' and ANused=1 order by ANBaseName  ; select * from auth_enum_number where ANClass='Unumber4' and ANused=1 order by ANBaseName  ; select * from auth_enum_number where ANClass='Unumber5' and ANused=1 order by ANBaseName  ;", "临时名");

        if ((bool)(return_ht["return_float"]))
        {
            DataSet redb = ((DataSet)return_ht["return_ds"]).Copy();
            //重新命名表名
            redb.Tables[0].TableName = "Unumber1";
            redb.Tables[1].TableName = "Unumber2";
            redb.Tables[2].TableName = "Unumber3";
            redb.Tables[3].TableName = "Unumber4";
            redb.Tables[4].TableName = "Unumber5";

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
            redb.Tables.Add(dsreturn.Tables["返回值单条"].Copy());
            return redb;

        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "获取失败";
            return dsreturn;
        }





        return dsreturn;
    }



    /// <summary>
    /// 获取权限权值枚举表分页数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "权限枚举表分页数据", Description = "获取权限权值枚举表分页数据")]
    public DataSet GetAuthEnumNumberList(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        try
        {


            DataSet ds_page;

            string[] page = new string[2] { (Convert.ToInt32(ht_forUI["R_PageNumber"]) - 1).ToString(), ht_forUI["R_PageSize"].ToString() };
            //调用执行方法获取数据
            pagerdemo pd = new pagerdemo();
            ds_page = pd.SetPagerInit(page);

            DataSet ds_res = new DataSet();//待返回的结果集 

            //设置分页功能需要的参数
            //dt_page.Rows[0]["this_dblink"] = "";//一般不用填，默认webconfig中的mainsqlserver。有特殊情况需连接别的数据库时使用，填写webconfig中添加的数据库链接的“name”值。
            // ds_page.Tables[0].Rows[0]["GetCustomersDataPage_NAME"] = " GetCustomersDataPage2 ";  //使用的存储过程名称,不设置的时候默认为GetCustomersDataPage。
            ds_page.Tables[0].Rows[0]["serach_Row_str"] = " ANID,ANClass ,ANClassName ,ANBaseName  ,ANBaseNumber,ANBaseNumber_p ,ANextendID ,ANused "; //检索字段(必须设置)
            ds_page.Tables[0].Rows[0]["search_tbname"] = " auth_enum_number  ";  //检索的表(必须设置)
            ds_page.Tables[0].Rows[0]["search_mainid"] = " ANID ";  //所检索表的主键(必须设置)




            //处理发过来的表头搜索条件
            string extseearchstr = " ";
            Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
            if (ht_forUI.Contains("mysearchtop"))
            {
                jqsearch_sql js = new jqsearch_sql();
                js.getmysearchtop(ref dic_mysearchtop, ht_forUI["mysearchtop"].ToString());

                //生成条件
                if (dic_mysearchtop.ContainsKey("ANClassName_ss"))
                {
                    extseearchstr = extseearchstr + " and ANClassName = '" + dic_mysearchtop["ANClassName_ss"] + "'";
                }
                if (dic_mysearchtop.ContainsKey("ANused_ss"))
                {
                    extseearchstr = extseearchstr + " and ANused = '" + dic_mysearchtop["ANused_ss"] + "'";
                }

            }

            if (ht_forUI.Contains("filters"))
            {
                //处理发过来的自带搜索功能的复杂条件
                JavaScriptSerializer js = new JavaScriptSerializer();
                JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

                jqsearch_sql jqsql = new jqsearch_sql();
                string spsp_sql = jqsql.getsql(jg);
                if (spsp_sql.Trim() != "")
                {
                    extseearchstr = extseearchstr + " and " + spsp_sql;
                }


            }


            //给条件赋值
            ds_page.Tables[0].Rows[0]["search_str_where"] = " 1=1 " + extseearchstr;  //检索条件(必须设置)

            //处理发过来的排序参数
            if (!ht_forUI.Contains("R_OrderBy") || (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString().Trim() == ""))
            {
                //设定默认排序
                ds_page.Tables[0].Rows[0]["search_paixu"] = " asc ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " ANID ";  //用于排序的字段(必须设置) 
            }
            else
            {
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + ht_forUI["R_Sort"].ToString() + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + ht_forUI["R_OrderBy"].ToString() + " ";  //用于排序的字段(必须设置)   
            }




            //调用执行方法获取数据
            return pd.GetPagerDB(ds_page);
        }
        catch (Exception ex)
        {
            return null;
        }






    }



    /// <summary>
    /// 用户权限简单浏览分页数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "用户权限简单浏览分页数据", Description = "用户权限简单浏览分页数据")]
    public DataSet GetAuthUserListSimple(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        try
        {


            DataSet ds_page;

            string[] page = new string[2] { (Convert.ToInt32(ht_forUI["R_PageNumber"]) - 1).ToString(), ht_forUI["R_PageSize"].ToString() };
            //调用执行方法获取数据
            pagerdemo pd = new pagerdemo();
            ds_page = pd.SetPagerInit(page);

            DataSet ds_res = new DataSet();//待返回的结果集 

            //设置分页功能需要的参数
            //dt_page.Rows[0]["this_dblink"] = "";//一般不用填，默认webconfig中的mainsqlserver。有特殊情况需连接别的数据库时使用，填写webconfig中添加的数据库链接的“name”值。
            // ds_page.Tables[0].Rows[0]["GetCustomersDataPage_NAME"] = " GetCustomersDataPage2 ";  //使用的存储过程名称,不设置的时候默认为GetCustomersDataPage。
            ds_page.Tables[0].Rows[0]["serach_Row_str"] = " * "; //检索字段(必须设置)
            ds_page.Tables[0].Rows[0]["search_tbname"] = " auth_users_auths  ";  //检索的表(必须设置)
            ds_page.Tables[0].Rows[0]["search_mainid"] = " UAid ";  //所检索表的主键(必须设置)




            //处理发过来的表头搜索条件
            string extseearchstr = " ";
            Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
            if (ht_forUI.Contains("mysearchtop"))
            {
                ;
            }

            if (ht_forUI.Contains("filters"))
            {
                //处理发过来的自带搜索功能的复杂条件
                JavaScriptSerializer js = new JavaScriptSerializer();
                JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

                jqsearch_sql jqsql = new jqsearch_sql();
                string spsp_sql = jqsql.getsql(jg);
                if (spsp_sql.Trim() != "")
                {
                    extseearchstr = extseearchstr + " and " + spsp_sql;
                }


            }


            //给条件赋值
            ds_page.Tables[0].Rows[0]["search_str_where"] = " 1=1 " + extseearchstr;  //检索条件(必须设置)

            //处理发过来的排序参数
            if (!ht_forUI.Contains("R_OrderBy") || (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString().Trim() == ""))
            {
                //设定默认排序
                ds_page.Tables[0].Rows[0]["search_paixu"] = " desc ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " UAid ";  //用于排序的字段(必须设置) 
            }
            else
            {
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + ht_forUI["R_Sort"].ToString() + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + ht_forUI["R_OrderBy"].ToString() + " ";  //用于排序的字段(必须设置)   
            }




            //调用执行方法获取数据
            return pd.GetPagerDB(ds_page);
        }
        catch (Exception ex)
        {
            return null;
        }






    }



    /// <summary>
    /// 获取单个用户权限
    /// </summary>
    /// <param name="uid">uid或账户名</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取单个用户权限", Description = "获取单个用户权限")]
    public DataSet GetOneUserAuthInfo(string uid)
    {

        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        param.Add("@UAid", uid);
        param.Add("@Uloginname", uid);



        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select  top 1 *,'0' as UfinalUnumber1,'0' as UfinalUnumber2,'0' as UfinalUnumber3,'0' as UfinalUnumber4,'0' as UfinalUnumber5 from auth_users_auths where UAid=@UAid or Uloginname=@UAid", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count > 0)
            {
                //重新处理权限，把最终的权值算出来赋上值，权限验证真正用的是这个
                //计算UfinalUnumber的新值
                BigInteger groupNum1 = 0;//组权限搞出来的值
                BigInteger groupNum2 = 0;
                BigInteger groupNum3 = 0;
                BigInteger groupNum4 = 0;
                BigInteger groupNum5 = 0;
                string tiaojian = redb.Rows[0]["Uingroups"].ToString();
                if (tiaojian.IndexOf(",") <= 0)
                {
                    tiaojian = "0";
                }
                Hashtable htyiju = I_DBL.RunProc("select Unumber1,Unumber2,Unumber3,Unumber4,Unumber5 from auth_group where SortID in (" + tiaojian + ")", "计算依据");
                if ((bool)(htyiju["return_float"]))
                {
                    DataSet ds_yiju = ((DataSet)htyiju["return_ds"]).Copy();
                    //累加所有组权限
                    for (int i = 0; i < ds_yiju.Tables["计算依据"].Rows.Count; i++)
                    {
                        groupNum1 = groupNum1 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber1"].ToString());
                        groupNum2 = groupNum2 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber2"].ToString());
                        groupNum3 = groupNum3 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber3"].ToString());
                        groupNum4 = groupNum4 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber4"].ToString());
                        groupNum5 = groupNum5 | BigInteger.Parse(ds_yiju.Tables["计算依据"].Rows[i]["Unumber5"].ToString());
                    }
                    //把组权限累加到直接权限上产生最终权限
                    redb.Rows[0]["UfinalUnumber1"] = BigInteger.Parse(redb.Rows[0]["Unumber1"].ToString()) | groupNum1;
                    redb.Rows[0]["UfinalUnumber2"] = BigInteger.Parse(redb.Rows[0]["Unumber2"].ToString()) | groupNum2;
                    redb.Rows[0]["UfinalUnumber3"] = BigInteger.Parse(redb.Rows[0]["Unumber3"].ToString()) | groupNum3;
                    redb.Rows[0]["UfinalUnumber4"] = BigInteger.Parse(redb.Rows[0]["Unumber4"].ToString()) | groupNum4;
                    redb.Rows[0]["UfinalUnumber5"] = BigInteger.Parse(redb.Rows[0]["Unumber5"].ToString()) | groupNum5;


                    dsreturn.Tables.Add(redb);
                    dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
                    dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
                }
                else
                {
                    //枚得不到正常的计算依据，不再继续保存
                    dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                    dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "计算组权限时出现问题！";
                }




            }
            else
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "找不到指定的用户权限信息！";
            }



        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }


    /// <summary>
    /// 编辑一个用户的权限
    /// </summary>
    /// <param name="UAid">UAid</param>
    /// <param name="Unumber1">Unumber1</param>
    /// <param name="Unumber2">Unumber2</param>
    /// <param name="Unumber3">Unumber3</param>
    /// <param name="Unumber4">UIUnumber4</param>
    /// <param name="Unumber5">Unumber5</param>
    /// <param name="Uingroups">Uingroups</param>
    /// <returns></returns>
    [WebMethod(MessageName = "编辑一个用户的权限", Description = "编辑一个用户的权限")]
    public string EditAuthOneUser(string UAid, string Unumber1, string Unumber2, string Unumber3, string Unumber4, string Unumber5, string Uingroups)
    {


        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        param.Add("@UAid", UAid);
        param.Add("@Unumber1", Unumber1);
        param.Add("@Unumber2", Unumber2);
        param.Add("@Unumber3", Unumber3);
        param.Add("@Unumber4", Unumber4);
        param.Add("@Unumber5", Unumber5);
        param.Add("@Uingroups", Uingroups);






        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("UPDATE auth_users_auths SET  Unumber1=@Unumber1,Unumber2=@Unumber2,Unumber3=@Unumber3,Unumber4=@Unumber4,Unumber5=@Unumber5 ,Uingroups=@Uingroups    where UAid=@UAid");



        return_ht = I_DBL.RunParam_SQL(alsql, param);



        return "ok";
    }

    # endregion

    # region 省市区数据
    /// <summary>
    /// 获取省市区数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取省市区数据", Description = "获取省市区数据")]
    public DataSet GetYHBCity(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        if (ht_forUI["cpq"].ToString() == "p")
        {
            return_ht = I_DBL.RunParam_SQL("select  p_number,p_namestr from AAA_CityList_Promary order by p_number asc", "省市区数据", param);
        }

        if (ht_forUI["cpq"].ToString() == "c")
        {
            param.Add("@p_number", ht_forUI["val_p"].ToString());
            return_ht = I_DBL.RunParam_SQL("select c_number,c_namestr from AAA_CityList_City where p_number = @p_number  order by sfshcs desc,c_number asc", "省市区数据", param);
        }
        if (ht_forUI["cpq"].ToString() == "q")
        {
            param.Add("@c_number", ht_forUI["val_p"].ToString());
            return_ht = I_DBL.RunParam_SQL("select q_number,q_namestr from AAA_CityList_qu where c_number=@c_number order by q_number asc", "省市区数据", param);
        }

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["省市区数据"].Copy();


            dsreturn.Tables.Add(redb);


            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }
    #endregion



    #region 站内信提醒相关
    /// <summary>
    /// 获取站内信右上角提醒
    /// </summary>
    /// <param name="UAid">用户唯一编号</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取站内信右上角提醒", Description = "获取站内信右上角提醒")]
    public DataSet Getuserznx_foralert(string UAid)
    {

        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        param.Add("@UAid", UAid);
        return_ht = I_DBL.RunParam_SQL("select top 10 Zid,'测试' as fromuser, '测试' as fromuser_realname,SUBSTRING(msgtitle,1,10) as msgtitle,addtime ,'/mytutu/defaulttouxiang.jpg' as myshowface from auth_znx as znx   where xuanxiang <> 2 and beread = 0 and touser=@UAid     ", "站内信提醒", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["站内信提醒"].Copy();


            dsreturn.Tables.Add(redb);


            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }

    #endregion


    #region 界面生成相关接口(表单)

    /// <summary>
    /// 获取指定表单界面配置用于生成
    /// </summary>
    /// <param name="formFID">表单唯一标示</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取指定表单界面配置用于生成", Description = "获取指定表单界面配置用于生成")]
    public DataSet GetOneFormsInfo(string formFID)
    {
 

        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        string sql1 = " select top 1 * from FUP_FormsMainInfo where FID = '"+ formFID + "' and F_ok=1 ;";
        string sql2 = " select * from FUP_FormsSubInfo where FS_FID = '" + formFID + "' and FS_ok=1 order by FS_index asc,FSID desc ;";

        return_ht = I_DBL.RunParam_SQL(sql1 + sql2, "xx", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataSet redb = (DataSet)return_ht["return_ds"];
            redb.Tables[0].TableName = "表单配置主表";
            redb.Tables[1].TableName = "表单配置子表";

            //进行处理，以便于下拉菜单、单选框、多选框支持从数据库读取数据
            for (int i = 0; i < redb.Tables["表单配置子表"].Rows.Count; i++)
            {
                string FS_SPPZ_list_static =  redb.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"].ToString();
                if (FS_SPPZ_list_static.Trim().IndexOf("[sql]") == 0)
                {
                    //多行值和显示不相同的情况
                    DataTable dtdz = ((DataSet)(I_DBL.RunProc(FS_SPPZ_list_static.Trim().Remove(0,5), "待转表")["return_ds"])).Tables["待转表"];
                    string dzstr = "";
                    for (int z = 0; z < dtdz.Rows.Count; z++)
                    {
                        dzstr = dzstr + ""+ dtdz.Rows[z][1].ToString() + "|" + dtdz.Rows[z][0].ToString() + ",";
                    }
                    dzstr = dzstr.TrimEnd(',');
                    redb.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"] = dzstr;
                }
                if (FS_SPPZ_list_static.Trim().IndexOf("[sqlone]") == 0)
                {
                    //一行数据用逗号隔开的，一般是枚举表
                    DataTable dtdz = ((DataSet)(I_DBL.RunProc(FS_SPPZ_list_static.Trim().Remove(0, 8), "待转表")["return_ds"])).Tables["待转表"];
                    string dzstr = dtdz.Rows[0][0].ToString();
                    dzstr = dzstr.TrimEnd(',');
                    redb.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"] = dzstr;
                }
            }


            dsreturn.Tables.Add(redb.Tables["表单配置主表"].Copy());
            dsreturn.Tables.Add(redb.Tables["表单配置子表"].Copy());
 

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }





    /// <summary>
    /// 获取弹窗中列表配置
    /// </summary>
    /// <param name="FSID">表单唯一标示</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取弹窗中列表配置", Description = "获取弹窗中列表配置")]
    public DataSet GetOneSubInfoAndSubDialog(string FSID)
    {


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        string sql1 = " select top 1 * from FUP_FormsSubInfo where FSID = '" + FSID + "'   ;";
        string sql2 = " select * from FUP_FormsSubDialog where DID_FSID = '" + FSID + "'  order by DID_px asc,DID desc ;";

        return_ht = I_DBL.RunParam_SQL(sql1 + sql2, "xx", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataSet redb = (DataSet)return_ht["return_ds"];
            redb.Tables[0].TableName = "字段配置主表";
            redb.Tables[1].TableName = "弹窗配置子表";

            for (int i = 0; i < redb.Tables["弹窗配置子表"].Rows.Count; i++)
            {
                string FS_SPPZ_list_static = redb.Tables["弹窗配置子表"].Rows[i]["DID_edit_spset"].ToString();
                if (FS_SPPZ_list_static.Trim().IndexOf("[sql]") == 0)
                {
                    //多行值和显示不相同的情况
                    DataTable dtdz = ((DataSet)(I_DBL.RunProc(FS_SPPZ_list_static.Trim().Remove(0, 5), "待转表")["return_ds"])).Tables["待转表"];
                    string dzstr = "";
                    for (int z = 0; z < dtdz.Rows.Count; z++)
                    {
                        dzstr = dzstr + "" + dtdz.Rows[z][1].ToString() + "|" + dtdz.Rows[z][0].ToString() + ",";
                    }
                    dzstr = dzstr.TrimEnd(',');
                    redb.Tables["弹窗配置子表"].Rows[i]["DID_edit_spset"] = dzstr;
                }
                if (FS_SPPZ_list_static.Trim().IndexOf("[sqlone]") == 0)
                {
                    //一行数据用逗号隔开的，一般是枚举表
                    DataTable dtdz = ((DataSet)(I_DBL.RunProc(FS_SPPZ_list_static.Trim().Remove(0, 8), "待转表")["return_ds"])).Tables["待转表"];
                    string dzstr = dtdz.Rows[0][0].ToString();
                    dzstr = dzstr.TrimEnd(',');
                    redb.Tables["弹窗配置子表"].Rows[i]["DID_edit_spset"] = dzstr;
                }
            }


            dsreturn.Tables.Add(redb.Tables["字段配置主表"].Copy());
            dsreturn.Tables.Add(redb.Tables["弹窗配置子表"].Copy());


            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }





    /// <summary>
    /// 获取弹窗内分页数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取弹窗内分页数据", Description = "获取弹窗内分页数据")]
    public DataSet GetDialogPageDB(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略
        DataSet ds_DD = new DataSet();
        if (ht_forUI.Contains("this_extforinfoFSID"))
        {
            //获取数据库取数据的配置
            ds_DD = GetOneSubInfoAndSubDialog(ht_forUI["this_extforinfoFSID"].ToString());
        }
        else
        {
            return null;
        }
        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        try
        {


            DataSet ds_page;

            string[] page = new string[2] { (Convert.ToInt32(ht_forUI["R_PageNumber"]) - 1).ToString(), ht_forUI["R_PageSize"].ToString() };
            //调用执行方法获取数据
            pagerdemo pd = new pagerdemo();
            ds_page = pd.SetPagerInit(page);

            DataSet ds_res = new DataSet();//待返回的结果集 

            //设置分页功能需要的参数
            //dt_page.Rows[0]["this_dblink"] = "";//一般不用填，默认webconfig中的mainsqlserver。有特殊情况需连接别的数据库时使用，填写webconfig中添加的数据库链接的“name”值。
            // ds_page.Tables[0].Rows[0]["GetCustomersDataPage_NAME"] = " GetCustomersDataPage2 ";  //使用的存储过程名称,不设置的时候默认为GetCustomersDataPage。
            //ds_page.Tables[0].Rows[0]["serach_Row_str"] = " SID,Sname ,Spassword ,Ssex  ,Scity  ,Sdiqu  ,Sint  ,Sdecimal  ,Convert(varchar(10),Stime,120) as Stime  ,Convert(varchar(10),Stime_begin,120) as Stime_begin  ,Convert(varchar(10),Stime_end,120) as Stime_end ,Smoney ,Sshouji   ,'assets/images/gallery/thumb-6.jpg' as Stupian,CreateTime "; //检索字段(必须设置)
            string field_str = "";
            if (ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_field"].ToString().Trim() == "")
            {
                field_str = "*";
            }
            else
            {
                field_str = ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_field"].ToString();
            }
            ds_page.Tables[0].Rows[0]["serach_Row_str"] = " " + ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_key"].ToString() + " as  jqgird_spid , " + field_str + " ";
            //ds_page.Tables[0].Rows[0]["search_tbname"] = " demouser  ";  //检索的表(必须设置)
            ds_page.Tables[0].Rows[0]["search_tbname"] = " " + ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_datatable"].ToString() + " ";
            //ds_page.Tables[0].Rows[0]["search_mainid"] = " SID ";  //所检索表的主键(必须设置)
            ds_page.Tables[0].Rows[0]["search_mainid"] = " " + ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_key"].ToString() + " ";

            //默认条件
            string default_where = "   ";
            if (ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_where"].ToString().Trim() == "")
            {
                default_where = "   ";
            }
            else
            {
                default_where = " and " + ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_where"].ToString().Replace("{idforedit}", "'"+ ht_forUI["idforedit"].ToString() + "'") + " ";
            }

            //处理发过来的表头搜索条件
            string extseearchstr = " ";
            Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
            if (ht_forUI.Contains("mysearchtop"))
            {
                jqsearch_sql js = new jqsearch_sql();
                js.getmysearchtop(ref dic_mysearchtop, ht_forUI["mysearchtop"].ToString());

                //生成条件
                //if (dic_mysearchtop.ContainsKey("Sname"))
                //{
                //    extseearchstr = extseearchstr + " and Sname like '%" + dic_mysearchtop["Sname"] + "%'";
                //}
                //if (dic_mysearchtop.ContainsKey("time1"))
                //{
                //    extseearchstr = extseearchstr + " and CreateTime >= '" + dic_mysearchtop["time1"] + " 00:00:00.000'";
                //}
                //if (dic_mysearchtop.ContainsKey("time2"))
                //{
                //    extseearchstr = extseearchstr + " and CreateTime <= '" + dic_mysearchtop["time2"] + " 23:59:59.999'";
                //}
            }

            if (ht_forUI.Contains("filters"))
            {
                //处理发过来的自带搜索功能的复杂条件
                JavaScriptSerializer js = new JavaScriptSerializer();
                JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

                jqsearch_sql jqsql = new jqsearch_sql();
                string spsp_sql = jqsql.getsql(jg);
                if (spsp_sql.Trim() != "")
                {
                    extseearchstr = extseearchstr + " and " + spsp_sql;
                }


            }


            //给条件赋值
            ds_page.Tables[0].Rows[0]["search_str_where"] = " 1=1 " + default_where + extseearchstr;  //检索条件(必须设置)

            //处理发过来的排序参数
            if (!ht_forUI.Contains("R_OrderBy") || (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString().Trim() == ""))
            {
                string paixu = ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_order"].ToString().Trim();
                string[] paixu_arr = paixu.Split(' ');
                string pp = "";
                string search_paixuZD = paixu_arr[0];
                paixu_arr[0] = "";
                string search_paixu = string.Join(" ", paixu_arr);
                //设定默认排序
                ds_page.Tables[0].Rows[0]["search_paixu"] = " "+ search_paixu + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " "+ search_paixuZD + " ";  //用于排序的字段(必须设置) 
            }
            else
            {
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + ht_forUI["R_Sort"].ToString() + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + ht_forUI["R_OrderBy"].ToString() + " ";  //用于排序的字段(必须设置)   
            }




            //调用执行方法获取数据
            return pd.GetPagerDB(ds_page);
        }
        catch (Exception ex)
        {
            //记录日志
            return null;
        }






    }








    /// <summary>
    /// 界面例子新增数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "界面例子新增数据", Description = "界面例子新增数据")]
    public DataSet FFadd(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = CombGuid.GetMewIdFormSequence("FUP_FormsDemoDB");
        param.Add("@id", guid);
        param.Add("@fieldtest", ht_forUI["fieldtest"].ToString());
 
 
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("INSERT INTO FUP_FormsDemoDB(id ,fieldtest) VALUES(@id ,@fieldtest )");



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "演示表单提交成功！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }




    /// <summary>
    /// 界面例子修改数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "界面例子修改数据", Description = "界面例子修改数据")]
    public DataSet FFedit(DataTable parameter_forUI)
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
        if (ht_forUI["idforedit"].ToString().Trim() == "")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有明确的修改目标！";
            return dsreturn;
        }

        //开始真正的处理，根据业务逻辑操作数据库

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
 
        param.Add("@id", ht_forUI["idforedit"].ToString());
        param.Add("@fieldtest", ht_forUI["fieldtest"].ToString());


        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("UPDATE FUP_FormsDemoDB SET fieldtest = @fieldtest where id=@id ");



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "演示表单修改成功！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }




    /// <summary>
    /// 界面例子获取待编辑数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "界面例子获取待编辑数据", Description = "界面例子获取待编辑数据")]
    public DataSet FFGetInfo(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@id", ht_forUI["idforedit"].ToString());




        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select  top 1 id, fieldtest, mima, xialakuang, xingbie, quanxian, xialakuangduoxuan, zhengshushuliang, erweixiao,  Convert(varchar(10),yigeriqi,120) as yigeriqi,   Convert(varchar(10),riqiqujian1,120) as riqiqujian1,   Convert(varchar(10),riqiqujian2,120) as riqiqujian2, beizhu, bianjiqi, yhb_city_Promary_diquxian,yhb_city_City_diquxian,yhb_city_Qu_diquxian, zhanghao from FUP_FormsDemoDB where id=@id", "数据记录", param);

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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，获取数据失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }



    #endregion



    #region 界面生成相关接口(列表)




    /// <summary>
    /// 保存或者获取用户布局
    /// </summary>
    /// <param name="uaid">用户编号</param>
    /// <param name="lx">操作对象类型</param>
    /// <param name="id">操作对象编号</param>
    /// <param name="jsonstr">json字符串配置</param>
    /// <param name="sp">保存,重置,获取</param>
    /// <returns></returns>
    [WebMethod(MessageName = "保存或者获取用户布局", Description = "保存或者获取用户布局")]
    public DataSet yonghubuju(string uaid, string lx, string id, string jsonstr, string sp)
    {


        if (sp == "保存" || sp == "重置")
        {
            //初始化返回值
            DataSet dsreturn = initReturnDataSet().Clone();
            dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            Hashtable param = new Hashtable();
            param.Add("@uaid", uaid);
            param.Add("@fsid", id);
            param.Add("@jsonstr", StringOP.uncMe(jsonstr, "mima"));

            Hashtable return_ht = new Hashtable();
            ArrayList alsql = new ArrayList();
            alsql.Add("delete FUP_FormsList_user_buju where uaid=@uaid and fsid=@fsid ");
            if (sp == "保存")
            { alsql.Add("INSERT INTO  FUP_FormsList_user_buju( uaid ,fsid,jsonstr) VALUES(@uaid ,@fsid,@jsonstr) "); }
           




            return_ht = I_DBL.RunParam_SQL(alsql, param);




            if ((bool)(return_ht["return_float"]))
            {

                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = sp + "用户布局成功！";
            }
            else
            {
                //其实要记录日志，而不是输出，这里只是演示
                //dsreturn.Tables.Add(parameter_forUI.Copy());
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，失败：" + return_ht["return_errmsg"].ToString();
            }





            return dsreturn;
        }
        if (sp == "获取")
        {
            //初始化返回值
            DataSet dsreturn = initReturnDataSet().Clone();
            dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

            //参数合法性各种验证，这里省略

            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
            Hashtable return_ht = new Hashtable();
            Hashtable param = new Hashtable();
            param.Add("@uaid", uaid);
            param.Add("@fsid", id);
            return_ht = I_DBL.RunParam_SQL("select  top 1 * from FUP_FormsList_user_buju where uaid=@uaid and fsid=@fsid", "自定义布局", param);

            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["自定义布局"].Copy();

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

        return null;

    }



    /// <summary>
    /// 获取通用数据列表配置
    /// </summary>
    /// <param name="FSID">列表唯一标示</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取通用数据列表配置", Description = "获取通用数据列表配置")]
    public DataSet GetFormListInfo(string FSID)
    {


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        string sql1 = " select top 1 * from FUP_FormsList where FSID = '" + FSID + "' and FS_ok=1 ;";
        string sql2 = " select * from FUP_FormsList_field where DID_FSID = '" + FSID + "' and DID_ok=1 order by DID_px asc,DID desc ;";

        return_ht = I_DBL.RunParam_SQL(sql1 + sql2, "xx", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataSet redb = (DataSet)return_ht["return_ds"];
            redb.Tables[0].TableName = "报表配置主表";
            redb.Tables[1].TableName = "字段配置子表";



            if(redb.Tables["报表配置主表"].Rows.Count > 0)
            {
                for(int i = 1;i < 4;i++)
                {
                    string SRE_showname = redb.Tables["报表配置主表"].Rows[0]["SRE_showname_" + i].ToString();
                    if (SRE_showname.IndexOf('*') > 0)
                    {
                        string SRE_showname_nnn = SRE_showname.Split('*')[0];
                        string SRE_showname_ppp = SRE_showname.Split('*')[1];
                        if (SRE_showname_ppp.Trim().IndexOf("[sql]") == 0)
                        {
                            //多行值和显示不相同的情况
                            DataTable dtdz = ((DataSet)(I_DBL.RunProc(SRE_showname_ppp.Trim().Remove(0, 5), "待转表")["return_ds"])).Tables["待转表"];
                            string dzstr = "";
                            for (int z = 0; z < dtdz.Rows.Count; z++)
                            {
                                dzstr = dzstr + "" + dtdz.Rows[z][1].ToString() + "|" + dtdz.Rows[z][0].ToString() + ",";
                            }
                            dzstr = dzstr.TrimEnd(',');
                            redb.Tables["报表配置主表"].Rows[0]["SRE_showname_" + i] = SRE_showname_nnn + "*" + dzstr;
                        }
                        if (SRE_showname_ppp.Trim().IndexOf("[sqlone]") == 0)
                        {
                            //一行数据用逗号隔开的，一般是枚举表
                            DataTable dtdz = ((DataSet)(I_DBL.RunProc(SRE_showname_ppp.Trim().Remove(0, 8), "待转表")["return_ds"])).Tables["待转表"];
                            string dzstr = dtdz.Rows[0][0].ToString();
                            dzstr = dzstr.TrimEnd(',');
                            redb.Tables["报表配置主表"].Rows[0]["SRE_showname_" + i] = SRE_showname_nnn + "*" + dzstr;
                        }
                    }
               

                }
               
            }


            dsreturn.Tables.Add(redb.Tables["报表配置主表"].Copy());
            dsreturn.Tables.Add(redb.Tables["字段配置子表"].Copy());


            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }



    /// <summary>
    /// 获取通用数据列表分页数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取通用数据列表分页数据", Description = "获取通用数据列表分页数据")]
    public DataSet GetFormListPageDB(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略
        DataSet ds_DD = new DataSet();
        if (ht_forUI.Contains("this_extforinfoFSID"))
        {
            //获取数据库取数据的配置
            ds_DD = GetFormListInfo(ht_forUI["this_extforinfoFSID"].ToString());
        }
        else
        {
            return null;
        }
        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        try
        {


            DataSet ds_page;

            string[] page = new string[2] { (Convert.ToInt32(ht_forUI["R_PageNumber"]) - 1).ToString(), ht_forUI["R_PageSize"].ToString() };
            //调用执行方法获取数据
            pagerdemo pd = new pagerdemo();
            ds_page = pd.SetPagerInit(page);

            DataSet ds_res = new DataSet();//待返回的结果集 

            //设置分页功能需要的参数
            //dt_page.Rows[0]["this_dblink"] = "";//一般不用填，默认webconfig中的mainsqlserver。有特殊情况需连接别的数据库时使用，填写webconfig中添加的数据库链接的“name”值。
            // ds_page.Tables[0].Rows[0]["GetCustomersDataPage_NAME"] = " GetCustomersDataPage2 ";  //使用的存储过程名称,不设置的时候默认为GetCustomersDataPage。
            //ds_page.Tables[0].Rows[0]["serach_Row_str"] = " SID,Sname ,Spassword ,Ssex  ,Scity  ,Sdiqu  ,Sint  ,Sdecimal  ,Convert(varchar(10),Stime,120) as Stime  ,Convert(varchar(10),Stime_begin,120) as Stime_begin  ,Convert(varchar(10),Stime_end,120) as Stime_end ,Smoney ,Sshouji   ,'assets/images/gallery/thumb-6.jpg' as Stupian,CreateTime "; //检索字段(必须设置)
            string field_str = "";
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_field"].ToString().Trim() == "")
            {
                field_str = "*";
            }
            else
            {
                field_str = ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_field"].ToString();
            }
            ds_page.Tables[0].Rows[0]["serach_Row_str"] = " " + ds_DD.Tables["报表配置主表"].Rows[0]["FD_D_key"].ToString() + " as  jqgird_spid , " + field_str + " ";
            //ds_page.Tables[0].Rows[0]["search_tbname"] = " demouser  ";  //检索的表(必须设置)
            ds_page.Tables[0].Rows[0]["search_tbname"] = " " + ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_datatable"].ToString() + " ";
            //ds_page.Tables[0].Rows[0]["search_mainid"] = " SID ";  //所检索表的主键(必须设置)
            ds_page.Tables[0].Rows[0]["search_mainid"] = " " + ds_DD.Tables["报表配置主表"].Rows[0]["FD_D_key"].ToString() + " ";

            //默认条件
            string default_where = "   ";
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_where"].ToString().Trim() == "")
            {
                default_where = "   ";
            }
            else
            {
                if (ht_forUI.Contains("idforedit"))
                {
                    default_where = " and " + ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_where"].ToString().Replace("{idforedit}", "'" + ht_forUI["idforedit"].ToString() + "'") + " ";
                }
                else
                {
                    default_where = " and " + ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_where"].ToString() + " ";
                }
           
            }
            


            //处理发过来的表头搜索条件
            string extseearchstr = " ";
            Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
            if (ht_forUI.Contains("mysearchtop"))
            {
                jqsearch_sql js = new jqsearch_sql();
                js.getmysearchtop(ref dic_mysearchtop, ht_forUI["mysearchtop"].ToString());

                if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_open"].ToString() == "1")
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_type_"+i].ToString() == "输入框")
                        {
                            if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()))
                            {
                                extseearchstr = extseearchstr + " and "+ ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " like '%" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()] + "%'";
                            }
                        }
                        if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_type_" + i].ToString() == "时间段")
                        {
                            if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()+"1"))
                            {
                                extseearchstr = extseearchstr + " and "+ ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " >= '" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()+"1"] + " 00:00:00.000'";
                            }
                            if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + "2"))
                            {
                                extseearchstr = extseearchstr + " and "+ ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " <= '" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()+"2"] + " 23:59:59.999'";
                            }
                        }
                        if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_type_" + i].ToString() == "下拉框")
                        {
                            if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() ))
                            {
                                extseearchstr = extseearchstr + " and " + ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " = '" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()] + "'";
                            }
                             
                        }
                    }
                

                }
                //生成条件
                //if (dic_mysearchtop.ContainsKey("Sname"))
                //{
                //    extseearchstr = extseearchstr + " and Sname like '%" + dic_mysearchtop["Sname"] + "%'";
                //}
                //if (dic_mysearchtop.ContainsKey("time1"))
                //{
                //    extseearchstr = extseearchstr + " and CreateTime >= '" + dic_mysearchtop["time1"] + " 00:00:00.000'";
                //}
                //if (dic_mysearchtop.ContainsKey("time2"))
                //{
                //    extseearchstr = extseearchstr + " and CreateTime <= '" + dic_mysearchtop["time2"] + " 23:59:59.999'";
                //}
            }

            if (ht_forUI.Contains("filters"))
            {
                //处理发过来的自带搜索功能的复杂条件
                JavaScriptSerializer js = new JavaScriptSerializer();
                JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

                jqsearch_sql jqsql = new jqsearch_sql();
                string spsp_sql = jqsql.getsql(jg);
                if (spsp_sql.Trim() != "")
                {
                    extseearchstr = extseearchstr + " and " + spsp_sql;
                }


            }


            //给条件赋值
            ds_page.Tables[0].Rows[0]["search_str_where"] = " 1=1 " + default_where + extseearchstr;  //检索条件(必须设置)

            //处理发过来的排序参数
            if (!ht_forUI.Contains("R_OrderBy") || (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString().Trim() == ""))
            {
                string paixu = ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_order"].ToString().Trim();
                string[] paixu_arr = paixu.Split(' ');
                string pp = "";
                string search_paixuZD = paixu_arr[0];
                paixu_arr[0] = "";
                string search_paixu = string.Join(" ", paixu_arr);
                //设定默认排序
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + search_paixu + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + search_paixuZD + " ";  //用于排序的字段(必须设置) 
            }
            else
            {
                ds_page.Tables[0].Rows[0]["search_paixu"] = " " + ht_forUI["R_Sort"].ToString() + " ";  //排序方式(必须设置)
                ds_page.Tables[0].Rows[0]["search_paixuZD"] = " " + ht_forUI["R_OrderBy"].ToString() + " ";  //用于排序的字段(必须设置)   
            }




            //调用执行方法获取数据
            DataSet dsjg =  pd.GetPagerDB(ds_page);

            //开始调用二次处理
            try {
                Assembly serviceAsm = Assembly.GetExecutingAssembly();
                Type typeName = serviceAsm.GetType("NoReSetAR_" + ht_forUI["this_extforinfoFSID"].ToString());
                if (typeName != null)
                {
                    object instance = serviceAsm.CreateInstance("NoReSetAR_" + ht_forUI["this_extforinfoFSID"].ToString());
                    object rtnObj = typeName.GetMethod("NRS_AR").Invoke(instance, new object[] { dsjg });
                    return (DataSet)rtnObj;
                }
            }
            catch  { return dsjg; }
            return dsjg;


        }
        catch (Exception ex)
        {
            return null;
        }






    }



    /// <summary>
    /// 根据配置生成导出数据
    /// </summary>
    /// <param name="parameter_forUI">提交的数据</param>
    /// <returns></returns>
    [WebMethod(MessageName = "根据配置生成导出数据", Description = "根据配置生成导出数据")]
    public DataSet GetDataSetForExcel(DataTable parameter_forUI)
    {

        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //参数合法性各种验证，这里省略
        DataSet ds_DD = new DataSet();
        if (ht_forUI.Contains("this_extforinfoFSID"))
        {
            //获取数据库取数据的配置
            ds_DD = GetFormListInfo(ht_forUI["this_extforinfoFSID"].ToString());
        }
        else
        {
            return null;
        }


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了
 
        //文件名
        string filename = ds_DD.Tables["报表配置主表"].Rows[0]["FS_name"].ToString();

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        //生成sq语句中的字段
        string field_str = "";
        for (int i = 0; i < ds_DD.Tables["字段配置子表"].Rows.Count; i++)
        {
            DataRow dr = ds_DD.Tables["字段配置子表"].Rows[i];
            if(dr["DID_hide"].ToString() == "false" && dr["DID_formatter"].ToString() != "自定义")
            {
                field_str = field_str + " " + dr["DID_name"].ToString() + " as " + dr["DID_showname"].ToString() + ",";
            }
   
        }
        field_str = field_str.TrimEnd(',');


        //生成条件
        string default_where = "   ";
        if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_where"].ToString().Trim() == "")
        {
            default_where = "   ";
        }
        else
        {
            default_where = " and " + ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_where"].ToString() + " ";
        }

        //处理发过来的表头搜索条件
        string extseearchstr = " ";
        Dictionary<string, string> dic_mysearchtop = new Dictionary<string, string>();
        if (ht_forUI.Contains("mysearchtop"))
        {
            jqsearch_sql js = new jqsearch_sql();
            js.getmysearchtop(ref dic_mysearchtop, ht_forUI["mysearchtop"].ToString());

            if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_open"].ToString() == "1")
            {
                for (int i = 1; i <= 3; i++)
                {
                    if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_type_" + i].ToString() == "输入框")
                    {
                        if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()))
                        {
                            extseearchstr = extseearchstr + " and " + ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " like '%" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString()] + "%'";
                        }
                    }
                    if (ds_DD.Tables["报表配置主表"].Rows[0]["SRE_type_" + i].ToString() == "时间段")
                    {
                        if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + "1"))
                        {
                            extseearchstr = extseearchstr + " and " + ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " >= '" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + "1"] + " 00:00:00.000'";
                        }
                        if (dic_mysearchtop.ContainsKey(ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + "2"))
                        {
                            extseearchstr = extseearchstr + " and " + ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + " <= '" + dic_mysearchtop[ds_DD.Tables["报表配置主表"].Rows[0]["SRE_idname_" + i].ToString() + "2"] + " 23:59:59.999'";
                        }
                    }
                }


            }
 
        }

        if (ht_forUI.Contains("filters"))
        {
            //处理发过来的自带搜索功能的复杂条件
            JavaScriptSerializer js = new JavaScriptSerializer();
            JqGridSearchTo jg = js.Deserialize<JqGridSearchTo>(ht_forUI["filters"].ToString());

            jqsearch_sql jqsql = new jqsearch_sql();
            string spsp_sql = jqsql.getsql(jg);
            if (spsp_sql.Trim() != "")
            {
                extseearchstr = extseearchstr + " and " + spsp_sql;
            }


        }
        string where_str = default_where + extseearchstr;


        //生成语句中的排序
        string order_str = "";
        if (ht_forUI.Contains("R_OrderBy") && ht_forUI["R_OrderBy"].ToString() != "")
        {
            order_str = ht_forUI["R_OrderBy"].ToString() + " " + ht_forUI["R_Sort"].ToString();
        }
        else
        {
            order_str = ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_order"].ToString();
        }

        //生成最终语句

            string sql_ex = "select "+ field_str + " from "+ ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_datatable"].ToString() + " where 1=1 " + where_str + " order by " + order_str + "";

        return_ht = I_DBL.RunParam_SQL(sql_ex, "导出的数据", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataSet redb = (DataSet)return_ht["return_ds"];
            dsreturn.Tables.Add(redb.Tables["导出的数据"].Copy());
  

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "";
            dsreturn.Tables["返回值单条"].Rows[0]["附件信息1"] = filename;
        }
        else
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误：" + return_ht["return_errmsg"].ToString();
            dsreturn.Tables["返回值单条"].Rows[0]["附件信息1"] = "err.txt";
        }




 


        //开始调用二次处理
        try
        {
            Assembly serviceAsm = Assembly.GetExecutingAssembly();
            Type typeName = serviceAsm.GetType("NoReSetAR_" + ht_forUI["this_extforinfoFSID"].ToString());
            if (typeName != null)
            {
                object instance = serviceAsm.CreateInstance("NoReSetAR_" + ht_forUI["this_extforinfoFSID"].ToString());
                object rtnObj = typeName.GetMethod("NRS_AR").Invoke(instance, new object[] { dsreturn });
                return (DataSet)rtnObj;
            }
        }
        catch { return dsreturn; }

        return dsreturn;
    }

    #endregion


    #region 通用配置后台维护处理接口

    /// <summary>
    /// 快速表单字段批量生成
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "快速表单字段批量生成", Description = "快速表单字段批量生成")]
    public DataSet typx_ht_addnewbiaodan_default(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = CombGuid.GetMewIdFormSequence("FUP_FormsMainInfo");
        param.Add("@FID", guid);
        param.Add("@Fname", ht_forUI["Fname"].ToString());
        string[] ziduanliebiao = ht_forUI["ziduanliebiao"].ToString().Split(',');
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("INSERT INTO FUP_FormsMainInfo(FID, Fname) VALUES(@FID ,@Fname)");

        for (int i = 0; i < ziduanliebiao.Length; i++)
        {
            string guid_sub = CombGuid.GetMewIdFormSequence("FUP_FormsSubInfo");
            param.Add("@FSID_" + i, guid_sub);
            param.Add("@FS_FID_" + i, guid);
            param.Add("@FS_name_" + i, ziduanliebiao[i].Trim());
            param.Add("@FS_title_" + i, ziduanliebiao[i].Trim());
            param.Add("@FS_index_" + i, (i+1).ToString());

            alsql.Add("INSERT INTO FUP_FormsSubInfo(FSID,FS_FID, FS_name,FS_title,FS_index) VALUES(@FSID_"+ i + ", @FS_FID_" + i + ",@FS_name_" + i + ",@FS_title_" + i + ",@FS_index_" + i + ")");
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "使用默认值快速生成完成["+ guid + "]，需要人工根据业务调整！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }



    /// <summary>
    /// 快速表单弹窗批量生成
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "快速表单弹窗批量生成", Description = "快速表单弹窗批量生成")]
    public DataSet typx_ht_addnewtanchuang_default(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
 
        string[] tanchuangziduan = ht_forUI["tanchuangziduan"].ToString().Split(',');
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();

        for (int i = 0; i < tanchuangziduan.Length; i++)
        {
            string guid_sub = CombGuid.GetMewIdFormSequence("FUP_FormsSubDialog");
            param.Add("@DID_" + i, guid_sub);
            param.Add("@DID_FSID_" + i, ht_forUI["ziduanzhujian"]);
            param.Add("@DID_showname_" + i, tanchuangziduan[i].Trim());
            param.Add("@DID_name_" + i, tanchuangziduan[i].Trim());
            param.Add("@DID_px_" + i, (i + 1).ToString());

            alsql.Add("INSERT INTO FUP_FormsSubDialog(DID,DID_FSID, DID_showname,DID_name,DID_px) VALUES(@DID_" + i + ", @DID_FSID_" + i + ",@DID_showname_" + i + ",@DID_name_" + i + ",@DID_px_" + i + ")");
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "使用默认值快速生成完成弹窗字段完成，需要人工根据业务调整！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }


    /// <summary>
    /// 快速列表字段批量生成
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数，就一行数据表单原封不动提交过来，但多了几列当前用户登录状态识别信息，比如用户内部编号或邮箱等</param>
    /// <returns></returns>
    [WebMethod(MessageName = "快速列表字段批量生成", Description = "快速列表字段批量生成")]
    public DataSet typx_ht_addnewlist_default(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        //string guid = CombGuid.GetNewCombGuid("D"); 
        //以两位年+两位月+两位日+6位序列顺序号方式生成
        string guid = CombGuid.GetMewIdFormSequence("FUP_FormsList");
        param.Add("@FSID", guid);
        param.Add("@FS_name", ht_forUI["FS_name"].ToString());
        param.Add("@FS_D_field", ht_forUI["FS_D_field"].ToString());
        param.Add("@FS_D_datatable", ht_forUI["FS_D_datatable"].ToString());
        param.Add("@FS_D_where", ht_forUI["FS_D_where"].ToString());
        param.Add("@FS_D_order", ht_forUI["FS_D_order"].ToString());
        param.Add("@FD_D_key", ht_forUI["FD_D_key"].ToString());
 

        string[] ziduanliebiao = ht_forUI["ziduanliebiao"].ToString().Split(',');
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        alsql.Add("INSERT INTO FUP_FormsList(FSID, FS_name,FS_D_field,FS_D_datatable,FS_D_where,FS_D_order,FD_D_key) VALUES(@FSID, @FS_name,@FS_D_field,@FS_D_datatable,@FS_D_where,@FS_D_order,@FD_D_key)");

        for (int i = 0; i < ziduanliebiao.Length; i++)
        {
            string guid_sub = CombGuid.GetMewIdFormSequence("FUP_FormsSubInfo");
            param.Add("@DID_" + i, guid_sub);
            param.Add("@DID_FSID_" + i, guid);
            param.Add("@DID_px_" + i, (i + 1).ToString());
            param.Add("@DID_showname_" + i, ziduanliebiao[i].Trim());
            param.Add("@DID_name_" + i, ziduanliebiao[i].Trim());

            alsql.Add("INSERT INTO FUP_FormsList_field(DID,DID_FSID, DID_px,DID_showname,DID_name) VALUES(@DID_" + i + ", @DID_FSID_" + i + ",@DID_px_" + i + ",@DID_showname_" + i + ",@DID_name_" + i + ")");
        }



        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "使用默认值快速生成完成[" + guid + "]，需要人工根据业务调整！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //dsreturn.Tables.Add(parameter_forUI.Copy());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "系统故障，保存失败：" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }





    /// <summary>
    /// 获取框架配置表数据为修改
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取框架配置表数据为修改", Description = "获取框架配置表数据为修改")]
    public DataSet GetInfoFUP_foredit(DataTable parameter_forUI)
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

        Hashtable param = new Hashtable();
        param.Add("@idforedit", ht_forUI["idforedit"].ToString());

        Hashtable return_ht = new Hashtable();

        //偷懒，一个接口一起处理了。
        string sql = "";
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsList")
        {
            sql = "select top 1 * from FUP_FormsList where FSID = @idforedit";
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsList_field")
        {
            sql = "select top 1 * from FUP_FormsList_field where DID = @idforedit";
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsMainInfo")
        {
            sql = "select top 1 * from FUP_FormsMainInfo where FID = @idforedit";
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsSubInfo")
        {
            sql = "select top 1 * from FUP_FormsSubInfo where FSID = @idforedit";
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsSubDialog")
        {
            sql = "select top 1 * from FUP_FormsSubDialog where DID = @idforedit";
        }

        return_ht = I_DBL.RunParam_SQL(sql, "数据记录", param);

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




    /// <summary>
    /// 保存框架配置表数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "保存框架配置表数据", Description = "保存框架配置表数据")]
    public DataSet FUP_Edit_save(DataTable parameter_forUI)
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
        if (ht_forUI["zheshiyige_FID"].ToString().Trim() == "")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有明确的配置表主键！";
            return dsreturn;
        }
        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        //偷懒，一个接口一起处理了。
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsList")
        {
            string toulanziduan = "FSID, FS_ok, FS_type, FS_name, FS_getJK, FS_delJK, FS_del_show, FS_can_download,FS_add_show,FS_add_show_link,FS_zdy_op, FS_D_shrinkToFit, FS_D_setGroupHeaders, FS_D_field, FS_D_datatable, FS_D_where, FS_D_order, FD_D_key, FD_D_pagesize, SRE_open, SRE_showname_1, SRE_idname_1, SRE_type_1, SRE_showname_2,SRE_idname_2, SRE_type_2, SRE_showname_3, SRE_idname_3, SRE_type_3";
            string sqlupdate = "UPDATE FUP_FormsList SET  ";
            string[] tlzd_arr = toulanziduan.Split(',');
            param.Add("@"+ tlzd_arr[0].Trim(), ht_forUI["idforedit"].ToString());
            for (int i = 1; i < tlzd_arr.Length; i++)
            {
                param.Add("@"+ tlzd_arr[i].Trim(), ht_forUI[tlzd_arr[i].Trim()].ToString());
                sqlupdate = sqlupdate + ""+ tlzd_arr[i].Trim() + "=@"+ tlzd_arr[i].Trim() + " ,";
            }
            sqlupdate = sqlupdate.TrimEnd(',');
            sqlupdate = sqlupdate + "  where " + tlzd_arr[0].Trim() + "=@"+ tlzd_arr[0].Trim() + "  ";
            alsql.Add(sqlupdate);



            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-sys_editadd_FUP_FormsList_sub_029";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id+"_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete FUP_FormsList_field where  DID_FSID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "DID" + "_" + i,   CombGuid.GetMewIdFormSequence("FUP_FormsList_field"));
                }
                else
                {
                    param.Add("@sub_" + "DID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }


                param.Add("@sub_" + "DID_ok" + "_" + i, subdt.Rows[i]["是否启用"].ToString());
                param.Add("@sub_" + "DID_px" + "_" + i, subdt.Rows[i]["排序权重"].ToString());
                param.Add("@sub_" + "DID_hide" + "_" + i, subdt.Rows[i]["是否隐藏"].ToString());
                param.Add("@sub_" + "DID_showname" + "_" + i, subdt.Rows[i]["列显示名"].ToString());
                param.Add("@sub_" + "DID_name" + "_" + i, subdt.Rows[i]["字段名"].ToString());
                param.Add("@sub_" + "DID_width" + "_" + i, subdt.Rows[i]["默认宽度"].ToString());
                param.Add("@sub_" + "DID_sortable" + "_" + i, subdt.Rows[i]["点击排序"].ToString());
                param.Add("@sub_" + "DID_fixed" + "_" + i, subdt.Rows[i]["拖动列宽"].ToString());
                param.Add("@sub_" + "DID_frozen" + "_" + i, subdt.Rows[i]["冻结列"].ToString());
                param.Add("@sub_" + "DID_formatter" + "_" + i, subdt.Rows[i]["显示格式"].ToString());
                param.Add("@sub_" + "DID_formatter_CS" + "_" + i, subdt.Rows[i]["显示格式参数"].ToString());
 


                string INSERTsql = "INSERT INTO FUP_FormsList_field ( DID, DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,  DID_frozen, DID_formatter, DID_formatter_CS ) VALUES(@sub_" + "DID" + "_" + i + ", @sub_MainID, @sub_DID_ok_" + i + ", @sub_DID_px_" + i + ", @sub_DID_hide_" + i + ", @sub_DID_showname_" + i + ", @sub_DID_name_" + i + ", @sub_DID_width_" + i + ", @sub_DID_sortable_" + i + ", @sub_DID_fixed_" + i + ",  @sub_DID_frozen_" + i + ", @sub_DID_formatter_" + i + ", @sub_DID_formatter_CS_" + i + "  )";
                alsql.Add(INSERTsql);
            }



        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsList_field")
        {
            string toulanziduan = " DID, DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,    DID_frozen, DID_formatter, DID_formatter_CS";
            string sqlupdate = "UPDATE FUP_FormsList_field SET  ";
            string[] tlzd_arr = toulanziduan.Split(',');
            param.Add("@" + tlzd_arr[0].Trim(), ht_forUI["idforedit"].ToString());
            for (int i = 1; i < tlzd_arr.Length; i++)
            {
                param.Add("@" + tlzd_arr[i].Trim(), ht_forUI[tlzd_arr[i].Trim()].ToString());
                sqlupdate = sqlupdate + "" + tlzd_arr[i].Trim() + "=@" + tlzd_arr[i].Trim() + " ,";
            }
            sqlupdate = sqlupdate.TrimEnd(',');
            sqlupdate = sqlupdate + "  where " + tlzd_arr[0].Trim() + "=@" + tlzd_arr[0].Trim() + "  ";
            alsql.Add(sqlupdate);
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsMainInfo")
        {
            string toulanziduan = " FID, F_ok, Fname, Ftype, Frun_add, Frun_edit, Frun_showinfo_foredit";
            string sqlupdate = "UPDATE FUP_FormsMainInfo SET  ";
            string[] tlzd_arr = toulanziduan.Split(',');
            param.Add("@" + tlzd_arr[0].Trim(), ht_forUI["idforedit"].ToString());
            for (int i = 1; i < tlzd_arr.Length; i++)
            {
                param.Add("@" + tlzd_arr[i].Trim(), ht_forUI[tlzd_arr[i].Trim()].ToString());
                sqlupdate = sqlupdate + "" + tlzd_arr[i].Trim() + "=@" + tlzd_arr[i].Trim() + " ,";
            }
            sqlupdate = sqlupdate.TrimEnd(',');
            sqlupdate = sqlupdate + "  where " + tlzd_arr[0].Trim() + "=@" + tlzd_arr[0].Trim() + "  ";
            alsql.Add(sqlupdate);

            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-sys_editadd_FUP_FormsMainInfo_sub_007";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }

            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete FUP_FormsSubInfo where  FS_FID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "FSID" + "_" + i,   CombGuid.GetMewIdFormSequence("FUP_FormsSubInfo"));
                }
                else
                {
                    param.Add("@sub_" + "FSID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }
 
                param.Add("@sub_" + "FS_ok" + "_" + i, subdt.Rows[i]["是否启用"].ToString());
                param.Add("@sub_" + "FS_type" + "_" + i, subdt.Rows[i]["控件类型"].ToString());
                param.Add("@sub_" + "FS_name" + "_" + i, subdt.Rows[i]["控件名"].ToString());
                param.Add("@sub_" + "FS_title" + "_" + i, subdt.Rows[i]["控件标题"].ToString());
                param.Add("@sub_" + "FS_minlength" + "_" + i, subdt.Rows[i]["最小值"].ToString());
                param.Add("@sub_" + "FS_maxlength" + "_" + i, subdt.Rows[i]["最大值"].ToString());
                param.Add("@sub_" + "FS_defaultvalue" + "_" + i, subdt.Rows[i]["默认值"].ToString());
                param.Add("@sub_" + "FS_tip_n" + "_" + i, subdt.Rows[i]["内部提示"].ToString());
                param.Add("@sub_" + "FS_tip_w" + "_" + i, subdt.Rows[i]["悬浮提示"].ToString());
                param.Add("@sub_" + "FS_passnull" + "_" + i, subdt.Rows[i]["必填项"].ToString());
                param.Add("@sub_" + "FS_nulltip" + "_" + i, subdt.Rows[i]["必填项提示"].ToString());
                param.Add("@sub_" + "FS_index" + "_" + i, subdt.Rows[i]["排序权重"].ToString());
                param.Add("@sub_" + "FS_SPPZ_list_static" + "_" + i, subdt.Rows[i]["静态内容"].ToString());
                param.Add("@sub_" + "FS_SPPZ_mask" + "_" + i, subdt.Rows[i]["输入掩码"].ToString());
                param.Add("@sub_" + "FS_SPPZ_readonly" + "_" + i, subdt.Rows[i]["是否只读"].ToString());
                param.Add("@sub_" + "FS_D_haveD" + "_" + i, subdt.Rows[i]["启用弹窗"].ToString());
                param.Add("@sub_" + "FS_D_yinruzhi" + "_" + i, subdt.Rows[i]["弹窗引入值设置"].ToString());
                param.Add("@sub_" + "FS_D_shrinkToFit" + "_" + i, subdt.Rows[i]["弹窗自适应列宽"].ToString());
                param.Add("@sub_" + "FS_D_setGroupHeaders" + "_" + i, subdt.Rows[i]["弹窗复合表头"].ToString());
                param.Add("@sub_" + "FS_D_field" + "_" + i, subdt.Rows[i]["弹窗取值字段"].ToString());
                param.Add("@sub_" + "FS_D_datatable" + "_" + i, subdt.Rows[i]["弹窗取值表"].ToString());
                param.Add("@sub_" + "FS_D_where" + "_" + i, subdt.Rows[i]["弹窗取值条件"].ToString());
                param.Add("@sub_" + "FS_D_order" + "_" + i, subdt.Rows[i]["弹窗取值排序"].ToString());
                param.Add("@sub_" + "FD_D_key" + "_" + i, subdt.Rows[i]["弹窗取值主键"].ToString());
                param.Add("@sub_" + "FD_D_pagesize" + "_" + i, subdt.Rows[i]["弹窗分页数量"].ToString());
           
                string INSERTsql = "INSERT INTO FUP_FormsSubInfo ( FSID, FS_FID, FS_ok, FS_type, FS_name, FS_title, FS_minlength, FS_maxlength, FS_defaultvalue, FS_tip_n,  FS_tip_w, FS_passnull, FS_nulltip, FS_index, FS_SPPZ_list_static, FS_SPPZ_mask, FS_SPPZ_readonly, FS_D_haveD,   FS_D_yinruzhi, FS_D_shrinkToFit, FS_D_setGroupHeaders, FS_D_field, FS_D_datatable, FS_D_where, FS_D_order,   FD_D_key, FD_D_pagesize) VALUES(@sub_" + "FSID" + "_" + i + ", @sub_MainID, @sub_FS_ok_" + i + ", @sub_FS_type_" + i + ", @sub_FS_name_" + i + ", @sub_FS_title_" + i + ", @sub_FS_minlength_" + i + ", @sub_FS_maxlength_" + i + ", @sub_FS_defaultvalue_" + i + ", @sub_FS_tip_n_" + i + ",  @sub_FS_tip_w_" + i + ", @sub_FS_passnull_" + i + ", @sub_FS_nulltip_" + i + ", @sub_FS_index_" + i + ", @sub_FS_SPPZ_list_static_" + i + ", @sub_FS_SPPZ_mask_" + i + ", @sub_FS_SPPZ_readonly_" + i + ", @sub_FS_D_haveD_" + i + ", @sub_FS_D_yinruzhi_" + i + ", @sub_FS_D_shrinkToFit_" + i + ", @sub_FS_D_setGroupHeaders_" + i + ", @sub_FS_D_field_" + i + ", @sub_FS_D_datatable_" + i + ", @sub_FS_D_where_" + i + ", @sub_FS_D_order_" + i + ", @sub_FD_D_key_" + i + ", @sub_FD_D_pagesize_" + i + ")";
                alsql.Add(INSERTsql);
            }
        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsSubInfo")
        {
            string toulanziduan = " FSID, FS_FID, FS_ok, FS_type, FS_name, FS_title, FS_minlength, FS_maxlength,  FS_defaultvalue, FS_tip_n,    FS_tip_w, FS_passnull, FS_nulltip, FS_index, FS_SPPZ_list_static, FS_SPPZ_mask, FS_SPPZ_readonly, FS_D_haveD,   FS_D_yinruzhi, FS_D_shrinkToFit, FS_D_setGroupHeaders, FS_D_field, FS_D_datatable, FS_D_where, FS_D_order,   FD_D_key, FD_D_pagesize";
            string sqlupdate = "UPDATE FUP_FormsSubInfo SET  ";
            string[] tlzd_arr = toulanziduan.Split(',');
            param.Add("@" + tlzd_arr[0].Trim(), ht_forUI["idforedit"].ToString());
            for (int i = 1; i < tlzd_arr.Length; i++)
            {
                param.Add("@" + tlzd_arr[i].Trim(), ht_forUI[tlzd_arr[i].Trim()].ToString());
                sqlupdate = sqlupdate + "" + tlzd_arr[i].Trim() + "=@" + tlzd_arr[i].Trim() + " ,";
            }
            sqlupdate = sqlupdate.TrimEnd(',');
            sqlupdate = sqlupdate + "  where " + tlzd_arr[0].Trim() + "=@" + tlzd_arr[0].Trim() + "  ";
            alsql.Add(sqlupdate);




            //遍历子表，先删除，再插入，已有主键的不重新生成。
            string zibiao_gts_id = "grid-table-subtable-sys_editadd_FUP_FormsSubInfo_sub_027";
            DataTable subdt = jsontodatatable.ToDataTable(ht_forUI[zibiao_gts_id].ToString());
            //必须验证js脚本获取的数量和c#反序列化获取的数量一致才能继续。防止出错
            if (ht_forUI[zibiao_gts_id + "_fcjsq"].ToString() != subdt.Rows.Count.ToString())
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "子表数据量与获取量不相符，系统出现问题。";
                return dsreturn;
            }
            param.Add("@sub_" + "MainID", ht_forUI["idforedit"].ToString()); //隶属主表id
            alsql.Add("delete FUP_FormsSubDialog where  DID_FSID = @sub_" + "MainID");
            for (int i = 0; i < subdt.Rows.Count; i++)
            {
                if (subdt.Rows[i]["隐藏编号"].ToString().Trim() == "")
                {
                    param.Add("@sub_" + "DID" + "_" + i,   CombGuid.GetMewIdFormSequence("FUP_FormsSubDialog"));
                }
                else
                {
                    param.Add("@sub_" + "DID" + "_" + i, subdt.Rows[i]["隐藏编号"].ToString());
                }

     
                param.Add("@sub_" + "DID_ok" + "_" + i, subdt.Rows[i]["是否启用"].ToString());
                param.Add("@sub_" + "DID_px" + "_" + i, subdt.Rows[i]["排序权重"].ToString());
                param.Add("@sub_" + "DID_hide" + "_" + i, subdt.Rows[i]["是否隐藏"].ToString());
                param.Add("@sub_" + "DID_showname" + "_" + i, subdt.Rows[i]["列显示名"].ToString());
                param.Add("@sub_" + "DID_name" + "_" + i, subdt.Rows[i]["字段名"].ToString());
                param.Add("@sub_" + "DID_width" + "_" + i, subdt.Rows[i]["默认宽度"].ToString());
                param.Add("@sub_" + "DID_sortable" + "_" + i, subdt.Rows[i]["点击排序"].ToString());
                param.Add("@sub_" + "DID_fixed" + "_" + i, subdt.Rows[i]["拖动列宽"].ToString());
                param.Add("@sub_" + "DID_frozen" + "_" + i, subdt.Rows[i]["冻结列"].ToString());
                param.Add("@sub_" + "DID_formatter" + "_" + i, subdt.Rows[i]["显示格式"].ToString());
                param.Add("@sub_" + "DID_formatter_CS" + "_" + i, subdt.Rows[i]["显示格式参数"].ToString());
                param.Add("@sub_" + "DID_edit_editable" + "_" + i, subdt.Rows[i]["是否允许编辑"].ToString());
                param.Add("@sub_" + "DID_edit_required" + "_" + i, subdt.Rows[i]["编辑是否必填"].ToString());
                param.Add("@sub_" + "DID_edit_ftype" + "_" + i, subdt.Rows[i]["编辑控件类型"].ToString());
                param.Add("@sub_" + "DID_edit_spset" + "_" + i, subdt.Rows[i]["编辑特殊参数"].ToString());
   

                string INSERTsql = "INSERT INTO FUP_FormsSubDialog ( DID, DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,  DID_frozen, DID_formatter, DID_formatter_CS, DID_edit_editable, DID_edit_required, DID_edit_ftype,   DID_edit_spset) VALUES(@sub_" + "DID" + "_" + i + ", @sub_MainID, @sub_DID_ok_" + i + ", @sub_DID_px_" + i + ", @sub_DID_hide_" + i + ", @sub_DID_showname_" + i + ", @sub_DID_name_" + i + ", @sub_DID_width_" + i + ", @sub_DID_sortable_" + i + ", @sub_DID_fixed_" + i + ",  @sub_DID_frozen_" + i + ", @sub_DID_formatter_" + i + ", @sub_DID_formatter_CS_" + i + ", @sub_DID_edit_editable_" + i + ", @sub_DID_edit_required_" + i + ", @sub_DID_edit_ftype_" + i + ", @sub_DID_edit_spset_" + i + " )";
                alsql.Add(INSERTsql);
            }




        }
        if (ht_forUI["zheshiyige_FID"].ToString() == "sys_editadd_FUP_FormsSubDialog")
        {
            string toulanziduan = " DID, DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,    DID_frozen, DID_formatter, DID_formatter_CS, DID_edit_editable, DID_edit_required, DID_edit_ftype,    DID_edit_spset";
            string sqlupdate = "UPDATE FUP_FormsSubDialog SET  ";
            string[] tlzd_arr = toulanziduan.Split(',');
            param.Add("@" + tlzd_arr[0].Trim(), ht_forUI["idforedit"].ToString());
            for (int i = 1; i < tlzd_arr.Length; i++)
            {
                param.Add("@" + tlzd_arr[i].Trim(), ht_forUI[tlzd_arr[i].Trim()].ToString());
                sqlupdate = sqlupdate + "" + tlzd_arr[i].Trim() + "=@" + tlzd_arr[i].Trim() + " ,";
            }
            sqlupdate = sqlupdate.TrimEnd(',');
            sqlupdate = sqlupdate + "  where " + tlzd_arr[0].Trim() + "=@" + tlzd_arr[0].Trim() + "  ";
            alsql.Add(sqlupdate);
        }
      




        return_ht = I_DBL.RunParam_SQL(alsql, param);




        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "配置修改成功！";
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
    /// 框架免代理通用接口增
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "框架免代理通用接口增", Description = "框架免代理通用接口增")]
    public DataSet PUB_NO_RESET_ADD(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //开始调用处理
        Assembly serviceAsm = Assembly.GetExecutingAssembly();
        Type typeName = serviceAsm.GetType("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object instance = serviceAsm.CreateInstance("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object rtnObj = typeName.GetMethod("NRS_ADD").Invoke(instance, new object[] { parameter_forUI });
        //若发生错误，写入日志，省略
 
        return (DataSet)rtnObj;
    }



    /// <summary>
    /// 框架免代理通用接口改
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "框架免代理通用接口改", Description = "框架免代理通用接口改")]
    public DataSet PUB_NO_RESET_EDIT(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //开始调用处理
        Assembly serviceAsm = Assembly.GetExecutingAssembly();
        Type typeName = serviceAsm.GetType("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object instance = serviceAsm.CreateInstance("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object rtnObj = typeName.GetMethod("NRS_EDIT").Invoke(instance, new object[] { parameter_forUI });
        //若发生错误，写入日志，省略

        return (DataSet)rtnObj;
    }



    /// <summary>
    /// 框架免代理通用接口改获
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "框架免代理通用接口改获", Description = "框架免代理通用接口改获")]
    public DataSet PUB_NO_RESET_EDIT_INFO(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        //开始调用处理
        Assembly serviceAsm = Assembly.GetExecutingAssembly();
        Type typeName = serviceAsm.GetType("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object instance = serviceAsm.CreateInstance("NoReSet_" + ht_forUI["zheshiyige_FID"].ToString());
        object rtnObj = typeName.GetMethod("NRS_EDIT_INFO").Invoke(instance, new object[] { parameter_forUI });
        //若发生错误，写入日志，省略

        return (DataSet)rtnObj;
    }


    /// <summary>
    /// 框架免代理通用接口删
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "框架免代理通用接口删", Description = "框架免代理通用接口删")]
    public string PUB_NO_RESET_DEL(DataTable parameter_forUI)
    {

        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }
        object rtnObj = null;
        if (ht_forUI.Contains("zdyname"))
        {
            //开始调用处理,发现是自定按钮的处理请求
            Assembly serviceAsm = Assembly.GetExecutingAssembly();
            Type typeName = serviceAsm.GetType("NoReSetDEL_" + ht_forUI["zheshiyige_FID"].ToString());
            object instance = serviceAsm.CreateInstance("NoReSetDEL_" + ht_forUI["zheshiyige_FID"].ToString());
            rtnObj = typeName.GetMethod("NRS_ZDY_" + ht_forUI["zdyname"].ToString()).Invoke(instance, new object[] { parameter_forUI });
            //若发生错误，写入日志，省略
        }
        else
        {
            //开始调用处理
            Assembly serviceAsm = Assembly.GetExecutingAssembly();
            Type typeName = serviceAsm.GetType("NoReSetDEL_" + ht_forUI["zheshiyige_FID"].ToString());
            object instance = serviceAsm.CreateInstance("NoReSetDEL_" + ht_forUI["zheshiyige_FID"].ToString());
            rtnObj = typeName.GetMethod("NRS_DEL").Invoke(instance, new object[] { parameter_forUI });
            //若发生错误，写入日志，省略
        }
        if (rtnObj == null)
        {
            return "系统错误，处理失败";
        }


        return rtnObj.ToString();
    }


    #endregion

}
