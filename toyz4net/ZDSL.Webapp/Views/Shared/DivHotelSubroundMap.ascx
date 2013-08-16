<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>

<% 
    string basePath =WebUtil.GetWebRootPath();
    IList<HotelLandMarkModel> hotelLls = ViewData[typeof(HotelLandMarkModel).Name] as IList<HotelLandMarkModel>;
    IList<HotelSurroundingAttractionModel> hotelSas = ViewData[typeof(HotelSurroundingAttractionModel).Name] as IList<HotelSurroundingAttractionModel>;
    IList<HotelSurroundingCommerceModel> hotelScs = ViewData[typeof(HotelSurroundingCommerceModel).Name] as IList<HotelSurroundingCommerceModel>;
    IList<HotelSurroundingRestaurantModel> hotelSrs = ViewData[typeof(HotelSurroundingRestaurantModel).Name] as IList<HotelSurroundingRestaurantModel>;
    IList<HotelSurroundingShopModel> hotelSss = ViewData[typeof(HotelSurroundingShopModel).Name] as IList<HotelSurroundingShopModel>;
    GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel; 
    HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
    
     %>
         
         <div  style=" overflow:auto" > 
       <div id="divSubroundMap" style=" height:400px"> </div>
        <% if (hotelLls.Count > 0)
           { %>
        <h3>标志物</h3>
        <%} %>
         <ul>
           
           <% foreach(HotelLandMarkModel hotelLl in hotelLls) {      
            %>
           <li>
             <span class="spanSubround"><%=hotelLl.landmarkName %></span><%=hotelLl.landmarkNameEn %>
           </li>
           <%} %>
         </ul>
         
                 <% if (hotelSas.Count > 0)
         { %>
         <h3>特色建筑</h3>
         <%} %>
         <ul>
           <% foreach (HotelSurroundingAttractionModel hotelSa in hotelSas)
              {      
            %>
           <li>
             <span class="spanSubround"><%= hotelSa.name %></span><%=hotelSa.distances %>公里
           </li>
           <%} %>
         </ul>
         
                 <% if (hotelScs.Count > 0)
             { %>
         <h3>商业区</h3>
         <%} %>
         <ul>
           <% foreach (HotelSurroundingCommerceModel hotelSc in hotelScs)
              {      
            %>
           <li>
             <%= hotelSc.name %>
           </li>
           <%} %>
         </ul>
         
                 <% if (hotelSrs.Count > 0)
          { %>
        <h3>餐厅</h3>
        <%} %>
         <ul>
           <% foreach (HotelSurroundingRestaurantModel hotelSr in hotelSrs)
              {      
            %>
           <li>
             <%=hotelSr.name %>
           </li>
           <%} %>
         </ul>
         
                 <% if (hotelSss.Count > 0)
                    { %>
                 <h3>商场</h3>
                 <%} %>
         <ul>
           <% foreach (HotelSurroundingShopModel hotelSs in hotelSss)
              {      
            %>
           <li>
             <%=hotelSs.name %>
           </li>
           <%} %>
         </ul>
         </div>
         
         
         <script type="text/javascript">

             var _hotelSubroundMap;;

             $(document).ready(function() {

                  _hotelSubroundMap=new BMap.Map("divSubroundMap");
                     BMap.Convertor.translate(new BMap.Point(<%= hotelDetail.lon %>,<%= hotelDetail.lat %>), 2, function(point) {
                   
                   //hotelMap.centerAndZoom(point,16);
                   //hotelSubroundMap.centerAndZoom(point,16);
                   
                   
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
                          var infoWindow = new BMap.InfoWindow(content, infoWindowOpts); 
                       marker.addEventListener("click", function() {
                             this.openInfoWindow(infoWindow);
                         });
                   _hotelSubroundMap.addOverlay(marker);
                   
                   
                   
                   _hotelSubroundMap.centerAndZoom(point,16);
                    marker.openInfoWindow(infoWindow);
                    
                    
                    var geocoder = new BMap.Geocoder();
                    // 将地址解析结果显示在地图上,并调整地图视野
                    $("#tabs-subround").find(".spanSubround").click(function(){
                    
                      var name=$.trim($(this).text());
                      
                          geocoder.getPoint(name, function(point){
                              if (point) {
                                _hotelSubroundMap.centerAndZoom(point, 15);
                                var offset=new BMap.Size(-25,25);
                                var label = new BMap.Label(name, { position: point, offset: offset });
                                var m=new BMap.Marker(point);
                                m.setLabel(label);
                                _hotelSubroundMap.addOverlay(m);
                                
                              }
                          }, "<%=geo.cityName %>");
                      });
                    });
                    
             });  //$(document).ready
         
         </script>