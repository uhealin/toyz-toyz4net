<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import  Namespace="Toyz4net.Core.Util"%>
<%@ Import  Namespace="ZDSL.Model.Data"%>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=PublicBiz.getCurPageSeo().hotelIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        string basePath = WebUtil.GetWebRootPath();
         IList<GeoModel> geos = ViewData[typeof(IList<GeoModel>).Name] as  IList<GeoModel>;
         //string strCheckInDate = DateTime.Now.ToString(DateTimeUtil.PATTERN_DB_DATE);
         //string strCheckOutDate = DateTime.Now.AddDays(1).ToString(DateTimeUtil.PATTERN_DB_DATE);
     %>
  
    <div class="left b" style=" width:74%">
       
        <div class="box">
        <form method="get" action="<%=basePath %>/Public/Hotel/Search" id="formHotelSearch">
          <% Html.RenderPartial("HotelSearchBox"); %>
          </form>
        </div>
        
        <div style=" overflow:visible">
           <% Html.RenderPartial("DivCityTopHotelTabs"); %>
        </div>
        <h3>城市热门商业区</h3>
	   <div style=" height:218px; overflow:hidden" id="divCityHotClsList" >
			         
			      </div>
	                
    </div>
    
    <div class="right" style=" width:25%">
      
              <div class="b" style=" margin-bottom:3px">
        		      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
        </div>
      
      			<div style=" margin-bottom:3px" >
                   <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
			   </div>
               <div >
                 
                 <% Html.RenderPartial("DivHotBrand"); %>
               </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  
   <script type="text/javascript">

       $(document).ready(function() {

       $("#divGeoLocation").tabs({
            event: "mouseover"
        });

        $("#divCityTopHotelTabs").tabs({

            event: "mouseover"
			              , select: function(event, ui)   // 初始化时绑定 select tab 事件
			              {
			                  //divCityHotClsList
			                  var i = ui.index + 1;
			                  var html = $("#divCityGeoCl-" + i).html();
			                  $("#divCityHotClsList").html(html);
			              }
                          , create: function(event, ui) {

                              var html = $("#divCityGeoCl-1").html();
                              $("#divCityHotClsList").html(html);
                          }

        });
        

       });    // $(document)
   
   </script>
   
   <style type="text/css">
   
   .divCityTopHotel
   {
   	 width:24%;
   	}
   
     .divBrand
{
	 width:29%;
	}
	

   
   </style>
  
</asp:Content>
