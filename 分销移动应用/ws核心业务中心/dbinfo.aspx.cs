using FMDBHelperClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dbinfo : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (!IsPostBack)
        {
            bd("");
 

        }

    }

    protected void bd(string db)
    {
        string sql = "select * from ViewAllTableLook ORDER BY 表名, 列顺序";
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain(db);
        Hashtable return_ht = new Hashtable();
        return_ht = I_DBL.RunProc(sql, "数据记录");
        DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
        GridView1.DataSource = redb.DefaultView;
        GridView1.DataBind();

        string sql2 = "select * from ViewAll_view_fun_exec ORDER BY 类型, 名称";
        I_Dblink I_DBL2 = (new DBFactory()).DbLinkSqlMain(db);
        Hashtable return_ht2 = new Hashtable();
        return_ht2 = I_DBL2.RunProc(sql2, "数据记录");
        DataTable redb2 = ((DataSet)return_ht2["return_ds"]).Tables["数据记录"].Copy();
        GridView2.DataSource = redb2;
        GridView2.DataBind();
    }

 
 

    /// <summary>
    /// 将网格数据导出到Excel
    /// </summary>
    /// <param name="ctrl">网格名称(如GridView1)</param>
    /// <param name="FileType">要导出的文件类型(Excel:application/ms-excel)</param>
    /// <param name="FileName">要保存的文件名</param>
    public static void GridViewToExcel(Control ctrl, string FileType, string FileName)
    {
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;//注意编码
        HttpContext.Current.Response.AppendHeader("Content-Disposition",
            "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        HttpContext.Current.Response.ContentType = FileType;//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 
        ctrl.Page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctrl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        GridViewToExcel(GridView1, "application/ms-excel", "dbinfo_table.xls");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        GridViewToExcel(GridView2, "application/ms-excel", "dbinfo_other.xls");
    }
}