using FMipcClass;
using FMPublicClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class menu_pub_edit : System.Web.UI.Page
{

    public string tbshowname = "";
    Hashtable ht_nemu_tb = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证权限
        //AuthComm.chekcAuth_fromsession("1", UserSession.最终权值_后台菜单权限, true);

        if(!IsPostBack)
        {
            //加载菜单  
            ReLoadNode();
        }
     

         

 
    }


    private void zhankai(TreeNode tn)
    {
        if (tn.Parent == null)
        {
            return;
        }
        else
        {
            tn.Parent.Expanded = true;
            zhankai(tn.Parent);
        }
    }

  

    /// <summary>
    /// 重新加载菜单
    /// </summary>
    private void ReLoadNode()
    {
        errmsg.Text = "";
        ht_nemu_tb["class_org"] = "组织架构";
        ht_nemu_tb["class_pro"] = "产品分类";
      
        if (Request["tb"] == null || Request["tb"].ToString().Trim() == "")
        {
            tbshowname = "组织架构";
            dbtbname.Text = "class_org";
        }
        else
        {
            string tb1 = Request["tb"].ToString().Trim();
            if (ht_nemu_tb.Contains(tb1))
            {
                tbshowname = ht_nemu_tb[tb1].ToString();
            }
            else
            {
                tbshowname = "";
            }
            dbtbname.Text = tb1;
        }

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
                try {
                    ee_SortName.Text = dsD.Tables["数据记录"].Rows[0]["SortName"].ToString();
                    sh_SortName.Text = dsD.Tables["数据记录"].Rows[0]["SortName"].ToString();

                    ee_m_url.Text = dsD.Tables["数据记录"].Rows[0]["m_url"].ToString();
                    ee_m_tip.Text = dsD.Tables["数据记录"].Rows[0]["m_tip"].ToString();
                    ee_m_tag.Text = dsD.Tables["数据记录"].Rows[0]["m_tag"].ToString();
                    ee_m_ico.Text = dsD.Tables["数据记录"].Rows[0]["m_ico"].ToString();
                    string yc = dsD.Tables["数据记录"].Rows[0]["m_show"].ToString();
                    if (yc == "不隐藏")
                    {
                        ee_m_show1.Checked = true;
                        ee_m_show0.Checked = false;
                    }
                    else
                    {
                        ee_m_show1.Checked = false;
                        ee_m_show0.Checked = true;
                    }
                }
                catch(Exception ex)
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
                string yanse = "blue";
                if (drRoot_nowzi["m_show"].ToString() != "不隐藏")
                {
                    yanse = "grey";
                }
                string tubiao = "";
                if (drRoot_nowzi["m_ico"].ToString() != "")
                {
                    tubiao = "<i class='ace-icon fa " + drRoot_nowzi["m_ico"].ToString() + "'></i>";
                }
                if (sh_SortID.Text == drRoot_nowzi["SortID"].ToString())
                {
                    yanse = "red";
                }
               

                root.Text = tubiao + "<span class='" + yanse + "'>" + drRoot_nowzi["SortName"].ToString() + "</span>";
                root.NavigateUrl = "?sortid=" + drRoot_nowzi["SortID"].ToString() + "&tb=" + dbtbname.Text;
                root.Target = "_top";
                this.TV.Nodes.Add(root);
                if (yanse == "red")
                {

                    root.Expanded = true;
                    zhankai(root);
                }
                else
                {
                    root.Expanded = false;
                }

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
                if (drChild["m_show"].ToString() != "不隐藏")
                {
                    yanse = "grey";
                }
                string tubiao = "";
                if (drChild["m_ico"].ToString() != "")
                {
                    tubiao = "<i class='ace-icon fa " + drChild["m_ico"].ToString() + "'></i>";
                }
                if (sh_SortID.Text == drChild["SortID"].ToString())
                {
                    yanse = "red";
               
                  
                }
               
                node.Text = tubiao + "<span class='" + yanse + "'>" + drChild["SortName"].ToString() + "</span>";
                node.NavigateUrl = "?sortid=" + drChild["SortID"].ToString() + "&tb=" + dbtbname.Text;
                node.Target = "_top";
                root.ChildNodes.Add(node);
                if (yanse == "red")
                {
 
                    node.Expanded = true;
                    zhankai(node);
                }
                else
                {
                    node.Expanded = false;
                }

                this.BuildChild(drChild, node, dt);


            }
        }
    }


    protected void editjiedian_Click(object sender, EventArgs e)
    {
        errmsg.Text = "";
        Button b = (Button)sender;

        //如果是生成xml，不调用操作接口，直接调用接口获取数据并本地生成
        if (b.ID == "shengchengxml")
        {
            //生成选定的菜单数据
            DataTable dtT = new DataTable();
            object[] re_dsiT = IPC.Call("获取菜单数据", new object[] { dbtbname.Text, 0, 1 });
            if (re_dsiT[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                dtT = (DataTable)re_dsiT[1];
                dtT.WriteXml(Server.MapPath("/xml/" + dbtbname.Text + ".xml"), XmlWriteMode.WriteSchema);
            }
            else
            {
                errmsg.Text = re_dsiT[1].ToString();//向客户端输出错误字符串

            }

            //不管选定的哪个菜单，都把权限枚举表里的有效数据也生成到本地，用于菜单权限判定。
            DataSet ds_mj = new DataSet();
            object[] re_dsi_mj = IPC.Call("获取所有已启用的权限枚举", new object[] { "" });
            if (re_dsi_mj[0].ToString() == "ok")
            {

                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                ds_mj = (DataSet)re_dsi_mj[1];
                ds_mj.WriteXml(Server.MapPath("/xml/auth_enum_number_ANused.xml"), XmlWriteMode.WriteSchema);

            }
            else
            {
                errmsg.Text = re_dsi_mj[1].ToString();//向客户端输出错误字符串

            }
        }

        //调用执行方法获取数据
        Hashtable HTforParameter = new Hashtable();
        HTforParameter["buttonid"] = b.ID;
        HTforParameter["dbtbname"] = dbtbname.Text;
        HTforParameter["SortID"] = sh_SortID.Text;
        HTforParameter["add_SortName"] = addnewjiedian_name.Text;
        HTforParameter["move_SortParentID"] = movenewsid.Text;
        HTforParameter["ee_SortName"] = ee_SortName.Text;
        HTforParameter["ee_m_url"] = ee_m_url.Text;
        HTforParameter["ee_m_tip"] = ee_m_tip.Text;
        HTforParameter["ee_m_tag"] = ee_m_tag.Text;
        HTforParameter["ee_m_ico"] = ee_m_ico.Text;
        HTforParameter["ee_m_show1"] = ee_m_show1.Checked.ToString();
        HTforParameter["ee_m_show0"] = ee_m_show0.Checked.ToString();

        DataTable dt_request = StringOP.GetDataTableFormHashtable(HTforParameter);
        object[] re_dsi = IPC.Call("菜单维护操作", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            string dsreturn = re_dsi[1].ToString();
            if (b.ID == "shanchu")
            {
                Response.Redirect("/adminht/auth_menu_edit.aspx?tb=" + dbtbname.Text);
            }
            else
            {
                Response.Redirect("/adminht/auth_menu_edit.aspx?sortid=" + sh_SortID.Text + "&tb=" + dbtbname.Text);
            }
            

        }
        else
        {
           errmsg.Text = re_dsi[1].ToString();//向客户端输出错误字符串

        }
    }
 
}