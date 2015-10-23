<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_noauth.aspx.cs" Inherits="auth_noauth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 <div class="row">
							<div class="col-xs-12">
								<!-- PAGE CONTENT BEGINS -->

								<!-- #section:pages/error -->
								<div class="error-container">
									<div class="well">
										<h1 class="grey lighter smaller">
											<span class="blue bigger-125">
												<i class="ace-icon fa fa-lightbulb-o "></i>
												无权访问
											</span>
											 .....
										</h1>

										<hr />
										 
										<div>
								 
									 
											<h4 class="smaller">抱歉，您无权访问该页面，请尝试:</h4>

											<ul class="list-unstyled spaced inline bigger-110 margin-15">
												<li>
													<i class="ace-icon fa fa-hand-o-right blue"></i>
													重新登录已获取最新分配的权限
												</li>

												<li>
													<i class="ace-icon fa fa-hand-o-right blue"></i>
													联系系统管理员请求帮助
												</li>

											 
											</ul>
										</div>

										<hr />
										<div class="space"></div>

										<div class="center">
											<a href="javascript:history.back()" class="btn btn-grey">
												<i class="ace-icon fa fa-arrow-left"></i>
												后退
											</a>

											<a href="/adminht/login.aspx?f=exit" class="btn btn-primary">
												<i class="ace-icon fa fa-refresh"></i>
												重新登录
											</a>
										</div>
									</div>
								</div>

								<!-- /section:pages/error -->

								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
 
</asp:Content>

