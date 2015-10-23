using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class alluseryuyue_list_nosystem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证权限
        AuthComm.chekcAuth_fromsession("8", UserSession.最终权值_后台菜单权限, true);
    }
}