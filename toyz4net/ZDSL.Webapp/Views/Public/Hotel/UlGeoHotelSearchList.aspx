<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.NorthBoundAPIService" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<%
    string basePath = WebUtil.GetWebRootPath();
    PageList<HotelForGetHotelList> pageList = ViewData[typeof(PageList<HotelForGetHotelList>).Name] as PageList<HotelForGetHotelList>;
    Dictionary<string, HotelDetailModel> dictHotelDetails = ViewData[typeof(Dictionary<string, HotelDetailModel>).Name] as Dictionary<string, HotelDetailModel>;
    DateTime today = DateTime.Now;
    DateTime tomorrow = today.AddDays(1);
    string strCheckInDate = WebUtil.GetParamAttr("checkInDate", today.ToString("yyyy-MM-dd"));
    string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", tomorrow.ToString("yyyy-MM-dd"));
     %>


<span class="ui-state-highlight ui-corner-all" id="inputTotal" total="<%=pageList.total %>">共找到酒店：<%=pageList.total %></span>
<ul>
 <%  int i = 1;
     foreach (HotelForGetHotelList hotel in pageList)
    {
        HotelDetailModel detail = null;
        try
        {
           detail = dictHotelDetails[hotel.HotelId];
           
        }
        catch (Exception ex) { continue; }
        string imgUrl = ZDSL.Webapp.Controllers.BaseZdController.GetTopNormalImgUrl(detail.id);
        string thumUrl = ZDSL.Webapp.Controllers.BaseZdController.GetTopThumImgUrl(detail.id);
        string hotelUrl = HotelController.GetPathDetail(hotel.HotelId);
        string parmaPrefix = "";
        if (hotelUrl.Contains("?"))
        {
            parmaPrefix = "&";
        }
        else {
            parmaPrefix = "?";
        }
        %>
   <li class="liHotel" lat="<%=detail.lat %>" lon="<%=detail.lon %>" hotelId="<%=detail.id %>" imgUrl="<%=imgUrl %>"
     url="<%=hotelUrl+parmaPrefix %>checkInDate=<%=strCheckInDate %>&checkOutDate=<%=strCheckOutDate %>"
     hotelName="<%= hotel.HotelName %>" address="<%=detail.address %>"  price="<%= NumberUtil.Format(hotel.LowestPrice,0) %>"
   > 
      <div class="ignore divHotelName" > <span class="spanHotelName" style=" overflow:hidden">&nbsp;<%=i++ %>.&nbsp;
      <a href="<%=hotelUrl+parmaPrefix %>checkInDate=<%=strCheckInDate %>&checkOutDate=<%=strCheckOutDate %>"><%= hotel.HotelName %></a>
      </div>
      <div class="ignore"><span class="spanHotelAddress" >地址:<%=hotel.HotelAddress %></span></div>
      <div class="divHotelPrice" > 最低价:<span class="spanHotelPrice" ><%=NumberUtil.Format(hotel.LowestPrice,0) %></span>
      </div>
      <div class="divStar star<%=hotel.StarCode %>" ></div>
      
   </li>
   <%} %>
</ul>


<style>
  .liHotel
  {
  	   clear:both;
  	}
</style>