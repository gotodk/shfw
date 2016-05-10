<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_touxiang.ascx.cs" Inherits="wuc_touxiang" %>

 <%@ Register Src="~/upimageuc.ascx" TagPrefix="uc1" TagName="upimageuc" %>


<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right" for="touxiang">
                                                    头像：</label>
                                           
                                                <div class="col-sm-10 col-xs-12">

                                                  <uc1:upimageuc runat="server" ID="upimageuc3"   Ctitle="上传头像" Cisp="yes"  Cidname="qqtutu3"  />
                                                </div>

                                            </div>
