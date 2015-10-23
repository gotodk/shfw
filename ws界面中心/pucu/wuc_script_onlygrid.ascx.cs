using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_wuc_script_onlygrid : System.Web.UI.UserControl
{

    public DataSet dsFPZ;
    public DataSet _dsFPZ
    {
        set { dsFPZ = value; }

    }


    public Hashtable htPP;
    public Hashtable _htPP
    {
        set { htPP = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}