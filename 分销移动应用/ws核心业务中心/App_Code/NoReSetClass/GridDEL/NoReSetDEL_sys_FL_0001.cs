using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_sys_FL_0001
{
 
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_DEL(DataTable parameter_forUI)
        {

        //接收转换参数
        Hashtable ht_forUI = new Hashtable();
        for (int i = 0; i < parameter_forUI.Rows.Count; i++)
        {
            ht_forUI[parameter_forUI.Rows[i]["参数名"].ToString()] = parameter_forUI.Rows[i]["参数值"].ToString();
        }


        //存在有效目标才删除
        if (ht_forUI.Contains("ajaxrun") && ht_forUI["ajaxrun"].ToString() == "del" && ht_forUI.Contains("oper") && ht_forUI["oper"].ToString() == "del" && ht_forUI.Contains("id") && ht_forUI["id"].ToString().Trim() != "")
        {
            //开始真正的处理，这里只是演示，所以直接在这里写业务逻辑代码了

            I_Dblink I_DBL = (new DBFactory()).DbLinkSqlMain("");

            Hashtable param = new Hashtable();

            Hashtable return_ht = new Hashtable();
            ArrayList alsql = new ArrayList();


            //删除数据表里的数据 
            string[] delids = ht_forUI["id"].ToString().Split(',');
            for (int d = 0; d < delids.Length; d++)
            {
                //sys开头的，不允许删除。为了防止意外导致系统出错。
                if(delids[d].IndexOf("sys_") < 0)
                {
                    param.Add("@FSID_" + d, delids[d]);

                    alsql.Add("delete FUP_FormsList  where FSID=@FSID_" + d);
                    alsql.Add("delete FUP_FormsList_field  where DID_FSID=@FSID_" + d);
                    alsql.Add("delete FUP_FormsList_user_buju  where fsid=@FSID_" + d);
                }
              
            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                ;
            }
            else
            {
                ;
            }
        }


        return "";
        }




    /// <summary>
    /// 自定义按钮处理
    /// </summary>
    /// <param name="parameter_forUI">前台表单传来的参数</param>
    /// <returns></returns>
    public string NRS_ZDY_kelong(DataTable parameter_forUI)
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


            //克隆数据表里的数据 
            string[] ids = ht_forUI["xuanzhongzhi"].ToString().Split(',');
            for (int d = 0; d < ids.Length; d++)
            {
                if (ids[d].Trim() != "")
                {
                    //克隆主表
                    string oldid = ids[d];
                    string guid = CombGuid.GetMewIdFormSequence("FUP_FormsList");
                    alsql.Add("insert into FUP_FormsList select '"+ guid + "' as FSID, FS_ok, FS_type, '复制自_'+FS_name+'_'+ CONVERT(nvarchar(20), datepart(ms,getdate()) ) as FS_name, FS_getJK, FS_delJK, FS_del_show, FS_can_download, FS_add_show,   FS_add_show_link, FS_zdy_op, FS_D_shrinkToFit, FS_D_setGroupHeaders, FS_D_field, FS_D_datatable, FS_D_where,  FS_D_order, FD_D_key, FD_D_pagesize, SRE_open, SRE_showname_1, SRE_idname_1, SRE_type_1,  SRE_showname_2, SRE_idname_2, SRE_type_2, SRE_showname_3, SRE_idname_3, SRE_type_3 from FUP_FormsList where FSID='" + oldid + "'");
                    //取出子表并重新插入
                    Hashtable HTsub = I_DBL.RunParam_SQL("select DID from FUP_FormsList_field where DID_FSID='" + oldid + "' ", "数据记录", param);
                    DataTable DTsub = ((DataSet)HTsub["return_ds"]).Tables["数据记录"].Copy();
                    for (int i = 0; i < DTsub.Rows.Count; i++)
                    {
                        string guid_sub = CombGuid.GetMewIdFormSequence("FUP_FormsList_field");
                        alsql.Add("insert into FUP_FormsList_field select '"+ guid_sub + "' as DID, '"+ guid + "' as DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,    DID_frozen, DID_formatter, DID_formatter_CS  from FUP_FormsList_field where DID='" + DTsub.Rows[i]["DID"].ToString() + "'");
                    }
                }

            }


            return_ht = I_DBL.RunParam_SQL(alsql, param);


            if ((bool)(return_ht["return_float"]))
            {

                return "批量克隆完成！";
            }

        }



        return "批量克隆失败，发生错误";
    }


}
 
