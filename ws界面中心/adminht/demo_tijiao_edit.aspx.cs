using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class demo_tijiao_edit : System.Web.UI.Page
{
    //处理编辑ID
    protected string idforedit = "";
    protected void Page_Load(object sender, EventArgs e)
    {
 

        if (Request["idforedit"] != null)
        {
            idforedit = Request["idforedit"].ToString();
        }
        else
        {
            idforedit = "D-A4A301780119-FBA61B05-3A19-4F14-BCEF";
        }
    }
}