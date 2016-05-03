<%@ Control Language="C#" AutoEventWireup="true" CodeFile="puc_ditu.ascx.cs" Inherits="qiandao_puc_ditu" %>
   <style type="text/css">
		 #allmap {width: 100%;height: 400px;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>

<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right" for="zuobiao">
                                                    地图：</label>
                                             <%--     <div class="col-sm-10 col-xs-12" id="dtqy_ttt_load">
                                                      正在定位，请稍后，请确保您的浏览器支持已允许定位权限。
                                                </div>--%>
                                                <div class="col-sm-10 col-xs-12" id="dtqy_ttt">

                                                  <div id="allmap"></div> 
                                                </div>

                                            </div>

    	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=5g5ctXCm8CUaAtqw6DoUYyEhuKGrbAeE"></script>
 

 
             <!-- 某些字段，在编辑时禁用，不想用新页面的情况使用 -->
    <script type="text/javascript">
    
        // 百度地图API功能
        var map = new BMap.Map("allmap");
        var point = new BMap.Point(122.09395837, 37.52878708);
        map.centerAndZoom(point, 16);
        var geoc = new BMap.Geocoder();
        var geolocation = new BMap.Geolocation();

     
        geolocation.getCurrentPosition(function (r) {
            if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                var mk = new BMap.Marker(r.point);
                map.addOverlay(mk);
                map.panTo(r.point);


                var pt = r.point;

                geoc.getLocation(pt, function (rs) {
                    var addComp = rs.addressComponents;
                    //alert(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber);
                    $("#zuobiao").val(r.point.lng + "," + r.point.lat);
                    $("#dizhi").val(addComp.street + ", " + addComp.streetNumber);
                    $("#chengshi").val(addComp.city);
                    $("#shengfen").val(addComp.province);
                    $("#quxian").val(addComp.district);

                    $("#addbutton1_top").removeAttr("disabled");
                    $("#addbutton1").removeAttr("disabled");
                    
                });
                //alert('您的位置：' + r.point.lng + ',' + r.point.lat);
            }
            else {
                alert('定位失败' + this.getStatus());
            }
        }, { enableHighAccuracy: true });

        //关于状态码
        //BMAP_STATUS_SUCCESS	检索成功。对应数值“0”。
        //BMAP_STATUS_CITY_LIST	城市列表。对应数值“1”。
        //BMAP_STATUS_UNKNOWN_LOCATION	位置结果未知。对应数值“2”。
        //BMAP_STATUS_UNKNOWN_ROUTE	导航结果未知。对应数值“3”。
        //BMAP_STATUS_INVALID_KEY	非法密钥。对应数值“4”。
        //BMAP_STATUS_INVALID_REQUEST	非法请求。对应数值“5”。
        //BMAP_STATUS_PERMISSION_DENIED	没有权限。对应数值“6”。(自 1.1 新增)
        //BMAP_STATUS_SERVICE_UNAVAILABLE	服务不可用。对应数值“7”。(自 1.1 新增)
        //BMAP_STATUS_TIMEOUT	超时。对应数值“8”。(自 1.1 新增)
 
     
        </script>