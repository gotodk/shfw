using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class wcj_edit_khda : System.Web.UI.Page
{

    #region 必备的公共变量
    /// <summary>
    /// 表单配置
    /// </summary>
    public DataSet dsFPZ = null;
    /// <summary>
    /// 其他辅助配置
    /// </summary>
    public Hashtable htPP = new Hashtable();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {


        //特殊判定，进行自动转向
        if (Request["fff"] != null && Request["fff"].ToString() == "1")
        {

            string i_tt = Request["idforedit"].ToString();
            if (i_tt.IndexOf("C") >= 0)
            {
                Response.Redirect( Request.Url.PathAndQuery.Replace("wcj", "bas").Replace("idforedit=C", "idforedit=") );
                return;
            }
            if (i_tt.IndexOf("W") >= 0)
            {
                Response.Redirect(Request.Url.PathAndQuery.Replace("idforedit=W", "idforedit=").Replace("showinfo=2", "showinfo=1"));
                return;
            }
        }



        //表单识别号
        string FID = "160519000046";
        #region 必备的配置代码
        //获取表单配置
        dsFPZ = CallIPCPB.Get_FormInfoDB(FID);
        htPP = FUPpublic.initPP(Request, dsFPZ);
        //给控件传值
        wuc_content._dsFPZ = dsFPZ;
        wuc_content._htPP = htPP;
        wuc_script._dsFPZ = dsFPZ;
        wuc_script._htPP = htPP;
        #endregion
 

    }
}