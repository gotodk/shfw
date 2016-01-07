<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="demo_tiaomasoamiao.aspx.cs" Inherits="demo_tiaomasoamiao" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">

       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />

    <!-- 处理自动提交扫描结果并定时关闭提示框 -->
    <script type="text/javascript">
      
        var getsubandadd_DD_tiaoma;
        var tiaomainput = $("#tiaoma");
        var grid_selector_ID = "#grid-table-subtable-160107000205";
        var _ajaxpage = "/adminht/linshi/demo_tiaomasoamiao_ajax.aspx";
        var tmlen = 12;

        //弹窗多选处理
        function getsubandadd_DD_PL_tiaoma(s_zhi)
        {
            var s_zhi_arr = s_zhi.split(",");
 
            for (var i = 0; i < s_zhi_arr.length; i++) {
                tiaomainput.val(s_zhi_arr[i]);
                getsubandadd_DD_tiaoma(s_zhi_arr[i]);
                tiaomainput.val('');
            }
        }
        jQuery(function ($) {

            
        
            tiaomainput.attr("maxlength", tmlen);
            tiaomainput.bind("input propertychange", function () {
                var isedit = getUrlParam("fff");
                if ($.trim($(this).val()).length == tmlen)
                {
                    if (isedit == "1") {
                        ;
                    }
                    else {
                        //调用ajax加载数据
                        getsubandadd_DD_tiaoma(tiaomainput.val());
                        try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().select().tooltip('hide'); } catch (e) { }
                    }
                }
            });

      


            //开始获取条码数据放入子表中
            getsubandadd_DD_tiaoma = function getsubandadd(tiaoma)
            {
                function callback(msg) {
                    try { $(formid1).find("input[type='text'][readonly!='readonly']").eq(0).focus().tooltip('hide'); } catch (e) { }
                    //显示提交结果
                    //alert(msg);
                    var newrowid = tiaoma;
                 
                    //判定newrowid是否已存在
                    var obj_RD = $(grid_selector_ID).jqGrid("getRowData");
                   
                    for(var i=1;i <= obj_RD.length;i++)
                    {
                        var ret_this_r = $(grid_selector_ID).jqGrid("getRowData",'jqg'+i);
                        if (ret_this_r.条码 == newrowid)
                        {
                            $("#onlyshowmsgTT").val("跳过，条码已在列表中存在！");
                            return false;
                        }
                    }

                  



                    //var mydata = [{ "开单条码": newrowid, 开单时间: "2007-10-01 10:10:10", 用户卡号: "test", 是否完件: "note", 是否发货: "200.00", 用户名称: "10.00", 业务名称: "210.00" } ];
                    if (msg.indexOf('错误err') == 0) {
                        $("#onlyshowmsgTT").val(msg);
                        return false;
                    }
                   
                    var mydata = $.parseJSON(msg);
                  

                    $(grid_selector_ID).jqGrid("addRowData", newrowid, mydata, "first");

            

                    $("#onlyshowmsgTT").val("成功获取条码信息");

                };
                $.ajax({
                    type: "POST",
                    url: _ajaxpage+"?guid=" + randomnumber(),
                    dataType: "html",
                    data: $(formid1).serialize(),
                    success: callback, //请求成功
                    error: errorForAjax//请求出错 

                });
            }
            
        });
        </script>

</asp:Content>

