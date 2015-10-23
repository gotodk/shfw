<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_content_onlygrid.ascx.cs" Inherits="pucu_wuc_content_onlygrid" %>
     <div class="row">
        <div class="col-xs-12">

            <div id="zheshichart" class="hidden">


                <div class="widget-box">
											<div class="widget-header widget-header-flat widget-header-small">
												<h5 class="widget-title">
													<i class="ace-icon fa fa-signal"></i>
													图表
												</h5>

												<div class="widget-toolbar no-border">
													<a href="#" data-action="collapse" id="zhedie_bbt">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>

												</div>
											</div>

											<div class="widget-body">
												<div class="widget-main">
													<!-- #section:plugins/charts.flotchart -->
											 <div id="piechart-placeholder"></div> 

											 <div id="sales-charts"></div>
                                  
										 
												</div><!-- /.widget-main -->
											</div><!-- /.widget-body -->
										</div><!-- /.widget-box -->



            </div>



            <!-- PAGE CONTENT BEGINS -->
            <%
                string is_hidden = "";
                if (dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_open"].ToString() == "1")
                {
                    is_hidden = "";
                }
                else
                {
                    is_hidden = "hidden";
                }
                 %>
            <form class="form-inline well well-sm <%=is_hidden %>" id="mysearchtop">
            <%
                if (dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_open"].ToString() == "1")
                {


                 %>
          

              <%
                  for(int i = 1; i <= 3;i++)
                  { 
                   %>
              <%
                  if(dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_type_"+i].ToString() == "输入框")
                  { 
                   %>
                              <label><%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_showname_"+i].ToString() %>：</label>
                <input class="form-control search-query" type="text" id="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>" name="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>" />
                <%   } %>

 <%
                  if(dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_type_"+i].ToString() == "时间段")
                  { 
                   %>
                      <label><%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_showname_"+i].ToString() %>：</label>

                <div class="input-daterange input-group">
                    <input class="form-control date-picker" id="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>1" name="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>1" type="text" />
                    <span class="input-group-addon">
                        <i class="fa fa-exchange"></i>
                    </span>
                    <input class="form-control date-picker" id="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>2" name="<%=dsFPZ.Tables["报表配置主表"].Rows[0]["SRE_idname_"+i].ToString() %>2" type="text" />
                </div>
                <%   } %>
                
                 <%   } %>
      

         
            <%   } %>

                          <button type="button" class="btn btn-purple btn-sm" id="MybtnSearch">
                    <i class="ace-icon fa fa-search bigger-110"></i>搜索
                </button>
               </form> 



            <div id="zheshiliebiaoquyu"></div>



            <script type="text/javascript">
                var $path_assets = "/assets";//this will be used in gritter alerts containing images
            </script>

            <!-- PAGE CONTENT ENDS -->
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->