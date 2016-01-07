using System;
using FMipcClass;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class adminht_linshi_demo_tiaomasoamiao_ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
        object[] re_dsi = IPC.Call("获取扫码演示结果并处理", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
        {
            Response.Write(re_dsi[1].ToString());
        }
        else
        { Response.Write("错误err，接口调用失败"); }
    }
}