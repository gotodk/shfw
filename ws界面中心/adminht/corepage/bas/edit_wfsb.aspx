<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_wfsb.aspx.cs" Inherits="bas_edit_wfsb" %>

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

        

                
                 var dfx_str = "#show_searchopenyhbspgogo_S_YYID";
                 var oldzhi = $(dfx_str).text();
                 var jiancha_UAid = window.setInterval(function () {
             
                     if ($(dfx_str).text() != oldzhi)
                     {
                         var re =/\[.*?\]/ig;
                         var arr = $(dfx_str).text().match(re);
                 
                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }
                           
                             }
                         }
                         
                         oldzhi = $(dfx_str).text();
                     }
                 }, 500);





                 var dfx_str_sb = "#show_searchopenyhbspgogo_S_SBID";
                 var oldzhi_sb = $(dfx_str_sb).text();
                 var jiancha_UAid_sb = window.setInterval(function () {

                     if ($(dfx_str_sb).text() != oldzhi_sb) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_sb).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备名称")
                                 { $("#Smingcheng").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[规格型号")
                                 { $("#Sxinghao").val($.trim(arr_z[1]).replace("]", "")); }
                  
                                     if (getUrlParam("fff") == "1") {

                                       
                                     }
                                     else {
                                         
                                         if (arr_z[0] == "[成本价")
                                         { $("#Schenbenjia").val($.trim(arr_z[1]).replace("]", "")); }
                                         if (arr_z[0] == "[保修期限")
                                         { $("#Sbaoxiuqixian").val($.trim(arr_z[1]).replace("]", "")); }
                                         if (arr_z[0] == "[保养周期")
                                         { $("#Sbaoyangzhouqi").val($.trim(arr_z[1]).replace("]", "")); }
                                     }
                               

                             }
                         }

                         oldzhi_sb = $(dfx_str_sb).text();
                     }
                 }, 500);


 

                 var jiancha_Sleixing = window.setInterval(function () {

                     //弹窗特殊条件

                     var str_Sleixing = $("#S_SBID").val();
                     $("#searchopenyhbspgogo_Sbanben").attr("teshuwhere", "BBB_SBID in (select SBID from ZZZ_SBLXBASE  where BBB_SBID='" + str_Sleixing + "')");

                 }, 500);

 
 
        });
        </script>
</asp:Content>

