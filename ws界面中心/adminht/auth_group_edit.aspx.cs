using FMipcClass;
using FMPublicClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class auth_group_edit : System.Web.UI.Page
{

    public string tbshowname = "";
    Hashtable ht_nemu_tb = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证权限
        AuthComm.chekcAuth_fromsession("1", UserSession.最终权值_后台菜单权限, true);

        if (!IsPostBack)
        {
            //绑定所有可选权限
            //调用执行方法获取数据
            DataSet ds = new DataSet();
            object[] re_dsi = IPC.Call("获取所有已启用的权限枚举", new object[] { "" });
            if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                ds = (DataSet)re_dsi[1];

                Unumber1.DataSource = ds.Tables[0];
                Unumber1.DataTextField = "ANBaseName";
                Unumber1.DataValueField = "ANBaseNumber";
                Unumber1.DataBind();
       

                Unumber2.DataSource = ds.Tables[1];
                Unumber2.DataTextField = "ANBaseName";
                Unumber2.DataValueField = "ANBaseNumber";
                Unumber2.DataBind();

                Unumber3.DataSource = ds.Tables[2];
                Unumber3.DataTextField = "ANBaseName";
                Unumber3.DataValueField = "ANBaseNumber";
                Unumber3.DataBind();

                Unumber4.DataSource = ds.Tables[3];
                Unumber4.DataTextField = "ANBaseName";
                Unumber4.DataValueField = "ANBaseNumber";
                Unumber4.DataBind();

                Unumber5.DataSource = ds.Tables[4];
                Unumber5.DataTextField = "ANBaseName";
                Unumber5.DataValueField = "ANBaseNumber";
                Unumber5.DataBind();

            }
            else
            {
                errmsg.Text = re_dsi[1].ToString();//向客户端输出错误字符串

            }




            //加载菜单  
            ReLoadNode();
        }
        

         

 
    }
 

    /// <summary>
    /// 重新加载菜单
    /// </summary>
    private void ReLoadNode()
    {
        errmsg.Text = "";
         
            tbshowname = "所有权限组";
            dbtbname.Text = "auth_group";
 

        if (Request["sortid"] == null || Request["sortid"].ToString().Trim() == "")
        {
            sh_SortID.Text = "0";
            ee_SortID.Text = "0";
        }
        else
        {
            sh_SortID.Text = Request["sortid"].ToString().Trim();
            ee_SortID.Text = Request["sortid"].ToString().Trim();
        }


        //加载原始数据等待修改
        if (sh_SortID.Text == "0" || sh_SortID.Text == "" || dbtbname.Text == "")
        { }
        else
        {
            //调用执行方法获取数据
            DataSet dsD = new DataSet();
            object[] re_dsiD = IPC.Call("获取单条菜单数据", new object[] { dbtbname.Text, sh_SortID.Text });
            if (re_dsiD[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                dsD = (DataSet)re_dsiD[1];
                try
                {
                    ee_SortName.Text = dsD.Tables["数据记录"].Rows[0]["SortName"].ToString();
                    sh_SortName.Text = dsD.Tables["数据记录"].Rows[0]["SortName"].ToString();

                    //把已有的权限分解并展示
                    Dictionary<string,string> dic_Unumber1 = AuthComm.GetEnumFormUnumber(dsD.Tables["数据记录"].Rows[0]["Unumber1"].ToString());
                    foreach (KeyValuePair<string, string> kv in dic_Unumber1)
                    {
                        ListItem lt = Unumber1.Items.FindByValue(kv.Value);
                        if (lt != null)
                        {
                            lt.Selected = true;
                        }
                        
                    }

                    Dictionary<string, string> dic_Unumber2 = AuthComm.GetEnumFormUnumber(dsD.Tables["数据记录"].Rows[0]["Unumber2"].ToString());
                    foreach (KeyValuePair<string, string> kv in dic_Unumber2)
                    {
                        ListItem lt = Unumber2.Items.FindByValue(kv.Value);
                        if (lt != null)
                        {
                            lt.Selected = true;
                        }
                    }

                    Dictionary<string, string> dic_Unumber3 = AuthComm.GetEnumFormUnumber(dsD.Tables["数据记录"].Rows[0]["Unumber3"].ToString());
                    foreach (KeyValuePair<string, string> kv in dic_Unumber3)
                    {
                        ListItem lt = Unumber3.Items.FindByValue(kv.Value);
                        if (lt != null)
                        {
                            lt.Selected = true;
                        }
                    }

                    Dictionary<string, string> dic_Unumber4 = AuthComm.GetEnumFormUnumber(dsD.Tables["数据记录"].Rows[0]["Unumber4"].ToString());
                    foreach (KeyValuePair<string, string> kv in dic_Unumber4)
                    {
                        ListItem lt = Unumber4.Items.FindByValue(kv.Value);
                        if (lt != null)
                        {
                            lt.Selected = true;
                        }
                    }

                    Dictionary<string, string> dic_Unumber5 = AuthComm.GetEnumFormUnumber(dsD.Tables["数据记录"].Rows[0]["Unumber5"].ToString());
                    foreach (KeyValuePair<string, string> kv in dic_Unumber5)
                    {
                        ListItem lt = Unumber5.Items.FindByValue(kv.Value);
                        if (lt != null)
                        {
                            lt.Selected = true;
                        }
                    }


                }
                catch (Exception ex)
                {
                    errmsg.Text = "获取数据出错";
                }
            }
            else
            {
                errmsg.Text = re_dsiD[1].ToString();//向客户端输出错误字符串

            }
        }

        TV.Nodes.Clear();
        //调用执行方法获取数据
        DataTable dt = new DataTable();
        object[] re_dsi = IPC.Call("获取菜单数据", new object[] { dbtbname.Text,0,1 });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dt = (DataTable)re_dsi[1];
        }
        else
        {
            errmsg.Text = re_dsi[1].ToString();//向客户端输出错误字符串

        }
        this.InitNode(dt);
        TV.ExpandAll();
    }

    /// <summary>
    /// 初始化节点
    /// </summary>
    /// <param name="dt">要加载成树结构的数据源</param>
    private void InitNode(DataTable dt)
    {
        DataRow[] drRoot0 = dt.Select("1=1");
        if (drRoot0 != null && drRoot0.Length > 0)
        {
            DataRow[] drRoot_zi1 = dt.Select("SortParentID='0'", "SortOrder asc");
            int allzi = drRoot_zi1.Length;
            for (int t = 0; t < allzi; t++)
            {
                DataRow drRoot_nowzi = drRoot_zi1[t];
                //检查菜单显示，隐藏禁止查看的菜单。

                TreeNode root = new TreeNode();
                string yanse = "orange";
                if (sh_SortID.Text == drRoot_nowzi["SortID"].ToString())
                {
                    yanse = "red";
                }
                string tubiao = "<i class='ace-icon fa fa-users'></i>";

                root.Text = tubiao + "<span class='" + yanse + "'>" + drRoot_nowzi["SortName"].ToString() + "</span>";
                root.NavigateUrl = "?sortid=" + drRoot_nowzi["SortID"].ToString();
                root.Target = "_top";
                this.TV.Nodes.Add(root);
                this.BuildChild(drRoot_nowzi, root, dt);


            }

        }

    }

    /// <summary>
    /// 加载子节点
    /// </summary>
    /// <param name="dr">父节点对应的行</param>
    /// <param name="root">父节点</param>
    /// <param name="dt">要加载成树结构的数据源</param>
    private void BuildChild(DataRow dr, TreeNode root, DataTable dt)
    {
        if (dr == null || root == null) return;
        DataRow[] drChilds = dt.Select("SortParentID='" + dr["SortID"] + "'", "SortOrder asc");
        if (drChilds != null || drChilds.Length > 0)
        {
            foreach (DataRow drChild in drChilds)
            {

                //检查菜单显示，隐藏禁止查看的菜单。

                TreeNode node = new TreeNode();
                string yanse = "blue";
                if (sh_SortID.Text == drChild["SortID"].ToString())
                {
                    yanse = "red";
                }
                string tubiao = "";
                node.Text = tubiao + "<span class='" + yanse + "'>" + drChild["SortName"].ToString() + "</span>";
                node.NavigateUrl = "?sortid=" + drChild["SortID"].ToString();
                node.Target = "_top";
                root.ChildNodes.Add(node);
                this.BuildChild(drChild, node, dt);


            }
        }
    }


    protected void editjiedian_Click(object sender, EventArgs e)
    {
        errmsg.Text = "";
        Button b = (Button)sender;

    
        //调用执行方法获取数据
        Hashtable HTforParameter = new Hashtable();
        HTforParameter["buttonid"] = b.ID;
        HTforParameter["dbtbname"] = dbtbname.Text;
        HTforParameter["SortID"] = sh_SortID.Text;
        HTforParameter["add_SortName"] = addnewjiedian_name.Text;
        HTforParameter["move_SortParentID"] = movenewsid.Text;
        HTforParameter["ee_SortName"] = ee_SortName.Text;


        BigInteger Unumber1_qx = 0;
        for (int i = 0; i < Unumber1.Items.Count; i++)
        {
            if (Unumber1.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber1.Items[i].Value);
                Unumber1_qx = Unumber1_qx | numS;
            }
        }
        HTforParameter["ee_Unumber1_qx"] = Unumber1_qx.ToString();


        BigInteger Unumber2_qx = 0;
        for (int i = 0; i < Unumber2.Items.Count; i++)
        {
            if (Unumber2.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber2.Items[i].Value);
                Unumber2_qx = Unumber2_qx | numS;
            }
        }
        HTforParameter["ee_Unumber2_qx"] = Unumber2_qx.ToString();

        BigInteger Unumber3_qx = 0;
        for (int i = 0; i < Unumber3.Items.Count; i++)
        {
            if (Unumber3.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber3.Items[i].Value);
                Unumber3_qx = Unumber3_qx | numS;
            }
        }
        HTforParameter["ee_Unumber3_qx"] = Unumber3_qx.ToString();

        BigInteger Unumber4_qx = 0;
        for (int i = 0; i < Unumber4.Items.Count; i++)
        {
            if (Unumber4.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber4.Items[i].Value);
                Unumber4_qx = Unumber4_qx | numS;
            }
        }
        HTforParameter["ee_Unumber4_qx"] = Unumber4_qx.ToString();

        BigInteger Unumber5_qx = 0;
        for (int i = 0; i < Unumber5.Items.Count; i++)
        {
            if (Unumber5.Items[i].Selected)
            {
                BigInteger numS = BigInteger.Parse(Unumber5.Items[i].Value);
                Unumber5_qx = Unumber5_qx | numS;
            }
        }
        HTforParameter["ee_Unumber5_qx"] = Unumber5_qx.ToString();
         

        DataTable dt_request = StringOP.GetDataTableFormHashtable(HTforParameter);
        object[] re_dsi = IPC.Call("菜单维护操作", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            string dsreturn = re_dsi[1].ToString();
            if (b.ID == "shanchu")
            {
                Response.Redirect("/adminht/auth_group_edit.aspx");
            }
            else
            {
                Response.Redirect("/adminht/auth_group_edit.aspx?sortid=" + sh_SortID.Text);
            }
            

        }
        else
        {
           errmsg.Text = re_dsi[1].ToString();//向客户端输出错误字符串

        }
    }
 
}