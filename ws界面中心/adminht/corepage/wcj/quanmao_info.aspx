<%@ Page Title="" Language="C#" MasterPageFile="~/adminht/MasterPageMain.master" AutoEventWireup="true" CodeFile="quanmao_info.aspx.cs" Inherits="quanmao_info" %>




<asp:Content ID="Content4" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
 
     
    

    <div class="row">
        <div class="col-xs-0 col-sm-1 hidden-xs"></div>
        <div class="col-xs-12 col-sm-10">

            
             <form runat="server" id="form1" visible="true">

                    <div class="widget-box" id="quyu_zhao" runat="server">
											<div class="widget-header widget-header-small">
												<h5 class="widget-title lighter">搜索客户</h5>
											</div>

											<div class="widget-body">
												<div class="widget-main">
													<div class="form-search">
														<div class="row">
															<div class="col-xs-12 col-sm-6">
																<div class="input-group">
																	<span class="input-group-addon">
																		<i class="ace-icon fa fa-edit"></i>
																	</span>

																	 
                                                                    <asp:TextBox ID="idorname" runat="server" data-rel="tooltip" placeholder="输入客户名称或编号" title="模糊搜索" CssClass="form-control search-query"></asp:TextBox> 
																	<span class="input-group-btn">

                                                                       <span class="ace-icon fa fa-search icon-on-right bigger-110"></span> <asp:Button ID="kaishizhao" runat="server" CssClass="btn btn-purple btn-sm"  Text="搜索" OnClick="kaishizhao_Click"  />
																		
																	</span>
																</div>
															</div>
                                                            <div class="col-xs-12 col-sm-12">

<div id="errmsg" class="red" runat="server">搜索结果：</div>
                                                                 
                                                    <asp:ListBox ID="ssjg" runat="server" Rows="1" SelectionMode="Single" class="col-xs-12 col-sm-12" AutoPostBack="True" OnSelectedIndexChanged="ssjg_SelectedIndexChanged"  >
                                                        <asp:ListItem Selected="True">选择搜索到的客户</asp:ListItem>
                                                </asp:ListBox>
                                                            </div>
														</div>
													</div>

                                                    
												</div>
											</div>
										</div>






       <asp:Repeater ID="showinfor" runat="server"> 
<HeaderTemplate><!--头--> 
          
</HeaderTemplate> 
<ItemTemplate><!--中间循环部分--> 

             <div class="widget-box">
            <div class="widget-header widget-header-flat">
                <h4 class="widget-title smaller">客户全貌(<%#Eval("uucjlx") %>)</h4>

                <div class="widget-toolbar">
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main" style="word-wrap: break-word; word-break: normal;">

                    <dl>
                        <dt>客户名称/编号：</dt>
                        <dd><%#Eval("YYname") %> / <%#Eval("YYID_uuuu") %></dd>
                    

                        <dt>省市区：</dt>
                        <dd><%#Eval("Promary_str") %>，<%#Eval("City_str") %>，<%#Eval("Qu_str") %></dd>
                        <dt>地址：</dt>
                        <dd><%#Eval("YYdizhi") %></dd>
                        <dt>电话传真：</dt>
                        <dd><%#Eval("YYdianhua") %> / <%#Eval("YYchuanzhen") %></dd>
                        <dt>开票信息：</dt>
                        <dd><%#Eval("YYkaipiao") %></dd>
                        <dt>创建日期：</dt>
                        <dd><%#Eval("YYaddtime") %></dd>
                        <dt>所属部门/负责人：</dt>
                        <dd><%#Eval("YYssbumen_name") %> / <%#Eval("YYfuwufuzeren_name") %></dd>
                        <dt>所有联系人：</dt>
                        <dd><%#Eval("lianxirenstr") %></dd>
                    </dl>






                </div>
            </div>
        </div>


    </ItemTemplate> 
<FooterTemplate><!--尾--> 
     
</FooterTemplate> 
</asp:Repeater> 






            <div class="form-inline well well-sm " >
            
                      <label>情报日期：</label>

                <div class="input-daterange input-group">
                
                     <asp:TextBox ID="Ktime1" runat="server"  class="form-control date-picker" ></asp:TextBox> 
                    <span class="input-group-addon">
                        <i class="fa fa-exchange"></i>
                    </span>
                    <asp:TextBox ID="Ktime2" runat="server"  class="form-control date-picker" ></asp:TextBox> 
                    
                </div>
             
                

                   <asp:Button ID="bbguolv" runat="server" CssClass="btn btn-default btn-sm"  Text="过滤" OnClick="bbguolv_Click"  />
																		
																 
 
             

        <div class="widget-box">
            <div class="widget-header widget-header-flat">
                <h4 class="widget-title smaller">最近情报</h4>

                <div class="widget-toolbar">
                </div>
            </div>

            <div class="widget-body">
                <div class="widget-main" style="word-wrap: break-word; word-break: normal;">

                 
       <asp:Repeater ID="Rqingbao" runat="server"> 
<HeaderTemplate><!--头--> 
          
</HeaderTemplate> 
<ItemTemplate><!--中间循环部分--> 
                       <dt>情报日期/创建人：</dt>
                        <dd><%#Eval("QBtime") %> / <%#Eval("xingming") %></dd>
                        
                        <dt>情报来源：</dt>
                        <dd><%#Eval("QBlaiyuan") %></dd>
                        <dt>情报设备/品牌：</dt>
                        <dd><%#Eval("QBsb") %> / <%#Eval("QBpinpai") %></dd>

                        <dt>情报描述：</dt>
                        <dd><%#Eval("QBmiaoshu") %></dd>
                        <dt>年采血量：</dt>
                        <dd><%#Eval("QBcxl") %></dd>
                        <dt>采购负责人：</dt>
                        <dd><%#Eval("QBcgfzr") %></dd>
                        <dt>备用资料1/2/3：</dt>
                        <dd><%#Eval("QBbaka") %>/<%#Eval("QBbakb") %>/<%#Eval("QBbakc") %></dd>
                    </dl>
                    <hr />

                   
  </ItemTemplate> 
<FooterTemplate><!--尾--> 
     
</FooterTemplate> 
</asp:Repeater> 


                </div>
            </div>
        </div>

   </form>
        </div>
        <div class="col-xs-0 col-sm-1 hidden-xs"></div>

       












    </div>
    <!-- /.row -->

              
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="sp_script" runat="Server">
    <!-- 附加的body底部本页专属的自定义js脚本 -->

    <script src="/assets/js/date-time/bootstrap-datepicker.js"></script>
    <script src="/assets/js/jquery.inputlimiter.1.3.1.js"></script>
    <script src="/assets/js/jquery.maskedinput.js"></script>

     <!-- 默认查询当天的数据 -->
    <script type="text/javascript">
             jQuery(function ($) {
                 //
                 //datepicker plugin初始化
                 $('.date-picker').datepicker({ autoclose: true, })
                 $('.date-picker').mask('9999-99-99');                 //var now = new Date();
                 //now.setDate(now.getDate() - 30);
                 //$("#sp_pagecontent_Ktime1").datepicker('setDate', now);;
                 //$("#sp_pagecontent_Ktime2").datepicker('setDate', new Date());;
 
        });

        </script>
</asp:Content>




 

