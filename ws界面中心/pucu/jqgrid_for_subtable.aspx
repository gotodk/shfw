<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jqgrid_for_subtable.aspx.cs" Inherits="pucu_jqgrid_for_subtable" %>


<!DOCTYPE html>
<!--[if lt IE 7 ]><html class="ie ie6" lang="zh"> <![endif]-->
<!--[if IE 7 ]><html class="ie ie7" lang="zh"> <![endif]-->
<!--[if IE 8 ]><html class="ie ie8" lang="zh"> <![endif]-->
<!--[if (gte IE 9)|!(IE)]><!-->
<html class="not-ie" lang="zh">
<!--<![endif]-->
<head><meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /><meta http-equiv="X-UA-Compatible" content="IE=9" /><meta charset="utf-8" /><title>
	 子表框架
</title><meta name="description" /><meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="/assets/css/bootstrap.css" /><link rel="stylesheet" href="/assets/css/font-awesome.css" /><link rel="stylesheet" href="/assets/css/jquery-ui.custom.css" /><link rel="stylesheet" href="/assets/css/chosen.css" />
    <!-- 往模板页附加的head内容 -->
    <link rel="stylesheet" href="/assets/css/datepicker.css" />
 
     <link rel="stylesheet" href="/assets/css/select2.css" />

        <link rel="stylesheet" href="/assets/css/ui.jqgrid.css" />

    	 

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
<body class="no-skin">
    <!-- #section:basics/navbar.layout -->
    <div id="navbar" class="navbar navbar-default">
        <script type="text/javascript">
            try { ace.settings.check('navbar', 'fixed') } catch (e) { }
        </script>

        <div class="navbar-container" id="navbar-container">
            <!-- #section:basics/sidebar.mobile.toggle -->
            <button type="button" class="navbar-toggle menu-toggler pull-left" id="menu-toggler" data-target="#sidebar">
                <span class="sr-only">Toggle sidebar</span>

                <span class="icon-bar"></span>

                <span class="icon-bar"></span>

                <span class="icon-bar"></span>
            </button>

            <!-- /section:basics/sidebar.mobile.toggle -->
            <div class="navbar-header pull-left">
                <!-- #section:basics/navbar.layout.brand -->
                <a href="javascript:void(0)" class="navbar-brand">
                    <small>
                        <i><img id="mysmlogo" border="0" /></i>
                        <span id="titleshowname">阿凡提demo --- 系统管理</span>
                    </small>
                </a>

                <!-- /section:basics/navbar.layout.brand -->

                <!-- #section:basics/navbar.toggle -->

                <!-- /section:basics/navbar.toggle -->
            </div>

            <!-- #section:basics/navbar.dropdown -->
            <div class="navbar-buttons navbar-header pull-right" role="navigation">
                <ul class="nav ace-nav">

 

                    <li class="green">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <i class="ace-icon fa fa-envelope icon-animated-vertical"></i>
                            <span class="badge badge-success">7</span>
                        </a>

                        <ul class="dropdown-menu-right dropdown-navbar dropdown-menu dropdown-caret dropdown-close">
                            <li class="dropdown-header">
                                <i class="ace-icon fa fa-envelope-o"></i>
                                有7条新站内信
                            </li>

                            <li class="dropdown-content">
                                <ul class="dropdown-menu dropdown-navbar">
<!--头--> 
          
<!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试专用消息所有 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/17 16:46:21</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试标题 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/15 18:14:51</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试标题 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/15 18:14:51</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试专用消息所有 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/17 16:46:21</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试专用消息所有 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/17 16:46:21</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试专用消息所有 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/17 16:46:21</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--中间循环部分-->
                                    <li>
 <a class="clearfix">
                                            <img src="/mytutu/defaulttouxiang.jpg" class="msg-photo" alt="" />
                                            <span class="msg-body">
                                                <span class="msg-title">
                                                    <span class="blue">测试:</span>
                                                    <br/>
                                                    测试标题 
                                                </br/>

                                                <span class="msg-time">
                                                    <i class="ace-icon fa fa-clock-o"></i>
                                                    <span>2015/7/15 18:14:51</span>
                                                </span>
                                            </span>
                                       </a>
                                    </li>

 <!--尾--> 
     
  

                                  

                                  
 

                                    
                                </ul>
                            </li>

                            <li class="dropdown-footer">
                                <a href="/adminht/myznx_shoudao_system.aspx">查看所有站内信
										<i class="ace-icon fa fa-arrow-right"></i>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <!-- #section:basics/navbar.user_menu -->
                    <li class="light-blue">
                        <a data-toggle="dropdown" href="#" class="dropdown-toggle">
                            <img class="nav-user-photo" src="/mytutu/defaulttouxiang_err.jpg" />
                            <small id="showusername">admin</small>
                            
                            <i class="ace-icon fa fa-caret-down"></i>
                        </a>

                        <ul class="dropdown-menu-right  dropdown-menu dropdown-caret dropdown-close">
                            <li>
                                <a href="/adminht/auth_editpassword.aspx">
                                    <i class="ace-icon fa fa-lock"></i>
                                    修改密码
                                </a>
                            </li>

                        </ul>
                    </li>
                                     <li class=" transparent">
                        <a data-toggle="dropdown" class="dropdown-toggle" href="#" id="forexit">
                            <i class="ace-icon fa fa-power-off"></i>
                            
                        </a>

 
                    </li>
                    <!-- /section:basics/navbar.user_menu -->
                </ul>
            </div>

            <!-- /section:basics/navbar.dropdown -->
        </div>
        <!-- /.navbar-container -->
    </div>

    <!-- /section:basics/navbar.layout -->
    <div class="main-container" id="main-container">
        <script type="text/javascript">
            try { ace.settings.check('main-container', 'fixed') } catch (e) { }
        </script>

        
<!-- #section:basics/sidebar -->
<div id="sidebar" class="sidebar                  responsive">
    <script type="text/javascript">
        try { ace.settings.check('sidebar', 'fixed') } catch (e) { }
    </script>

    <div class="sidebar-shortcuts" id="sidebar-shortcuts">
        <div class="sidebar-shortcuts-large" id="sidebar-shortcuts-large">
            <button class="btn btn-success" onclick="window.top.location.href='demo_home.aspx'">
                <i class="ace-icon fa fa-home"></i>
            </button>

            <button class="btn btn-info">
                <i class="ace-icon fa fa-pencil"></i>
            </button>

            <!-- #section:basics/sidebar.layout.shortcuts -->
            <button class="btn btn-warning">
                <i class="ace-icon fa fa-users"></i>
            </button>

            <button class="btn btn-grey">
                <i class="ace-icon fa fa-cogs"></i>
            </button>

            <!-- /section:basics/sidebar.layout.shortcuts -->
        </div>

        <div class="sidebar-shortcuts-mini" id="sidebar-shortcuts-mini">
            <span class="btn btn-success"></span>

            <span class="btn btn-info"></span>

            <span class="btn btn-warning"></span>

            <span class="btn btn-grey"></span>
        </div>
    </div>
    <!-- /.sidebar-shortcuts -->
 
    <ul id="masterpageleftmenu1_menuUL" class="nav nav-list"><li class=''><a href='/adminht/demo_home.aspx' ><i class='menu-icon fa fa-home'></i><span class='menu-text'> 欢迎页 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/fff.aspx' ><i class='menu-icon fa fa-bar-chart-o'></i><span class='menu-text'> 数据分析 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/sss.aspx' ><i class='menu-icon fa fa-cogs'></i><span class='menu-text'> 搜索系统设置 </span></a><b class='arrow'></b></li><li class='active open'><a href='' class='dropdown-toggle'><i class='menu-icon fa fa-cogs'></i><span class='menu-text'> 开发专用 </span><b class='arrow fa fa-angle-down'></b></a><b class='arrow'></b><ul class='submenu'><li class=''><a href='' class='dropdown-toggle'><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 权限数据维护 </span><b class='arrow fa fa-angle-down'></b></a><b class='arrow'></b><ul class='submenu'><li class=''><a href='/adminht/auth_menu_edit.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 系统菜单调整 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/auth_enum_list.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 权限枚举维护 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/auth_group_edit.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 权限组维护 </span></a><b class='arrow'></b></li></ul></li><li class='active open'><a href='' class='dropdown-toggle'><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 基本表单demo </span><b class='arrow fa fa-angle-down'></b></a><b class='arrow'></b><ul class='submenu'><li class=''><a href='/adminht/linshi/kongjian_fz.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 封装控件临时 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/demo_tijiao_edit.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 编辑保存demo </span></a><b class='arrow'></b></li><li class='active'><a href='/adminht/linshi/demo_pz_add.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 提交表单demo </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/demo_uploadface.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 上传头像demo </span></a><b class='arrow'></b></li></ul></li><li class=''><a href='/adminht/demo_liebiao.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 数据列表demo </span></a><b class='arrow'></b></li></ul></li><li class=''><a href='' class='dropdown-toggle'><i class='menu-icon fa fa-key'></i><span class='menu-text'> 系统权限配置 </span><b class='arrow fa fa-angle-down'></b></a><b class='arrow'></b><ul class='submenu'><li class=''><a href='/adminht/auth_oneuser.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 用户权限分配 </span></a><b class='arrow'></b></li><li class=''><a href='/adminht/auth_userlist.aspx' ><i class='menu-icon fa fa-caret-right'></i><span class='menu-text'> 用户权限浏览 </span></a><b class='arrow'></b></li></ul></li></ul>
    <!-- /.nav-list -->

    <!-- #section:basics/sidebar.layout.minimize -->
    <div class="sidebar-toggle sidebar-collapse" id="sidebar-collapse">
        <i class="ace-icon fa fa-angle-double-left" data-icon1="ace-icon fa fa-angle-double-left" data-icon2="ace-icon fa fa-angle-double-right"></i>
    </div>

    <!-- /section:basics/sidebar.layout.minimize -->
    <script type="text/javascript">
        try { ace.settings.check('sidebar', 'collapsed') } catch (e) { }
    </script>
</div>


        <!-- /section:basics/sidebar -->
        <div class="main-content">
            <div class="main-content-inner">
                <!-- #section:basics/content.breadcrumbs -->
                <div class="breadcrumbs" id="breadcrumbs">
                    <script type="text/javascript">
                        try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
                    </script>
                    <ul id="dongtaidaohang" class="breadcrumb"><li><i class='ace-icon fa fa-home home-icon'></i><a href='demo_home.aspx'>首页</a></li><li><a href='javascript:void(0)'>开发专用</a></li><li><a href='javascript:void(0)'>基本表单demo</a></li><li class='active'>提交表单demo</li></ul>
                    <!-- /.breadcrumb -->

                    
    <!-- 附加的本页导航栏内容 -->



                </div>

                <!-- /section:basics/content.breadcrumbs -->
                <div class="page-content">
              







                    
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->


           <div id="dialog-message" class="hide" >
										 
               <div class="row">

    <div class="col-sm-12" id="zheshiliebiaoquyu">
        


    </div>
</div>
            

										</div><!-- #dialog-message -->


            


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
                                    增加 -- 测试表单      
                                </a>
                            </li>


                        </ul>

                        <div class="tab-content">
                            <div id="addadd" class="tab-pane fade in active">

                                <div class="row">
                                    <div class="col-sm-12">

                                     


                                        <form class="form-horizontal" role="form" id="myform1">
                                            <!-- #section:elements.form -->
                                            <input type="hidden" name="zheshiyige_FID" id="zheshiyige_FID" value="D-A4A301780119-FBA61B05-3A19-4F14-BCEF"> 
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="fieldtest">
                                                    
                                                    <i class="light-red">*  </i>
                                                    测试字段：</label>

                                                <div class="col-sm-10">
 
                                                    
                                                    <div class="input-group col-xs-12 col-sm-5">
                                                         
                                                    <input data-rel="tooltip" type="text" id="fieldtest" name="fieldtest" placeholder="输入文字……" title="这是一个说明" data-placement="bottom" class="form-control search-query"  />

                                                    
                                                        
                                                        
                                                    <span class="input-group-btn"><button class=" btn  btn-sm  searchopenyhbspgogo" type="button" id="searchopenyhbspgogo_fieldtest" title="账号:地区,包装数量" guid="D-A4A30178435D-EA2EE72E-3923-4251-B041" >
												<span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
											</button> </span>
                                                    </div>

                                                    <div class=" col-sm-12 no-padding-left" id="show_searchopenyhbspgogo_fieldtest"></div>
                                                    

                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>

                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zhanghao">
                                                    
                                                    <i class="light-red">*  </i>
                                                    账号：</label>

                                                <div class="col-sm-10">
 
                                                    
                                                    <div class="input-group col-xs-12 col-sm-5">
                                                         
                                                    <input data-rel="tooltip" type="text" id="zhanghao" name="zhanghao" placeholder="输入文字……" title="这是一个说明" data-placement="bottom" class="form-control search-query"  />

                                                    
                                                        
                                                        
                                                    <span class="input-group-btn"><button class=" btn  btn-sm  searchopenyhbspgogo" type="button" id="searchopenyhbspgogo_zhanghao" title="创建日期:地区" guid="D-A4A30178435D-EA2EE72E-3923-4251-ccccccc" >
												<span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
											</button> </span>
                                                    </div>

                                                    <div class=" col-sm-12 no-padding-left" id="show_searchopenyhbspgogo_zhanghao"></div>
                                                    

                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>

                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="mima"> 
                                                    <i class="light-red">*  </i>
                                                    密码：</label>

                                                <div class="col-sm-10">
                                                    <input data-rel="tooltip" type="password" id="mima" name="mima" placeholder="输入文字……" title="这是一个说明" data-placement="bottom" class="col-xs-12 col-sm-5" />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="xialakuang">
                                                    
                                                    <i class="light-red">*  </i>
                                                    会议室：</label>

                                                <div class="col-sm-10">
                                                    <select class="col-xs-12 col-sm-5" id="xialakuang" name="xialakuang">
                                                        <option value="" selected>请选择</option>
                                                         <option value='001'>东会议室</option> <option value='002'>西会议室</option> <option value='003'>三楼会议室</option> <option value='004'>阶梯教室</option> <option value='005'>大礼堂</option> <option value='006'>食堂</option>
                                                    </select>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="xingbie">
                                                    
                                                    <i class="light-red">*  </i>
                                                    性别：</label>

                                                <div class="col-sm-10">

                                                    <div class="no-padding-left radio col-xs-12 col-sm-5">


                                                        <label><input name='xingbie' type='radio' value='男' class='ace'/><span class='lbl'>男性</span></label><label><input name='xingbie' type='radio' value='女' class='ace'/><span class='lbl'>女性</span></label>

                                                        
                                                        
                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <hr />
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="quanxian">
                                                    
                                                    <i class="light-red">*  </i>
                                                    权限例子：</label>

                                                <div class="col-sm-10">

                                                    <div class="no-padding-left checkbox col-xs-12 col-sm-5">



                                                        <label><input name='quanxian' type='checkbox' value='add' class='ace'/><span class='lbl'>添加</span></label><label><input name='quanxian' type='checkbox' value='del' class='ace'/><span class='lbl'>删除</span></label><label><input name='quanxian' type='checkbox' value='edit' class='ace'/><span class='lbl'>修改</span></label>


                                                    




                                                    </div>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                             <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zhengshushuliang">
                                                    
                                                    <i class="light-red">*  </i>
                                                    数量：</label>

                                                <div class="col-sm-10">
                                                    <input type="text" class="input-mini" id="zhengshushuliang" name="zhengshushuliang"    />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                             <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="erweixiao">
                                                    
                                                    <i class="light-red">*  </i>
                                                    价格：</label>

                                                <div class="col-sm-10">
                                                    <input type="text" class="input-mini" id="erweixiao" name="erweixiao"    />
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="yigeriqi">
                                                    
                                                    <i class="light-red">*  </i>
                                                    有效日期：</label>

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
                                                <label class="col-sm-2 control-label no-padding-right" for="riqiqujian">
                                                    
                                                    <i class="light-red">*  </i>
                                                    日期段：</label>

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
                                                <label class="col-sm-2 control-label no-padding-right" for="beizhu">
                                                    
                                                    <i class="light-red">*  </i>
                                                    其他备注：</label>

                                                <div class="col-sm-10">
                                                    <textarea placeholder="请选择……" class="limited col-xs-12 col-sm-5" id="beizhu" name="beizhu" maxlength="50" rows="5"></textarea>
                                                    <span class="help-button" data-rel="popover" data-trigger="hover" data-placement="left" data-content="这是一个说明" title="录入要求">?</span>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="bianjiqi">
                                                    
                                                    <i class="light-red">*  </i>
                                                    复杂格式：</label>

                                                <div class="col-sm-10">
                                                    <div class="wysiwyg-editor" id="bianjiqi"></div>
                                                    <input name="bianjiqi_html" type="hidden" id="bianjiqi_html">
                                                    <input name="bianjiqi_text" type="hidden" id="bianjiqi_text">
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="xialakuangduoxuan">
                                                    
                                                    <i class="light-red">*  </i>
                                                    下拉框多选：</label>

                                                <div class="col-sm-10">
                                                    <select multiple=""  data-placeholder="请选择…"  class=" select2 tag-input-style" id="xialakuangduoxuan"  name="xialakuangduoxuan">
                                                     
                                                         <option value='001'>一号</option> <option value='002'>二号</option> <option value='003'>三号</option>
                                                    </select>
                                                    <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                                </div>
                                            </div>
                                            

                                            <div class="form-group">
                                                <label class="col-sm-2 control-label no-padding-right" for="zibiao001">
                                                    
                                                    <i class="light-red">*  </i>
                                                    这是一个子表：</label>

                                                <div class="col-sm-10">
                                                    <iframe src="/pucu/jqgrid_for_subtable.aspx" name="xxxx1" id="xxxx1" frameborder="0"  style="height:300px; width:100%;" scrolling="no" width="0"></iframe>
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












                </div>
                <!-- /.page-content -->
            </div>
        </div>
        <!-- /.main-content -->

        <a href="#" id="btn-scroll-up" class="btn-scroll-up btn btn-sm btn-inverse">
            <i class="ace-icon fa fa-angle-double-up icon-only bigger-110"></i>
        </a>
    </div>
    <!-- /.main-container -->

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
    <script src="/assets/js/ace/ace.ajax-content.js"></script>
    <script src="/assets/js/ace/ace.touch-drag.js"></script>
    <script src="/assets/js/ace/ace.sidebar.js"></script>
    <script src="/assets/js/ace/ace.sidebar-scroll-1.js"></script>
    <script src="/assets/js/ace/ace.submenu-hover.js"></script>
    <script src="/assets/js/ace/ace.widget-box.js"></script>
    <script src="/assets/js/ace/ace.widget-on-reload.js"></script>
    <script src="/assets/js/ace/ace.searchbox-autocomplete.js"></script>


    <script type="text/javascript">

    
        jQuery(function ($) {

            //$(document).off('click.bs.dropdown.data-api');
            $(document).on('click', "#forexit", function () {
                
                bootbox.confirm({  
                    buttons: {  
                        confirm: {  
                            label: '确定',  
                            className: 'btn-sm btn-danger'
                        },  
                        cancel: {  
                            label: '取消',  
                            className: 'btn-sm'
                        }  
                    },  
                    message: '您确定要退出登录吗？',  
                    callback: function(result) {  
                        if (result) {
                            window.top.location.href = "/adminht/login.aspx?f=exit";
                            
                        } else {  
                            //alert('点击取消按钮了');  
                        }  
                    },  
                    //title: "退出登录",  
                });
            });

        });



    </script>
    
    <!-- 附加的body底部本页专属的自定义js脚本 -->
    <script src="/assets/js/jquery.validate.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>



        <!-- page specific plugin scripts -->
    <script src="/assets/js/jqGrid/jquery.jqGrid.min.js"></script>
    <script src="/assets/js/jqGrid/i18n/grid.locale-cn.js"></script>

    <script src="/assets/js/jquery.hotkeys.js"></script>
    <script src="/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="/assets/js/ace/elements.colorpicker.js"></script>
    <script src="/assets/js/ace/elements.wysiwyg.js"></script>

    <script type="text/javascript" src="/assets/js/desforcsharp.js"></script>

    <script src="/assets/js/select2.js"></script>

    

    <!-- **********全局变量配置******** -->
    <script type="text/javascript">
        //配置参数
        var gogoajax1_CanRun = true;//ajax提交防重复
        var formid1 = "#myform1";//表单id
        var buttonid1 = "#addbutton1";//提交按钮id
        var url1 = "/ajax_pp_do.aspx";//处理页面
        var jkname_save1 = "提交表单demo";//保存数据接口名
        var jkname_page1 = encodeURIComponent("获取弹窗内分页数据");//获取分页数据接口名
        var jkname_del1 = encodeURIComponent("xxxxxxxxxx");//删除数据接口名
        var dialog_tanchuang = null;
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

 


            //添加提交事件(编辑器)
            $(document).on('click', buttonid1, function () {
                if ($('#bianjiqi').text().trim() != '') {
                    $('#bianjiqi_html').val(encMe($('#bianjiqi').html(), 'mima'));
                    $('#bianjiqi_text').val(encMe($('#bianjiqi').text(), 'mima'));
    }
                else {
                    $('#bianjiqi_html').val('');
                    $('#bianjiqi_text').val('');
}
            });

            $('#bianjiqi').ace_wysiwyg(); 





 



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
        $.validator.addMethod("_xxxxxxxxxxxx", function (value, element) {
            return true;
        }, "_xxxxxxxxxxxx");
        jQuery(function ($) {
            //表单验证

            $(formid1).validate({
                errorElement: 'div',
                errorClass: 'help-block',
                ignore: "",
                rules: {
               
                  

fieldtest : {


required: true, 
minlength: 0, 
maxlength: 50, 
 email: true, equalTo:'#mima',  
_xxxxxxxxxxxx: [''] 

 
},



zhanghao : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



mima : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



xialakuang : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



xingbie : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



fenzuxian1 : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



quanxian : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



zhengshushuliang : {


required: true, 
minlength: 0, 
maxlength: 9999999, 
 
_xxxxxxxxxxxx: [''] 

 
},



erweixiao : {


required: true, 
minlength: 0, 
maxlength: 9999999, 
lrunlv: [''], 
 
_xxxxxxxxxxxx: [''] 

 
},



yigeriqi : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



riqiqujian1 : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



beizhu : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



bianjiqi_html : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



xialakuangduoxuan : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},



zibiao001 : {


required: true, 
minlength: 0, 
maxlength: 50, 
 
_xxxxxxxxxxxx: [''] 

 
},

 _xxxxxxxxxxxx: {} 
                },

                messages: {
                   

fieldtest : {


required: "请输入测试字段", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
email: '必须是电子邮件格式',equalTo:'输入值必须跟密码一致',   
_xxxxxxxxxxxx: [''] 

 
},



zhanghao : {


required: "请输入账号", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



mima : {


required: "请输入密码", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



xialakuang : {


required: "请输入会议室", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



xingbie : {


required: "请输入性别", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



fenzuxian1 : {


required: "请输入分组线1", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



quanxian : {


required: "请输入权限例子", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



zhengshushuliang : {


required: "请输入数量", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



erweixiao : {


required: "请输入价格", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



yigeriqi : {


required: "请输入有效日期", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



riqiqujian1 : {


required: "请输入日期段", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



beizhu : {


required: "请输入其他备注", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



bianjiqi_html : {


required: "请输入复杂格式", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



xialakuangduoxuan : {


required: "请输入下拉框多选", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},



zibiao001 : {


required: "请输入这是一个子表", 
minlength: $.validator.format("最少{0}个字符"), 
maxlength: $.validator.format("最多{0}个字符"), 
 
_xxxxxxxxxxxx: [''] 

 
},

 _xxxxxxxxxxxx: {} 
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

 
            //过滤和数字控制
              $('#zhengshushuliang').ace_spinner({ value: 0, min: 0, max: 9999999, step: 1, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' }); 
 $('#erweixiao').ace_spinner({ value: 0, min: 0, max: 9999999, step: 0.01, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up', icon_down: 'ace-icon fa fa-caret-down' }); 


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


            //select2
            $('.select2').css('width', '220px').select2({ allowClear: true })


            //弹窗
            $(".searchopenyhbspgogo").on('click', function (e) {
                openeditdialog(e,$(this));
            });

            function c_dialog_yinru(kczd)
            {
                var open_now_ziduan = kczd.attr('id').replace("searchopenyhbspgogo_", "");

                var open_now_ziduan_byy = kczd.attr('title');

                var ids = $(grid_selector).jqGrid('getGridParam', 'selarrrow');
                if (ids.length <= 1) {
                    if (ids.length == 1) {
                        var rowId = $(grid_selector).jqGrid("getGridParam", "selrow");
                        var rowData = $(grid_selector).jqGrid("getRowData", rowId);

            
                        if (open_now_ziduan_byy.indexOf(':') > 0)
                        {
                            //有显示值
                            var open_now_ziduan_byy_arr = new Array();
                            open_now_ziduan_byy_arr = open_now_ziduan_byy.split(":");
                            var g00 = new Array();
                            g00 = open_now_ziduan_byy_arr[0].split(",");
                            var str_00 = "";
                            for(var i = 0;i < g00.length;i++)
                            {
                                str_00 = str_00 + rowData[g00[i]] + ",";
                            }
                            var g11 = new Array();
                            g11 = open_now_ziduan_byy_arr[1].split(",");
                            var str_11 = "";
                            for (var i = 0; i < g11.length; i++) {
                                str_11 = str_11 + "[" + g11[i] + ":" + rowData[g11[i]] + "]  ";
                            }
                            $("#" + open_now_ziduan).val(str_00.substring(0, str_00.length - 1));
                            $("#show_searchopenyhbspgogo_" + open_now_ziduan).html(str_11.substring(0, str_11.length - 1));
                           
                        }
                        else
                        {
                            //无显示值
                            var g00 = new Array();
                            g00 = open_now_ziduan_byy.split(",");
                            var str_00 = "";
                            for (var i = 0; i < g00.length; i++) {
                                str_00 = str_00 + rowData[g00[i]] + ",";
                            }
                            $("#" + open_now_ziduan).val(str_00.substring(0, str_00.length - 1));
                            $("#show_searchopenyhbspgogo_" + open_now_ziduan).html("");
                        }
                      
             

                    }

                } else {
                    alert("此版本不支持多选，多选用于特殊情况需要特殊处理");
                }

                dialog_tanchuang.dialog("close");
             
            }

            function openeditdialog(e, kczd)
            {

                //重新生成一个新的弹窗
                $t = $("<table id=\"grid-table\"></table><div id=\"grid-pager\"></div>");

                $("#zheshiliebiaoquyu").empty().html($t);
 

                kczd.attr('disabled', "true");
                var aj = $.ajax({
                    url: '/pucu/jqgirdjs_for_dialog.aspx?guid=' + kczd.attr('guid'),
                    type: 'get',
                    cache: false,
                    dataType: 'html',
                    success: function (data) {
                        //alert(data);

                        eval(data);

                        var open_now_ziduan = kczd.attr('id').replace("searchopenyhbspgogo_", "");
                        e.preventDefault();

                        dialog_tanchuang = $("#dialog-message").removeClass('hide').dialog({
                            modal: true,
                            title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-check'></i> 选择并引入--测试信息表</h4></div>",
                            width: '80%',
                            buttons: [
                                {
                                    text: "  取消选择  ",
                                    "class": "btn btn-xs",
                                    click: function () {
                                        $(this).dialog("close");
                                    }
                                },
                                {
                                    text: "  确认引入  ",
                                    "class": "btn btn-primary btn-xs querenyinruanniu",
                                    click: function () {
                                        c_dialog_yinru(kczd);
                                    }
                                }
                            ]
                        });

                        kczd.removeAttr("disabled");

 
                        var postData = $(grid_selector).jqGrid("getGridParam", "postData");
                        $.extend(postData, { this_extforinfoFSID: kczd.attr('guid') });
                        $(grid_selector).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据



                    },
                    error: function () {
                        kczd.removeAttr("disabled");
                        alert("加载列表配置失败！");
                    }
                });

 


            }

 


            //附加其他自定义控制

        });



    </script>








        <!-- **********jqgrid相关处理******** -->
    <script type="text/javascript">
 


        var grid_selector = "#grid-table";
        var pager_selector = "#grid-pager";

 






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
                $(grid_selector).setGridWidth($(window).width() * 0.78);
            });
            ////resize on sidebar collapse/expand
            //var parent_column = $(grid_selector).closest('[class*="col-"]');
            //$(document).on('settings.ace.jqGrid', function (ev, event_name, collapsed) {
            //    if (event_name === 'sidebar_collapsed' || event_name === 'main_container_fixed') {
            //        //setTimeout is for webkit only to give time for DOM changes and then redraw!!!
            //        setTimeout(function () {
            //            $(grid_selector).jqGrid('setGridWidth', parent_column.width());
            //        }, 0);
            //    }
            //});

      
 
          //==========原来表格代码在这里
   









        });


    </script>


</body>
</html>
