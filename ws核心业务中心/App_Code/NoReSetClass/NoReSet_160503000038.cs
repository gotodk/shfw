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

public class NoReSet_160503000038
{
    static double DEF_PI = 3.14159265359; // PI

    static double DEF_2PI = 6.28318530712; // 2*PI

    static double DEF_PI180 = 0.01745329252; // PI/180.0

    static double DEF_R = 6370693.5; // radius of earth

    public double GetShortDistance(double lon1, double lat1, double lon2, double lat2)

    {

        double ew1, ns1, ew2, ns2;

        double dx, dy, dew;

        double distance;

        // 角度转换为弧度

        ew1 = lon1 * DEF_PI180;

        ns1 = lat1 * DEF_PI180;

        ew2 = lon2 * DEF_PI180;

        ns2 = lat2 * DEF_PI180;

        // 经度差

        dew = ew1 - ew2;

        // 若跨东经和西经180 度，进行调整

        if (dew > DEF_PI)

            dew = DEF_2PI - dew;

        else if (dew < -DEF_PI)

            dew = DEF_2PI + dew;

        dx = DEF_R * Math.Cos(ns1) * dew; // 东西方向长度(在纬度圈上的投影长度)

        dy = DEF_R * (ns1 - ns2); // 南北方向长度(在经度圈上的投影长度)

        // 勾股定理求斜边长

        distance = Math.Sqrt(dx * dx + dy * dy);

        return distance;

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
    /// 获取签到参数
    /// </summary>
    /// <param name="parameter_forUI">获取签到参数</param>
    /// <returns></returns>
    public DataSet GetCS(string uaid)
    {
 

        //初始化返回值
        DataSet dsreturn = initReturnDataSet().Clone();
        dsreturn.Tables["返回值单条"].Rows.Add(new string[] { "err", "初始化" });

        //参数合法性各种验证，这里省略

        //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@CSID", "10000");

        return_ht = I_DBL.RunParam_SQL("select top 1 * from ZZZ_kaoqin_cs where CSID=@CSID;select top 1 * from  ZZZ_kaoqin where  K_UAID='" + uaid + "' order by Ktime desc;select top 1 * from ZZZ_kaoqin_dd order by DDID;", "签到参数", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["Table1"].Copy();
 

            dsreturn.Tables.Add(redb);
            dsreturn.Tables.Add(((DataSet)return_ht["return_ds"]).Tables["Table2"].Copy());
            dsreturn.Tables.Add(((DataSet)return_ht["return_ds"]).Tables["签到参数"].Copy());


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


        //获取参数 “Table1”是最后一次签到记录，“Table2”是地点标示，“签到参数”是参数
        DataSet dscs = GetCS(ht_forUI["yhbsp_session_uer_UAid"].ToString());
        //检查是否连续签到了，N分钟内不允许连续签到
        if (dscs.Tables["Table1"].Rows.Count > 0 )
        {
            DateTime lastQD = (DateTime)(dscs.Tables["Table1"].Rows[0]["Ktime"]);
            DateTime nowQD = DateTime.Now;
            TimeSpan ND = nowQD - lastQD;
            double mm = ND.TotalMinutes;
            if (mm < Convert.ToDouble(dscs.Tables["签到参数"].Rows[0]["CSjiange"]))
            {
                dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
                dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，"+ dscs.Tables["签到参数"].Rows[0]["CSjiange"].ToString() + "分钟内不允许重复签到，距离上次签到"+  Math.Round(mm, 2).ToString() + "分钟！";
                return dsreturn;
            }
        }

        //检查不在签到时间段内不允许签到
        string CSshang1 = dscs.Tables["签到参数"].Rows[0]["CSshang1"].ToString().Replace(":",".");
        string CSshang2 = dscs.Tables["签到参数"].Rows[0]["CSshang2"].ToString().Replace(":", ".");
        string CSxia1 = dscs.Tables["签到参数"].Rows[0]["CSxia1"].ToString().Replace(":", ".");
        string CSxia2 = dscs.Tables["签到参数"].Rows[0]["CSxia2"].ToString().Replace(":", ".");
        string dqsj = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();
        bool canqd = false;
        if (Convert.ToDouble(dqsj) >= Convert.ToDouble(CSshang1) && Convert.ToDouble(dqsj) <= Convert.ToDouble(CSshang2))
        {
            canqd = true;
        }
        if (Convert.ToDouble(dqsj) >= Convert.ToDouble(CSxia1) && Convert.ToDouble(dqsj) <= Convert.ToDouble(CSxia2))
        {
            canqd = true;
        }
        if (!canqd && ht_forUI["leixing"].ToString() == "上下班")
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败，特定时间段以外不允许签到，上班"+ CSshang1.Replace(".",":") + "到" + CSshang2.Replace(".", ":") + "，下班" + CSxia1.Replace(".", ":") + "到" + CSxia2.Replace(".", ":") + "。";
            return dsreturn;
        }

        //坐标检查
        string zuobiao = ht_forUI["zuobiao"].ToString();
        if (zuobiao.Trim() == "" || zuobiao.IndexOf(",") < 0)
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "err";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存失败坐标无效！";
            return dsreturn;
        }
        string[] point_now = zuobiao.Split(',');

        //开始真正的处理，根据业务逻辑操作数据库
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        ArrayList alsql = new ArrayList();
        Hashtable param = new Hashtable();
        //以可排序guid方式生成
        string guid = CombGuid.GetNewCombGuid("K");
        param.Add("@KID", guid);
        param.Add("@K_UAID", ht_forUI["yhbsp_session_uer_UAid"].ToString());
        param.Add("@Kleixing", ht_forUI["leixing"].ToString());
        param.Add("@Kzuobiao", zuobiao);
        param.Add("@Kdizhi", ht_forUI["dizhi"].ToString());
        param.Add("@Ksheng", ht_forUI["shengfen"].ToString());
        param.Add("@Kshi", ht_forUI["chengshi"].ToString());
        param.Add("@Kqu", ht_forUI["quxian"].ToString());
        param.Add("@Kbeizhu", ht_forUI["beizhu"].ToString());
        //分析当前时间是迟到还是早退，还是被忽略的考勤数据
        if (ht_forUI["leixing"].ToString() != "上下班")
        {
            param.Add("@Kfx", "正常");
        }
        else
        {
            //判定迟到还是早退
            string CSshang1P = dscs.Tables["签到参数"].Rows[0]["CSshang1P"].ToString().Replace(":", ".");
            string CSshang2P = dscs.Tables["签到参数"].Rows[0]["CSshang2P"].ToString().Replace(":", ".");
            string CSxia1P = dscs.Tables["签到参数"].Rows[0]["CSxia1P"].ToString().Replace(":", ".");
            string CSxia2P = dscs.Tables["签到参数"].Rows[0]["CSxia2P"].ToString().Replace(":", ".");
            string dqsjP = DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString();
            
            if (Convert.ToDouble(dqsjP) >= Convert.ToDouble(CSshang1P) && Convert.ToDouble(dqsjP) <= Convert.ToDouble(CSshang2P))
            {
                //迟到
                param.Add("@Kfx", "迟到");
            }
            if (Convert.ToDouble(dqsjP) >= Convert.ToDouble(CSxia1P) && Convert.ToDouble(dqsjP) <= Convert.ToDouble(CSxia2P))
            {
                //早退
                if (!param.Contains("@Kfx"))
                {
                    param.Add("@Kfx", "早退");
                }
                
            }
           
        }


        //分析当前是否在指定的预设范围内。
    

        double mLon1 = Convert.ToDouble(point_now[0].Trim()); // point1经度
        double mLat1 = Convert.ToDouble(point_now[1].Trim()); // point1纬度


        for (int i = 0; i < dscs.Tables["Table2"].Rows.Count; i++)
        {
          

            double mLon2 = Convert.ToDouble(dscs.Tables["Table2"].Rows[i]["DDzuobiao"].ToString().Split(',')[0]);// point2经度
            double mLat2 = Convert.ToDouble(dscs.Tables["Table2"].Rows[i]["DDzuobiao"].ToString().Split(',')[1]);// point2纬度
            double distance = GetShortDistance(mLon1, mLat1, mLon2, mLat2);
            //单位是米
            double DDfanwei = Convert.ToDouble(dscs.Tables["Table2"].Rows[i]["DDfanwei"].ToString());
            if (distance < DDfanwei)
            {
                param.Add("@Kfanweinei", dscs.Tables["Table2"].Rows[i]["DDbiaojiming"].ToString());
                break;
            }
        }
        if (!param.Contains("@Kfanweinei"))
        {
            //找不到范围标记，记录下城市名
            param.Add("@Kfanweinei", ht_forUI["chengshi"].ToString().Replace("市",""));
        }
    


        alsql.Add("INSERT INTO ZZZ_kaoqin(KID, K_UAID, Kleixing, Kzuobiao, Kdizhi, Ksheng, Kshi, Kqu, Ktime, Kfx,Kfanweinei,Kbeizhu) VALUES(@KID, @K_UAID, @Kleixing, @Kzuobiao, @Kdizhi, @Ksheng, @Kshi, @Kqu, getdate(), @Kfx,@Kfanweinei,@Kbeizhu )");
 

        return_ht = I_DBL.RunParam_SQL(alsql, param);

        if ((bool)(return_ht["return_float"]))
        {
            dsreturn.Tables["返回值单条"].Rows[0]["执行结果"] = "ok";
            dsreturn.Tables["返回值单条"].Rows[0]["提示文本"] = "保存成功！";
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
        return null;
    }

    /// <summary>
    /// 编辑数据前获取数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_EDIT_INFO(DataTable parameter_forUI)
    {
        return null;
    }


}
