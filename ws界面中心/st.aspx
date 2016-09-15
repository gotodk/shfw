<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain_frm_subtab.master" AutoEventWireup="true" CodeFile="st.aspx.cs" Inherits="st" %>

<%@ Register Src="~/pucu/wuc_css_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_css_onlygrid" %>
<%@ Register Src="~/pucu/wuc_content_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_content_onlygrid" %>
<%@ Register Src="~/pucu/wuc_script_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_script_onlygrid" %>





<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 附加的head内容 -->

    <uc1:wuc_css_onlygrid runat="server" ID="wuc_css_onlygrid" />
    <!-- page specific plugin styles -->
    <link rel="stylesheet" href="/assets/css/colorbox.css" />
    <link href="/video-js/video-js.css" rel="stylesheet">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_daohang" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_pagecontent" runat="Server">


    <div>
        <ul class="list-group">
            <!-- #section:pages/gallery -->
            <li class="list-group-item red">共<%=arr_tupian.Length.ToString() %>个附件</li>
            <%      
                for (int i = 0; i < arr_tupian.Length; i++)
                { %>

            <li class="list-group-item">

                <%
                    if (Checktu(arr_tupian[i]))
                    {
                %>

                <a href="st_d.aspx?fn=<%=arr_tupian[i] %>" target="_blank">这是图片附件<%=i.ToString() %>，点击下载</a>
                <img class="img-responsive" src="<%=arr_tupian[i] %>" />
                <%
                    }
                    else
                    {


                        if (Checkshipin(arr_tupian[i]))
                        {
                %>
                <a href="<%=arr_tupian[i] %>" target="_blank">点击下载此文件：这是视频附件<%=i.ToString() %></a>
                <br />

                <video id="my-video-<%=i.ToString() %>" class="video-js" controls preload="auto" width="10" height="10"
                    poster="/video-js/1.jpg" data-setup="{}">
                    <source src="<%=arr_tupian[i] %>" type='video/mp4'>
                    <%--    <source src="MY_VIDEO.webm" type='video/webm'>--%>
                    <p class="vjs-no-js">
                        此浏览器不支持该视频插件。
                    </p>
                </video>

                <%
                    }
                    else
                    {
                %>
                <a href="st_d.aspx?fn=<%=arr_tupian[i] %>" target="_blank">点击下载此文件：这是文件附件<%=i.ToString() %></a>

                <%
                        }

                    } %>





                   </li>



                <%}
                %>
                            
 

         







        </ul>
    </div>
    <!-- PAGE CONTENT ENDS -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_script" runat="Server">

    <script src="/video-js/video.js"></script>
        <script src="/video-js/ie8/videojs-ie8.min.js"></script>
    <script>  
        videojs.options.flash.swf = "/video-js/video-js.swf"
</script>  
    <!-- 强制设置高度和宽度 -->
    <script type="text/javascript">
        jQuery(function ($) {
            //
            //getVideoInfo("#my-video-1");

            var wait=setInterval(function(){  
                var kd = ($(window).width() > 700 ? 640 : $(window).width() * 0.9-60);
                var gd = ($(window).width() > 700 ? 264 : 200);
                //alert(kd + "," + gd);

                $('div[id^="my-video-"]').each(function () {
                    videojs($(this).attr("id")).width(kd);
                    videojs($(this).attr("id")).height(gd);
                });

               
        
            }, 500);

        
           

        });
    </script>

</asp:Content>

