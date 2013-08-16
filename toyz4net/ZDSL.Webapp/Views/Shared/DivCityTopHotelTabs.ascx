<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<% 
    string basePath =WebUtil.GetWebRootPath();
    Dictionary<string, IList<HotelDetailModel>> dictCityHotel = ViewData[BaseZdController.VD_KEY_CITY_HOT_HOTEL_DICT] as Dictionary<string, IList<HotelDetailModel>>;
    Dictionary<string, IList<GeoCommercialLocationModel>> dictCityCls = ViewData[BaseZdController.VD_KEY_CITY_HOT_GEO_CLS_DICT] as Dictionary<string, IList<GeoCommercialLocationModel>>;
    GeoBiz geoBiz=GeoBiz.GetInstant();
 %>

<div style=" padding:2px;"  id="divCityTopHotelTabs">
			        <ul>
			        <%  int index = 0;
			            foreach (string key in dictCityHotel.Keys)
                    {
                        
                     %>
	                <li><a href="#tabs-<%=index++ %>"><%=key %></a></li>
	                <%} %>
	                </ul>
	                
	                <%  index=0; 
	                    foreach (string key in dictCityHotel.Keys)
                    {
                        
                     %>
	                <div  style="  overflow:hidden" id="tabs-<%=index++ %>" >
	                   <% foreach (HotelDetailModel hotelDetail in dictCityHotel[key])
                       {
                           string imgUrl = BaseZdController.GetTopThumImgUrl(hotelDetail.id);
                           GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
                           %>
                         <div  class="b divCityTopHotel" >
                           <div class="left" style=" width:35%; height:99%; overflow:hidden">
                               <img src="<%= imgUrl %>" alt="<%=hotelDetail.name %>" title="<%=hotelDetail.name %>"  style=" height:100%; width:100%" />
                           </div>
                           
                           <div class="right ignore" style=" width:64%; height:99%; " >
                              
                              <span ><a target="_blank"  title="<%=hotelDetail.name %>"  href="<%=HotelController.GetPathDetail(hotelDetail.id) %>" > <%=hotelDetail.name %> </a> </span> 
                              <br/>
                              <div class="divStar star<%=hotelDetail.star %>"></div>
                           </div>
                            
                         </div>
	                     
	          
	                   <%} %>
	                   <div style=" display:none" id="divCityGeoCl-<%=index %>">
	                      <ul>
	                      <% foreach(GeoCommercialLocationModel geoCl in dictCityCls[key]){
                              GeoModel geo=BaseZdBiz.Load<GeoModel>(geoCl.geoFk);
                              string url = HotelController.GetPathClHotel(geo, geoCl);
                              %>
	                       <li class="liCondition ignore" style=" float:left"><span style=" padding-right:5px"><a title="<%=geoCl.name %>酒店" href="<%=url %>">  <%=geoCl.name %> </a></span></li>  
	                      <%} %>
	                      </ul>
	                   </div>
	                </div>
	                <%} %>
	                 
			      </div>
			      
