using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sceas_c_ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //根据表单，生成接口需要的数据集

        DataSet dsmain = new DataSet();
        DataTable dbHead = new DataTable();
        dbHead.TableName = "dbHead";
        dbHead.Columns.Add("操作类型", typeof(string));
        dbHead.Columns.Add("本地单号", typeof(string));
        dbHead.Columns.Add("事务类型", typeof(string));
        dbHead.Columns.Add("成本中心", typeof(string));
        dbHead.Columns.Add("部门", typeof(string));
        dbHead.Columns.Add("制单人", typeof(string));
        dbHead.Columns.Add("销售方式", typeof(string));
        dbHead.Columns.Add("销售组织", typeof(string));
        dbHead.Columns.Add("币别", typeof(string));
        dbHead.Columns.Add("汇率", typeof(string));
        dbHead.Columns.Add("订货客户", typeof(string));
        dbHead.Columns.Add("付款类型", typeof(string));
        dbHead.Columns.Add("库存组织", typeof(string));
        dbHead.Columns.Add("控制单元", typeof(string));
        dbHead.Columns.Add("业务类型", typeof(string));
        dsmain.Tables.Add(dbHead);
        DataTable dbEntry = new DataTable();
        dbEntry.TableName = "dbEntry";
        dbEntry.Columns.Add("物料编码", typeof(string));
        dbEntry.Columns.Add("物料类别", typeof(string));
        dbEntry.Columns.Add("发货数量", typeof(string));
        dbEntry.Columns.Add("批号", typeof(string));
        dbEntry.Columns.Add("单价", typeof(string));
        dbEntry.Columns.Add("子表主键", typeof(string));
        dsmain.Tables.Add(dbEntry);

        dsmain.Tables["dbHead"].Rows.Add(new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
        dsmain.Tables["dbHead"].Rows[0]["操作类型"] = "1";
        dsmain.Tables["dbHead"].Rows[0]["本地单号"] = Request["dhdh"].ToString();
 

 
        object[] re_dsi = IPC.Call("EAS出库单小伟生成", new object[] { dsmain });
        if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
        {
            DataSet dsre = (DataSet)(re_dsi[1]);
            //            3、	返回数据DataSet包含三个DataTable(0, 1, 2)
            //4、	Datatable(0):一行一列，存储返回提示文本内容。
            //5、	Datatable(1)：单据头。
            //6、	Datatable(2)：单据分录，可能会变更分录“批号”。
            if (dsre.Tables[0].Rows[0][0].ToString().IndexOf("查询成功") < 0)
            {
                Response.Write("错误err，接口调用成功，但接口返回：" + dsre.Tables[0].Rows[0][0].ToString());

            }
            else
            {
                //处理返回的子表数据,
                string restr = "a";
                for (int p = 0; p < dsre.Tables[2].Rows.Count; p++)
                {
                    restr = restr +  dsre.Tables[2].Rows[p]["批号"].ToString() + "|";
                }
                Response.Write(restr);
            }
        }
        else
        {

            Response.Write("错误err，接口调用失败。" + re_dsi[1].ToString() );
        

        }

 
    }
}