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


        //检查是否连续签到了，五分钟内不允许连续签到

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
            param.Add("@Kfx", "正常");
        }


        //分析当前是否在指定的预设范围内。
        double mLat1 = Convert.ToDouble( point_now[0].Trim()); // point1纬度

        double mLon1 = Convert.ToDouble(point_now[1].Trim()); // point1经度

        double mLat2 = 122.12833;// point2纬度

        double mLon2 = 37.518964;// point2经度

        double distance = GetShortDistance(mLon1, mLat1, mLon2, mLat2);
        
        if(distance < 400000)
        {
            param.Add("@Kfanweinei", "总部");
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
