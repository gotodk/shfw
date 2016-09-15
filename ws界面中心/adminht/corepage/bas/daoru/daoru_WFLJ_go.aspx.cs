using FMipcClass;
using org.in2bits.MyXls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class corepage_daoru_WFLJ_go : System.Web.UI.Page
{

    #region 必备的公共变量
    /// <summary>
    /// 表单配置
    /// </summary>
    public DataSet dsFPZ = null;
    /// <summary>
    /// 其他辅助配置
    /// </summary>
    public Hashtable htPP = new Hashtable();

    /// <summary>
    /// 列检查
    /// </summary>
    public string liejiancha = "配件编码,配件名称,规格型号,单位,拼音,erp编号,成本价,最低价,零售价,保修期限,状态";
    public string drbj = "配件物料表导入";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //表单识别号
        string FID = "sys_demo_0001";
        #region 必备的配置代码
        //获取表单配置
        dsFPZ = CallIPCPB.Get_FormInfoDB(FID);
        htPP = FUPpublic.initPP(Request, dsFPZ);
        //给控件传值
        wuc_content._dsFPZ = dsFPZ;
        wuc_content._htPP = htPP;
        wuc_script._dsFPZ = dsFPZ;
        wuc_script._htPP = htPP;
        #endregion
 

    }





    /// <summary>
    /// 导入EXCEL至DataSet
    /// </summary>
    /// <param name="sheetname">要查询的Sheet名称</param>
    /// <param name="Path">EXCEL文件全路径</param>
    /// <returns>EXCEL内容</returns>
    public static DataSet GetOneSheet(string sheetname, string Path)
    {
        try
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 12.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            strExcel = "select * from [" + sheetname + "$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            DataSet ds = ds = new DataSet();
            myCommand.Fill(ds, "table1");
            conn.Close();
            return ds;
        }
        catch
        {
            return null;
        }
    }

 


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string saveName = System.DateTime.Now.ToFileTimeUtc() + System.IO.Path.GetExtension(this.fupExcel.FileName);
       
            if (System.IO.Directory.Exists(Server.MapPath("/daoru/")))
            {
                this.fupExcel.SaveAs(Server.MapPath("/daoru/") + saveName);
            }
            else
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("/daoru/"));
                this.fupExcel.SaveAs(Server.MapPath("/daoru/") + saveName);
            }

            string fullPath = Server.MapPath("/daoru/") + saveName;
            TextBox2.Text = fullPath;
            DataTable dtTemp = GetOneSheet(TextBox1.Text, fullPath).Tables[0];

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                GridView1.DataSource = dtTemp.DefaultView;
                GridView1.DataBind();

                Button3.Visible = true;
                Button2.Visible = true;
                Button1.Visible = false;
            }
            else
            {
                tishimsg.Text = "电子表格中没有数据";
                return;
            }

        }
        catch(Exception ex)
        {
 
            tishimsg.Text = ex.ToString();
            return;
        }
    }

    /// <summary>
    /// 将导入的数据保存到对应数据表中
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
 

        DataTable dtTemp = GetOneSheet(TextBox1.Text, TextBox2.Text).Tables[0];

        //调用远程接口处理这个导入。

        //验证是否含有规定的字段
        string lie = liejiancha;
        string[] liearr = lie.Split(',');
        string queshao = "";
        for (int i = 0; i < liearr.Length; i++)
        {
            if (!dtTemp.Columns.Contains(liearr[i]))
            {
                queshao = queshao + liearr[i] + ",";
            }
             
        }
        if (queshao != "")
        {
            //列名存在异常
            tishimsg.Text = "无法导入，导入的电子表格缺少列： " + queshao;
       
            return;
        }


        //调用接口，开始导入
        //调用执行方法获取数据
        object[] re_dsi = IPC.Call("通过电子表格导入数据", new object[] { dtTemp,UserSession.唯一键, drbj });
        if (re_dsi[0].ToString() == "ok")
        {

            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet dsreturn = (DataSet)re_dsi[1];
            if (dsreturn.Tables["返回值单条"].Rows[0]["执行结果"].ToString() == "ok")
            {
                //如果成功
                tishimsg.Text = dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString();
                
                return;
            }
            else
            {
                //如果失败
                tishimsg.Text = dsreturn.Tables["返回值单条"].Rows[0]["提示文本"].ToString();
 
                return;
            }
        }
        else
        {
            tishimsg.Text = re_dsi[1].ToString();
       
            return;
        }




  
    }



    /// <summary>
    /// 重新上传
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }





}