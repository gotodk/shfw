<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_group_edit.aspx.cs" Inherits="auth_group_edit" %>

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
       
                         <div class="col-sm-8 col-xs-12">
                             <asp:Label ID="errmsg"  Text="" runat="server"></asp:Label>
                             <div class="widget-box widget-color-green2">
											<div class="widget-header">
												<h4 class="widget-title lighter smaller">权限组编辑</h4>
                                                 <div class="widget-toolbar no-border">
                                      
                                                        
                                                        权限组名： [ <asp:Label ID="sh_SortName"  Text="未选中" runat="server"></asp:Label> ] ------  SortID： [ <asp:Label ID="sh_SortID"  Text="0" runat="server"></asp:Label> ] 
                                 </div>
											</div>
                                
											<div class="widget-body">
												<div class="widget-main">
											 

                                                
                                                    <h3 class="header smaller lighter blue">配置权限组权限</h3>
                                                     
                                                                  <div class="row" id="xiugaiquyu">
                                <div class="col-sm-12">
                                    <div class="form-horizontal" role="form" id="myform1">
                                        <!-- #section:elements.form -->
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_SortID">组ID：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_SortID" runat="server" Text="0" ReadOnly="true"  CssClass="col-xs-12 col-sm-12 "></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ee_SortName">权限组名：</label>

                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ee_SortName" runat="server" data-rel="tooltip" placeholder="填写权限组名称…" title="权限组用于显示的名称" CssClass="col-xs-12 col-sm-12 "></asp:TextBox>
                          
                                            </div>

                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="Unumber1">后台菜单权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber1" runat="server" SelectionMode="Multiple" class="select2  tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="Unumber2">前台导航权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber2" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="Unumber3">全局独立权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber3" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="Unumber4">特殊权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber4" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="Unumber5">备用权限：</label>
                                            <div class="col-sm-9">
                                                <asp:ListBox ID="Unumber5" runat="server" SelectionMode="Multiple" class="select2 tag-input-style"  data-placeholder="选择多个权限...">
                                                </asp:ListBox>
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
                                                      
 
                                                         
                                          <h3 class="header smaller lighter blue">在当前选中权限组中新增</h3>
                                                    <p>
                                                    <asp:TextBox ID="addnewjiedian_name" runat="server" data-rel="tooltip" placeholder="填写新权限组名称…" title="权限组名称" CssClass="col-xs-12 col-sm-5 "></asp:TextBox>
&nbsp;<asp:Button ID="tianjia" runat="server" CssClass="btn btn-sm btn-pink" Text="添加" OnClick="editjiedian_Click" />
                                                    </p>
                                                   
                                                    <h3 class="header smaller lighter blue">移动当前权限组到</h3>
                                                    <p>
                                                        <asp:TextBox ID="movenewsid" runat="server" data-rel="tooltip" placeholder="填写新父级SortID…" title="录入正确的要转移到的新父级SortID" CssClass="col-xs-12 col-sm-5 "></asp:TextBox>
&nbsp;<asp:Button ID="yidong" runat="server" CssClass="btn btn-sm btn-pink" Text="移动"  OnClick="editjiedian_Click" />
                                                    </p>
                                                    <h3 class="header smaller lighter blue">其他操作</h3>
                                                    <p>
                                                      <asp:Button ID="shang" runat="server" CssClass="btn btn-sm btn-pink" Text="上移排序" OnClick="editjiedian_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="xia" runat="server" CssClass="btn btn-sm btn-pink" Text="下移排序" OnClick="editjiedian_Click" />  &nbsp;&nbsp;&nbsp;<asp:Button ID="shanchu" runat="server" CssClass="btn btn-sm btn-pink" Text="删除当前权限组" OnClientClick="javascript:return confirm('危险操作无法恢复！彻底删除当前权限组及其所有子权限组，确定吗？');"  OnClick="editjiedian_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="zhengli" runat="server" CssClass="btn btn-sm btn-pink" Text="修正" OnClientClick="javascript:return confirm('危险操作无法恢复！重新整理全部权限组排序和父级关系，确定吗？');"  OnClick="editjiedian_Click" />
                                                    


												</div>
											</div>
										</div>
                    
                         </div>


                                           <div class="col-sm-4 col-xs-12">
                             <div class="widget-box widget-color-pink">
											<div class="widget-header">
												<h4 class="widget-title lighter smaller"><%=tbshowname %>[<asp:Label ID="dbtbname" runat="server" Text=""></asp:Label>]</h4>
                                               <div class="widget-toolbar no-border">
 
 
 
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
    <script src="/assets/js/jquery.maskedinput.js"></script>
    <script src="/assets/js/select2.js"></script>


 
 
    <script type="text/javascript">

 
        jQuery(function ($) {
 
            //加载表单数据
 
            if ($("#<%=sh_SortID.ClientID%>").text() == "0" || $("#<%=sh_SortID.ClientID%>").text() == "" || $("#<%=dbtbname.ClientID%>").text() == "") {
                $("#xiugaiquyu").html("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;选中权限组后再修改数据。");
            }
          
            


        });

             </script>

       <script type="text/javascript">
           jQuery(function ($) {
               //select2
               $('.select2').css('width', '100%').select2({ allowClear: true })
        
        


               //启用悬浮简述tooltip
               $('[data-rel=tooltip]').tooltip({ container: 'body' });
               $('[data-rel=popover]').popover({ container: 'body' });
 

           });



    </script>
</asp:Content>

