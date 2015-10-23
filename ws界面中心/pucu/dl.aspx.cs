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

public partial class pucu_dl : System.Web.UI.Page
{
    private bool DelFileAll(String strPath)
    {
        DirectoryInfo oDirectory = new DirectoryInfo(strPath);
        try
        {
            foreach (FileInfo file in oDirectory.GetFiles())
            {
                
                    TimeSpan   ts = file.CreationTime - DateTime.Now;
                if (File.Exists(file.FullName) && ts.Days <= -3)
                {
                    File.Delete(file.FullName);
                }
                 
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //模拟长时间处理
        //Thread.Sleep(5000);

        //清理历史数据
        string lujing = "/exceltemp/";
        DelFileAll(Server.MapPath(lujing));

        //获取参数
        DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);


        //根据id获取配置自动生成电子表格存到服务器上，注意命名防止重复覆盖。
        DataSet dsreturn = new DataSet();
        object[] re_dsi = IPC.Call("根据配置生成导出数据", new object[] {  dt_request });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            dsreturn = (DataSet)(re_dsi[1]);
        }

        //文件名
        string filename = dsreturn.Tables["返回值单条"].Rows[0]["附件信息1"].ToString() + "-" + DateTime.Now.ToFileTime().ToString() + ".xls";
 
        //生成
        if (Directory.Exists(Server.MapPath(lujing)))
        {
        }
        else
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Server.MapPath(lujing));
            directoryInfo.Create();
        }
        DataSet dsxlsDB = new DataSet();
        dsxlsDB.Tables.Add(dsreturn.Tables["导出的数据"].Copy());
        MyXlsClass.goxls(dsxlsDB, filename, dsreturn.Tables["返回值单条"].Rows[0]["附件信息1"].ToString(), dsreturn.Tables["返回值单条"].Rows[0]["附件信息1"].ToString(), 30, Server.MapPath(lujing));
        //返回下载地址和状态""
        Response.Write("ok|" + lujing + filename);
    }
}