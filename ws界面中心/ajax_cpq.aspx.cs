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

public partial class ajax_cpq : System.Web.UI.Page
{
    private string geterrmod(string msg)
    {
        return "<?xml version=\"1.0\" encoding=\"utf-8\" ?><错误><错误提示>" + msg + "</错误提示></错误>";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "text/xml";
        Response.Charset = "UTF-8";

 
        try
        {
            DataSet dsjieguo = new DataSet();
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_ds = IPC.Call("获取省市区数据", new object[] { dt_request });
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
            if (!dsjieguo.Tables.Contains("省市区数据"))
            {
                Response.Write(geterrmod("没有数据!"));
                return;
            }
            dsjieguo.Tables["省市区数据"].WriteXml(writer);
            string xmlstr = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"  + writer.ToString();


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