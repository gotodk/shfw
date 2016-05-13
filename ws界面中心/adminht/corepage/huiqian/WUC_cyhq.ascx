<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_cyhq.ascx.cs" Inherits="WUC_cyhq" %>
 
<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right hidden-480" for="huiqiandan">
                                                    会签单：</label>
                                     
                                                <div class="col-sm-10 col-xs-12">

                                           










<div class="widget-box">
													<div class="widget-header widget-header-flat">
														<h4 class="widget-title smaller">会签单(<%=dsr.Tables["数据记录"].Rows[0]["QID"].ToString() %>)</h4>

												 		<div class="widget-toolbar">

											<%--				<label class="pull-right inline">
													<small class="muted smaller-90">结单:</small>

													<input id="id-button-jiedan" checked="" type="checkbox" class="ace ace-switch ace-switch-5" />
													<span class="lbl middle"></span>
												</label>--%>                                                            
														</div> 
													</div>

													<div class="widget-body">
														<div class="widget-main" style="word-wrap: break-word;word-break: normal;">
                                                            	 
                                                            	<dl id="dt-list-1">
																<dt>主题：</dt>
																<dd><%=dsr.Tables["数据记录"].Rows[0]["Qzhuti"].ToString() %></dd>
																<dt>会签内容</dt>
																<dd><%=dsr.Tables["数据记录"].Rows[0]["Qneirong"].ToString() %></dd>
															 
																<dt>发起人</dt>
																<dd><%=dsr.Tables["数据记录"].Rows[0]["Qcjr_name"].ToString() %>[<%=dsr.Tables["数据记录"].Rows[0]["Qcjr_bumen"].ToString() %>]</dd>
																<dt>结单人</dt>
																<dd><%=dsr.Tables["数据记录"].Rows[0]["Qjiedanren_name"].ToString() %>[<%=dsr.Tables["数据记录"].Rows[0]["Qjiedanren_bumen"].ToString() %>]<input type="text"  class="hidden" id="h_jdr" value="<%=dsr.Tables["数据记录"].Rows[0]["Qjiedanren"].ToString() %>" />
																</dd>
                                                                    <dt>发起时间</dt>
																<dd><%=dsr.Tables["数据记录"].Rows[0]["Qaddtime"].ToString() %></dd>

															</dl>
<%-- <div class="input-group"> 
    									 
   

																<span class="input-group-btn">
                                                                      <select multiple="" data-placeholder="请选择…" class=" select2 tag-input-style   " id="xialakuangduoxuan" name="xialakuangduoxuan">

                                                         <option value='001'>一号</option> <option value='002'>二号</option> <option value='003'>三号</option> <option value='004'>四号</option> <option value='005'>2三号</option> <option value='006'>3三号</option>
                                                    </select>
																	<button class="btn btn-sm btn-info no-radius" type="button">
																		<i class="ace-icon fa fa-share"></i>
																		转发
																	</button>
																</span>
															</div>--%>
														  
                                                   
														
														</div>
													</div>
												</div>













                                                </div>
</div>
<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right hidden-480" for="yijian">
                                                    意见：</label>
                                     
                                                <div class="col-sm-10 col-xs-12">

                                               









<div class="widget-box">
											<div class="widget-header">
												<h4 class="widget-title lighter smaller">
													<i class="ace-icon fa fa-comment blue"></i>
													会签意见
												</h4>
											</div>

											<div class="widget-body">
												<div class="widget-main no-padding">
													<!-- #section:pages/dashboard.conversations -->
													<div class="dialogs "> 
													 
                                                        <%for (int i = 0; i < dsr.Tables["Table1"].Rows.Count; i++)
                                                            { %>
														<div class="itemdiv dialogdiv">
															<div class="user">
																<img  src="<%=dsr.Tables["Table1"].Rows[i]["utouxiang"].ToString() %>">
															</div>

															<div class="body">
																<div class="time">
                                                                     <%

    if (dsr.Tables["Table1"].Rows[i]["YJzhuangtai"].ToString() == "待签")
    {
                                                                         %>
																	<i class="ace-icon fa fa-coffee  "></i>
                                                                   
																	<span class="red"><%=dsr.Tables["Table1"].Rows[i]["YJzhuangtai"].ToString() %></span>
                                                                    <%}
    else
    { %>
                                                                    <i class="ace-icon fa fa-pencil-square-o  "></i>
                                                                   
																	<span class="green"><%=dsr.Tables["Table1"].Rows[i]["YJzhuangtai"].ToString() %></span>

                                                                    <%} %>
																</div>

																<div class="name">
																	<span class="blue"><%=dsr.Tables["Table1"].Rows[i]["YJqianhsuren_name"].ToString() %>[<%=dsr.Tables["Table1"].Rows[i]["YJqianhsuren_bumen"].ToString() %>]</span>
																</div>
																<div class="text"><%=dsr.Tables["Table1"].Rows[i]["YJyijian"].ToString() %></div>
                                                                <div class="text">该会签人由<span class="blue"><%=dsr.Tables["Table1"].Rows[i]["YJlaiyuan_name"].ToString() %>[<%=dsr.Tables["Table1"].Rows[i]["YJlaiyuan_bumen"].ToString() %>]</span>在<%=dsr.Tables["Table1"].Rows[i]["YJlysj"].ToString() %>分配，意见签署时间：<%=dsr.Tables["Table1"].Rows[i]["YJqsshijian"].ToString() %></div>
																<div class="tools">
																	<%--<a href="#" class="btn btn-minier btn-info">
																		<i class="icon-only ace-icon fa fa-share"></i>
																	</a>--%>
                                                                    <input type="text"  class="hidden" name="h_qsr" value="<%=dsr.Tables["Table1"].Rows[i]["YJqianhsuren"].ToString() %>" />
																</div>
															</div>
														</div>
 
                                                        <%} %>
													 
                                                        </div>
													<!-- /section:pages/dashboard.conversations -->
												<%-- <div class="input-group">
																<input placeholder="Type your message here ..." type="text" class="form-control" name="message" />
																<span class="input-group-btn">
																	<button class="btn btn-sm btn-info no-radius" type="button">
																		<i class="ace-icon fa fa-share"></i>
																		Send
																	</button>
																</span>
															</div>--%>
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div>














                                                </div>
</div>

 







 