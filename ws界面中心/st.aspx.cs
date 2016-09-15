using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class st : System.Web.UI.Page
{
    public DataSet ds_DD = null;
    public string[] arr_tupian = null;


  


    /// <summary>
    /// 检查是否图片
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Checktu(string p)
    {
        bool fileOK = false;
        String fileExtension = System.IO.Path.GetExtension(Server.MapPath(p)).ToLower();
        String[] allowedExtensions = { ".gif", ".png", ".bmp", ".jpg", ".jpeg" };
        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (fileExtension == allowedExtensions[i])
            {
                fileOK = true;
            }
        }

        return fileOK;
    }

    /// <summary>
    /// 检查是否支持在线播放的视频
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Checkshipin(string p)
    {
        bool fileOK = false;
        String fileExtension = System.IO.Path.GetExtension(Server.MapPath(p)).ToLower();
        String[] allowedExtensions = { ".mp4"};
        for (int i = 0; i < allowedExtensions.Length; i++)
        {
            if (fileExtension == allowedExtensions[i])
            {
                fileOK = true;
            }
        }

        return fileOK;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Request["yz"] == null || Request["yz"].Trim() != "000")
        //{
        //    if (Request.UrlReferrer == null || Request.UrlReferrer.ToString().Trim() == "" || (Request.UrlReferrer.ToString().IndexOf(":28888") < 0 && Request.UrlReferrer.ToString().IndexOf(":8866") < 0 && Request.UrlReferrer.ToString().IndexOf(":3456") < 0))
        //    {
        //        arr_tupian = new string[] { };
        //        Response.Write("无法识别的操作来源");
        //        return;
        //    }
        //}



            DataTable dt_request = RequestForUI.Get_parameter_forUI(Request);
        ///st.aspx|&xxx=1|idforedit|_blank
        object[] re_dsi = IPC.Call("获取单据图片列表", new object[] { dt_request });
        if (re_dsi[0].ToString() == "ok" && re_dsi[1] != null)
        {
            ds_DD = (DataSet)(re_dsi[1]);
            try {
                arr_tupian = ds_DD.Tables["数据记录"].Rows[0]["tupian"].ToString().Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                /*
                //如果只有一个文件，直接转到下载链接
                if (arr_tupian.Length == 1)
                {
                    //单如果是特定格式，就打开而不是下载
                    if (Checktu(arr_tupian[0]))
                    {
                        Response.Redirect("st_d.aspx?fn=" + arr_tupian[0]);
                    }
                    else
                    {
                        Response.Redirect(arr_tupian[0]);
                    }
                        
                    return;
                }
                */
            }
            catch (Exception ex)
            {
                Response.Write("暂无图片");
                arr_tupian = new string[]{ };
            }
           
       


        }
        else
        {
            Response.Write("错误err，接口调用失败"); 
        }
    }

 


}