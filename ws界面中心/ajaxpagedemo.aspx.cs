using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMDBHelperClass;
using System.Collections;
using System.Data;
using FMipcClass;
using System.Threading;

public partial class ajaxpagedemo : System.Web.UI.Page
{

    private string geterrmod(string msg)
    {
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?><错误><错误提示>" + msg + "</错误提示></错误>";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "text/xml";
        Response.Charset = "UTF-8";

        if (Request["R_PageNumber"] == null || Request["R_PageNumber"].ToString().Trim() == "")
        {
            Response.Write(geterrmod("获取数据失败!"));
            return;
        }
        if (Request["R_PageSize"] == null || Request["R_PageSize"].ToString().Trim() == "")
        {
            Response.Write(geterrmod("获取数据失败!"));
            return;
        }
        if (Request["jkname"] == null || Request["jkname"].ToString().Trim() == "")
        {
            Response.Write(geterrmod("获取数据失败!"));
            return;
        }
        string jkname = Request["jkname"].ToString();
        jkname = HttpUtility.UrlDecode(jkname, System.Text.Encoding.UTF8);
        string currentpage = Request["R_PageNumber"].ToString();
        string PageSize = Request["R_PageSize"].ToString();
        try
        {
            DataSet dsjieguo = new DataSet();
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_ds = IPC.Call(jkname, new object[] { dt_request });
            if (re_ds[0].ToString() == "ok")
            {
                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                dsjieguo = (DataSet)re_ds[1];
            }
            else
            {
                Response.Write(geterrmod(re_ds[1].ToString()));
                return;
            }

            //转换xml
            System.IO.StringWriter writer = new System.IO.StringWriter();
            if (!dsjieguo.Tables.Contains("主要数据"))
            {
                Response.Write(geterrmod("没有主要数据!"));
                return;
            }
            dsjieguo.Tables["主要数据"].WriteXml(writer);


            //为图表增加新的解析
            string fujaitubiao_str = "<chartYHB>";
            if (dsjieguo.Tables.Contains("饼图数据"))
            {
                System.IO.StringWriter writer_bing = new System.IO.StringWriter();
                dsjieguo.Tables["饼图数据"].WriteXml(writer_bing);
                fujaitubiao_str = fujaitubiao_str + writer_bing;
            }
    
            for (int t = 0; t < dsjieguo.Tables.Count; t++)
            {
                if (dsjieguo.Tables[t].TableName.IndexOf("曲线图数据") >= 0)
                {
                    System.IO.StringWriter writer_quxian = new System.IO.StringWriter();
                    dsjieguo.Tables[t].WriteXml(writer_quxian);
                    fujaitubiao_str = fujaitubiao_str + writer_quxian;
                }
             
            }
            fujaitubiao_str = fujaitubiao_str + "</chartYHB>";

            string xmlstr = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                 + "<invoices>"+ fujaitubiao_str + "<request>true</request><currentpage>" + currentpage + "</currentpage><totalpages>" + dsjieguo.Tables["附加数据"].Rows[0]["分页数"].ToString() + "</totalpages><totalrecords>" + dsjieguo.Tables["附加数据"].Rows[0]["记录数"].ToString() + "</totalrecords>"
                + writer.ToString() + "</invoices>";

          


            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlstr);
            doc.Save(Response.OutputStream);
            
        }
        catch (Exception ex)
        {
            Response.Write(geterrmod("获取数据失败，执行错误!"));
            return;
        }
        
    }

 

}