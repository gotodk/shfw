<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="cyhq.aspx.cs" Inherits="huiqian_cyhq" %>

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

        //新增提交后强制调用的函数
        function editok_after_msgshow(msg) {
          
            if (msg.indexOf("保存成功") >= 0) {
                //window.location.href = '/adminht/corepage/huiqian/cyhq_list_s.aspx';
            }

        }


        jQuery(function ($) {

            if (getUrlParam("fff") == "1" && getUrlParam("showinfo") == "1")
            {
                $(".c_fanhuishangyiye_top").hide();

                var bjm = "fanhuiliebiao";
                var bjm_wenben = "返回会签列表";
                var bjm_tubiao = "ace-icon fa fa-undo red2";
                $("#myTab").append("<li class='c_" + bjm + "_top'><button class='btn btn-white btn-info btn-bold' id='" + bjm + "_top'><i class='ace-icon fa " + bjm_tubiao + "'></i>" + bjm_wenben + "</button></li><li class='c_" + bjm + "_top'>&nbsp;&nbsp;</li>");
                //给特殊按钮添加事件，调用批量操作的接口
                $(document).on('click', "#" + bjm + "_top", function () {

                    window.location.href = '/adminht/corepage/huiqian/cyhq_list_s.aspx';

                });
            }

            if (getUrlParam("showinfo") == "2" || getUrlParam("showinfo") == "1") {
                

                $("#fifsssss_YJyijian").closest(".form-group").hide();
                $("#dropzone").closest(".form-group").hide();
                $("#fifsssss_YJqianhsuren").closest(".form-group").hide();
                if (getUrlParam("showinfo") == "1")
                {
                    $(".c_bianji_top").hide();
                    $(".c_xinzeng_top").hide();
                }

               


            }


            $("#addbutton1_top").html($("#addbutton1_top").html().replace("保存", "提交"));
            $("#addbutton1").html($("#addbutton1").html().replace("保存", "提交"));


            //折叠转发人区域
            //alert($("label[for='YJqianhsuren']").closest(".form-group").html());
            //$("label[for='YJqianhsuren']").attr("data-toggle", "collapse");
            //$("label[for='YJqianhsuren']").attr("data-target", "#xzzfrqy");
            $("label[for='YJqianhsuren']").next().attr("id", "xzzfrqy");
            $("label[for='YJqianhsuren']").next().addClass("collapse");
            $("label[for='YJqianhsuren']").html("转发至：<br/><button type='button' class='btn btn-xs ' data-toggle='collapse' data-target='#xzzfrqy'>展开/隐藏</button>");
            if (getUrlParam("jck") == "1") {

         

                $("#addbutton1_top").attr({ "disabled": "disabled" });
                $("#addbutton1").attr({ "disabled": "disabled" });
                $("#reloaddb").attr({ "disabled": "disabled" });
            }
            if (getUrlParam("fff") == "1" && getUrlParam("showinfo") != "2" && getUrlParam("showinfo") != "1") {
                     $("#title_f_id").html("参与会签");
          
                     //不是结单人，不显示是否结单选项
                     if ($("#h_jdr").val() != '<%=UserSession.唯一键%>')
                     {
                         $("input[name='Qzhuangtai']").closest(".form-group").remove();
                         
                     }
                     //只有被分发的人才显示会签意见输入框

                     window.setTimeout(function () {

                         $("#YJyijian").closest(".form-group").hide();
                         $("#dropzone").closest(".form-group").hide();
                     $("#YJyijian").val("自动隐藏");
                     $("input[name='h_qsr']").each(function () {
                        
                         if ($(this).val() == '<%=UserSession.唯一键%>')
                         {
                             $("#YJyijian").val("");
                             $("#YJyijian").closest(".form-group").show();
                             $("#dropzone").closest(".form-group").show();
                         }
                     });

                     },500); 

                    



                   
             
                 }
 
        });
        </script>
</asp:Content>

