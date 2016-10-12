<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="qd_guiji_s.aspx.cs" Inherits="qiandao_qd_guiji_s" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />
 <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
        <script type="text/javascript" src="/adminht/corepage/qiandao/qd_rili_ajax.aspx?zhiling=kaoqinfugaidian&<%=cscscs %>"></script> 
<form class="form-inline well well-sm " id="mysearchtop">
            
                      <label>时间：</label>

                <div class="input-daterange input-group">
                    <input class="form-control date-picker" id="Ktime1" name="Ktime1" type="text">
                    <span class="input-group-addon">
                        <i class="fa fa-exchange"></i>
                    </span>
                    <input class="form-control date-picker" id="Ktime2" name="Ktime2" type="text">
                </div>

    <label>姓名：</label>
                <input class="form-control search-query" type="text" id="xingming" name="xingming">

    <br/><br/>
    
    <select id="suoshuquyu_show" name="suoshuquyu_show">
 
        <option value="售后管理部" selected="">售后管理部</option>
        <option value="华北大区">华北大区</option>
        <option value="华东大区">华东大区</option>
        <option value="华南大区">华南大区</option>
        <option value="华中大区">华中大区</option>
        <option value="西南大区">西南大区</option>
                                                    </select>

       
    <select id="guolshuju" name="guolshuju">
                                  
                                                         <option value="z" selected="">每个人的最后一次签到</option> <option value="a">全部数据</option>
                                                    </select>
                
       
                          <button type="button" class="btn btn-purple btn-sm" id="MybtnSearch">
                    <i class="ace-icon fa fa-search bigger-110"></i>搜索
                </button>
               </form>

    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">
   
       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />


        <script type="text/javascript">

           

             jQuery(function ($) {
                 if (getUrlParam("fff") == "1") {

                      
                 }
                 function getUrlParam_teshu(str,name) {
                     var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
                     var r = str.match(reg);  //匹配目标参数
                     if (r != null) return r[2]; return ""; //返回参数值
                 }

                 var recs = uncMe(getUrlParam("cscscs"), "mima");
                 var ktime1 = decodeURI(getUrlParam_teshu(recs, "Ktime1"));
                 var ktime2 = decodeURI(getUrlParam_teshu(recs, "Ktime2"));

                 var xingming = decodeURI(getUrlParam_teshu(recs, "xingming"));
                 var suoshuquyu_show = decodeURI(getUrlParam_teshu(recs, "suoshuquyu_show"));
                 var guolshuju = decodeURI(getUrlParam_teshu(recs, "guolshuju"));

                 var time_zz1 = new Date(ktime1).Format_go("yyyy-MM-dd");
                 var time_zz2 = new Date(ktime2).Format_go("yyyy-MM-dd");
            if (time_zz1 == "" || time_zz1 == null || time_zz1.indexOf("aN") >= 0) {
                time_zz1 = null;
            }
            if (time_zz2 == "" || time_zz2 == null || time_zz2.indexOf("aN") >= 0) {
                time_zz2 = null;
            }
            $("#Ktime1").datepicker('setDate', time_zz1);
            $("#Ktime2").datepicker('setDate', time_zz2);
            $("#xingming").val(xingming);
            if (suoshuquyu_show != "")
            { $("#suoshuquyu_show").val(suoshuquyu_show); }
            if (guolshuju != "")
            { $("#guolshuju").val(guolshuju); }
        

                 $("#title_f_id").html("签到轨迹覆盖图");
                 $("#addbutton1_top").hide();
                 $("#addbutton1").css({ visibility: "hidden" });
                 $("#reloaddb").css({ visibility: "hidden" });
                 $(".no-padding-right").hide();
                 

                 $(document).on('click', "#MybtnSearch", function () {
                  
                     window.top.location.href = "/adminht/corepage/qiandao/qd_guiji_s.aspx?cscscs=" + encMe($("#mysearchtop").serialize(),"mima");

                 });

          
        });
        </script>

</asp:Content>

