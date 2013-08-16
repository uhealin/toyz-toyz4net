<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
   <%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <% 
       BrandModel brand = ViewData[typeof(BrandModel).Name] as BrandModel;
        %>
	<%=string.Format(PublicBiz.getCurPageSeo().brandHotelTitle,brand.brandID,brand.brandNameLong,brand.brandName,brand.brandPinYin) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



<%
    string basePath = WebUtil.GetWebRootPath();
    IList<HotelDetailModel> hotelDetails = ViewData[typeof(HotelDetailModel).Name] as IList<HotelDetailModel>;
    BrandModel brand = ViewData[typeof(BrandModel).Name] as BrandModel;

    Dictionary<string, IList<HotelDetailModel>> dictGeosHotels = new Dictionary<string, IList<HotelDetailModel>>();
    GeoBiz geoBiz=GeoBiz.GetInstant();
    foreach (HotelDetailModel hotelDetail in hotelDetails) {
        if (!dictGeosHotels.ContainsKey(hotelDetail.city))
        {
            dictGeosHotels.Add(hotelDetail.city, new List<HotelDetailModel>());    
        }
        dictGeosHotels[hotelDetail.city].Add(hotelDetail);
    }
    string homeUrl = HomeController.GetPathIndex();
    string brandUrl = BrandController.GetPathIndex();
    string brandHotelUrl = BrandController.GetPathBrandHotel(brand.brandID);
  %>
  
  <div class="left" style=" width:70%">
           <div class="box b" style=" margin-bottom:3px">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="连锁品牌" href="<%=brandUrl %>" >连锁品牌</a> >> </li>
              <li><%=brand.brandNameLong %>  </li>
           </ul>
          
        </div>
      <div class="box b" style=" margin-bottom:3px">
         <div class="left" style=" width:20%">
           <img src="<%=brand.picURL %>" style=" width:80px; border-width:3px" class="box"  />
           <br />
           <a href="<%=basePath %>/Public/Brand/Index">更多连锁品牌</a>
         </div>
         <div class="right" style=" width:79%" >
           <ul>
              <li><span>品牌名:</span><%= brand.brandNameLong %></li>
              <li><span>酒店数:</span><%= brand.hotelCount %></li> 
              <li><span>地区分布:</span>
                 <ul>
                <% foreach (string key in dictGeosHotels.Keys)
                   {  %>
                   <li style=" float:left; margin-right:5px"><a href="<%=brandHotelUrl+"#"+key %>"><%= geoBiz.GetGeoByCityId(key).cityName%></a> </li>
                <%
                    }
                %>
                </ul>
              </li>
           </ul>
           
          
         </div>
         
         
      </div>
      
     <div class="b">
     <%foreach (string key in dictGeosHotels.Keys)
     { %>
      <div>
         <h3><a name="<%=key %>"> <%= geoBiz.GetGeoByCityId(key).cityName%></a></h3>
	                   <% foreach (HotelDetailModel hotelDetail in dictGeosHotels[key])
                       {
                           string imgUrl = BaseZdController.GetTopNormalImgUrl(hotelDetail.id);
                           GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
                           string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
                           %>
                         <div  class="b divBrandHotelDetail" >
                           <div class="left" style=" width:35%; height:99%">
                               <img src="<%= imgUrl %>" alt="<%=hotelDetail.name %>" title="<%=hotelDetail.name %>"  style=" max-height:100%; max-width:100%" />
                           </div>
                           
                           <div class="right" style=" width:64%; height:99%; " >
                              
                              <span ><a target="_blank" href="<%=hotelUrl %>" title="<%=hotelDetail.name %>" > <%=hotelDetail.name %> </a> </span> <span class="divStar star<%=hotelDetail.star %>"></span>
                              <br/>
                              <span ><%=hotelDetail.address %></span>
                              
                           </div>
                            
                         </div>
	                     
	                   <%} %>
      </div>
     <%} %>
     </div>
  </div>
  
  <div class="right" style=" width:29%">
      <div class="b" style=" margin-bottom:3px">
      <h3>快速搜索</h3>
      <div class="box">
        <% Html.RenderPartial("HotelQuickSearchBox"); %>
      </div>
      </div>
      
              <div class="b" style=" margin-bottom:3px">
        		      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
        </div>
          			<div  >
                   <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
			   </div>
  </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <style>
  
   .divBrandHotelDetail
   {
   	 width:49%;
   	 float:left;
   	  height:80px;
   	 margin-right:3px;
   	 margin-top:3px;
   	}
  
  </style>

</asp:Content>
