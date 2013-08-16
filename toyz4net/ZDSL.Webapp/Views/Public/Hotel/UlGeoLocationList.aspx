<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import  Namespace="ZDSL.Model.Data"%>

<%
    string typeName = ViewData["typeName"].ToString();
    IList<GeoLocationModel> locations = ViewData[typeof(GeoLocationModel).Name] as IList<GeoLocationModel>;
     %>
     <ul>
           <li><%=typeName %>：</li>
           <% foreach (GeoLocationModel location in locations)
           { %>
           <li class="liGeo"><span class="spanGeoCl" id="<%=location.locationId %>" ><%=location.name%></span></li>
           <%} %>
    </ul>
         
