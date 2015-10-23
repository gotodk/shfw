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

 

  function InitYHBCityList($element) {

      this.$container = $element;

      this.$yhb_city_p = this.$container.find('.yhb_city_p');
      this.$yhb_city_c = this.$container.find('.yhb_city_c');
      this.$yhb_city_q = this.$container.find('.yhb_city_q');
   

      this.init();

 

  };



  InitYHBCityList.prototype = {
 
    init: function () {
        //初始化
        //this.$yhb_city_p.empty();
 
        //this.$yhb_city_c.empty();
        //this.$yhb_city_q.empty();
   
        this.init_p();
      this.addListener();
  
    },

    addListener: function () {
        this.$yhb_city_p.on('change', $.proxy(this.p_change, this));
        this.$yhb_city_c.on('change', $.proxy(this.c_change, this));
 
    },

 
    init_p: function () {
     
        var datac = "cpq=p";
        var t_p = this.$yhb_city_p;
        $.ajax("/ajax_cpq.aspx", {
            type: 'post',
            data: datac,
            dataType: 'xml',
            async: false,
            beforeSend: function () {
                ;
            },

            success: function (data) {
                //解析xml并显示在界面上
                if ($(data).find('错误>错误提示').text() != "") {
                    bootbox.alert($(data).find('错误>错误提示').text());
                    return false;
                }
                else {
                    t_p.empty();
                    t_p.append("<option value='0' selected>请选择省份</option>");  //添加一项option
                    $(data).find("省市区数据").each(function (i) {

                        var zhi = $(this).children("p_number").text();
                        var ming = $(this).children("p_namestr").text();
                        t_p.append("<option value='" + zhi + "' >" + ming + "</option>");  //添加一项option
                    });
                }
              
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus || errorThrown);
            },

            complete: function () {
                ;
            }
        });

    },
  

    p_change: function () {
       
        var datac = "cpq=c&val_p=" + this.$yhb_city_p.val();
 
        var t_c = this.$yhb_city_c;
        var t_q = this.$yhb_city_q;
        $.ajax("/ajax_cpq.aspx", {
            type: 'post',
            data: datac,
            dataType: 'xml',
            async: false,
            beforeSend: function () {
                ;
            },

            success: function (data) {

                //解析xml并显示在界面上
                if ($(data).find('错误>错误提示').text() != "") {
                    bootbox.alert($(data).find('错误>错误提示').text());
                    return false;
                }
                else {
                    t_q.empty();
                    t_q.append("<option value='0' selected>请选择区县</option>");  //添加一项option

                    t_c.empty();
                    t_c.append("<option value='0' selected>请选择城市</option>");  //添加一项option
                    $(data).find("省市区数据").each(function (i) {

                        var zhi = $(this).children("c_number").text();
                        var ming = $(this).children("c_namestr").text();
                        t_c.append("<option value='" + zhi + "'>" + ming + "</option>");  //添加一项option
                    });
                }
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus || errorThrown);
            },

            complete: function () {
                ;
            }
        });
         
    },

    c_change: function () {
        var datac = "cpq=q&val_p=" + this.$yhb_city_c.val();

        var t_q = this.$yhb_city_q;
        $.ajax("/ajax_cpq.aspx", {
            type: 'post',
            data: datac,
            dataType: 'xml',
            async: false,
            beforeSend: function () {
                ;
            },

            success: function (data) {

                //解析xml并显示在界面上
                if ($(data).find('错误>错误提示').text() != "") {
                    bootbox.alert($(data).find('错误>错误提示').text());
                    return false;
                }
                else {
                    t_q.empty();
                    t_q.append("<option value='0' selected>请选择区县</option>");  //添加一项option
                    $(data).find("省市区数据").each(function (i) {

                        var zhi = $(this).children("q_number").text();
                        var ming = $(this).children("q_namestr").text();
                        t_q.append("<option value='" + zhi + "'>" + ming + "</option>");  //添加一项option
                    });
                }
            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus || errorThrown);
            },

            complete: function () {
                ;
            }
        });

    } 

 
  };
  
  $(function () {

      $(".yhb_city").each(function () {
           new InitYHBCityList($(this));
      });
  });

});
