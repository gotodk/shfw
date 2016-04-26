using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class st : System.Web.UI.Page
{
    public DataSet ds_DD = null;
    public string[] arr_tupian = null;
    protected void Page_Load(object sender, EventArgs e)
    {





        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
        ///st.aspx|&xxx=1|idforedit|_blank
        object[] re_dsi = IPC.Call("获取单据图片列表", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
        {
            ds_DD = (DataSet)(re_dsi[1]);
            try {
                arr_tupian = ds_DD.Tables["数据记录"].Rows[0]["tupian"].ToString().Split(',');
            }
            catch (Exception ex)
            {
                Response.Write("暂无图片");
                arr_tupian = new string[]{ };
            }
           
       


        }
        else
        {
            Response.Write("错误err，接口调用失败"); 
        }
    }

 


}