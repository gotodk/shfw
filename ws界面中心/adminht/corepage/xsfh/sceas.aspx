<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="sceas.aspx.cs" Inherits="xsfh_sceas" %>

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

            if (getUrlParam("showinfo") == "1")
            {
                //强制关闭
                window.opener = null; window.open('', '_self'); window.close();
            }

                 window.setInterval(function () {
                     $(".ui-pg-table .navtable").hide();
                     $("#gview_grid-table-subtable-160923000244").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160923000244").find("input[name='设备档案序列号']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160923000244").find("input[name='单价']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160923000244").find("input[name='单价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160923000244").find("input[name='发货数量']").val();
                     $("#gview_grid-table-subtable-160923000244").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));
                 }, 500);
                 $("#addbutton1").html($("#addbutton1").html().replace("保存", "确认生成"));
                 $("#addbutton1_top").html($("#addbutton1_top").html().replace("保存", "确认生成"));
                 $("#title_f_id").html($("#title_f_id").html().replace("修改--", ""));


             
 
        });
        </script>
 
</asp:Content>

