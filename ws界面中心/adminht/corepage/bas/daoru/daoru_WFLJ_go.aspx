<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="daoru_WFLJ_go.aspx.cs" Inherits="corepage_daoru_WFLJ_go" %>

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

       <form id="form1_daoru" runat="server">
               <div class="hidden">
    <asp:TextBox ID="tishimsg" runat="server"></asp:TextBox></div>
            <div class="row">
               <div class="col-sm-12"><a href="daoruwuliao.rar">下载导入模板：物料导入模板</a></div>
           </div>
           <div class="row">
               <div class="col-sm-12"><asp:FileUpload ID="fupExcel" runat="server"  CssClass="btn btn-sm btn-info" /></div>
           </div>
           <br/>
           <div class="row">
               <div class="col-sm-12">指定工作薄名称：<asp:TextBox ID="TextBox1" runat="server" Text="Sheet1" Width="170px"></asp:TextBox><asp:TextBox ID="TextBox2" runat="server" Text="Sheet1" CssClass="hidden"></asp:TextBox></div>
           </div>
           <br/>
           <div class="row">
               <div class="col-sm-12"> <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="检查新数据" CssClass="btn btn-sm btn-info" /><asp:Button ID="Button2" runat="server" Text="提交数据" OnClick="Button2_Click" Visible="False"  CssClass="btn btn-sm btn-info" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button3" runat="server" Text="重新上传" OnClick="Button3_Click" Visible="False"  CssClass="btn btn-sm btn-grey" /></div>
           
           </div>
    
     <br/>

           <div class="row">
                <div class="col-sm-12">
                     <asp:GridView ID="GridView1" runat="server"   Width="100%">
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>

                </div>
               </div>
    
    </form>
     
    <div class="hidden">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />
 </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">

       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />
        <script type="text/javascript">
             jQuery(function ($) {
                 window.setInterval(function () {
                     if ($("#sp_pagecontent_tishimsg").val() != "")
                     {
                         alert($("#sp_pagecontent_tishimsg").val());
                         window.top.location.href = window.location.href;
                     }
                 }, 500);
        });
        </script>

</asp:Content>

