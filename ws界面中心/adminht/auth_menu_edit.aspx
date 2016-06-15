<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_menu_edit.aspx.cs" Inherits="auth_menu_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
                  <form id="form1" runat="server">
                     <div class="row">
       
                         <div class="col-sm-7 col-xs-12">
                             <asp:Label ID="errmsg"  Text="" runat="server"></asp:Label>
                             <div class="widget-box widget-color-green2">
											<div class="widget-header">
												<h4 class="widget-title lighter smaller">节点编辑</h4>
                                                 <div class="widget-toolbar no-border">
                                      
                                                        
                                                        名称： [ <asp:Label ID="sh_SortName"  Text="根节点" runat="server"></asp:Label> ] ------  SortID： [ <asp:Label ID="sh_SortID"  Text="0" runat="server"></asp:Label> ] 
                                 </div>
											</div>
                                
											<div class="widget-body">
												<div class="widget-main">
											 

                                                
                                                    <h3 class="header smaller lighter blue">修改当前节点附加数据</h3>
                                                     
                                                                  <div class="row" id="xiugaiquyu">
                                <div class="col-sm-12">
                                    <div class="form-horizontal" role="form" id="myform1">
                                        <!-- #section:elements.form -->
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_SortID">SortID：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_SortID" runat="server" Text="0" ReadOnly="true"  CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_SortName">节点名：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_SortName" runat="server" data-rel="tooltip" placeholder="填写节点名称…" title="节点用于显示的名称" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_url">链接地址：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_m_url" runat="server" data-rel="tooltip" placeholder="填写节点链接url…" title="新节点链接url" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_url_formenu_g">高亮跟踪：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_m_url_formenu_g" runat="server" data-rel="tooltip" placeholder="填写高亮等效地址关键字…" title="高亮等效地址关键字，★隔开" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_tip">tip提示：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_m_tip" runat="server" data-rel="tooltip" placeholder="填写节点tip提示…" title="新节点tip提示" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_tag">打开方式：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_m_tag" runat="server" data-rel="tooltip" placeholder="填写节点链接打开方式…" title="新节点链接打开方式(_blank,_self,_parent,_top)" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_ico">图标：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_m_ico" runat="server" data-rel="tooltip" placeholder="填写节点图标…" title="使用图标样式表名称,须查阅aceadmin自带图标" CssClass="col-xs-12 col-sm-9 "></asp:TextBox>
                          
                                            </div>

                                        </div>


                                          <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_m_show">是否隐藏：</label>

                                            <div class="col-sm-9">

                                                <div class="no-padding-left radio col-xs-12 col-sm-10">
                                                    <label>
                                                       <asp:RadioButton ID="ee_m_show1" GroupName="ee_m_show" runat="server" value="不隐藏" class="ace" />
                                                        <span class="lbl">不隐藏</span>
                                                    </label>
                                                    <label>
                                                        <asp:RadioButton ID="ee_m_show0"  GroupName="ee_m_show" runat="server" value="隐藏" class="ace" />
                                                        <span class="lbl">隐藏</span>
                                                    </label>
                                                </div>
                                          
                                            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-12">




                                            <label class="col-sm-3 control-label"></label>

                                            <div class="col-sm-9">
                                                <div class="col-xs-12 col-sm-10">


                                                           <asp:Button ID="xiugai" runat="server" CssClass="btn btn-sm btn-pink" Text="保存修改" OnClick="editjiedian_Click" />

                                                </div>

                                            </div>


                                        </div>




                                    </div>
                                </div>
                                <!-- /.col -->
                            </div>
                                                      
 
                                                         
                                <h3 class="header smaller lighter blue">当前节点下增加新节点</h3>
                                                    <p>
                                                    <asp:TextBox ID="addnewjiedian_name" runat="server" data-rel="tooltip" placeholder="填写新节点名称…" title="新节点用于显示的名称" CssClass="col-xs-12 col-sm-5 "></asp:TextBox>
&nbsp;<asp:Button ID="tianjia" runat="server" CssClass="btn btn-sm btn-pink" Text="添加" OnClick="editjiedian_Click" />
                                                    </p>
                                                   
                                                    <h3 class="header smaller lighter blue">移动当前节点到</h3>
                                                    <p>
                                                        <asp:TextBox ID="movenewsid" runat="server" data-rel="tooltip" placeholder="填写新父级SortID…" title="录入正确的要转移到的新父级SortID" CssClass="col-xs-12 col-sm-5 "></asp:TextBox>
&nbsp;<asp:Button ID="yidong" runat="server" CssClass="btn btn-sm btn-pink" Text="移动"  OnClick="editjiedian_Click" />
                                                    </p>
                                                    <h3 class="header smaller lighter blue">其他操作</h3>
                                                    <p>
                                                      <asp:Button ID="shengchengxml" runat="server" CssClass="btn btn-sm btn-pink" Text="生成xml" OnClick="editjiedian_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="shang" runat="server" CssClass="btn btn-sm btn-pink" Text="上移排序" OnClick="editjiedian_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="xia" runat="server" CssClass="btn btn-sm btn-pink" Text="下移排序" OnClick="editjiedian_Click" />  &nbsp;&nbsp;&nbsp;<asp:Button ID="shanchu" runat="server" CssClass="btn btn-sm btn-pink" Text="删除当前节点" OnClientClick="javascript:return confirm('危险操作无法恢复！彻底删除当前节点及其所有子节点，确定吗？');"  OnClick="editjiedian_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="zhengli" runat="server" CssClass="btn btn-sm btn-pink" Text="修正全部节点" OnClientClick="javascript:return confirm('危险操作无法恢复！重新整理全部节点排序和父级关系，确定吗？');"  OnClick="editjiedian_Click" />
                                                    


												</div>
											</div>
										</div>
                    
                         </div>


                                           <div class="col-sm-5 col-xs-12">
                             <div class="widget-box widget-color-pink">
											<div class="widget-header">
												<h4 class="widget-title lighter smaller"><%=tbshowname %>[<asp:Label ID="dbtbname" runat="server" Text=""></asp:Label>]</h4>
                                               <div class="widget-toolbar no-border">
 

													<button class="btn btn-xs bigger btn-yellow dropdown-toggle" data-toggle="dropdown">
														切换数据表
														<i class="ace-icon fa fa-chevron-down icon-on-right"></i>
													</button>

													<ul class="dropdown-menu dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
														<li>
															<a href="auth_menu_edit.aspx?tb=auth_menu_b">auth_menu_b(后台菜单)</a>
														</li>

														<li>
															<a href="auth_menu_edit.aspx?tb=auth_menu_f">auth_menu_f(前台菜单)</a>
														</li>

													 
													</ul>
												</div>


											</div>

											<div class="widget-body">
												<div class="widget-main">
													<asp:TreeView ID="TV" runat="server" ShowLines="True">
                <NodeStyle Font-Size="Small" NodeSpacing="0px" />
        </asp:TreeView>
												</div>
											</div>
										</div>



                                         
                         </div>
                     </div>


                    
                  </form>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
    <script src="/assets/js/jquery.validate.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>

 
 
    <script type="text/javascript">

 

        jQuery(function ($) {
 
            if ($("#<%=sh_SortID.ClientID%>").text() == "0" || $("#<%=sh_SortID.ClientID%>").text() == "" || $("#<%=dbtbname.ClientID%>").text() == "") {
                $("#xiugaiquyu").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;根节点无法修改节点数据，请先选择节点。");
            }
           
            


        });

             </script>

       <script type="text/javascript">
           jQuery(function ($) {
 

               //启用悬浮简述tooltip
               $('[data-rel=tooltip]').tooltip({ container: 'body' });
               $('[data-rel=popover]').popover({ container: 'body' });
 

           });



    </script>
</asp:Content>

