using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class puc_ditu_guiji : System.Web.UI.UserControl, ISetForWUC
{


    private Hashtable htPP;
    Hashtable ISetForWUC.htPP
    {
        set
        {
            htPP = value;
        }
    }

    private DataSet dsFPZ;
    DataSet ISetForWUC.dsFPZ
    {
        set
        {
            dsFPZ = value;
            //这里写链接数据库的等代码进行动态处理
            //Response.Write(dsFPZ.Tables[0].TableName);
        }
    }


}