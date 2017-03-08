using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_160423000028
{

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_DEL(DataTable parameter_forUI)
    {
        

        return null;
    }




    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_genghuanren(DataTable parameter_forUI)
    {
        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //存在有效目标才删除
        if (ht_forUI.Contains("zdyname") && ht_forUI["xuanzhongzhi"].ToString() != "")
        {
            if (ht_forUI["xuanzhongzhi"].ToString().Trim() == "")
            {
                return "未选中任何要操作的数据。";
            }
            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            Hashtable param = new Hashtable();

            Hashtable return_ht = new Hashtable();
            ArrayList alsql = new ArrayList();


            //更新数据表里的数据 
            string[] ids = ht_forUI["xuanzhongzhi"].ToString().Split(',');
            string sql_in_GLKHID = "('十全大补丸'";
            
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    sql_in_GLKHID = sql_in_GLKHID + ",('"+ ids[d].Trim() + "')";
                }

            }
            sql_in_GLKHID = sql_in_GLKHID + ")";
            return_ht = I_DBL.RunParam_SQL("select Uaid,xingming from ZZZ_userinfo where Uaid in (select  UAid from ZZZ_userinfo_glkh where GLKHID in "+ sql_in_GLKHID + ")", "数据记录", param);
            if ((bool)(return_ht["return_float"]))
            {
                DataTable redb = ((DataSet)return_ht["return_ds"]).Tables["数据记录"].Copy();
                string showinfo = "";
                for (int i = 0; i < redb.Rows.Count; i++)
                {
                    showinfo = showinfo + ""+ redb.Rows[i]["xingming"].ToString() + "(" + redb.Rows[i]["Uaid"].ToString() + "),";
                }
                showinfo = "选中的来源员工为：<br/><span id='zhuanyilaiyuan'>" + showinfo + "</span><br/><hr/><button class='btn btn-danger btn-block' id='zhuanyi_gogo_dh' onclick='alert(\"此功能尚未实现，开发中……\")'>直接将选中的两个员工所辖客户进行调换</button><br/><hr/>将上述来源员工的客户转移至新员工<input   type='text' id='zhuanyimubiao' name='zhuanyimubiao' placeholder='请输入工号'   value='' />账号上。 <br/><span class='red'>来源员工名下客户将被清空，请谨慎操作，主要用于员工离职的批量客户转移！</span> <br/><br/><button class='btn btn-danger btn-block' id='zhuanyi_gogo' onclick='alert(\"此功能尚未实现，开发中……\")'>确认转移</button><br/> ";
                return  showinfo;
            }
            else
            {
                return "处理失败，获取转移来源员工失败，发生错误";
            }

 

        }



        return "处理失败，发生错误";
    }



}

