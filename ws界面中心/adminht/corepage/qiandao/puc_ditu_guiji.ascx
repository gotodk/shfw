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
   <script type="text/javascript" src="/adminht/corepage/qiandao/qd_rili_ajax.aspx?zhiling=kaoqinfugaidian"></script>

 
             <!-- 某些字段，在编辑时禁用，不想用新页面的情况使用 -->
<script type="text/javascript">
    var map = new BMap.Map("allmap", {});                        // 创建Map实例
    map.centerAndZoom(new BMap.Point(105.000, 38.000), 5);     // 初始化地图,设置中心点坐标和地图级别
    map.enableScrollWheelZoom();                        //启用滚轮放大缩小
    if (document.createElement('canvas').getContext) {  // 判断当前浏览器是否支持绘制海量点
        var points = [];  // 添加海量点数据
        for (var i = 0; i < data.data.length; i++) {
          points.push(new BMap.Point(data.data[i][0], data.data[i][1]));
        }
        var options = {
            size: BMAP_POINT_SIZE_NORMAL,
            shape: BMAP_POINT_SHAPE_STAR,
            color: '#008B00'
        }
        var pointCollection = new BMap.PointCollection(points, options);  // 初始化PointCollection
        pointCollection.addEventListener('click', function (e) {
          //alert('单击点的坐标为：' + e.point.lng + ',' + e.point.lat);  // 监听点击事件
        });
        map.addOverlay(pointCollection);  // 添加Overlay

    } else {
        alert('请在chrome、safari、IE8+以上浏览器查看');
    }
  </script>