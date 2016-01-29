using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_savebuju : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserSession.唯一键 == "")
        {
            Response.Write("保存失败，找不到用户登录状态信息。");
            return;
        }
        if (Request["caozuo"].ToString() == "")
        {
            Response.Write("保存失败，操作不明确。");
            return;
        }
        if (Request["id"].ToString() == "")
        {
            Response.Write("保存失败，布局目标不明确。");
            return;
        }

        if (Request["lx"].ToString() == "")
        {
            Response.Write("保存失败，布局目标类型不明确。");
            return;
        }
        //保存布局
        string sp = "";
        if (Request["caozuo"].ToString() == "baocun")
        {
            sp = "保存";
        }
        if (Request["caozuo"].ToString() == "chongzhi")
        {
            sp = "重置";
        }
        object[] re_dsi = IPC.Call("保存或者获取用户布局", new object[] { UserSession.唯一键, Request["lx"].ToString(), Request["id"].ToString(), Request["jsonstr"].ToString(), sp });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet dsreturn = (DataSet)re_dsi[1];
            Response.Write(dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString());

        }
        else
        {
            Response.Write(re_dsi[1].ToString());//向客户端输出错误字符串

        }

    }
}