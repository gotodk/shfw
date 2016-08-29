<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="fc_shenqing.aspx.cs" Inherits="fanchang_fc_shenqing" %>

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
        <!-- 选择产品编号后，自动带入一些数据 -->
    <script type="text/javascript">
        jQuery(function ($) {




                
                 function bianjicaogao()
                 {
                     $("#FCshenqingren").closest(".form-group").hide();
                     $("#FCshenqingren_name").closest(".form-group").hide();
                     $("#FCshenqingshijian").closest(".form-group").hide();
                     $("#FCshenheren").closest(".form-group").hide();
                     $("#FCshenheren_name").closest(".form-group").hide();
                     $("#FCshenheshijian").closest(".form-group").hide();
                     $("#FCwuliudan").closest(".form-group").hide();
                     $("#FCwuliugongsi").closest(".form-group").hide();
                     $("#FCfahuoren").closest(".form-group").hide();
                     $("#FCfahuoren_name").closest(".form-group").hide();
                     $("#FCfahuoshijian").closest(".form-group").hide();
                     $("#FCjisongdizhi").closest(".form-group").hide();
                     $("#FClianxifangshi").closest(".form-group").hide();
                     $("#FCshoujianren").closest(".form-group").hide();
                     $("#FCshoujianshijian").closest(".form-group").hide();
                     $("#FCqurenshoujianren").closest(".form-group").hide();
                     $("#FCqurenshoujianren_name").closest(".form-group").hide();
                     $("#FCzhuangtai").closest(".form-group").hide();
                     $("input[name='shenhe_yincang']").closest(".form-group").hide();
                     $("hr").hide();
                 }

                 //发货处理
                 function fahuo() {
                     $("#searchopenyhbspgogo_FC_YYID").hide();

                
                     $("#FCwuliudan").removeAttr("readonly");
                     $("#FCwuliugongsi").removeAttr("readonly");

                     $("#FCfahuoren").closest(".form-group").hide();
                     $("#FCfahuoren_name").closest(".form-group").hide();
                     $("#FCfahuoshijian").closest(".form-group").hide();

                     $("#FCjisongdizhi").removeAttr("readonly");
                     $("#FClianxifangshi").removeAttr("readonly");
                     $("#FCshoujianren").removeAttr("readonly");

                   
                     $("#FCzhuangtai").closest(".form-group").hide();
                     $("input[name='shenhe_yincang']").closest(".form-group").hide();
 
                 }
               
                 //把业务类型参数放到隐藏控件里面
                 //
                 window.setInterval(function () {
                     $("#ywlx_yincang").val(getUrlParam("ywlx"));


                 
                     $("#ywlx_yincang").closest(".form-group").hide();

                     //弹窗特殊条件，隐藏的弹窗的条件
                     var khbh_str = $("#FC_YYID").val();
                     $("#searchopenyhbspgogo_subtcid_FCSbh").attr("teshuwhere", "S_YYID='" + khbh_str + "'");

                     //弹窗特殊条件，隐藏的弹窗的条件
                     var khbh_str = $("#FC_YYID").val();
                     $("#searchopenyhbspgogo_FCsbbh").attr("teshuwhere", "S_YYID='" + khbh_str + "'");

                     //设备子表处理
                     $("#gview_grid-table-subtable-160818000119").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160818000119").find("input[name='配件数量']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160818000119").find("input[name='单价']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160818000119").find("input[name='备注']").removeAttr("readonly");

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160818000119").find("input[name='单价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160818000119").find("input[name='配件数量']").val();
                     $("#gview_grid-table-subtable-160818000119").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));

                     if (getUrlParam("fff") == "1") {
                         //给会签单号增加超链接
                         var hqdh = $("#Fhuiqiandanhao").val();
                         $("label[for='Fhuiqiandanhao']").html("<i class='light-red'>*  </i><a href='/adminht/corepage/huiqian/cyhq.aspx?idforedit=" + hqdh + "&fff=1&jck=1&showinfo=2'  target='_blank'>会签单号</a>");
                     }
                     
                 }, 500);

                 //业务处理
       
                 if (getUrlParam("fff") == "1") {

                     //编辑草稿时候的处理
                     if (getUrlParam("ywlx") == "bianjicaogao") {
                         bianjicaogao();
                     }
                     //编辑发货申请时的处理
                     if (getUrlParam("ywlx") == "fahuo") {
                        
                         fahuo();
                     }
                     //仅查看时的处理
                     if (getUrlParam("ywlx") == "chakan") {
                         $("#addbutton1_top").attr({ "disabled": "disabled" });
                         $("#addbutton1").attr({ "disabled": "disabled" });
                         $("#reloaddb").attr({ "disabled": "disabled" });

                         $("input[name='shenhe_yincang']").closest(".form-group").hide();
                     }
                
                  
                 }
                 else {
                     //新增申请隐藏一些数据
                     bianjicaogao();
                    
                 }



                 var nowloginuser = "<%=UserSession.唯一键%>";
            // 过滤自己的客户。 包括全客户关联设置、客户关联表、客户档案服务负责人
            $("#searchopenyhbspgogo_FC_YYID").attr("teshuwhere", " ( ( charindex(','+'" + nowloginuser + "'+',',(select top 1 ','+YSTR+',' from ZZZ_ZFCMJ where YID='tskhgl')) > 0 ) or ( YYID in (select YYID from ZZZ_userinfo_glkh where UAid='" + nowloginuser + "' and shixiaoriqi >= getdate()  UNION  select YYID from ZZZ_KHDA where YYfuwufuzeren='" + nowloginuser + "') ) )");

                 var dfx_str_kh = "#show_searchopenyhbspgogo_FC_YYID";
                 var oldzhi_kh = $(dfx_str_kh).text();
            
                 var jiancha_YYID = window.setInterval(function () {

                  
                 
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




            //设备序列号主表弹窗
                 var dfx_str_sbxlh = "#show_searchopenyhbspgogo_FCsbbh";
                 var oldzhi_sbxlh = $(dfx_str_sbxlh).text();

                 var jiancha_sbxlh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_sbxlh).text() != oldzhi_sbxlh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_sbxlh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备名称")
                                 { $("#FCsbmingcheng").val($.trim(arr_z[1]).replace("]", "")); }
                                 if (arr_z[0] == "[设备型号")
                                 { $("#FCsbguige").val($.trim(arr_z[1]).replace("]", "")); }
                             }
                         }

                         oldzhi_sbxlh = $(dfx_str_sbxlh).text();
                     }
                 }, 500);


                 //返厂配件子表弹窗
                 var dfx_str_subtt_wbh = "#show_searchopenyhbspgogo_subtcid_FCWbh";
                 var oldzhi_subtt_wbh = $(dfx_str_subtt_wbh).text();

                 var jiancha_subtt_wbh = window.setInterval(function () {



                     //带入字段
                     if ($(dfx_str_subtt_wbh).text() != oldzhi_subtt_wbh) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subtt_wbh).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[名称") {
                                     //离弹窗最近的特定name的输入框  设备规格

                                     var zj = $(dfx_str_subtt_wbh).closest("tr").find("input[name='配件物料名称']");

                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[规格") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt_wbh).closest("tr").find("input[name='配件物料规格']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subtt_wbh = $(dfx_str_subtt_wbh).text();
                     }
                 }, 500);


               //返厂设备子表弹窗
                 var dfx_str_subtt = "#show_searchopenyhbspgogo_subtcid_FCSbh";
                 var oldzhi_subtt = $(dfx_str_subtt).text();

                 var jiancha_subtt = window.setInterval(function () {

                     

                     //带入字段
                     if ($(dfx_str_subtt).text() != oldzhi_subtt) {
                         var re = /\[.*?\]/ig;
                         var arr = $(dfx_str_subtt).text().match(re);

                         if (arr != null) {//如果能匹配成功即arr数组不为空，循环输出结果
                             for (var i = 0; i < arr.length; i++) {
                                 var arr_z = arr[i].split(':');
                                 if (arr_z[0] == "[设备型号")
                                 {
                                     //离弹窗最近的特定name的输入框  设备规格
                                
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='设备规格']");
                                
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[设备名称") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='设备名称']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                             }
                         }

                         oldzhi_subtt = $(dfx_str_subtt).text();
                     }
                 }, 500);

 
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
                if ($("#fifsssss_FCzhuangtai").text() == "草稿" && getUrlParam("ywlx") == "bianjicaogao") {
                    var bjm = "tijiaogo";
                    var bjm_wenben = "提交";
                    var bjm_tubiao = "fa-check blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        
                        begin_ajax("tijiaoshenqing", getUrlParam("idforedit"), "160513000042")

                    });

                }
               
                //
                if ($("#fifsssss_FCzhuangtai").text() == "在途" && getUrlParam("shouhuogo") == "yes") {
                    var bjm = "shouhuogo";
                    var bjm_wenben = "确认收货";
                    var bjm_tubiao = "fa-gift blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {

                        begin_ajax("querenshouhuo", getUrlParam("idforedit"), "160514000045")

                    });

                }


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

