﻿<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="list_wqdcstj.aspx.cs" Inherits="tongjifenxi_wqdcstj" %>

 
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
    <!-- 默认查询当天的数据 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //
                 
                 $("#suoshuquyu").val("129"); //默认售后管理部
                 $("#tsthcs_shijian1").datepicker('setDate', new Date());;
                 $("#tsthcs_shijian2").datepicker('setDate', new Date());;
 
        });

        </script>
                    <!-- 强制添加列表特殊条件 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //
                 var nowloginuser = "<%=UserSession.唯一键%>";
                 if (getUrlParam("bm") == "bm")
                 {
                     //部门
                     $("#zheshiliebiaoquyu").attr('teshuwhere', " K_UAID in (select UAid from ZZZ_userinfo where suoshuquyu=(select top 1 suoshuquyu from ZZZ_userinfo where Uaid='" + nowloginuser + "') )   ");
                 }
                 

 
        });

        </script>
</asp:Content>

