<%@ Page Language="C#" AutoEventWireup="true" CodeFile="printp_fwbg.aspx.cs" Inherits="adminht_corepage_fwbg_printp_fwbg" %>

<!DOCTYPE html>

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="/assets/css/bootstrap.css" />
    <link rel="stylesheet" href="/assets/css/font-awesome.css" />
    <link rel="stylesheet" href="/assets/css/jquery-ui.custom.css" />
    <link rel="stylesheet" href="/assets/css/chosen.css" />

        <!-- text fonts -->
    <link rel="stylesheet" href="/assets/css/ace-fonts.css" />

    <!-- ace styles -->
    <link rel="stylesheet" href="/assets/css/ace.css" class="ace-main-stylesheet" />

    <!--[if lte IE 9]>
			<link rel="stylesheet" href="/assets/css/ace-part2.css" class="ace-main-stylesheet" />
		<![endif]-->

    <!--[if lte IE 9]>
		  <link rel="stylesheet" href="/assets/css/ace-ie.css" />
		<![endif]-->

    <!-- inline styles related to this page -->

    <!-- ace settings handler -->
    <script src="/assets/js/ace-extra.js"></script>

    <!-- HTML5shiv and Respond.js for IE8 to support HTML5 elements and media queries -->

    <!--[if lte IE 8]>
		<script src="/assets/js/html5shiv.js"></script>
		<script src="/assets/js/respond.js"></script>
		<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div class="PrintArea_F">
<div class="page-content ">    <div class="page-header">
							<h1>
								服务报告
								<small>
									<i class="ace-icon fa fa-angle-double-right"></i>
									来自威高售后，时间<%=dsr.Tables["数据记录"].Rows[0]["Gaddtime"].ToString() %>
								</small>
							</h1>
						</div><!-- /.page-header -->
    <div class="row"> 

        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">报告单号：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["GID"].ToString() %></div>        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">服务类型：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gfwlx"].ToString() %></div>
    </div>    <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>     <div class="row"> 

        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">工程师：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gtianxieren_name"].ToString() %></div>        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">维保类型：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gbylx"].ToString() %></div>
    </div>     <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>    <div class="row"> 

        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">报修日期：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gsbtime"].ToString() %></div>        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">客户名称：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["YYname"].ToString() %></div>
    </div>     <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>     <div class="row"> 

        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">科室：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gkeshi"].ToString() %></div>        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">客户联系人：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Glianxiren"].ToString() %></div>
    </div>      <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>    <div class="row"> 
        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2  text-right">客户要求：</div><div class="col-sm-10 col-xs-10 col-sm-10 col-lg-10  "><%=dsr.Tables["数据记录"].Rows[0]["Gkehuyaoqiu"].ToString() %></div>  
    </div>    <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>    <div class="row"> 
        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2  text-right">故障摘要：</div><div class="col-sm-10 col-xs-10 col-sm-10 col-lg-10  "><%=dsr.Tables["数据记录"].Rows[0]["Gguzhang_a"].ToString() %></div>  
    </div>    <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>    <div class="row"> 
        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2  text-right">过程摘要：</div><div class="col-sm-10 col-xs-10 col-sm-10 col-lg-10   "><%=dsr.Tables["数据记录"].Rows[0]["Gguocheng_a"].ToString() %></div>  
    </div>    <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>    <div class="row"> 

        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">开工时间：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gkaigongtime"].ToString() %></div>        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2 text-right ">完工时间：</div><div class="col-sm-4 col-xs-4 col-sm-4 col-lg-4 text-left "><%=dsr.Tables["数据记录"].Rows[0]["Gwangongtime"].ToString() %></div>
    </div>      <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>       <h5>设备信息列表</h5>    
										<table class="table  ">
											<thead>
												<tr>
 
													<th>序列号</th>
                                                    <th>名称</th>
                                                    <th>规格</th>
                                                    <th>保修截止</th>
                                                
												</tr>
											</thead>

											<tbody>
                                                 <%for (int i = 0; i < dsr.Tables["设备子表"].Rows.Count; i++)
                                                        {
                                                        %>
												<tr>
                                                   
											        <td><%=dsr.Tables["设备子表"].Rows[i]["sb_SID"].ToString() %></td>
													<td><%=dsr.Tables["设备子表"].Rows[i]["sbmingcheng"].ToString() %></td>
                                                    <td><%=dsr.Tables["设备子表"].Rows[i]["sbguige"].ToString() %></td>
												    <td><%=dsr.Tables["设备子表"].Rows[i]["baoxiu_sss"].ToString() %></td>
                                               
												</tr>
                                                <%} %>
 
											</tbody>
										</table>
								     <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>           <h5>配件信息列表</h5>    
										<table class="table  ">
											<thead>
												<tr>
 
													<th>编号</th>
                                                    <th>名称</th>
                                                    <th>规格</th>
                                                    <th>数量</th>
                                                    <th>保修截止</th>
												</tr>
											</thead>

											<tbody>
                                                <%for (int i = 0; i < dsr.Tables["零件子表"].Rows.Count; i++)
                                                        {
                                                        %>
												<tr>
											        <td><%=dsr.Tables["零件子表"].Rows[i]["lj_LID"].ToString() %></td>
													<td><%=dsr.Tables["零件子表"].Rows[i]["ljmingcheng"].ToString() %></td>
                                                    <td><%=dsr.Tables["零件子表"].Rows[i]["ljxinghao"].ToString() %></td>
												    <td><%=dsr.Tables["零件子表"].Rows[i]["ljshuliang"].ToString() %></td>
                                                    <td><%=dsr.Tables["零件子表"].Rows[i]["baoxiu_sss"].ToString() %></td>
                                                 
												</tr>
                                                  <%} %>
                                                
 
											</tbody>
										</table>     <div class=" space-6" style="border:0px;border-bottom:1px solid #f1f1f1;"></div>        <div class="row"> 
        <div class="col-sm-2 col-xs-2 col-sm-2 col-lg-2  text-right">签字栏：</div><div class="col-sm-10 col-xs-10 col-sm-10 col-lg-10  "></div>  
    </div></div>
            </div>
    </form>



        <!-- basic scripts -->

    <!--[if !IE]> -->
    <script type="text/javascript">
        window.jQuery || document.write("<script src='/assets/js/jquery-2.1.1.min.js'>" + "<" + "/script>");
    </script>

    <!-- <![endif]-->

    <!--[if IE]>
<script type="text/javascript">
 window.jQuery || document.write("<script src='/assets/js/jquery-1.11.1.min.js'>"+"<"+"/script>");
</script>
<![endif]-->
    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='/assets/js/jquery.mobile.custom.js'>" + "<" + "/script>");
    </script>
    <script src="/assets/js/bootstrap.js"></script>

    <!-- page specific plugin scripts -->

    <!--[if lte IE 8]>
		  <script src="/assets/js/excanvas.js"></script>
		<![endif]-->
    <script src="/assets/js/jquery-ui.custom.js"></script>
    <script src="/assets/js/jquery.ui.touch-punch.js"></script>
            <script src="/assets/js/bootbox.js"></script>
    <script src="/assets/js/fuelux/fuelux.spinner.js"></script>
		<script src="/assets/js/jquery.autosize.js"></script>


		<script src="/assets/js/bootstrap-tag.js"></script>



    <!-- ace scripts -->
    <script src="/assets/js/ace/elements.scroller.js"></script>
    <script src="/assets/js/ace/elements.typeahead.js"></script>
    <script src="/assets/js/ace/elements.spinner.js"></script>
    <script src="/assets/js/ace/elements.aside.js"></script>
    <script src="/assets/js/ace/ace.js"></script>
 
    <script src="/assets/js/jquery.PrintArea.js"></script>

    	<!-- 打印处理 -->
  <script>
 
      function beginPrint_go(dayinquyu)
      {
          var close = false; //是否自动弹窗关闭
          var extraCss = "";//扩展样式

          //打印区域
  
          var print = "." + dayinquyu;
         
          //携带属性
          var keepAttr = [];
          keepAttr.push("class");
          keepAttr.push("id");
          keepAttr.push("style");

          //加入头标记
          var headElements = true ? '<meta charset="utf-8" />,<meta http-equiv="X-UA-Compatible" content="IE=edge"/>' : '';

          var options = {  popClose: close, extraCss: extraCss, retainAttr: keepAttr, extraHead: headElements };

          try
          {
              $(print).printArea(options);
          }
          catch(e) {;}
         
      }
      jQuery(function ($) {
   
          //beginPrint_go('PrintArea_F');

        
    });

  </script>
</body>
</html>
