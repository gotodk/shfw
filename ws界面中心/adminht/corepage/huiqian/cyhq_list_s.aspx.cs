using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cyhq_list_s : System.Web.UI.Page
{
    public DataSet dsr = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取我参与的未结单的会签
        //获取参数
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);

        dt_request.Rows.Add(new string[] { "hqlx", "mylist", "临时添加" });
        object[] re_dsi = IPC.Call("获取会签数据", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dsr = (DataSet)(re_dsi[1]);
        }
    }
}