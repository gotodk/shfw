using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// K3MM 的摘要说明
/// </summary>
public static class K3MM
{
    public static DataTable dtt = null;
    public static Dictionary<string, Dictionary<int, string>> dicDZB = new Dictionary<string, Dictionary<int, string>>();

    public static void  Init_ddt()
    {
        if (dtt == null)
        {
            string lujing = ConfigurationManager.AppSettings["K3mimaDZB"].ToString();
            dtt = new DataTable();
            dtt.ReadXmlSchema(lujing + "k3mima_s.xml");
            dtt.ReadXml(lujing + "k3mima.xml");



 
           

            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                Dictionary<int, string> dicW = new Dictionary<int, string>();
                dicW.Clear();
                dicW[1] = dtt.Rows[i]["w1"].ToString();
                dicW[2] = dtt.Rows[i]["w2"].ToString();
                dicW[3] = dtt.Rows[i]["w3"].ToString();
                dicW[4] = dtt.Rows[i]["w4"].ToString();
                dicW[5] = dtt.Rows[i]["w5"].ToString();
                dicW[6] = dtt.Rows[i]["w6"].ToString();

                dicW[7] = dtt.Rows[i]["w1"].ToString();
                dicW[8] = dtt.Rows[i]["w2"].ToString();
                dicW[9] = dtt.Rows[i]["w3"].ToString();
                dicW[10] = dtt.Rows[i]["w4"].ToString();
                dicW[11] = dtt.Rows[i]["w5"].ToString();
                dicW[12] = dtt.Rows[i]["w6"].ToString();

                dicW[13] = dtt.Rows[i]["w1"].ToString();
                dicW[14] = dtt.Rows[i]["w2"].ToString();
                dicW[15] = dtt.Rows[i]["w3"].ToString();
                dicW[16] = dtt.Rows[i]["w4"].ToString();
                dicW[17] = dtt.Rows[i]["w5"].ToString();
                dicW[18] = dtt.Rows[i]["w6"].ToString();

                dicW[19] = dtt.Rows[i]["w1"].ToString();
                dicW[20] = dtt.Rows[i]["w2"].ToString();
                dicW[21] = dtt.Rows[i]["w3"].ToString();
                dicW[22] = dtt.Rows[i]["w4"].ToString();
                dicW[23] = dtt.Rows[i]["w5"].ToString();
                dicW[24] = dtt.Rows[i]["w6"].ToString();
                dicDZB[dtt.Rows[i]["dm"].ToString()] = dicW;

            }

            string aa = "";

        }
        else
        {
            ;
        }
        
    }


    public static string GetMM(string ming)
    {
        Init_ddt();

        string miwen = "";
        for (int i = 1; i <= ming.Length; i++)
        {
            miwen = miwen + dicDZB[ming[i-1].ToString()][i];
        }

        return miwen;
    }

   

} 