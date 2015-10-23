using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class demo_liebiao : System.Web.UI.Page
{
    
    public string delbuttonshow = "false";
    protected void Page_Load(object sender, EventArgs e)
    {
        delbuttonshow = "true";
    }
}