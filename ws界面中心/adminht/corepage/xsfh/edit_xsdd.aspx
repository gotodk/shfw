<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_xsdd.aspx.cs" Inherits="xsfh_edit_xsdd" %>

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
                

                 if (getUrlParam("fff") == "1" && (getUrlParam("showinfo") == "1" || getUrlParam("showinfo") == "2")) {
                     $(".c_bianji_top").hide();
                     if ($("#fifsssss_FCzhuangtai").val() == "草稿") {
                         $(".c_bianji_top").show();
                     }
                 }




                 window.setInterval(function () {
 
                     //物料类型弄成只读的
                     $("#gview_grid-table-subtable-160907000140").find("input").attr("readonly", "readonly");
                     $("#gview_grid-table-subtable-160907000140").find("input[name='发货数量']").removeAttr("readonly");

                     $("#gview_grid-table-subtable-160907000140").find("input[name='单价']").removeAttr("readonly");
                     //$("#gview_grid-table-subtable-160713000991").find("input[name='金额']").removeAttr("readonly");
                     $("#gview_grid-table-subtable-160907000140").find("input[name='备注']").removeAttr("readonly");
                

                     //自动计算子表金额
                     var zz_sjsj = $("#gview_grid-table-subtable-160907000140").find("input[name='单价']").val();
                     var zz_shuliang = $("#gview_grid-table-subtable-160907000140").find("input[name='发货数量']").val();
                     $("#gview_grid-table-subtable-160907000140").find("input[name='金额']").val((zz_sjsj * zz_shuliang).toFixed(2));

                 }, 500);


 

                 
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
                                 if (arr_z[0] == "[物料名称") {
                                     //离弹窗最近的特定name的输入框   
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='物料名称']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[物料型号")
                                 {
                                     //离弹窗最近的特定name的输入框   
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='规格型号']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[物料类别") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='物料类别']");
                                     zj.val($.trim(arr_z[1]).replace("]", ""));
                                 }
                                 if (arr_z[0] == "[保修期限") {
                                     //离弹窗最近的特定name的输入框  
                                     var zj = $(dfx_str_subtt).closest("tr").find("input[name='保修期限']");
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
                //下推操作要防重复，这里要做loading处理
                if (zdyname == "xiatui")
                {
                    $("#xiatuigo_top").attr('disabled', "true");;
                }

                $.ajax({
                    url: '/pucu/gqzidingyi.aspx?zdyname=' + zdyname + '&xuanzhongzhi=' + xuanzhongzhi + '&zheshiyige_FID=' + zheshiyige_FID,
                    type: 'post',
                    data: null,
                    cache: false,
                    dataType: 'html',
                    success: function (data) {

                        if (zdyname == "xiatui") {
                            $("#xiatuigo_top").removeAttr("disabled"); 
                        }

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
                if ($("#fifsssss_FCzhuangtai").text() == "草稿" && getUrlParam("caozuo") == "grbj") {
                    var bjm = "tijiaogo";
                    var bjm_wenben = "提交";
                    var bjm_tubiao = "fa-check blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        
                        begin_ajax("tijiaoshenqing", getUrlParam("idforedit"), "160907000013")

                    });

                }
               
                //
                if ($("#fifsssss_FCzhuangtai").text() == "提交" && getUrlParam("caozuo") == "grbj") {
                    var bjm = "xiatuigo";
                    var bjm_wenben = "生成销售发货单";
                    var bjm_tubiao = "fa-hdd-o blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {

                        begin_ajax("xiatui", getUrlParam("idforedit"), "160907000013")

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

