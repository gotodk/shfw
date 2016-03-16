using System;
using System.Collections.Generic;
using System.Web;
using FMDBHelperClass;
using FMipcClass;
using System.Collections;
using System.Data;
using FMPublicClass;
using System.Numerics;



public class NoReSetDEL_sys_FL_0003
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
                    param.Add("@FID_" + d, delids[d]);

                    alsql.Add("delete FUP_FormsMainInfo  where FID=@FID_" + d);
                    alsql.Add("delete FUP_FormsSubDialog  where DID_FSID in (select FSID from FUP_FormsSubInfo where FS_FID=@FID_" + d + ")");
                    alsql.Add("delete FUP_FormsSubInfo  where FS_FID=@FID_" + d);
              
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
                    string guid = CombGuid.GetMewIdFormSequence("FUP_FormsMainInfo");
                    alsql.Add("insert into FUP_FormsMainInfo select '" + guid + "' as FID, F_ok , '复制自_'+Fname+'_'+ CONVERT(nvarchar(20), datepart(ms,getdate()) ) as Fname, Ftype, Frun_add, Frun_edit, Frun_showinfo_foredit from FUP_FormsMainInfo where FID='" + oldid + "'");
                    //取出子表并重新插入
                    Hashtable HTsub = I_DBL.RunParam_SQL("select FSID from FUP_FormsSubInfo where FS_FID='" + oldid + "' ", "数据记录", param);
                    DataTable DTsub = ((DataSet)HTsub["return_ds"]).Tables["数据记录"].Copy();
                    for (int i = 0; i < DTsub.Rows.Count; i++)
                    {
                        string guid_sub = CombGuid.GetMewIdFormSequence("FUP_FormsSubInfo");
                        alsql.Add("insert into FUP_FormsSubInfo select '" + guid_sub + "' as FSID, '" + guid + "' as FS_FID, FS_ok, FS_type, FS_name, FS_title, FS_minlength, FS_maxlength, FS_defaultvalue, FS_tip_n,    FS_tip_w, FS_passnull, FS_nulltip, FS_index, FS_SPPZ_list_static, FS_SPPZ_mask, FS_SPPZ_readonly, FS_D_haveD,  FS_D_yinruzhi, FS_D_shrinkToFit, FS_D_setGroupHeaders, FS_D_field,  FS_D_datatable, FS_D_where, FS_D_order,    FD_D_key, FD_D_pagesize  from FUP_FormsSubInfo where  FSID='" + DTsub.Rows[i]["FSID"].ToString() + "'");


                        //取出弹窗并重新插入
                        Hashtable HTsub_dlg = I_DBL.RunParam_SQL("select DID from FUP_FormsSubDialog where DID_FSID='" + DTsub.Rows[i]["FSID"].ToString() + "' ", "数据记录", param);
                        DataTable DTsub_dlg = ((DataSet)HTsub_dlg["return_ds"]).Tables["数据记录"].Copy();
                        for (int dlg = 0; dlg < DTsub_dlg.Rows.Count; dlg++)
                        {
                            string guid_sub_dlg = CombGuid.GetMewIdFormSequence("FUP_FormsSubDialog");
                            alsql.Add("insert into FUP_FormsSubDialog select '" + guid_sub_dlg + "' as DID, '" + guid_sub + "' as DID_FSID, DID_ok, DID_px, DID_hide, DID_showname, DID_name, DID_width, DID_sortable, DID_fixed,    DID_frozen, DID_formatter, DID_formatter_CS, DID_edit_editable, DID_edit_required, DID_edit_ftype,  DID_edit_spset from FUP_FormsSubDialog  where DID ='" + DTsub_dlg.Rows[dlg]["DID"].ToString() + "'");
                        }

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
 
