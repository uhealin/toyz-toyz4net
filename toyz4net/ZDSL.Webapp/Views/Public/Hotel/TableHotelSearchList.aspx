<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Toyz4net.Core.NorthBoundAPIService" %>
<%@ Import Namespace="Toyz4net.Core.Service" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<%   
    string basePath = WebUtil.GetWebRootPath();
    PageList<HotelForGetHotelList> pageList = ViewData[typeof(PageList<HotelForGetHotelList>).Name] as PageList<HotelForGetHotelList>;
    Dictionary<string, HotelDetailModel> distHotelDetails = ViewData[typeof(Dictionary<string, HotelDetailModel>).Name] as Dictionary<string, HotelDetailModel>;
    Dictionary<string, HotelFeatrueInfoModel> dictHotelFeat = ViewData[typeof(HotelFeatrueInfoModel).Name] as Dictionary<string, HotelFeatrueInfoModel>;
    string strCheckInDate = WebUtil.GetParamAttr("checkInDate",DateTime.Now.ToString("yyyy-MM-dd"));
    string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
    ToyzNumberRangeObject priceRange = ViewData[typeof(ToyzNumberRangeObject).Name] as ToyzNumberRangeObject;
%>
    <div class="ui-state-highlight ui-corner-all">
    <span id="spanTotal" total="<%=pageList.total %>">共找到酒店：<%=pageList.total %></span>
    </div>
<table   style=" width:100%">

   <% foreach(HotelForGetHotelList hotel in pageList ){
          HotelDetailModel detail = null;
          HotelFeatrueInfoModel featrueInfo = null;
          try
          {
              detail = distHotelDetails[hotel.HotelId];

          }
          catch (Exception ex) { continue; }
          try {
              featrueInfo = dictHotelFeat[hotel.HotelId];
          }catch(Exception ex){
             featrueInfo = new HotelFeatrueInfoModel();
          }
          string imgUrl = ZDSL.Webapp.Controllers.BaseZdController.GetTopNormalImgUrl(hotel.HotelId);
          RoomForGetHotelList[] rooms = hotel.Rooms.OrderBy(p => p.RatePlans[0].Rates.TotalPrice).ToArray();
          if (rooms.Count() == 0) continue;
          string hotelUrl = HotelController.GetPathDetail(hotel.HotelId);
          string parmaPrefix = "";
          if (hotelUrl.Contains("?"))
          {
              parmaPrefix = "&";
          }
          else
          {
              parmaPrefix = "?";
          }
        
    %>
   <tr class="bottomline">
     <td style=" vertical-align:top;">
      <div class="b box" style=" padding:2px; margin-top:1%;">
       <img   src="<%=imgUrl %>" style=" width:100px; height:80px" />
      </div>
     </td>
     <td style="padding:1%;"> <span style=" font-size:1.3em; font-weight:bolder" class="spanHotel"
      lat="<%=detail.lat %>" lon="<%=detail.lon %>" hotelId="<%=detail.id %>" imgUrl="<%=imgUrl %>"
     url="<%=hotelUrl+parmaPrefix %>checkInDate=<%=strCheckInDate %>&checkOutDate=<%=strCheckOutDate %>"
     hotelName="<%= hotel.HotelName %>" address="<%=detail.address %>"  price="<%= NumberUtil.Format(hotel.LowestPrice,0) %>"
     >
     <a href="<%=hotelUrl+parmaPrefix %>checkInDate=<%=strCheckInDate %>&checkOutDate=<%=strCheckOutDate %>" target="_blank">
     <%=hotel.HotelName %></a> <div class="divStar star<%=hotel.StarCode %>" ></div></span> 
      <br/><%=hotel.HotelAddress %>
      <br /><%=featrueInfo.overview %>
      <table   style=" width:100%">
        <tr class="bottomline">
           <td style=" width:25%">房型</td>
      
           <td style=" width:15%">早餐</td>
           <td style=" width:10%">日均价</td>
           <td style=" width:15%">是否有房</td>
           <td style=" width:15%"></td>
        </tr>
        
        <% for (int i = 0,j=0; j<3&&i < rooms.Count();i++)
        {
            RoomForGetHotelList room = rooms[i];
            if (priceRange != null) {
               int rate =Convert.ToInt32(room.RatePlans[0].Rates.rates[0].MemberRate);
               if (!priceRange.isBetween(rate)) continue;
            }
            j++;
           
        %>
        <tr >
           <td><%=room.RoomName %></td>
           <td><%=room.RatePlans[0].RatePlanName %></td>
           <td><%=NumberUtil.Format(room.RatePlans[0].Rates.rates[0].MemberRate,0) %></td>
           <td><%=ELongApiService.GetTextRoomInvStatusCode(room.RoomInvStatusCode)   %></td>
           <!--
           <td>
               <% if (room.RoomInvStatusCode == ELongApiService.ROOM_IVN_STATUE_CODE_NORMAL)
                  { %>
                <form action="<%=basePath %>/Public/Hotel/OrderEdit" method="get">
                        <input type="hidden" name="hotelId" value="<%=hotel.HotelId %>" />
                        <input type="hidden" name="roomTypeId" value="<%=room.RoomTypeId %>" />
                        <input type="hidden" name="ratePlanId" value="<%=room.RatePlans[0].RatePlanID %>" />
                        <input type="hidden" name="checkInDate" value="<%=strCheckInDate %>" />
                        <input type="hidden" name="checkOutDate" value="<%=strCheckOutDate %>" />
                       <button type="submit">预订</button>
                </form>
                <%} %>
           </td>
           -->
           <td>
                <form action="<%=hotelUrl %>" method="get">
                        <input type="hidden" name="hotelId" value="<%=hotel.HotelId %>" />
                        <input type="hidden" name="checkInDate" value="<%=strCheckInDate %>" />
                        <input type="hidden" name="checkOutDate" value="<%=strCheckOutDate %>" />
                       <button type="submit">查看</button>
                </form>
           </td>
        </tr>
        
        <%} %>
      </table> 
     </td>
     
   
   </tr>
   
   <%} %>
</table>

<script type"text/javascript">

    $(document).ready(function() {

        $("button").button();

    });
  
</script>
