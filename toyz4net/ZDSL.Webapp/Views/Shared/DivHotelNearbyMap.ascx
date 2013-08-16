<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>



<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<% 
    string basePath =WebUtil.GetWebRootPath();
    GeoModel geo = ViewData[typeof(GeoModel).Name] as GeoModel; 
    HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
    
     %>
        
         <div  style=" overflow:auto" > 
         <!--
         <div>离酒店距离：
            <select onchange="_condition['radius']=this.value;refalshHotelNearby();">
               <option value="1000" selected>1000</option>
               <option value="2000">2000</option>
               <option value="3000">3000</option>
            </select>
         </div>
        -->
       <div id="divNearbyMap" style=" height:400px"> </div>
        <div id="divNearybyHotelList" ></div>
        <div id="divNearbyHotelPager"></div>
         </div>
         
         <style>
         
          .liHotel
          {
            width:46.5%;
            float:left;	
             padding:0.5%;
             margin-right:0.5%;
           
           margin-top:0.5%;
           clear:none;	
          }
          
          #inputTotal
          {
            display:none;	
          }
          
          .spanHotelAddress
          {
            display:none;	
          }
          
          .liHotel .divStar
          {
            display:none;	
          }
          
          .liHotel .divHotelName
          {
            float:left;	
            width:70%;
          }
          
         .liHotel .divHotelPrice
          {
            float:right;
            
          }
         
         </style>
         
         <script type="text/javascript">

             var _hotelNearbyMap;
             
             var _point=new BMap.Point(<%= hotelDetail.lon %>,<%= hotelDetail.lat %>); 
             
             function refalshHotelNearby(point){
             
                       var url="<%=basePath %>/Public/Hotel/ViewHotelSearch";
                       _condition["viewName"]="UlGeoHotelSearchList";
                       _condition["geoMode"]="2";
                       _condition["startLat"]=_point["lat"];
                       _condition["startLng"]=_point["lng"];
                       _condition["orderbyCode"]="distance";
                       _condition["orderbyType"]="asc";
                       _condition["rows"]=17;
                       _condition["page"]=1;
                     //StartPageWating("酒店搜索进行中");
                       $("#divNearybyHotelList").load(url,_condition,function(){
                       //EndPageWating();
                          //  var total = parseInt($(this).find("#spanTotal").attr("total"));
                          //  var p = total % _condition["rows"] > 0 ? 1 : 0;
                //var pageCount = parseInt(total / _condition["rows"]) + p;
               // $("#divNearbyHotelPager").pager({ pagenumber: pagenum, pagecount: pageCount, maxPage: 5, buttonClickCallback: clickPager });

                    _hotelNearbyMap.clearOverlays();

                 $(this).find(".liHotel").each(function(index) {
                    // var aHotelDetail = $(this).find(".aHotelDetail");
                   
                    $(this).find("a").attr("href", "javascript:void()");
                    var valHotelName = $(this).attr("hotelName");
                    var valHotelPrice = $(this).attr("price");
                    var lat = parseFloat($(this).attr("lat"));
                    var lon = parseFloat($(this).attr("lon"));
                    var address = $(this).attr("address");
                    var url = $(this).attr("url");
                    var imgUrl = $(this).attr("imgUrl");
                    var _this = $(this);
                    //$(this).find("a").attr("href", "javascript:void()");
                    //alert(valHotelName);
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
                        
                        _hotelNearbyMap.addOverlay(marker);
                        _this.click(function() {
                            ScrollTop();
                            marker.openInfoWindow(infoWindow);
                        });
                        
                       });  // BMap.Convertor
               }); // $(this).find(".liHotel")
              }); // $("#divNearybyHotelList")
             }

             $(document).ready(function() {
                  _condition["radius"]=5000;
                  _hotelNearbyMap=new BMap.Map("divNearbyMap");
                  _hotelNearbyMap.addControl(new BMap.NavigationControl());
                  _hotelNearbyMap.enableScrollWheelZoom();
                  _hotelNearbyMap.setMinZoom(12);
                     BMap.Convertor.translate(new BMap.Point(<%= hotelDetail.lon %>,<%= hotelDetail.lat %>), 2, function(point) {
                   
                      var marker= new BMap.Marker(point);
                      var offset=new BMap.Size(-25,25);
                      var label = new BMap.Label("<%=hotelDetail.name %>", { position: point, offset: offset });
                       marker.setLabel(label);
                      
                       var infoWindowOpts = {
                            width: 250,     // 信息窗口宽度
                            height: 100,     // 信息窗口高度
                            title: "<%=hotelDetail.name %>"  // 信息窗口标题
                        }
                        var content = "";
                        content += "酒店地址:<%=hotelDetail.address %> <p />";
                       // content += "<a target='_blank' href='" + url + "'>查看 " + hotelDetail.name + "</a>";
                        var infoWindow = new BMap.InfoWindow(content, infoWindowOpts); 
                        marker.addEventListener("click", function() {
                             this.openInfoWindow(infoWindow);
                         });
                       _hotelNearbyMap.addOverlay(marker);
                       _hotelNearbyMap.centerAndZoom(point,15);
                       marker.openInfoWindow(infoWindow);
                      // _point=point;
                       refalshHotelNearby();


                    });
                   
                   
             });  //$(document).ready
         
         </script>