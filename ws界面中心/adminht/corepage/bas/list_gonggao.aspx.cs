using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bas_list_gonggao : System.Web.UI.Page
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
        //列表识别号
        string FID = "160921000115";
        #region 必备的配置代码
        //获取表单配置
        dsFPZ = CallIPCPB.Get_FormsListDB(FID);
        htPP = FUPpublic.initPP_list(Request, dsFPZ);
        //给控件传值
        wuc_content_onlygrid._dsFPZ = dsFPZ;
        wuc_content_onlygrid._htPP = htPP;
        wuc_script_onlygrid._dsFPZ = dsFPZ;
        wuc_script_onlygrid._htPP = htPP;
        #endregion
    }
}