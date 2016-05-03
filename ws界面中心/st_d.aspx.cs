using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_st_d : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fn = Request["fn"].ToString();
        Response.ContentType = "application/x-msdownload";
        string filename = "attachment; filename=" + fn;
        Response.AddHeader("Content-Disposition", filename);
        string filepath = fn;
        Response.TransmitFile(Server.MapPath(filepath));
    }
}