using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_wuc_content : System.Web.UI.UserControl
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

    /// <summary>
    /// 动态输出自定义控件
    /// </summary>
    /// <param name="ucPath"></param>
    /// <returns></returns>
    public string RenderUserControlToString(string ucPath)
    {
        if (ucPath.Trim() == "")
        {
            return "";
        }
        UserControl uc = (UserControl)LoadControl(ucPath);
        ISetForWUC ISW = (ISetForWUC)uc;
        ISW.htPP = htPP;
        ISW.dsFPZ = dsFPZ;


        using (TextWriter tw = new StringWriter())
        using (HtmlTextWriter htw = new HtmlTextWriter(tw))
        {
            uc.RenderControl(htw);
            return tw.ToString();
        }
    }

 

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}