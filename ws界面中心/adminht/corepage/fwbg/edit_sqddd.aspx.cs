using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fwbh_edit_sqddd : System.Web.UI.Page
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
        //检查权限
        AuthComm.chekcAuth_fromsession("16", UserSession.最终权值_全局独立权限, true);

        //调度单是编辑的特殊用法，发现showinfo，跳转到我的报修申请，避免错误 
        if (Request["showinfo"] != null)
        {
            Response.Redirect("/adminht/corepage/fwbg/list_bxsq_my.aspx");
            return;
        }

        //表单识别号
        string FID = "160610000055";
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