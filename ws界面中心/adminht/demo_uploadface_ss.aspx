<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="demo_uploadface_ss.aspx.cs" Inherits="demo_uploadface_ss" %>
 
<%@ Register Src="~/upimageuc.ascx" TagPrefix="uc1" TagName="upimageuc" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
   <link href="/assets/css/cropper/cropper.min.css" rel="stylesheet">
  <link href="/assets/css/cropper/main.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 <form>
    <div class="row"><div class="col-sm-12">
         <!-- 模拟小图，使用默认初始图片-->
 
<%--        <uc1:upimageuc runat="server" ID="upimageuc1" Cloadingimg="/mytutu/uploadto.gif" Csite="sm" Cnowimg="/mytutu/uploadto_a.gif" Ctitle="" Cisp="yes" Cidname="qqtutu1"   /> 
        <br/>
        
        <!-- 模拟大图，随便一张初始图片(应从数据库读取)  -->
        <uc1:upimageuc runat="server" ID="upimageuc2" Cloadingimg="" Csite="md" Cnowimg="/mytutu/uploadto_a.gif" Ctitle="" Cisp="no"  Cidname="qqtutu2"  />

                <br/>--%>
        
        <!-- 模拟很大图，随便一张初始图片(应从数据库读取)  -->
        <uc1:upimageuc runat="server" ID="upimageuc3"  Csite="md" Ctitle="上传头像" Cisp="yes"  Cidname="qqtutu3"  />
 
        <br/>

                     </div></div>
 </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
   <script src="/assets/js/cropper/cropper.min.js"></script>
  <script src="/assets/js/cropper/main.js"></script>
        <script type="text/javascript">
            //同步顶部头像
            jQuery(function ($) {
                var jiancha_UAid = window.setInterval(function () {
             
                $(".nav-user-photo").attr("src", $(".avatar-Cidname").val());
                }, 500);
            });
            </script>
</asp:Content>
