<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_dd.aspx.cs" Inherits="qiandao_edit_dd" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />
 <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
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


        <script type="text/javascript">

            //新增提交后强制调用的函数
            function addok_after_msgshow(msg) {
                ;

            }

             jQuery(function ($) {
                 if (getUrlParam("fff") == "1") {
                     setTimeout(bjtd, 500);

                     function bjtd() {
                         var zuobiao = $("#DDzuobiao").val();
                         if (zuobiao != "" && zuobiao.indexOf(',') >= 0) {
                             var zuobiao_arr = zuobiao.split(",");
                             //
                             var point = new BMap.Point($.trim(zuobiao_arr[0]), $.trim(zuobiao_arr[1]));
                             map.centerAndZoom(point, 16);

                             //跳动标记
                             map.clearOverlays();
                             var marker = new BMap.Marker(point);  // 创建标注
                             map.addOverlay(marker);               // 将标注添加到地图中
                             marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
                         }

                     };
                    
                      
                 }
         
          
          
        });
        </script>

</asp:Content>

