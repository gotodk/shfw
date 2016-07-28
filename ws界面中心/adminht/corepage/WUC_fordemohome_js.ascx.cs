using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adminht_corepage_WUC_fordemohome_js : System.Web.UI.UserControl
{
    public bool qx_zysj = false; //仪表盘是否显示重要数据
    protected void Page_Load(object sender, EventArgs e)
    {
        //检查仪表盘显示权限
        qx_zysj = AuthComm.chekcAuth_fromsession("2", UserSession.最终权值_全局独立权限, false);
    }
}