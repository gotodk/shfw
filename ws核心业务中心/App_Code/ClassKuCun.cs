using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Data;

/// <summary>
/// ClassKuCun 的摘要说明
/// </summary>
public class ClassKuCun
{
    public ClassKuCun()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    /// <summary>
    /// 获取当前库存量
    /// </summary>
    /// <param name="r_chu"></param>
    /// <param name="r_ru"></param>
    /// <param name="r_cpbh"></param>
    /// <param name="r_pihao"></param>
    /// <param name="r_shuliang"></param>
    /// <param name="r_danwei"></param>
    /// <param name="sp"></param>
    /// <returns>数组0 如果是0就是不存在，如果是1就是存在，如果是负数代表执行错误。 数组1代表库存数量</returns>
    public Decimal[] getnow_kucun(string kuwei,string r_cpbh, string r_pihao, string r_danwei, string spsp)
    {
        //第一个是是否存在数据,0是不存在，1是存在,负数代表执行错误， 第二个是存在的情况下，库存数量是多少。
        Decimal[] re = new Decimal[2] { 0,0};
     
        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@kuwei", r_cpbh);
        param.Add("@r_cpbh", r_cpbh);
        param.Add("@r_pihao", r_cpbh);
        param.Add("@r_danwei", r_danwei);

        return_ht = I_DBL.RunParam_SQL("select kucun from ZZZ_C_Inventory where  i_dpid=@kuwei and i_pid=@r_cpbh and danwei=@r_danwei and pihao=@r_pihao", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            if (redb.Rows.Count < 1)
            {
                re[0] = 0;
                re[1] = 0;
                return re;
            }
            else
            {
                if (redb.Rows.Count == 1)
                {
                    re[0] = 1;
                    re[1] = Convert.ToDecimal(redb.Rows[0]["kucun"]);
                    return re;
                }
                else
                {
                    re[0] = -1;
                    re[1] = 0;
                    return re;
                }

               
            }

        }
        else
        {
            re[0] = -1;
            re[1] = 0;
            return re;
        }
         
    }


    public ArrayList get_sql_str(string rid,string sp)
    {
        ArrayList alre = new ArrayList();
        alre.Add("err"); //默认提示
        alre.Add("");

        I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");
        Hashtable return_ht = new Hashtable();
        Hashtable param = new Hashtable();
        param.Add("@rid", rid);
 

        return_ht = I_DBL.RunParam_SQL("select * from ZZZ_C_record_sub where  rid=@rid ", "数据记录", param);

        if ((bool)(return_ht["return_float"]))
        {
            DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();

            for (int i = 0; i < redb.Rows.Count; i++)
            {
                ArrayList al = get_sql_str(redb.Rows[i]["r_chu"].ToString(), redb.Rows[i]["r_ru"].ToString(), redb.Rows[i]["r_cpbh"].ToString(), redb.Rows[i]["r_pihao"].ToString(), redb.Rows[i]["r_shuliang"].ToString(), redb.Rows[i]["r_danwei"].ToString(), sp);
                if (al[0].ToString() == "ok")
                {
                    alre[0] = "ok";
                    al.RemoveAt(0);
                    alre.AddRange(al);
                }
                else
                {
                    alre[0] = al[0].ToString();
                    alre[1] = al[1].ToString();
                    return alre;
                }
            }
            return alre;

        }
        else
        {
            alre[0] = "err";
            alre[1] = "获取错误z";
            return alre;
        }
    }


    /// <summary>
    /// 获取更改库存的sql语句
    /// </summary>
    /// <param name="r_chu">出库库位号</param>
    /// <param name="r_ru">入库库位号</param>
    /// <param name="r_cpbh">产品编号</param>
    /// <param name="r_pihao">品号</param>
    /// <param name="r_shuliang">数量</param>
    /// <param name="r_danwei">单位</param>
    /// <param name="sp">特殊配置。标准,带参数</param>
    /// <returns>索引0 如果是ok，从索引1开始就是需要执行的语句。 如果是err，索引1就是错误提示。</returns>
    public ArrayList get_sql_str(string r_chu,string r_ru,string r_cpbh,string r_pihao,string r_shuliang,string r_danwei,string sp)
    {
        ArrayList alre = new ArrayList();
        alre.Add("err"); //默认提示
        alre.Add("");
        //如果有 出库库位
        if (r_chu != null && r_chu.Trim() != "")
        {
            Decimal[] nowKC = getnow_kucun(r_chu, r_cpbh, r_pihao, r_danwei,"");

            //if (nowKC[1] < 1)
            //{
            //    alre[0] = "err";
            //    alre[1] = "库存不足";
            //}
            if (nowKC[0] == -1)
            {
                alre[0] = "err";
                alre[1] = "检查库存发生错误c";
            }

            if (nowKC[0] == 0)
            {
                alre[0] = "ok";
                if (sp == "带参数")
                {
                    alre.Add("insert into ZZZ_C_Inventory (iid,i_dpid, i_pid, kucun, danwei,pihao) VALUES (newid(),@r_chu,@r_cpbh,-@r_shuliang,@r_danwei,@r_pihao)");
                }
                else
                {
                    alre.Add("insert into ZZZ_C_Inventory (iid,i_dpid, i_pid, kucun, danwei,pihao) VALUES (newid(),'"+ r_chu + "','"+ r_cpbh + "',-"+ r_shuliang + ",'"+ r_danwei + "','"+ r_pihao + "')");
                }
     
            }
            if (nowKC[0] == 1)
            {
                alre[0] = "ok";
                if (sp == "带参数")
                {
                    alre.Add("update ZZZ_C_Inventory set kucun = kucun - @r_shuliang where i_dpid=@r_chu and i_pid=@r_cpbh and danwei=@r_danwei  and pihao=@r_pihao");
                }
                else
                {
                    alre.Add("update ZZZ_C_Inventory set kucun = kucun - "+ r_shuliang + " where i_dpid='"+ r_chu + "' and i_pid='"+ r_cpbh + "' and danwei='"+ r_danwei + "'  and pihao='"+ r_pihao + "'");
                }
                    
            }
        }

        //如果有 入库库位
        if (r_ru != null && r_ru.Trim() != "")
        {
            Decimal[] nowKC = getnow_kucun(r_ru, r_cpbh, r_pihao, r_danwei, "");
            if (nowKC[0] == -1)
            {
                alre[0] = "err";
                alre[1] = "检查库存发生错误r";
            }

            if (nowKC[0] == 0)
            {
                alre[0] = "ok";
                if (sp == "带参数")
                {
                    alre.Add("insert into ZZZ_C_Inventory (iid,i_dpid, i_pid, kucun, danwei,pihao) VALUES (newid(),@r_ru,@r_cpbh,@r_shuliang,@r_danwei,@r_pihao)");
                }
                else
                {
                    alre.Add("insert into ZZZ_C_Inventory (iid,i_dpid, i_pid, kucun, danwei,pihao) VALUES (newid(),'" + r_ru + "','" + r_cpbh + "'," + r_shuliang + ",'" + r_danwei + "','" + r_pihao + "')");
                }
                   
            }
            if (nowKC[0] == 1)
            {
                alre[0] = "ok";
                if (sp == "带参数")
                {
                    alre.Add("update ZZZ_C_Inventory set kucun = kucun + @r_shuliang where i_dpid=@r_ru and i_pid=@r_cpbh and danwei=@r_danwei and pihao=@r_pihao");
                }
                else
                {
                    alre.Add("update ZZZ_C_Inventory set kucun = kucun + " + r_shuliang + " where i_dpid='" + r_ru + "' and i_pid='" + r_cpbh + "' and danwei='" + r_danwei + "'  and pihao='" + r_pihao + "'");

                }
                  
            }
        }

        return alre;
    }
}