<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<%  
    string basePath = WebUtil.GetWebRootPath();
    PageList<OrderModel> orders = ViewData[typeof(OrderModel).Name] as PageList<OrderModel>;
%>
<table style=" width:100%">

<tr>
<th class="order_th" align="center">订单号</th>
<th >提交时间</th>
<th >酒店名</th>
<th >入住时间</th>
<th >订单状态</th>
<th >备注</th>
<th>操作</th>
</tr>


<% foreach(OrderModel order in orders ){  %>
<tr>
<td><%=order.id   %></td>
<td><%=order.createDate.ToShortDateString() %></td>
<td><a href="<%=basePath %>/Public/Hotel/Detail?hotelId=<%=order.hotelId %>"><%=order.hotelName %></a> </td>
<td><%=order.checkInDate.ToShortDateString() %></td>
<td><%=OrderModel.GetDictOrderStatus()[order.orderStatus] %></td>
<td><%= order.remark %></td>
<td>
     <% if (order.orderStatus == OrderModel.ORDER_STATUS_NEW)
     { %>
          <a href="<%=basePath %>/MemberAdmin/Order/DoCancel?orderId=<%=order.id %>">取消</a>
     <%}
        else if (order.orderStatus == OrderModel.ORDER_STATUS_CANCEL)
        { %>
        
         <a href="<%=basePath %>/MemberAdmin/Order/DoRenew?orderId=<%=order.id %>">还原</a>
     <%}
        else if (order.orderStatus == OrderModel.ORDER_STATUS_DEAL)
        { 
            %>
         <button  type="button" onclick="DoComment('<%=order.id %>')">发表点评</button>
     <%} %>
</td>

</tr>


<%} %>


</table>

