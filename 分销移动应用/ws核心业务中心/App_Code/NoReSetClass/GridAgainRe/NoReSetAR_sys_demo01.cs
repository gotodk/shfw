using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetAR_sys_demo01
{

    /// <summary>
    /// 二次处理数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public DataSet NRS_AR(DataSet oldDS)
    {

        DataSet NewDS = null;
        NewDS = oldDS;

        //通常是循环这个数据集，补充未取到的数据。以提高效率。

        #region  图表数据集示例

        //制造一个模拟的报表数据，补充到数据集里面(必须是这个列名，一行一个饼块)
        //DataTable dtc1 = new DataTable("饼图数据");
        //dtc1.Columns.Add("项目名");
        //dtc1.Columns.Add("百分数");
        //dtc1.Columns.Add("颜色");
        //dtc1.Rows.Add(new string[] { "生产", "38.70", "#68BC31" });
        //dtc1.Rows.Add(new string[] { "销售", "24.50", "#2091CF" });
        //dtc1.Rows.Add(new string[] { "技术", "8.20", "#AF4E96" });
        //dtc1.Rows.Add(new string[] { "后勤", "18.6", "#DA5430" });
        //dtc1.Rows.Add(new string[] { "财务", "10", "#FEE074" });
        //NewDS.Tables.Add(dtc1.Copy());


        //========================================================


        //DataTable dtc2 = new DataTable("曲线图数据");
        //dtc2.Columns.Add("项目名");
        //dtc2.Columns.Add("颜色");

        //dtc2.Rows.Add(new string[] { "生产", "#68BC31" });
        //dtc2.Rows.Add(new string[] { "销售", "#2091CF" });
        //dtc2.Rows.Add(new string[] { "技术", "#FEE074" });

        //DataTable dtc2_sub1 = new DataTable("曲线图数据-" + "生产");
        //dtc2_sub1.Columns.Add("X轴");
        //dtc2_sub1.Columns.Add("Y轴");
        //dtc2_sub1.Rows.Add(new string[] { "2014", "1" });
        //dtc2_sub1.Rows.Add(new string[] { "2015", "4" });
        //dtc2_sub1.Rows.Add(new string[] { "2016", "10" });
        //dtc2_sub1.Rows.Add(new string[] { "2017", "20.5" });
        //dtc2_sub1.Rows.Add(new string[] { "2018", "18.5" });
        //dtc2_sub1.Rows.Add(new string[] { "2019", "6.5" });

        //DataTable dtc2_sub2 = new DataTable("曲线图数据-" + "销售");
        //dtc2_sub2.Columns.Add("X轴");
        //dtc2_sub2.Columns.Add("Y轴");
        //dtc2_sub2.Rows.Add(new string[] { "2013", "1" });
        //dtc2_sub2.Rows.Add(new string[] { "2014", "6" });
        //dtc2_sub2.Rows.Add(new string[] { "2015", "12" });
        //dtc2_sub2.Rows.Add(new string[] { "2016", "17" });

        //DataTable dtc2_sub3 = new DataTable("曲线图数据-" + "技术");
        //dtc2_sub3.Columns.Add("X轴");
        //dtc2_sub3.Columns.Add("Y轴");
        //dtc2_sub3.Rows.Add(new string[] { "2013", "12" });
        //dtc2_sub3.Rows.Add(new string[] { "2014", "17" });
        //dtc2_sub3.Rows.Add(new string[] { "2015", "12.5" });
        //dtc2_sub3.Rows.Add(new string[] { "2016", "17.5" });

        //NewDS.Tables.Add(dtc2.Copy());
        //NewDS.Tables.Add(dtc2_sub1.Copy());
        //NewDS.Tables.Add(dtc2_sub2.Copy());
        //NewDS.Tables.Add(dtc2_sub3.Copy());

        #endregion




        return NewDS;
    }


}
 
