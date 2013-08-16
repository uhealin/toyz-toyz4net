<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<% 
    string basePath = WebUtil.GetWebRootPath();
    IList<HotelModel> hotels = ViewData[BaseZdController.VD_KEY_HOT_COMMENT_HOTEL_LIST] as IList<HotelModel>;
    GeoBiz geoBiz = GeoBiz.GetInstant();
%>


		        <ul style=" padding-left:3px" >
			             <%   
                             int i = 1;
			                 foreach ( HotelModel hotel in hotels)
                             {
                                 HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotel.hotelId);
                                 if (hotelDetail == null) continue;
                                 GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
                                 string imgUrl = BaseZdController.GetTopThumImgUrl(hotelDetail.id);
                                 string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
                                 string cityUrl = HotelController.GetPathCityHotel(geo);
                          %>

                         
                            <li class="ignore bottomline" style=" margin-top:5px;"><%=i++  %>
                            
                            <span >[<a title="<%=geo.cityName %>酒店"  href="<%=cityUrl %>" > <%=geo.cityName %> </a> ]</span> 
                            <span> <a target="_blank" href="<%=hotelUrl %>" title="<%=hotelDetail.name %>" > <%=hotelDetail.name %> </a></span> 
                            
                            </li>
                         
			            <%} %>
			            </ul>