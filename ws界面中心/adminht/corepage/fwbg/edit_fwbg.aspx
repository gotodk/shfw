<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_fwbg.aspx.cs" Inherits="fwbh_edit_fwbg" %>

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


            }
            else {
                $("#Gkaigongtime").datepicker('setDate', new Date());
                $("#Gwangongtime").datepicker('setDate', new Date());
            }



            $("#yc_czlx").closest(".form-group").hide();
                 if (getUrlParam("fff") == "1") {
                     var yc_czlx = getUrlParam("yc_czlx");
                     if (yc_czlx == "xiugai")
                     {
                         $("input[name='Gshenpixuanxiang']").closest(".form-group").hide();
                     }
                     if (yc_czlx == "shenhe")
                     {
                         $("#addbutton1_top").html($("#addbutton1_top").html().replace("保存", "确定"));
                         $("#addbutton1").html($("#addbutton1").html().replace("保存", "确定"));
 
                      
                     }
                     if (yc_czlx == "chakan")
                     {
                         $("#addbutton1_top").attr({ "disabled": "disabled" });
                         $("#addbutton1").attr({ "disabled": "disabled" });
                         $("#reloaddb").attr({ "disabled": "disabled" });

                         $("input[name='Gshenpixuanxiang']").closest(".form-group").hide();

                     }

                 }
                 else {
                     $("#GID").closest(".form-group").hide();
                     $("#Gtianxieren").closest(".form-group").hide();
                     $("#Gtianxieren_name").closest(".form-group").hide();
                     $("#Gzhuangtai").closest(".form-group").hide();
                     $("#Gaddtime").closest(".form-group").hide();

                     $("#Gspyj").closest(".form-group").hide();
                     $("#Gspren").closest(".form-group").hide();
                     $("#Gspren_name").closest(".form-group").hide();
                     $("#Gspshijian").closest(".form-group").hide();
                     $("input[name='Gshenpixuanxiang']").closest(".form-group").hide();
                 }

                 window.setInterval(function () {

                     $("#yc_czlx").val(getUrlParam("yc_czlx"));

                     //设置子表输入框只读
                  
                     $("#gview_grid-table-subtable-160427000664").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='保修截止日期']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='运转时间']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='备注']").removeAttr("readonly");


 
                     $("#gview_grid-table-subtable-160427000665").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000665").find("input[name='备注']").removeAttr("readonly");




                 
                     $("#gview_grid-table-subtable-160427000666").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='位置标记']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='实际售价']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='零件数量']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='保修截止日期']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='批号']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='备注']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160427000666").find("input[name='实际售价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160427000666").find("input[name='零件数量']").val();
                     $("#gview_grid-table-subtable-160427000666").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));

                     //自动计算报告总金额
                     var aa_zbje = $("#grid-table-subtable-160427000666").getCol("金额", false, "sum");
                     var aa_Gjishufuwufei = $("#Gjishufuwufei").val();
                     var aa_zj = aa_zbje*1 + aa_Gjishufuwufei*1;
                     $("#Gzongjia").val(aa_zj.toFixed(2));

                     //弹窗特殊条件，隐藏的弹窗的条件
                     var khbh_str = $("#G_YYID").val();
                     $("#searchopenyhbspgogo_subtcid_sb_SID").attr("teshuwhere", "S_YYID='" + khbh_str + "'");


                     //弹窗特殊条件，隐藏的弹窗的条件(从库存选择零件时才启用)
                     <%--var userlogin = "<%=UserSession.唯一键%>";
                     $("#searchopenyhbspgogo_subtcid_lj_LID").attr("teshuwhere", "dpname=(select xingming from  ZZZ_userinfo where UAid='" + userlogin + "')");--%>
                         
 
                 }, 500);



                 //设备序列号子表弹窗

                 var dfx_str_subsbxlh = "#show_searchopenyhbspgogo_subtcid_sb_SID";
                 var oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();

                 var jiancha_subsbxlh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subsbxlh).text() != oldzhi_subsbxlh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subsbxlh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[设备型号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[ERP编号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='ERP编号']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[保修到期日期") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='保修截止日期']");

                                     var time_zz = new Date($.trim(arr_z[1]).replace("]", "")).Format_go("yyyy-MM-dd");
                                    if (time_zz == "" || time_zz == null || time_zz.indexOf("aN") >= 0) {
                                      time_zz = null;
                                     }
                                     zj.datepicker('setDate', time_zz);

                                     //zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();
                     }
                 }, 500);







                 //预制错误子表弹窗

                 var dfx_str_subyzcw = "#show_searchopenyhbspgogo_subtcid_bj_EID";
                 var oldzhi_subyzcw = $(dfx_str_subyzcw).text();

                 var jiancha_subyzcw = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subyzcw).text() != oldzhi_subyzcw) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subyzcw).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[错误名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='错误名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[错误类型") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='错误类型']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subyzcw = $(dfx_str_subyzcw).text();
                     }
                 }, 500);





                 //零件编号子表弹窗

                 var dfx_str_subljbh = "#show_searchopenyhbspgogo_subtcid_lj_LID";
                 var oldzhi_subljbh = $(dfx_str_subljbh).text();

                 var jiancha_subljbh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subljbh).text() != oldzhi_subljbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subljbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[零件名称") {
                                     //离弹窗最近的特定name的输入框  设备规格
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='零件名称']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[规格型号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='规格型号']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[零件单位") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='零件单位']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[零售价") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj0 = $(dfx_str_subljbh).closest("tr").find("input[name='实际售价']");
                                     zj0.val($.trim(arr_z[1]).replace("]", ""));
                                     var zj1 = $(dfx_str_subljbh).closest("tr").find("input[name='零售价']");
                                     zj1.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[批号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='批号']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[库位") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='出库库位']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subljbh = $(dfx_str_subljbh).text();
                     }
                 }, 500);














                 ///////////////////////////////////////////////////////



                 //关联单据弹窗

                 var dfx_str_gldh = "#show_searchopenyhbspgogo_G_BID";
                 var oldzhi_gldh = $(dfx_str_gldh).text();

                 var jiancha_gldh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_gldh).text() != oldzhi_gldh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_gldh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户编号")
                                 { $("#G_YYID").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[服务类型")
                                 { $("#Gfwlx").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[申报时间")
                                 { $("#Gsbtime").val($.trim(arr_z[1]).replace("]", "").split(' ')[0]); }

                             }
                         }

                         oldzhi_gldh = $(dfx_str_gldh).text();
                     }
                 }, 500);





                 //客户编号弹窗

                 var dfx_str_khbh = "#show_searchopenyhbspgogo_G_YYID";
                 var oldzhi_khbh = $(dfx_str_khbh).text();

                 var jiancha_khbh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_khbh).text() != oldzhi_khbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_khbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }


                             }
                         }

                         oldzhi_khbh = $(dfx_str_khbh).text();
                     }
                 }, 500);





                 var lz_bid = getUrlParam("lz_bid");
                 if (lz_bid != "" && getUrlParam("fff") == "0") {

                     //获取带入单据的资料
                 zdy_ajaxdb("");
                 function callback_zdy_ajaxdb(xml) {
                     //解析xml并显示在界面上
                     if ($(xml).find('返回值单条>执行结果').text() != "ok") {
                         bootbox.alert("查找数据失败!" + $(xml).find('返回值单条>提示文本').text());
                         return false;
                     };
                     $("#G_BID").val($(xml).find('数据记录>BID').text());
                     $("#G_YYID").val($(xml).find('数据记录>B_YYID').text());
                     $("#YYname").val($(xml).find('数据记录>YYname').text());
                     var Bsbtime_zz_ss = new Date($(xml).find('数据记录>Bsbtime').text()).Format_go("yyyy-MM-dd");
                     $("#Gsbtime").datepicker('setDate', Bsbtime_zz_ss);
 
                 };
                 function zdy_ajaxdb(cs) {
                     $.ajax({
                         type: "POST",
                         url: url1 + "?guid=" + randomnumber(),
                         dataType: "xml",
                         data: "ajaxrun=info&jkname=" + encodeURIComponent("获取某些个人资料") + "&idforedit=" + lz_bid + "&spspsp=guanliandanju",
                         success: callback_zdy_ajaxdb, //请求成功
                         error: errorForAjax//请求出错 
                         //complete: complete//请求完成
                     });

                 };

                 }

                 


 
        });
        </script>
</asp:Content>

