<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="list_record_main.aspx.cs" Inherits="lpjgl_list_record_main" %>

 
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


                <!-- **********其他附加脚本，用于实现个性化功能******** -->
    <script type="text/javascript">
        jQuery(function ($) {
            //根据穿入值，动态改变查询参数
            var cs_name = "rdb";
            var cs = getUrlParam(cs_name);
            if (cs != null && cs != "")
            {
                if (cs == "diaobo")
                { cs = "调拨单";}
                if (cs == "shenqing")
                { cs = "申请单"; }
                if (cs == "tuihui")
                { cs = "退回单"; }
                if (cs == "tiaozheng")
                { cs = "调整单"; }
                $('#' + cs_name).val(cs);
            }

       

        });
    </script>

</asp:Content>

