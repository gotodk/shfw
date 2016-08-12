<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="add_bxsq_chayue.aspx.cs" Inherits="fwbh_add_bxsq_chayue" %>

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
                //根据现有状态，添加特殊按钮
                
                if ($("#fifsssss_Bzhuangtai").text() == "待处理" && getUrlParam("caozuo") == "jieshu") {
                    var bjm = "zuofeigo";
                    var bjm_wenben = "结束";
                    var bjm_tubiao = "fa-trash-o grey";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        bootbox.dialog({
                            message: "<form  id='myform_spanniu'>结束原因：<br/><textarea placeholder='请输入' class='limited col-xs-12' id='ddtj_Bjieshuyuanyin' name='ddtj_Bjieshuyuanyin' maxlength='500' rows='5' ></textarea></form><hr/>===========",
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
                                    label: "确认结束",
                                    className: "btn-primary",
                                    callback: function () {
                                        begin_ajax("zuofei", getUrlParam("idforedit"), "160605000055");
                                    }
                                }
                            }
                        });
                         

                    });


                     

                }



                if (($("#fifsssss_Bzhuangtai").text() == "待处理" || $("#fifsssss_Bzhuangtai").text() == "已接收") && getUrlParam("caozuo") == "jieshu") {
                    var bjm = "buchonggo";
                    var bjm_wenben = "标注信息";
                    var bjm_tubiao = "fa-book blue";

                    $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                    //给特殊按钮添加事件，调用批量操作的接口
                    $(document).on('click', "#" + bjm + "_top", function () {
                        bootbox.dialog({
                            message: "<form  id='myform_spanniu'><div class='form-group' style='margin:0px;line-height:0.5;'>  <label class='hidden' for='ddtj_Bxsfhdh'>销售发货单号：</label>  <div class='input-group '><input class='form-control search-query' id='ddtj_Bxsfhdh' name='ddtj_Bxsfhdh'  type='text' title=''  value=''   /><span class='input-group-btn'><button class='btn  btn-sm  searchopenyhbspgogo hidden' type='button' id='searchopenyhbspgogo_ddtj_Bxsfhdh' title='单号:客户编号' guid='弹窗字段配置主键' onclick='openeditdialog(null, $(this));'></span><span class='ace-icon fa fa-search icon-on-right bigger-110'></span> </button></div>  <div class='col-sm-12 no-padding-left' id='show_searchopenyhbspgogo_ddtj_Bxsfhdh'></div>  </div>      </form><br/>===========",
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
                                        begin_ajax("buchong", getUrlParam("idforedit"), "160605000055");
                                    }
                                }
                            }
                        });


                        $(".modal-content").css("z-index", 99);



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

