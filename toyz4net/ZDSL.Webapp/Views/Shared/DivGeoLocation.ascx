<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<%    
       string basePath = WebUtil.GetWebRootPath();
       GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel;
       IList<GeoCommercialLocationModel> geoCls = ViewData[BaseZdController.VD_KEY_GEO_CLS] as IList<GeoCommercialLocationModel>;
       IList<GeoDistrictsModel> geoDs = ViewData[BaseZdController.VD_KEY_GEO_DS] as IList<GeoDistrictsModel>;
       IList<GeoLandmarkLocationModel> geoLls = ViewData[BaseZdController.VD_KEY_GEO_LLS] as IList<GeoLandmarkLocationModel>;
       string strCityName = geo.cityName;
     %>

          <div class="divTabs">
            <ul>
              <li><a href="#GeoD"><%=geo.cityName %>行政区</a></li>
              <li><a href="#GeoCl"><%=geo.cityName %>商业区</a></li>
            </ul>
            
            <div id="GeoD" style=" overflow:auto">
                      <ul>
           
           <% foreach (GeoDistrictsModel geoD in geoDs)
              {
                  string url = HotelController.GetPathDHotel(geo, geoD);
                  %>
           <li class="liCondition ignore" style=" float:left; margin-left:1%;"><span class="spanGeoD" code="<%=geoD.locationId %>" ><a href="<%=url %>" title="<%=geo.cityName+geoD.name %>酒店"><%=geoD.name %></a></span></li>
           <%} %>
         </ul>
            </div>
            
            <div id="GeoCl" style=" overflow:auto">
                             <ul>
           
           <% foreach (GeoCommercialLocationModel geoCl in geoCls)
              {
                  string url = HotelController.GetPathClHotel(geo, geoCl);
           %>
           <li class="liCondition ignore" style="float:left; margin-left:1%;"><span class="spanGeoCl" code="<%=geoCl.locationId %>" ><a href="<%=url %>" title="<%=geo.cityName+geoCl.name %>酒店"><%=geoCl.name %></a></span></li>
           <%} %>
         </ul>
          
            
            </div>
            
          </div>



         
         
         <script type="text/javascript" >
             _condition["cityName"] = "<%=geo.cityName %>";
            
             $(document).ready(function() {

/*
                 $(".spanGeoCl").click(function() {
                     var text = $(this).text();
                     var code = $(this).attr("code");
                     _condition["geoDId"] = "";
                     _condition["geoLlId"] = "";
                     _condition["geoClId"] = code;
                     _condition["geoMode"] = "";
                     ToUrl("<%=basePath %>/Public/Hotel/Search", _condition);

                 });

                 $(".spanGeoD").click(function() {
                     var text = $(this).text();
                     var code = $(this).attr("code");
                     _condition["geoClId"] = "";
                     _condition["geoLlId"] = "";
                     _condition["geoDId"] = code;
                     _condition["geoMode"] = "";
                     ToUrl("<%=basePath %>/Public/Hotel/Search", _condition);
                 });
*/
                 /*
                 $(".spanGeoLl").click(function() {
                 var text = $(this).text();
                 var code = $(this).attr("code");
                 _condition["geoDId"] = "";
                 _condition["geoClId"] = "";
                 _condition["geoLlId"] = code;
                 ToUrl("<%=basePath %>/Public/Hotel/Search", _condition);
                 });
                 */
             });
           
           
         </script>