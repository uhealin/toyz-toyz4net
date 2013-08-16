<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<% 
    string basePath =WebUtil.GetWebRootPath();
    IList<HotelTrafficInfoModel> hotelTrs = ViewData[typeof(HotelTrafficInfoModel).Name] as IList<HotelTrafficInfoModel>;
    IList<HotelLandMarkModel> hotelLls = ViewData[typeof(HotelLandMarkModel).Name] as IList<HotelLandMarkModel>;
    IList<HotelSurroundingAttractionModel> hotelSas = ViewData[typeof(HotelSurroundingAttractionModel).Name] as IList<HotelSurroundingAttractionModel>;
    IList<HotelSurroundingCommerceModel> hotelScs = ViewData[typeof(HotelSurroundingCommerceModel).Name] as IList<HotelSurroundingCommerceModel>;
    IList<HotelSurroundingRestaurantModel> hotelSrs = ViewData[typeof(HotelSurroundingRestaurantModel).Name] as IList<HotelSurroundingRestaurantModel>;
    IList<HotelSurroundingShopModel> hotelSss = ViewData[typeof(HotelSurroundingShopModel).Name] as IList<HotelSurroundingShopModel>;
    HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
    
    string[] keywords = new string[] { "机场","火车站","地铁站","公交站"};
     %>
         
         <div  style=" overflow:auto" > 

            <div id="divHotelTrafficMap" style=" height:400px; border-width:2px"></div>
                    
                        <div class="b" >
                         <h3>交通信息</h3>
                        <!--
                               <input id="inputHotelTrafficKeyword" /> 
                 <button type="button"  onclick="var keyword=$('#inputHotelTrafficKeyword').val(); if(keyword=='')return; _hotelTrafficSearcher.search(keyword);">搜索</button>
                    -->
                    <div class="box">
<pre><%= hotelDetail.trafficOverview %></pre>
              <ul>
                <% if (hotelLls.Count() > 0)
                   { %>
                <li>
                  <span>周边地标：</span>
                  <% foreach (HotelLandMarkModel hotelLl in hotelLls)
                     {%>
                     <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelLl.landmarkName%> </span>
                  <%} %>
                </li>
                <%} %>
                
                                <% if (hotelSas.Count() > 0&&1==2)
                                   { %>
                
                                <li>
                  <span>周边景点：</span>
                  <% foreach (HotelSurroundingAttractionModel hotelSa in hotelSas)
                     {%>
                     <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelSa.name%> </span>
                  <%} %>
                </li>
                <%} %>
                
                                <% if (hotelScs.Count() > 0)
                                   { %>
                                <li>
                  <span>周边商业区：</span>
                  <% foreach (HotelSurroundingCommerceModel hotelSc in hotelScs)
                     {%>
                     <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelSc.name%> </span>
                  <%} %>
                </li>
                <%} %>
                
                                <% if (hotelSrs.Count() > 0 && 1 == 2)
                                   { %>
                                <li>
                  <span>周边餐厅：</span>
                  <% foreach (HotelSurroundingRestaurantModel hotelSr in hotelSrs)
                     {%>
                     <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelSr.name%> </span>
                  <%} %>
                </li>
                <%} %>
                
                                <% if (hotelSss.Count() > 0 && 1 == 2)
                                   { %>
                                <li>
                  <span>周边商店：</span>
                  <% foreach (HotelSurroundingShopModel hotelSs in hotelSss)
                     {%>
                     <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelSs.name%> </span>
                  <%} %>
                </li>
                <%} %>
                
                
                <li>
                  <span>周边交通：</span>
                                   <%foreach(string keyword in keywords){ %>
                   <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;"><%=keyword %></span>
                 <%} %>
                              |
                 <%foreach (HotelTrafficInfoModel hotelTr in  hotelTrs )
              { %>
              <span class="spanHotelTrafficKeyword pointer"  style="  margin-right:1em;">  <%= hotelTr.name %>  <%= hotelTr.distances %></span>
            <%} %>
                 </li>
              </ul>
                            

            </div>
              
              


   
            </div>
            
            <div id="divHotelTrafficList" style="font-size:13px;margin-top:10px;"></div>
            
         </div>
         
         
         <script type="text/javascript">

             var _hotelTrafficMap;
             var _hotelTrafficSearcher;
             $(document).ready(function() {

                 _hotelTrafficMap = new BMap.Map("divHotelTrafficMap");  
                 _hotelTrafficMap.addControl(new BMap.NavigationControl());
                 _hotelTrafficMap.enableScrollWheelZoom() ;
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
                       _hotelTrafficMap.addOverlay(marker);
                       _hotelTrafficMap.centerAndZoom(point,15);
                       marker.openInfoWindow(infoWindow);
                       
                       
                 }); 
                 
                 _hotelTrafficSearcher = new BMap.LocalSearch(_hotelTrafficMap, {
                  renderOptions:{map: _hotelTrafficMap,panel: "divHotelTrafficList"}
                 });
               //  local.search("小吃");
               
               $(".spanHotelTrafficKeyword").click(function(){
               
                 var keyword=$(this).text();
                 _hotelTrafficSearcher.search(keyword);
               
               });
                 
             });  //$(document).ready
         
         </script>