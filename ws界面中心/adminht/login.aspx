<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>
<html lang="zh">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title>系统管理登录</title>

    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
   
    	<!-- bootstrap & fontawesome -->
		<link rel="stylesheet" href="/assets/css/bootstrap.css" />
       
		<link rel="stylesheet" href="/assets/css/font-awesome.css" />
 

		<!-- text fonts -->
		<link rel="stylesheet" href="/assets/css/ace-fonts.css" />

		<!-- ace styles -->
		<link rel="stylesheet" href="/assets/css/ace.css" />

		<!--[if lte IE 9]>
			<link rel="stylesheet" href="/assets/css/ace-part2.css" />
		<![endif]-->
		<link rel="stylesheet" href="/assets/css/ace-rtl.css" />

		<!--[if lte IE 9]>
		  <link rel="stylesheet" href="/assets/css/ace-ie.css" />
		<![endif]-->

		<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->

		<!--[if lt IE 9]>
		<script src="/assets/js/html5shiv.js"></script>
		<script src="/assets/js/respond.js"></script>
		<![endif]-->

</head>
	<body class="login-layout">
 

        <form id="form1" runat="server">
		<div class="main-container">
			<div class="main-content">
				<div class="row">
					<div class="col-sm-10 col-sm-offset-1">
						<div class="login-container">
							<div class="center">
								<h1>
									<i class="ace-icon fa fa-cubes green"></i>
									<span class="white" id="denglu_title" runat="server">系统名称</span>
								</h1>
						 
							</div>

							<div class="space-6"></div>

							<div class="position-relative">
								<div id="login-box" class="login-box visible widget-box no-border">
									<div class="widget-body">
										<div class="widget-main">
											<h4 class="header blue lighter bigger">
												<i class="ace-icon fa fa-coffee green"></i>
												管理登录
											</h4>

											<div class="space-6"></div>

											 
												<fieldset>
													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="text" class="form-control" placeholder="账号"  name="zhanghao" id="zhanghao" value="<%=inputzhanghao %>" />
															<i class="ace-icon fa fa-user"></i>
														</span>
													</label> 

													<label class="block clearfix">
														<span class="block input-icon input-icon-right">
															<input type="password" class="form-control" placeholder="密码" name="mima"  id="mima" />
															<i class="ace-icon fa fa-lock"></i>
														</span>
													</label> 
                                                    <label class="hidden" id="showyzm">
														<span class="block input-icon input-icon-right">
															<input type="text" class="form-control" placeholder="输入下方数字" name="yanzhengma" id="yanzhengma"  /><i class="ace-icon fa fa-lightbulb-o"></i>
                                                            
														</span>
                                                   
														<asp:Image ID="verifycodeimg" runat="server" ImageUrl="/assets/img/loading.gif" /> <button class="btn btn-sm btn-white" id="shuaxin" type="button">点我刷新</button>

													 
													</label>

													<div class="space"></div>

													<div class="clearfix">
														<label class="inline">
															<input type="checkbox" class="ace" id="remme"  name="remme" checked="checked" />
															<span class="lbl"> 记住我</span>
														</label>

														<button type="button" class="width-35 pull-right btn btn-sm btn-primary" id="logininb">
															<i class="ace-icon fa fa-key"></i>
															<span class="bigger-110">登录</span>
														</button>
													</div>

													<div class="space-4"></div>
												</fieldset>
											 
                                            <div class="alert alert-danger" id="errmsg">
									 
 
										</div>
										 
										 
										 
										</div><!-- /.widget-main -->

										<div class="toolbar clearfix">
											<div>
												<a href="/"  class="forgot-password-link">
													<i class="ace-icon fa fa-arrow-left"></i>
													返回首页
												</a>
											</div>

											<div>
												<a href="#"  class="user-signup-link">
													其他链接
													<i class="ace-icon fa fa-arrow-right"></i>
												</a>
											</div>
										</div>
									</div><!-- /.widget-body -->
								</div><!-- /.login-box -->

				 
							</div><!-- /.position-relative -->

							<div class="navbar-fixed-top align-right hidden">
								<br />
								&nbsp;
								<a id="btn-login-dark" href="#">黑夜</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btn-login-blur" href="#">蓝调</a>
								&nbsp;
								<span class="blue">/</span>
								&nbsp;
								<a id="btn-login-light" href="#">亮白</a>
								&nbsp; &nbsp; &nbsp;
							</div>
						</div>
					</div><!-- /.col -->
				</div><!-- /.row -->
			</div><!-- /.main-content -->
		</div><!-- /.main-container -->

		<!-- basic scripts -->

		<!--[if !IE]> -->
		<script type="text/javascript">
            window.jQuery || document.write("<script src='/assets/js/jquery-2.1.1.min.js'>"+"<"+"/script>");
		</script>

		<!-- <![endif]-->

		<!--[if IE]>
<script type="text/javascript">
 window.jQuery || document.write("<script src='/assets/js/jquery-1.11.1.min.js'>"+"<"+"/script>");
</script>
<![endif]-->
               <script type="text/javascript">
                var $path_assets = "/assets";//以备某段js动态调用了这个变量
            </script>
            
		<script type="text/javascript">
			if('ontouchstart' in document.documentElement) document.write("<script src='/assets/js/jquery.mobile.custom.js'>"+"<"+"/script>");
		</script>
          
  <script src="/assets/js/jquery.cookie.js"></script>
  <script type="text/javascript" src="/assets/js/desforcsharp.js?rrr=12345"></script>
		<!-- inline scripts related to this page -->
             <!-- **********全局变量配置******** -->
     <script type="text/javascript">
         //配置参数
         var gogoajax1_CanRun = true;//ajax提交防重复
         var formid1 = "#<%=form1.ClientID%>";//表单id
         var buttonid1 = "#logininb";//提交按钮id
         var url1 = "login_do.aspx";//处理页面
         var jkname_save1 = "后台管理登录验证";//保存数据接口名
 
         //ajax系统错误统一提示
         function errorForAjax(XMLHttpRequest, textStatus, errorThrown) {
 
             // 通常情况下textStatus和errorThown只有其中一个有值 
             $("#errmsg").show(150);
             $("#errmsg").html("<strong><i class='ace-icon fa fa-times'></i></strong>抱歉，系统出现问题!");
    
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
             if (r != null) return r[2]; return ""; //返回参数值
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
                if ($.trim($("#zhanghao").val()) == "" || $.trim($("#mima").val()) == "") {
                    $("#errmsg").show(150);
                    $("#errmsg").html("<strong><i class='ace-icon fa fa-times'></i></strong>请填写账号和密码。");
                    return false;
                }
                else {
                    //显示等待提示，禁用必要区域
                    gogoajax1_CanRun = false;
                    $(t_buttonid).addClass("disabled");
                    $(t_buttonid).html("<i class='ace-icon fa fa-spinner fa-spin orange'></i>正在登录");
                    $("#errmsg").hide();
                }


                function callback(msg) {
                    

                    $("#__VIEWSTATE").attr("disabled", false);//启用视图
                    if (msg == "ok")
                    {
                        window.top.location.href = "<%=homeurl%>";
                    }
                    else
                    {
                        changeImg();//重置验证码
                        //显示提交结果
                        $("#errmsg").show(150);
                        $("#errmsg").html("<strong><i class='ace-icon fa fa-times'></i></strong>" + msg);

                        //最后跑这个
                        setTimeout(function () {
                            //取消等待显示，开放禁用区域
                            gogoajax1_CanRun = true;
                            $(t_buttonid).removeClass("disabled");
                            $(t_buttonid).html("<i class='ace-icon fa fa-key'></i>登录");
                        }, 500);
                    }
 
                    
                
                };
                $("#__VIEWSTATE").attr("disabled", true);//禁用视图
                $.ajax({
                    type: "POST",
                    url: t_url + "?guid=" + randomnumber(),
                    dataType: "html",
                    //data: "ajaxrun=backlogin&jkname=" + t_jkname + "&" + $(t_formid).serialize(),
                    data: "ajaxrun=backlogin&jkname=" + t_jkname + "&" + $(t_formid).serialize(),
                    success: callback, //请求成功
                    error: errorForAjax//请求出错 

                });
            }


            jQuery(function ($) {
                $("#errmsg").hide();
                //添加提交事件
                $(document).on('click', buttonid1, function () {
                    gogoajax1(formid1, buttonid1, url1, jkname_save1); 
                });
                //回车提交
                $(document).on('keydown', document, function (event) {
                    if (event.keyCode == 13) {
                        gogoajax1(formid1, buttonid1, url1, jkname_save1);
                    }
                   
                });
            });
          </script>

   
            <!-- **********验证码处理******** -->
       <script type="text/javascript">
         
           function changeImg() {
 
               if ($.cookie("vccs") == null || $.cookie("vccs") == ""|| parseInt($.cookie("vccs")) < 5) {
                   $("#showyzm").addClass("hidden");
                   $("#showyzm").removeClass("block clearfix");
             
                   return;
               }
               else {
                   if (parseInt($.cookie("vccs")) >= 5) {
                       $("#showyzm").removeClass("hidden");
                       $("#showyzm").addClass("block clearfix");
                       var imgSrc = $("#<%=verifycodeimg.ClientID%>");

                       var src = imgSrc.attr("src");
                       var timestamp = (new Date()).valueOf();
                       imgSrc.attr("src", "/VerifyCode.aspx?ts=" + timestamp);
                   }
               }


          
           }
         
           jQuery(function ($) {
      
               changeImg();
               $(document).on('click', "#shuaxin", function () {
                   changeImg();
           
               });
           })
        
		</script>
		<script type="text/javascript">
			jQuery(function($) {
			 $(document).on('click', '.toolbar a[data-target]', function(e) {
				e.preventDefault();
				var target = $(this).data('target');
				$('.widget-box.visible').removeClass('visible');//hide others
				$(target).addClass('visible');//show target
			 });
			});
			
			//you don't need this, just used for changing background
			jQuery(function ($) {
			    $('#btn-login-dark').on('click', function (e) {
			        $('body').attr('class', 'login-layout');
			        $('#id-text2').attr('class', 'white');
			        $('#id-company-text').attr('class', 'blue');

			        e.preventDefault();
			    });
			    $('#btn-login-light').on('click', function (e) {
			        $('body').attr('class', 'login-layout light-login');
			        $('#id-text2').attr('class', 'grey');
			        $('#id-company-text').attr('class', 'blue');

			        e.preventDefault();
			    });
			    $('#btn-login-blur').on('click', function (e) {
			        $('body').attr('class', 'login-layout blur-login');
			        $('#id-text2').attr('class', 'white');
			        $('#id-company-text').attr('class', 'light-blue');

			        e.preventDefault();
			    });
 
			 
			});
		</script>


          <%--  微信企业号访问内访问时，尝试获取当前微信号对应的账号，如果账号存在，提取这个账号的密码，自动写入界面并自动提交登录请求--%>
		<script type="text/javascript">
		    function isWeiXin() {
		        var ua = window.navigator.userAgent.toLowerCase();
		        if (ua.match(/MicroMessenger/i) == 'micromessenger') {
		            return true;
		        } else {
		            return false;
		        }
		    }
		    //var wx_username_fra = $(window.frames["wxcheckzdy"].document);
		    jQuery(function ($) {
		        var aulcscs = getUrlParam("aulcscs");
			    if (isWeiXin() && getUrlParam("aulgogo") != "1")
			    {
			        var appid = "<%=sCorpID%>";
			        var wx_checkurl = "<%=wx_checkurl%>";
			        var wx_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + wx_checkurl + "&response_type=code&scope=SCOPE#wechat_redirect";
			        //不能直接ajax访问，有跨域问题。 要跳转。
			        top.location.href = wx_url;
		
			    }

			    //如果有自动登录参数，则自动登录
			
			    if (getUrlParam("aulgogo") == "1" && aulcscs.indexOf("|") > 0)
			    {
			        var aulcscs_arr = new Array(); //定义一数组 
			        aulcscs_arr = aulcscs.split("|"); //字符分割 
			    
			        $("#zhanghao").val(aulcscs_arr[0]);
			        $("#mima").val(uncMe(aulcscs_arr[1], "mima"));
			        //alert(uncMe(aulcscs_arr[1], "mima"));
			        gogoajax1(formid1, buttonid1, url1, jkname_save1);
			    }
			  
			     
			   
			});

			
		</script>



             </form>
	</body>
</html>
