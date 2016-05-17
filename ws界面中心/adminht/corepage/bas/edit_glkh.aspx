<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_glkh.aspx.cs" Inherits="bas_edit_glkh" %>

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
        <!-- 选择产品编号后，自动带入一些数据 -->
    <script type="text/javascript">
             jQuery(function ($) {
                
                 var dfx_str = "#show_searchopenyhbspgogo_UAid";
                 var oldzhi = $(dfx_str).text();
                 var jiancha_UAid = window.setInterval(function () {
             
                     if ($(dfx_str).text() != oldzhi)
                     {
                         var re =/\[.*?\]/ig;
                         var arr = $(dfx_str).text().match(re);
                 
                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[姓名")
                                 { $("#xingming").val($.trim(arr_z[1]).replace("]", "")); }
                           
                             }
                         }
                         
                         oldzhi = $(dfx_str).text();
                     }
                 }, 500);




                 var dfx_str_kh = "#show_searchopenyhbspgogo_YYID";
                 var oldzhi_kh = $(dfx_str_kh).text();
                 var jiancha_YYID = window.setInterval(function () {

                     if ($(dfx_str_kh).text() != oldzhi_kh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_kh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }

                             }
                         }

                         oldzhi_kh = $(dfx_str_kh).text();
                     }
                 }, 500);
 
        });
        </script>
</asp:Content>

