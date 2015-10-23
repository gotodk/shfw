<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo_umeditor.aspx.cs" Inherits="adminht_demo_umeditor" %>

<!DOCTYPE html>
<html lang="zh">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta charset="utf-8" />
    <title></title>

    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="/assets/css/bootstrap.css" />
        <link href="/umeditor1_2_2/themes/default/css/umeditor.min.css" type="text/css" rel="stylesheet">

    <!-- HTML5shiv and Respond.js for IE8 to support HTML5 elements and media queries -->

    <!--[if lte IE 8]>
		<script src="/assets/js/html5shiv.js"></script>
		<script src="/assets/js/respond.js"></script>
		<![endif]-->

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


    
    <script type="text/javascript" charset="utf-8" src="/umeditor1_2_2/umeditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/umeditor1_2_2/umeditor.min.js"></script>
    <script type="text/javascript" src="/umeditor1_2_2/lang/zh-cn/zh-cn.js"></script>
    <script type="text/javascript" src="/assets/js/desforcsharp.js"></script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
    由于umeditor跟ace的样式表、表单验证等相关功能有冲突，因此若非要使用此编辑器，需要独立页面使用。
        <br/>
        bianjiqi_html 和 bianjiqi_text是最终提交表单的内容加密过了的。
        <br/>
        这个编辑器也不支持移动优先原则，尽量别用。
        <br/>
                                                            <!--style给定宽度可以影响编辑器的最终宽度-->
<script type="text/plain" id="myEditor"   style="width:600px;height:240px;" name="bianjiqi"></script>
                                                    <input name="bianjiqi_html" type="hidden" id="bianjiqi_html">
                                                    <input name="bianjiqi_text" type="hidden" id="bianjiqi_text">

        <br/>
          <button type="button" id="addbutton1">保存  </button>
    </div>
    </form>
</body>
</html>
          <!-- **********umeditor编辑器处理******** -->
     <script type="text/javascript">
         //实例化编辑器
         var TTum = UM.getEditor('myEditor');
         //内容放入表单事件
         jQuery(function ($) {
             //添加提交事件
             $(document).on('click', "#addbutton1", function () {

                 if (TTum.hasContents()) {

                     $("#bianjiqi_html").val(encMe(TTum.getContent(), "mima"));
                     $("#bianjiqi_text").val(encMe(TTum.getContentTxt(), "mima"));
                 }
                 else {
                     $("#bianjiqi_html").val("");
                     $("#bianjiqi_text").val("");
                 }


                 


             });
         });
</script>