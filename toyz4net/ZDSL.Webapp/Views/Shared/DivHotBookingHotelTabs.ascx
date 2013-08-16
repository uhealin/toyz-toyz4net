<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>
<% 
    string basePath = WebUtil.GetWebRootPath();

    IList<HotelModel> weekHotels = ViewData[BaseZdController.VD_KEY_HOT_WEEK_BOOKING_HOTEL_LIST] as IList<HotelModel>;
    IList<HotelModel> monthHotels = ViewData[BaseZdController.VD_KEY_HOT_MONTH_BOOKING_HOTEL_LIST] as IList<HotelModel>;
    GeoBiz geoBiz = GeoBiz.GetInstant();
%>


			      <div  class="divTabs" style=" padding:2px;"   >
			        <ul>
			           <li><a href="#top_month">本月排行</a></li>
			           <li><a href="#top_week">本周排行</a></li>
			        </ul>
			        <div id="top_month" style=" overflow:hidden;padding:2px;">
			        <ul style=" padding-left:3px" >
			             <%  int i = 1;
                    foreach (HotelModel hotel in monthHotels)
                             {
                                 HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotel.hotelId);
                                 if (hotelDetail == null) continue;
                                 string imgUrl = BaseZdController.GetTopThumImgUrl(hotelDetail.id);
                                 GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
                                 string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
                                 string cityUrl = HotelController.GetPathCityHotel(geo);
                          %>
 
                         
                            <li class="ignore bottomline" style=" margin-top:5px;"><%=i++  %>
                            
                            <span >[<a  href="<%=cityUrl %>" title="<%=geo.cityName %>酒店" > <%=geo.cityName %> </a> ]</span>  
                            <span><a target="_blank" href="<%=hotelUrl %>" title="<%=hotelDetail.name %>" > <%=hotelDetail.name%> </a> </span>
                            
                            </li>
                         
			            <%} %>
			            </ul>
			        </div>
			        <div id="top_week" style=" overflow:hidden;padding:2px;">
			        
		        <ul style=" padding-left:3px" >
			             <%  
                             i = 1;
                             foreach (HotelModel hotel in weekHotels)
                             {
                                 HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotel.hotelId);
                                 if (hotelDetail == null) continue;
                                 GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
                                 string imgUrl = BaseZdController.GetTopThumImgUrl(hotelDetail.id);
                                 string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
                                 string cityUrl = HotelController.GetPathCityHotel(geo);
                          %>

                         
                            <li class="ignore bottomline" style=" margin-top:5px;"><%=i++  %>
                            
                            <span >[<a  href="<%=cityUrl %>" title="<%=geo.cityName %>酒店" > <%=geo.cityName %> </a> ]</span> 
                            <span><a target="_blank" href="<%=hotelUrl %>" title="<%=hotelDetail.name %>" > <%=hotelDetail.name %> </a></span></li>
                         
			            <%} %>
			            </ul>
			        
			        </div>
			      </div>