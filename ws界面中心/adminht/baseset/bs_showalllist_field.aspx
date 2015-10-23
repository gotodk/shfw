<%@ Page Language="C#"  MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="bs_showalllist_field.aspx.cs" Inherits="adminht_baseset_bs_showalllist_field" %>

<%@ Register Src="~/pucu/wuc_css_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_css_onlygrid" %>
<%@ Register Src="~/pucu/wuc_content_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_content_onlygrid" %>
<%@ Register Src="~/pucu/wuc_script_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_script_onlygrid" %>





<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 附加的head内容 -->
 
    <uc1:wuc_css_onlygrid runat="server" ID="wuc_css_onlygrid" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_daohang" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content_onlygrid runat="server" ID="wuc_content_onlygrid" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_script" runat="Server">

    
        <!-- **********其他附加脚本，用于实现个性化功能******** -->
    <script type="text/javascript">
        jQuery(function ($) {
            //根据穿入值，动态改变查询参数
            var cs_name = "DID_FSID";
            var cs = getUrlParam(cs_name);
            if (cs != null && cs != "")
            {
                $('#' + cs_name).val(cs);
            }

       

        });
    </script>


    <uc1:wuc_script_onlygrid runat="server" ID="wuc_script_onlygrid" />








</asp:Content>