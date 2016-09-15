<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_script.ascx.cs" Inherits="pucu_wuc_script" %>
                     <%
     bool onlyshow = false; //仅显示的标记
     //发现预览标志 
     if (Request["showinfo"] != null && Request["fff"] != null && Request["fff"].ToString() == "1")
     {
         onlyshow = true;
     }%>

    <!-- 附加的body底部本页专属的自定义js脚本 -->
    <script src="/assets/js/jquery.validate.js"></script>
<script src="/assets/js/jquery.colorbox.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
<script src="/assets/js/date-time/dateRangeUtil.js"></script>

    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>

    <script src="/assets/js/dropzone.js"></script>

    <!-- page specific plugin scripts -->
    <script src="/assets/js/jqGrid/jquery.jqGrid.min.js"></script>
    <script src="/assets/js/jqGrid/i18n/grid.locale-cn.js"></script>
    <script src="/assets/js/jqGrid/grid.setcolumns.js"></script>

    <script src="/assets/js/jquery.hotkeys.js"></script>
    <script src="/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="/assets/js/ace/elements.colorpicker.js"></script>
    <script src="/assets/js/ace/elements.wysiwyg.js"></script>

    <script type="text/javascript" src="/assets/js/desforcsharp.js"></script>

    <script src="/assets/js/select2.js"></script>


    <script src="/assets/js/yhb_city.js"></script>

<script src="/assets/js/jquery.PrintArea.js"></script>

    <%
        string[] strfin = FormsValidateMod.GetJS_fin(dsFPZ, (Hashtable)(htPP["othercheck"]));
    %>

    <!-- **********全局变量配置******** -->
    <script type="text/javascript">
 
        //配置参数
        var gogoajax1_CanRun = true;//ajax提交防重复
        var formid1 = "#myform1";//表单id
        var buttonid1 = "#addbutton1";//提交按钮id
        var url1 = "/ajax_pp_do.aspx";//处理页面
        var jkname_save1 = "<% =dsFPZ.Tables["表单配置主表"].Rows[0]["Frun_add"].ToString()%>";//保存数据接口名
        var jkname_save2 = "<% =dsFPZ.Tables["表单配置主表"].Rows[0]["Frun_edit"].ToString()%>";//保存数据接口名
        var jkname_info1 = "<% =dsFPZ.Tables["表单配置主表"].Rows[0]["Frun_showinfo_foredit"].ToString()%>";//获取数据接口名
        var jkname_page1 = encodeURIComponent("获取弹窗内分页数据");//获取分页数据接口名
        var jkname_del1 = encodeURIComponent("xxxxxxxxxx");//删除数据接口名
        var dialog_tanchuang = null;
        var dialog_tanchuang_subtable = null;
        //ajax系统错误统一提示
        function errorForAjax(XMLHttpRequest, textStatus, errorThrown) {
            // 通常情况下textStatus和errorThown只有其中一个有值 
            bootbox.alert("抱歉，系统出现问题!<br/>" + "textStatus:" + textStatus + "<br/>XMLHttpRequest.readyState:" + XMLHttpRequest.readyState + "<br/>XMLHttpRequest.status:" + XMLHttpRequest.status);
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
            if (r != null) return  r[2] ; return ""; //返回参数值
        }
        function GetPageName() {
            var url = window.location.href;//获取完整URL 
            var tmp = new Array();//临时变量，保存分割字符串 
            tmp = url.split("/");//按照"/"分割 
            var pp = tmp[tmp.length - 1];//获取最后一部分，即文件名和参数 
            tmp = pp.split("?");//把参数和文件名分割开 
            return tmp[0];
        }

        var gridview_cs_for_ie = true;
        function isIE() {
            if (!!window.ActiveXObject || "ActiveXObject" in window) {
                return true;
            } else {
                return false;
            }
        }

        function IEVersion() {
            var rv = -1;
            if (navigator.appName == 'Microsoft Internet Explorer') {
                var ua = navigator.userAgent;
                var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
                if (re.exec(ua) != null)
                    rv = parseFloat(RegExp.$1);
            } else if (navigator.appName == 'Netscape') {
                var ua = navigator.userAgent;
                var re = new RegExp("Trident/.*rv:([0-9]{1,}[\.0-9]{0,})");
                if (re.exec(ua) != null)
                    rv = parseFloat(RegExp.$1);
            }
            return rv;
        }
        if (isIE()) {
            if (IEVersion() < 10) {
                gridview_cs_for_ie = false; //针对ie9，设置jqgrid的参数。使其兼容
            };
        };

        function js_method_kj(t1,t2,sp) //日期段快捷设置
        {
            if (sp == "本周")
            {
                var startStop = dateRangeUtil.getCurrentWeek();
                $("#" + t1).datepicker('setDate', startStop[0]);
                $("#" + t2).datepicker('setDate', startStop[1]);
            }
            if (sp == "本月") {
                var startStop = dateRangeUtil.getCurrentMonth();
                $("#" + t1).datepicker('setDate', startStop[0]);
                $("#" + t2).datepicker('setDate', startStop[1]);
            }
            if (sp == "本季度") {
                var startStop = dateRangeUtil.getCurrentSeason();
                $("#" + t1).datepicker('setDate', startStop[0]);
                $("#" + t2).datepicker('setDate', startStop[1]);
            }
            if (sp == "本年") {
                var startStop = dateRangeUtil.getCurrentYear();
                $("#" + t1).datepicker('setDate', startStop[0]);
                $("#" + t2).datepicker('setDate', startStop[1]);
            }
        }


        function delQueStr(url, ref) //删除参数值
        {
            var str = "";

            if (url.indexOf('?') != -1)
                str = url.substr(url.indexOf('?') + 1);
            else
                return url;
            var arr = "";
            var returnurl = "";
            var setparam = "";
            if (str.indexOf('&') != -1) {
                arr = str.split('&');
                for (i in arr) {
                    if (arr[i].split('=')[0] != ref) {
                        returnurl = returnurl + arr[i].split('=')[0] + "=" + arr[i].split('=')[1] + "&";
                    }
                }
                return url.substr(0, url.indexOf('?')) + "?" + returnurl.substr(0, returnurl.length - 1);
            }
            else {
                arr = str.split('=');
                if (arr[0] == ref)
                    return url.substr(0, url.indexOf('?'));
                else
                    return url;
            }
        }


        (function ($) {
            $.extend({
                Request: function (m) {
                    var sValue = location.search.match(new RegExp("[\?\&]" + m + "=([^\&]*)(\&?)", "i"));
                    return sValue ? sValue[1] : sValue;
                },
                UrlUpdateParams: function (url, name, value) {
                    var r = url;
                    if (r != null && r != 'undefined' && r != "") {
                        value = encodeURIComponent(value);
                        var reg = new RegExp("(^|)" + name + "=([^&]*)(|$)");
                        var tmp = name + "=" + value;
                        if (url.match(reg) != null) {
                            r = url.replace(eval(reg), tmp);
                        }
                        else {
                            if (url.match("[\?]")) {
                                r = url + "&" + tmp;
                            } else {
                                r = url + "?" + tmp;
                            }
                        }
                    }
                    return r;
                }




            });
        })(jQuery);





    </script>



    <!-- **********编辑器处理(一定放最前面)******** -->
    <script type="text/javascript">
        jQuery(function ($) {


<%=strfin[3]%>






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
        
            var JSON_sub_str = "";
            var canbesavesub = 0;
            $("table[id^='grid-table-subtable-']").each(function()
            {
                var objDB = $(this).jqGrid("getRowData");
         
                JSON_sub_str = JSON_sub_str + "&" + $(this).attr('id') + "_fcjsq=" + objDB.length + "&" + $(this).attr('id') + "=" + encMe(JSON.stringify(objDB), "mima");
                if ($(this).attr('lastsel_yhb') != "-999999")
                {
                    canbesavesub++;
                }
                
            });
            
            if (canbesavesub > 0)
            {
                bootbox.alert("错误：某些子表尚未保存，无法提交!");
                return false;
            }
   

            //防重复提交
            if (!gogoajax1_CanRun) {
                return false;
            }

            var tijiaoanniu_str_old = $(t_buttonid).text();

            //验证
            if (!$(t_formid).valid()) {

                return false;
            }
            else {
                //显示等待提示，禁用必要区域
                gogoajax1_CanRun = false;
                $(t_buttonid).addClass("disabled");
                $(t_buttonid).html("<i class='ace-icon fa fa-spinner fa-spin orange bigger-110'></i>正在处理");
                $(t_buttonid+"_top").addClass("disabled");
                $(t_buttonid + "_top").html("<i class='ace-icon fa fa-floppy-o bigger-120 blue'></i>正在处理");
            }


            function callback(msg) {

           
                var isedit = getUrlParam("fff");
                             var ppp_pp = msg.match(/{(\S*)}/);                var newguid_re = "";                if (ppp_pp && ppp_pp.length > 0)                {
                    msg = msg.replace(ppp_pp[0],"");                    newguid_re=ppp_pp[1];
                }             

                try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().tooltip('hide'); } catch (e) { }
                //显示提交结果
                bootbox.alert({
                    message: msg,
                    callback: function () {
                        setTimeout(function () {
                            try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().select().tooltip('hide'); } catch (e) { }
                            if (newguid_re != "") {
                               
                                //跳转到编辑
                                 
                                var newurl = $.UrlUpdateParams(window.location.href, "idforedit", newguid_re);
                                newurl = $.UrlUpdateParams(newurl, "fff", "1");
                                newurl = $.UrlUpdateParams(newurl, "showinfo", "1");

                                //对某些特殊结构的非标准化单据提供额外的参数，这个是定制的，针对新增。
                                if (isedit != "1")
                                {
                                    if (window.location.href.indexOf("adminht/corepage/xsfh/fc_shenqing.aspx") > 0 || window.location.href.indexOf("adminht/corepage/fanchang/fc_shenqing.aspx") > 0) {
                                        //销售发货的发货申请新增，或返厂申请新增
                                        newurl = $.UrlUpdateParams(newurl, "ywlx", "bianjicaogao");
                                    }
                                    if (window.location.href.indexOf("adminht/corepage/fwbg/edit_fwbg.aspx") > 0) {
                                        //服务报告新增
                                        newurl = $.UrlUpdateParams(newurl, "yc_czlx", "xiugai");
                                    }
                                }
                                
                                

                                location.href = newurl;
                                
                            }
                            
                        }, 1);
                    
                }  });

           
                if (isedit == "1") {
                    //加载表单数据
                    loadinfoajax1($("#idforedit").val());

                    //是更新，强制调用一个函数，不一定存在
                    try {
                        eval("editok_after_msgshow('" + msg + "')");
                    } catch (e) { }

                }
                else {
                    //不是更新，强制调用一个函数，不一定存在
                    try
                    {
                        eval("addok_after_msgshow('" + msg + "')");
                    }catch(e){}
                   

                }
               
                
                
           

                //最后跑这个
                setTimeout(function () {
                    //取消等待显示，开放禁用区域
                    gogoajax1_CanRun = true;
                    $(t_buttonid).removeClass("disabled");
                    $(t_buttonid).html("<i class='ace-icon fa fa-check bigger-110'></i>" + tijiaoanniu_str_old);
                    $(t_buttonid+"_top").removeClass("disabled");
                    $(t_buttonid + "_top").html("<i class='ace-icon fa fa-floppy-o bigger-120 blue'></i>" + tijiaoanniu_str_old);
                }, 1500)


            };
            $.ajax({
                type: "POST",
                url: t_url + "?guid=" + randomnumber(),
                dataType: "html",
                data: "ajaxrun=save&jkname=" + t_jkname + "&" + $(t_formid).serialize() + JSON_sub_str,
                success: callback, //请求成功
                error: errorForAjax//请求出错 

            });
        }


        jQuery(function ($) {
            var isedit = getUrlParam("fff");
         
            var showinfo = getUrlParam("showinfo");
            if (isedit == "1") {
                $(".c_fanhuishangyiye_top").removeClass("hidden");
            }
            else {
                $(".c_fanhuishangyiye_top").addClass("hidden");

            }
            

            //处理showinfo标志
            if (showinfo == "1") {
                $(".c_xinzeng_top").removeClass("hidden");
                $(".c_bianji_top").removeClass("hidden");
            }
            if (showinfo == "2") {
                //仅查看
            }
             
            $(document).on('click', "#xinzeng_top", function () {
                var newurl = delQueStr(window.location.href, "fff");
                newurl = delQueStr(newurl, "idforedit");
                newurl = delQueStr(newurl, "showinfo");
                location.href = newurl;

            });
            $(document).on('click', "#bianji_top", function () {
                var newurl = delQueStr(window.location.href, "showinfo");
                
                location.href = newurl;

            });

            //添加返回代码
            $(document).on('click', "#fanhuishangyiye_top", function () {
                if (location.href.indexOf("edit_mygzt.aspx") > 0)
                {
                    location.href = "/adminht/demo_home.aspx?guidrere=" + randomnumber();
                }
                else
                {
                    location.href = document.referrer;
                    //history.back(-1);
                }
                

            });
            
            //添加提交事件
            $(document).on('click', buttonid1+"_top", function () {
                if (isedit == "1") {
                    gogoajax1(formid1, buttonid1, url1, jkname_save2);
                }
                else {
                    gogoajax1(formid1, buttonid1, url1, jkname_save1);
                }

            });
            $(document).on('click', buttonid1, function () {

           
                if (isedit == "1") {
                    gogoajax1(formid1, buttonid1, url1, jkname_save2);
                }
                else {
                    gogoajax1(formid1, buttonid1, url1, jkname_save1);
                }
              
            });
        });
    </script>





    <!-- **********ajax获初始化表单数据******** -->
    <script type="text/javascript">

         
        function callback_info(xml) {


            //解析xml并显示在界面上
            if ($(xml).find('返回值单条>执行结果').text() != "ok") {
          
                bootbox.alert("查找数据失败!");
                return false;
            };

         




            <%
        for (int i = 0; i < dsFPZ.Tables["表单配置子表"].Rows.Count; i++)
        {
            string[] ARR_list_static = dsFPZ.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"].ToString().Split(',');
            string FS_name_temp = dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString();
                                            %>
            <%
        
        switch (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_type"].ToString())
        {
            case "输入框":
                                            %>

            <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>

            <%    break;
        case "密码框":
                                                        %>
                        <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>
            <%
            break;

        case "下拉框":
                                                        %>


            <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            
            var sssss_zhi_show = $("#<%=FS_name_temp%> option[value='" + sssss_zhi + "']").text();
            if (sssss_zhi_show != sssss_zhi) {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show + '[' + sssss_zhi + ']');
            }
            else {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show);
            }

            
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>

    
            <%
            break;

        case "单选框":
                                                        %>




             <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            
            var sssss_zhi_show = $("input:radio[name='<%=FS_name_temp%>'][value='" + $(xml).find('数据记录><%=FS_name_temp%>').text() + "']").next('span').text();
            
            if (sssss_zhi_show != sssss_zhi) {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show + '[' + sssss_zhi + ']');
            }
            else {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show);
            }

            
            <%}
        else
        { %>
            $("input:radio[name='<%=FS_name_temp%>'][value='" + $(xml).find('数据记录><%=FS_name_temp%>').text() + "']").prop("checked", true);
            <%}%>



            
            <%
            break;

        case "普通多选框":
                                                        %>



            <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            var sssss_zhi_show = "";
            $("input[name='<%=FS_name_temp%>']:checkbox").each(function () {
                if (("," + sssss_zhi + ",").indexOf("," + $(this).val() + ",") >= 0) {
                    sssss_zhi_show = sssss_zhi_show + $(this).next('span').text() + ',';
                }

            });
        



            if (sssss_zhi_show.replace(/,$/g, "") != sssss_zhi) {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show + '[' + sssss_zhi + ']');
            }
            else {
                $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show);
            }

            
            <%}
        else
        { %>
                        $("input[name='<%=FS_name_temp%>']").prop("checked", false);
            $("input[name='<%=FS_name_temp%>']:checkbox").each(function () {
                if (("," + $(xml).find('数据记录><%=FS_name_temp%>').text() + ",").indexOf("," + $(this).val() + ",") >= 0) {
                    $(this).prop("checked", true);
                }

            });
            <%}%>









            <%
            break;

        case "下拉多选框":
                                                          %>
            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%> option").each(function () {
                if ($(this).val() == $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text()) {
 
                    $(this).attr("selected", true);
                }
            });
        
                <%
            break;

        case "省市区联动":

                                                    %>
                //省市区赋值()
            var promary_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> = $(xml).find('数据记录>yhb_city_Promary_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>').text();
            var city_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> = $(xml).find('数据记录>yhb_city_City_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>').text();
            var qu_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> = $(xml).find('数据记录>yhb_city_Qu_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>').text();
            if (promary_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> != "")
                {
                    $("#yhb_city_Promary_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>").val(promary_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>);
                    $("#yhb_city_Promary_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>").trigger('change');
                }
            if (city_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> != "") {
                    $("#yhb_city_City_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>").val(city_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>);
                    $("#yhb_city_City_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>").trigger('change');
                }
            if (qu_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %> != "") {
                    $("#yhb_city_Qu_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>").val(qu_s_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>);
            }

            <% if (onlyshow)
        {%>  
            var sssss_zhi = promary_s_<%=FS_name_temp %> + ',' + city_s_<%=FS_name_temp %> + ',' + qu_s_<%=FS_name_temp %>;
            var sssss_zhi_show = $("#yhb_city_Promary_<%=FS_name_temp %> option[value='" + promary_s_<%=FS_name_temp %> + "']").text() + ',' + $("#yhb_city_City_<%=FS_name_temp %> option[value='" + city_s_<%=FS_name_temp %> + "']").text() + ',' + $("#yhb_city_Qu_<%=FS_name_temp %> option[value='" + qu_s_<%=FS_name_temp %> + "']").text();
            sssss_zhi_show = sssss_zhi_show.replace("请选择省份", "").replace("请选择城市", "").replace("请选择区县", "");
            $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi_show + '[' + sssss_zhi + ']');
            <% }%>  

                <%


            break;

        case "整数":
                                                        %>
                <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>
                <%
            break;
        case "两位小数":
                                                         %>
                <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            $("#fifsssss_<%=FS_name_temp%>").text(sssss_zhi);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>
                <%
            break;
        case "日期框":
                                                        %>
             var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            var time_zz = new Date(sssss_zhi).Format_go("yyyy-MM-dd");
            if (time_zz == "" || time_zz == null || time_zz.indexOf("aN") >= 0) {
                time_zz = null;
            }

            <% if (onlyshow)
        {%>  
            
            $("#fifsssss_<%=FS_name_temp%>").text(time_zz);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").datepicker('setDate', time_zz);
            <%}%>

            

            <%
            break;
        case "日期区间框":
                                                        %>
         
            var time_zz1 = new Date($(xml).find('数据记录><%=FS_name_temp%>1').text()).Format_go("yyyy-MM-dd");
            var time_zz2 = new Date($(xml).find('数据记录><%=FS_name_temp%>2').text()).Format_go("yyyy-MM-dd");
            if (time_zz1 == "" || time_zz1 == null || time_zz1.indexOf("aN") >= 0) {
                time_zz1 = null;
            }
            if (time_zz2 == "" || time_zz2 == null || time_zz2.indexOf("aN") >= 0) {
                time_zz2 = null;
            }

            <% if (onlyshow)
        {%>  
            
            $("#fifsssss_<%=FS_name_temp%>").text('从 '+time_zz1 + ' 到 ' + time_zz2);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>1").datepicker('setDate', time_zz1);
            $("#<%=FS_name_temp%>2").datepicker('setDate', time_zz2);
            <%}%>


            
 

                <%
            break;
        case "大文本框":
                                                        %>
                 <% if (onlyshow)
        {%>  
            var sssss_zhi = $(xml).find('数据记录><%=FS_name_temp%>').text();
            $("#fifsssss_<%=FS_name_temp%>").html(sssss_zhi);
            <%}
        else
        { %>
            $("#<%=FS_name_temp%>").val($(xml).find('数据记录><%=FS_name_temp%>').text());
            <%}%>
                <%
            break;
        case "富文本框":
                                                        %>
                $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").html($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
            <%
            break;
        case "上传组件":
            %>
            //上传控件处理
            $("#Stupian_old").val($(xml).find('数据记录>Stupian').text());
            myDropzone.removeAllFiles();
            $(xml).find("图片记录>Ttupianpath").each(function (i) {
                var wenjian = $(this).text();
                if (wenjian != "") {
                    var mockFile = { name: wenjian, size: 0, file: wenjian };
                    myDropzone.emit("addedfile", mockFile);
                    myDropzone.emit("thumbnail", mockFile, wenjian);
                    myDropzone.emit("complete", mockFile);
                    myDropzone.emit("success", mockFile, wenjian);
                    myDropzone.files.push(mockFile);
                }

            });

            //  dropzone_only_for_showinfo
            <% if (onlyshow)
        {%>  
            var showh_str = "<ul class='ace-thumbnails clearfix'>";
            $(".dz-preview").each(function (i) {
                if ($(this).hasClass("dz-file-preview"))
                {
                    var t = "/mytutu/wjlxwfyl.jpg";
                    var t2 = $(this).find("img").eq(0).attr("src");
                    showh_str = showh_str + "<li><a href='" + t2 + "'  target='_blank'><img width='120' height='120' alt='120x120' src='" + t + "' /></a></li>"
                }
                if ($(this).hasClass("dz-image-preview"))
                {
                    
                    var t = $(this).find("img").eq(0).attr("src");
                    showh_str = showh_str + "<li><a href='" + t + "' data-rel='colorbox'><img width='120' height='120' alt='120x120' src='" + t + "' /></a></li>"
                }

            });
            showh_str = showh_str + "</ul>";
            $("#dropzone_only_for_showinfo").html(showh_str);

            var $overflow = '';
            var colorbox_params = {
                rel: 'colorbox',
                reposition: true,
                scalePhotos: true,
                scrolling: false,
                previous: '<i class="ace-icon fa fa-arrow-left"></i>',
                next: '<i class="ace-icon fa fa-arrow-right"></i>',
                close: '&times;',
                current: '{current} of {total}',
                maxWidth: '100%',
                maxHeight: '100%',
                onOpen: function () {
                    $overflow = document.body.style.overflow;
                    document.body.style.overflow = 'hidden';
                },
                onClosed: function () {
                    document.body.style.overflow = $overflow;
                },
                onComplete: function () {
                    $.colorbox.resize();
                }
            };
            $('.ace-thumbnails [data-rel="colorbox"]').colorbox(colorbox_params);
             <%}%>

            <%
            break;
        case "子表数据":
                                                        %>
            $("table[id^='grid-table-subtable-']").each(function () {

                var postData = $(this).jqGrid("getGridParam", "postData");
                $.extend(postData, { this_extforinfoFSID: $(this).attr('sub_this_extforinfoFSID') });
                $(this).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据
                $(this).attr("lastsel_yhb", "-999999");
                <% if (onlyshow)
        {%>  
                window.setInterval(function () { $(".ui-pg-table .navtable").hide(); }, 1000);
                
                <%}%>
            });
                <%
                break;
            case "xx2":

                break;

            default:

                break;
        }
                                            %>


                <%
        }
                                            %>


                   //其他加载操作
   


                //隐藏等待提示，显示必要区域
                $("#editloadinfo").addClass("hide");
                $(formid1).removeClass("hide");
            try {
                $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().tooltip('hide');
            }
            catch (e) {
            }

            }
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


            $.ajax({
                type: "POST",
                url: url1 + "?guid=" + randomnumber(),
                dataType: "xml",
                data: "ajaxrun=info&jkname=" + jkname_info1 + "&idforedit=" + dbid + "&zheshiyige_FID=" + $("#zheshiyige_FID").val(),
                success: callback_info, //请求成功
                error: errorForAjax//请求出错 
                //complete: complete//请求完成

            });


  
        }


        jQuery(function ($) {
            var isedit = getUrlParam("fff");
            var idforedit = getUrlParam("idforedit");
        
            if (isedit == "1") {
                $("#idforedit").val(idforedit);

                $(document).on('click', "#reloaddb", function () { loadinfoajax1($("#idforedit").val()); });

                //加载表单数据
                loadinfoajax1($("#idforedit").val());
            }
            else {
                $(document).on('click', "#reloaddb", function () {
                    //重新加载子表
                    $("table[id^='grid-table-subtable-']").each(function () {

                        var postData = $(this).jqGrid("getGridParam", "postData");
                        $.extend(postData, { this_extforinfoFSID: $(this).attr('sub_this_extforinfoFSID') });
                        $(this).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据
                        $(this).attr("lastsel_yhb", "-999999");
                    });
                });
            }



        });

    </script>




    <!-- **********ajax提交前验证******** -->
    <script type="text/javascript">
        //自定义表单验证方法
        $.validator.addMethod("lrunlv_zhengshu", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{0,0})?$/.test(value);
        }, "请输入整数。");
        $.validator.addMethod("lrunlv_1", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,1})?$/.test(value);
        }, "请精确至一位小数。");
        $.validator.addMethod("lrunlv_2", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,2})?$/.test(value);
        }, "请精确至两位小数。");
        $.validator.addMethod("lrunlv_3", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,3})?$/.test(value);
        }, "请精确至三位小数。");
        $.validator.addMethod("lrunlv_4", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,4})?$/.test(value);
        }, "请精确至四位小数。");
        $.validator.addMethod("lrunlv_5", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,5})?$/.test(value);
        }, "请精确至五位小数。");
        $.validator.addMethod("lrunlv_6", function (value, element) {
            return this.optional(element) || /^\d+(\.\d{1,6})?$/.test(value);
        }, "请精确至六位小数。");
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

                  <%=strfin[0]%>
                },

                messages: {
                   <%=strfin[1]%>
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
             <%=strfin[2]%>

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


            //第一个输入框获得焦点
 
            try {
                $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().tooltip('hide');
            }
            catch (e) {
            }

            //$(formid1).find("input[type='text'][readonly!='readonly']").eq(0).attr('title', temp_title_zz);
            //select2
            $('.select2').css('width', '220px').select2();

        

            if ($(".dropzone").length > 0) {
                //上传控件Dropzone
                try {
                    Dropzone.autoDiscover = false;
                    myDropzone = new Dropzone("#dropzone", {
                        paramName: "file1", // The name that will be used to transfer the file
                        maxFilesize: 500, // MB
                        maxFiles: 20,
                        url: '/ajaxdropzoneupload.aspx',
                        addRemoveLinks: true,
                        createImageThumbnails: true,
                        acceptedFiles: "image/*,.rar,.zip,.txt,.docx,.doc,.xls,.xlsx,.pdf,.vsdx,.vsd,.mp4",
                        //maxThumbnailFilesize: 10,
                        //thumbnailWidth: 1024,
                        //thumbnailHeight: 1024,
                        //accept: function (file, done) {
                        //    //alert(file.url);
                        //    return done(file);
                        //},
                        dictDefaultMessage:
                        '<i class="upload-icon ace-icon fa fa-cloud-upload blue"></i>点击或拖拽文件上传'
                  ,
                        dictResponseError: 'Error while uploading file!',

                        //change the previewTemplate to use Bootstrap progress bars
                        previewTemplate: "<div class=\"dz-preview dz-file-preview\">\n  <div class=\"dz-details\">\n    <div class=\"dz-filename\"><span data-dz-name></span></div>\n    <div class=\"dz-size\" data-dz-size></div>\n    <img data-dz-thumbnail />\n  </div>\n  <div class=\"progress progress-small progress-striped active\"><div class=\"progress-bar progress-bar-success\" data-dz-uploadprogress></div></div>\n  <div class=\"dz-success-mark\"><span></span></div>\n  <div class=\"dz-error-mark\"><span></span></div>\n  <div class=\"dz-error-message\"><span data-dz-errormessage></span></div>\n</div>",
                        init: function () {
                            this.on("success", function (file, servermsg) {
                                //把服务器返回的数据放入自定义表单
                                var hide_server_path = document.createElement("input");
                                hide_server_path.name = "allpath_" + this.options.paramName;
                                hide_server_path.type = "checkbox";
                                hide_server_path.value = servermsg;
                                hide_server_path.checked = "checked";
                                $(hide_server_path).hide();
                                file.previewTemplate.appendChild(hide_server_path);
                                
                          
                                var houzhui = servermsg.substring(servermsg.lastIndexOf("."), servermsg.lastIndexOf(".").length);//后缀名 
                          
                                if (houzhui != ".jpg" && houzhui != ".bmp" && houzhui != ".gif" && houzhui != ".png" && houzhui != ".jpeg")
                                {
                                    //$(file.previewTemplate).find("img").remove();
                                    file.previewElement.classList.remove("dz-image-preview");
                                    file.previewElement.classList.add("dz-file-preview");
                                }
                                else
                                {
                                    $(file.previewTemplate).find("img").attr('src', servermsg);
                                }
                                

                            
                                //调试用，显示服务返回的数据
                                //file.previewTemplate.appendChild(document.createTextNode(servermsg));
                            });

                        }
                        , sending: function (xhr, formData) {
                            if (!(window.File || window.FileReader || window.FileList || window.Blob)) {
                                alert('不支持的操作');
                            }
                            
                            //alert(readFile(document.querySelector("input[type=file]")));
                            
                            }
                    });

                } catch (e) {
                    alert(e);
                }
            }
          
         





        });



    </script>



    <!-- **********弹窗处理******** -->
    <script type="text/javascript">
        var grid_selector = "#grid-table";
        var pager_selector = "#grid-pager";

        function openeditdialog(e, kczd) {

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
                    var dialog_title = $("[for=" + open_now_ziduan + "]").text().replace("*", "").replace("：", "");
                    //e.preventDefault();

                    dialog_tanchuang = $("#dialog-message").removeClass('hide').dialog({
                        modal: true,
                        title: "<div class='widget-header widget-header-small'><h4 class='smaller'><i class='ace-icon fa fa-check'></i> 选择并引入--" + dialog_title + "</h4></div>",
                        width: ($(window).width() > 700 ? "70%" : "95%"),
                  
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
                    $.extend(postData, { this_extfor_teshuwhere: kczd.attr('teshuwhere') });
                    $(grid_selector).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据
                    //设置冻结列
                    $(grid_selector).jqGrid('setFrozenColumns');


                },
                error: function () {
                    kczd.removeAttr("disabled");
                    alert("加载列表配置失败！");
                }
            });




        };


        function c_dialog_yinru(kczd) {
            var open_now_ziduan = kczd.attr('id').replace("searchopenyhbspgogo_", "");

            var open_now_ziduan_byy = kczd.attr('title');

            var ids = $(grid_selector).jqGrid('getGridParam', 'selarrrow');

            //发现多选处理函数
            try
            {
                if (ids.length > 0) {
                    var str_00 = "";
                    for (var i = 0; i < ids.length; i++) {
                        var rowData = $(grid_selector).jqGrid("getRowData", ids[i]);
                        str_00 = str_00 + rowData[open_now_ziduan_byy] + ",";
                    }
                    eval("getsubandadd_DD_PL_" + open_now_ziduan + "('" + str_00.substring(0, str_00.length - 1) + "')");
                }
            }
            catch(e){
                //其他正常情况
                if (ids.length <= 1) {
                    if (ids.length == 1) {
                        var rowId = $(grid_selector).jqGrid("getGridParam", "selrow");
                        var rowData = $(grid_selector).jqGrid("getRowData", rowId);


                        if (open_now_ziduan_byy.indexOf(':') > 0) {
                            //有显示值
                            var open_now_ziduan_byy_arr = new Array();
                            open_now_ziduan_byy_arr = open_now_ziduan_byy.split(":");
                            var g00 = new Array();
                            g00 = open_now_ziduan_byy_arr[0].split(",");
                            var str_00 = "";
                            for (var i = 0; i < g00.length; i++) {
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
                        else {
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
            }
        
             
           

            dialog_tanchuang.dialog("close");

        };

        jQuery(function ($) {



            //弹窗
            $(".searchopenyhbspgogo").on('click', function (e) {
                openeditdialog(e, $(this));
            });

            //自定义搜索事件
            $(document).on('click', "#MybtnSearch", function () {
                var zdy = $('#mysearchtop').serialize()
                var postData = $(grid_selector).jqGrid("getGridParam", "postData");
                $.extend(postData, { mysearchtop: zdy });
                $(grid_selector).jqGrid("setGridParam", { search: true }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid
            });


            
            //resize to fit page size
            $(window).on('resize.jqGrid', function () {

                $(grid_selector).setGridWidth(($(window).width() > 700 ? $(window).width() * 0.7 - 30 : $(window).width() * 0.95 - 30));
                

                var youbianquyu_for_gw = $("#addadd").width();
                $("table[id^='grid-table-subtable-']").each(function () {
               
                    $(this).setGridWidth(youbianquyu_for_gw);
                });
                 
            });







        });



    </script>




    <!-- **********子表列表处理******** -->
    <script type="text/javascript">

 

        function init_subtable_jqgird(FSID) {

            var grid_selector_001 = "grid-table-subtable-" + FSID;
            var pager_selector_001 = "grid-pager-subtable-" + FSID;
           
            //resize to fit page size
            //$(window).on('resize.jqGrid', function () {

            //    $("#" + grid_selector_001).setGridWidth($("#" + grid_selector_001).parent().width() - 15);
               
            //});

         
  
            var aj = $.ajax({
                url: '/pucu/jqgirdjs_for_subtable.aspx?guid=' + FSID + "&grid_selector_ID=" + grid_selector_001 + "&pager_selector_ID=" + pager_selector_001,
                type: 'get',
                cache: false,
                dataType: 'html',
                success: function (data) {
                    //alert(data);
                    eval(data);
                    var postData = $("#" + grid_selector_001).jqGrid("getGridParam", "postData");
                    $.extend(postData, { this_extforinfoFSID: FSID });
                    $("#" + grid_selector_001).attr("sub_this_extforinfoFSID", FSID);
                    $("#" + grid_selector_001).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据

                

                },
                error: function () {
                    alert("加载子表配置失败！");
                }
            });

 


        }


 

        jQuery(function ($) {
       

            <%
        for (int i = 0; i < dsFPZ.Tables["表单配置子表"].Rows.Count; i++)
        {
            if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_type"].ToString() == "子表数据")
            {
                Response.Write("init_subtable_jqgird('" + dsFPZ.Tables["表单配置子表"].Rows[i]["FSID"].ToString() + "');" + Environment.NewLine);
            }

        }
        %>

   

        });



    </script>
    <!-- **********jqgrid相关处理******** -->
    <script type="text/javascript">






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



	<!-- 打印处理 -->
  <script>

      function beginPrint_go(dayinquyu)
      {

          //判定对应格式的打印页面是否存在，如果存在就使用指定页面打开。不存在调用默认打印。
          if (getUrlParam("printp") != null && getUrlParam("printp") != "") {
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
              beginPrint_go('PrintArea_F');
          
        });

        
    });

  </script>




<script type="text/javascript">
			jQuery(function($) {


	
 
	
	
	$(document).one('ajaxloadstart.page', function(e) {
		$('#colorbox, #cboxOverlay').remove();
   });
})
		</script>