<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="qd_rili.aspx.cs" Inherits="qd_rili" %>

<%@ Register Src="~/adminht/corepage/qiandao/qd_WUC_forrili.ascx" TagPrefix="uc1" TagName="qd_WUC_forrili" %>
<%@ Register Src="~/adminht/corepage/qiandao/qd_WUC_forrili_js.ascx" TagPrefix="uc1" TagName="qd_WUC_forrili_js" %>



<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
 <link rel="stylesheet" href="/assets/css/fullcalendar.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 <uc1:qd_WUC_forrili runat="server" ID="qd_WUC_forrili" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
 <uc1:qd_WUC_forrili_js runat="server" ID="qd_WUC_forrili_js" />

</asp:Content>

