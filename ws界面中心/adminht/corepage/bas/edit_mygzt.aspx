<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_mygzt.aspx.cs" Inherits="bas_edit_mygzt" %>

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
                     

                     //$("#Mdizhi").closest(".form-group").hide();
                     //$("#Mbiaoti").closest(".form-group").hide();
                     
                     //解析左侧菜单的可用链接
                     $("#Mxuancaidan").empty();
                     $("#Mxuancaidan").append('<option value="">暂无快捷链接</option>');
                     var showstr = "";
                     $("#masterpageleftmenu1_menuUL a").each(function (a, b) {
                         var href = $(b).attr("href");
                     
                         if (typeof (href) != "undefined" && href != "") {
                             var showname = $.trim($(b).find("span").text());
                             $("#Mxuancaidan").append('<option value="' + href + '">'+showname+'</option>');
                             showstr = showstr + showname + ", ";
                         }
                        
                         
                        
                         
                     });
                     //alert(showstr);
                  


                     $('#Mxuancaidan').change(function () {
                         $("#Mbiaoti").val($(this).children('option:selected').text());
                         $("#Mdizhi").val( $(this).children('option:selected').val());
                     })
 
                   
                 }
 
        });
        </script>
</asp:Content>

