<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>


<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%
        HotelModel hotel = ViewData[typeof(HotelModel).Name] as HotelModel;
        GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel;
        HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;    
	        %>
	<%=string.Format( PublicBiz.getCurPageSeo().hotelDetailTitle,hotel.hotelId,hotel.hotelName,hotelDetail.address,hotelDetail.description,geo.cityCode,geo.cityName) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% 
       string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
       HotelModel hotel = ViewData[typeof(HotelModel).Name] as HotelModel;
       HotelFeatrueInfoModel hotelFi = ViewData[typeof(HotelFeatrueInfoModel).Name] as HotelFeatrueInfoModel;
       GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel; 
       IList<HotelImageModel> hotelImages = ViewData[typeof(HotelImageModel).Name] as IList<HotelImageModel>;
      IList<HotelTrafficInfoModel> hotelTrs = ViewData[typeof(HotelTrafficInfoModel).Name] as IList<HotelTrafficInfoModel>;
       IList<HotelCommentModel> hotelComments = ViewData[typeof(HotelCommentModel).Name] as IList<HotelCommentModel>;
       GeoCommercialLocationModel hotelGeoCl = null;
       GeoDistrictsModel hotelGeoD = null;
       IList<GeoCommercialLocationModel> geoCls = ViewData[BaseZdController.VD_KEY_GEO_CLS] as IList<GeoCommercialLocationModel>;
       IList<GeoDistrictsModel> geoDs = ViewData[BaseZdController.VD_KEY_GEO_DS] as IList<GeoDistrictsModel>;
       string imgUrl = BaseZdController.GetTopNormalImgUrl(hotelDetail.id);
       string locationDUrl = "";
       string locationClUrl = "";
       foreach (GeoCommercialLocationModel geoCl in geoCls) {
           if (geoCl.locationId == hotelDetail.businessZone) {
               hotelGeoCl = geoCl;
               locationClUrl = HotelController.GetPathClHotel(geo, hotelGeoCl);
               break;
           }
       }
       foreach (GeoDistrictsModel geoD in geoDs)
       {
           if (geoD.locationId == hotelDetail.district)
           {
               hotelGeoD = geoD;
               locationDUrl = HotelController.GetPathDHotel(geo, hotelGeoD);
               break;
           }
       }
       string homeUrl = HomeController.GetPathIndex();
       string cityUrl = HotelController.GetPathCityHotel(geo);
       string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
    %>
    <div   class="right b" style=" width:74.0% ; height:auto"  >
    
            <div class="box">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="<%=geo.cityName %>酒店预订" href="<%=cityUrl %>" ><%=geo.cityName %>酒店</a> >> </li>
               <li><a title="<%=hotelDetail.name %>酒店预订" href="<%=hotelUrl %>" ><%=hotelDetail.name %>酒店</a>  </li>
           </ul>
          
        </div>
    
    <div  style=" padding:2%;">
       <h2><%=hotelDetail.name %> <div class="divStar star<%=hotelDetail.star %>"></div></h2>
       <h2><%=hotel.hotelNameEn %></h2>
    </div>   
       
    <div id="tabs" style=" overflow:visible; height:auto;">
	    <ul>
		    <li><a href="#tabs-booking">客房预订</a></li>
		   <li><a href="#tabs-image">酒店图片</a></li> 
		    <!-- <li><a href="#tabs-descp">酒店介绍</a></li>-->
		    <li><a href="#tabs-comment">客人点评</a></li>
		    <li><a href="#tabs-traffic">交通地图</a></li>
		 
		    <li><a href="#tabs-nearby">周边酒店</a></li>
	    </ul>
	    <div id="tabs-booking"  >
	    
	        <div class="left " style=" width:57%; padding:1%">
                 <% Html.RenderPartial("UlHotelBaseInfo"); %>
	        </div>
        
        <div class="right"  style=" width:37%; ">
            <% Html.RenderPartial("DivHotelImageSider2"); %>
            <div style=" padding-right:10px; text-align:right;" class="right">
            <!--
            <a onclick="$('#tabs').tabs('select',1);ScrollTop()"> 更多酒店图片</a>
            -->
            </div> 
        </div>
        <!--
             <div class="right"  style=" width:47%; height:230px; padding:0.5%;border: 2px solid #6699FF;">

               <img style=" width:99%" id="imgHotelBmap" />
             </div>
           -->
           
  	        
	        <div style="clear:both" class="b" >
	        <h3>酒店介绍</h3>
	        <div class="box">
	         <pre><%= hotelDetail.introEditor %></pre>
	        </div>
	       
	       </div>
	       
	       
	       	
	<div  id="divOrderRangeDate" class="b"  style=" margin-top:5px">
	  <h3>房型与房价</h3>
	  <div style=" text-align:right">
	   入住日期：<input name="checkInDate"  />
	   退房日期：<input name="checkOutDate"  />
	   <button type="button" onclick="updateHotelPrice()" >修改日期</button>
	   </div>
	</div>
	<div id="divHotelPriceInfo"  class="b" style=" height:auto"></div>
	
	
	        
	         <div class="b"  style=" margin-top:5px">
	         <h3>相关信息</h3>
	           <ul>
	             <li><span>酒店服务设施:</span><%=hotelDetail.generalOverview %></li>
	             <!--
	             <li><span>房间服务设施:</span><%= hotelDetail.roomArray %></li>
	             -->
	             <li><span>休闲服务设施:</span><%=  hotelDetail.recreationOverview %></li>
	             <li><span>会议服务设施:</span><%=  hotelDetail.conferenceOverview %></li>
	             <li><span>餐饮服务设施:</span><%= hotelDetail.dinnerOverview %></li>
	             <li><span>信用卡</span><%=hotelDetail.ccAccepted %></li>
	           </ul>
	         </div>
	
	
	       
	        <div class="b"  style=" margin-top:5px">
	           <h3>相关评论</h3>
                <% Html.RenderPartial("TableHotelCommentList", hotelComments);%>
                <div style=" text-align:right; " class="box"><span class="pointer" onclick="$('#tabs').tabs('select',2);ScrollTop()">更多评论</span></div>
	        </div>
	       
	    </div>     
	    <div id="tabs-image" style=" overflow:auto" >
           
            <div >
              
<h3>图片观赏</h3>
                <% 
                       foreach (HotelImageModel hotelImage in hotelImages)
                       {
                           if (!hotelImage.isNormal() ) continue;
                    
                      %>
               <div style=" float:left; width:102px; height:110px; padding:0.5%; text-align:center"  ><a class="single_image" href="<%=hotelImage.imgUrl %>"><img class="imgThum" title="<%=hotelDetail.name+hotelImage.title %>" src="<%=hotelImage.imgUrl %>" alt="<%=hotelDetail.name+hotelImage.title %>"/></a>
                  <br/><%=hotelImage.title%>
                </div>
                
               
                <%
                   }%>
           
            </div>
            
            <h3>全景观赏</h3>
            <div >
              
                <%
                     
                  foreach (HotelImageModel hotelImage in hotelImages)
                  {
                      if (!hotelImage.isAround()) continue;
                      %>
               <div style=" float:left; width:102px; height:130px; padding:1%"  ><a class="single_image" href="<%=hotelImage.imgUrl %>"><img class="imgThum" title="<%=hotelDetail.name+hotelImage.title %>" src="<%=hotelImage.imgUrl %>" alt="<%=hotelDetail.name+hotelImage.title %>"/></a>
                  <br/><%=hotelImage.title %>
                </div>
                
                <%} %>
                
                
           
            </div>
            

	    </div>
<!--
	     <div id="tabs-image">
	   		   
			 
	   </div>
	   -->
	   
	   <div id="tabs-comment" style=" overflow:auto" >
	     
	      <div id="divHotelComment"></div>
          <div>
             <div class="left" style=" width:61%">
                 <% Html.RenderPartial("DivHotelCommentEdit"); %></div>
             <div class="right "  style=" width:38%; height:300px" >
                 <% Html.RenderPartial("DivSocietyLogin");  %>
             </div>
          </div>

	      
	   </div>
       <div id="tabs-traffic" >
            <% Html.RenderPartial("DivHotelTrafficMap"); %>
       </div>
 
       
        <div id="tabs-nearby" style=" overflow:auto">
            <% Html.RenderPartial("DivHotelNearbyMap"); %>
       </div>
	</div>
	



      
    </div>
     <div  class="left" style=" width:25% ">
        <div id="divHotelSearchBox" class="b" style=" margin-bottom:3px" >
          <h3>快速查询</h3>
          <% Html.RenderPartial("HotelQuickSearchBox"); %>
        </div>
        
        <div class="b" style=" margin-bottom:3px" >
        		      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
        </div>
        
        <div id="divGeoCondition">
          <% Html.RenderPartial("DivGeoLocation"); %>
        </div>
        
        
     </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

        <%
            
            string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
            HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
            HotelModel hotel = ViewData[typeof(HotelModel).Name] as HotelModel;
            GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel; 
            %>
            
            <style>
            #tabs-booking  li,#tabs-comment  li
              {
               border-bottom: 1px dashed #CCCCCC;	
              }
              
              .ulImages li
              {
                 float:left;
                 margin-left:5px;
              }
              
              .imgThum
              {
                 max-width:100px;
                 max-height:100px;
              }
              

            </style>
  <link rel="stylesheet" href="<%= basePath  %>/js3rd/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.css" type="text/css" />		
  
  <script type="text/javascript"  src="<%= basePath  %>/js3rd/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
 

 
<script type="text/javascript" src="http://api.map.baidu.com/api?v=1.2 " ></script>
<script type="text/javascript" src="http://dev.baidu.com/wiki/static/map/API/examples/script/convertor.js"></script>
        <script type="text/javascript">



           var valServiceScore=0;
           var valHygieneScore=0;
           var valFacilityScore=0;
           var valPriceScore=0;
           var hotelId="<%=hotelDetail.id %>";
           var hotelName = "<%=hotelDetail.name %>";
           _condition["cityName"] = "<%=geo.cityName %>";
          
           
            function updateHotelPrice (){
               var valCheckInDate=$("#divOrderRangeDate").find("input[name=checkInDate]").val();
               var valCheckOutDate=$("#divOrderRangeDate").find("input[name=checkOutDate]").val();
               StartPageWating();
                $("#divHotelPriceInfo").load("<%=basePath %>/Public/Hotel/TableHotelRoomPriceInfo"
                ,{hotelId:"<%=hotel.hotelId %>",checkInDate:valCheckInDate,checkOutDate:valCheckOutDate}
                ,function(){
                   EndPageWating();

         
                }
                );
            }

             function updateHotelComment(){
             $("#divHotelComment").empty();
            
                $("#divHotelComment").load("<%=basePath %>/Public/Comment/TableHotelCommentList"
                ,{hotelId:hotelId}
                ,function(){
                   EndPageWating();

         
                }
                );
            }


            $(document).ready(function() { //jQuery(window).load() must be used instead of jQuery(document).ready() because of Webkit compatibility


                $("#tabs").tabs({
                    collapsible: false
                 , panelTemplate: "<div style='overflow-y:auto;></div>"
                });

                //   var valToday = $.datepicker.formatDate($.datepicker._defaults.dateFormat, Date.today());
                //   var valTomorrow = $.datepicker.formatDate($.datepicker._defaults.dateFormat, Date.today().addDays(1));
                //  $("#divOrderRangeDate").find("input[name=checkInDate]").val(valToday);
                //   $("#divOrderRangeDate").find("input[name=checkOutDate]").val(valTomorrow);
                SetHotelConditionForm("#divOrderRangeDate");
                SetHotelConditionForm("#divHotelSearchBox");
                updateHotelPrice();
                updateHotelComment();

                SetPreUrl();











                $("a.single_image").fancybox();

                /* Using custom settings */

                $("a#inline").fancybox({
                    'hideOnContentClick': true
                });

                /* Apply fancybox to multiple items */

                $("a.group").fancybox({
                    'transitionIn': 'elastic',
                    'transitionOut': 'elastic',
                    'speedIn': 600,
                    'speedOut': 200,
                    'overlayShow': false
                });






            });    //$(document)
            
            
		</script>
        
        

</asp:Content>
