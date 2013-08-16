<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.NorthBoundAPIService" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>

<%   
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    //PriceHotel[] priceHotels = ViewData[typeof(PriceHotel).Name] as PriceHotel[];  
    HotelForGetHotelList hotel = ViewData[typeof(HotelForGetHotelList).Name] as HotelForGetHotelList;
   
    
    //PriceRoomType[] priceRoomTypes=priceHotels[0].Rooms;
    RoomForGetHotelList[] priceRoomTypes = hotel.Rooms; 
    
    
    IList<HotelRoomModel> hotelRooms = ViewData[typeof(HotelRoomModel).Name] as IList<HotelRoomModel>;
    string checkInDate = ViewData["checkInDate"].ToString();
    string checkOutDate = ViewData["checkOutDate"].ToString();
    ToyzRangeDateObject toyzDateRange = DateTimeUtil.ParseToyzDateTime(string.Format("range@[{0}],[{1}]",checkInDate,checkOutDate)) as ToyzRangeDateObject;
    int dayCount = toyzDateRange.days.Count - 1;
    DataBiz dataBiz = DataBiz.GetInstant();
    MemberRewardRuleModel rewardRule= dataBiz.GetCurRewardRule();
    
    %>

 
 <script type="text/javascript">

       var dates={};
         
 
         
      function initDates(){
               dates={};
              <%        for(int i=0;i<dayCount;++i)
              {   DateTime dt=toyzDateRange.getDiffDay(i);
             %>
            dates["<%=dt.ToString("yyyy-MM-dd") %>"]="满房";
         <%} %>
      
      }   
         
     
     var divRate = $("<div></div>");
     
     
    function drawRatesDialog(strRates) {

          initDates();
         
         var rates = strRates.split(',');
         var totalPrice=0;
         for (var i=0;i<rates.length;i++) {
             
             var rate=rates[i]; 
            // var strRateDate = rate.split('@')[0];
             var rateDate=rate.split('@')[0];
             var ratePrice = parseInt(rate.split('@')[1]);
            

                 if(dates[rateDate]){
                 dates[rateDate]=ratePrice;
                 }
   
             
         }
         
         
         divRate.empty();
         var ul = $("<ul></ul>");
         for(var key in dates){
            var dt=Date.parse(key);
            var li=$("<li></li>");
            li.css("float","left");
            var span=$("<span></span>");
            var dt=Date.parse(key);
            span.text(GetDayOfWeekCn(dt));
            li.append(span).append("<br/>").append(dates[key]);
            ul.append(li);
         }
         divRate.append(ul);
         //divRate.append("<hr />").append("总价格:"+totalPrice);
         //divRate.dialog();
         
     }
     
     $(document).ready(function() {

/*
      $("#tableHotelRoomPriceInfo").find(".spanRoomInfo").hover(
         function() {
             $(this).parent().parent().nextAll(".trHotelRoomDetail:eq(0)").slideDown("slow");
            // $(this).attr("src", "<%=basePath %>/image/icon/003_52.png");
                $(this).qtip({
                                 content: context,
                                 show: 'mouseover',
                                 hide: 'mouseout',
                                 target: 'bottomMiddle'

                 });
         }
         , function() {
         $(this).parent().parent().nextAll(".trHotelRoomDetail:eq(0)").slideUp("fast");
        // $(this).attr("src", "<%=basePath %>/image/icon/003_50.png");
         }
         );
  */      
  
        $("#tableHotelRoomPriceInfo").find(".spanRoomInfo").each(
         function() {
            var context= $(this).parent().find(".divHotelRoomDetail:eq(0)").html();
            // $(this).attr("src", "<%=basePath %>/image/icon/003_52.png");
                $(this).qtip({
                                 content: context,
                                 show: 'mouseover',
                                 hide: 'mouseout'
                                 
                                 ,style: { 
                                 width: 400
                                 }


                 });
         }
         ); 
         
         $("button").button();
         

               $("#tableHotelRoomPriceInfo").find(".spanRate").each(function() {
                               var valRates = $(this).attr("rates")||"";
                               if(valRates=="") return true;
                              
                                drawRatesDialog(valRates);
                               var context = divRate.html();

                                $(this).qtip({
                                 content: context,
                                 show: 'mouseover',
                                 hide: 'mouseout',
                                 position: {
                              corner: {
                                 target: 'bottomLeft'
                                 //,tooltip: 'bottomLeft'
                              }
}

                             });
               });
               
               $("#spanRewardAlert").qtip({
                  content: $("#divRewardAlert").html(),
                                 show: 'mouseover',
                                 hide: 'mouseout',
                                 style:{
                                   name:"cream"
                                 }
                                , position: {
                              corner: {
                                 target: 'bottomLeft'
                                 //,tooltip: 'bottomLeft'
                              }
                              }
               });
               
     });         //$(document).ready(function()
 
 </script>   
 
 <div id="divRewardAlert" style=" display:none">
    
    <ol type="1">
       <li>通过网上下单预订酒店并且入住后，返回中国商旅网并对你入住的酒店发表点评，既可获得点评奖金</li>
       <li>每次的点评奖金暂存入你的会员账户，累计金额达到<%=rewardRule.rebateLimit %>元时，即可申请提现</li>
    </ol>
 
 </div>
 
<table id="tableHotelRoomPriceInfo" border="0"  style=" width:100%">
   <thead>
      <tr style=" background-color: #66CCFF">
         <td >房型</td>
         <td >床型</td>
         <td >价格选项</td>
         <td>优惠价</td>
         <td><span id="spanRewardAlert">点评奖励？</span></td>
         <td></td>
      </tr>
   </thead>
   
   <tbody>
   
   <%  foreach (HotelRoomModel hotelRoom in hotelRooms)
      {

          RatePlanForGetHotelList[] ratePlans = new RatePlanForGetHotelList[] { };
          foreach (RoomForGetHotelList priceRoomType in priceRoomTypes)
          {
              if (priceRoomType.RoomTypeId == hotelRoom.roomTypeId) {
                  ratePlans = priceRoomType.RatePlans.OrderBy(p=>p.Rates.rates[0].MemberRate).ToArray(); 
                  break;
              }
          }
          if ( ratePlans.Count()==0) continue;
          
   %>
       
        <tr class="trRoomTypeName">
         <td class="tdRoomTypeName" rowspan="<%=ratePlans.Length==0?1:ratePlans.Length  %>" > 
         <!--
           <img src="<%=basePath %>/image/icon/003_50.png" class="imgShowRoomInfo" style=" width:16px; margin-right:5px" alt="<%= hotelRoom.roomName %>" /> 
           -->
           <%= hotelRoom.roomName %>  <span  class="spanRoomInfo">详情</span>
             <div class="divHotelRoomDetail" style=" display:none">
             <table style=" table-layout:auto;"  cellpadding="3" >
                  <tr>
                      <td >床型：<%=hotelRoom.bedType%></td>
                      <td>房间面积：<%= hotelRoom.area%></td>
                      <td>楼层：<%= hotelRoom.floor%> </td>
                  </tr>
                  <tr>
                      <td style="width:400px"  colspan="4">简介：<%=hotelRoom.bedDescription%></td>
                  </tr>
                   <tr>
                      <td style="width:400px"  colspan="4">备注：<%=hotelRoom.note%></td>
                  </tr>
               </table>
               </div>
          </td>
         <td class="tdRoomTypeName" rowspan="<%=ratePlans.Length==0?1:ratePlans.Length  %>">  <%=hotelRoom.bedType  %></td>
         <% if (ratePlans.Length == 0)
            { %>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
             <td>&nbsp;</td>
         <%} %>
         <% 
           
         for (int i = 0; i < ratePlans.Length; i++)
         { %>
                <%if (i == 0)
                  { %>
                  <td>
                    <%=ratePlans[i].RatePlanName%>

                  </td>
                  <!--
                  <td style=" text-decoration:line-through">
                     
   
         
                     <span class="spanRate"> ¥<%= NumberUtil.Format(ratePlans[i].Rates[0].RetailRate,0)%></span>
                    
                  </td>
                  -->
                 
               
                  
                  <% 
                     string strRates = "";
                     int rateDaysCount = 0;
                       foreach (RateForGetHotelList rate in ratePlans[i].Rates.rates) {
                           
                           ToyzRangeDateObject range = new ToyzRangeDateObject(rate..StartDate.Value, rate.EndDate.Value);
                           rateDaysCount += range.days.Count;
                           foreach (DateTime dt in range.days)
                           {
                               strRates += dt.ToString("yyyy-MM-dd");
                               strRates += "@" + Convert.ToInt32(rate.MemberRate) + ",";
                           }
                           
                           
                           
                       }
                       strRates = strRates.TrimEnd(',');
                      
                       int rewardAmount = rewardRule.getRewardAmount(Convert.ToInt32( ratePlans[i].Rates[0].MemberRate));
                    %>
                   <td >
                   <span class="spanRate"   rates="<%=strRates %>" > ¥<%= NumberUtil.Format(ratePlans[i].Rates[0].MemberRate,0)%></span>
                   </td>
                   <td>
                    <span    > ¥<%=rewardAmount%></span> 
                    
                  </td>
                                  

                  
                  <td>
                   <% if (dayCount <= rateDaysCount)
                      { %>
                     <form action="<%=basePath %>/Public/Hotel/OrderEdit" method="get">
                        <input type="hidden" name="hotelId" value="<%=hotelRoom.hotelFk %>" />
                        <input type="hidden" name="roomTypeId" value="<%=hotelRoom.roomTypeId %>" />
                        <input type="hidden" name="ratePlanId" value="<%=ratePlans[i].RatePlanID %>" />
                        <input type="hidden" name="checkInDate" value="<%=checkInDate %>" />
                        <input type="hidden" name="checkOutDate" value="<%=checkOutDate %>" />
                       <button type="submit">预订</button>
                     </form>
                     <% }
                      else
                      {%>
                       <button type="button" disabled="disabled">满房</button>
                     <%} %>
                     
                  </td>
                  </tr>
                <% }
                  else
                  {%>
                  <tr>
                   <td><%=ratePlans[i].RatePlanName%> </td>
                   <!--
                   <td style=" text-decoration:line-through">
   
         
                     <span class="spanRate"> ¥<%= NumberUtil.Format(ratePlans[i].Rates[0].RetailRate,0)%></span>
                  </td>
                  -->
               
                                      <% 
                     string strRates = "";
                     int rateDaysCount = 0;
                       foreach (Rate rate in ratePlans[i].Rates) {
                          
                           ToyzRangeDateObject range = new ToyzRangeDateObject(rate.StartDate.Value, rate.EndDate.Value);
                           rateDaysCount += range.days.Count;
                           foreach(DateTime dt in range.days){
                               strRates+=dt.ToString("yyyy-MM-dd");
                               strRates += "@" + Convert.ToInt32(rate.MemberRate) + ",";
                           }
                           
                          
                       }
                       strRates = strRates.TrimEnd(',');
                       int rewardAmount = rewardRule.getRewardAmount(Convert.ToInt32( ratePlans[i].Rates[0].MemberRate));
                    %>
                   <td >
                   <span class="spanRate"   rates="<%=strRates %>" > ¥<%= NumberUtil.Format(ratePlans[i].Rates[0].MemberRate,0)%></span>
                   </td>
                   <td>
                   <span    > ¥<%=rewardAmount %></span>
                  </td>
                  <td>
                    <% if (dayCount <= rateDaysCount)
                       { %>
                     <form action="<%=basePath %>/Public/Hotel/OrderEdit" method="get">
                        <input type="hidden" name="hotelId" value="<%=hotelRoom.hotelFk %>" />
                        <input type="hidden" name="roomTypeId" value="<%=hotelRoom.roomTypeId %>" />
                        <input type="hidden" name="ratePlanId" value="<%=ratePlans[i].RatePlanID %>" />
                        <input type="hidden" name="checkInDate" value="<%=checkInDate %>" />
                        <input type="hidden" name="checkOutDate" value="<%=checkOutDate %>" />
                       <button type="submit">预订</button>
                     </form>
                     <%}
                       else
                       { %>
                       <button type="button" disabled="disabled">满房</button>
                     <%} %>
                  </td>
                  </tr>
                <%} %>
         <%} %>
        
   <%} %>
   
   
    
   </tbody>
</table>
