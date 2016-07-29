<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="cyhq_list_s.aspx.cs" Inherits="cyhq_list_s" %>

<%@ Register Src="~/adminht/corepage/qiandao/qd_WUC_forrili.ascx" TagPrefix="uc1" TagName="qd_WUC_forrili" %>
<%@ Register Src="~/adminht/corepage/qiandao/qd_WUC_forrili_js.ascx" TagPrefix="uc1" TagName="qd_WUC_forrili_js" %>



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
										<table id="sample-table-1" style="TABLE-LAYOUT:fixed;WORD-BREAK:break-all" class="table table-striped table-bordered table-hover ">
											<thead>
												<tr>
												 
													<th>会签标题</th>
												 
													<th style=" width: 75px">参与情况</th>

												 
												</tr>
											</thead>

											<tbody>
                                                <% for (int i = 0; i < dsr.Tables["数据记录"].Rows.Count; i++)
                                                    { %>
												<tr>
											 

													<td style="vertical-align:Middle">
														<h5><a href="/adminht/corepage/huiqian/cyhq.aspx?idforedit=<%=dsr.Tables["数据记录"].Rows[i]["QID"].ToString() %>&fff=1"><%=dsr.Tables["数据记录"].Rows[i]["Qzhuti"].ToString() %></a></h5> 
                                                        <%=dsr.Tables["数据记录"].Rows[i]["Qaddtime"].ToString() %>由<%=dsr.Tables["数据记录"].Rows[i]["Qcjr_name"].ToString()+"["+dsr.Tables["数据记录"].Rows[i]["Qcjr_bumen"].ToString() + "]" %>发起
													</td>
													 

													<td style="vertical-align:Middle">
														<span class="label label-sm label-warning"><%=dsr.Tables["数据记录"].Rows[i]["canyuqingkuang"].ToString() %></span>
													</td>

											 
												</tr>
                                                <% } %>
											 
											</tbody>
										</table>
									</div><!-- /.span -->
								</div>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
  

</asp:Content>

