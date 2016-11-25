<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_wfsb.aspx.cs" Inherits="bas_edit_wfsb" %>

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
            var $overflow_lj = '';

            $("#searchopenyhbspgogo_S_YYID").hide();
            $("#S_YYID").after("<input type='hidden' name='old_S_YYID' id='old_S_YYID' value='' ></input>");

            var sssss = window.setInterval(function () {
            $("#old_S_YYID").val($("#S_YYID").val());
            }, 500);

            window.setInterval(function () {

                //把弹窗中的特殊链接处理成悬浮图片
                $("a[href*='xianshitupian.aspx']").each(function (index, domEle) {
                    //alert($(this).attr("href"));
                    //index就是索引值
                    //domEle 表示获取遍历每一个dom对象

                    $(this).attr("data-rel", "colorbox");
                    $(this).attr("class", "cboxElement");
                    var oldhref = $(this).attr("href");
                    var bianhao = $(this).text();
                    var c_fujian = $(this).parents("tr").find("td[aria-describedby='grid-table_附件']").text().split(',')[0];
                    var newhref = c_fujian;
                    $(this).attr("href", newhref);

                    var c_beizhu = $(this).parents("tr").find("td[aria-describedby='grid-table_备注']").html();
                    var newbeizhu = "备注：" + c_beizhu;
                   
          
 

                    var colorbox_params_lj = {
                        rel: 'colorbox',
                        reposition: true,
                        scalePhotos: true,
                        scrolling: false,
                      
                        previous: '<i class="ace-icon fa fa-arrow-left"></i>',
                        next: '<i class="ace-icon fa fa-arrow-right"></i>',
                        close: '&times;',
                        current: '{current} of {total}',
                        maxWidth: "80%",
                        maxHeight: "80%",
                        html: "<img src=\"" + c_fujian + "\"  style=\"border:0px;\" /><br/><div >" + newbeizhu + "</div>",
                        onOpen: function () {
                            $overflow_lj = document.body.style.overflow;
                            document.body.style.overflow = 'hidden';
                        },
                        onClosed: function () {
                            document.body.style.overflow = $overflow_lj;
                        },
                        onComplete: function () {
                            $.colorbox.resize();
                        }
                    };
                    $(this).colorbox(colorbox_params_lj);
               
                });

                





            }, 500);



            if (getUrlParam("fff") == "1" && getUrlParam("showinfo") == "") {

                $("#SID").attr("readonly", "readonly");
                //$('#SID').closest(".form-group").append("<div class='form-group'><label class='col-sm-2 control-label no-padding-right'>特殊操作：</label><div class='col-sm-10 col-xs-12'></div>  <button class='btn btn-xs btn-danger'><i class='ace-icon fa fa-bolt bigger-110'></i>开启修改序列号</button>查看修改记录 </div>");
                $("<div class='form-group'><label class='col-sm-2 control-label no-padding-right'>特殊操作：</label><div class='col-sm-10 col-xs-12'><button class='btn btn-xs btn-danger' id='tscz_goxiugai'><i class='ace-icon fa fa-bolt bigger-110'></i>开启修改序列号</button>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<button class='btn btn-xs btn-info' id='tscz_showjilu'><i class='ace-icon fa fa-bolt bigger-110'></i>查看修改记录</button></div>   </div>").insertAfter($('#SID').closest(".form-group"));

                $(document).on('click', "#tscz_goxiugai", function () {
                    clearInterval(sssss);
                    $("#SID").removeAttr("readonly");
                    $("#searchopenyhbspgogo_S_YYID").show();

                });
                $(document).on('click', "#tscz_showjilu", function () {

                    var form = $('<form></form>');
                    // 设置属性  
                    form.attr('action', "/adminht/corepage/bas/edit_wfsb_xlhxgjl.aspx");
                    form.attr('method', 'post');
                    form.attr('target', '_blank');
                    // 提交表单  
                    form.submit();

                });
            }

                
                 var dfx_str = "#show_searchopenyhbspgogo_S_YYID";
                 var oldzhi = $(dfx_str).text();
                 var jiancha_UAid = window.setInterval(function () {
             
                     if ($(dfx_str).text() != oldzhi)
                     {
                         var re =/\[.*?\]/ig;
                         var arr = $(dfx_str).text().match(re);
                 
                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[客户名称")
                                 { $("#YYname").val($.trim(arr_z[1]).replace("]", "")); }
                           
                             }
                         }
                         
                         oldzhi = $(dfx_str).text();
                     }
                 }, 500);





                 var dfx_str_sb = "#show_searchopenyhbspgogo_S_SBID";
                 var oldzhi_sb = $(dfx_str_sb).text();
                 var jiancha_UAid_sb = window.setInterval(function () {

                     if ($(dfx_str_sb).text() != oldzhi_sb) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_sb).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备名称")
                                 { $("#Smingcheng").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[规格型号")
                                 { $("#Sxinghao").val($.trim(arr_z[1]).replace("]", "")); }
                  
                                     if (getUrlParam("fff") == "1") {

                                       
                                     }
                                     else {
                                         
                                         if (arr_z[0] == "[成本价")
                                         { $("#Schenbenjia").val($.trim(arr_z[1]).replace("]", "")); }
                                         if (arr_z[0] == "[保修期限")
                                         { $("#Sbaoxiuqixian").val($.trim(arr_z[1]).replace("]", "")); }
                                         if (arr_z[0] == "[保养周期")
                                         { $("#Sbaoyangzhouqi").val($.trim(arr_z[1]).replace("]", "")); }
                                     }
                               

                             }
                         }

                         oldzhi_sb = $(dfx_str_sb).text();
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



 

                 window.setInterval(function () {

                     //弹窗特殊条件

                     var str_Sleixing = $("#S_SBID").val();
                     $("#searchopenyhbspgogo_Sbanben").attr("teshuwhere", "BBB_SBID in (select SBID from ZZZ_SBLXBASE  where BBB_SBID='" + str_Sleixing + "')");

 

                 }, 500);

                 window.setInterval(function () {

 

                     $("#gview_grid-table-subtable-160726000004").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='位置标记']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='实际售价']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='零件数量']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='保修截止日期']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='批号']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160726000004").find("input[name='备注']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160726000004").find("input[name='实际售价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160726000004").find("input[name='零件数量']").val();
                     $("#gview_grid-table-subtable-160726000004").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));

                 }, 500);

 
 
        });
        </script>
</asp:Content>

