<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_enum_list.aspx.cs" Inherits="auth_enum_list" %>

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

            <div id="modal_form_edit" class="modal fade" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="blue bigger">编辑选中数据</h4><div style="color:red;">要使调整生效，必须重新生成菜单xml文件，<br/>如果界面中心在多个集群上，还要手工复制分发xml文件！</div>
                        </div>

                        <div class="modal-body">





                            <div class="row">
                                <div class="col-sm-12">
                                    <i class="ace-icon fa fa-spinner fa-spin orange bigger-300" id="editloadinfo"></i>
                                    <form class="form-horizontal hide" role="form" id="myform1">
                                        <!-- #section:elements.form -->
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="idforedit"><i class="light-red">*  </i>唯一编号：</label>

                                            <div class="col-sm-9">
                                                <input readonly="readonly" data-rel="tooltip" type="text" id="idforedit" name="idforedit" data-placement="bottom" class="col-xs-12 col-sm-10" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ANBaseName"><i class="light-red">*  </i>权限中文名称：</label>

                                            <div class="col-sm-9">
                                                <input data-rel="tooltip" type="text" id="ANBaseName" name="ANBaseName" placeholder="请输入…" title="谨慎修改" data-placement="bottom" class="col-xs-12 col-sm-10" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ANextendID"><i class="light-red">*  </i>扩展ID：</label>

                                            <div class="col-sm-9">
                                                <input data-rel="tooltip" type="text" id="ANextendID" name="ANextendID" placeholder="请输入…" title="谨慎修改，将结合用户拥有的权限影响菜单的自动隐藏和显示。没有就写“无”" data-placement="bottom" class="col-xs-12 col-sm-10" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                        </div>
                           
                                          <div class="form-group">
                                            <label class="col-sm-3 control-label no-padding-right" for="ANused"><i class="light-red">*  </i>是否被使用：</label>

                                            <div class="col-sm-9">

                                                <div class="no-padding-left radio col-xs-12 col-sm-10">
                                                    <label>
                                                        <input name="ANused" id="ANused0" type="radio" value="0" class="ace" />
                                                        <span class="lbl">没用上</span>
                                                    </label>
                                                    <label>
                                                        <input name="ANused" id="ANused1" type="radio" value="1" class="ace" />
                                                        <span class="lbl">已经分配使用</span>
                                                    </label>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                        </div>

                                        <div class="clearfix form-actions col-xs-12 col-sm-12">




                                            <label class="col-sm-3 control-label"></label>

                                            <div class="col-sm-9">
                                                <div class="col-xs-12 col-sm-10">


                                                    <button class="btn btn-info pull-left" type="button" id="addbutton1">
                                                        <i class="ace-icon fa fa-check bigger-110"></i>
                                                        保存
                                                    </button>

                                                    <button class="btn pull-right" type="button" id="reloaddb">
                                                        <i class="ace-icon fa fa-undo bigger-110"></i>
                                                        重填
                                                    </button>

                                                </div>

                                            </div>


                                        </div>




                                    </form>
                                </div>
                                <!-- /.col -->
                            </div>




                        </div>

                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>






















 




                        <form class="form-inline well well-sm" id="mysearchtop">
                <label>权限分类中文名：</label>
                        
                <select  id="ANClassName_ss" name="ANClassName_ss">
                                                        <option value="" selected>不限</option>
                                                        <option value="后台菜单权限">后台菜单权限</option>
                                                        <option value="前台导航权限">前台导航权限</option>
                                                        <option value="全局独立权限">全局独立权限</option>
                                                        <option value="特殊权限">特殊权限</option>
                                                        <option value="备用权限">备用权限</option>
                                                    </select>
                              

                <label>是否启用：</label>
                            <select  id="ANused_ss" name="ANused_ss">
                                                        <option value="" selected>不限</option>
                                                        <option value="1">已分配使用</option>
                                                        <option value="0">没用上</option>
                                            
                                                    </select>
                
                <button type="button" class="btn btn-purple btn-sm" id="MybtnSearch">
                    <i class="ace-icon fa fa-search bigger-110"></i>搜索
                </button>

            </form>


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

    <script src="/assets/js/jquery.validate.js"></script>
 

    <!-- inline scripts related to this page -->


    <!-- **********全局变量配置******** -->
    <script type="text/javascript">
        //配置参数
        var gogoajax1_CanRun = true;//ajax提交防重复
        var formid1 = "#myform1";//表单id
        var buttonid1 = "#addbutton1";//提交按钮id
        var url1 = "/ajax_pp_do.aspx";//处理页面
        var jkname_page1 = encodeURIComponent("权限枚举表分页数据");//获取分页数据接口名
        var jkname_save1 = "编辑一条权限枚值举值";//保存数据接口名
        var jkname_info1 = "获取一条权值枚举";//获取数据接口名
        var jkname_del1 = ".....";//删除数据接口名

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
    </script>
    <!-- **********ajax获初始化表单数据******** -->
    <script type="text/javascript">

        //获取数据填充表单
        function loadinfoajax1(dbid) {


            //显示等待提示，隐藏必要区域
            $("#editloadinfo").removeClass("hide");
            $(formid1).addClass("hide");

            //没有唯一识别号不允许编辑
            if ($.trim(dbid) == "") {
                bootbox.alert("无有效参数!");
                return false;
            }

            function callback(xml) {
                //解析xml并显示在界面上
                if ($(xml).find('返回值单条>执行结果').text() != "ok") {
                    bootbox.alert("查找数据失败!");
                    return false;
                }

                //权限中文名称
                $("#ANBaseName").val($(xml).find('数据记录>ANBaseName').text());
                //扩展ID
                $("#ANextendID").val($(xml).find('数据记录>ANextendID').text());

                //是否被使用
                var ANused = $(xml).find('数据记录>ANused').text();
                $("input:radio[name='ANused'][value='" + ANused + "']").prop("checked", true);
             
                //隐藏等待提示，显示必要区域
                $("#editloadinfo").addClass("hide");
                $(formid1).removeClass("hide");

            };
            $.ajax({
                type: "POST",
                url: url1+"?guid=" + randomnumber(),
                dataType: "xml",
                data: "ajaxrun=info&jkname=" + jkname_info1 + "&idforedit=" + dbid,
                success: callback, //请求成功
                error: errorForAjax//请求出错 
                //complete: complete//请求完成

            });
        }

        jQuery(function ($) {
            $(document).on('click', "#reloaddb", function () { loadinfoajax1($("#idforedit").val()); });
        });
    </script>
    <!-- **********ajax提交表单******** -->
    <script type="text/javascript">


        function gogoajax1(t_formid, t_buttonid, t_url, t_jkname) {

            //防重复提交
            if (!gogoajax1_CanRun) {
                return false;
            }
            //验证
            if (!$(t_formid).valid()) {

                return false;
            }
            else {
                //显示等待提示，禁用必要区域
                gogoajax1_CanRun = false;
                $(t_buttonid).addClass("disabled");
                $(t_buttonid).html("<i class='ace-icon fa fa-spinner fa-spin orange bigger-110'></i>正在处理");
            };


            function callback(msg) {
                $(grid_selector).trigger("reloadGrid");
                $('#modal_form_edit').modal('hide');
                //显示提交结果
                bootbox.alert(msg);


                //最后跑这个
                setTimeout(function () {
                    //取消等待显示，开放禁用区域
                    gogoajax1_CanRun = true;
                    $(t_buttonid).removeClass("disabled");
                    $(t_buttonid).html("<i class='ace-icon fa fa-check bigger-110'></i>保存");
                }, 1500)


            };
            $.ajax({
                type: "POST",
                url: t_url + "?guid=" + randomnumber(),
                dataType: "html",
                data: "ajaxrun=save&jkname=" + t_jkname + "&" + $(t_formid).serialize(),
                success: callback, //请求成功
                error: errorForAjax//请求出错 

            });
        }

        jQuery(function ($) {
            //添加提交事件
            $(document).on('click', buttonid1, function () {
                gogoajax1(formid1, buttonid1, url1, jkname_save1);
            });
        });

    </script>
    <!-- **********ajax提交前验证******** -->
    <script type="text/javascript">
        //自定义表单验证方法
        $.validator.addMethod("lrunlv", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,2})?$/.test(value);
        }, "请精确至两位小数。");
        jQuery(function ($) {
            //表单验证
            $(formid1).validate({
                errorElement: 'div',
                errorClass: 'help-block',
                ignore: "",
                rules: {
                    ANBaseName: {
                        required: true
                    },
                    ANextendID: {
                        required: true
                    }


                },

                messages: {
                    ANBaseName: {
                        required: "权限中文名称必须填写." 
                    },
                    ANextendID: {
                        required: "扩展ID必须填写.没有就写无"
                    }
                },


                highlight: function (e) {
                    $(e).closest('.form-group').addClass('has-error');
                },

                success: function (e) {
                    $(e).closest('.form-group').removeClass('has-error');
                    $(e).remove();
                },

                errorPlacement: function (error, element) {
                    element.closest('.form-group').find(".help-block").remove();

                    error.appendTo(element.closest('.form-group').find(".ValidErrInfo"));
                },

                submitHandler: function (form) {
                },
                invalidHandler: function (form) {
                }
            });
        });
    </script>

    <!-- **********输入过程控制处理******** -->
    <script type="text/javascript">
        jQuery(function ($) {
 
            //启用悬浮简述tooltip
            $('[data-rel=tooltip]').tooltip({ container: 'body' });
            $('[data-rel=popover]').popover({ container: 'body' });
 
        });



    </script>

    <!-- **********jqgrid相关处理******** -->
    <script type="text/javascript">

        //打开编辑对话框
        function openeditdialog(idforedit) {

            if (idforedit == null || idforedit == "") {

                var selectid = $(grid_selector).jqGrid('getGridParam', 'selrow');
                var rowData = $(grid_selector).jqGrid('getRowData', selectid);
                idforedit = rowData.隐藏编号;

            }
            else {
                $("#idforedit").val(idforedit);

            }

            loadinfoajax1($("#idforedit").val());
            $('#modal_form_edit').modal({ backdrop: false, keyboard: false });

        }


        var grid_selector = "#grid-table";
        var pager_selector = "#grid-pager";

        jQuery(function ($) {

            //自定义搜索事件
            $(document).on('click', "#MybtnSearch", function () {
                var zdy = $('#mysearchtop').serialize()
                var postData = $(grid_selector).jqGrid("getGridParam", "postData");
                $.extend(postData, { mysearchtop: zdy });
                $(grid_selector).jqGrid("setGridParam", { search: true }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid
            });



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
                    id: "ANID"
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
                    { name: '隐藏编号', xmlmap: 'ANID', hidden: true },

                    { name: '编号', xmlmap: 'ANID', index: 'ANID', width: 50, fixed: true },

                    { name: '权值分类字段名', xmlmap: 'ANClass', index: 'ANClass' },

                    { name: '权值分类中文名', xmlmap: 'ANClassName', index: 'ANClassName' },

                    { name: '权限中文名称', xmlmap: 'ANBaseName', index: 'ANBaseName' },

                    { name: '权限基础值', xmlmap: 'ANBaseNumber', index: 'ANBaseNumber' },

                     { name: '基础值次幂', xmlmap: 'ANBaseNumber_p', index: 'ANBaseNumber_p', formatter: 'integer' },
                      { name: '扩展ID', xmlmap: 'ANextendID', index: 'ANextendID' },
                     

                    { name: '是否启用', xmlmap: 'ANused', index: 'ANused', width: 70, fixed: true },
                  {
                      name: '自定列', width: 60, fixed: true, sortable: false,
                      formatter: function (cellvalue, options, rowObject) { return "<button  type='button' onclick=\"openeditdialog('" + $(rowObject).find('ANID').text() + "');\" class='btn btn-minier btn-success'>编辑</button>" },

                  }
                ],

                gridview: true, //这个选项必须开
                viewrecords: true,
                rowNum: 25,
                rowList: [25, 50, 100],
                pager: pager_selector,
                altRows: true,
                //toppager: true,
                sortable: true,

                //multiselect: true,
                //multikey: "ctrlKey",
                //multiboxonly: true,
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

