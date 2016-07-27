<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="demo_tijiao.aspx.cs" Inherits="demo_tijiao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <link rel="stylesheet" href="/assets/css/datepicker.css" />


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 
    <div class="row ">
        <div class="col-xs-12">
            <!-- PAGE CONTENT BEGINS -->
            <div class="row ">
                <div class="col-xs-12">
                    <!-- #section:elements.tab -->
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active">
                                <a data-toggle="tab" href="#addadd">
                                    <i class="green ace-icon fa fa-pencil-square-o bigger-120"></i>
                                    增加
                                </a>
                            </li>


                        </ul>

                        <div class="tab-content">
                            <div id="addadd" class="tab-pane fade in active">

                                <div class="row">
                                    <div class="col-sm-12">
                                        <form class="form-horizontal" role="form" id="myform1">
                                            <!-- #section:elements.form -->
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zhanghao"><i class="light-red">*  </i>账号：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="text" id="zhanghao" name="zhanghao" placeholder="请输入…" title="需要电子邮件地址" data-placement="bottom" class="col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>

                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="mima"><i class="light-red">*  </i>密码：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="password" id="mima" name="mima" placeholder="请输入…" title="这里录入简短说明" data-placement="bottom" class="col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="shengfen"><i class="light-red">*  </i>省份：</label>

                                                <div class="col-sm-10">
                                                    <select class="col-xs-12 col-sm-5" id="shengfen" name="shengfen">
                                                        <option value="" selected>请选择</option>
                                                        <option value="山东">山东</option>
                                                        <option value="北京">北京</option>
                                                        <option value="上海">上海</option>
                                                        <option value="新疆维吾尔自治区">新疆维吾尔自治区</option>
                                                    </select>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="xingbie"><i class="light-red">*  </i>性别：</label>

                                                <div class="col-sm-10">

                                                    <div class="no-padding-left radio col-xs-12 col-sm-5">
                                                        <label>
                                                        <input name="xingbie" type="radio" value="男"   class="ace" checked="checked" />
                                                        <span class="lbl">男性</span>
                                                                </label>
                                                        <label>
                                                        <input name="xingbie" type="radio" value="女" class="ace"  />
                                                        <span class="lbl">女性</span>
                                                    </label>
                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="diqu"><i class="light-red">*  </i>地区：</label>

                                                <div class="col-sm-10">

                                                    <div class="no-padding-left checkbox col-xs-12 col-sm-5">
                                                        <label>
                                                        <input name="diqu" type="checkbox" class="ace" value="济南" />
                                                        <span class="lbl">济南</span>
                                                             </label>
 
                                                        <label>
                                                        <input name="diqu" type="checkbox" class="ace" value="青岛" checked="checked" />
                                                        <span class="lbl">青岛</span>
                                                            </label>
                                                        <label>
                                                        <input name="diqu" type="checkbox" class="ace" value="威海" />
                                                        <span class="lbl">威海</span>
                                                        </label>
                                                        <label>
                                                        <input name="diqu" type="checkbox" class="ace" value="烟台" />
                                                        <span class="lbl">烟台</span>
                                                    </label>




                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>



                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zhengshu">整数：</label>

                                                <div class="col-sm-10">
                                                    <input type="text" class="input-mini" id="zhengshu" name="zhengshu" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="erweixiao">两位小数：</label>

                                                <div class="col-sm-10">
                                                    <input type="text" class="input-mini" id="erweixiao" name="erweixiao" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="yigeriqi">一个日期：</label>

                                                <div class="col-sm-10">
                                                    <div class="input-group col-xs-12 col-sm-5">
                                                        <input class="form-control date-picker" id="yigeriqi" name="yigeriqi" type="text" />
                                                        <span class="input-group-addon">
                                                            <i class="fa fa-calendar bigger-110"></i>
                                                        </span>
                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right">日期区间：</label>

                                                <div class="col-sm-10">
                                                    <div class="input-daterange input-group col-xs-12 col-sm-5">
                                                        <input class="form-control date-picker" id="riqiqujian1" name="riqiqujian1" type="text" />
                                                        <span class="input-group-addon">
                                                            <i class="fa fa-exchange"></i>
                                                        </span>
                                                        <input class="form-control date-picker" id="riqiqujian2" name="riqiqujian2" type="text" />
                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                         



                            


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="shouji">手机：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="text" id="shouji" name="shouji" title="这里录入简短说明" data-placement="bottom" class="input-mask-phone col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="beizhu">备注：</label>

                                                <div class="col-sm-10">
                                                    <textarea placeholder="请输入…" class="limited col-xs-12 col-sm-5" id="beizhu" name="beizhu" maxlength="200" rows="5"></textarea>
                                                    <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="这里录入比较详细的说明.这里录入比较详细的说明" title="备注要求">?</span>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="bianjiqi">编辑器：</label>

                                                <div class="col-sm-10">
                                                    <div class="wysiwyg-editor" id="editor1"></div>
                                                    <input name="bianjiqi_html" type="hidden" id="bianjiqi_html">
                                                    <input name="bianjiqi_text" type="hidden" id="bianjiqi_text">
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>


                             
                                            <div class="clearfix form-actions col-xs-12 col-sm-12">




                                                <label class="col-sm-2 control-label"></label>

                                                <div class="col-sm-10">
                                                    <div class="col-xs-12 col-sm-5">


                                                        <button class="btn btn-info pull-left" type="button" id="addbutton1">
                                                            <i class="ace-icon fa fa-check bigger-110"></i>
                                                            保存
                                                        </button>

                                                        <button class="btn pull-right" type="reset">
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
                                <!-- /.row -->


                            </div>

                            
                        </div>
                    </div>

                    <!-- /section:elements.tab -->
                </div>
                <!-- /.col -->


            </div>
            <!-- /.row -->








            <script type="text/javascript">
                var $path_assets = "/assets";//以备某段js动态调用了这个变量
            </script>

            <!-- PAGE CONTENT ENDS -->
        </div>
        <!-- /.col -->
    </div>
    <!-- /.row -->

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->
    <script src="/assets/js/jquery.validate.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>

  
		<script src="/assets/js/jquery.hotkeys.js"></script>
		<script src="/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="/assets/js/ace/elements.colorpicker.js"></script>
		<script src="/assets/js/ace/elements.wysiwyg.js"></script>
     
    <script type="text/javascript" src="/assets/js/desforcsharp.js"></script>

 <!-- **********全局变量配置******** -->
     <script type="text/javascript">
         //配置参数
         var gogoajax1_CanRun = true;//ajax提交防重复
         var formid1 = "#myform1";//表单id
         var buttonid1 = "#addbutton1";//提交按钮id
         var url1 = "/ajax_pp_do.aspx";//处理页面
         var jkname_save1 = "提交表单demo";//保存数据接口名
 
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



           <!-- **********编辑器处理(一定放最前面)******** -->
		<script type="text/javascript">
		    jQuery(function ($) {
		        var editname_w = "#editor1";
		        //添加提交事件
		        $(document).on('click', buttonid1, function () {

		            if ($(editname_w).text().trim() != "") {
		                $("#bianjiqi_html").val(encMe($(editname_w).html(), "mima"));
		                $("#bianjiqi_text").val(encMe($(editname_w).text(), "mima"));
		            }
		            else {
		                $("#bianjiqi_html").val("");
		                $("#bianjiqi_text").val("");
		            }





		        });


 
		        $('#editor1').ace_wysiwyg();//this will create the default editor will all buttons

		 


		        /**
                //make the editor have all the available height
                $(window).on('resize.editor', function() {
                    var offset = $(editname_w).parent().offset();
                    var winHeight =  $(this).height();
                    
                    $(editname_w).css({'height':winHeight - offset.top - 10, 'max-height': 'none'});
                }).triggerHandler('resize.editor');
                */



		        //RESIZE IMAGE

		        //Add Image Resize Functionality to Chrome and Safari
		        //webkit browsers don't have image resize functionality when content is editable
		        //so let's add something using jQuery UI resizable
		        //another option would be opening a dialog for user to enter dimensions.
		        if (typeof jQuery.ui !== 'undefined' && ace.vars['webkit']) {

		            var lastResizableImg = null;
		            function destroyResizable() {
		                if (lastResizableImg == null) return;
		                lastResizableImg.resizable("destroy");
		                lastResizableImg.removeData('resizable');
		                lastResizableImg = null;
		            }

		            var enableImageResize = function () {
		                $('.wysiwyg-editor')
                        .on('mousedown', function (e) {
                            var target = $(e.target);
                            if (e.target instanceof HTMLImageElement) {
                                if (!target.data('resizable')) {
                                    target.resizable({
                                        aspectRatio: e.target.width / e.target.height,
                                    });
                                    target.data('resizable', true);

                                    if (lastResizableImg != null) {
                                        //disable previous resizable image
                                        lastResizableImg.resizable("destroy");
                                        lastResizableImg.removeData('resizable');
                                    }
                                    lastResizableImg = target;
                                }
                            }
                        })
                        .on('click', function (e) {
                            if (lastResizableImg != null && !(e.target instanceof HTMLImageElement)) {
                                destroyResizable();
                            }
                        })
                        .on('keydown', function () {
                            destroyResizable();
                        });
		            }

		            enableImageResize();

		            /**
                    //or we can load the jQuery UI dynamically only if needed
                    if (typeof jQuery.ui !== 'undefined') enableImageResize();
                    else {//load jQuery UI if not loaded
                        $.getScript($path_assets+"/js/jquery-ui.custom.min.js", function(data, textStatus, jqxhr) {
                            enableImageResize()
                        });
                    }
                    */
		        }


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
            }


            function callback(msg) {
             
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
                url: t_url+"?guid=" + randomnumber(),
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
                    zhanghao: {
                        required: true,
                        email: true
                    },
                    mima: {
                        required: true,
                        minlength: 5
                    },
                    shengfen: {
                        required: true
                    },
                    diqu: {
                        required: true
                    },
                    erweixiao: "lrunlv"


                },

                messages: {
                    zhanghao: {
                        required: "请输入正确的电子邮件格式.",
                        email: "请输入正确的电子邮件格式.."
                    },
                    mima: {
                        required: "请输入密码",
                        minlength: "密码至少5个字符"
                    },
                    shengfen: {
                        required: "请选择省份",
                    },
                    diqu: {
                        required: "请至少选择一个选择地区",
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

            //过滤输入格式
            $('.input-mask-phone').mask('99999999999');
            $('#zhengshu').ace_spinner({ value: 1, min: 1, max: 999999999, step: 1, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' });
            $('#erweixiao').ace_spinner({ value: 1, min: 1, max: 999999999, step: 0.01, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' });
            $('textarea.limited').inputlimiter({
                remText: '剩余可用%n,',
                limitText: '最大字符:%n'
            });
            //datepicker plugin初始化
            $('.date-picker').datepicker({ autoclose: true, })
            $('.date-picker').mask('9999-99-99');

            //启用悬浮简述tooltip
            $('[data-rel=tooltip]').tooltip({ container: 'body' });
            $('[data-rel=popover]').popover({ container: 'body' });


        

        });



    </script>


 

</asp:Content>

