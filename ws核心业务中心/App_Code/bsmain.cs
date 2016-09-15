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


        if (ht_forUI["zhiling"].ToString() == "ziji")
        {


            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
            Hashtable return_ht = new Hashtable();
            Hashtable param = new Hashtable();

            param.Add("@start", ht_forUI["start"].ToString());
            param.Add("@end", ht_forUI["end"].ToString());
            param.Add("@K_UAID", ht_forUI["qdren"].ToString());
            return_ht = I_DBL.RunParam_SQL("select *,Kfx+'-'+Kfanweinei as showstr  from ZZZ_kaoqin where Ktime >= @start and Ktime <= @end and K_UAID=@K_UAID ", "数据记录", param);

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




        if (ht_forUI["zhiling"].ToString() == "kaoqinfugaidian")
        {
            //var data = {"data":[[74.438,39.006,1],[74.932,38.382,1]],"total":5365,"rt_loc_cnt":47764510,"errorno":0,"NearestTime":"2016-08-01 15:20:00","userTime":"2016-08-01 15:20:00"}
            string revarstr_0 = "var data = {\"data\":[";
            string revarstr_x = "[0,0,1]";
            string revarstr_1 = "],\"total\":总量,\"rt_loc_cnt\":47764510,\"errorno\":0,\"NearestTime\":\"2016-08-01 15:20:00\",\"userTime\":\"2016-08-01 15:20:00\"}";

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
            Hashtable return_ht = new Hashtable();
            Hashtable param = new Hashtable();

      
            return_ht = I_DBL.RunParam_SQL("select Kzuobiao as Kzuobiao, count(Kzuobiao) as cishu  from ZZZ_kaoqin group by Kzuobiao  ", "数据记录", param);

            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                if (redb.Rows.Count < 1)
                {
                    return "错误err，无签到！";
                }
                else
                {
                     
                    for (int i = 0; i < redb.Rows.Count; i++)
                    {

                        string Kzuobiao = redb.Rows[i]["Kzuobiao"].ToString();
                        string cishu = redb.Rows[i]["cishu"].ToString();

                        if (Kzuobiao.IndexOf(",") > 0)
                        {
                            revarstr_x = revarstr_x + ",[" + Kzuobiao + ","+ cishu + "]";
                        }





                    }
                   
                    return revarstr_0 + revarstr_x + revarstr_1.Replace("总量", redb.Rows.Count.ToString());
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


        if (ht_forUI["mod"].ToString().ToLower() == "wfsb")
        {
            param.Add("@SID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,Sfujian as tupian from  View_ZZZ_WFSB_list where SID=@SID", "数据记录", param);
        }
        if (ht_forUI["mod"].ToString().ToLower() == "sblxbase")
        {
            param.Add("@SBID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,SBfujian as tupian from  ZZZ_SBLXBASE where SBID=@SBID", "数据记录", param);
        }
        if (ht_forUI["mod"].ToString().ToLower() == "wflj")
        {
            param.Add("@LID", ht_forUI["idforedit"].ToString());

            return_ht = I_DBL.RunParam_SQL("select top 1 *,Lfujian as tupian from  ZZZ_WFLJ where LID=@LID", "数据记录", param);
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
    /// 获取用户基础资料
    /// </summary>
    /// <param name="UAid">UI端的参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取用户基础资料", Description = "获取用户基础资料")]
    public DataTable GetUserjichuziliao(string UAid)
    {


        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();

        Hashtable return_ht = new Hashtable();

        param.Add("@UAid", UAid);
        return_ht = I_DBL.RunParam_SQL("select top 1 * from ZZZ_userinfo where UAid=@UAid     ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
            if (redb.Rows.Count > 0)
            {
                return redb;
            }
            else
            {
                return null;
            }

        }
        else
        {
            return null;
        }

        return null;
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
            sql = "select *, case when Qcjr=@uaid then '由我发起' when Qjiedanren=@uaid then '待我结单' when (select count(YJID) from ZZZ_HQ_YJ where YJzhuangtai='待签' and YJ_QID=View_ZZZ_HQ_ex.QID and YJqianhsuren=@uaid) > 0 then '需我参与' else '我已参与' end  as canyuqingkuang from View_ZZZ_HQ_ex where ( Qjiedanren=@uaid  or  Qcjr=@uaid  or  QID in (select YJ_QID from ZZZ_HQ_YJ where YJqianhsuren=@uaid ) ) and Qzhuangtai='未结单' order by Qaddtime desc";

             
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
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "意外错误，获取失败：" + return_ht["return_errmsg"].ToString();
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
            string nowloginuser = a;
            string ziji = " and ( ( charindex(','+'" + nowloginuser + "'+',',(select top 1 ','+YSTR+',' from ZZZ_ZFCMJ where YID='tskhgl')) > 0 ) or (uucjlx='未成交') or ( YYID in (select YYID from ZZZ_userinfo_glkh where UAid='" + nowloginuser + "' and shixiaoriqi >= getdate()  UNION  select YYID from ZZZ_KHDA where YYfuwufuzeren='" + nowloginuser + "') ) )";
            sql = "SELECT   TOP (100) YYID_uuuu, uucjlx, YYID, YYname, YYaddtime, YYname + '('+uucjlx+')'as Lshowname FROM      View_ZZZ_KHDA_wcj_hb where (YYname like '%'+@key+'%' or YYID_uuuu like '%'+@key+'%') "+ ziji + " ORDER BY YYaddtime desc";


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
            return_ht = I_DBL.RunParam_SQL("select top 1 BID,B_YYID,YYname,Bfwlx,Bsbtime,Bzhuangtai,(select top 1 GID from ZZZ_workplan where G_BID=BID order by Gaddtime desc) as G_jihua_GID from View_ZZZ_BXSQ_ex where BID=@BID", "数据记录", param);
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


        if (ht_forUI["spspsp"].ToString() == "dayin_fwbg")
        {
            linghangtishi = "没有找到服务报告编号!";
            param.Add("@GID", ht_forUI["idforedit"].ToString().Trim());
            return_ht = I_DBL.RunParam_SQL("select top 1 * from View_ZZZ_FWBG_ex where GID=@GID; select *, CONVERT(varchar(100), sbbaoxiujiezhi, 23) as baoxiu_sss from ZZZ_FWBG_shebei where sb_GID=@GID; select *, CONVERT(varchar(100), ljbaoxiujiezhi, 23) as baoxiu_sss from ZZZ_FWBG_lingjian  where lj_GID=@GID ", "数据记录", param);
        }

        if (ht_forUI["spspsp"].ToString() == "gerenziliao")
        {
            linghangtishi = "没有找到个人资料!";
            return_ht = I_DBL.RunParam_SQL("select top 1 * from view_ZZZ_userinfo_ex where UAid=@UAid ", "数据记录", param);
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

            if (ht_forUI["spspsp"].ToString() == "dayin_fwbg")
            {
                DataTable redb1 = ((DataSet)return_ht["return_ds"]).Tables[1].Copy();
                redb1.TableName = "设备子表";
                DataTable redb2 = ((DataSet)return_ht["return_ds"]).Tables[2].Copy();
                redb2.TableName = "零件子表";
                dsreturn.Tables.Add(redb1);
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





    /// <summary>
    /// 获取仪表盘数据
    /// </summary>
    /// <param name="hqbz">获取标记</param>
    /// <param name="UAid">用户标记</param>
    /// <param name="spsp">其他参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取仪表盘数据", Description = "获取仪表盘数据")]
    public string Get_yibiaopan(string hqbz,string UAid,string spsp)
    {

  
        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");


        //获取重要数据的饼图
        if (hqbz == "zysj_pie")
        {
            Hashtable param = new Hashtable();
            param.Add("@xxxx", "");

            Hashtable return_ht = new Hashtable();

            return_ht = I_DBL.RunParam_SQL("select top 5 设备种类, 种类数量 from View_yibiaopan_bing order by 种类数量 desc", "数据记录", param);


            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                if (redb.Rows.Count < 1)
                {
                    //暂无数据
                    return "{ label: '暂无数据', data: 20, shuliang: '0', color: '#DA5430' },{ label: '暂无数据', data: 20, shuliang: '0', color: '#68BC31' },{ label: '暂无数据', data: 20, shuliang: '0', color: '#2091CF' },{ label: '暂无数据', data: 20, shuliang: '0', color: '#AF4E96' }, { label: '暂无数据', data: 20, shuliang: '0', color: '#FEE074' }";
                }
                else
                {
                    string shuju = "";
                    string[] yanse = new string[] { "#DA5430", "#68BC31", "#2091CF", "#AF4E96", "#FEE074" };
                    for (int i = 0; i < redb.Rows.Count; i++)
                    {
                        shuju = shuju + "{ label: '"+ redb.Rows[i]["设备种类"].ToString() + "', data: " + redb.Rows[i]["种类数量"].ToString() + ", shuliang: '" + redb.Rows[i]["种类数量"].ToString() + "', color: '"+ yanse[i] + "' },";
                    }
                    shuju = shuju.TrimEnd(',');
                    return shuju;
                }

            }
            else
            {
                return "";
            }
        }



        //获取重要数据的一些数字
        if (hqbz == "zysj_yixieshuzi")
        {
            Hashtable param = new Hashtable();
            param.Add("@xxxx", "");

            Hashtable return_ht = new Hashtable();

            return_ht = I_DBL.RunParam_SQL("select 返修设备种类_累积, 返修设备种类_本月, 返修设备种类_上月, 服务报告金额_本年, 服务报告金额_上月,   今日实际出勤人数, 今日应出勤人数 from View_yibiaopan_shu_1", "数据记录", param);


            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                int shijieren = Convert.ToInt32(redb.Rows[0]["今日实际出勤人数"].ToString());
                int yingren = Convert.ToInt32(redb.Rows[0]["今日应出勤人数"].ToString());
                double bili = 0;
                if (yingren == 0)
                {
                    bili = 0;
                }
                else
                {
                    bili = shijieren / yingren;
                }
                string qdl = bili.ToString("f1");
                string qdly = shijieren + "/" + yingren;
                string restr =  "返修设备种类_累积:"+ redb.Rows[0]["返修设备种类_累积"].ToString() + ",返修设备种类_本月:" + redb.Rows[0]["返修设备种类_本月"].ToString() + ",返修设备种类_上月:" + redb.Rows[0]["返修设备种类_上月"].ToString() + ",服务报告金额_本年:" + redb.Rows[0]["服务报告金额_本年"].ToString() + ",服务报告金额_上月:" + redb.Rows[0]["服务报告金额_上月"].ToString() + ",签到率:"+ qdl + ",签到率来源:"+ qdly + "";
                return restr;
            }
            else
            {
                return "";
            }
        }



        //获取重要列表
        if (hqbz == "zysj_liebiao_001")
        {
            Hashtable param = new Hashtable();
            param.Add("@xxxx", "");

            Hashtable return_ht = new Hashtable();

            return_ht = I_DBL.RunParam_SQL("select 大区, 发货总金额, 服务报告数量 from View_yibiaopan_liebiao ORDER BY 大区", "数据记录", param);


            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                string shuju = "";
                for (int i = 0; i < redb.Rows.Count; i++)
                {
                    shuju = shuju + "" + redb.Rows[i]["发货总金额"].ToString() + ",";
                    shuju = shuju + "" + redb.Rows[i]["服务报告数量"].ToString() + ",";
                }
                shuju = shuju.TrimEnd(',');
                return shuju;
            }
            else
            {
                return "";
            }
        }


        //获取重要列表
        if (hqbz == "zysj_liebiao_002")
        {
            Hashtable param = new Hashtable();
            param.Add("@xxxx", "");

            Hashtable return_ht = new Hashtable();

            return_ht = I_DBL.RunParam_SQL("select 大区, 发货总金额, 服务报告数量 from View_yibiaopan_liebiao_002 ORDER BY 大区", "数据记录", param);


            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                string shuju = "";
                for (int i = 0; i < redb.Rows.Count; i++)
                {
                    shuju = shuju + "" + redb.Rows[i]["发货总金额"].ToString() + ",";
                    shuju = shuju + "" + redb.Rows[i]["服务报告数量"].ToString() + ",";
                }
                shuju = shuju.TrimEnd(',');
                return shuju;
            }
            else
            {
                return "";
            }
        }



        //获取重要列表
        if (hqbz == "baoxiuchao24")
        {
            Hashtable param = new Hashtable();
            param.Add("@xxxx", "");

            Hashtable return_ht = new Hashtable();

            return_ht = I_DBL.RunParam_SQL("select count(*) from ZZZ_BXSQ  where Bzhuangtai='待处理' and datediff( hour, (select top 1 DDtime from ZZZ_BXSQ_DD where DD_BID=BID order by DDtime desc), getdate() ) > 24", "数据记录", param);


            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

                string shuju = "";
                shuju = redb.Rows[0][0].ToString();
                return shuju;
            }
            else
            {
                return "";
            }
        }


        return "";
    }





    /// <summary>
    /// 获取我的工作台数据
    /// </summary>
    /// <param name="UAid">用户标记</param>
    /// <param name="spsp">其他参数</param>
    /// <returns></returns>
    [WebMethod(MessageName = "获取我的工作台数据", Description = "获取我的工作台数据")]
    public DataTable Get_gongzuotai(string UAid, string spsp)
    {


        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");


        Hashtable param = new Hashtable();
        param.Add("@UAid", UAid);

        Hashtable return_ht = new Hashtable();

        string sql = "";
        sql = sql + " if not exists (select  MID from ZZZ_MYGZT where MID like 'gzt_'+@UAid+'_%' ) begin ";

        //sql = sql + " delete ZZZ_MYGZT where MID  like 'gzt_'+@UAid+'_%' ";
        for (int i = 0; i < 8; i++)
        {
            sql = sql + " insert into ZZZ_MYGZT (MID,Mbiaoti,Mdizhi,Mbeiwanglu) values('gzt_'+@UAid+'_" + i.ToString() + "','','','') ";
        }
        sql = sql + " end   ";
     
        sql = sql + " select  MID,(Case When Mbiaoti ='' then '暂无快捷链接' Else Mbiaoti End ) as Mbiaoti , (Case When Mdizhi ='' then 'javascript:void(0)' Else Mdizhi End ) as Mdizhi, (Case When Mbeiwanglu ='' then '备忘：暂无备忘信息' Else '备忘：'+Mbeiwanglu End ) as Mbeiwanglu from ZZZ_MYGZT where MID  like 'gzt_'+@UAid+'_%' order by MID asc ";
        return_ht = I_DBL.RunParam_SQL(sql, "数据记录", param);


        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            return redb;

        }
        else
        {
            return null;
        }

    }





    #region  导入
    /// <summary>
    /// 通过电子表格导入数据
    /// </summary>
    /// <param name="DRDT">将电子表格转化后的DataTable数据</param>
    /// <param name="UAID">操作人</param>
    /// <param name="spsp">标记是导入的什么</param>
    /// <returns></returns>
    [WebMethod(MessageName = "通过电子表格导入数据", Description = "通过电子表格导入数据")]
    public DataSet pubdaoru(DataTable DRDT, string UAID,string spsp)
    {

        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略
        if (DRDT == null || DRDT.Rows.Count < 1)
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "没有要导入的数据！";
            return dsreturn;
        }


        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

        Hashtable param = new Hashtable();
        ArrayList alsql = new ArrayList();

        if(spsp== "设备物料表导入")
        {
        
            for (int i = 0; i < DRDT.Rows.Count; i++)
            {
                //以可排序guid方式生成
               // SBID, SBmingcheng, SBxinghao, SBdanwei, SBchengbenjia, SBbaoxiuqixian, SBbaoyangzhouqi,  SBchanpinshouming, SBxiaoshoujiage, SBshengchanchang, SBerpbianma, SBzhuangtai, SBbeizhu
                param.Add("@SBID" + "_" + i, DRDT.Rows[i]["设备编号"].ToString());
                param.Add("@SBmingcheng" + "_" + i, DRDT.Rows[i]["设备名称"].ToString());
                param.Add("@SBxinghao" + "_" + i, DRDT.Rows[i]["设备型号"].ToString());
                param.Add("@SBdanwei" + "_" + i, DRDT.Rows[i]["单位"].ToString());
                param.Add("@SBchengbenjia" + "_" + i, DRDT.Rows[i]["成本价"].ToString());
                param.Add("@SBbaoxiuqixian" + "_" + i, DRDT.Rows[i]["保修期限"].ToString());
                param.Add("@SBbaoyangzhouqi" + "_" + i, DRDT.Rows[i]["保养周期"].ToString());
                param.Add("@SBchanpinshouming" + "_" + i, DRDT.Rows[i]["产品说明"].ToString());
                param.Add("@SBxiaoshoujiage" + "_" + i, DRDT.Rows[i]["销售价格"].ToString());
                param.Add("@SBshengchanchang" + "_" + i, DRDT.Rows[i]["生产厂家"].ToString());
                param.Add("@SBerpbianma" + "_" + i, DRDT.Rows[i]["ERP编号"].ToString());
                param.Add("@SBzhuangtai" + "_" + i, DRDT.Rows[i]["设备状态"].ToString());
                param.Add("@SBbeizhu" + "_" + i, DRDT.Rows[i]["备注"].ToString());


                alsql.Add("INSERT INTO ZZZ_SBLXBASE(SBID, SBmingcheng, SBxinghao, SBdanwei, SBchengbenjia, SBbaoxiuqixian, SBbaoyangzhouqi,  SBchanpinshouming, SBxiaoshoujiage, SBshengchanchang, SBerpbianma, SBzhuangtai, SBbeizhu) VALUES(@SBID" + "_" + i + ", @SBmingcheng" + "_" + i + ", @SBxinghao" + "_" + i + ", @SBdanwei" + "_" + i + ", @SBchengbenjia" + "_" + i + ", @SBbaoxiuqixian" + "_" + i + ", @SBbaoyangzhouqi" + "_" + i + ", @SBchanpinshouming" + "_" + i + ", @SBxiaoshoujiage" + "_" + i + ",   @SBshengchanchang" + "_" + i + ", @SBerpbianma" + "_" + i + ", @SBzhuangtai" + "_" + i + ", @SBbeizhu" + "_" + i + " )");
            }
        }
        if (spsp == "配件物料表导入")
        {

            for (int i = 0; i < DRDT.Rows.Count; i++)
            {
                //以可排序guid方式生成
                //配件编码,配件名称,规格型号,单位,拼音,erp编号,成本价,最低价,零售价,保修期限,状态
                //LID, Lmingcheng, Lguige, Ldanwei, Lpinyin, Lerpbianhao, Lchengbenjia, Lzuidijia, Lshoujia, Lbaoxiuqi,Lzhuangtai
                param.Add("@LID" + "_" + i, DRDT.Rows[i]["配件编码"].ToString());
                param.Add("@Lmingcheng" + "_" + i, DRDT.Rows[i]["配件名称"].ToString());
                param.Add("@Lguige" + "_" + i, DRDT.Rows[i]["规格型号"].ToString());
                param.Add("@Ldanwei" + "_" + i, DRDT.Rows[i]["单位"].ToString());
                param.Add("@Lpinyin" + "_" + i, DRDT.Rows[i]["拼音"].ToString());
                param.Add("@Lerpbianhao" + "_" + i, DRDT.Rows[i]["erp编号"].ToString());
                param.Add("@Lchengbenjia" + "_" + i, DRDT.Rows[i]["成本价"].ToString());
                param.Add("@Lzuidijia" + "_" + i, DRDT.Rows[i]["最低价"].ToString());
                param.Add("@Lshoujia" + "_" + i, DRDT.Rows[i]["零售价"].ToString());
                param.Add("@Lbaoxiuqi" + "_" + i, DRDT.Rows[i]["保修期限"].ToString());
                param.Add("@Lzhuangtai" + "_" + i, DRDT.Rows[i]["状态"].ToString());
          


                alsql.Add("INSERT INTO ZZZ_WFLJ(LID, Lmingcheng, Lguige, Ldanwei, Lpinyin, Lerpbianhao, Lchengbenjia, Lzuidijia, Lshoujia, Lbaoxiuqi,Lzhuangtai) VALUES(@LID" + "_" + i + ", @Lmingcheng" + "_" + i + ", @Lguige" + "_" + i + ", @Ldanwei" + "_" + i + ", @Lpinyin" + "_" + i + ", @Lerpbianhao" + "_" + i + ", @Lchengbenjia" + "_" + i + ", @Lzuidijia" + "_" + i + ", @Lshoujia" + "_" + i + ",   @Lbaoxiuqi" + "_" + i + ", @Lzhuangtai" + "_" + i + " )");
            }
        }



        Hashtable return_ht = new Hashtable();
        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {

            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "成功导入" + DRDT.Rows.Count.ToString() + "条数据！";
        }
        else
        {
            //其实要记录日志，而不是输出，这里只是演示
            //StringOP.WriteLog(Server.MapPath("/") + "//", "导入失败：" + return_ht["return_errmsg"].ToString());
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "发生意外，导入失败！" + return_ht["return_errmsg"].ToString();
        }





        return dsreturn;
    }
    #endregion

}
