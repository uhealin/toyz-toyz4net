<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>

<%    
       string basePath = WebUtil.GetWebRootPath();
       GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel;
       IList<GeoCommercialLocationModel> geoCls = ViewData[BaseZdController.VD_KEY_GEO_CLS] as IList<GeoCommercialLocationModel>;
       IList<GeoDistrictsModel> geoDs = ViewData[BaseZdController.VD_KEY_GEO_DS] as IList<GeoDistrictsModel>;
       IList<GeoLandmarkLocationModel> geoLls = ViewData[BaseZdController.VD_KEY_GEO_LLS] as IList<GeoLandmarkLocationModel>;
       string strCityName = geo.cityName;
     %>
    
     <ul>
     <li class="liCondition" style=" float:left; margin-left:1%;"><span class="spanGeoD" qn="" code="none" >不限</span></li>
      </ul>
        <div style=" clear:both"></div>
       <div id="GeoD" style=" overflow:auto">
       <h3><%=geo.cityName %> 行政区</h3>
                      <ul>
           
           <% foreach (GeoDistrictsModel geoD in geoDs)
              {
                  %>
           <li class="liCondition" style=" float:left; margin-left:1%;"><span class="spanGeoD" qn="geoDId" code="<%=geoD.locationId %>" ><%=geoD.name %></span></li>
           <%} %>
         </ul>
            </div>
            
            <div id="GeoCl" style=" overflow:auto">
            
            <h3><%=geo.cityName %> 商业区</h3>
            
                             <ul>
           
           <% foreach (GeoCommercialLocationModel geoCl in geoCls)
              {
                  
           %>
           <li class="liCondition" style="float:left; margin-left:1%;"><span class="spanGeoCl" qn="geoClId" code="<%=geoCl.locationId %>" ><%=geoCl.name%></span></li>
           <%} %>
         </ul>
          
            
            </div>
            
            
            
           <script type="text/javascript">

               $(document).ready(function() {

                   $(".liCondition").click(function() {

                       
                       var qn = $(this).find("span:first").attr("qn");
                       var code = $(this).find("span:first").attr("code");
                       var text = $(this).find("span:first").text();

                       $("#inputGeoLocation").val("");
                       _condition["geoDId"] = "";
                       _condition["geoClId"] = "";
                       $("#formHotelSearch").find("input[name=geoDId]").val("");
                       $("#formHotelSearch").find("input[name=geoClId]").val("");
                       if (code != "none") {
                           $("#inputGeoLocation").val(text);
                           _condition[qn] = code;
                           $("#formHotelSearch").find("input[name=" + qn + "]").val(code);
                       }
                       $("#inputGeoLocation").qtip("hide");
                   });

               });
           
           </script>