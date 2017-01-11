<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_htgl.aspx.cs" Inherits="bas_edit_htgl" %>

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
                


                 //隐藏子表弹窗共享字段
                 $("#yincang_sblb").closest(".form-group").hide();
                 $("#Hfukuanriqi").closest(".form-group").hide();
                 $("#Hfukuanzhouqi").closest(".form-group").hide();

                 if (getUrlParam("fff") == "1" && getUrlParam("showinfo") == "") {

                     $("#HID").attr("readonly", "readonly");
             
                     $("<div class='form-group'><label class='col-sm-2 control-label no-padding-right'>特殊操作：</label><div class='col-sm-10 col-xs-12'><button class='btn btn-xs btn-danger' id='tscz_goxiugai'><i class='ace-icon fa fa-bolt bigger-110'></i>开启修改合同编号</button> </div>   </div>").insertAfter($('#HID').closest(".form-group"));

                     $(document).on('click', "#tscz_goxiugai", function () {

                         $("#HID").removeAttr("readonly");

                     });
                     
                 }




                 window.setInterval(function () {
 
                     //设置子表输入框只读
                  
                     $("#gview_grid-table-subtable-160902000184").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160902000184").find("input[name='付款说明']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160902000184").find("input[name='付款日期']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160902000184").find("input[name='付款比例']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160902000184").find("input[name='备注']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_bili = $("#gview_grid-table-subtable-160902000184").find("input[name='付款比例']").val();
                     var zz_zongjia = $("#Hzongjia").val();
                     $("#gview_grid-table-subtable-160902000184").find("input[name='付款金额']").val((zz_zongjia*(zz_bili * 1 / 100)).toFixed(2));
                     var skjeheji = $("#grid-table-subtable-160902000184").getCol("付款金额", false, "sum") * 1;
                     $("#Hshouuanjin").val(skjeheji);
                 }, 500);

                 var dfx_str_kh = "#show_searchopenyhbspgogo_H_YYID";
                 var oldzhi_kh = $(dfx_str_kh).text();
            
                 var jiancha_YYID = window.setInterval(function () {

                     //弹窗特殊条件
                     //$("#Hbeizhu").val($("#searchopenyhbspgogo_subtcid_HSB_SID").attr("teshuwhere"));
                     var guolv = $("#H_YYID").val();
                     $("#searchopenyhbspgogo_subtcid_HSB_SID").attr("teshuwhere", "S_YYID='" + guolv + "'");

                     //带入字段
                     if ($(dfx_str_kh).text() != oldzhi_kh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_kh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }

                             }
                         }

                         oldzhi_kh = $(dfx_str_kh).text();
                     }
                 }, 500);





                 var dfx_str_khdl = "#show_searchopenyhbspgogo_Hdailishang";
                 var oldzhi_khdl = $(dfx_str_khdl).text();

                 var jiancha_khdl = window.setInterval(function () {

                     //带入字段
                     if ($(dfx_str_khdl).text() != oldzhi_khdl) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_khdl).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#dailishangname").val($.trim(arr_z[1]).replace("]", "")); }

                             }
                         }

                         oldzhi_khdl = $(dfx_str_khdl).text();
                     }
                 }, 500);




                 //设备序列号子表弹窗

                 var dfx_str_subsbxlh = "#show_searchopenyhbspgogo_subtcid_HSB_SID";
                 var oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();

                 var jiancha_subsbxlh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subsbxlh).text() != oldzhi_subsbxlh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subsbxlh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[物料编码") {
                                     //离弹窗最近的特定name的输入框  设备规格
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("#subtcid_HSB_SBID");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[设备名称") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备名称']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[设备型号") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='设备规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 
                              

                             }
                         }

                         oldzhi_subsbxlh = $(dfx_str_subsbxlh).text();
                     }
                 }, 500);





                 var dfx_str_subtt = "#show_searchopenyhbspgogo_subtcid_HSB_SBID";
                 var oldzhi_subtt = $(dfx_str_subtt).text();

                 var jiancha_subtt = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subtt).text() != oldzhi_subtt) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subtt).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[物料名称") {
                                     //离弹窗最近的特定name的输入框   
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='设备名称']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[物料型号") {
                                     //离弹窗最近的特定name的输入框   
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='设备规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                  
                             }
                         }

                         oldzhi_subtt = $(dfx_str_subtt).text();
                     }
                 }, 500);



 
        });
        </script>
</asp:Content>

