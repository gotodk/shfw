using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class auth_userlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证权限
        AuthComm.chekcAuth_fromsession("4", UserSession.最终权值_后台菜单权限, true);
    }
}