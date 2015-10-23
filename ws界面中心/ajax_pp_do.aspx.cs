using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class ajax_pp_do : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //处理ajax请求
        string ajaxrun = "";
        if (Request["ajaxrun"] == null || Request["ajaxrun"].ToString().Trim() == "")
        {
            return;
        }
        if (Request["jkname"] == null || Request["jkname"].ToString().Trim() == "")
        {
            return;
        }
        string jkname = Request["jkname"].ToString();
        ajaxrun = Request["ajaxrun"].ToString();

        if (ajaxrun == "save")
        {/*
            string show = "<br>Form:<br>";
            for (int i = 0; i < Request.Form.Count; i++)
            {

                show = show + Request.Form.Keys[i].ToString() + " = " + Request.Form[i].ToString() + "<br>";
            }
            show = show + "<br>QueryString:<br>";
            for (int i = 0; i < Request.QueryString.Count; i++)
            {

                show = show + Request.QueryString.Keys[i].ToString() + " = " + Request.QueryString[i].ToString() + "<br>";
            }

            Response.Write(show);//向客户端输出字符串
          */


      
            
            
     
            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {
     
                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];
                Response.Write(dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString());

            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串
          
            }





        }
        if (ajaxrun == "del")
        {

 

            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {

                ;
            }
            else
            {
                Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

            }
        }
        if (ajaxrun == "info")
        {

 
            string idforedit = "";
            if (Request["idforedit"] != null && Request["idforedit"].ToString().Trim() != "")
            {
                idforedit = Request["idforedit"].ToString();
            }
            else
            {
                //没有id传进来返回空内容
                Response.Write("");
                return;
            }

 
            //调用执行方法获取数据
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
            object[] re_dsi = IPC.Call(jkname, new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                DataSet dsreturn = (DataSet)re_dsi[1];


                //转换xml

                TextWriter tw = new StringWriter();
                dsreturn.WriteXml(tw);
                string twstr = tw.ToString();
       
                StringWriter writer = new StringWriter();
                string xmlstr = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                    + twstr;
                Response.ContentType = "text/xml";
                Response.Charset = "UTF-8";
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xmlstr);
                doc.Save(Response.OutputStream);
                Response.End(); 

            }
            else
            {
                Response.Write("");
                return;

            }

            
     
        }
    }
}