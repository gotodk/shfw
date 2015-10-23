using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upfaceuc : System.Web.UI.UserControl
{

    private string _Cidname = "tutututu";

    [Description("用于表单中name的名称")]
    [DefaultValue("tutututu")]
    public string Cidname
    {
        get
        {
            return _Cidname;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Cidname = "tutututu";
            }
            else
            { _Cidname = value; }
        }
    }

 
    private string _Csite = "sm";

    [Description("图像显示尺寸(md或sm或bg)")]
    [DefaultValue("sm")]
    public string Csite   
    {
        get
        {
            return _Csite;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Csite = "sm";
            }
            else
            { _Csite = value; }

        }
    }

    private string _Cnowimg = "/mytutu/touxiangupload_a.gif";

    [Description("图片初始路径(一般从数据库读取后赋值)")]
    [DefaultValue("/mytutu/touxiangupload_a.gif")]
    public string Cnowimg   
    {
        get
        {
            return _Cnowimg;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Cnowimg = "/mytutu/touxiangupload_a.gif";
            }
            else
            { _Cnowimg = value; }
        }
    }


    private string _Ctitle = "上传图片";

    [Description("弹窗标题")]
    [DefaultValue("上传图片")]
    public string Ctitle
    {
        get
        {
            return _Ctitle;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Ctitle = "上传图片";
            }
            else
            { _Ctitle = value; }
        }
    }


    private string _Cloadingimg = "/mytutu/touxiangupload.gif";

    [Description("进度条动画图片")]
    [DefaultValue("/mytutu/touxiangupload.gif")]
    public string Cloadingimg
    {
        get
        {
            return _Cloadingimg;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Cloadingimg = "/mytutu/touxiangupload.gif";
            }
            else
            { _Cloadingimg = value; }
        }
    }


    private string _Cupaction = "/ajaxcropperupload.aspx";

    [Description("后台处理地址")]
    [DefaultValue("/ajaxcropperupload.aspx")]
    public string Cupaction
    {
        get
        {
            return _Cupaction;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Cupaction = "/ajaxcropperupload.aspx";
            }
            else
            { _Cupaction = value; }
        }
    }



    private string _Cisp = "no";

    [Description("是否头像专用(yes或no)")]
    [DefaultValue("no")]
    public string Cisp
    {
        get
        {
            return _Cisp;
        }
        set
        {
            if (value.Trim() == "")
            {
                _Cisp = "no";
            }
            else
            { _Cisp = value; }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}