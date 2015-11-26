using FMipcClass;
using FMPublicClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_gqzidingyi : System.Web.UI.Page
{
 

    protected void Page_Load(object sender, EventArgs e)
    {
        //模拟长时间处理
        //Thread.Sleep(5000);
 

        //获取参数
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);


        //调用框架免代理通用接口删，公用一下删除接口
        string return_str = "";
        object[] re_dsi = IPC.Call("框架免代理通用接口删", new object[] {  dt_request });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            return_str = re_dsi[1].ToString();
        }
        else
        {
            return_str = "调用错误"+ re_dsi[1].ToString();
        }

 
        //返回下载地址和状态""
        Response.Write(return_str);
    }
}