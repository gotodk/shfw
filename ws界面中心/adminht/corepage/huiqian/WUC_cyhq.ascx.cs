using FMipcClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WUC_cyhq : System.Web.UI.UserControl, ISetForWUC
{
    public DataSet dsr = new DataSet();


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


            //获取会签单主表子表详细内容 Table1是会签意见子表  . 数据记录 是会签单主表
            //获取参数
            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);

            dt_request.Rows.Add(new string[] { "hqlx", "one", "临时添加" });
             

            object[] re_dsi = IPC.Call("获取会签数据", new object[] { dt_request });
            if (re_dsi[0].ToString() == "ok")
            {
                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                dsr = (DataSet)(re_dsi[1]);

                //处理一下头像
                dsr.Tables["Table1"].Columns.Add("utouxiang");
                for (int i = 0; i < dsr.Tables["Table1"].Rows.Count; i++)
                {
                    if (System.IO.File.Exists(Server.MapPath("/uploadfiles/faceup/" + dsr.Tables["Table1"].Rows[i]["YJqianhsuren"] + ".jpg")))
                    {
                        dsr.Tables["Table1"].Rows[i]["utouxiang"] = "/uploadfiles/faceup/" + dsr.Tables["Table1"].Rows[i]["YJqianhsuren"] + ".jpg";
                    }
                    else
                    {
                        dsr.Tables["Table1"].Rows[i]["utouxiang"] = "/mytutu/defaulttouxiang.jpg";
                    }
                }
                
            }


        }
    }
}