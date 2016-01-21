<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="edit_wp.aspx.cs" Inherits="bas_edit_wp" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>




<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />


    <link rel="stylesheet" type="text/css" href="/assets/GooFlow/codebase/GooFlow2.css"/>
    <link rel="stylesheet" type="text/css" href="/assets/GooFlow/default.css"/>



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


    <script type="text/javascript" src="/assets/GooFlow/data1.js"></script>
 
<script type="text/javascript" src="/assets/GooFlow/GooFunc.js"></script>
<script type="text/javascript" src="/assets/GooFlow/json2.js"></script>

<script type="text/javascript" src="/assets/GooFlow/codebase/GooFlow.js"></script>
<script type="text/javascript">
var property={
	width:500,
	height: 600,
	toolBtns: ["start mix", "end round", "task round", "node", "chat", "state", "plug", "join", "fork", "complex"],
	//toolBtns:["start round","end round","task round","node","chat","state","plug","join","fork","complex mix"],
	haveHead: true,
    //headBtns:["new","open","save","undo","redo","reload"],
	headBtns:["undo","redo"],//如果haveHead=true，则定义HEAD区的按钮
	haveTool:true,
	haveGroup:true,
	useOperStack:true
};
var remark={
	cursor:"选择指针",
	direct:"结点连线",
	start:"入口结点",
	"end":"结束结点",
	"task":"任务结点",
	node:"自动结点",
	chat:"决策结点",
	state:"状态结点",
	plug:"附加插件",
	fork:"分支结点",
	"join":"联合结点",
	group:"组织划分框编辑开关"
};
var workprocess_area;
$(document).ready(function(){
    workprocess_area = $.createGooFlow($("#workprocess_area"), property);
    workprocess_area.setNodeRemarks(remark);
	//demo.onItemDel=function(id,type){
	//	return confirm("确定要删除该单元吗?");
	//}
    workprocess_area.loadData(jsondata);

    workprocess_area.reinitSize($("#workprocess_area").parent().width() - 30, 600);
	$(window).on('resize.jqGrid', function () {

	    workprocess_area.reinitSize($("#workprocess_area").parent().width() - 30, 600);

	});
});
 
function Export(){
    //JSON.stringify(demo.exportData())
}
</script>


</asp:Content>

