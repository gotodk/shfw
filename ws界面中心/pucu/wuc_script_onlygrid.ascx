<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_script_onlygrid.ascx.cs" Inherits="pucu_wuc_script_onlygrid" %>
  <!-- 附加的body底部本页专属的自定义js脚本 -->
 
    <script src="/assets/js/jqGrid/jquery.jqGrid.min.js"></script>
    <script src="/assets/js/jqGrid/i18n/grid.locale-cn.js"></script>
    <script src="/assets/js/jqGrid/grid.setcolumns.js"></script>
 
    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>
 
		<script src="/assets/js/jquery.easypiechart.js"></script>
		<script src="/assets/js/flot/jquery.flot.js"></script>
		<script src="/assets/js/flot/jquery.flot.pie.js"></script>
		<script src="/assets/js/flot/jquery.flot.resize.js"></script>

<script src="/assets/js/jquery.PrintArea.js"></script>

    <!-- inline scripts related to this page -->


    <!-- **********全局变量配置******** -->
    <script type="text/javascript">
        //配置参数
        var gogoajax1_CanRun = true;//ajax提交防重复
        var url1 = "/ajax_pp_do.aspx";//处理页面
        var jkname_page1 = encodeURIComponent("<%=dsFPZ.Tables["报表配置主表"].Rows[0]["FS_getJK"].ToString()%>");//获取分页数据接口名
        var jkname_del1 = encodeURIComponent("<%=dsFPZ.Tables["报表配置主表"].Rows[0]["FS_delJK"].ToString()%>");//删除数据接口名

        var myDropzone = null;
        //ajax系统错误统一提示
        function errorForAjax(XMLHttpRequest, textStatus, errorThrown) {
            // 通常情况下textStatus和errorThown只有其中一个有值 
            bootbox.alert("抱歉，系统出现问题!");
        };
        //ajax统一guid生成
        function randomnumber() {
            var d = new Date();
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1) + "-" + d.getFullYear() + d.getMonth() + d.getDate() + d.getHours() + d.getMinutes() + d.getSeconds() + d.getMilliseconds();
        }

        //获取url中的参数
        function getUrlParam(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
        function GetPageName() {
            var url = window.location.href;//获取完整URL 
            var tmp = new Array();//临时变量，保存分割字符串 
            tmp = url.split("/");//按照"/"分割 
            var pp = tmp[tmp.length - 1];//获取最后一部分，即文件名和参数 
            tmp = pp.split("?");//把参数和文件名分割开 
            return tmp[0];
        }
    </script>

    <!-- **********输入过程控制处理******** -->
    <script type="text/javascript">
        jQuery(function ($) {

 
            //datepicker plugin初始化
            $('.date-picker').datepicker({ autoclose: true, })
            $('.date-picker').mask('9999-99-99');

            //启用悬浮简述tooltip
            $('[data-rel=tooltip]').tooltip({ container: 'body' });
            $('[data-rel=popover]').popover({ container: 'body' });
 
        });



    </script>

        <!-- **********报表处理******** -->
    <script type="text/javascript">
        var grid_selector = "#grid-table";
        var pager_selector = "#grid-pager";


        jQuery(function ($) {

 
            initjqgrid();

            //快速回车搜索
            $("#mysearchtop").submit(function () {
                $("#MybtnSearch").click();
                return false;
            });
            //自定义搜索事件
            $(document).on('click', "#MybtnSearch", function () {
                var zdy = $('#mysearchtop').serialize();
                var postData = $(grid_selector).jqGrid("getGridParam", "postData");
                $.extend(postData, { mysearchtop: zdy });
                $(grid_selector).jqGrid("setGridParam", { search: true }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid
            });



            //resize to fit page size
            $(window).on('resize.jqGrid', function () {
            
                $(grid_selector).setGridWidth($("#zheshiliebiaoquyu").width() );

                var ss = getPageSize();
 
                $(grid_selector).setGridHeight(ss.WinH - $("#zheshiliebiaoquyu").offset().top - 150);
            });


      

            function initjqgrid() {
             

                var t_guid = "<%=dsFPZ.Tables["报表配置主表"].Rows[0]["FSID"].ToString()%>";
                //重新生成一个新的列表
                $t = $("<table id=\"grid-table\" ></table><div id=\"grid-pager\"></div>");
                $("#zheshiliebiaoquyu").empty().html($t);
                var aj = $.ajax({
                    url: '/pucu/jqgirdjs_for_grid.aspx?guid=' + t_guid,
                    type: 'get',
                    cache: false,
                    dataType: 'html',
                    success: function (data) {
                        //bootbox.alert(data);

                        eval(data);
                         
                        var postData = $(grid_selector).jqGrid("getGridParam", "postData");
                        var zdy = $('#mysearchtop').serialize();
                        $.extend(postData, { mysearchtop: zdy });
                        $.extend(postData, { this_extforinfoFSID: t_guid });
                        $(grid_selector).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据
                        //设置冻结列
                        $(grid_selector).jqGrid('setFrozenColumns');
                       

                    },
                    error: function () {
                        bootbox.alert("加载列表配置失败！");
                    }
                });




            }


        });



    </script>
    <!-- **********jqgrid相关处理******** -->
    <script type="text/javascript">


        function getPageSize() {
 
            var winW, winH;
            if (window.innerHeight) {// all except IE 
                winW = window.innerWidth;
                winH = window.innerHeight;
            } else if (document.documentElement && document.documentElement.clientHeight) {// IE 6 Strict Mode 
                winW = document.documentElement.clientWidth;
                winH = document.documentElement.clientHeight;
            } else if (document.body) { // other 
                winW = document.body.clientWidth;
                winH = document.body.clientHeight;
            }  // for small pages with total size less then the viewport  
            return { WinW: winW, WinH: winH };
        }



        function style_edit_form(form) {

            //update buttons classes
            var buttons = form.next().find('.EditButton .fm-button');
            buttons.addClass('btn btn-sm').find('[class*="-icon"]').hide();//ui-icon, s-icon
            buttons.eq(0).addClass('btn-primary').prepend('<i class="ace-icon fa fa-check"></i>');
            buttons.eq(1).prepend('<i class="ace-icon fa fa-times"></i>')

            buttons = form.next().find('.navButton a');
            buttons.find('.ui-icon').hide();
            buttons.eq(0).append('<i class="ace-icon fa fa-chevron-left"></i>');
            buttons.eq(1).append('<i class="ace-icon fa fa-chevron-right"></i>');
        }



        function style_delete_form(form) {
            var buttons = form.next().find('.EditButton .fm-button');
            buttons.addClass('btn btn-sm btn-white btn-round').find('[class*="-icon"]').hide();//ui-icon, s-icon
            buttons.eq(0).addClass('btn-danger').prepend('<i class="ace-icon fa fa-trash-o"></i>');
            buttons.eq(1).addClass('btn-default').prepend('<i class="ace-icon fa fa-times"></i>')
        }

        function style_search_filters(form) {
            form.find('.delete-rule').val('X');
            form.find('.add-rule').addClass('btn btn-xs btn-primary');
            form.find('.add-group').addClass('btn btn-xs btn-success');
            form.find('.delete-group').addClass('btn btn-xs btn-danger');

        }
        function style_search_form(form) {
            var dialog = form.closest('.ui-jqdialog');
            var buttons = dialog.find('.EditTable')
            buttons.find('.EditButton a[id*="_reset"]').addClass('btn btn-sm btn-info').find('.ui-icon').attr('class', 'ace-icon fa fa-retweet');
            buttons.find('.EditButton a[id*="_query"]').addClass('btn btn-sm btn-inverse').find('.ui-icon').attr('class', 'ace-icon fa fa-comment-o');
            buttons.find('.EditButton a[id*="_search"]').addClass('btn btn-sm btn-purple').find('.ui-icon').attr('class', 'ace-icon fa fa-search');

        }


        function beforeDeleteCallback(e) {
            var form = $(e[0]);
            if (form.data('styled')) return false;

            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
            style_delete_form(form);

            form.data('styled', true);
        }

        function beforeEditCallback(e) {
            var form = $(e[0]);
            form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
            style_edit_form(form);
        }


        //replace icons with FontAwesome icons like above
        function updatePagerIcons(table) {
            var replacement =
            {
                'ui-icon-seek-first': 'ace-icon fa fa-angle-double-left bigger-140',
                'ui-icon-seek-prev': 'ace-icon fa fa-angle-left bigger-140',
                'ui-icon-seek-next': 'ace-icon fa fa-angle-right bigger-140',
                'ui-icon-seek-end': 'ace-icon fa fa-angle-double-right bigger-140'
            };
            $('.ui-pg-table:not(.navtable) > tbody > tr > .ui-pg-button > .ui-icon').each(function () {
                var icon = $(this);
                var $class = $.trim(icon.attr('class').replace('ui-icon', ''));

                if ($class in replacement) icon.attr('class', 'ui-icon ' + replacement[$class]);
            })
        }

        function enableTooltips(table) {
            $('.navtable .ui-pg-button').tooltip({ container: 'body' });
            $(table).find('.ui-pg-div').tooltip({ container: 'body' });
        }



    </script>



		<!-- 图形报表处理 -->
		<script type="text/javascript">

            //定义饼状图数据
		    var piechart_data = function (l, d,c) {
		        var re = {};
		        re.label = l;
		        re.data = d;
		        re.color = c;
		        return re;

		    }


		    //定义曲线图数据
		    var sales_data = function (l,d, c) {
		        var re = {};
		        re.label = l;
		        re.data = d;
		        re.color = c;
		        return re;

		    }
            //初始化图表，就只能跑一次
		    function init_chart_gogo(griddb)
		    {
		        //判定是否存在二次处理的图表数据
		        var data = [];
		        var data_sales = [];
		        if (griddb.find("chartYHB>NewDataSet>饼图数据").text() == "" && griddb.find("chartYHB>NewDataSet>曲线图数据").text() == "") {
		            return false;
		        }
		        else {
		            $("#zheshichart").removeClass("hidden");

		            //依据返回的数据集，生成图形参数
		            if (griddb.find("chartYHB>NewDataSet>饼图数据").text() != "")
		            {
		                griddb.find("chartYHB>NewDataSet>饼图数据").each(function (index, ele) {
		                    data.push(piechart_data($(ele).find("项目名").text(), $(ele).find("百分数").text(), $(ele).find("颜色").text()));

		                });
		            }
		            if (griddb.find("chartYHB>NewDataSet>曲线图数据").text() != "") {
		                griddb.find("chartYHB>NewDataSet>曲线图数据").each(function (index, ele) {
		                  
		                    var ddd = [];
		                   
		                    griddb.find("chartYHB>NewDataSet>曲线图数据-" + $(ele).find("项目名").text()).each(function (index_sub, ele_sub) {
		                        ddd.push([$(ele_sub).find("X轴").text(), $(ele_sub).find("Y轴").text()]);
		                    });
		                    data_sales.push(sales_data($(ele).find("项目名").text(), ddd, $(ele).find("颜色").text()));
		                    
		                });
		            }
		        }
		      
		


		        //flot chart resize plugin, somehow manipulates default browser resize event to optimize it!
		        //but sometimes it brings up errors with normal resize event handlers
		        $.resize.throttleWindow = false;

		        //pie chart tooltip example
		        var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
		        var previousPoint = null;

		        if (griddb.find("chartYHB>NewDataSet>饼图数据").text() != "") {


		            var placeholder = $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });
		            // var data = [  { label: 'social networks', data: 38.7, color: '#68BC31' },  { label: 'search engines', data: 24.5, color: '#2091CF' },  { label: 'ad campaigns', data: 8.2, color: '#AF4E96' },   { label: 'direct traffic', data: 18.6, color: '#DA5430' },  { label: 'other', data: 10, color: '#FEE074' }  ] ;
		            function drawPieChart(placeholder, data, position) {
		                $.plot(placeholder, data, {
		                    series: {
		                        pie: {
		                            show: true,
		                            tilt: 0.8,
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
		                        margin: [-30, 15]
		                    }
                          ,
		                    grid: {
		                        hoverable: true,
		                        clickable: true
		                    }
		                })
		            }
		            drawPieChart(placeholder, data);

		            /**
                    we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
                    so that's not needed actually.
                    */
		            placeholder.data('chart', data);
		            placeholder.data('draw', drawPieChart);




		            placeholder.on('plothover', function (event, pos, item) {
		                if (item) {
		                    if (previousPoint != item.seriesIndex) {
		                        previousPoint = item.seriesIndex;
		                        var tip = item.series['label'] + " : " + item.series['percent'] + '%';
		                        $tooltip.show().children(0).html(tip);
		                    }
		                    $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
		                } else {
		                    $tooltip.hide();
		                    previousPoint = null;
		                }

		            });

		            /////////////////////////////////////
		            $(document).one('ajaxloadstart.page', function (e) {
		                $tooltip.remove();
		            });


		        }

		        if (griddb.find("chartYHB>NewDataSet>曲线图数据").text() != "") {

		            var sales_charts = $('#sales-charts').css({ 'width': '100%', 'height': '220px' });
		            $.plot("#sales-charts", data_sales, {
		                hoverable: true,
		                shadowSize: 0,
		                series: {
		                    lines: { show: true },
		                    points: { show: true }
		                },
		                xaxis: {
		                    //tickLength: 0,
		                    tickDecimals: 0
		                },
		                yaxis: {
		                    //ticks: 10,
		                    //min: -10,
		                    //max: 10,
		                    tickDecimals: 2
		                },
		                grid: {
		                    backgroundColor: { colors: ["#fff", "#fff"] },
		                    borderWidth: 1,
		                    borderColor: '#555',
		                    hoverable: true
		                },
		                legend: { show: true, position: "ne" }
		            });

		            $("#sales-charts").bind("plothover", function (event, pos, item) {

		                if (item) {
		                    if (previousPoint != item.seriesIndex) {
		                        previousPoint = item.seriesIndex;
		                        var tip = "<div style='text-align:left; '>目标: " + item.series['label'] + " <br/>X轴: " + item.datapoint[0].toString() + "<br/>Y轴: " + item.datapoint[1].toFixed(2) + "</div>";
		                        $tooltip.show().children(0).html(tip);
		                    }
		                    $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
		                } else {
		                    $tooltip.hide();
		                    previousPoint = null;
		                }
		            });

		        }

		        //收缩时重新定义列表高度
		        $(grid_selector).setGridHeight(getPageSize().WinH - $("#zheshiliebiaoquyu").offset().top - 150);
		        $("#zhedie_bbt").click(function () {

		            setTimeout(function () {
		                var ss = getPageSize();
		                $(grid_selector).setGridHeight(ss.WinH - $("#zheshiliebiaoquyu").offset().top - 150);
		            }, 500);
		        });
 

		    }
 
             

                
		 
		</script>


	<!-- 打印处理 -->
  <script>

      function beginPrint_go(dayinquyu)
      {
          //判定对应格式的打印页面是否存在，如果存在就使用指定页面打开。不存在调用默认打印。
          if (getUrlParam("printp") != null && getUrlParam("printp") != "")
          {
              //window.location.search
              //window.location.pathname
              var newurl = '' + window.location.pathname.replace(GetPageName(), 'printp_' + getUrlParam('printp') + '.aspx') + window.location.search;
 
              var form = $("<form></form>")
              form.attr('target', '_blank')
              form.attr('action', newurl)
              form.attr('method', 'post')
              form.appendTo("body")
              form.css('display', 'none')
              form.submit()
              return false;
          }

          var mode = "popup";   //"popup"或者"iframe"
         

          var close = mode == "popup" && false; //是否自动弹窗关闭
          var extraCss = "";//扩展样式

          //打印区域
          var print = "";
          print += (print.length > 0 ? "," : "") + "." + dayinquyu;

          //携带属性
          var keepAttr = [];
          keepAttr.push("class");
          keepAttr.push("id");
          keepAttr.push("style");

          //加入头标记
          var headElements = true ? '<meta charset="utf-8" />,<meta http-equiv="X-UA-Compatible" content="IE=edge"/>' : '';

          var options = { mode: mode, popClose: close, extraCss: extraCss, retainAttr: keepAttr, extraHead: headElements };

          $(print).printArea(options);
      }
      jQuery(function ($) {
   
        //让带有printarea_go_dayinanniu样式的对象，能触发打印带有PrintArea_F样式的区域
          $(".printarea_go_dayinanniu").on('click', function (e) {
              beginPrint_go("PrintArea_F");
          
        });

        
    });

  </script>