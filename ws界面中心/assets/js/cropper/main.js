(function (factory) {
  if (typeof define === 'function' && define.amd) {
    define(['jquery'], factory);
  } else if (typeof exports === 'object') {
    // Node / CommonJS
    factory(require('jquery'));
  } else {
    factory(jQuery);
  }
})(function ($) {

  'use strict';

  var console = window.console || { log: function () {} };

 

  function CropAvatar($element) {
      this.$container = $element;

      this.$avatarView = this.$container.find('.avatar-view');
      this.$avatar = this.$avatarView.find('img');

      this.$avatarCidname = this.$avatarView.find('.avatar-Cidname');
      this.$avatarModal = this.$container.find('.avatar-modal-bb');

      this.$avatar_old_img_url = this.$avatar.attr('src');

      this.$avatarForm = this.$avatarModal.find('.avatar-form');
      this.$avatarUpload = this.$avatarForm.find('.avatar-upload');
      this.$avatarSrc = this.$avatarForm.find('.avatar-src');
      this.$avatarData = this.$avatarForm.find('.avatar-data');
 
      this.$avatarInput = this.$avatarForm.find('iframe').contents().find("body").find(".upupfile")
      this.$avatartijiao = this.$avatarForm.find('iframe').contents().find("body").find(".tijiao")
      this.$avatarqingkong = this.$avatarForm.find('iframe').contents().find("body").find(".qingkong")
      this.$avatar_h_form = this.$avatarForm.find('iframe').contents().find("body").find("form")
      this.$avatar_h_form.attr('action', this.$avatarView.attr("upaction"));
    

      this.$avatarSave = this.$avatarForm.find('.avatar-save');
      this.$avatarBtns = this.$avatarForm.find('.avatar-btns');
      this.$img = null;
      this.$avatarququ = this.$avatarForm.find('.avatar-ququ');
      this.$avatargaibili = this.$avatarForm.find('.avatar-gaibili');
      this.$avatargaibiliquyu = this.$avatarForm.find('.avatar-gaibili-quyu');
      this.$avatarWrapper = this.$avatarModal.find('.avatar-wrapper');
      this.$avatarPreview = this.$avatarModal.find('.avatar-preview');

      this.init();

 

  };



  CropAvatar.prototype = {
    constructor: CropAvatar,
 
    support: {
        fileList: !!$('<input type="file">').prop('files'),
      blobURLs: !!window.URL && URL.createObjectURL,
      formData: !!window.FormData
    },

    init: function () {
      this.support.datauri = this.support.fileList && this.support.blobURLs;
  
      if (!this.support.formData) {
        this.initIframe();
      }

      this.initTooltip();
      this.initModal();
      this.addListener();
  
    },

    addListener: function () {
      this.$avatarView.on('click', $.proxy(this.click, this));
      this.$avatarInput.on('change', $.proxy(this.change, this));
   
      this.$avatarSave.on('click', $.proxy(this.submit, this));
      this.$avatarBtns.on('click', $.proxy(this.rotate, this));
      this.$avatargaibili.on('click', $.proxy(this.gaibili, this));
    },

    initTooltip: function () {
      this.$avatarView.tooltip({
        placement: 'bottom'
      });
    },

    initModal: function () {
      this.$avatarModal.modal({
        show: false
      });
    },

    initPreview: function () {
      var url = this.$avatar.attr('src');

      this.$avatarPreview.empty().html('');
    },

    initIframe: function () {
        var target = 'upload-iframe-' + (new Date()).getTime(),
            $iframe = $('<iframe>').attr({
                name: target,
                src: ''
            }),
            _this = this;

        // Ready ifrmae
        $iframe.one('load', function () {

            // respond response
            $iframe.on('load', function () {
                var data;

                try {
                    data = $(this).contents().find('body').text();
                } catch (e) {
                    console.log(e.message);
                }

                if (data) {
                    try {
                        data = $.parseJSON(data);
                    } catch (e) {
                        console.log(e.message);
                    }
                    
                    _this.submitDone(data);
                } else {
                    _this.submitFail('上传失败:!'+data.message);
                }

                _this.submitEnd();

            });
        });

        this.$iframe = $iframe;
        this.$avatar_h_form.attr('target', target).after($iframe.hide());
    },

    click: function () {
  
        if (this.$avatar.attr('src') == this.$avatarView.attr("loadingimg"))
        {
            bootbox.alert("上传中，请稍后……");
            return;
        }

        if (this.$avatarView.attr("isp") == "yes") {
            this.$avatargaibiliquyu.hide()
           // this.$img.cropper("setAspectRatio", "1");
        }
        else {
            this.$avatargaibiliquyu.show();
            //this.$img.cropper("setAspectRatio", "NaN");
        }

        this.$avatarqingkong.click();
        this.stopCropper();
        
      this.$avatarModal.modal('show');
      this.initPreview();
    },

   

    change: function () {
      var files,
          file;

      if (this.support.datauri) {
          files = this.$avatarInput.prop('files');
          var resetneed = 1;
          if (files.length > 0) {
              file = files[0];

              if (this.isImageFile(file)) {
                  if (this.url) {
                      URL.revokeObjectURL(this.url); // Revoke the old one
                  }

                  this.url = URL.createObjectURL(file);

                  resetneed = 0;
                  this.startCropper();
              }
          }

          if (resetneed == 1)
          {
              this.$avatarqingkong.click();
              this.$avatar.attr('src', this.$avatar_old_img_url);
              this.stopCropper();
          }
         

      } else {
          this.$avatartijiao.click();
      }


 

    },

    submit: function () {
      if (!this.$avatarSrc.val() && !this.$avatarInput.val()) {
        return false;
      }

     
        this.ajaxUpload();
       
    },

    rotate: function (e) {
      var data;

      if (this.active) {
        data = $(e.target).data();

        if (data.method) {
          this.$img.cropper(data.method, data.option);
        }
      }
    },
    gaibili: function (e) {
 
        var data;

        if (this.active) {
            data = $(e.target).data();

            if (data.method) {
                this.$img.cropper(data.method, data.option);
            }
        }
        
    },
    isImageFile: function (file) {
      if (file.type) {
        return /^image\/\w+$/.test(file.type);
      } else {
        return /\.(jpg|jpeg|png|gif|bmp)$/.test(file);
      }
    },

    startCropper: function () {
        var _this = this;
 
       
      if (this.active) {
        this.$img.cropper('replace', this.url);
      } else {
        this.$img = $('<img src="' + this.url + '">');
        this.$avatarWrapper.empty().html(this.$img);

        this.$img.cropper({
          aspectRatio: 1,
          preview: this.$avatarPreview.selector,
          strict: false,
          crop: function (data) {
              //console.log(data);
            var json = [
                  '{"x":' + data.x,
                  '"y":' + data.y,
                  '"height":' + data.height,
                  '"width":' + data.width,
                  '"rotate":' + data.rotate + '}'
                ].join();

            _this.$avatarData.val(json);
          }
        });

        this.active = true;
        if (this.$avatarView.attr("isp") == "no") {
            this.$img.cropper("setAspectRatio", "NaN");
        }
      }
    },

    stopCropper: function () {
      if (this.active) {
        this.$img.cropper('destroy');
        this.$img.remove();
        this.active = false;
      }
    },

    ajaxUpload: function () {
        
        var url = this.$avatarView.attr("upaction");
        var data = null;
         
        if (this.support.formData) {
            data = new FormData();
            data.append("avatar_file", this.$avatarInput[0].files[0]);
            data.append("avatar_data", this.$avatarData.val());
            data.append("hhbiaozhi", "upandre");
            var _this = this;
            $.ajax(url, {
                type: 'post',
                data: data,
                dataType: 'json',
                processData: false,
                contentType: false,

                beforeSend: function () {

                    _this.submitStart();
                },

                success: function (data) {

                    _this.submitDone(data);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    _this.submitFail(textStatus || errorThrown);
                },

                complete: function () {

                    _this.submitEnd();
                }
            });
        }
        else {
            data = "avatar_data=" + this.$avatarData.val() + "&hhbiaozhi=onlyre&reurl=" + this.$avatarSrc.val();
            var _this = this;
            $.ajax(url, {
                type: 'post',
                data: data,
                dataType: 'json',
           

                beforeSend: function () {

                    _this.submitStart();
                },

                success: function (data) {

                    _this.submitDone(data);
                },

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    _this.submitFail(textStatus || errorThrown);
                },

                complete: function () {

                    _this.submitEnd();
                }
            });
        }
      


    },

    syncUpload: function () {
      this.$avatarSave.click();
    },

    submitStart: function () {
        this.$avatarqingkong.click();
        this.$avatar.attr('src', this.$avatarView.attr("loadingimg"));
        this.stopCropper();
        this.$avatarModal.modal('hide');

     
    },

    submitDone: function (data) {
   
        if ($.isPlainObject(data)) {
            if (data.state == "200") {
                if (data.result) {
                    this.url = data.result;

                    if (this.support.datauri || this.uploaded) {
                        this.uploaded = false;
                        this.cropDone();
                        var w_h = data.message.split(",");
                        var nk = (this.$avatarView.height() * w_h[0]) / w_h[1];
                      
                       this.$avatarView.width(nk);
                    } else {
                        this.uploaded = true;
                        this.$avatarSrc.val(this.url);
                        this.startCropper();
                     
                    }

                    this.$avatarInput.val('');
                } else if (data.message) {


                    this.$avatarqingkong.click();
                    this.$avatar.attr('src', this.$avatar_old_img_url);
                    this.stopCropper();
                    this.$avatarModal.modal('hide');

          
                    bootbox.alert(data.message);

                }
            }
            else {
                this.$avatarqingkong.click();
                this.$avatar.attr('src', this.$avatar_old_img_url);
                this.stopCropper();
                this.$avatarModal.modal('hide');

      
                bootbox.alert(data.message);
            }
      } else {
        

            this.$avatarqingkong.click();
          this.$avatar.attr('src', this.$avatar_old_img_url);
          this.stopCropper();
          this.$avatarModal.modal('hide');
 
          bootbox.alert('意外错误，上传失败');
      }
    },

    submitFail: function (msg) {
   

        this.$avatarqingkong.click();
        this.$avatar.attr('src', this.$avatar_old_img_url);
        this.stopCropper();
        this.$avatarModal.modal('hide');


        bootbox.alert(msg);
    },

    submitEnd: function () {
       ;
    },

    cropDone: function () {
        this.$avatarqingkong.click();
        this.$avatar.attr('src', this.url);
        this.$avatarCidname.val(this.url);
      this.stopCropper();
      this.$avatarModal.modal('hide');
    },

    alert: function (msg) {
      var $alert = [
            '<div class="alert alert-danger avatar-alert alert-dismissable">',
              '<button type="button" class="close" data-dismiss="alert">&times;</button>',
              msg,
            '</div>'
          ].join('');
      this.$avatarView.width(this.$avatarView.height());
      this.$avatarUpload.after($alert);
    }
  };

  $(function () {
      
      $(".crop-avatar-zdy").each(function () {

          
          var iframe = $(this).find('iframe');
          var _thiss = $(this);
       
          // Ready ifrmae
          iframe.on('load', function () {
              new CropAvatar(_thiss);
          });
 
          iframe.attr("src", "/assets/js/cropper/hideform.html")

           
      });
  });

});
