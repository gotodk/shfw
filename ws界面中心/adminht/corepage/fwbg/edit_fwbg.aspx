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
                     //$("#gview_grid-table-subtable-160427000664").find("input[name='保修截止日期']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='运转时间']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000664").find("input[name='备注']").removeAttr("readonly");


 
                     $("#gview_grid-table-subtable-160427000665").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000665").find("input[name='备注']").removeAttr("readonly");




                 
                     $("#gview_grid-table-subtable-160427000666").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='位置标记']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='实际售价']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='零件数量']").removeAttr("readonly");
                  
                     $("#gview_grid-table-subtable-160427000666").find("input[name='批号']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160427000666").find("input[name='备注']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160427000666").find("input[name='实际售价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160427000666").find("input[name='零件数量']").val();
                     $("#gview_grid-table-subtable-160427000666").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));

                     //自动计算报告总金额
                     var aa_gsje = $("#grid-table-subtable-160427000665").getCol("工时金额", false, "sum");
                     var aa_zbje = $("#grid-table-subtable-160427000666").getCol("金额", false, "sum");
                     $("#Gjishufuwufei").val(aa_gsje.toFixed(2));
                     $("#Ggongshi").val(aa_zbje.toFixed(2));

                     var aa_tt1 = $("#Gxcpjzfy").val();
                     var aa_tt2 = $("#Gquyufy").val();

                     var aa_zongjia = aa_gsje * 1 + aa_zbje * 1 + aa_tt1 * 1 + aa_tt2*1;
                      
                     $("#Gzongjia").val(aa_zongjia.toFixed(2));

                     //弹窗特殊条件，隐藏的弹窗的条件
                     var khbh_str = $("#G_YYID").val();
                     $("#searchopenyhbspgogo_subtcid_sb_SID").attr("teshuwhere", "S_YYID='" + khbh_str + "'");


                     //弹窗特殊条件，隐藏的弹窗的条件(从库存选择零件时才启用)
                     <%--var userlogin = "<%=UserSession.唯一键%>";
                     $("#searchopenyhbspgogo_subtcid_lj_LID").attr("teshuwhere", "dpname=(select xingming from  ZZZ_userinfo where UAid='" + userlogin + "')");--%>
                        
                     //弹窗特殊条件，隐藏的弹窗的条件(从销售发货单选择零件时才启用)
                   
                     $("#searchopenyhbspgogo_subtcid_lj_LID").attr("teshuwhere", " FC_YYID='" + khbh_str + "' ");

                     //弹窗特殊条件，只从自己的计划中选择
                     $("#searchopenyhbspgogo_G_jihua_GID").attr("teshuwhere", " G_UAID='<%=UserSession.唯一键%>' ");

                     var nowloginuser = "<%=UserSession.唯一键%>";
                     //弹窗特殊条件，只从自己的客户中选择客户
                     // 过滤自己的客户。 包括全客户关联设置、客户关联表、客户档案服务负责人、报修申请表的服务负责人
                     $("#searchopenyhbspgogo_G_YYID").attr("teshuwhere", " ( ( charindex(','+'" + nowloginuser + "'+',',(select top 1 ','+YSTR+',' from ZZZ_ZFCMJ where YID='tskhgl')) > 0 ) or ( YYID in (select YYID from ZZZ_userinfo_glkh where UAid='" + nowloginuser + "' and shixiaoriqi >= getdate()  UNION  select YYID from ZZZ_KHDA where YYfuwufuzeren='" + nowloginuser + "')  )   or (YYID in (select B_YYID from ZZZ_BXSQ where Bfwfzr='" + nowloginuser + "' and Bzhuangtai='已接收' ))   )");


                     //弹窗特殊条件，只从自己的报障申请中选择
                     $("#searchopenyhbspgogo_G_BID").attr("teshuwhere", " Bfwfzr='" + nowloginuser + "' ");

 
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
                                 if (arr_z[0] == "[保修期限") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='保修期限']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[保修到期日期") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subsbxlh).closest("tr").find("input[name='保修截止日期']");

                                     var time_zz = new Date($.trim(arr_z[1]).replace("]", "")).Format_go("yyyy-MM-dd");
                                    if (time_zz == "" || time_zz == null || time_zz.indexOf("aN") >= 0) {
                                      time_zz = null;
                                     }
                                     //zj.datepicker('setDate', time_zz);

                                    zj.val(time_zz);
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
                                 if (arr_z[0] == "[故障名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='故障名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[故障类型") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='故障类型']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[标准工时") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='标准工时']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[标准工时单价") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='标准工时单价']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[标准工时金额") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subyzcw).closest("tr").find("input[name='工时金额']");
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
                                 if (arr_z[0] == "[保修期限") {
                                 
                                     //根据规则填入保修截止日期
                                     var val_Gbylx = $('input[name="Gbylx"]:checked ').val();
                                     if (val_Gbylx == "保内") {

                                         var bxjzrq = $("#grid-table-subtable-160427000664").jqGrid("getRowData", $("#grid-table-subtable-160427000664").getGridParam('selarrrow')[0]).保修截止日期;
                                         //alert(JSON.stringify(bxjzrq));
                                         var zj = $(dfx_str_subljbh).closest("tr").find("input[name='保修截止日期']");
                                         zj.val(bxjzrq);
                                     }
                                     else {

                                         var bxqx = $.trim(arr_z[1]).replace("]", ""); //带入的保修期限

                                         var now_data = new Date();
                                         now_data.setDate(now_data.getDate() + 10 + parseInt(bxqx));
                                         var bxjzrq = now_data.Format_go("yyyy-MM-dd");
                                         if (bxjzrq.indexOf("aN") >= 0) {
                                             bxjzrq = "";
                                         }
                                         var zj = $(dfx_str_subljbh).closest("tr").find("input[name='保修截止日期']");
                                         zj.val(bxjzrq);
                                     }
                                 }
                                 if (arr_z[0] == "[使用表主键") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subljbh).closest("tr").find("input[name='使用表主键']");
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
                 if (lz_bid != "") {

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
                     $("#Gfwlx").val($(xml).find('数据记录>Bfwlx').text());
                     $("#YYname").val($(xml).find('数据记录>YYname').text());
                     $("#G_jihua_GID").val($(xml).find('数据记录>G_jihua_GID').text());
                     var Bsbtime_zz_ss = new Date($(xml).find('数据记录>Bsbtime').text()).Format_go("yyyy-MM-dd");
                     $("#Gsbtime").datepicker('setDate', Bsbtime_zz_ss);

                     //验证如果保修申请已结单，则提示不需要建立报告，并禁用提交按钮
                     var Bzhuangtai = $(xml).find('数据记录>Bzhuangtai').text();
                     if (Bzhuangtai != "已接收")
                     {
                         $("#addbutton1_top").attr({ "disabled": "disabled" });
                         $("#addbutton1").attr({ "disabled": "disabled" });
                         bootbox.alert("错误：报修申请单状态为[" + Bzhuangtai + "]，不允许建立服务报告！");
                     }
 
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



                    <!-- 增加一些特殊处理按钮，例如提交，收货 -->
    <script type="text/javascript">
        jQuery(function ($) {

            //调用批量操作的接口
            function begin_ajax(zdyname, xuanzhongzhi, zheshiyige_FID)
            {
                $.ajax({
                    url: '/pucu/gqzidingyi.aspx?zdyname=' + zdyname + '&xuanzhongzhi=' + xuanzhongzhi + '&zheshiyige_FID=' + zheshiyige_FID,
                    type: 'post',
                    data: null,
                    cache: false,
                    dataType: 'html',
                    success: function (data) {
                        //显示调用接口并刷新当前页面
                        bootbox.alert({
                            message: data,
                            callback: function () {
                                var newurl = window.location.href;
                                location.href = newurl;

                            }
                        });


                    },
                    error: function () {
                        bootbox.alert('操作失败，接口调用失败！');
                    }
                });
            }


            

            function add_anniu_spsp()
            {
                //根据现有状态，添加特殊按钮
                if (($("#fifsssss_Gzhuangtai").text() == "保存" || $("#fifsssss_Gzhuangtai").text() == "驳回") && getUrlParam("yc_czlx") == "xiugai") {
                    var bjm = "tijiaogo";
                    var bjm_wenben = "提交";
                    var bjm_tubiao = "fa-check blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        
                        begin_ajax("tijiaoshenqing", getUrlParam("idforedit"), "160611000057")

                    });

                }
               
                //
                


            }
            
            if (getUrlParam("showinfo") == "1" || getUrlParam("showinfo") == "2") {
                //数据加载完成才执行，只执行一次
                var jiancha_bdjzwc = window.setInterval(function () {
                    if ($("#editloadinfo").hasClass("hide")) {
                        clearInterval(jiancha_bdjzwc);
                        add_anniu_spsp();
                    }

                }, 1000);
            }
            

        });
        </script>

</asp:Content>

