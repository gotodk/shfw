using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class demo_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        if (Request["idforedit"] == null || Request["idforedit"].Trim() == "")
        {
            return;
        }

        //调用执行方法获取数据
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
        object[] re_dsi = IPC.Call("获取数据demo", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet dsreturn = (DataSet)re_dsi[1];
            showinfor.DataSource = dsreturn.Tables["数据记录"].DefaultView;//数据绑定 
            showinfor.DataBind(); 
            //Response.Write(dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString());

        }
        else
        {
            Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

        }
    }
}