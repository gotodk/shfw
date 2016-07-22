using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class easjkceshi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //调用执行方法获取数据
     
        object[] re_dsi = IPC.Call("EAS正式登录调用", new object[] { "user", "wang", "eas", "a28", "l2", 2 });
        if (re_dsi[0].ToString() == "ok")
        {
            
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            object restr = re_dsi[1];
            //



            //编造一个xml
            DataTable dt = new DataTable("eas其他出库单写入");
            string xmlstr = File.ReadAllText(Server.MapPath("/xml/easceshi.txt").ToString());
            object[] re_dsi_22 = IPC.Call("eas其他出库单写入", new object[] { xmlstr });
            if (re_dsi_22[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                string[] restr_22 = (string[])(re_dsi_22[1]);
                for (int i = 0; i < restr_22.Length; i++)
                {
                    Response.Write(restr_22[i] + "<br/>====<br/>");
                }
                

            }
            else
            {
                Response.Write(re_dsi_22[1].ToString());//向客户端输出错误字符串

            }

        }
        else
        {
            Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

        }
    }
}