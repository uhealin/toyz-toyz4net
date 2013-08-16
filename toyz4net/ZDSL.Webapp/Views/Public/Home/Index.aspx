<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= PublicBiz.getCurPageSeo().homeIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%  string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); 
    
   

   
    IList<HotelCommentModel> listHotelComment = ViewData[typeof(HotelCommentModel).Name] as IList<HotelCommentModel>;

    
    %>
    		<div class="left" class="b" style=" width:65.5%">
			
			
			<div  class="b" style=" height:125px" >
			 <form method="get" action="<%=basePath %>/Public/Hotel/Search" id="formHotelSearch">
			    <% Html.RenderPartial("HotelSearchBox"); %>
			    </form>
			</div>
			
			<div style=" height:320px; margin-top:5px; overflow:visible" >
			   <div class="left" style="height:inherit; width:35%; overflow-x: hidden ">
                   <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
			   </div>
			   <div class="right"  style="height:inherit; width:64.5%; ">
			      <% Html.RenderPartial("DivCityTopHotelTabs"); %>
			      
			   </div>
			</div>
			
		   <div style=" height:250px; margin-top:5px">
			   <div class="left b" style="height:inherit; width:35%;">
			      
			      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
			      
			      <h3 >新闻热点</h3>
			      <div class="box">
                  <% Html.RenderPartial("UlHotNews"); %>
                  </div>
			   </div>
			   <div class="right b" style="height:inherit; width:64.0%">
			      <h3>城市热门商业区</h3>
			      <div style=" height:218px; overflow:hidden" id="divCityHotClsList" >
			         
			      </div>
			   </div>
			</div>
	
		
		</div>	
		<div style=" width:33.5%" class="right">
			
		    
			<div style=" height:125px; margin-bottom:5px; " class="b " >
			
			  <% Html.RenderPartial("DivAdSider2"); %>
			 
			</div>
            
            <div style=" height:320px; " class="b">
            <h3>酒店点评排行</h3>
			<div class="box">
			   <% Html.RenderPartial("UlHotCommentHotel"); %>
			</div>
			</div>
			
            <div style=" height:250px; margin-top:8px; overflow:hidden " >
                <% Html.RenderPartial("DivHotBrand"); %>
			</div>
		</div>	


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">



<style  type="text/css">
  
   .divBrand
   {
   	 width:21.4%;
   	 height:38px;
   	}
   	
   .divBrand img
   {
   	
   	 max-height:100%;
   	}
   	
   .divCityTopHotel
   {
    width:48.9%;
    margin-right:0.5%;	
    height:50px;
   }
   
   	.spanBrandName
	{
		 display:none;
		
		}


</style>


			      <script type="text/javascript">

			          $(document).ready(function() {

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

			          });             //$(document)

			          
			      
			      </script>


</asp:Content>
