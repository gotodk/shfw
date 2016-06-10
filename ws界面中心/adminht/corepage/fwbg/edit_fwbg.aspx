<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_fwbg.aspx.cs" Inherits="fwbh_edit_fwbg" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">

       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />
         <!-- 某些字段，在编辑时禁用，不想用新页面的情况使用 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 if (getUrlParam("fff") == "1") {

                      
                 }

                 window.setInterval(function () {

                     //设置子表输入框只读
                     $("#gview_grid-table-subtable-160427000664").find("input[id^='subtcid_']").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[id^='自动生成']").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='运转时间']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='备注']").removeAttr("readonly");
 
                 }, 500);



                 //设备序列号子表弹窗

                 var dfx_str_subsbxlh = "#show_searchopenyhbspgogo_subtcid_sb_SID";
                 var oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();

                 var jiancha_subsbxlh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subsbxlh).text() != oldzhi_subsbxlh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subsbxlh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[设备型号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[ERP编号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='ERP编号']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();
                     }
                 }, 500);















                 var dfx_str_gldh = "#show_searchopenyhbspgogo_G_BID";
                 var oldzhi_gldh = $(dfx_str_gldh).text();

                 var jiancha_gldh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_gldh).text() != oldzhi_gldh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_gldh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户编号")
                                 { $("#G_YYID").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[申报时间")
                                 { $("#Gsbtime").val($.trim(arr_z[1]).replace("]", "").split(' ')[0]); }

                             }
                         }

                         oldzhi_gldh = $(dfx_str_gldh).text();
                     }
                 }, 500);







                 var dfx_str_khbh = "#show_searchopenyhbspgogo_G_YYID";
                 var oldzhi_khbh = $(dfx_str_khbh).text();

                 var jiancha_khbh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_khbh).text() != oldzhi_khbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_khbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }


                             }
                         }

                         oldzhi_khbh = $(dfx_str_khbh).text();
                     }
                 }, 500);




 
        });
        </script>
</asp:Content>

