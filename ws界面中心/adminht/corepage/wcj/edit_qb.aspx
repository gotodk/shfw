<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_qb.aspx.cs" Inherits="wcj_edit_qb" %>

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



                var nowloginuser = "<%=UserSession.唯一键%>";
            // 过滤自己的客户。 包括全客户关联设置、客户关联表、客户档案服务负责人
            $("#searchopenyhbspgogo_QB_YYID").attr("teshuwhere", " ( ( charindex(','+'" + nowloginuser + "'+',',(select top 1 ','+YSTR+',' from ZZZ_ZFCMJ where YID='tskhgl')) > 0 ) or (uucjlx='未成交') or ( YYID in (select YYID from ZZZ_userinfo_glkh where UAid='" + nowloginuser + "' and shixiaoriqi >= getdate()  UNION  select YYID from ZZZ_KHDA where YYfuwufuzeren='" + nowloginuser + "') ) )");


                
                 var dfx_str = "#show_searchopenyhbspgogo_QB_YYID";
                 var oldzhi = $(dfx_str).text();
                 var jiancha_cpbh = window.setInterval(function () {
             
                     if ($(dfx_str).text() != oldzhi)
                     {
                         var re =/\[.*?\]/ig;
                         var arr = $(dfx_str).text().match(re);
                 
                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#QBname").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[档案类型")
                                 { $("#QBleibie").val($.trim(arr_z[1]).replace("]", "")); }
                             }
                         }
                         
                         oldzhi = $(dfx_str).text();
                     }
                 }, 500); 
 
        });
        </script>
</asp:Content>

