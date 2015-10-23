<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="auth_editpassword.aspx.cs" Inherits="auth_editpassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 附加的head内容 -->
  
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 
    <div class="row">
        <div class="col-xs-12">
            <!-- PAGE CONTENT BEGINS -->
            <div class="row">
                <div class="col-xs-12">
                    <!-- #section:elements.tab -->
                    <div class="tabbable">
                        <ul class="nav nav-tabs" id="myTab">
                            <li class="active">
                                <a data-toggle="tab" href="#addadd">
                                    <i class="green ace-icon fa fa-pencil-square-o bigger-120"></i>
                                    修改登录密码
                                </a>
                            </li>


                        </ul>

                        <div class="tab-content">
                            <div id="addadd" class="tab-pane fade in active">

                                <div class="row">
                                    <div class=" col-xs-12 col-sm-12">
              
                                        <form class="form-horizontal" role="form" id="myform1">
                                            <!-- #section:elements.form -->
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="idforedit"><i class="light-red">*  </i>唯一编号：</label>

                                                <div class="col-sm-10">
                                                    <input readonly="readonly" data-rel="tooltip" type="text" id="idforedit" name="idforedit" data-placement="bottom" class="col-xs-12 col-sm-5" value='<%=UserSession.唯一键 %>' />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>

                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zhanghao"><i class="light-red">*  </i>账号：</label>

                                                <div class="col-sm-10">
                                                    <input readonly="readonly" data-rel="tooltip" type="text" id="zhanghao" name="zhanghao" data-placement="bottom" class="col-xs-12 col-sm-5" value='<%=UserSession.登录名 %>' />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>

                                            </div>


                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="mima_old"><i class="light-red">*  </i>原始密码：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="password" id="mima_old" name="mima_old" placeholder="请输入…" title="输入原始密码" data-placement="bottom" class="col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>

                                         <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="mima_new"><i class="light-red">*  </i>设置新密码：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="password" id="mima_new" name="mima_new" placeholder="请输入…" title="输入要设置的新密码" data-placement="bottom" class="col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="mima_new_re"><i class="light-red">*  </i>重复新密码：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="password" id="mima_new_re" name="mima_new_re" placeholder="请输入…" title="再次输入要设置的新密码" data-placement="bottom" class="col-xs-12 col-sm-5" />
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
 

     <!-- **********全局变量配置******** -->
     <script type="text/javascript">
         //配置参数
         var gogoajax1_CanRun = true;//ajax提交防重复
         var formid1 = "#myform1";//表单id
         var buttonid1 = "#addbutton1";//提交按钮id
         var url1 = "/ajax_pp_do.aspx";//处理页面
         var jkname_save1 = "修改登录密码";//保存数据接口名
  
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

                    //重置区域内容
                    $("#mima_old").val("");
                    $("#mima_new").val("");
                    $("#mima_new_re").val("");
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
     
            jQuery(function ($) {
                //表单验证
                $(formid1).validate({
                    errorElement: 'div',
                    errorClass: 'help-block',
                    ignore: "",
                    rules: {
                        mima_old: {
                            required: true,
                            minlength: 6
                        },
                        mima_new: {
                            required: true,
                            minlength: 6
                        },
                        mima_new_re: {
                            required: true,
                            minlength: 6,
                            equalTo: "#mima_new"
                        }

                    },

                    messages: {
                       
                        mima_old: {
                            required: "必须填写原始密码",
                            minlength: "密码至少6位"
                        },
                        mima_new: {
                            required: "必须填写新密码",
                            minlength: "密码至少6位"
                        },
                        mima_new_re: {
                            required: "必须重复填写一遍新密码",
                            minlength: "密码至少6位",
                            equalTo: "重复密码必须与新密码一致"
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


     

 
</asp:Content>

