 
<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain_frm_subtab.master" AutoEventWireup="true" CodeFile="st.aspx.cs" Inherits="st" %>
 
<%@ Register Src="~/pucu/wuc_css_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_css_onlygrid" %>
<%@ Register Src="~/pucu/wuc_content_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_content_onlygrid" %>
<%@ Register Src="~/pucu/wuc_script_onlygrid.ascx" TagPrefix="uc1" TagName="wuc_script_onlygrid" %>





<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 附加的head内容 -->
 
    <uc1:wuc_css_onlygrid runat="server" ID="wuc_css_onlygrid" />
    <!-- page specific plugin styles -->
		<link rel="stylesheet" href="/assets/css/colorbox.css" />
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

											<a href="st_d.aspx?fn=<%=arr_tupian[i] %>" >
                                                <%
    }
    else
    { 
                                                     %>
                                                <a href="<%=arr_tupian[i] %>" target="_blank" >
                                                <%} %>
                                                <% if (Checktu(arr_tupian[i]))
                                                    {
                                                        %>
                                                    这是图片附件<%=i.ToString() %>
                                                <img class="img-responsive"   src="<%=arr_tupian[i] %>" />
                                                <%
                                                    }
                                                    else
                                                    {
                                                %>
                                                点击下载此文件：这是文件附件<%=i.ToString() %>
                                                <%
                                                    }
                                                        %>

												
											 
											</a>
										</li>

                                           <%}
                                                    %>
                            
 



                             

                                   



									</ul>
								</div><!-- PAGE CONTENT ENDS --> 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_script" runat="Server">
 
 

</asp:Content>

