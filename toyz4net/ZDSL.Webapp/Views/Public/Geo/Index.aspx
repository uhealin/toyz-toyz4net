<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网地图搜索
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
   // string strCheckInDate = WebUtil.GetParamAttr("checkInDate", DateTime.Now.ToString("yyyy-MM-dd"));
   // string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
  //  IList<GeoCommercialLocationModel> geoCls = ViewData[typeof(GeoCommercialLocationModel).Name] as IList<GeoCommercialLocationModel>;
  //  IList<GeoDistrictsModel> geoDs = ViewData[typeof(GeoDistrictsModel).Name] as IList<GeoDistrictsModel>;
  //  IList<GeoLandmarkLocationModel> geoLls = ViewData[typeof(GeoLandmarkLocationModel).Name] as IList<GeoLandmarkLocationModel>;
    string strCityName = WebUtil.GetParamAttr("cityName", "广州");
    string strKeyWord = WebUtil.GetParamAttr("keyword", "");
    string strStar = WebUtil.GetParamAttr("star", "");
    string strPirceRegexp = WebUtil.GetParamAttr("priceRegexp", "");
    string homeUrl = HomeController.GetPathIndex();
    GeoLocationModel geoLocation = ViewData[typeof(GeoLocationModel).Name] as GeoLocationModel;
    GeoModel geo = ViewData[typeof(GeoModel).Name] as GeoModel;
    string locationUrl = "";
    string lastNav = "";

    if (!string.IsNullOrEmpty(strStar))
    {
        if (strStar == "1,2")
        {
            lastNav += "经济型";
        }
        else if (strStar == "3")
        {
            lastNav += "三星级";
        }
        else if (strStar == "4")
        {
            lastNav += "四星级";
        }
        else if (strStar == "5")
        {
            lastNav += "五星级";
        }
    }

    if (!string.IsNullOrEmpty(strPirceRegexp))
    {
        ToyzNumberRangeObject priceRange = NumberUtil.ParseToyz(strPirceRegexp) as ToyzNumberRangeObject;
        lastNav += string.Format(" {0}-{1} ", priceRange.min, priceRange.max);
    }

    if (!string.IsNullOrEmpty(strKeyWord))
    {
        lastNav += " " + strKeyWord + " ";
    }
    
  %>
  
                     <div class="box b" style=" margin-bottom:3px">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="<%=strCityName %>地图搜索" href="<%=basePath %>/Public/Geo/Index?cityName=<%=strCityName %>" ><%=strCityName%>酒店</a> >>  </li>
              <%if (geoLocation != null)
                {
                    if (geoLocation is GeoDistrictsModel) {
                        locationUrl = HotelController.GetPathDHotel(geo,geoLocation);
                    }
                    else if (geoLocation is GeoCommercialLocationModel) {
                        locationUrl = HotelController.GetPathClHotel(geo, geoLocation);
                    }
                    %>
              <li><a title="<%=geo.cityName+geoLocation.name %>酒店预订" href="<%=locationUrl %>" ><%=geoLocation.name %>酒店</a> >> </li>
              <%} %>
              <li><%=lastNav %>&nbsp;地图搜索</li>
           </ul>
          
        </div>
  
  <div class="b box" style=" padding:0.5%; height:125px; margin-bottom:3px">
    <div id="divGeoCondition" class="left" style=" width:63%;" >
         
         <form method="get" action="<%=basePath %>/Public/Geo/Index" id="formHotelSearch">
          <% Html.RenderPartial("HotelSearchBox"); %>
          </form>
  
       
    </div>
    
    <div class="right" style=" width:33.5%">
			
			  <% Html.RenderPartial("DivAdSider2"); %>
			
     </div>
     
  </div>       


         
    <div id="divMap" class="left b" style=" height:540px; width:80%">
     
    </div>
    
    <div  class="right b" style=" width:19%;height:540px; ">

     <div id="divHotels" style="height:460px;"></div>
     
    
     <div id="pager"> </div>
     
  

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
       var myDrag;
       var myCity;
       var isGeoSearch = false;

       
       var drapAndZoomTool;
       function ChangeCurrCity() {
           mapSearcher.search($("input[name=cityName]").val());
       }





       function clickPager(pagenum) {
           _condition["page"] = pagenum;
           reflashHotels();
       }
       
       function reflashHotels() {

            _condition["viewName"] = "UlGeoHotelSearchList";
            var pagenum = _condition["page"];
            var params = $.param(_condition);
            StartPageWating("酒店搜索进行中");
            $("#divHotels").load("<%=basePath %>/Public/Hotel/ViewHotelSearch?" + params, function() {
                EndPageWating();
                // var pageCount = parseInt($(this).find("#inputPageCount").val());
                var total = parseInt($(this).find("#spanTotal").attr("total"));
                var p = total % _condition["rows"] > 0 ? 1 : 0;
                var pageCount = parseInt(total / _condition["rows"]) + p;
                $("#pager").pager({ pagenumber: pagenum, pagecount: pageCount, maxPage: 5, buttonClickCallback: clickPager });

                map.clearOverlays();

                $(this).find(".liHotel").each(function(index) {
                    // var aHotelDetail = $(this).find(".aHotelDetail");
                    var valHotelName = $(this).attr("hotelName");
                    var valHotelPrice = $(this).attr("price");
                    var lat = parseFloat($(this).attr("lat"));
                    var lon = parseFloat($(this).attr("lon"));
                    var address = $(this).attr("address");
                    var url = $(this).attr("url");
                    var imgUrl = $(this).attr("imgUrl");
                    var _this = $(this);
                    $(this).find("a").attr("href", "javascript:void()");
                    BMap.Convertor.translate(new BMap.Point(lon, lat), 2, function(point) {
                        var infoWindowOpts = {
                            width: 280,     // 信息窗口宽度
                            height: 170,     // 信息窗口高度
                            title: valHotelName  // 信息窗口标题
                        }
                        var content = "";

                        content += " <div class='left' style='width:90px'><img style='max-width:100%'= src='" + imgUrl + "'></div>";
                        content += "<div class='right' style='width:170px' >";
                        content += "酒店地址:" + address + "<p />";
                        content += "最低价:" + valHotelPrice + "<p />";
                        content += "<a target='_blank' href='" + url + "'>查看 " + valHotelName + "</a>";
                        content += "</div>";
                        var infoWindow = new BMap.InfoWindow(content, infoWindowOpts);

                        var marker = new BMap.Marker(point);
                        var offset = new BMap.Size(-25, 25);
                        var label = new BMap.Label(valHotelName, { position: point, offset: offset });
                        marker.setTitle(valHotelName);
                        marker.setLabel(label);
                        marker.addEventListener("click", function() {
                            this.openInfoWindow(infoWindow);
                        });

                        map.addOverlay(marker);

                        _this.click(function() {

                            marker.openInfoWindow(infoWindow);
                        });
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



       // 定义一个控件类,即function
       function ZoomControl() {
           // 默认停靠位置和偏移量
           this.defaultAnchor = BMAP_ANCHOR_TOP_LEFT;
           this.defaultOffset = new BMap.Size(10, 10);
       }

       // 通过JavaScript的prototype属性继承于BMap.Control
       ZoomControl.prototype = new BMap.Control();

       // 自定义控件必须实现自己的initialize方法,并且将控件的DOM元素返回
       // 在本方法中创建个div元素作为控件的容器,并将其添加到地图容器中
       ZoomControl.prototype.initialize = function(map) {
           // 创建一个DOM元素
           var div = document.createElement("div");
           // 添加文字说明
           div.appendChild(document.createTextNode("拉框搜索酒店"));
           // 设置样式
           div.style.cursor = "pointer";
           div.style.border = "1px solid gray";
           div.style.backgroundColor = "white";
           // 绑定事件,点击一次放大两级
           div.onclick = function(e) {
               isGeoSearch = true;
               myDrag.open();
           }
           // 添加DOM元素到地图中
           map.getContainer().appendChild(div);
           // 将DOM元素返回
           return div;
       }
       // 创建控件
       var myZoomCtrl;




       $(document).ready(function() {

           _condition["rows"] = 10;
           _condition["page"] = 1;

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



           mapSearcher.search(_condition["cityName"]);

           reflashHotels();


           myDrag = new BMapLib.RectangleZoom(map, {
               followText: "拖拽鼠标搜索酒店"
           });

 

           // 给“更换城市”链接添加点击操作


           $("#curCityText,#popup_close").click(function() {
               $("#cityList").toggle();

           });



           map.addEventListener("zoomend", function(type, target) {
               if (!isGeoSearch) {

                   return;
               }
               var b = map.getBounds();
               var sw = b.getSouthWest();
               var ne = b.getNorthEast();
               _condition["startLng"] = sw["lng"];
               _condition["endLng"] = ne["lng"];
               _condition["startLat"] = ne["lat"];
               _condition["endLat"] = sw["lat"];
               _condition["geoMode"] = "1";
               _condition["page"] = 1;
               reflashHotels();
               myDrag.close();
               isGeoSearch = false;
           }
           );


           map.addEventListener("moveend", function(type, target) {
               if (!isGeoSearch) {

                   return;
               }
               var b = map.getBounds();
               var sw = b.getSouthWest();
               var ne = b.getNorthEast();
               _condition["startLng"] = sw["lng"];
               _condition["endLng"] = ne["lng"];
               _condition["startLat"] = ne["lat"];
               _condition["endLat"] = sw["lat"];
               _condition["geoMode"] = "1";
               _condition["page"] = 1;
               reflashHotels();
               myDrag.close();
               isGeoSearch = false;
           }
           );

           myZoomCtrl = new ZoomControl();
           myZoomCtrl.setAnchor(BMAP_ANCHOR_TOP_RIGHT);
           map.addControl(myZoomCtrl);


       });                                //$(document)

   </script>
  
  
  <style>
	.cityList{height: 320px;width:372px;overflow-y:auto;}
    
    .map_popup {position: absolute;z-index: 200000;width: 382px;height: 344px;top:270px;}
    .map_popup .popup_main { background:#fff;border: 1px solid #8BA4D8;height: 100%;overflow: hidden;position: absolute;width: 100%;z-index: 2;}
    .map_popup .title {background: url("http://map.baidu.com/img/popup_title.gif") repeat scroll 0 0 transparent;
    color: #6688CC;font-size: 12px;font-weight: bold;height: 24px;line-height: 25px;padding-left: 7px;}
    .map_popup button {background: url("http://map.baidu.com/img/popup_close.gif") no-repeat scroll 0 0 transparent;
    border: 0 none;cursor: pointer;height: 12px;position: absolute;right: 4px;top: 6px;width: 12px;}	
    
    #pager ul.pages li
    {
      padding:2px;	
    }
    
    .spanHotelAddress
    {
      display:none;	
    }
    
         .liHotel .divHotelStar
          {
            float:left	
          }
    
            .liHotel .divHotelPrice
          {
            float:right;	
          }
    
</style>

</asp:Content>
