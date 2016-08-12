<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_sqddd_shenhe.aspx.cs" Inherits="fwbh_edit_sqddd_shenhe" %>

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
                //给报障申请单号增加链接，新窗口打开
                var urldanhao = $("#fifsssss_BID").text();
                $("#fifsssss_BID").html("<a href='/adminht/corepage/fwbg/add_bxsq_chayue.aspx?idforedit=" + urldanhao + "&fff=1&showinfo=2' target='_blank'>" + urldanhao + "[点击查看]</a>");

                //根据现有状态，添加特殊按钮
              
                if ($("#fifsssss_DDzhuangtai").text() == "待审核" && getUrlParam("caozuo") == "shenhe") {
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
                                        begin_ajax("bohui", getUrlParam("idforedit"), "160811000008");
                                    }
                                }
                                , OK: {
                                    label: "审核通过",
                                    className: "btn-primary",
                                    callback: function () {
                                        begin_ajax("shenhe", getUrlParam("idforedit"), "160811000008");
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

