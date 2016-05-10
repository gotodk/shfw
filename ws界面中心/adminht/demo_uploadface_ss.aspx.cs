using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class demo_uploadface_ss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        upimageuc3.Cnowimg = "/uploadfiles/faceup/" + UserSession.唯一键 + ".jpg?id=99";
        upimageuc3.Cupaction = "/ajaxcropperupload.aspx?uaid="+ UserSession.唯一键 + "&touxiang=1";
        //upimageuc3.Cupaction = "/ajaxcropperupload.aspx";
    }
}