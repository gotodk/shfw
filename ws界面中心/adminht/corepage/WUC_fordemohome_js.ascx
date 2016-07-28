<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_fordemohome_js.ascx.cs" Inherits="adminht_corepage_WUC_fordemohome_js" %>



		<script src="/assets/js/jquery.easypiechart.js"></script>
        <script src="/assets/js/jquery.sparkline.js"></script>
		<script src="/assets/js/flot/jquery.flot.js"></script>
		<script src="/assets/js/flot/jquery.flot.pie.js"></script>
		<script src="/assets/js/flot/jquery.flot.resize.js"></script>

<script src="/assets/js/jquery.PrintArea.js"></script>

    		<!-- inline scripts related to this page -->
		<script type="text/javascript">
		    jQuery(function ($) {

		        function shuaxin_easy_pie_chart() {
		            $('.easy-pie-chart.percentage').each(function () {
		                var $box = $(this).closest('.infobox');
		                var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
		                var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
		                var size = parseInt($(this).data('size')) || 50;
		                $(this).easyPieChart({
		                    barColor: barColor,
		                    trackColor: trackColor,
		                    scaleColor: false,
		                    lineCap: 'butt',
		                    lineWidth: parseInt(size / 10),
		                    animate: /msie\s*(8|7|6)/.test(navigator.userAgent.toLowerCase()) ? false : 1000,
		                    size: size
		                });
		            })
		        }

			
		 
					                           <%
            if(qx_zysj)
            {
                %>
	
			
			  //flot chart resize plugin, somehow manipulates default browser resize event to optimize it!
			  //but sometimes it brings up errors with normal resize event handlers
			  $.resize.throttleWindow = false;
			
			  var placeholder = $('#piechart-placeholder').css({'width':'90%' , 'min-height':'145px'});
			  var data = [
			    { label: "", data: 0, shuliang: '0', color: "#ffffff" }
			  ];

			  function drawPieChart(placeholder, data, position) {
			 	  $.plot(placeholder, data, {
					series: {
						pie: {
							show: true,
							tilt:0.8,
							highlight: {
								opacity: 0.25
							},
							stroke: {
								color: '#fff',
								width: 2
							},
							startAngle: 2
						}
					},
					legend: {
						show: true,
						position: position || "ne", 
						labelBoxBorderColor: null,
						margin:[-30,15]
					}
					,
					grid: {
						hoverable: true,
						clickable: true
					},
					legend: {
					    show: true, //显示图例  
					    labelFormatter: function legendFormatter(label, series) {
					        return '<div ' +
                                   'style="font-size:8pt;text-align:left;padding:2px;" title="' + label + '">' +
                                   label.substring(0, 10) + '</div>';
					    }
					}
				 })
			  }
                //
			 drawPieChart(placeholder, data);
			
			 /**
			 we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
			 so that's not needed actually.
			 */
			 placeholder.data('chart', data);
			 placeholder.data('draw', drawPieChart);
			
			
			  //pie chart tooltip example
			  var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
			  var previousPoint = null;
			
			  placeholder.on('plothover', function (event, pos, item) {
				if(item) {
					if (previousPoint != item.seriesIndex) {
						previousPoint = item.seriesIndex;
						
						var tip = item.series['label'] + "，<br/>本月维修数量:" + item.series['shuliang'] + "<br/>占比：[" + Math.round(item.series['percent']) + "%]";
						//var tip = item.series['label'] + " : " + item.series['percent'] + '%';
						$tooltip.show().children(0).html(tip);
					}
					$tooltip.css({top:pos.pageY + 10, left:pos.pageX + 10});
				} else {
					$tooltip.hide();
					previousPoint = null;
				}
				
			  });



                			 
                                                  <%
            }
            %>








			
				/////////////////////////////////////
				$(document).one('ajaxloadstart.page', function(e) {
					$tooltip.remove();
				});
			
			
			
			 
		 
				
				//Android's default browser somehow is confused when tapping on label which will lead to dragging the task
				//so disable dragging when clicking on label
				var agent = navigator.userAgent.toLowerCase();
				if("ontouchstart" in document && /applewebkit/.test(agent) && /android/.test(agent))
				  $('#tasks').on('touchstart', function(e){
					var li = $(e.target).closest('#tasks li');
					if(li.length == 0)return;
					var label = li.find('label.inline').get(0);
					if(label == e.target || $.contains(label, e.target)) e.stopImmediatePropagation() ;
				});
		 



			    //============加载统计分析数据(重要数据部分)
				function loadyibiaopan001()
				{
				    $.ajax({
				        type: "POST",
				        url: "/adminht/corepage/WUC_fordemohome_ajax.aspx",
				        data: "hqbz=zysj_pie",
				        dataType: "html",
				        success: function (data) {
				            $("#loadingpie_tu").hide();

				            eval("var redata = [" + data + "]");
				            drawPieChart(placeholder, redata);
				            placeholder.data('chart', redata);
				            placeholder.data('draw', drawPieChart);
				        } 
				    });

				    $.ajax({
				        type: "POST",
				        url: "/adminht/corepage/WUC_fordemohome_ajax.aspx",
				        data: "hqbz=zysj_yixieshuzi",
				        dataType: "html",
				        success: function (data) {
				            if (data != "")
				            {
				                var fengetemp = data.split(',');
				                $.each(fengetemp, function (n, value) {
				                    var ft = value.split(':');
				                    if (ft[0] == "签到率")
				                    {
				                        $("#zysj_danbeng_" + ft[0]).attr("data-percent", ft[1]);
				                        $("#zysj_danbeng_" + ft[0]).html("<span class='percent'>" + ft[1] + "</span>%");
				                        shuaxin_easy_pie_chart();
				                    }
				                    else
				                    {
				                        $("#zysj_danbeng_" + ft[0]).html(ft[1]);
				                    }
				                    
				                });
				            }
				            
				        }
				    });

				    $.ajax({
				        type: "POST",
				        url: "/adminht/corepage/WUC_fordemohome_ajax.aspx",
				        data: "hqbz=zysj_liebiao_001",
				        dataType: "html",
				        success: function (data) {
				            if (data != "") {
				                var fengetemp = data.split(',');
				                var i = 0;
				                $(".liess_bb").each(function (a, kj) {
				                    $(kj).html(fengetemp[i]);
				                    i++;
				                });
				            }

				        }
				    });



				    $.ajax({
				        type: "POST",
				        url: "/adminht/corepage/WUC_fordemohome_ajax.aspx",
				        data: "hqbz=zysj_liebiao_002",
				        dataType: "html",
				        success: function (data) {
				            if (data != "") {
				                var fengetemp = data.split(',');
				                var i = 0;
				                $(".liess_bb_yue").each(function (a, kj) {
				                    $(kj).html(fengetemp[i]);
				                    i++;
				                });
				            }

				        }
				    });

				}

		                           <%
            if(qx_zysj)
            {
                %>
				loadyibiaopan001();
                                                  <%
            }
            %>

  //  			    var data = [
  //{ label: "ddd", data: 10, shuliang: '233', color: "#68BC31" },
  //{ label: "--", data: 20, shuliang: '233', color: "#2091CF" },
  //{ label: "--", data: 30, shuliang: '233', color: "#AF4E96" },
  //{ label: "--", data: 40, shuliang: '233', color: "#DA5430" },
  //{ label: "--", data: 50, shuliang: '233', color: "#FEE074" }
  //  			    ];
    			    
    			   

				

               
			


		 
			
			})
		</script>
    		<!-- 工作台相关 -->
		<script type="text/javascript">
		    jQuery(function ($) {

		      

			})
		</script>