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

/// <summary>
/// 核心业务的相关处理接口
/// </summary>
[WebService(Namespace = "http://corebusiness.aftipc.com/", Description = "V1.00->xxx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class bsmain : System.Web.Services.WebService
{

    public bsmain()
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




    /// <summary>
    /// 获取扫码演示结果并处理
    /// </summary>
    /// <param name="parameter_forUI">参数</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "获取扫码演示结果并处理", Description = "获取扫码演示结果并处理")]
    public string getsaomajieguo_demo(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@SID", ht_forUI["tiaoma"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 SID,Sname from demouser where SID=@SID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "错误err，条码不存在！";
            }
            else
            {
                DataRow dr = redb.Rows[0];
                return "[{ \"条码\": \"" + dr["SID"].ToString() + "\", \"姓名\": \"" + dr["Sname"].ToString() + "\"  }]";
            }

        }
        else
        {
            return "错误err，系统异常！";
        }


    }





    /// <summary>
    /// 工厂日历管理处理
    /// </summary>
    /// <param name="parameter_forUI">参数</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "工厂日历管理处理", Description = "工厂日历管理处理")]
    public string gongchangrili_demo(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        if (ht_forUI["zhiling"].ToString() == "all")
        {


            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
            Hashtable return_ht = new Hashtable();
            Hashtable param = new Hashtable();
            param.Add("@start", ht_forUI["start"].ToString());
            param.Add("@end", ht_forUI["end"].ToString());
            return_ht = I_DBL.RunParam_SQL("select *  from ZZZ_calendar_pub where dayrq >= @start and dayrq <= @end  ", "数据记录", param);

            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                if (redb.Rows.Count < 1)
                {
                    return "错误err，日期不存在！";
                }
                else
                {
                    string restr = "[";
                    for (int i = 0; i < redb.Rows.Count; i++)
                    {
                        string dayrq = redb.Rows[i]["dayrq"].ToString();
                        string daytype = redb.Rows[i]["daytype"].ToString();
                        string classname = "label-danger";

                        string[] daytype_arr = daytype.Split(',');
                        for (int a = 0; a < daytype_arr.Length; a++)
                        {
                            if (daytype_arr[a] == "工作日")
                            { classname = "label-danger"; }
                            else if (daytype_arr[a] == "周末")
                            { classname = "label-success"; }
                            else
                            { classname = "label-yellow"; }
                            restr = restr + "{\"title\":\"" + daytype_arr[a] + "\",\"start\":\"" + dayrq + "\",\"end\":\"" + dayrq + "\",\"url\":null,\"allDay\":true,\"className\":\"" + classname + "\"},";
                        }



                    }
                    restr = restr.TrimEnd(',');
                    restr = restr + "]";
                    return restr;
                }

            }
            else
            {
                return "错误err，系统异常！";
            }
        }

        return "无效指令";

    }




    /// <summary>
    /// 获取考勤数据
    /// </summary>
    /// <param name="parameter_forUI">参数</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "获取考勤数据", Description = "获取考勤数据")]
    public string get_kaoqinshuju(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        if (ht_forUI["zhiling"].ToString() == "all")
        {


            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
            Hashtable return_ht = new Hashtable();
            Hashtable param = new Hashtable();
            param.Add("@start", ht_forUI["start"].ToString());
            param.Add("@end", ht_forUI["end"].ToString());
            return_ht = I_DBL.RunParam_SQL("select *,Kfx+'-'+Kfanweinei as showstr  from ZZZ_kaoqin where Ktime >= @start and Ktime <= @end  ", "数据记录", param);

            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                if (redb.Rows.Count < 1)
                {
                    return "错误err，日期不存在！";
                }
                else
                {
                    string restr = "[";
                    for (int i = 0; i < redb.Rows.Count; i++)
                    {
                        string dayrq = ((DateTime)(redb.Rows[i]["Ktime"])).ToLocalTime().ToString();
                        string daytype = redb.Rows[i]["showstr"].ToString();
                        string classname = "label-danger";

                        string[] daytype_arr = daytype.Split(',');
                        for (int a = 0; a < daytype_arr.Length; a++)
                        {

                            if (daytype_arr[a].IndexOf("正常") >= 0)
                            { classname = "label-success"; }
                            else if (daytype_arr[a].IndexOf("迟到") >= 0 || daytype_arr[a].IndexOf("早退") >= 0)
                            { classname = "label-danger"; }
                            else
                            { classname = "label-yellow"; }
                            restr = restr + "{\"title\":\"" + daytype_arr[a] + "\",\"start\":\"" + dayrq + "\",\"end\":\"" + dayrq + "\",\"url\":null,\"allDay\":false,\"className\":\"" + classname + "\"},";
                        }



                    }
                    restr = restr.TrimEnd(',');
                    restr = restr + "]";
                    return restr;
                }

            }
            else
            {
                return "错误err，系统异常！";
            }
        }

        return "无效指令";

    }


    /// <summary>
    /// 获取单据图片列表
    /// </summary>
    /// <param name="parameter_forUI">参数</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "获取单据图片列表", Description = "获取单据图片列表")]
    public DataSet getdanjutupian(DataTable parameter_forUI)
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



        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();

        if (ht_forUI["mod"].ToString().ToLower() == "wendang")
        {
            param.Add("@FID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,Ffujian as tupian from  ZZZ_WENDANG where FID=@FID", "数据记录", param);
        }

        if (ht_forUI["mod"].ToString().ToLower() == "huiqian")
        {
            param.Add("@QID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,Qfujian as tupian from  ZZZ_HQ where QID=@QID", "数据记录", param);
        }

        if (ht_forUI["mod"].ToString().ToLower() == "fwbg")
        {
            param.Add("@GID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,Gfujian as tupian from  ZZZ_FWBG where GID=@GID", "数据记录", param);
        }


        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有找到指定数据!";
                return dsreturn;
            }
            //redb.WriteXmlSchema("d://k3mima_s.xml");
            //redb.WriteXml("d://k3mima.xml");
            dsreturn.Tables.Add(redb);
            return dsreturn;
        }
        else
        {
            return dsreturn;
        }


    }


    /// <summary>
    /// 获取用户头像
    /// </summary>
    /// <param name="UAid">UI端的参数</param>
    /// <returns>只要返回值不是“可用”，就不能再注册了</returns>
    [WebMethod(MessageName = "获取用户头像", Description = "获取用户头像")]
    public string GetUserTouxiang(string UAid)
    {


        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        param.Add("@UAid", UAid);
        return_ht = I_DBL.RunParam_SQL("select top 1 myshowface from ZZZ_userinfo where UAid=@UAid     ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
            if (redb.Rows.Count > 0)
            {
                return redb.Rows[0]["myshowface"].ToString();
            }
            else
            {
                return "/mytutu/defaulttouxiang_err.jpg";
            }

        }
        else
        {
            return "/mytutu/defaulttouxiang_err.jpg";
        }

        return "/mytutu/defaulttouxiang_err.jpg";
    }




    /// <summary>
    /// 获取会签数据
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取会签数据", Description = "获取会签数据")]
    public DataSet GetList_HQ(DataTable parameter_forUI)
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
        param.Add("@uaid", ht_forUI["yhbsp_session_uer_UAid"].ToString());

        Hashtable return_ht = new Hashtable();

        //偷懒，一个接口一起处理了。
        string sql = "";
        if (ht_forUI["hqlx"].ToString() == "mylist")
        {
            //所有需要我参与的未结单的会签
            sql = "select *, case when Qcjr=@uaid then '由我发起' when Qjiedanren=@uaid then '待我结单' else '需我参与' end  as canyuqingkuang from View_ZZZ_HQ_ex where ( Qjiedanren=@uaid  or  Qcjr=@uaid  or  QID in (select YJ_QID from ZZZ_HQ_YJ where YJqianhsuren=@uaid ) ) and Qzhuangtai='未结单' order by Qaddtime desc";

             
        }
        if (ht_forUI["hqlx"].ToString() == "one")
        {
            param.Add("@QID", ht_forUI["idforedit"].ToString());

            sql = "select * from View_ZZZ_HQ_ex where QID = @QID; select * from View_ZZZ_HQ_YJ_ex where YJ_QID = @QID order by YJlysj asc;";

            
        }
            return_ht = I_DBL.RunParam_SQL(sql, "数据记录", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (ht_forUI["hqlx"].ToString() == "one" && redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有找到指定数据!";
                return dsreturn;
            }

            dsreturn.Tables.Add(redb);
            if (ht_forUI["hqlx"].ToString() == "one")
            {
                DataTable redb1 = ((DataSet)return_ht["return_ds"]).Tables["Table1"].Copy();
                dsreturn.Tables.Add(redb1);

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
    /// 获取客户全貌
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取客户全貌", Description = "获取客户全貌")]
    public DataSet GetList_quanmao(string sp,string str,string a, string b)
    {

        


        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
     
        Hashtable return_ht = new Hashtable();

        //偷懒，一个接口一起处理了。
        string sql = "";
        if (sp == "找客户")
        {
            param.Add("@key", str);

            sql = "SELECT   TOP (50) YYID_uuuu, uucjlx, YYID, YYname, YYaddtime, YYname + '('+uucjlx+')'as Lshowname FROM      View_ZZZ_KHDA_wcj_hb where YYname like '%'+@key+'%' or YYID_uuuu like '%'+@key+'%' ORDER BY YYaddtime desc";


        }
        if (sp == "找详情")
        {
            string lxrb = "";
            if (str.IndexOf('W') == 0)
            {
                param.Add("@QBleibie", "未成交");
                lxrb = "ZZZ_KHLXR_wcj";
            }
            if (str.IndexOf('C') == 0)
            {
                param.Add("@QBleibie", "成交");
                lxrb = "ZZZ_KHLXR";
            }
            param.Add("@YYID_uuuu", str);

            param.Add("@QB_YYID", str.Replace("W","").Replace("C", ""));

          


            sql = "SELECT   TOP (1) *,'' as lianxirenstr FROM      View_ZZZ_KHDA_wcj_hb where YYID_uuuu=@YYID_uuuu; SELECT  * FROM "+ lxrb + " where K_YYID =  @QB_YYID; select top 50 * from   View_ZZZ_KHDA_QB_list where QBleibie=@QBleibie and QB_YYID=@QB_YYID ";


        }
        return_ht = I_DBL.RunParam_SQL(sql, "数据记录", param);



        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

          

            dsreturn.Tables.Add(redb);
            if (sp == "找详情")
            {
                //二次处理联系人,并把情报表加入数据集
                DataTable redb1 = ((DataSet)return_ht["return_ds"]).Tables["Table1"].Copy();
                dsreturn.Tables.Add(redb1);
                DataTable redb2 = ((DataSet)return_ht["return_ds"]).Tables["Table2"].Copy();
                dsreturn.Tables.Add(redb2);

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







    /// <summary>
    /// 根据客户uaid，获取某些信息,获取某些个人资料
    /// </summary>
    /// <param name="parameter_forUI">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取某些个人资料", Description = "获取某些个人资料")]
    public DataSet GetInfoFromUAid(DataTable parameter_forUI)
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
        param.Add("@UAid", ht_forUI["idforedit"].ToString());




        Hashtable return_ht = new Hashtable();
        string linghangtishi = "";//0行提示
        if(ht_forUI["spspsp"].ToString() == "gerenkuwei")
        {
            linghangtishi = "没有找到对应个人库位信息!";
            return_ht = I_DBL.RunParam_SQL("select top 1 dpid,dpname,wmname from View_ZZZ_C_warehouse_ex where dpname = (select top 1 xingming from ZZZ_userinfo where UAid=@UAid)", "数据记录", param);
        }
        if (ht_forUI["spspsp"].ToString() == "guanliandanju")
        {
            linghangtishi = "没有找到关联单据信息!";
            param.Add("@BID", ht_forUI["idforedit"].ToString());
            return_ht = I_DBL.RunParam_SQL("select top 1 BID,B_YYID,YYname,Bsbtime from View_ZZZ_BXSQ_ex where BID=@BID", "数据记录", param);
        }
        if (ht_forUI["spspsp"].ToString() == "erp_kehudangan")
        {
            linghangtishi = "没有找到ERP客户档案编号!";
            param.Add("@YYID", ht_forUI["idforedit"].ToString().Trim());
            return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_erp_kehudangan where YYID=@YYID", "数据记录", param);
        }
        if (ht_forUI["spspsp"].ToString() == "erp_wuliaoxinxi")
        {
            linghangtishi = "没有找到ERP物料编号!";
            param.Add("@bianhao", ht_forUI["idforedit"].ToString().Trim());
            return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_erp_wuliaoxinxi where bianhao=@bianhao", "数据记录", param);
        }

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = linghangtishi;
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




    /// <summary>
    /// 获取微信自动登录参数
    /// </summary>
    /// <param name="username_wx">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取微信自动登录参数", Description = "获取微信自动登录参数")]
    public string GetInfoFromUsername_wx(string username_wx)
    {
 
        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        param.Add("@Uloginname", username_wx);




        Hashtable return_ht = new Hashtable();

        return_ht = I_DBL.RunParam_SQL("select top 1 UAid, Uloginname, Uloginpassword from auth_users_auths where Uloginname=@Uloginname", "数据记录", param);


        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "";
            }
            else
            {
                string zhanghao = redb.Rows[0]["Uloginname"].ToString();
                string mima = redb.Rows[0]["Uloginpassword"].ToString();
                string jm = zhanghao+"|"+ mima;
                return jm;
            }

        }
        else
        {
            return "";
        }





        return "";
    }


}
