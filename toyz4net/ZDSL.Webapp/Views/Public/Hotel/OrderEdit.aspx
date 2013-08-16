<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.NorthBoundAPIService" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%
        string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
        HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
        HotelRoomModel hotelRoom = ViewData[typeof(HotelRoomModel).Name] as HotelRoomModel;   
	        %>
	 <%="预订"+hotelDetail.name+hotelRoom.roomName %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();   
        HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
        HotelRoomModel hotelRoom = ViewData[typeof(HotelRoomModel).Name] as HotelRoomModel;
        RatePlanForGetHotelList ratePlan = ViewData[typeof(RatePlanForGetHotelList).Name] as RatePlanForGetHotelList;
        RatesForGetHotelList rates = ratePlan.Rates;
        string checkInDate = ViewData["checkInDate"] as string;
        string checkOutDate = ViewData["checkOutDate"] as string;
        ToyzRangeDateObject toyzDateRange = DateTimeUtil.ParseToyzDateTime(string.Format("range@[{0}],[{1}]", checkInDate, checkOutDate)) as ToyzRangeDateObject;
        int dayCount = toyzDateRange.days.Count - 1;
        bool isBook = true;
        MemberModel member = WebUtil.GetSessionAttr<MemberModel>(typeof(MemberModel).Name);
        if (member == null) {
            member = new MemberModel();
        }
        GeoBiz geoBiz = GeoBiz.GetInstant();
        GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
        string homeUrl = HomeController.GetPathIndex();
        string cityUrl = HotelController.GetPathCityHotel(geo);
        string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
        
    %>
    <div   class="right b" style=" width:74.0% "  >
    
                <div class="box">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="<%=geo.cityName %>酒店预订" href="<%=cityUrl %>" ><%=geo.cityName %>酒店</a> >> </li>
               <li><a title="<%=hotelDetail.name %>酒店预订" href="<%=hotelUrl %>" ><%=hotelDetail.name %>酒店</a> >> </li>
             <li><a title="<%=hotelDetail.name+hotelRoom.roomName %>预订" href="<%= Request.Url.ToString() %>" ><%=hotelRoom.roomName %> 预订信息</a>  </li>
           </ul>
          
        </div>
    
      <form method="post" action="<%=basePath %>/Public/Hotel/DoSubmitOrder" id="formOrder">
        <input type="hidden" name="hotelId" value="<%=hotelRoom.hotelFk %>" />
        <input type="hidden" name="hotelName" value="<%= hotelDetail.name  %>" />
        <input type="hidden" name="roomTypeId" value="<%=hotelRoom.roomTypeId %>" />
        <input type="hidden" name="roomName" value="<%=hotelRoom.roomName %>" />
        <input type="hidden" name="ratePlanName" value="<%=ratePlan.RatePlanName %>" />
        <input type="hidden" name="ratePlanId" value="<%=ratePlan.RatePlanID %>" />
        <input type="hidden" name="ratePlanCode" value="<%=ratePlan.RatePlanCode %>" />
        <input type="hidden" name="totalPrice" value="<%=rates.TotalPrice %>" />
        
      <div  >
       <h3>客房信息</h3>
         <div class="box">
         <table >
            <tr>
              <th>酒店：</th>
              <td><a href="<%=basePath %>/Public/Hotel/Detail?hotelId=<%=hotelDetail.id %>&checkInDate=<%=checkInDate %>&checkOutDate=<%=checkOutDate %>"><%=hotelDetail.name %></a></td>
            </tr>
            <tr>
              <th>房型名称：</th>
              <td><%=hotelRoom.roomName %></td>
            </tr>
            
             <tr>
              <th>房间数量：</th>
              <td>
                <select name="roomAmount" onchange="var num=parseInt(this.value);onChangRoomNum(num);">
                  <option value="1"  selected="selected">1</option>
                  <option value="2" >2</option>
                  <option value="3" >3</option>
                  <option value="4" >4</option>
                  <option value="5" >5</option>
                </select>
              </td>
            </tr>
            
            <tr>
              <th style="  vertical-align:top">住店时间：</th>
              <td >
                 <div id="divOrderRangeDate">
                       
	                   <input name="checkInDate" value="<%=checkInDate %>" /> &nbsp; - &nbsp; 
	                   <input name="checkOutDate" value="<%=checkOutDate %>" />
	                   <button type="button"  onclick="updateRatePlan()" >刷新价格</button>
	                   
	             </div>
	             
	             <div >
	               <ul>
	                  <% 
                          for (int i = 0; i < dayCount; i++)
                          {
                              DateTime dt = toyzDateRange.days.ElementAt(i);
                              try
                              {
                                  RateForGetHotelList rate = rates.rates[i];
                              
                       %>
                          <li style=" float:left; margin-right:4%;" class="b" > <%= DateTimeUtil.FormateWeekDateCn(dt, "周{0}")%><br /><%=NumberUtil.Format(rate.MemberRate, 0)%></li>
                       <%
                          }
                              catch (Exception ex) {
                                  isBook= false;
                                  %>
                              <li style=" float:left; margin-right:4%;" class="b" > <%= DateTimeUtil.FormateWeekDateCn(dt, "周{0}")%><br />满房</li>
                              
                              <% 
                              
                              }
                          } %>
	               </ul>
	             </div>
              </td>
            </tr>
            
            <% if (isBook)
               { %>
            <tr>
              <th>应付金额：</th>
              <td>
              <div class="ui-state-highlight ui-corner-all" id="divTotalPrice" style=" padding:0.7em; font-size:1.3em; font-weight:bolder">
              1*<%=NumberUtil.Format(rates.TotalPrice, 0)%>=<%=NumberUtil.Format(rates.TotalPrice, 0)%>
              </div>
              </td>
              
            </tr>
            <%} %>
         </table>
         </div>
        </div>
        
                  <% if (isBook)
                     { %>
      <div>
        <h3>入住信息</h3>
        <div class="box">
        <table>
          <tr>
            <th>客人姓名：</th>
            <td>
              
                 <input name="guestsNameArray" style=" width:300px"  value="<%=member.realName  %>" nullmsg="请住客姓名！" />(多个客人姓名之间请使用英文逗号,隔开)
              
            </td>
          </tr>

          <tr>
            <th>联系手机：</th>
            <td>
               <input name="mobile" style=" width:200px" value="<%=member.moblie %>" datatype="m"  nullmsg="请输入手机号码！" errormsg="请输入有效的手机号码！" />
               <div style=" display:inline" class="Validform_checktip">请正确填写您的手机号码，以便接收预订确认短信。</div>
            </td>
            
          </tr>
          <tr>
            <th>E-mail：</th>
            <td>
               <input name="email" style=" width:200px" value="<%=member.email %>" />
            </td>
          </tr>
          
         <tr>
            <th>房间保留时间：</th>
            <td>
              <div class="radios">
                <input type="radio"  checked="checked" value="a" name="arraivalLastTimeType" id="cbA" onclick="$('#divMsg').text('房间保留至当日18:00，如不能在18:00前到酒店，请及时通知捷达商旅或与酒店联系。');">
                <label for="cbA">18:00</label>
                <input type="radio" value="b" name="arraivalLastTimeType" id="cbB" onclick="$('#divMsg').text('房间保留至次日6:00，当日14:00 前免费取消订单，担保金额会立即退回。');">
                <label for="cbB">次日06：00 </label>
              </div>
            </td>
          </tr>
          
          
          <tr>
             <th></th>
             <td>
               
                 <div class="ui-state-highlight ui-corner-all" id="divMsg" style=" padding:0.7em">
                   房间保留至当日18:00，如不能在18:00前到酒店，请及时通知艺龙或与酒店联系。
                 </div>
            
             </td>
          </tr>
        </table>
        </div>
        
        <%}
                     else
                     { %>
                         <div class="ui-state-error ui-corner-all" id="div1" style=" padding:0.7em">
                   部分时间房满，请重新选择预订日期
                 </div>
        <%} %>
      </div>
      
      <div style=" text-align:center">
         <button type="submit">提交订单</button>
      </div>
      </form>
    
    </div>
    
    
    <div  class="left" style=" width:25% ">
           
           <div class="b" style=" margin-bottom:3px">
            <h3><%=hotelDetail.name%></h3>
              <div class="box">
             <% Html.RenderPartial("UlHotelBaseInfo"); %>
              </div>
           </div>
           
           <div class="b" style=" margin-top:5px">
            <h3>快速搜索</h3>
            <div class="box">
           <% Html.RenderPartial("HotelQuickSearchBox"); %>
           </div>
           </div>
           
     </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
   <%
       string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       RatePlanForGetHotelList ratePlan = ViewData[typeof(RatePlanForGetHotelList).Name] as RatePlanForGetHotelList;
       RatesForGetHotelList rates = ratePlan.Rates;

     %>
    <script type="text/javascript">

        var singlePrice = parseFloat("<%=rates.TotalPrice %>");
        
        
               
        

        function updateHotelPrice() {
            var valCheckInDate = $("#divOrderRangeDate").find("input[name=checkInDate]").val();
            var valCheckOutDate = $("#divOrderRangeDate").find("input[name=checkOutDate]").val();

        }


        function onChangRoomNum(num) { 
            
            // $('#divGuestNames').empty();
             //for(var i=0;i<num;i++){
             //    var input = $("<input />");
              //   input.attr("name", "guestsNameArray")
            //     $('#divGuestNames').append(input).append("&nbsp;");
             //}
             var totalPrice=num*singlePrice; 
             $("input[name=totalPrice]").val(totalPrice);
             $("#divTotalPrice").text(num+"*"+singlePrice+"="+totalPrice);

         }

         function updateRatePlan() {
             
             var hotelId=$("input[name=hotelId]").val();
             var roomTypeId=$("input[name=roomTypeId]").val();
             var ratePlanId=$("input[name=ratePlanId]").val();
             var checkInDate = $("input[name=checkInDate]").val();
             var checkOutDate = $("input[name=checkOutDate]").val();
             var param = $.param({
             hotelId: hotelId
             , roomTypeId: roomTypeId
             , ratePlanId: ratePlanId
             , checkInDate: checkInDate
             , checkOutDate: checkOutDate
         });
         document.location.search = param;
         }


         $(document).ready(function() {
             SetHotelConditionForm("#tableHotelQuickSearch");
             SetHotelConditionForm("#divOrderRangeDate");

             SetPreUrl();




             $("#formOrder").Validform({
                 tiptype: function(msg, o, cssctl) {
                     //msg：提示信息;
                     //o:{obj:*,type:*}, obj指向的是当前验证的表单元素（或表单对象），type指示提示的状态，值为1、2、3、4， 1：正在检测/提交数据，2：通过验证，3：验证失败，4：提示ignore状态;
                     //cssctl:内置的提示信息样式控制函数，该函数需传入两个参数：显示提示信息的对象 和 当前提示的状态（既形参o中的type）;
                     if (!o.obj.is("form")) {//验证表单元素时o.obj为该表单元素，全部验证通过提交表单时o.obj为该表单对象;
                         var objtip = o.obj.parent().next().find(".Validform_checktip");
                         cssctl(objtip, o.type);
                         objtip.text(msg);
                     } else {
                         var objtip = o.obj.find("#msgdemo");
                         cssctl(objtip, o.type);
                         objtip.text(msg);
                     }
                 },
                 datatype: {//传入自定义datatype类型【方式二】;
                     "z2-4": /^[\u4E00-\u9FA5\uf900-\ufa2d]{2,4}$/,
                     "*6-20": /^[^\s]{6,20}$/,
                     "n0-12": /^[a-z,0-9]{0,12}$/

                 },
                 // ajaxPost: true,
                 postonce: true
             });
             
             
             
         });   //$(docment)
    
    </script>
    
</asp:Content>
