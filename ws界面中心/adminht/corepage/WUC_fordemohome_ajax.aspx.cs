using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WUC_fordemohome_ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        if(Request["hqbz"].ToString() == "pie")
        {
            Response.Write("  { label: 'ddd', data: 10, shuliang: '233', color: '#68BC31' },{ label: '--', data: 20, shuliang: '233', color: '#2091CF' },{ label: '--', data: 30, shuliang: '233', color: '#AF4E96' },{ label: '--', data: 40, shuliang: '233', color: '#DA5430' }, { label: '--', data: 50, shuliang: '233', color: '#FEE074' }");
        }
        */

        //调用执行方法获取数据
        object[] re_dsi = IPC.Call("获取仪表盘数据", new object[] { Request["hqbz"].ToString(), UserSession.唯一键, "" });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            string restr = re_dsi[1].ToString();
            //
            Response.Write(restr);

        }
        else
        {
            Response.Write("");//向客户端输出错误字符串

        }




    }
}