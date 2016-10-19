<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_fordemohome.ascx.cs" Inherits="adminht_corepage_WUC_fordemohome" %>


<!-- 报修申请超过24小时未接收的单据数量提示 -->
<div class="row bxsqts_zzz hidden">
							<div class="col-xs-12 col-sm-12">
                                <div class="alert alert-block alert-warning">
									<button type="button" class="close" data-dismiss="alert">
										<i class="ace-icon fa fa-times"></i>
									</button>

									<i class="ace-icon fa fa-exclamation-triangle red"></i>
									 
										请注意：有<span id="bxsqts_chao24">0</span>个报修申请单已超过24小时未接收处理！
								 
                                     
	 
</div> 
                                </div></div>





<div class="row">
							<div class="col-xs-12 col-sm-12">


                                   <%
            if(qx_zysj)
            {
                %>

								<div class="row">
									 
									<div class="col-sm-7 infobox-container">
                                                                             <!-- 重要提示 -->
<div class="alert alert-block alert-success">
								<%--	<button type="button" class="close" data-dismiss="alert">
										<i class="ace-icon fa fa-times"></i>
									</button>--%>

									<i class="ace-icon fa fa-check green"></i>
									您好，
									<strong class="red2">
										欢迎使用威高售后服务系统，版本号V1.0。
									</strong>
                                     
	 
</div> 
          

           
										<!-- #section:pages/dashboard.infobox -->
										<div class="infobox infobox-green">
											<div class="infobox-icon">
												<i class="ace-icon fa fa-recycle"></i>
											</div>

											<div class="infobox-data">
												<span class="infobox-data-number" id="zysj_danbeng_返修设备种类_本月">--</span>
												<div class="infobox-content">返修设备种类</div>
											</div>

											<!-- #section:pages/dashboard.infobox.stat -->
											<div class="badge badge-success">
												本月
											</div>

											<!-- /section:pages/dashboard.infobox.stat -->
										</div>

										<div class="infobox infobox-blue">
											<div class="infobox-icon">
												<i class="ace-icon fa fa-recycle"></i>
											</div>

											<div class="infobox-data">
												<span class="infobox-data-number"  id="zysj_danbeng_返修设备种类_上月">--</span>
												<div class="infobox-content">返修设备种类</div>
											</div>

											<div class="badge badge-success">
												上月
											</div>
										</div>


                                        										<div class="infobox infobox-red">
											<div class="infobox-icon">
												<i class="ace-icon fa fa-shield"></i>
											</div>

											<div class="infobox-data">
												<span class="infobox-data-number" id="zysj_danbeng_返修设备种类_累积">--</span>
												<div class="infobox-content">返修设备种类</div>
											</div>
                                            <div class="badge badge-success">
												累积
											</div>
										</div>

										 

										<div class="infobox infobox-blue2" style="padding:0px; background:url(/mytutu/qdl.jpg) no-repeat center center;">
                                            
                                           <div class="infobox-data row-xs-12"><div class="col-sm-6 col-xs-6 text-center" id="qd_sh">售后:--/--</div><div class="col-sm-6 col-xs-6 text-center" id="qd_hd">华东:--/--</div><div class="col-sm-6 col-xs-6 text-center"  id="qd_hb">华北:--/--</div><div class="col-sm-6 col-xs-6 text-center" id="qd_hz">华中:--/--</div><div class="col-sm-6 col-xs-6 text-center" id="qd_hn">华南:--/--</div><div class="col-sm-6 col-xs-6 text-center" id="qd_xn">西南:--/--</div></div>

											<%--<div class="infobox-progress">
												<!-- #section:pages/dashboard.infobox.easypiechart -->
                                                
												<div class="easy-pie-chart percentage"  id="zysj_danbeng_签到率" data-percent="0.00" data-size="46">
													<span class="percent">--</span>%
												</div>
                                                   
												<!-- /section:pages/dashboard.infobox.easypiechart -->
											</div>

											<div class="infobox-data">
												<span class="infobox-text"><a href="/adminht/corepage/qiandao/list_qd.aspx?bm=all">签到率<span id="zysj_danbeng_签到率来源">--/--</span></a></span>

												<div class="infobox-content">
													<span class="bigger-110">~</span>
													 售后、销售
												</div>
											</div> --%>

										 
										</div>

										<div class="infobox infobox-orange">
											<div class="infobox-icon">
												<i class="ace-icon fa fa-exclamation-triangle"></i>
											</div>

											<div class="infobox-data">
												<span class="infobox-data-number" id="zysj_danbeng_服务报告金额_本年">--</span>
												<div class="infobox-content">服务报告金额</div>
											</div>
											<div class="badge badge-success">
												年度
											</div>
										</div>
                                        <div class="infobox infobox-pink">
											<div class="infobox-icon">
												<i class="ace-icon fa fa-exclamation-triangle"></i>
											</div>

											<div class="infobox-data">
												<span class="infobox-data-number" id="zysj_danbeng_服务报告金额_上月">--</span>
												<div class="infobox-content">服务报告金额</div>
											</div>
											<div class="badge badge-success">
												上月
											</div>
										</div>


        
									 
									</div>

									<div class="vspace-12-sm"></div>

									<div class="col-sm-5">
										<div class="widget-box">
											<div class="widget-header widget-header-flat widget-header-small">
												<h5 class="widget-title">
													<i class="ace-icon fa fa-signal"></i>
													维修设备排行
												</h5>

												<div class="widget-toolbar no-border">
											<%--		<div class="inline dropdown-hover">
														<button class="btn btn-minier btn-primary">
															本周
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
														</button>

														<ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
															<li class="active">
																<a href="#" class="blue">
																	<i class="ace-icon fa fa-caret-right bigger-110">&nbsp;</i>
																	本周
																</a>
															</li>

															<li>
																<a href="#">
																	<i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
																	上周
																</a>
															</li>

															<li>
																<a href="#">
																	<i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
																	本月
																</a>
															</li>

															<li>
																<a href="#">
																	<i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
																	上月
																</a>
															</li>
														</ul>
													</div>--%>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main">
													<!-- #section:plugins/charts.flotchart -->
                                                    <div id="loadingpie_tu"><i class="ace-icon fa fa-spinner fa-spin orange bigger-300" id="editloadinfo"></i></div>
													<div id="piechart-placeholder"></div>
                                                     
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->
								</div><!-- /.row -->

								<!-- #section:custom/extra.hr -->
								<div class="hr  hr-dotted"></div>


                                             <%
            }
            %>
                                                <%
            if(qx_zysj)
            {
                %>
								<!-- /section:custom/extra.hr -->
								<div class="row">

        

									<div class="col-sm-6">
										<div class="widget-box transparent">
											<div class="widget-header widget-header-flat">
												<h4 class="widget-title lighter">
													<i class="ace-icon fa fa-star orange"></i>
													本季度市场概览
												</h4>

												<div class="widget-toolbar">
													<a href="#" data-action="collapse">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main no-padding">
													<table class="table table-bordered table-striped">
														<thead class="thin-border-bottom">
															<tr>
																<th>
																	<i class="ace-icon fa fa-caret-right blue"></i>大区
																</th>

																<th>
																	<i class="ace-icon fa fa-caret-right blue"></i>发货总金额
																</th>

																<th class="hidden-480">
																	<i class="ace-icon fa fa-caret-right blue"></i>服务报告数量
																</th>
															</tr>
														</thead>

														<tbody>
															<tr>
																<td>华北大区</td>

																<td>
														 
																	<b class="red liess_bb" >--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb" >--</span>
																</td>
															</tr>

															<tr>
																<td>华东大区</td>

																<td>
																	<b class="red liess_bb">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb">--</span>
																</td>
															</tr>

															<tr>
																<td>华南大区</td>

																<td>
																	<b class="red liess_bb">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb">--</span>
																</td>
															</tr>

															<tr>
																<td>华中大区</td>

																<td>
																	 
																	<b class="red liess_bb">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb">--</span>
																</td>
															</tr>

															<tr>
																<td>西南大区</td>

																<td>
																	<b class="red liess_bb">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb">--</span>
																</td>
															</tr>
														</tbody>
													</table>
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->

                                    <div class="col-sm-6">
										<div class="widget-box transparent">
											<div class="widget-header widget-header-flat">
												<h4 class="widget-title lighter">
													<i class="ace-icon fa fa-star orange"></i>
													本月度市场概览
												</h4>

												<div class="widget-toolbar">
													<a href="#" data-action="collapse">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main no-padding">
													<table class="table table-bordered table-striped">
														<thead class="thin-border-bottom">
															<tr>
																<th>
																	<i class="ace-icon fa fa-caret-right blue"></i>大区
																</th>

																<th>
																	<i class="ace-icon fa fa-caret-right blue"></i>发货总金额
																</th>

																<th class="hidden-480">
																	<i class="ace-icon fa fa-caret-right blue"></i>服务报告数量
																</th>
															</tr>
														</thead>

														<tbody>
															<tr>
																<td>华北大区</td>

																<td>
														 
																	<b class="red liess_bb_yue" >--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb_yue" >--</span>
																</td>
															</tr>

															<tr>
																<td>华东大区</td>

																<td>
																	<b class="red liess_bb_yue">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb_yue">--</span>
																</td>
															</tr>

															<tr>
																<td>华南大区</td>

																<td>
																	<b class="red liess_bb_yue">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb_yue">--</span>
																</td>
															</tr>

															<tr>
																<td>华中大区</td>

																<td>
																	 
																	<b class="red liess_bb_yue">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb_yue">--</span>
																</td>
															</tr>

															<tr>
																<td>西南大区</td>

																<td>
																	<b class="red liess_bb_yue">--</b>
																</td>

																<td class="hidden-480">
																	<span class="label label-info arrowed-right arrowed-in liess_bb_yue">--</span>
																</td>
															</tr>
														</tbody>
													</table>
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->

								</div><!-- /.row -->
 

                                    <div class="hr  hr-dotted"></div>

                                                               <%
            }
            %>
 

                            

                                <div class="row">

                                    <div class="col-sm-12">
										<div class="widget-box transparent">
											<div class="widget-header widget-header-flat">
												<h4 class="widget-title lighter">
													<i class="ace-icon fa fa-signal"></i>
													我的工作台
												</h4>

												<div class="widget-toolbar">
													<a href="#" data-action="collapse">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>
												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main padding-4">
													 


                                                    <div class="space-12"></div>

                                                    <div class="row">
									                  
                                                        <%
                                                            if (dtgzt != null)
                                                            {
                                                                for (int i = 0; i < dtgzt.Rows.Count; i++)
                                                                {
                                                                    if (i < 4)
                                                                    {
                                                                    %>
                                                           <div class="col-sm-3">
									  <div class="well">
											<h4 class="green smaller lighter"><a class="red" href="<%=dtgzt.Rows[i]["Mdizhi"].ToString() %>"><%=dtgzt.Rows[i]["Mbiaoti"].ToString() %></a><a href="/adminht/corepage/bas/edit_mygzt.aspx?idforedit=gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>&fff=1"><i id="gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>" class="ace-icon fa fa-pencil-square-o align-top bigger-125 pull-right inline" style="cursor:pointer"></i></a></h4>
											<%=dtgzt.Rows[i]["Mbeiwanglu"].ToString() %>
										</div>
									</div>

                                                        <%
                                                                    }
                                                                }
                                                            }
                                                             %>
                                                               
								</div> <div class="row">
									                  
                                               <%
                                                            if (dtgzt != null)
                                                            {
                                                                for (int i = 0; i < dtgzt.Rows.Count; i++)
                                                                {
                                                                    if (i > 3)
                                                                    {
                                                                    %>
                                                           <div class="col-sm-3">
									  <div class="well">
											<h4 class="green smaller lighter"><a class="orange" href="<%=dtgzt.Rows[i]["Mdizhi"].ToString() %>"><%=dtgzt.Rows[i]["Mbiaoti"].ToString() %></a><a href="/adminht/corepage/bas/edit_mygzt.aspx?idforedit=gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>&fff=1"><i id="gzt_<%=UserSession.唯一键%>_<%=i.ToString() %>" class="ace-icon fa fa-pencil-square-o align-top bigger-125 pull-right inline" style="cursor:pointer"></i></a></h4>
											<%=dtgzt.Rows[i]["Mbeiwanglu"].ToString() %>
										</div>
									</div>

                                                        <%
                                                                    }
                                                                }
                                                            }
                                                             %>                
								</div>




												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->
									</div><!-- /.col -->
								</div><!-- /.row -->





								<!-- PAGE CONTENT ENDS -->
							</div><!-- /.col -->
						</div><!-- /.row -->






