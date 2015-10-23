<%@ Control Language="C#" AutoEventWireup="true" CodeFile="upimageuc.ascx.cs" Inherits="upfaceuc" %>
     <div class="crop-avatar-zdy">

    <!-- Current avatar -->
    <div class="avatar-view avatar-view-<%=Csite %>" title="<%=Ctitle %>" loadingimg="<%=Cloadingimg %>" upaction="<%=Cupaction %>"  isp="<%=Cisp %>">
      <img src="<%=Cnowimg %>" class="img img-thumbnail img-responsive"   >
      <input class="avatar-Cidname" name="<%=Cidname %>" type="hidden" value="<%=Cnowimg %>">
    </div>
 
    <!-- Cropping modal -->
    <div class="modal fade avatar-modal-bb"  aria-hidden="true" aria-labelledby="avatar-modal-label" role="dialog" tabindex="-1">
      <div class="modal-dialog modal-lg">
        <div class="modal-content">
          <div class="avatar-form" >
<%--            <div class="modal-header">
              <button class="close" data-dismiss="modal" type="button">&times;</button>
              <h4 class="modal-title"  >上传图片</h4>
            </div>--%>
            <div class="modal-body">
              <div class="avatar-body">

                <!-- Upload image and data -->
                <div class="avatar-upload">
                  <input class="avatar-src"  type="hidden">
                  <input class="avatar-data"  type="hidden">
                          <iframe  src=""  style="width:100%; height:30px; " frameborder="0" scrolling="no" > </iframe>
            
                </div>
                    
                  <div class="col-md-9 avatar-gaibili-quyu" >截取比例：
                      <button class="btn btn-info btn-sm avatar-gaibili"   data-method="setAspectRatio" data-option="NaN"  type="button" title="自由比例" >自由比例</button>
                 <button class="btn btn-info btn-sm avatar-gaibili"   data-method="setAspectRatio" data-option="1.7777777777777777"  type="button" title="16 / 9" >16 / 9</button>
                 <button class="btn btn-info btn-sm avatar-gaibili"   data-method="setAspectRatio" data-option="1.3333333333333333"  type="button" title="4 / 3" >4 / 3</button>
                 <button class="btn btn-info btn-sm avatar-gaibili"   data-method="setAspectRatio" data-option="1"  type="button" title="1 / 1" >1 / 1</button>
                 <button class="btn btn-info btn-sm avatar-gaibili"   data-method="setAspectRatio" data-option="0.6666666666666666"  type="button" title="2 / 3" >2 / 3</button>
                 
                  </div>
                <!-- Crop and preview -->
                <div class="row avatar-ququ">
                  <div class="col-md-9">
                    <div class="avatar-wrapper"></div>
                  </div>
                  <div class="col-md-3">
                    <div class="avatar-preview preview-lg"></div>
                    <div class="avatar-preview preview-md"></div>
                    <div class="avatar-preview preview-sm"></div>
                  </div>
                </div>

                <div class="row avatar-btns avatar-ququ">
                  <div class="col-md-9">逆时针旋转：
                    
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="-90" type="button" title="Rotate -90 degrees">90度</button>
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="-15" type="button">15度</button>
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="-30" type="button">30度</button>
               
              
                    顺时针旋转：
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="90" type="button" title="Rotate 90 degrees">90度</button>
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="15" type="button">15度</button>
                      <button class="btn btn-sm btn-primary" data-method="rotate" data-option="30" type="button">30度</button>
        
                 
                  </div>
                  <div class="col-md-3">
                    <button class="btn btn-pink btn-sm  avatar-save" type="button">确认上传</button>
                      <button class="btn btn-grey btn-sm" data-dismiss="modal" type="button">取消</button>
                      
                  </div>
                </div>
              </div>
            </div>
            <!-- <div class="modal-footer">
              <button class="btn btn-default" data-dismiss="modal" type="button">Close</button>
            </div> -->
          </div>
        </div>
      </div>
    </div><!-- /.modal -->


  </div>