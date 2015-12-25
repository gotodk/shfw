<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_script.ascx.cs" Inherits="pucu_wuc_script" %>

    <!-- 附加的body底部本页专属的自定义js脚本 -->
    <script src="/assets/js/jquery.validate.js"></script>
    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>

    <script src="/assets/js/dropzone.js"></script>

    <!-- page specific plugin scripts -->
    <script src="/assets/js/jqGrid/jquery.jqGrid.min.js"></script>
    <script src="/assets/js/jqGrid/i18n/grid.locale-cn.js"></script>

    <script src="/assets/js/jquery.hotkeys.js"></script>
    <script src="/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="/assets/js/ace/elements.colorpicker.js"></script>
    <script src="/assets/js/ace/elements.wysiwyg.js"></script>

    <script type="text/javascript" src="/assets/js/desforcsharp.js"></script>

    <script src="/assets/js/select2.js"></script>


    <script src="/assets/js/yhb_city.js"></script>



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
            if (r != null) return unescape(r[2]); return ""; //返回参数值
        }
   

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
         
                JSON_sub_str = JSON_sub_str = "&" + $(this).attr('id') + "_fcjsq=" + objDB.length + "&" + $(this).attr('id') + "=" + encMe(JSON.stringify(objDB), "mima");
             
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
                try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().tooltip('hide'); } catch (e) { }
                //显示提交结果
                bootbox.alert({
                    message: msg,
                    callback: function () {
                        setTimeout(function () {
                            try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().select().tooltip('hide'); } catch (e) { }
                        }, 1);
                    
                }  });

                var isedit = getUrlParam("fff");
                if (isedit == "1") {
                    //加载表单数据
                    loadinfoajax1($("#idforedit").val());
                }
               
                
                
           

                //最后跑这个
                setTimeout(function () {
                    //取消等待显示，开放禁用区域
                    gogoajax1_CanRun = true;
                    $(t_buttonid).removeClass("disabled");
                    $(t_buttonid).html("<i class='ace-icon fa fa-check bigger-110'></i>保存");
                    $(t_buttonid+"_top").removeClass("disabled");
                    $(t_buttonid + "_top").html("<i class='ace-icon fa fa-floppy-o bigger-120 blue'></i>保存");
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
            if (isedit == "1") {
           
                $("#fanhuishangyiye").removeClass("hidden");
            }
            else {
                $("#fanhuishangyiye").addClass("hidden");

            }
            //添加返回代码
            $(document).on('click', "#fanhuishangyiye_top", function () {
                history.back(-1);

            });
            $(document).on('click', "#fanhuishangyiye", function () {
                history.back(-1);

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
                                            %>
            <%
        switch (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_type"].ToString())
        {
            case "输入框":
                                            %>

            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());

            <%    break;
        case "密码框":
                                                        %>
            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
            <%
            break;

        case "下拉框":
                                                        %>
            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
            <%
            break;

        case "单选框":
                                                        %>
            $("input:radio[name='<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>'][value='" + $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text() + "']").prop("checked", true);
            <%
            break;

        case "普通多选框":
                                                        %>
            $("input[name='<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>']").prop("checked", false);
            $("input[name='<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>']:checkbox").each(function () {
                if (("," + $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text() + ",").indexOf("," + $(this).val() + ",") >= 0) {
                    $(this).prop("checked", true);
                }

            });
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
                <%


            break;

        case "整数":
                                                        %>
                $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
                <%
            break;
        case "两位小数":
                                                         %>
                $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
                <%
            break;
        case "日期框":
                                                        %>
            <%--$("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());--%>
            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").datepicker('setDate', $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());

            <%
            break;
        case "日期区间框":
                                                        %>

            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>1").datepicker('setDate', $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>1').text());
            $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>2").datepicker('setDate', $(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>2').text());
  <%--              $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>1").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>1').text());
                $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>2").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>2').text());--%>

                <%
            break;
        case "大文本框":
                                                        %>
                $("#<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>").val($(xml).find('数据记录><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString()%>').text());
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



            <%
            break;
        case "子表数据":
                                                        %>
            $("table[id^='grid-table-subtable-']").each(function () {

                var postData = $(this).jqGrid("getGridParam", "postData");
                $.extend(postData, { this_extforinfoFSID: $(this).attr('sub_this_extforinfoFSID') });
                $(this).jqGrid("setGridParam", { search: true, datatype: 'xml' }).trigger("reloadGrid", [{ page: 1 }]);  //重载JQGrid数据
                $(this).attr("lastsel_yhb", "-999999");
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
                        maxFilesize: 3, // MB
                        maxFiles: 20,
                        url: '/ajaxdropzoneupload.aspx',
                        addRemoveLinks: true,
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
                                $(file.previewTemplate).find("img").attr('src', servermsg);
                                //调试用，显示服务返回的数据
                                //file.previewTemplate.appendChild(document.createTextNode(servermsg));
                            });

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




        };


        function c_dialog_yinru(kczd) {
            var open_now_ziduan = kczd.attr('id').replace("searchopenyhbspgogo_", "");

            var open_now_ziduan_byy = kczd.attr('title');

            var ids = $(grid_selector).jqGrid('getGridParam', 'selarrrow');
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

                $(grid_selector).setGridWidth($(window).width() * 0.78);
 
            });







        });



    </script>




    <!-- **********子表列表处理******** -->
    <script type="text/javascript">

 

        function init_subtable_jqgird(FSID) {

            var grid_selector_001 = "grid-table-subtable-" + FSID;
            var pager_selector_001 = "grid-pager-subtable-" + FSID;

            //resize to fit page size
            $(window).on('resize.jqGrid', function () {

                $("#" + grid_selector_001).setGridWidth($("#" + grid_selector_001).parent().width() -30);
               
            });

         
  
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