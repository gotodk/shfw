using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class JqGridSearchTo
{
    public String groupOp { get; set; }
    public List<FieldDTO> rules { get; set; }

    public List<JqGridSearchTo> groups { get; set; }
}
public class FieldDTO
{
    public string field { set; get; }
    public string op { get; set; }
    public string data { set; get; }

}

public class jqsearch_sql
{
    public string getsql(JqGridSearchTo jg)
    {
        string D_sql = "";
        if (jg == null)
        {
            return D_sql;
        }
        List<FieldDTO> D_rules = jg.rules;
        if (D_rules != null && D_rules.Count > 0)
        {
            D_sql = "";
            bool diyici = true;
            for (int i = 0; i < D_rules.Count; i++)
            {
                string this_groupOp = jg.groupOp;
                string this_field = D_rules[i].field.Trim();
                string this_op = D_rules[i].op.Trim();
                //处理影响拼语句的特殊字符。包括： '  %
                string this_data = D_rules[i].data.Trim().Replace("%", "[%]").Replace("'", "''");
                if (this_op == "nu" || this_op == "nn") 
                {
                    this_data = "硬塞无用值";
                }
                if (this_field != "" && this_op != "" && this_data != "")
                {
                    if (diyici)
                    {
                        this_groupOp = "    ( ";
                    }
                    //分析操作符
                    switch (this_op)
                    {
                        case "eq"://等于 =
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "=" + " '" + this_data + "'" + " ";
                            break;
                        case "ne"://不等于 <>
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "<>" + " '" + this_data + "'" + " ";
                            break;
                        case "bw"://开始于  like  'xxxx%'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "like" + " '" + this_data + "%'" + " ";
                            break;
                        case "bn"://不开始于 not like 'xxxx%'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "not like" + " '" + this_data + "%'" + " ";
                            break;
                        case "ew"://结束于 like '%xxxx'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "like" + " '%" + this_data + "'" + " ";
                            break;
                        case "en"://不结束于 not like '%xxxx'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "not like" + " '%" + this_data + "'" + " ";
                            break;
                        case "cn"://包含 like '%xxxx%'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "like" + " '%" + this_data + "%'" + " ";
                            break;
                        case "nc"://不包含 not like '%xxxx%'
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "not like" + " '%" + this_data + "%'" + " ";
                            break;
                        case "nu"://不存在  is null 或 =''
                            D_sql = D_sql + this_groupOp + " (" + this_field + " " + "is null or " + this_field + " = '')" + " ";
                            break;
                        case "nn"://存在  not is null 并且 <>''
                            D_sql = D_sql + this_groupOp + " (" + this_field + " " + "is not null and " + this_field + " <> '')" + " ";
                            break;
                        case "in"://属于  in ('xxxx','xxxx','xxxx')
                            string in_this_data1 = this_data.Replace(",", "','");
                            in_this_data1 = "('" + in_this_data1 + "')";
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "in" + " " + in_this_data1 + " ";
                            break;
                        case "ni"://不属于  not in ('xxxx','xxxx','xxxx')
                            string in_this_data2 = this_data.Replace(",", "','");
                            in_this_data2 = "('" + in_this_data2 + "')";
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + "not in" + " " + in_this_data2 + " ";
                            break;
                        default:
                            D_sql = D_sql + this_groupOp + " " + this_field + " " + this_op + " '" + this_data + "'" + " ";
                            break;
                    }
                    
                    diyici = false;
                }
                   
            }
            List<JqGridSearchTo> D_jg = jg.groups;
            if (D_jg != null && D_jg.Count > 0)
            {
                foreach (JqGridSearchTo jg_sub in D_jg)
                {
                    string sub_sql = getsql(jg_sub);
                    if (sub_sql.Trim() != "")
                    {
                        D_sql = D_sql + jg.groupOp + " " + sub_sql;
                    }
                        
                }
            }

            if (D_sql.Trim().IndexOf('(') == 0)
            {
                D_sql = D_sql + ")     ";
            }
            
        }

        return D_sql;
    }

    /// <summary>
    /// 解析分页中的头部搜索条件(空字符串的会被忽略掉)
    /// </summary>
    /// <param name="dic_mysearchtop">待放入的字典</param>
    /// <param name="mysearchtop_str">待解析内容</param>
    public void getmysearchtop(ref Dictionary<string, string> dic_mysearchtop, string mysearchtop_str)
    {
        string[] s1 = mysearchtop_str.Split('&');
        for (int y = 0; y < s1.Length; y++)
        {
            if (s1[y].Trim() != "")
            {
                string[] s2 = s1[y].ToString().Split('=');
                string k = HttpUtility.UrlDecode(s2[0], System.Text.Encoding.UTF8).Trim();
                //处理影响拼语句的特殊字符。包括： '  %
                string v = HttpUtility.UrlDecode(s2[1], System.Text.Encoding.UTF8).Trim().Replace("%", "[%]").Replace("'", "''");
                if (v != "")
                {
                    dic_mysearchtop.Add(k, v);
                }


            }
        }
    }
}