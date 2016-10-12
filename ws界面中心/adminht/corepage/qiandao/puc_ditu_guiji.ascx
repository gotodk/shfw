<%@ Control Language="C#" AutoEventWireup="true" CodeFile="puc_ditu_guiji.ascx.cs" Inherits="puc_ditu_guiji" %>
   <style type="text/css">
		 #allmap {width: 100%;height: 400px;overflow:visible;margin:0;font-family:"微软雅黑";}
	</style>






<div class="form-group" >
                                                <label class="col-sm-2 col-xs-12 control-label no-padding-right" for="zuobiao">
                                                    地图：</label>
                                             <%--     <div class="col-sm-10 col-xs-12" id="dtqy_ttt_load">
                                                      正在定位，请稍后，请确保您的浏览器支持已允许定位权限。
                                                </div>--%>
                                                <div class="col-sm-12 col-xs-12" id="dtqy_ttt">

                                                  <div id="allmap"></div> 
                                                </div>

                                            </div>


    	<script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=5g5ctXCm8CUaAtqw6DoUYyEhuKGrbAeE"></script>


 
             <!-- 某些字段，在编辑时禁用，不想用新页面的情况使用 -->
<script type="text/javascript">
 

    var map = new BMap.Map("allmap", {});                        // 创建Map实例
    map.centerAndZoom(new BMap.Point(105.000, 38.000), 5);     // 初始化地图,设置中心点坐标和地图级别
    map.enableScrollWheelZoom();                        //启用滚轮放大缩小
    

    var opts = {
        width: 250,     // 信息窗口宽度
        height: 80,     // 信息窗口高度
        title: "详细信息", // 信息窗口标题
        enableMessage: true//设置允许信息窗发送短息
    };




    function jiazai()
    {
        map.clearOverlays();
        for (var i = 0; i < data_info.length; i++) {
            var marker = new BMap.Marker(new BMap.Point(data_info[i][0], data_info[i][1]));  // 创建标注
            var content = data_info[i][2];
            map.addOverlay(marker);               // 将标注添加到地图中
            addClickHandler(content, marker);
        }
    }

    function addClickHandler(content, marker) {
        marker.addEventListener("click", function (e) {
            openInfo(content, e)
        }
		);
    }
    function openInfo(content, e) {
        var p = e.target;
        var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
        var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
        map.openInfoWindow(infoWindow, point); //开启信息窗口
    }


 
    jiazai();
 

  </script>