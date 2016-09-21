using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class jqgirdjs_for_subtable : System.Web.UI.Page
{

 

    protected void Page_Load(object sender, EventArgs e)
    {
        string rehtml = "";
        object[] re_dsi = IPC.Call("获取弹窗中列表配置", new object[] { Request["guid"].ToString() });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet ds_DD = (DataSet)(re_dsi[1]);
            string jsmod = File.ReadAllText(Server.MapPath("/pucu/jqgirdjs_for_subtable_mod.txt").ToString());

            //根据模板和配置数据，生成js代码
            rehtml = jsmod;

            //复合表头
            rehtml = rehtml.Replace("[*[FS_D_setGroupHeaders]*]", ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_setGroupHeaders"].ToString());

            //自适应宽度
            if (ds_DD.Tables["字段配置主表"].Rows[0]["FS_D_shrinkToFit"].ToString() == "true")
            { rehtml = rehtml.Replace("[*[FS_D_shrinkToFit]*]", "true"); }
            else
            { rehtml = rehtml.Replace("[*[FS_D_shrinkToFit]*]", "false"); }

            //分页可选量
            if (ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_pagesize"].ToString().Trim() == "")
            { rehtml = rehtml.Replace("[*[FD_D_pagesize]*]", "25,50,100");
                rehtml = rehtml.Replace("[*[Default_FD_D_pagesize]*]", "25");
            }
            else
            { rehtml = rehtml.Replace("[*[FD_D_pagesize]*]", ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_pagesize"].ToString());
                rehtml = rehtml.Replace("[*[Default_FD_D_pagesize]*]", ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_pagesize"].ToString().Split(',')[0]);
            }

        
            rehtml = rehtml.Replace("[*[FSID]*]", ds_DD.Tables["字段配置主表"].Rows[0]["FSID"].ToString());

 
 
            rehtml = rehtml.Replace("[*[FS_title]*]", ds_DD.Tables["字段配置主表"].Rows[0]["FS_title"].ToString());


            //定义合计需要的变量，准备替换
            bool needsum = false;//是否需要合计
            bool beshowhejistr = false; //是否已找到适合显示合计两个字的列
            List<String> alop = new List<String>();//存储配置
            //列配置
            string c_str = "";
            //特殊处理第一列
            //因为第一列在自带查看里不显示，所以要显示编号需要额外弄一列(这一列在sql取数据时一定要有)
            c_str = c_str + " { name: '隐藏编号', xmlmap: 'jqgird_spid', hidden: true,editable:false }, " + Environment.NewLine;
       //     c_str = c_str + @"{
       //     name: 'myac',index: '', width: 80, fixed:true, sortable: false, resize: false,
							//formatter: 'actions', 
							//formatoptions:
       //         {
       //         keys: true,
							//	//delbutton: false,//disable delete button
								
							//	delOptions: { recreateForm: true, beforeShowForm: beforeDeleteCallback},
							//	//editformbutton:true, editOptions:{recreateForm: true, beforeShowForm:beforeEditCallback}
							//}
       //     },";

            for (int i = 0; i < ds_DD.Tables["弹窗配置子表"].Rows.Count; i++)
            {
                DataRow dr = ds_DD.Tables["弹窗配置子表"].Rows[i];
                string DID_edit_editable = dr["DID_edit_editable"].ToString();
                string DID_edit_required = dr["DID_edit_required"].ToString();
                string DID_edit_ftype = dr["DID_edit_ftype"].ToString();
                string DID_edit_spset = dr["DID_edit_spset"].ToString();

                string DID_name = dr["DID_name"].ToString();
                string DID_showname = dr["DID_showname"].ToString();

                if (dr["DID_hide"].ToString() == "false" && !beshowhejistr)
                {
                    alop.Add("'" + DID_showname + "':'合计：'");
                    beshowhejistr = true;
                }


                switch (dr["DID_formatter"].ToString())
                    {
                        case "字符串":
 
                        if (dr["DID_name"].ToString() == ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_key"].ToString())
                        {
                            DID_edit_editable = "false";
                        }
                        string edittype_custom = " ";
                        if (dr["DID_edit_ftype"].ToString() == "下拉框")
                        {
                            string[] epzhi = DID_edit_spset.Split(',');
                            string epzhi_str = "";
                            for (int p = 0; p < epzhi.Length; p++)
                            {
                                 
                                    if (epzhi[p].IndexOf('|') >= 0)
                                    {
                                        epzhi_str = epzhi_str + epzhi[p].Split('|')[1].Trim() + ":" + epzhi[p].Split('|')[0].Trim() + ";";
                                    }
                                    else
                                    {
                                        epzhi_str = epzhi_str + epzhi[p].Split('|')[0].Trim() + ":" + epzhi[p].Split('|')[0].Trim() + ";";
                                    }
                               


                            }
                            epzhi_str = epzhi_str.TrimEnd(';');
                            edittype_custom = ",edittype: 'select', editoptions: { value: '"+ epzhi_str + "' }";
                        }

                        if (dr["DID_edit_ftype"].ToString() == "弹窗选择")
                        {
                            //根据DID_edit_spset，读取隐藏的字段表数据。
                            object[] re_dsi_yctc = IPC.Call("获取弹窗中列表配置", new object[] { DID_edit_spset });
                            if (re_dsi_yctc[0].ToString() == "ok")
                            {
                                //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
                                DataSet ds_DD_yctc = (DataSet)(re_dsi_yctc[1]);
                                string s_FS_D_yinruzhi = ds_DD_yctc.Tables["字段配置主表"].Rows[0]["FS_D_yinruzhi"].ToString();
                                string FS_SPPZ_readonly = ds_DD_yctc.Tables["字段配置主表"].Rows[0]["FS_SPPZ_readonly"].ToString();
                              

                                edittype_custom = ",edittype: 'custom', editoptions: { custom_element: subtab_tanchuang_elem, custom_value: subtab_tanchuang_value, DIDname:'" + DID_name + "', DIDshowname:'" + DID_showname + "',DIDeditspset:'" + DID_edit_spset + "',s_FS_D_yinruzhi:'" + s_FS_D_yinruzhi + "',FS_SPPZ_readonly:'" + FS_SPPZ_readonly + "'}";
                            }
                            

                         
                        }


                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " ,editable:"+ DID_edit_editable + " "+ edittype_custom + ",editrules: {required: " + DID_edit_required + "} }, " + Environment.NewLine;
                            break;
                        case "链接":
                 
                        if (dr["DID_name"].ToString() == ds_DD.Tables["字段配置主表"].Rows[0]["FD_D_key"].ToString())
                        {
                            DID_edit_editable = "false";
                        }
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'showlink', formatoptions: { baseLinkUrl: '" + dr["DID_formatter_CS"].ToString().Split('|')[0] + "', target: '_blank', showAction: '', addParam: '" + dr["DID_formatter_CS"].ToString().Split('|')[1] + "', idName: '" + dr["DID_formatter_CS"].ToString().Split('|')[2] + "' } ,editable:" + DID_edit_editable + ",editrules: {required: " + DID_edit_required + "} }, " + Environment.NewLine;
                            break;
                        case "整数":
                    
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'integer' ,editable:"+ DID_edit_editable + ",edittype: 'custom', editoptions: { custom_element: subtab_spinner_elem, custom_value: subtab_spinner_value},editrules: {required: " + DID_edit_required + ",integer:true } }, " + Environment.NewLine;

                        alop.Add("'" + dr["DID_showname"].ToString() + "':$(this).getCol('" + dr["DID_showname"].ToString() + "',false,'sum')");
                        needsum = true;

                        break;
                        case "小数":
          
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'number' ,editable:" + DID_edit_editable + " ,editrules: {required: " + DID_edit_required + ",custom:true, custom_func:ck_erweixiaoshu } }, " + Environment.NewLine;

                        alop.Add("'" + dr["DID_showname"].ToString() + "':$(this).getCol('" + dr["DID_showname"].ToString() + "',false,'sum')");
                        needsum = true;

                        break;
                        case "日期时间":
             
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'date', formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d H:i:s' },editable:" + DID_edit_editable + ",editrules: {required: " + DID_edit_required + "}  }, " + Environment.NewLine;
                            break;
                        case "仅日期":
                
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'date', formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } ,editable:"+ DID_edit_editable + ",edittype: 'custom', editoptions: { custom_element: subtab_datepicker_elem, custom_value: subtab_datepicker_value},editrules: {required: " + DID_edit_required + "}   }, " + Environment.NewLine;
                            break;
                        case "图片":
                             
                            break;
                        case "自定义":
                   
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: false,hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + ","+ dr["DID_formatter_CS"].ToString() + "  ,editable:" + DID_edit_editable + " }, " + Environment.NewLine;
                        break;

                        default:
                             //正常走不到这里，走到了就是数据库配置错了
                            break;
                    }
                
               
            }

            rehtml = rehtml.Replace("[*[SubDialog]*]", c_str.TrimEnd(','));

            if (needsum)
            {

                string js = "$(this).footerData('set',{ " + string.Join(",", alop.ToArray()) + " });";
                rehtml = rehtml.Replace("[*[needsum]*]", "true").Replace("[*[footerData_js]*]", js);

            }
            else
            {
                rehtml = rehtml.Replace("[*[needsum]*]", "false").Replace("[*[footerData_js]*]", " ");
            }


            //替换表格id
            rehtml = rehtml.Replace("[*[grid_selector_ID]*]", "#"+Request["grid_selector_ID"].ToString());
            rehtml = rehtml.Replace("[*[pager_selector_ID]*]", "#" + Request["pager_selector_ID"].ToString());


        }
        else
        {
            rehtml = "alert('获取弹窗配置失败:"+ re_dsi[1].ToString() + "')";
        }


        Response.Write(rehtml);
    }
}