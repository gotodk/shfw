<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_xiaoliang.aspx.cs" Inherits="jwm_edit_xiaoliang" %>

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

        <script src="/assets/js/jquery.cookie.js"></script>
     <!-- 强制添加弹窗特殊条件 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //
                 var caozuoyuan = $.cookie('user_Uloginname_onlyforinput');
                 $("#searchopenyhbspgogo_Xyiyuan").attr("teshuwhere", "FUserName='" + caozuoyuan + "'");
                

                 $("#zdy_CZY").val(caozuoyuan);
 
        });
        </script>


              <!-- 特殊处理 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //隐藏打印
                 $("#dayinzheyiye_top").hide();
                
 
        });
        </script>
</asp:Content>

