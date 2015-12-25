using FMipcClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pucu_jqgirdjs_for_grid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rehtml = "";
        object[] re_dsi = IPC.Call("获取通用数据列表配置", new object[] { Request["guid"].ToString() });
        if (re_dsi[0].ToString() == "ok")
        {
            //这个就是得到远程方法真正的返回值，不同类型的，自行进行强制转换即可。
            DataSet ds_DD = (DataSet)(re_dsi[1]);
            string jsmod = File.ReadAllText(Server.MapPath("/pucu/jqgirdjs_for_grid_mod.txt").ToString());

            //根据模板和配置数据，生成js代码
            rehtml = jsmod;

            //复合表头
            rehtml = rehtml.Replace("[*[FS_D_setGroupHeaders]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_setGroupHeaders"].ToString());

            //自适应宽度
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_D_shrinkToFit"].ToString() == "true")
            { rehtml = rehtml.Replace("[*[FS_D_shrinkToFit]*]", "true"); }
            else
            { rehtml = rehtml.Replace("[*[FS_D_shrinkToFit]*]", "false"); }

            //分页可选量
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FD_D_pagesize"].ToString().Trim() == "")
            { rehtml = rehtml.Replace("[*[FD_D_pagesize]*]", "25,50,100");
                rehtml = rehtml.Replace("[*[Default_FD_D_pagesize]*]", "25");
            }
            else
            { rehtml = rehtml.Replace("[*[FD_D_pagesize]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FD_D_pagesize"].ToString());
                rehtml = rehtml.Replace("[*[Default_FD_D_pagesize]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FD_D_pagesize"].ToString().Split(',')[0]);
            }

            //是否显示删除按钮
            rehtml = rehtml.Replace("[*[FS_del_show]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FS_del_show"].ToString());
            //本列表的配置主键（用于删除标记传递）
            rehtml = rehtml.Replace("[*[FSID]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FSID"].ToString());

            //是否显示导出按钮(连带打印)
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_can_download"].ToString() == "1")
            {
                rehtml = rehtml.Replace("[*[FS_can_download]*]", "$('.bz-zheshidaochu').show();$('.bz-zheshidaochu').show();");
            }
            else
            {
                rehtml = rehtml.Replace("[*[FS_can_download]*]", "$('.bz-zheshidaochu').hide();$('.bz-zheshidaochu').hide();");
            }

            //是否显示新增按钮
            rehtml = rehtml.Replace("[*[FS_add_show_link]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FS_add_show_link"].ToString());
            if (ds_DD.Tables["报表配置主表"].Rows[0]["FS_add_show"].ToString() == "1")
            {
                rehtml = rehtml.Replace("[*[FS_add_show]*]", "$('.bz-zheshixinzeng').show();");
            }
            else
            {
                rehtml = rehtml.Replace("[*[FS_add_show]*]", "$('.bz-zheshixinzeng').hide();");
            }


            //处理自定义按钮
            string zdy_tihuan_str = "";
            string jsmod_zdy_op = File.ReadAllText(Server.MapPath("/pucu/jqgirdjs_for_grid_mod_zdy_op.txt").ToString());
            string zdy_op = ds_DD.Tables["报表配置主表"].Rows[0]["FS_zdy_op"].ToString();
            string[] zdy_op_arr = zdy_op.Split(',');
            for (int op = 0; op < zdy_op_arr.Length; op++)
            {
                string tempanniu = zdy_op_arr[op];
                if (tempanniu.Trim() != "")
                {
                    string[] tp = tempanniu.Split('|');
                    string anniustr = jsmod_zdy_op.Replace("[*[zdyop_title]*]", tp[0]);
                    anniustr = anniustr.Replace("[*[zdyop_buttonicon]*]", tp[1]);
                    anniustr = anniustr.Replace("[*[zdyop_zdyname]*]", tp[2]);
                    anniustr = anniustr.Replace("[*[FSID]*]", ds_DD.Tables["报表配置主表"].Rows[0]["FSID"].ToString());
            
                    zdy_tihuan_str = zdy_tihuan_str + anniustr;
                }
            }
            rehtml = rehtml.Replace("[*[FS_zxy_op_tihuan]*]", zdy_tihuan_str);

            //列配置
            string c_str = "";
            //特殊处理第一列
            //因为第一列在自带查看里不显示，所以要显示编号需要额外弄一列(这一列在sql取数据时一定要有)
            c_str = c_str + " { name: '隐藏编号', xmlmap: 'jqgird_spid', hidden: true,frozen:true } ," + Environment.NewLine;
            for (int i = 0; i < ds_DD.Tables["字段配置子表"].Rows.Count; i++)
            {
                DataRow dr = ds_DD.Tables["字段配置子表"].Rows[i];
 
                    switch (dr["DID_formatter"].ToString())
                    {
                        case "字符串":
                            c_str = c_str + " { name: '"+ dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + "  }, " + Environment.NewLine;
                            break;
                        case "链接":
                        string target_str = "";
                        if (dr["DID_formatter_CS"].ToString().Split('|').Count() < 4)
                        {
                            target_str = "_top";
                        }
                        else
                        {
                            target_str = dr["DID_formatter_CS"].ToString().Split('|')[3];
                        }

                            c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'showlink', formatoptions: { baseLinkUrl: '" + dr["DID_formatter_CS"].ToString().Split('|')[0] + "', target: '"+ target_str + "', showAction: '', addParam: '" + dr["DID_formatter_CS"].ToString().Split('|')[1] + "', idName: '" + dr["DID_formatter_CS"].ToString().Split('|')[2] + "' } }, " + Environment.NewLine;
                            break;
                        case "整数":
                            c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'integer' }, " + Environment.NewLine;
                            break;
                        case "小数":
                            c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'number' }, " + Environment.NewLine;
                            break;
                        case "日期时间":
                            c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'date', formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d H:i:s' } }, " + Environment.NewLine;
                            break;
                        case "仅日期":
                            c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + " , formatter: 'date', formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d' }, searchoptions: {dataInit:datePick }  }," + Environment.NewLine;
                            break;
                        case "自定义":
                        c_str = c_str + " { name: '" + dr["DID_showname"].ToString() + "', xmlmap: '" + dr["DID_name"].ToString() + "', index: '" + dr["DID_name"].ToString() + "', width: " + dr["DID_width"].ToString() + ", fixed: " + dr["DID_fixed"].ToString() + ", sortable: " + dr["DID_sortable"].ToString() + ",hidden: " + dr["DID_hide"].ToString() + ",frozen:" + dr["DID_frozen"].ToString() + ","+ dr["DID_formatter_CS"].ToString() + "   }, " + Environment.NewLine;
                        break;

                        default:
                             //正常走不到这里，走到了就是数据库配置错了
                            break;
                    }
                
               
            }
            rehtml = rehtml.Replace("[*[SubDialog]*]", c_str.TrimEnd(','));
            


        }
        else
        {
            rehtml = "alert('获取报表配置失败:"+ re_dsi[1].ToString() + "')";
        }


        Response.Write(rehtml);
    }
}