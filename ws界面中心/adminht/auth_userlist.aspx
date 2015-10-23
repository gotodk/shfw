<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_userlist.aspx.cs" Inherits="auth_userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 附加的head内容 -->
    <!-- page specific plugin styles -->

    <link rel="stylesheet" href="/assets/css/ui.jqgrid.css" />
 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_daohang" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->

    <div class="row">
        <div class="col-xs-12">
            <!-- PAGE CONTENT BEGINS -->
 

 


            <table id="grid-table"></table>

            <div id="grid-pager"></div>






            <script type="text/javascript">
                var $path_assets = "/assets";//this will be used in gritter alerts containing images
            </script>

            <!-- PAGE CONTENT ENDS -->
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->

    <!-- page specific plugin scripts -->
    <script src="/assets/js/jqGrid/jquery.jqGrid.min.js"></script>
    <script src="/assets/js/jqGrid/i18n/grid.locale-cn.js"></script>

 
    <!-- inline scripts related to this page -->
        <!-- **********全局变量配置******** -->
    <script type="text/javascript">
        //配置参数
        var url1 = "/ajax_pp_do.aspx";//处理页面
        var jkname_page1 = encodeURIComponent("用户权限简单浏览分页数据");//获取分页数据接口名
        var jkname_del1 = ".....";//删除数据接口名
 
    </script>
 
    <!-- **********jqgrid相关处理******** -->
    <script type="text/javascript">

 
        var grid_selector = "#grid-table";
        var pager_selector = "#grid-pager";

        jQuery(function ($) {

 

            //resize to fit page size
            $(window).on('resize.jqGrid', function () {
                $(grid_selector).jqGrid('setGridWidth', $(".page-content").width());
            });
            //resize on sidebar collapse/expand
            var parent_column = $(grid_selector).closest('[class*="col-"]');
            $(document).on('settings.ace.jqGrid', function (ev, event_name, collapsed) {
                if (event_name === 'sidebar_collapsed' || event_name === 'main_container_fixed') {
                    //setTimeout is for webkit only to give time for DOM changes and then redraw!!!
                    setTimeout(function () {
                        $(grid_selector).jqGrid('setGridWidth', parent_column.width());
                    }, 0);
                }
            });

            //if your grid is inside another element, for example a tab pane, you should use its parent's width:
            /**
            $(window).on('resize.jqGrid', function () {
                var parent_width = $(grid_selector).closest('.tab-pane').width();
                $(grid_selector).jqGrid( 'setGridWidth', parent_width );
            })
            //and also set width when tab pane becomes visible
            $('#myTab a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
              if($(e.target).attr('href') == '#mygrid') {
                var parent_width = $(grid_selector).closest('.tab-pane').width();
                $(grid_selector).jqGrid( 'setGridWidth', parent_width );
              }
            })
            */

            //关键配置
            jQuery(grid_selector).jqGrid({
                //direction: "rtl",

                //subgrid options
                subGrid: false,
                //data: grid_data,
                url: "/ajaxpagedemo.aspx?jkname=" + jkname_page1,
                datatype: "xml",
                mtype: "POST",
                xmlReader: {
                    root: "NewDataSet",
                    row: "主要数据",
                    page: "invoices>currentpage",
                    total: "invoices>totalpages",
                    records: "invoices>totalrecords",
                    repeatitems: false,
                    id: "UAid"
                },
                prmNames: {
                    page: 'R_PageNumber',
                    rows: 'R_PageSize',
                    sort: 'R_OrderBy',
                    order: 'R_Sort'
                },
                height: 350,
                //autowidth:true,
                //loadui:'block',
                //colNames是自定义列明，这里可以不定义，直接在colModel默认使用name作为列名比较方便
                //colNames: ['隐藏编号','唯一编号', '账号', '省份', '性别', '地区', '整数', '两位小数', '一个日期', '创建日期','图片绑定', '自定义操作'],
                colModel: [
                    //因为第一列在自带查看里不显示，所以要显示编号需要额外弄一列
                    { name: '隐藏编号', xmlmap: 'UAid', hidden: true },

                    { name: '编号', xmlmap: 'UAid', index: 'UAid', width: 50, fixed: true },

                    { name: '登录账号', xmlmap: 'Uloginname', index: 'Uloginname', formatter: 'showlink', formatoptions: { baseLinkUrl: 'auth_oneuser.aspx', target: '_top', showAction: '', addParam: '', idName: 'UAid' } },
                  
                    { name: '特征码', xmlmap: 'Uattrcode', index: 'Uattrcode' },

                    { name: '后台菜单权限', xmlmap: 'Unumber1', index: 'Unumber1' },
                    { name: '前台导航权限', xmlmap: 'Unumber2', index: 'Unumber1' },
                    { name: '全局独立权限', xmlmap: 'Unumber3', index: 'Unumber1' },
                    { name: '特殊权限', xmlmap: 'Unumber4', index: 'Unumber1' },
                    { name: '备用权限', xmlmap: 'Unumber5', index: 'Unumber1' },

                    { name: '隶属权限组', xmlmap: 'Uingroups', index: 'Uingroups' },
                    { name: '超管', xmlmap: 'SuperUser', index: 'SuperUser' } 
           
                 
                ],

                gridview: true, //这个选项必须开
                viewrecords: true,
                rowNum: 25,
                rowList: [25, 50, 100],
                pager: pager_selector,
                altRows: true,
                //toppager: true,
                sortable: true,

                multiselect: true,
                //multikey: "ctrlKey",
                multiboxonly: true,
                loadError: function (xhr, status, error) {
                    bootbox.alert(status + "--" + error);

                },
                loadComplete: function (data) {

                    if ($(data).text() == "") {
                        bootbox.alert("无法获取数据！");
                    }
                    if ($(data).find('错误>错误提示').text() != "") {
                        bootbox.alert($(data).find('错误>错误提示').text());
                    }

                    var table = this;
                    setTimeout(function () {
                        updatePagerIcons(table);
                        enableTooltips(table);
                    }, 0);
                },
                editurl: url1 + "?ajaxrun=del&jkname=" + jkname_del1,
                caption: ""




            });


            //其他界面相关辅助代码
            $(window).triggerHandler('resize.jqGrid');//trigger window resize to make the grid get the correct size


            //navButtons
            jQuery(grid_selector).jqGrid('navGrid', pager_selector,
                { 	//navbar options
                    edit: false,
                    editicon: 'ace-icon fa fa-pencil blue',
                    add: false,
                    addicon: 'ace-icon fa fa-plus-circle purple',
                    del: false,
                    delicon: 'ace-icon fa fa-trash-o red',
                    search: true,
                    searchicon: 'ace-icon fa fa-search orange',
                    refresh: true,
                    refreshicon: 'ace-icon fa fa-refresh green',
                    view: true,
                    viewicon: 'ace-icon fa fa-search-plus grey',
                },
                {
                    recreateForm: true,
                },
                {
                    recreateForm: true,
                },
                {
                    //delete record form
                    recreateForm: true,
                    beforeShowForm: function (e) {
                        var form = $(e[0]);
                        if (form.data('styled')) return false;

                        form.closest('.ui-jqdialog').find('.ui-jqdialog-titlebar').wrapInner('<div class="widget-header" />')
                        style_delete_form(form);

                        form.data('styled', true);
                    },
                    onClick: function (e) {
                        //alert(1);
                    }
                },
                {
                    //search form
                    recreateForm: true,
                    afterShowSearch: function (e) {
                        var form = $(e[0]);
                        form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />')
                        style_search_form(form);
                    },
                    afterRedraw: function () {
                        style_search_filters($(this));
                    }
                    ,
                    multipleSearch: true,

                    multipleGroup: true,
                    showQuery: true

                },
                {
                    //view record form
                    recreateForm: true,
                    width: 500,
                    beforeShowForm: function (e) {
                        var form = $(e[0]);
                        form.closest('.ui-jqdialog').find('.ui-jqdialog-title').wrap('<div class="widget-header" />')
                    }
                }
            )



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




        });


    </script>
</asp:Content>

