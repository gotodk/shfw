<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_content.ascx.cs" Inherits="pucu_wuc_content" %>


<div id="dialog-message-forsubtable-addedit" class="hide">

    <div class="row">

        <div class="col-sm-12">
            <iframe src="about:blank" style="width: 100%; height: 500px; border: 0px; overflow-x: hidden;" id="iframforsubtab"></iframe>
        </div>
    </div>


</div>
<!-- #dialog-message-forsubtable-addedit -->

<div id="dialog-message" class="hide">

    <div class="row">
        <%--    <div class="col-sm-12">
        <form class="form-inline well well-sm" id="mysearchtop">
             
                <label>创建时间：</label>

                <input class="form-control search-query" type="text" id="Sname" name="Sname" />

                <button type="button" class="btn btn-purple btn-sm" id="MybtnSearch">
                    <i class="ace-icon fa fa-search bigger-110"></i>搜索
                </button>

            </form>

    </div>--%>
        <div class="col-sm-12 PrintArea_D"   id="zheshiliebiaoquyu">

            <%--            <table id="grid-table"></table>

            <div id="grid-pager"></div>--%>
        </div>
    </div>


</div>
<!-- #dialog-message -->


 <%
     string css_str = "";
     bool onlyshow = false; //仅显示的标记
     //发现预览标志 
     if (Request["showinfo"] != null && Request["fff"] != null && Request["fff"].ToString() == "1")
     {
         css_str="hidden";
         onlyshow = true;
     }%>


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
                                <%if (onlyshow) { htPP["title_f"] = htPP["title_f"].ToString().Replace("修改--","查看--").Replace("维护","").Replace("编辑","").Replace("修改",""); } %>
                                <span id="title_f_id"><% =htPP["title_f"].ToString()%></span>
                            </a>

                        </li>
                        <li class="">&nbsp;&nbsp;</li>
                        <li class="  <%=css_str %>">
                            <button class="btn btn-white btn-info btn-bold" id="addbutton1_top">
                                <i class="ace-icon fa fa-floppy-o bigger-120 blue"></i>
                                保存
                            </button>
                        </li>
                        <li class="<%=css_str %>">&nbsp;&nbsp;</li>
                        <li class="hidden-480">
                            <button class="btn btn-white btn-info btn-bold printarea_go_dayinanniu" id="dayinzheyiye_top">
                                <i class="ace-icon fa fa-print grey"></i>
                                打印
                            </button>
                        </li>
                        <li class="">&nbsp;&nbsp;</li>
                        <li class="c_fanhuishangyiye_top hidden">
                            <button class="btn btn-white btn-info btn-bold" id="fanhuishangyiye_top">
                                <i class="ace-icon fa fa-undo red2"></i>
                                返回
                            </button>
                        </li>
                        <li class="c_fanhuishangyiye_top hidden">&nbsp;&nbsp;</li>

                        <li class="c_xinzeng_top hidden">
                            <button class="btn btn-white btn-info btn-bold" id="xinzeng_top">
                                <i class="ace-icon fa fa-plus-circle purple"></i>
                                新增
                            </button>
                        </li>
                        <li class="c_xinzeng_top hidden">&nbsp;&nbsp;</li>


                        <li class="c_bianji_top hidden">
                            <button class="btn btn-white btn-info btn-bold" id="bianji_top">
                                <i class="ace-icon fa fa-pencil-square-o green"></i>
                                编辑
                            </button>
                        </li>
                        <li class="c_bianji_top hidden">&nbsp;&nbsp;</li>

                      
                    </ul>




                    <div class="tab-content">
                        <div id="addadd" class="tab-pane fade in active">

                            <div class="row">
                                <div class="col-sm-12">


                                    <%
                                        if (htPP["form_hide"].ToString() == "hide")
                                        {
                                            Response.Write("<i class=\"ace-icon fa fa-spinner fa-spin orange bigger-300\" id=\"editloadinfo\"></i>");
                                        }
                                    %>

                                    <form class="PrintArea_F  form-horizontal <%=htPP["form_hide"].ToString() %>" role="form" id="myform1">
                                        <!-- #section:elements.form -->
                                        <input type="hidden" name="zheshiyige_FID" id="zheshiyige_FID" value="<%=dsFPZ.Tables["表单配置主表"].Rows[0]["FID"] %>">
                                        <input type="hidden" name="idforedit" id="idforedit" value="">
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
                                        <div class="form-group " >
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">

                                                <%
                                                    string inputclass_temp = "";
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_D_haveD"].ToString() == "1")
                                                    {
                                                        inputclass_temp = "form-control search-query";
                                                %>
                                                <div class="input-group col-xs-12 col-sm-5">
                                                    <%}
                                                        else
                                                        { inputclass_temp = "col-xs-12 col-sm-5"; } %>
                                                    <input data-rel="tooltip" type="text" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" placeholder="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_n"] %>" title="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_w"] %>" data-placement="bottom" class="<%=inputclass_temp %>" <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_SPPZ_readonly"] %> value="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"] %>" />

                                                    <%
                                                        if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_D_haveD"].ToString() == "1")
                                                        {
                                                    %>


                                                    <span class="input-group-btn">
                                                        <button class=" btn  btn-sm  searchopenyhbspgogo" type="button" id="searchopenyhbspgogo_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" title="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_D_yinruzhi"] %>" guid="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FSID"] %>" teshuwhere="">
                                                            <span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
                                                        </button>
                                                    </span>
                                                </div>

                                                <div class=" col-sm-12 no-padding-left" id="show_searchopenyhbspgogo_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>"></div>
                                                <%} %>

                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>

                                        </div>
                                        <%    break;
                                            case "密码框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10  <%=css_str %>">
                                                <input data-rel="tooltip" type="password" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" placeholder="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_n"] %>" title="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_w"] %>" data-placement="bottom" class="col-xs-12 col-sm-5" value="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"] %>" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>

                                        </div>
                                        <%
                                                break;

                                            case "下拉框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <select class="col-xs-12 col-sm-5" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                    <option value="" selected>请选择</option>
                                                    <%

                                                        for (int p = 0; p < ARR_list_static.Length; p++)
                                                        {
                                                            if (ARR_list_static[p].Trim() != "")
                                                            {
                                                                if (ARR_list_static[p].IndexOf('|') >= 0)
                                                                {
                                                                    string mrz = "";
                                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"].ToString() == ARR_list_static[p].Split('|')[1].Trim())
                                                                    { mrz = " selected "; }
                                                                    Response.Write(" <option value='" + ARR_list_static[p].Split('|')[1].Trim() + "' " + mrz + ">" + ARR_list_static[p].Split('|')[0].Trim() + "</option>");
                                                                }
                                                                else
                                                                {
                                                                    string mrz = "";
                                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"].ToString() == ARR_list_static[p].Trim())
                                                                    { mrz = " selected "; }
                                                                    Response.Write(" <option value='" + ARR_list_static[p].Trim() + "' " + mrz + ">" + ARR_list_static[p].Trim() + "</option>");
                                                                }
                                                            }


                                                        }
                                                    %>
                                                </select>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;

                                            case "单选框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">

                                                <div class="no-padding-left radio col-xs-12 col-sm-10">


                                                    <%

                                                        for (int p = 0; p < ARR_list_static.Length; p++)
                                                        {
                                                            if (ARR_list_static[p].Trim() != "")
                                                            {
                                                                if (ARR_list_static[p].IndexOf('|') >= 0)
                                                                {
                                                                    string mrz = "";
                                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"].ToString() == ARR_list_static[p].Split('|')[1].Trim())
                                                                    { mrz = "  checked  "; }
                                                                    Response.Write("<label><input name='" + dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString() + "' type='radio' value='" + ARR_list_static[p].Split('|')[1].Trim() + "' " + mrz + " class='ace'/><span class='lbl'>" + ARR_list_static[p].Split('|')[0].Trim() + "</span></label>");
                                                                }
                                                                else
                                                                {
                                                                    string mrz = "";
                                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"].ToString() == ARR_list_static[p].Trim())
                                                                    { mrz = " checked "; }
                                                                    Response.Write("<label><input name='" + dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString() + "' type='radio' value='" + ARR_list_static[p].Trim() + "' " + mrz + " class='ace'/><span class='lbl'>" + ARR_list_static[p].Trim() + "</span></label>");
                                                                }
                                                            }


                                                        }
                                                    %>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>

                                        </div>
                                        <%
                                                break;

                                            case "普通多选框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">

                                                <div class="no-padding-left checkbox col-xs-12 col-sm-10">



                                                    <%
                                                        string fenzu_old = "";
                                                        Regex reg = new Regex(@"\[(\S*)\]");
                                                        for (int p = 0; p < ARR_list_static.Length; p++)
                                                        {
                                                            if (ARR_list_static[p].Trim() != "")
                                                            {
                                                                if (ARR_list_static[p].IndexOf('|') >= 0)
                                                                {
                                                                    string fenzu_new = ARR_list_static[p].Split('|')[0].Trim();

                                                                    MatchCollection mc_new = reg.Matches(fenzu_new);
                                                                    MatchCollection mc_old = reg.Matches(fenzu_old);

                                                                    if (mc_new.Count > 0 && mc_old.Count > 0 && mc_new[0].Value != mc_old[0].Value)
                                                                    {
                                                                        Response.Write("<hr/>");
                                                                    }


                                                                    Response.Write("<label><input name='" + dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString() + "' type='checkbox' value='" + ARR_list_static[p].Split('|')[1].Trim() + "' class='ace'/><span class='lbl'>" + ARR_list_static[p].Split('|')[0].Trim() + "</span></label>");




                                                                    fenzu_old = ARR_list_static[p].Split('|')[0].Trim();
                                                                }
                                                                else
                                                                {
                                                                    string fenzu_new = ARR_list_static[p].Trim();

                                                                    MatchCollection mc_new = reg.Matches(fenzu_new);
                                                                    MatchCollection mc_old = reg.Matches(fenzu_old);

                                                                    if (mc_new.Count > 0 && mc_old.Count > 0 && mc_new[0].Value != mc_old[0].Value)
                                                                    {
                                                                        Response.Write("<hr/>");
                                                                    }

                                                                    Response.Write("<label><input name='" + dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString() + "' type='checkbox' value='" + ARR_list_static[p].Trim() + "' class='ace'/><span class='lbl'>" + ARR_list_static[p].Trim() + "</span></label>");



                                                                    fenzu_old = ARR_list_static[p].Trim();
                                                                }
                                                            }


                                                        }
                                                    %>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;

                                            case "下拉多选框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10">
                                                <select multiple="" data-placeholder="请选择…" class=" select2 tag-input-style" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">

                                                    <%

                                                        for (int p = 0; p < ARR_list_static.Length; p++)
                                                        {
                                                            if (ARR_list_static[p].Trim() != "")
                                                            {
                                                                if (ARR_list_static[p].IndexOf('|') >= 0)
                                                                {
                                                                    Response.Write(" <option value='" + ARR_list_static[p].Split('|')[1].Trim() + "'>" + ARR_list_static[p].Split('|')[0].Trim() + "</option>");
                                                                }
                                                                else
                                                                {
                                                                    Response.Write(" <option value='" + ARR_list_static[p] + "'>" + ARR_list_static[p].Trim() + "</option>");
                                                                }
                                                            }


                                                        }
                                                    %>
                                                </select>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                             
                                        </div>
                                        <%
                                                break;

                                            case "省市区联动":

                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <div class="yhb_city">
                                                    <select class="yhb_city_p" name="yhb_city_Promary_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" id="yhb_city_Promary_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                        <option value="0" selected>请选择省份</option>

                                                    </select>
                                                    <select class="yhb_city_c" name="yhb_city_City_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" id="yhb_city_City_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                        <option value="0" selected>请选择城市</option>
                                                    </select>
                                                    <select class="yhb_city_q" name="yhb_city_Qu_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" id="yhb_city_Qu_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                        <option value="0" selected>请选择区县</option>
                                                    </select>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%


                                                break;

                                            case "整数":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <input type="text" class="input-mini" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" value="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"] %>" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;
                                            case "两位小数":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <input type="text" class="input-mini" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" value="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"] %>" />
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;
                                            case "日期框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <div class="input-group col-xs-12 col-sm-5">
                                                    <input class="form-control date-picker" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" type="text" style="ime-mode: disabled" />
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-calendar bigger-110"></i>
                                                    </span>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                           <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;
                                            case "日期区间框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <div class="input-daterange input-group col-xs-12 col-sm-5">
                                                    <input class="form-control date-picker" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1" type="text" style="ime-mode: disabled" />
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-exchange"></i>
                                                    </span>
                                                    <input class="form-control date-picker" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2" type="text" style="ime-mode: disabled" />





                                                    <div class=" input-daterange input-group-btn btn-group zheshiyigekuiajieshezhi">
                                                        <button data-toggle="dropdown" class="btn badge dropdown-toggle">
                                                            快捷
													<span class="ace-icon fa fa-caret-down icon-on-right"></span>
                                                        </button>

                                                        <ul class="dropdown-menu dropdown-default">
                                                            <li>
                                                                <a href="javascript:void(0);" onclick="js_method_kj('<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1','<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2','本周')">本周</a>
                                                            </li>

                                                            <li>
                                                                <a href="javascript:void(0);" onclick="js_method_kj('<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1','<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2','本月')">本月</a>
                                                            </li>

                                                            <li>
                                                                <a href="javascript:void(0);" onclick="js_method_kj('<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1','<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2','本季度')">本季度</a>
                                                            </li>
                                                            <li>
                                                                <a href="javascript:void(0);" onclick="js_method_kj('<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>1','<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>2','本年')">本年</a>
                                                            </li>

                                                        </ul>
                                                    </div>










                                                </div>



                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>

                                        </div>
                                        <%
                                                break;
                                            case "大文本框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <textarea placeholder="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_n"] %>" class="limited col-xs-12 col-sm-5" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>" maxlength="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_maxlength"] %>" rows="5"><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_defaultvalue"] %></textarea>
                                                <span class="help-button hidden-480" data-rel="popover" data-trigger="hover" data-placement="left" data-content="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_tip_w"] %>" title="录入要求">?</span>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                            <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-7 control-label" style="text-align: left;font-size:14px;border:0px;border-bottom:1px solid #f1f1f1;min-height:30px;color:#000000;" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">--</div>

                                            </div>
                                            <%} %>
                                        </div>
                                        <%
                                                break;
                                            case "分组线":
                                        %>
                                        <hr id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"].ToString() %>" />
                                        <%
                                                break;
                                            case "富文本框":
                                        %>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label no-padding-right" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-10 <%=css_str %>">
                                                <div class="wysiwyg-editor" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>"></div>
                                                <input name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>_html" type="hidden" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>_html">
                                                <input name="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>_text" type="hidden" id="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>_text">
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>

                                             <% if(onlyshow) {%>  
                                            <div class="col-sm-10">
                                                <div class="col-xs-12 col-sm-12" id="fifsssss_<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>"></div>

                                            </div>
                                            <%} %>

                                        </div>
                                        <%
                                                break;
                                            case "上传组件":
                                        %>
                                        <div class="form-group">
                                            <%
                                                string chayueURL = "";
                                                if (Request["idforedit"] != null)
                                                {
                                                    chayueURL = dsFPZ.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"].ToString().Replace("[[编号]]",Request["idforedit"].ToString());
                                                    chayueURL = "<br/><a href='"+chayueURL+"' target='_blank'>查阅文件</a>";
                                                }
                                                 %>
                                            <label class="col-sm-2 control-label no-padding-right" for="beizhu"><%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：<%=chayueURL %></label>

                                            <div class="col-sm-10">
                                                <div class="dropzone col-xs-12 col-sm-12 <%=css_str %>" id="dropzone">

                                                    <div class="fallback">
                                                        <input type="file" />
                                                    </div>

                                                </div>
                                                <div class=" col-xs-12 col-sm-12" id="dropzone_only_for_showinfo">
 
                                                </div>
                                            </div>
                                            <%--隐藏这个图片集合原始guid--%>
                                            <input class="hide" id="Stupian_old" name="Stupian_old" />
                                            <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                        </div>
                                        <%
                                                break;
                                            case "子表数据":
                                        %>

                                        <div class="form-group">
                                            <label class="col-sm-12 hidden" for="<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_name"] %>">
                                                <%
                                                    if (dsFPZ.Tables["表单配置子表"].Rows[i]["FS_passnull"].ToString() == "1")
                                                    {
                                                %>
                                                <i class="light-red">*  </i>
                                                <%
                                                    }
                                                %>
                                                <%=dsFPZ.Tables["表单配置子表"].Rows[i]["FS_title"] %>：</label>

                                            <div class="col-sm-12">
                                                <div>
                                                    <table lastsel_yhb="-999999" id="grid-table-subtable-<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FSID"] %>"></table>

                                                    <div id="grid-pager-subtable-<%=dsFPZ.Tables["表单配置子表"].Rows[i]["FSID"] %>"></div>
                                                </div>
                                                <div class="ValidErrInfo col-sm-12 no-padding-left"></div>
                                            </div>
                                        </div>


                                        <%
                                                break;
                                            case "特殊代码":
                                        %>
                                        <%=RenderUserControlToString(dsFPZ.Tables["表单配置子表"].Rows[i]["FS_SPPZ_list_static"].ToString())%>
                                        <%
                                                    break;

                                                default:

                                                    break;
                                            }
                                        %>


                                        <%
                                            }
                                        %>












                                        <div class="clearfix form-actions col-xs-12 col-sm-12">




                                            <label class="col-sm-2 control-label"></label>

                                            <div class="col-sm-10  <%=css_str %>">
                                                <div class="col-xs-12 col-sm-6">


                                                    <button class="btn btn-info pull-left" type="button" id="addbutton1">
                                                        <i class="ace-icon fa fa-check bigger-110"></i>
                                                        保存
                                                    </button>



                                                    <%--      <button class="btn pull-right hidden" type="<%=htPP["reloaddb_type"].ToString() %>"  id="fanhuishangyiye">
                                                            <i class="ace-icon fa fa-undo bigger-110"></i>
                                                            返回
                                                        </button>--%>
                                                    <button class="btn pull-right" type="<%=htPP["reloaddb_type"].ToString() %>" id="reloaddb">
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
