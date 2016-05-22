using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class quanmao_info : System.Web.UI.Page
{


    public DataSet dsinfo = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["idforedit"] == null || Request["idforedit"].ToString().Trim() == "")
        {
            return;
        }
        else
        {
            form1.Visible = false;
            getinfo(Request["idforedit"].ToString().Trim());
        }

        
    }

    /// <summary>
    /// 根据关键字显示搜索结果
    /// </summary>
    /// <param name="key"></param>
    private void getlistkh(string key)
    {
        //调用执行方法获取数据
         
        object[] re_dsi = IPC.Call("获取客户全貌", new object[] { "找客户", key.Trim(), "","" });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet dsreturn = (DataSet)re_dsi[1];
            ssjg.DataSource = dsreturn.Tables["数据记录"].DefaultView;//数据绑定
            ssjg.DataTextField = "Lshowname";
            ssjg.DataValueField = "YYID_uuuu";
            ssjg.DataBind();

            if (dsreturn.Tables["数据记录"].Rows.Count > 0)
            {
                getinfo(dsreturn.Tables["数据记录"].Rows[0]["YYID_uuuu"].ToString());
            }
            else
            {
                getinfo("");
            }
           



        }
        else
        {
            ssjg.DataSource = null;
            ssjg.DataBind();

            getinfo("");

        }

       


    }



    /// <summary>
    /// 根据客户编号获取详细信息
    /// </summary>
    /// <param name="key"></param>
    private void getinfo(string id)
    {
        //调用执行方法获取数据

        object[] re_dsi = IPC.Call("获取客户全貌", new object[] { "找详情", id, "", "" });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dsinfo = (DataSet)re_dsi[1];

            if (dsinfo.Tables["数据记录"].Rows.Count > 0)
            {
                //对数据集进行二次处理 Table2是情报列表， Table1是联系人列表, 数据记录 是档案主表

                string lianxirenstr = "";
                for (int i = 0; i < dsinfo.Tables["Table1"].Rows.Count; i++)
                {
                    string tt = dsinfo.Tables["Table1"].Rows[i]["Klianiren"].ToString() + "，" + dsinfo.Tables["Table1"].Rows[i]["KKS"].ToString() + "，" + dsinfo.Tables["Table1"].Rows[i]["Kzhicheng"].ToString() + "，" + dsinfo.Tables["Table1"].Rows[i]["Kxingbie"].ToString() + "，" + dsinfo.Tables["Table1"].Rows[i]["Kdianhua"].ToString() + "。<br/>";
                    lianxirenstr = lianxirenstr + tt;
                }
                dsinfo.Tables["数据记录"].Rows[0]["lianxirenstr"] = lianxirenstr;
                 

                showinfor.DataSource = dsinfo.Tables["数据记录"];
                showinfor.DataBind();

                Rqingbao.DataSource = dsinfo.Tables["Table2"];
                Rqingbao.DataBind();
            }
            else
            {
                showinfor.DataSource = null;
                showinfor.DataBind();

                Rqingbao.DataSource = null;
                Rqingbao.DataBind();
            }

        


        }
        else
        {
            showinfor.DataSource = null;
            showinfor.DataBind();

            Rqingbao.DataSource = null;
            Rqingbao.DataBind();

        }
    }




    protected void kaishizhao_Click(object sender, EventArgs e)
    {
        getlistkh(idorname.Text);
    }

    protected void ssjg_SelectedIndexChanged(object sender, EventArgs e)
    {
        getinfo(ssjg.SelectedValue);
    }
}