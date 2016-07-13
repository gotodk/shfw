<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_sbbase.aspx.cs" Inherits="bas_edit_sbbase" %>

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
                     $("#erpyinru").closest(".form-group").hide();

                 }
                 else {

                     $('#searchopenyhbspgogo_erpyinru').unbind();
                     $("#searchopenyhbspgogo_erpyinru").html("引入");

                     $("#searchopenyhbspgogo_erpyinru").on('click', function (e) {
                         //调用ajax获取数据
                         zdy_ajaxdb($("#erpyinru").val());
                     });
                 }


                 //获取默认个人库存信息
                 function callback_zdy_ajaxdb(xml) {
                     $("#searchopenyhbspgogo_erpyinru").html("引入");
                     //解析xml并显示在界面上
                     if ($(xml).find('返回值单条>执行结果').text() != "ok") {
                         bootbox.alert("查找数据失败!" + $(xml).find('返回值单条>提示文本').text());
                         return false;
                     };
                     $("#SBID").val($(xml).find('数据记录>bianhao').text());
                     $("#SBmingcheng").val($(xml).find('数据记录>mingcheng').text());
                     $("#SBxinghao").val($(xml).find('数据记录>guige').text());
                     $("#SBdanwei").val($(xml).find('数据记录>danwei').text());
                     $("#SBerpbianma").val($(xml).find('数据记录>bianhao').text());

                     bootbox.alert("引入完成！");

                 };
                 function zdy_ajaxdb(cs) {
                     $("#searchopenyhbspgogo_erpyinru").html("正在查询……");
                     $.ajax({
                         type: "POST",
                         url: url1 + "?guid=" + randomnumber(),
                         dataType: "xml",
                         data: "ajaxrun=info&jkname=" + encodeURIComponent("获取某些个人资料") + "&idforedit=" + cs + "&spspsp=erp_wuliaoxinxi",
                         success: callback_zdy_ajaxdb, //请求成功
                         error: errorForAjax//请求出错 
                         //complete: complete//请求完成
                     });

                 };

                 //=================
 
        });
        </script>
</asp:Content>

