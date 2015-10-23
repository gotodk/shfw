using FMDBHelperClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// CombGuid 的摘要说明
/// </summary>
public static class CombGuid
{
    /// <summary>
    /// 得到一个新的可排序的GUID
    /// </summary>
    /// <param name="qianzhui">自定义前缀，不能为null或空字符串，不能含有-</param>
    /// <returns></returns>
    public static string GetNewCombGuid(string qianzhui)
	{
        //对应的数据库方式为: select dbo.GetCG('自定义前缀',NEWID())
        //用sql语句的话，必须存在两个函数 CG和GetCG,一般情况不需要从数据库直接调用

        if (qianzhui == null || qianzhui.Trim() == "" || qianzhui.IndexOf("-") >=0)
        {
            throw new Exception("无效的自定义前缀");
        }

        byte[] guidArray = System.Guid.NewGuid().ToByteArray();
        DateTime baseDate = new DateTime(1900, 1, 1);
        DateTime now = DateTime.Now;
        // Get the days and milliseconds which will be used to build the byte string 
        TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
        TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));

        // Convert to a byte array 
        // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
        byte[] daysArray = BitConverter.GetBytes(days.Days);
        byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

        // Reverse the bytes to match SQL Servers ordering 
        Array.Reverse(daysArray);
        Array.Reverse(msecsArray);

        // Copy the bytes into the guid 前4个字节保存和19000101这个日期的 差值，SQL Server的基准日期是19000101
        Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
        Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
        //处理字符串
        string[] newguidarr = new System.Guid(guidArray).ToString().ToUpper().Split('-');
        return qianzhui + "-" + newguidarr[4] + "-" + newguidarr[0] + "-" + newguidarr[1] + "-" + newguidarr[2] + "-" + newguidarr[3]; 
	}



    /// <summary>
    /// 根据指定模块名或业务名称特定标识，从序列中生成一个新的编号.如果生成失败就反馈可排序的GUID
    /// </summary>
    /// <param name="modname">模块名或业务名称特定标识</param>
    /// <returns></returns>
    public static string GetMewIdFormSequence(string modname)
    {
        if (modname.Trim() == "")
        {
            return GetNewCombGuid("ifSequenceERR");
        }
        try
        {
            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            DataSet DataSet_this = new DataSet();
            Hashtable return_ht = new Hashtable();
            Hashtable putin_ht = new Hashtable();
            putin_ht["ModuleName"] = modname;
            return_ht = I_DBL.RunProc_CMD("GetSequence", "新编号", putin_ht);
            DataSet_this = (DataSet)return_ht["return_ds"];
            return DataSet_this.Tables[0].Rows[0][0].ToString();
        }
        catch
        {
            return GetNewCombGuid("ifSequenceERR");
        }



    }


    /// <summary>
    /// 根据CombGuid得到时间值
    /// </summary>
    /// <param name="CombGuid">一个可排序的GUID</param>
    /// <returns></returns>
    public static string GetDateFromComb(string CombGuid)
    {
        //变化格式方便解析时间
        string[] t = CombGuid.Split('-');

        Guid guid = new Guid(t[2] + "-" + t[3] + "-" + t[4] + "-" + t[5] + "-" + t[1]);
        DateTime baseDate = new DateTime(1900, 1, 1);
        byte[] daysArray = new byte[4];
        byte[] msecsArray = new byte[4];
        byte[] guidArray = guid.ToByteArray();
        // Copy the date parts of the guid to the respective byte arrays. 
        Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
        Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);
        // Reverse the arrays to put them into the appropriate order 
        Array.Reverse(daysArray);
        Array.Reverse(msecsArray);
        // Convert the bytes to ints 
        int days = BitConverter.ToInt32(daysArray, 0);
        int msecs = BitConverter.ToInt32(msecsArray, 0);
        DateTime date = baseDate.AddDays(days);
        date = date.AddMilliseconds(msecs * 3.333333);
 

        return date.ToString("yyyy-MM-dd HH:mm:ss fff");
    }
 
}