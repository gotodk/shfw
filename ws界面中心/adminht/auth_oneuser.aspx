<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_oneuser.aspx.cs" Inherits="auth_oneuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
     <link rel="stylesheet" href="/assets/css/select2.css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
                 <form id="form1" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <!-- PAGE CONTENT BEGINS -->
            <div class="row">
                <div class="col-xs-12">



                    <div class="widget-box" id="quyu_zhao" runat="server">
											<div class="widget-header widget-header-small">
												<h5 class="widget-title lighter">精确输入完整信息并进入配置</h5>
											</div>

											<div class="widget-body">
												<div class="widget-main">
													<div class="form-search">
														<div class="row">
															<div class="col-xs-12 col-sm-6">
																<div class="input-group">
																	<span class="input-group-addon">
																		<i class="ace-icon fa fa-edit"></i>
																	</span>

																	 
                                                                    <asp:TextBox ID="idorname" runat="server" data-rel="tooltip" placeholder="输入用户账号或UAid" title="非模糊搜索，要完整输入" CssClass="form-control search-query"></asp:TextBox>
																	<span class="input-group-btn">

                                                                       <span class="ace-icon fa fa-search icon-on-right bigger-110"></span> <asp:Button ID="kaishizhao" runat="server" CssClass="btn btn-purple btn-sm"  Text="搜索并配置" OnClick="kaishizhao_Click" />
																		
																	</span>
																</div>
															</div>
														</div>
													</div>
                                                    <div id="errmsg" class="red" runat="server"></div>
												</div>
											</div>
										</div>











                    <!-- #section:elements.tab -->
                    <div class="tabbable"  id="quyu_peizhi" runat="server" visible="false">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active">
                                <a data-toggle="tab" href="#addadd">
                                    <i class="green ace-icon fa fa-pencil-square-o bigger-120"></i>
                                    用户权限分配
                                </a>
                            </li>


                        </ul>

                        <div class="tab-content">
                            <div id="addadd" class="tab-pane fade in active">

                                <div class="row">
                                    <div class="col-sm-12">
                                       

                                        <div class="form-horizontal" role="form" id="myform1">
                                        <!-- #section:elements.form -->
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="ee_UAid">UAid：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_UAid" runat="server" Text="0" ReadOnly="true"  CssClass="col-xs-12 col-sm-12 "></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="ee_Uloginname">登陆账号：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_Uloginname" runat="server" ReadOnly="true"  CssClass="col-xs-12 col-sm-12 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Unumber1">后台菜单权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber1" runat="server" SelectionMode="Multiple" class="select2  tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Unumber2">前台导航权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber2" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Unumber3">全局独立权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber3" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Unumber4">特殊权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber4" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Unumber5">备用权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber5" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                            <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="Uingroups">隶属权限组：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Uingroups" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限组名...">
                                                </asp:ListBox>
                                            </div>
                                        </div>

                                            <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="UfinalUnumber">最终权值：</label>
                                            <div class="col-sm-9" runat="server" id="UfinalUnumber_show">
                                                 <asp:TextBox ID="UfinalUnumber" runat="server" ReadOnly="true"  CssClass="col-xs-12 col-sm-12 " TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                            
                                       <div class="clearfix form-actions col-xs-12 col-sm-12">




                                                <label class="col-sm-2 control-label"></label>

                                                <div class="col-sm-10">
                                                    <div class="col-xs-12 col-sm-5">
                                                        <asp:Button ID="addbutton1" runat="server" CssClass="btn btn-info pull-left"  Text="保存修改" OnClick="addbutton1_Click"  />
                                                   

                                                    </div>

                                                </div>


                                            </div>





                                    </div>

                                    </div>
                                    <!-- /.col -->
                                </div>
                                <!-- /.row -->


                            </div>

                            
                        </div>
                    </div>

                    <!-- /section:elements.tab -->
                </div>
                <!-- /.col -->


            </div>
            <!-- /.row -->








            <script type="text/javascript">
                var $path_assets = "/assets";//以备某段js动态调用了这个变量
            </script>

            <!-- PAGE CONTENT ENDS -->
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
 </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
        <script src="/assets/js/select2.js"></script>
        <script type="text/javascript">
            jQuery(function ($) {
                //select2
                $('.select2').css('width', '100%').select2({ allowClear: true })
            });



    </script>
</asp:Content>
