using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// CallIPCPB 的摘要说明
/// </summary>
public static class CallIPCPB
{
 
    /// <summary>
    /// 获取表单配置
    /// </summary>
    /// <returns></returns>
    public static DataSet Get_FormInfoDB(string formFID)
    {
        object[] re_dsi = IPC.Call("获取指定表单界面配置用于生成", new object[] { formFID });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            return (DataSet)(re_dsi[1]);
        }
        return null;

    }


    /// <summary>
    /// 获取通用列表配置
    /// </summary>
    /// <returns></returns>
    public static DataSet Get_FormsListDB(string formFID)
    {
        object[] re_dsi = IPC.Call("获取通用数据列表配置", new object[] { formFID });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            return (DataSet)(re_dsi[1]);
        }
        return null;

    }

}