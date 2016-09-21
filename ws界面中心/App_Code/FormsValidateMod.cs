using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// FormsValidateMod 的摘要说明
/// </summary>
public static class FormsValidateMod
{
    /// <summary>
    /// 验证控制的属性模板
    /// </summary>
    /// <returns></returns>
    private static string GetJS_mod_ziduan_pz()
    {
        return @"

required: [[是否必填]], 
minlength: [[最小长度]], 
maxlength: [[最大长度]], 
[[其他验证]] 
_xxxxxxxxxxxx: [''] 

";
   
    }

    private static string GetJS_for_ace_wysiwyg()
    {
        return @"

            //添加提交事件(编辑器)
            $(document).on('click', buttonid1, function () {
                if ($('#[[控件名]]').text().trim() != '') {
                    $('#[[控件名]]_html').val(encMe($('#[[控件名]]').html(), 'mima'));
                    $('#[[控件名]]_text').val(encMe($('#[[控件名]]').text(), 'mima'));
    }
                else {
                    $('#[[控件名]]_html').val('');
                    $('#[[控件名]]_text').val('');
}
            });

            //添加提交事件(编辑器)
            $(document).on('click', buttonid1+'_top', function () {
                if ($('#[[控件名]]').text().trim() != '') {
                    $('#[[控件名]]_html').val(encMe($('#[[控件名]]').html(), 'mima'));
                    $('#[[控件名]]_text').val(encMe($('#[[控件名]]').text(), 'mima'));
    }
                else {
                    $('#[[控件名]]_html').val('');
                    $('#[[控件名]]_text').val('');
}
            });

            $('#[[控件名]]').ace_wysiwyg(); 

";
    }


    /// <summary>
    /// 表单验证的字段模板
    /// </summary>
    /// <returns></returns>
    private static string GetJS_mod_ziduan()
    {
        return  @"

[[字段名]] : {
[[属性模板]] 
},

";
     
    }

    /// <summary>
    /// 根据类型重新定义字段名
    /// </summary>
    /// <param name="FS_name"></param>
    /// <param name="typename"></param>
    /// <returns></returns>
    private static string re_FS_ziduanming(string FS_name, string typename)
    {
        if (typename == "富文本框")
        {
            return FS_name + "_html";
        }
        if (typename == "日期区间框")
        {
            return FS_name + "1";
        }

        //原样返回
        return FS_name;
    }

    /// <summary>
    /// 获取最终的js代码
    /// </summary>
    /// <param name="dsFPZ">配置集合</param>
    /// <param name="othercheck">其他验证拓展</param>
    /// <returns></returns>
    public static string[] GetJS_fin(DataSet dsFPZ,Hashtable othercheck)
    {
        string[] strfin = new string[4] {"","","","" };
        for (int i = 0; i < dsFPZ.Tables["表单配置子表"].Rows.Count; i++)
        {
            DataRow drFPZ = dsFPZ.Tables["表单配置子表"].Rows[i];
            //=======================================================
            string mod_ziduan_00 = GetJS_mod_ziduan();
            mod_ziduan_00 = mod_ziduan_00.Replace("[[字段名]]", re_FS_ziduanming(drFPZ["FS_name"].ToString(), drFPZ["FS_type"].ToString()));

            string mod_pz_00 = GetJS_mod_ziduan_pz();
            if (drFPZ["FS_passnull"].ToString() == "1")
            { mod_pz_00 = mod_pz_00.Replace("[[是否必填]]", "true"); }
            else
            { mod_pz_00 = mod_pz_00.Replace("[[是否必填]]", "false"); }
            mod_pz_00 = mod_pz_00.Replace("[[最小长度]]", drFPZ["FS_minlength"].ToString());
            mod_pz_00 = mod_pz_00.Replace("[[最大长度]]", drFPZ["FS_maxlength"].ToString());

            if (drFPZ["FS_type"].ToString() == "两位小数")
            {
                if (drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "")
                {
                    mod_pz_00 = mod_pz_00.Replace("[[其他验证]]", "lrunlv_2: [''], " + Environment.NewLine + "[[其他验证]]");
                }
                else
                {
                    if (drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "1" || drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "2" || drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "3" || drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "4" || drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "5" || drFPZ["FS_SPPZ_list_static"].ToString().Trim() == "6")
                    {
                        mod_pz_00 = mod_pz_00.Replace("[[其他验证]]", "lrunlv_"+ drFPZ["FS_SPPZ_list_static"].ToString().Trim() + ": [''], " + Environment.NewLine + "[[其他验证]]");
                    }
                }
           
            }
            if (drFPZ["FS_type"].ToString() == "整数")
            {
                mod_pz_00 = mod_pz_00.Replace("[[其他验证]]", "lrunlv_zhengshu: [''], " + Environment.NewLine + "[[其他验证]]");
            }

            //拓展
            if (othercheck.ContainsKey(drFPZ["FS_name"].ToString()))
            {
                mod_pz_00 = mod_pz_00.Replace("[[其他验证]]", ((string[])(othercheck[drFPZ["FS_name"].ToString()]))[0]);
            }
            else
            {
                mod_pz_00 = mod_pz_00.Replace("[[其他验证]]", "");
            }



            mod_ziduan_00 = mod_ziduan_00.Replace("[[属性模板]]", mod_pz_00);



            //=======================================================
            string mod_ziduan_11 = GetJS_mod_ziduan();
            mod_ziduan_11 = mod_ziduan_11.Replace("[[字段名]]", re_FS_ziduanming(drFPZ["FS_name"].ToString(), drFPZ["FS_type"].ToString()));

            string mod_pz_11 = GetJS_mod_ziduan_pz();
            if (drFPZ["FS_nulltip"].ToString().Trim() == "")
            {
                mod_pz_11 = mod_pz_11.Replace("[[是否必填]]", "\"请输入"+ drFPZ["FS_title"].ToString() + "\"");
            }
            else
            {
                mod_pz_11 = mod_pz_11.Replace("[[是否必填]]", "\"" + drFPZ["FS_nulltip"].ToString() + "\"");
            }
            mod_pz_11 = mod_pz_11.Replace("[[最小长度]]", "$.validator.format(\"最少{0}个字符\")");
            mod_pz_11 = mod_pz_11.Replace("[[最大长度]]", "$.validator.format(\"最多{0}个字符\")");
            //拓展
            if (othercheck.ContainsKey(drFPZ["FS_name"].ToString()))
            {
                mod_pz_11 = mod_pz_11.Replace("[[其他验证]]", ((string[])(othercheck[drFPZ["FS_name"].ToString()]))[1]);
            }
            else
            {
                mod_pz_11 = mod_pz_11.Replace("[[其他验证]]", "");
            }

            mod_ziduan_11 = mod_ziduan_11.Replace("[[属性模板]]", mod_pz_11);
            //==================
            strfin[0] = strfin[0] + mod_ziduan_00;
            strfin[1] = strfin[1] + mod_ziduan_11;

            //==================
            //增加输入过滤
            if (drFPZ["FS_type"].ToString() == "输入框" && drFPZ["FS_SPPZ_mask"].ToString().Trim() != "")
            {
                strfin[2] = strfin[2] + " $('#"+ drFPZ["FS_name"].ToString() + "').mask('"+ drFPZ["FS_SPPZ_mask"].ToString() + "'); " + Environment.NewLine;
            }
            //增加数字录入控制
            if (drFPZ["FS_type"].ToString() == "整数")
            {
                strfin[2] = strfin[2] + " $('#" + drFPZ["FS_name"].ToString() + "').ace_spinner({ value: 0, min: " + drFPZ["FS_minlength"].ToString() + ", max: " + drFPZ["FS_maxlength"].ToString() + ", step: 1, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' }); " + Environment.NewLine;
            }
            if (drFPZ["FS_type"].ToString() == "两位小数")
            {
                strfin[2] = strfin[2] + " $('#" + drFPZ["FS_name"].ToString() + "').ace_spinner({ value: 0, min: " + drFPZ["FS_minlength"].ToString() + ", max: " + drFPZ["FS_maxlength"].ToString() + ", step: 0.01, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' }); " + Environment.NewLine;
            }
            //增加编辑器识别
            if (drFPZ["FS_type"].ToString() == "富文本框")
            {
                strfin[3] = strfin[3] + GetJS_for_ace_wysiwyg().Replace("[[控件名]]", drFPZ["FS_name"].ToString()) + Environment.NewLine;
            }
            //====================


        }
        strfin[0] = strfin[0] + " _xxxxxxxxxxxx: {} ";
        strfin[1] = strfin[1] + " _xxxxxxxxxxxx: {} ";

        return strfin;
    }

}