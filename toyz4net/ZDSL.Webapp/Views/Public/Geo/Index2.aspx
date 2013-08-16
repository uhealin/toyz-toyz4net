<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Geo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
   // string strCheckInDate = WebUtil.GetParamAttr("checkInDate", DateTime.Now.ToString("yyyy-MM-dd"));
   // string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
    IList<GeoCommercialLocationModel> geoCls = ViewData[typeof(GeoCommercialLocationModel).Name] as IList<GeoCommercialLocationModel>;
    IList<GeoDistrictsModel> geoDs = ViewData[typeof(GeoDistrictsModel).Name] as IList<GeoDistrictsModel>;
    IList<GeoLandmarkLocationModel> geoLls = ViewData[typeof(GeoLandmarkLocationModel).Name] as IList<GeoLandmarkLocationModel>;
    
  %>
    <div id="divGeoCondition">

         
  <table  id="tableHotelSearch" class="tableCondition"  style=" width:99.5%;" >
  
      <tr>
         <th>城市：</th>
         <td><div style=" float:left;" id="inputCityPicker" ></div> <button type="button" onclick="ChangeCurrCity()">切换城市</button></td>
         <th>入店日期：</th>
         <td><input name="checkInDate"  readonly /></td>
         <th>离店日期：</th>
         <td><input name="checkOutDate"  readonly /></td>
         
         <th  rowspan="2">
           <button type="button" style=" height:45px;" onclick="reflashHotels({page:1})">查找酒店</button>
         </th>
      </tr>
      

      
      <tr>
         <th>价格范围：</th>
         <td>
                         <select  id="inputPriceRegexp"  name="priceRegexp">
                <option  value="gt@(0)">不限</option>
                <option value="gt@(700)">700以上</option>
                <option value="between@(500),(700)">500-700</option>
                <option value="between@(300),(500)">300-500</option>
                <option value="between@(200),(300)">200-300</option>
                <option value="lt@(200)">200以下</option>
                </select>
         </td>
         <th>酒店星级：</th>
         <td>
             <select id="inputStar"  name="star" >
                <option  value="">不限</option>
                <option value="0">经济型</option>
                <option value="1">一星级</option>
                <option value="2">二星级</option>
                <option value="3">三星级</option>
                <option value="4">四星级</option>
                <option value="5">五星级</option>
                
              </select>
         </td>
         <th>关键词搜索：</th>
         <td ><input name="keyword"  value=""  /></td>
        

      </tr>
      </table>
       
    </div>
    
    <div id="divMap" class="left b" style=" height:500px; width:65%">
     
    </div>
    
    <div  class="right b" style=" width:34%; height:500px; overflow:scroll;">
     <div id="divHotels"></div>
     <div id="pager"></div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<%  
    string basePath = WebUtil.GetWebRootPath();
   %>
   <script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2" ></script>
   <script type="text/javascript" src="http://dev.baidu.com/wiki/static/map/API/examples/script/convertor.js"></script>
   <script type="text/javascript" src="http://api.map.baidu.com/library/RectangleZoom/1.2/src/RectangleZoom_min.js"></script>
   <script type="text/javascript">

       
       var map;
       var rows = 10;
       var drapAndZoomTool;
       function ChangeCurrCity() {
           mapSearcher.search($("input[name=cityName]").val());
       }

       function clickPager(pagenum) {
           reflashHotels({ page: pagenum });
       }
       
       function reflashHotels(condition) {
            condition = condition || {};
            condition["checkInDate"] = $("#tableHotelSearch").find("input[name=checkInDate]").val();
            condition["checkOutDate"] = $("#tableHotelSearch").find("input[name=checkOutDate]").val();
            condition["cityName"] = $("#tableHotelSearch").find("input[name=cityName]").val();
            condition["star"] = $("#tableHotelSearch").find("#inputStar").val();
            condition["priceRegexp"] = $("#tableHotelSearch").find("#inputPriceRegexp").val();
            condition["keyword"] = $("#tableHotelSearch").find("input[name=keyword]").val();
            condition["rows"] = rows;
            condition["viewName"] = "UlGeoHotelSearchList";
            var pagenum = condition["page"];
            var params = $.param(condition);
            StartPageWating("酒店搜索进行中");
            $("#divHotels").load("<%=basePath %>/Public/Hotel/ViewHotelSearch?" + params, function() {
                EndPageWating();
               // var pageCount = parseInt($(this).find("#inputPageCount").val());
                var total = parseInt($(this).find("#spanTotal").attr("total"));
                var p = total % condition["rows"] > 0 ? 1 : 0;
                var pageCount = parseInt(total / condition["rows"]) + p;
                $("#pager").pager({ pagenumber: pagenum, pagecount: pageCount,maxPage:5 , buttonClickCallback: clickPager });

                map.clearOverlays();

                $(this).find(".liHotel").each(function(index) {
                    // var aHotelDetail = $(this).find(".aHotelDetail");
                    var valHotelName = $(this).attr("hotelName");
                    var valHotelPrice = $(this).attr("price");
                    var lat = parseFloat($(this).attr("lat"));
                    var lon = parseFloat($(this).attr("lon"));
                    var address = $(this).attr("address");
                    var url = $(this).attr("url");
                    BMap.Convertor.translate(new BMap.Point(lon, lat), 2, function(point) {
                        var infoWindowOpts = {
                            width: 250,     // 信息窗口宽度
                            height: 150,     // 信息窗口高度
                            title: valHotelName  // 信息窗口标题
                        }
                        var content = "";
                        content += "酒店地址:" + address + "<p />";
                        content += "最低价:" + valHotelPrice + "<p />";
                        content += "<a target='_blank' href='" + url + "'>查看 " + valHotelName + "</a>";
                        var infoWindow = new BMap.InfoWindow(content, infoWindowOpts);

                        var marker = new BMap.Marker(point);
                        var offset=new BMap.Size(-25,25);
                        var label = new BMap.Label(valHotelName, { position: point, offset: offset });
                         marker.setTitle(valHotelName);
                         marker.setLabel(label);
                         marker.addEventListener("click", function() {
                             this.openInfoWindow(infoWindow);
                         });

                         map.addOverlay(marker);

                        // var overlay = new ComplexCustomOverlay(point, valHotelPrice, valHotelName + ":" + valHotelPrice, infoWindow);
                        // overlay.addEventListener("click", function() {
                        //    this.openInfoWindow(infoWindow);
                        //  });
                        // map.addOverlay(overlay);

                        //var label = new BMap.Label(valHotelName, { position: point });

                        //map.addOverlay(label);
                    });







                });
                map.zoomTo(13);
            });
        }


       var mapSearcher = new BMap.LocalSearch("中国", {
           onSearchComplete: function(result) {
           if (mapSearcher.getStatus() == BMAP_STATUS_SUCCESS) {
                   var res = result.getPoi(0);
                   var point = res.point;
                   map.centerAndZoom(point, 12);
               }
           }, renderOptions: {  //结果呈现设置，

               map: map,
               autoViewport: true,
               selectFirstResult: true
           }, onInfoHtmlSet: function(poi, html) {//标注气泡内容创建后的回调函数，有了这个，可以简单的改一下返回的html内容了。
               alert(html.innerHTML)
           } //这一段可以不要，只不过是为学习更深层次应用而加入的。
       });




       function ComplexCustomOverlay(point, text, mouseoverText,infoWindow) {
           this._point = point;
           this._text = text;
           this._overText = mouseoverText;
           this._infoWindow = infoWindow;
       }
       ComplexCustomOverlay.prototype = new BMap.Overlay();
       ComplexCustomOverlay.prototype.initialize = function(map) {
           this._map = map;
           var infoWindow = this._infoWindow;
           var div = this._div = document.createElement("div");
           div.style.position = "absolute";
           div.style.zIndex = BMap.Overlay.getZIndex(this._point.lat);
           div.style.backgroundColor = "#EE5D5B";
           div.style.border = "1px solid #BC3B3A";
           div.style.color = "white";
           div.style.height = "18px";
           div.style.padding = "2px";
           div.style.lineHeight = "18px";
           div.style.whiteSpace = "nowrap";
           div.style.MozUserSelect = "none";
           div.style.fontSize = "12px"
           var span = this._span = document.createElement("span");
           div.appendChild(span);
           span.appendChild(document.createTextNode(this._text));
           var that = this;

           var arrow = this._arrow = document.createElement("div");
           arrow.style.background = "url(http://map.baidu.com/fwmap/upload/r/map/fwmap/static/house/images/label.png) no-repeat";
           arrow.style.position = "absolute";
           arrow.style.width = "11px";
           arrow.style.height = "10px";
           arrow.style.top = "22px";
           arrow.style.left = "10px";
           arrow.style.overflow = "hidden";
           div.appendChild(arrow);

           div.onmouseover = function() {
               this.style.backgroundColor = "#6BADCA";
               this.style.borderColor = "#0000ff";
               this.getElementsByTagName("span")[0].innerHTML = that._overText;
               arrow.style.backgroundPosition = "0px -20px";
           }

           div.onmouseout = function() {
               this.style.backgroundColor = "#EE5D5B";
               this.style.borderColor = "#BC3B3A";
               this.getElementsByTagName("span")[0].innerHTML = that._text;
               arrow.style.backgroundPosition = "0px 0px";
           }

           div.onclick = function() {
               alert("xxx");
               //that.openInfoWindow(infoWindow);
           }

           map.getPanes().labelPane.appendChild(div);

           return div;
       }
       ComplexCustomOverlay.prototype.draw = function() {
           var map = this._map;
           var pixel = map.pointToOverlayPixel(this._point);
           this._div.style.left = pixel.x - parseInt(this._arrow.style.left) + "px";
           this._div.style.top = pixel.y - 30 + "px";
       }




       // 定义一个控件类,即function
       function ZoomControl() {
           // 默认停靠位置和偏移量
           this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
           this.defaultOffset = new BMap.Size(500, 10);
       }

       // 通过JavaScript的prototype属性继承于BMap.Control
       ZoomControl.prototype = new BMap.Control();

       // 自定义控件必须实现自己的initialize方法,并且将控件的DOM元素返回
       // 在本方法中创建个div元素作为控件的容器,并将其添加到地图容器中
       ZoomControl.prototype.initialize = function(map) {
           // 创建一个DOM元素
           var div = document.createElement("div");
           // 添加文字说明
           div.appendChild(document.createTextNode("拖框搜索"));
           // 设置样式
           div.style.cursor = "pointer";
           div.style.border = "1px solid gray";
           div.style.backgroundColor = "white";
           // 绑定事件,点击一次放大两级
           div.onclick = function(e) {
               // map.setZoom(map.getZoom() + 2);
               drapAndZoomTool.open();
           }
           // 添加DOM元素到地图中
           map.getContainer().appendChild(div);
           // 将DOM元素返回
           return div;
       }



       $(document).ready(function() {

       SetHotelConditionForm("#divGeoCondition");

           map = new BMap.Map("divMap");
           map.addControl(new BMap.NavigationControl());
           map.enableScrollWheelZoom();
           //map.centerAndZoom(new BMap.Point(116.404, 39.915), 11);

           // 创建控件
          // var myZoomCtrl = new ZoomControl();
           // 添加到地图当中
          // map.addControl(myZoomCtrl);

           //map.addEventListener("zoomend", function(type, target) {
               //var pointSW = this.getSouthWest();
              // var pointNE = this.getNorthEast();
          // });

         //  drapAndZoomTool = new BMapLib.RectangleZoom(map, {
         //      followText: "拖拽鼠标搜索酒店"
          // , autoClose: true
         //  });



           mapSearcher.search("广州");

           reflashHotels({ page: 1 });

           $("#tableHotelSearch").find("input[name=checkInDate]").datepicker({
               defaultDate: "0"
             , minDate: "0"
             , maxDate: "20"
             , changeMonth: false
             , numberOfMonths: 2
             , onSelect: function(selectedDate) {
                 var _minDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, selectedDate).addDays(1);
                 var _maxDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, selectedDate).addDays(20);
                 var valDate = $.datepicker.formatDate($.datepicker._defaults.dateFormat, _minDate);
                 $("#divOrderRangeDate").find("input[name=checkOutDate]").val(valDate);
                 var option = {

                     minDate: _minDate
                     , maxDate: _maxDate
                 };
                 $("#divOrderRangeDate").find("input[name=checkOutDate]").datepicker("option", option);
                 // $("#tableHotelQuickSearch").find("input[name=checkOutDate]").datepicker("show");

             }
           });

           $("#tableHotelSearch").find("input[name=checkOutDate]").datepicker({
               defaultDate: "1"
              , minDate: "1"
               , changeMonth: false
               , numberOfMonths: 2
           });


       });         //$(document)

   </script>

</asp:Content>
