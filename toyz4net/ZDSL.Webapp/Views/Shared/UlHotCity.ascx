<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>



<% 
    string basePath = WebUtil.GetWebRootPath();
    IList<GeoModel> geos = ViewData[BaseZdController.VD_KEY_HOT_CITY] as IList<GeoModel>;
%>


			        <ul>
			          <%foreach (GeoModel geo in geos)
                      {
                          string url = HotelController.GetPathCityHotel(geo);
                   %>
                       <li class="ignore liHotCity"><a title="<%=geo.cityName %>酒店" href="<%=url %>"><%=geo.cityName %></a></li>
			          <%} %>
			        </ul>
			        
			        <style>
			            .liHotCity
   {
   	  float:left;
   	  margin-right:15px;
   	}
			        </style>