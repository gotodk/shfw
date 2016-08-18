<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_fcwxd.aspx.cs" Inherits="fanchang_edit_fcwxd" %>

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
                    data: $('#myform_spanniu').serialize(),
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
                if (($("#fifsssss_FCwx_zt").text() == "" || $("#fifsssss_FCwx_zt").text() == "未填写" || $("#fifsssss_FCwx_zt").text() == "草稿") && getUrlParam("caozuo") == "bianji") {
                    var bjm = "weixiuqingkuanggo";
                    var bjm_wenben = "编辑维修情况";
                    var bjm_tubiao = "fa-pencil-square-o blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        var form = $('<form></form>');
                        // 设置属性  
                        form.attr('action', "/adminht/corepage/fanchang/edit_fcwxd_qk.aspx?fff=1&showinfo=1&idforedit=" + getUrlParam("idforedit"));
                        form.attr('method', 'post');
                        form.attr('target', '_blank');
                        // 提交表单  
                        form.submit();
                      

                    });

                }

                //根据现有状态，添加特殊按钮
                if ($("#fifsssss_FCwx_zt").text() == "草稿" && getUrlParam("caozuo") == "bianji") {
                    var bjm = "tijiaogo";
                    var bjm_wenben = "提交维修情况";
                    var bjm_tubiao = "fa-check blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        
                        begin_ajax("tijiaoshenqing", getUrlParam("idforedit"), "160817000009")

                    });

                }
               
                //
                if ($("#fifsssss_FCwx_zt").text() == "提交" && getUrlParam("caozuo") == "shenhe") {
                    var bjm = "shenhego";
                    var bjm_wenben = "审核/驳回";
                    var bjm_tubiao = "fa-gavel blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        bootbox.dialog({
                            message: "点击按钮选择审核结果：<form  id='myform_spanniu'></form>",
                            title: bjm_wenben,
                            buttons: {
                                Cancel: {
                                    label: "驳回",
                                    className: "btn-default",
                                    callback: function () {
                                        begin_ajax("bohui", getUrlParam("idforedit"), "160817000009");
                                    }
                                }
                                , OK: {
                                    label: "审核通过",
                                    className: "btn-primary",
                                    callback: function () {
                                        begin_ajax("shenhe", getUrlParam("idforedit"), "160817000009");
                                    }
                                }
                            }
                        });



                    });

                }
                

                if ($("#fifsssss_FCwx_zt").text() == "审核" && ($("#fifsssss_FCwx_sfcd").text() == "" || $("#fifsssss_FCwx_sfcd").text() == "否") && getUrlParam("caozuo") == "cundang") {
                    var bjm = "zuizhongchuligo";
                    var bjm_wenben = "最终处理";
                    var bjm_tubiao = "fa-lock blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        bootbox.dialog({
                            message: "<form  id='myform_spanniu'>   <div class='form-group' style='margin:0px;line-height:0.5;'>  <label   for='ddtj_FCwx_zzcl'>最终处理情况：</label> <div class='input-group '><input class='form-control search-query' id='ddtj_FCwx_zzcl' name='ddtj_FCwx_zzcl'  type='text' title=''  value=''   /></div>   </div>   <br/>   <div class='form-group' style='margin:0px;line-height:0.5;'>  <label  for='ddtj_FCwx_hqd'>维修会签单：</label>  <div class='input-group '><input class='form-control search-query' id='ddtj_FCwx_hqd' name='ddtj_FCwx_hqd'  type='text' title=''  value=''   /><span class='input-group-btn'><button class='btn  btn-sm  searchopenyhbspgogo hidden' type='button' id='searchopenyhbspgogo_ddtj_FCwx_hqd' title='单号:主题' guid='弹窗字段配置主键' onclick='openeditdialog(null, $(this));'></span><span class='ace-icon fa fa-search icon-on-right bigger-110'></span> </button></div>  <div class='col-sm-12 no-padding-left' id='show_searchopenyhbspgogo_ddtj_FCwx_hqd'></div>  <br/> <div class='form-group' style='margin:0px;line-height:0.5;'>  <label   for='ddtj_FCwx_sfcd'>是否存档：</label>  <div class='radio'><label><input name='ddtj_FCwx_sfcd' type='radio' value='是' class='ace'><span class='lbl'>是</span></label><label><input name='ddtj_FCwx_sfcd' type='radio' value='否' checked='true' class='ace'><span class='lbl'>否</span></label></div>  </div>      </div>      </form><br/>===========",
                            title: bjm_wenben,
                            buttons: {
                                Cancel: {
                                    label: "取消",
                                    className: "btn-default",
                                    callback: function () {
                                        ;
                                    }
                                }
                                , OK: {
                                    label: "确认",
                                    className: "btn-primary",
                                    callback: function () {
                                        begin_ajax("zuizhongchuli", getUrlParam("idforedit"), "160817000009");
                                    }
                                }
                            }
                        });






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

