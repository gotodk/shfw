using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ceshi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ceshi = K3MM.GetMM("3030321");
        Response.Write(ceshi);
    }
}