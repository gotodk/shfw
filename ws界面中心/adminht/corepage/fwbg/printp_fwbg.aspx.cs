using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminht_corepage_fwbg_printp_fwbg : System.Web.UI.Page
{
    public DataSet dsr = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取参数
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);

        dt_request.Rows.Add(new string[] { "spspsp", "dayin_fwbg", "临时添加" });


        object[] re_dsi = IPC.Call("获取某些个人资料", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dsr = (DataSet)(re_dsi[1]);
            return;

        }
    }
}