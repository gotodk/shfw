<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_record_diaobo_edit.aspx.cs" Inherits="lpjgl_edit_record_diaobo_edit" %>

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
                  


                 //把业务类型参数放到隐藏控件里面
                 //
                 window.setInterval(function () {


                     //设置子表输入框只读
                     $("#gview_grid-table-subtable-160616000940").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160616000940").find("input[name='调整数量']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160616000940").find("input[name='批号']").removeAttr("readonly");
                 
                 }, 500);

                 //业务处理

                 if (getUrlParam("fff") == "1") {



                 }
                 else {
                

                 }


           


                 //出库子表弹窗

                 var dfx_str_subchu = "#show_searchopenyhbspgogo_subtcid_r_chu";
                 var oldzhi_subchu = $(dfx_str_subchu).text();

                 var jiancha_subchu = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subchu).text() != oldzhi_subchu) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subchu).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[库位名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subchu).closest("tr").find("input[name='出库库位名']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[仓库名称") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subchu).closest("tr").find("input[name='出库仓库名']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subchu = $(dfx_str_subchu).text();
                     }
                 }, 500);





                 //入库子表弹窗

                 var dfx_str_subru = "#show_searchopenyhbspgogo_subtcid_r_ru";
                 var oldzhi_subru = $(dfx_str_subru).text();

                 var jiancha_subru = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subru).text() != oldzhi_subru) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subru).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[库位名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subru).closest("tr").find("input[name='入库库位名']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[仓库名称") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subru).closest("tr").find("input[name='入库仓库名']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subru = $(dfx_str_subru).text();
                     }
                 }, 500);







                 //入库主表弹窗

                 var dfx_str_subruzhubiao = "#show_searchopenyhbspgogo_yincangkucun";
                 var oldzhi_subruzhubiao = $(dfx_str_subruzhubiao).text();

                 var jiancha_subruzhubiao = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subruzhubiao).text() != oldzhi_subruzhubiao) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subruzhubiao).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[库位名称") {
                                     $("#t_rukukuwei").val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[仓库名称") {
                                     $("#t_rukucangku").val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subruzhubiao = $(dfx_str_subruzhubiao).text();
                     }
                 }, 500);






                 //选择品号的子表弹窗

                 var dfx_str_subtt = "#show_searchopenyhbspgogo_subtcid_r_cpbh";
                 var oldzhi_subtt = $(dfx_str_subtt).text();

                 var jiancha_subtt = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subtt).text() != oldzhi_subtt) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subtt).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='零件名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[规格") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='零件规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[单位") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='单位']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subtt = $(dfx_str_subtt).text();
                     }
                 }, 500);


 
        });
        </script>
</asp:Content>

