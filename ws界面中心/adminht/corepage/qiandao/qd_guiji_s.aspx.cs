﻿using FMipcClass;
using FMPublicClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class qiandao_qd_guiji_s : System.Web.UI.Page
{

    public string cscscs = "";

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
        //表单识别号
        string FID = "160805000002";
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

        if (this.Page.Request["cscscs"] != null)
        {
            cscscs = StringOP.uncMe(Request["cscscs"].ToString(), "mima");
        }
        else
        {
            cscscs = "a=1";
        }
    }
}