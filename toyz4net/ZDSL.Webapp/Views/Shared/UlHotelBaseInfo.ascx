<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<% 
    string basePath =WebUtil.GetWebRootPath();
    HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
    GeoBiz geoBiz = GeoBiz.GetInstant();
    GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
    GeoDistrictsModel hotelGeoD = BaseZdBiz.Load<GeoDistrictsModel>(Restrictions.Eq("geoFk",geo.id),Restrictions.Eq("locationId",hotelDetail.district));
    GeoCommercialLocationModel hotelGeoCl = BaseZdBiz.Load<GeoCommercialLocationModel>(Restrictions.Eq("geoFk", geo.id), Restrictions.Eq("locationId", hotelDetail.businessZone));
    string cityUrl = HotelController.GetPathCityHotel(geo);
    string dUrl = HotelController.GetPathDHotel(geo, hotelGeoD);
    string clUrl = HotelController.GetPathClHotel(geo, hotelGeoCl);
     %>


	           <ul>
	             <li><span>酒店地址:</span><%=hotelDetail.address %></li>
	             <li><span>预订电话:</span>020-61138986  85219620</li>	             
	             <li><span>酒店区域:</span>
	             <a title="<%=geo.cityName %>酒店" href="<%=cityUrl%>" ><%= geo.cityName %></a>&nbsp;
	             <a title="<%=geo.cityName+hotelGeoD.name %>酒店" href="<%=dUrl %>"><%= hotelGeoD.name %></a> &nbsp; 
	             <a title="<%=geo.cityName+hotelGeoCl.name %>酒店" href="<%=clUrl %>"><%= hotelGeoCl.name %></a></li>
	             <li><span>酒店地图:</span>
	              <%= hotelDetail.name %> &nbsp;在 &nbsp;<a href="<%=basePath %>/Public/Geo/Index?cityName=<%=geo.cityName %>" ><%= geo.cityName %>地图</a>&nbsp;的位置
	             </li>
	             <li><span>酒店描述:</span><%=hotelDetail.description %></li>
	            
	           </ul>