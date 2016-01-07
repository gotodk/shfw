using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Web.Script.Serialization;
using System.Numerics;

/// <summary>
/// 核心业务的相关处理接口
/// </summary>
[WebService(Namespace = "http://corebusiness.aftipc.com/", Description = "V1.00->xxx")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class bsmain : System.Web.Services.WebService
{

    public bsmain()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }


    # region  基础前置方法

    /// <summary>
    /// 初始化返回值数据集,执行结果只有两种ok和err(大多数情况是这个标准)
    /// </summary>
    /// <returns></returns>
    private DataSet initReturnDataSet()
    {
        DataSet ds = new DataSet();
        DataTable auto2 = new DataTable();
        auto2.TableName = "返回值单条";
        auto2.Columns.Add("执行结果");
        auto2.Columns.Add("提示文本");
        auto2.Columns.Add("附件信息1");
        auto2.Columns.Add("附件信息2");
        auto2.Columns.Add("附件信息3");
        auto2.Columns.Add("附件信息4");
        auto2.Columns.Add("附件信息5");
        ds.Tables.Add(auto2);
        return ds;
    }
    /// <summary>
    /// 是否开启防篡改验证
    /// </summary>
    /// <returns></returns>
    private bool IsMD5check()
    {
        return true;
    }

    /// <summary>
    /// 测试该接口是否还活着(每个接口必备)
    /// </summary>
    /// <param name="temp">随便传</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "测试接口", Description = "测试该接口是否还活着(每个接口必备)")]
    public string onlinetest(string temp)
    {
        //根据不同的传入值，后续可以检查不同的东西，比如这个接口所连接的数据库，比如进程池，服务器空间等等。。。
        return "ok";
    }



    /// <summary>
    /// 获取扫码演示结果并处理
    /// </summary>
    /// <param name="tiaoma">条码</param>
    /// <returns>返回ok就是接口正常</returns>
    [WebMethod(MessageName = "获取扫码演示结果并处理", Description = "获取扫码演示结果并处理")]
    public string getsaomajieguo_demo(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@SID", ht_forUI["tiaoma"].ToString());

        return_ht = I_DBL.RunParam_SQL("select top 1 SID,Sname from demouser where SID=@SID", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                return "错误err，条码不存在！";
            }
            else
            {
                DataRow dr = redb.Rows[0];
                return "[{ \"条码\": \"" + dr["SID"].ToString() + "\", \"姓名\": \"" + dr["Sname"].ToString() + "\"  }]";
            }

        }
        else
        {
            return "错误err，系统异常！";
        }


    }


    # endregion

}
