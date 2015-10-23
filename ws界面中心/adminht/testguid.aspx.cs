using FMPublicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class testguid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
 
        Label1.Text = CombGuid.GetNewCombGuid("X").ToString();
 
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Label2.Text = CombGuid.GetDateFromComb(Label1.Text);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Label3.Text = StringOP.encMe("000000", "mima");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Label4.Text = "__"+string.Join(",", AuthComm.GetEnumFormUnumber("0").Values.ToArray())+"__";
    }
}