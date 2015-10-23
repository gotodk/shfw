<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageTop.master" AutoEventWireup="true" CodeFile="demo_info.aspx.cs" Inherits="demo_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_container" Runat="Server">
 


<div class="row">
    <div class="col-xs-1 col-sm-3"></div>
							<div class="col-xs-10 col-sm-6">
								<!-- PAGE CONTENT BEGINS -->
				

                                <div id="meiquanxian" runat="server"></div>
<asp:Repeater ID="showinfor" runat="server"> 
<HeaderTemplate><!--头--> 
          
</HeaderTemplate> 
<ItemTemplate><!--中间循环部分--> 
    										<h1>唯一编号：</h1>
										<p class="lead">
											<%#Eval("SID") %>
										</p>
                                          <hr />
                                        <h1>账号：</h1>
										<p class="lead">
											<%#Eval("Sname") %>
										</p>
                                          <hr />
										<h1>密码：</h1>
										<p class="lead">
											<%#Eval("Spassword") %>
										</p>
                                          <hr />
    										<h1>性别：</h1>
										<p class="lead">
											<%#Eval("Ssex") %>
										</p>
                                          <hr />
    										<h1>省份：</h1>
										<p class="lead">
											<%#Eval("Scity") %>
										</p>
                                          <hr />
    										<h1>地区：</h1>
										<p class="lead">
											<%#Eval("Sdiqu") %>
										</p>
                                          <hr />
    										<h1>整数：</h1>
										<p class="lead">
											<%#Eval("Sint") %>
										</p>
                                          <hr />
    										<h1>两位小数：</h1>
										<p class="lead">
											<%#Eval("Sdecimal") %>
										</p>
                                          <hr />
    										<h1>一个日期：</h1>
										<p class="lead">
											<%#Eval("Stime") %>
										</p>
                                          <hr />
    										<h1>日期开始：</h1>
										<p class="lead">
											<%#Eval("Stime_begin") %>
										</p>
                                          <hr />
    										<h1>日期结束：</h1>
										<p class="lead">
											<%#Eval("Stime_end") %>
										</p>
                                          <hr />
    										<h1>备注：</h1>
										<p class="lead">
											<%#Eval("Sbeizhu") %>
										</p>
                                      
</ItemTemplate> 
<FooterTemplate><!--尾--> 
     
</FooterTemplate> 
</asp:Repeater> 



								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div><!-- /.row -->


            
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" Runat="Server">
</asp:Content>

