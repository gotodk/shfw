using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class auth_oneuser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证权限
        AuthComm.chekcAuth_fromsession("4", UserSession.最终权值_后台菜单权限, true);

        if (!IsPostBack)
        {
            if (Request["UAid"] == null || Request["UAid"].ToString().Trim() == "")
            {
                ;
            }
            else
            {
                string UAid = Request["UAid"].ToString().Trim();
                //有UAid，开始找数据，找到了才能修改
                DataSet ds = new DataSet();
                object[] re_dsi = IPC.Call("获取单个用户权限", new object[] { UAid });
                if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
                {

                    //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                    ds = (DataSet)re_dsi[1];
                    if (ds.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "ok")
                    {
                        quyu_zhao.Visible = false;
                        quyu_peizhi.Visible = true;


                        //把可用的权限组弄上
                        //调用执行方法获取数据
                        DataTable dtdtD = new DataTable();
                        object[] re_dsiD = IPC.Call("获取菜单数据", new object[] { "auth_group", 0, 1 });
                        if (re_dsiD[0].ToString() == "ok")
                        {

                            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                            dtdtD = (DataTable)re_dsiD[1];
                            Uingroups.DataSource = dtdtD;
                            Uingroups.DataTextField = "SortNameTree";
                            Uingroups.DataValueField = "SortID";
                            Uingroups.DataBind();
                        }
                        else
                        {
                            errmsg.InnerHtml = re_dsiD[1].ToString();//向客户端输出错误字符串

                        }



                        //把下拉菜单默认值弄上
                        ee_UAid.Text = ds.Tables["数据记录"].Rows[0]["UAid"].ToString();
                        ee_Uloginname.Text = ds.Tables["数据记录"].Rows[0]["Uloginname"].ToString();
                        UfinalUnumber.Text =  ds.Tables["数据记录"].Rows[0]["UfinalUnumber1"].ToString() + "," + ds.Tables["数据记录"].Rows[0]["UfinalUnumber2"].ToString() + "," + ds.Tables["数据记录"].Rows[0]["UfinalUnumber3"].ToString() + "," + ds.Tables["数据记录"].Rows[0]["UfinalUnumber4"].ToString() + "," + ds.Tables["数据记录"].Rows[0]["UfinalUnumber5"].ToString();
                        //调用执行方法获取数据
                        DataSet dsQ = new DataSet();
                        object[] re_dsiQ = IPC.Call("获取所有已启用的权限枚举", new object[] { "" });
                        if (re_dsiQ[0].ToString() == "ok" && re_dsiQ[1] != null)
                        {

                            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                            dsQ = (DataSet)re_dsiQ[1];

                            Unumber1.DataSource = dsQ.Tables[0];
                            Unumber1.DataTextField = "ANBaseName";
                            Unumber1.DataValueField = "ANBaseNumber";
                            Unumber1.DataBind();


                            Unumber2.DataSource = dsQ.Tables[1];
                            Unumber2.DataTextField = "ANBaseName";
                            Unumber2.DataValueField = "ANBaseNumber";
                            Unumber2.DataBind();

                            Unumber3.DataSource = dsQ.Tables[2];
                            Unumber3.DataTextField = "ANBaseName";
                            Unumber3.DataValueField = "ANBaseNumber";
                            Unumber3.DataBind();

                            Unumber4.DataSource = dsQ.Tables[3];
                            Unumber4.DataTextField = "ANBaseName";
                            Unumber4.DataValueField = "ANBaseNumber";
                            Unumber4.DataBind();

                            Unumber5.DataSource = dsQ.Tables[4];
                            Unumber5.DataTextField = "ANBaseName";
                            Unumber5.DataValueField = "ANBaseNumber";
                            Unumber5.DataBind();

                        }
                        else
                        {
                            errmsg.InnerHtml = re_dsiQ[0].ToString();//向客户端输出错误字符串

                        }

                        //给对应的地方赋值
                        Dictionary<string, string> dic_Unumber1 = AuthComm.GetEnumFormUnumber(ds.Tables["数据记录"].Rows[0]["Unumber1"].ToString());
                        foreach (KeyValuePair<string, string> kv in dic_Unumber1)
                        {
                            ListItem lt = Unumber1.Items.FindByValue(kv.Value);
                            if (lt != null)
                            {
                                lt.Selected = true;
                            }
                        }

                        Dictionary<string, string> dic_Unumber2 = AuthComm.GetEnumFormUnumber(ds.Tables["数据记录"].Rows[0]["Unumber2"].ToString());
                        foreach (KeyValuePair<string, string> kv in dic_Unumber2)
                        {
                            ListItem lt = Unumber2.Items.FindByValue(kv.Value);
                            if (lt != null)
                            {
                                lt.Selected = true;
                            }
                        }

                        Dictionary<string, string> dic_Unumber3 = AuthComm.GetEnumFormUnumber(ds.Tables["数据记录"].Rows[0]["Unumber3"].ToString());
                        foreach (KeyValuePair<string, string> kv in dic_Unumber3)
                        {
                            ListItem lt = Unumber3.Items.FindByValue(kv.Value);
                            if (lt != null)
                            {
                                lt.Selected = true;
                            }
                        }

                        Dictionary<string, string> dic_Unumber4 = AuthComm.GetEnumFormUnumber(ds.Tables["数据记录"].Rows[0]["Unumber4"].ToString());
                        foreach (KeyValuePair<string, string> kv in dic_Unumber4)
                        {
                            ListItem lt = Unumber4.Items.FindByValue(kv.Value);
                            if (lt != null)
                            {
                                lt.Selected = true;
                            }
                        }

                        Dictionary<string, string> dic_Unumber5 = AuthComm.GetEnumFormUnumber(ds.Tables["数据记录"].Rows[0]["Unumber5"].ToString());
                        foreach (KeyValuePair<string, string> kv in dic_Unumber5)
                        {
                            ListItem lt = Unumber5.Items.FindByValue(kv.Value);
                            if (lt != null)
                            {
                                lt.Selected = true;
                            }
                        }

                        string[] groupstr = ds.Tables["数据记录"].Rows[0]["Uingroups"].ToString().Split(',');
                        for (int i = 0; i < groupstr.Count(); i++)
                        {
                            if (groupstr[i].Trim() != "")
                            {
                                ListItem lt = Uingroups.Items.FindByValue(groupstr[i]);
                                if (lt != null)
                                {
                                    lt.Selected = true;
                                }
                                 
                            }
             
                        }

                    }
                    else
                    {
                        errmsg.InnerHtml = ds.Tables["返回值单条"].Rows[0]["提示文本"].ToString();
                    }

                }
                else
                {
                    errmsg.InnerHtml = re_dsi[1].ToString();

                }
                
            }
        }
    }
    protected void kaishizhao_Click(object sender, EventArgs e)
    {
        //用id跳转自己
        Response.Redirect("/adminht/auth_oneuser.aspx?UAid=" + idorname.Text);
    }
    protected void addbutton1_Click(object sender, EventArgs e)
    {
        BigInteger Unumber1_qx = 0;
        for (int i = 0; i < Unumber1.Items.Count; i++)
        {
            if (Unumber1.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber1.Items[i].Value);
                Unumber1_qx = Unumber1_qx | numS;
            }
        }
       

        BigInteger Unumber2_qx = 0;
        for (int i = 0; i < Unumber2.Items.Count; i++)
        {
            if (Unumber2.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber2.Items[i].Value);
                Unumber2_qx = Unumber2_qx | numS;
            }
        }
       
        BigInteger Unumber3_qx = 0;
        for (int i = 0; i < Unumber3.Items.Count; i++)
        {
            if (Unumber3.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber3.Items[i].Value);
                Unumber3_qx = Unumber3_qx | numS;
            }
        }
         

        BigInteger Unumber4_qx = 0;
        for (int i = 0; i < Unumber4.Items.Count; i++)
        {
            if (Unumber4.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber4.Items[i].Value);
                Unumber4_qx = Unumber4_qx | numS;
            }
        }
         

        BigInteger Unumber5_qx = 0;
        for (int i = 0; i < Unumber5.Items.Count; i++)
        {
            if (Unumber5.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber5.Items[i].Value);
                Unumber5_qx = Unumber5_qx | numS;
            }
        }

        string groupstr = "";
        for (int i = 0; i < Uingroups.Items.Count; i++)
        {
            if (Uingroups.Items[i].Selected)
            {
                groupstr = groupstr + Uingroups.Items[i].Value + ",";
            }
        }
        groupstr = groupstr.TrimEnd(',');


        object[] re_dsiD = IPC.Call("编辑一个用户的权限", new object[] { ee_UAid.Text, Unumber1_qx.ToString(), Unumber2_qx.ToString(), Unumber3_qx.ToString(), Unumber4_qx.ToString(), Unumber5_qx.ToString(), groupstr });

        //用id跳转自己
        Response.Redirect("/adminht/auth_oneuser.aspx?UAid=" + ee_UAid.Text);
    }
}