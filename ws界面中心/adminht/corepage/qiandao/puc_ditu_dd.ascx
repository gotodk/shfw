<%@ Control Language="C#" AutoEventWireup="true" CodeFile="puc_ditu_dd.ascx.cs" Inherits="qiandao_puc_ditu_dd" %>
   <style type="text/css">
		 #allmap {width: 100%;height: 400px;overflow: hidden;margin:0;font-family:"微软雅黑";}
	</style>

<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right" for="zuobiao">
                                                    地图：</label>
                                            
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
       
        map.enableScrollWheelZoom();
        map.enableInertialDragging();

        map.enableContinuousZoom();


        /*

        //添加城市选择
        var offset_cs = new BMap.Size(80, 20);
        map.addControl(new BMap.CityListControl({
            anchor: BMAP_ANCHOR_TOP_LEFT,
            offset: offset_cs,
            // 切换城市之间事件
            // onChangeBefore: function(){
            //    alert('before');
            // },
            // 切换城市之后事件
            // onChangeAfter:function(){
            //   alert('after');
            // }
        }));

        */

       
        // 添加带有定位的导航控件
        var navigationControl_123 = new BMap.NavigationControl({
            // 靠左上角位置
            anchor: BMAP_ANCHOR_TOP_LEFT,
            // LARGE类型
            type: BMAP_NAVIGATION_CONTROL_LARGE,
            // 启用显示定位
            enableGeolocation: true
        });
        map.addControl(navigationControl_123);


        // 添加定位控件
        var geolocationControl = new BMap.GeolocationControl();
        geolocationControl.addEventListener("locationSuccess", function (e) {
            // 定位成功事件
            //var address = '';
            //address += e.addressComponent.province;
            //address += e.addressComponent.city;
            //address += e.addressComponent.district;
            //address += e.addressComponent.street;
            //address += e.addressComponent.streetNumber;
            //alert("当前定位地址为：" + address);
        });
        geolocationControl.addEventListener("locationError", function (e) {
            // 定位失败事件
            alert(e.message);
        });
        map.addControl(geolocationControl);
     

        //单击获取点击的经纬度
        map.addEventListener("click", function (e) {
            var pt = e.point;
            //获取地址
            geoc.getLocation(pt, function (rs) {
             
                //提示
                var addComp = rs.addressComponents;
                bootbox.alert("您已选中" + addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber + " 坐标:" + pt.lng + "," + pt.lat);
                $("#DDzuobiao").val(pt.lng + "," + pt.lat);
                $("#DDdizhi").val(addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber);

                //跳动标记
                map.clearOverlays();
                var marker = new BMap.Marker(pt);  // 创建标注
                map.addOverlay(marker);               // 将标注添加到地图中
                marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
            });

            
        });



        </script>