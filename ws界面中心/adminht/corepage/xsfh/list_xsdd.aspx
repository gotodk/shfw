<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="list_xsdd.aspx.cs" Inherits="xsfh_list_xsdd" %>

 
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

    <!-- 自定义新增 -->
        <div id="dialog-message-test" class="hide">
        











<div class="row">
                                    <div class="col-sm-12">


              弹窗例子内容
                                    </div>
                                    <!-- /.col -->
                                </div>
                                <!-- /.row -->
















    </div>
    <!-- #dialog-message -->


    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content_onlygrid runat="server" ID="wuc_content_onlygrid" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_script" runat="Server">
    <uc1:wuc_script_onlygrid runat="server" ID="wuc_script_onlygrid" />
        <script type="text/javascript">
            jQuery(function ($) {
                window.setInterval(function () {
                    $("#clon_del_grid-table").hide();
                    $("#del_grid-table").hide();
                }, 500);
                   });
        </script>
     <!-- **********自定义新增脚本******** -->
     <script type="text/javascript">
         var dialog_tanchuang_zdy = null;
         function openeditdialoggogozdy(zdyID) {
             dialog_tanchuang_zdy = $("#dialog-message-test").removeClass('hide').dialog({
                 modal: true,
                 title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-check'></i> 操作</h4></div>",
                 width: '60%',
                 buttons: [
                     {
                         text: "  取消  ",
                         "class": "btn btn-xs",
                         click: function () {
                             $(this).dialog("close");
                         }
                     },
                     {
                         text: "  确认  ",
                         "class": "btn btn-primary btn-xs querenyinruanniu",
                         click: function () {
                             $(this).dialog("close");
                         }
                     }
                 ]
             });
         }
     </script>
                <!-- 强制添加列表特殊条件 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //
              var nowloginuser = "<%=UserSession.唯一键%>";
            // 过滤自己的客户。 包括全客户关联设置、客户关联表、客户档案服务负责人
                 $("#zheshiliebiaoquyu").attr("teshuwhere", " ( ( charindex(','+'" + nowloginuser + "'+',',(select top 1 ','+YSTR+',' from ZZZ_ZFCMJ where YID='tskhgl')) > 0 ) or ( H_YYID in (select YYID from ZZZ_userinfo_glkh where UAid='" + nowloginuser + "' and shixiaoriqi >= getdate()  UNION  select YYID from ZZZ_KHDA where YYfuwufuzeren='" + nowloginuser + "') ) )");
             
                
 
        });

        </script>
</asp:Content>

