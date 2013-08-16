<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.NorthBoundAPIService" %>

<%   
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    PriceHotel[] priceHotels = ViewData[typeof(PriceHotel).Name] as PriceHotel[];  
    PriceRoomType[] priceRoomTypes=priceHotels[0].Rooms;
    
    %>
<table>
   <thead>
      <tr>
         <td>房型</td>
         <td>产品类型</td>
         <td>零售价</td>
         <td></td>
      </tr>
   </thead>
   
   <tbody>
   <%foreach (PriceRoomType room in priceRoomTypes)
     { %>
      <tr>
         <td rowspan="<%=room.RatePlans.Length %>">  <%= room.RoomTypeName  %></td>
         <% for (int i = 0; i < room.RatePlans.Length; i++)
         { %>
                <%if (i == 0)
                  { %>
                  <td>
                    <%=room.RatePlans[i].RatePlanName%>
                    
                  </td>
                  <td>
                    <%=room.RatePlans[i].Rates[0].RetailRate%>
                  </td>
                  
                  <td>
                     <a href="<%=basePath %>/Public/Hotel/Order?hotelId=" >预订</a>
                  </td>
                  </tr>
                <% }
                  else
                  {%>
                  <tr>
                   <td><%=room.RatePlans[i].RatePlanName%> </td>
                   <td>
                    <%=room.RatePlans[i].Rates[0].RetailRate%>
                  </td>
                                    <td>
                     <a href="<%=basePath %>/Public/Hotel/CreateOrder?hotelId=" >预订</a>
                  </td>
                  </tr>
                <%} %>
         <%} %>
     
      

    <%} %>
   </tbody>
</table>
