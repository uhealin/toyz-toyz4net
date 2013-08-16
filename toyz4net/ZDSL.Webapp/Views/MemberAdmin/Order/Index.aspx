<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网订单管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        string basePath = WebUtil.GetWebRootPath();
        string startDate = ViewData["startDate"] as string;
        string endDate = ViewData["endDate"] as string;
        Dictionary<string, string[]> dictOrderStatus =new Dictionary<string, string[]>();
        dictOrderStatus.Add("新订单", new string[] { OrderModel.ORDER_STATUS_NEW});
        dictOrderStatus.Add("预订成功", new string[] { OrderModel.ORDER_STATUS_ELONG_SUCCESS,OrderModel.ORDER_STATUS_ZD_SUCCESS });
        dictOrderStatus.Add("已成交", new string[] { OrderModel.ORDER_STATUS_DEAL });
        dictOrderStatus.Add("已点评", new string[] { OrderModel.ORDER_STATUS_COMMENTED });  
        dictOrderStatus.Add("已取消", new string[] { OrderModel.ORDER_STATUS_CANCEL });
        dictOrderStatus.Add("全部", new string[] { });
        IList<OrderModel> orders = ViewData[typeof(OrderModel).Name] as IList<OrderModel>;

        Dictionary<string, IList<OrderModel>> dictOrders = new Dictionary<string, IList<OrderModel>>();
        foreach (string key in dictOrderStatus.Keys)
        {
            string[] orderStatues = dictOrderStatus[key];
            IList<OrderModel> tempOrders = new List<OrderModel>();
            if (orderStatues.Length == 0)
            {
                tempOrders = orders;
            }
            else
            {
                foreach (string tempStatue in orderStatues)
                {
                    foreach (OrderModel order in orders)
                    {
                        if (order.orderStatus == tempStatue)
                        {
                            tempOrders.Add(order);
                        }
                    }
                }
            }
            dictOrders.Add(key,tempOrders);
        }
        string homeUrl = HomeController.GetPathIndex();
       
     %>
     
              
     
    <div class="left" style=" width:69%">
     
        <div  class="box b">
                   <ul id="ulNav">
           
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="订单管理" href="<%= Request.Url.ToString() %>" >订单管理</a>  </li>
           </ul>
           <br />
        <form action="<%=basePath %>/MemberAdmin/Order/Index"  method="post">
           <ul>
              <li class="liCondition"><a href="?dateRegexp=_week">本周</a></li>
              <li class="liCondition"><a href="?dateRegexp=_month">本月</a></li>
              
              <li><input name="startDate" value="<%=startDate %>" />&nbsp;~&nbsp;<input name="endDate" value="<%=endDate %>" />&nbsp;&nbsp;<button type="submit">查询订单</button></li>
           </ul>
          
           </form>
        </div>
        
        <div class="tabs">
        
          
          <ul>
          <% 
              int i = 0;
              foreach (string key in dictOrders.Keys)
          { %>
             <li><a href="#tab<%=i++ %>"><%=key %>(<%=dictOrders[key].Count() %>)</a></li>
          <%} %>
          

                   
          </ul>

          <% i = 0;

              foreach(string key in dictOrders.Keys){
                  IList<OrderModel> tempOrders = dictOrders[key];
          %>
             <div class="divOrderList"  status="" id="tab<%=i++ %>" style="  height:600px; overflow-y:scroll" >
                <table style=" width:100%">

                    <tr class="bottomline">
                    <td>订单号</td>
                    <td >提交时间</td>
                    <td >酒店名</td>
                    <td >入住时间</td>
                    <td >订单状态</td>
                    <td >总金额</td>
                    <td>操作</td>
                    </tr>
                    
                    
                    <% foreach(OrderModel order in tempOrders ){  %>
                        <tr>
                        <td><%=order.id   %>&nbsp;<span class="spanShowRemark pointer" href="javascript:void();" >详情</span>
                               <div class="divRemark" style=" display:none">
                                <table style=" table-layout:auto">
                                 <tr>
                                    <th>房型:</th>
                                    <td><%=order.roomName %></td>
                                  </tr>
                                <tr>
                                    <th>产品类型:</th>
                                    <td><%=order.ratePlanName %></td>
                                  </tr>
                                  <tr>
                                    <th>住客:</th>
                                    <td><%=order.guestsNameArray %></td>
                                  </tr>
                                  <tr>
                                    <th>联系手机:</th>
                                    <td><%=order.mobile %></td>
                                  </tr>
                                  <tr>
                                    <th>邮箱地址:</th>
                                    <td><%=order.email %></td>
                                  </tr>
                                  <tr>
                                    <th>备注:</th>
                                    <td colspan="3"><%=order.remark %></td>
                                  </tr>
                                </table>
                               
                               </div></td>
                        <td><%=order.createDate.ToShortDateString() %></td>
                        <td><a href="<%=basePath %>/Public/Hotel/Detail?hotelId=<%=order.hotelId %>"><%=order.hotelName %></a> </td>
                        <td><%=order.checkInDate.ToShortDateString() %></td>
                        <td><%=OrderModel.GetDictOrderStatus()[order.orderStatus] %></td>
                        <td><%=Convert.ToInt32(order.totalPrice) %></td>
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
             </div>
          <%} %>
                            
        
        </div>
    </div>
    
    <div class="right b"  style=" width:29%">
           <h3>会员信息</h3>
       <div class="box">
          <% Html.RenderPartial("DivMemberInfo"); %>
       </div>
       <h3>快速搜索</h3>
       <div class="box">
       <% Html.RenderPartial("HotelQuickSearchBox"); %>
       </div>
       
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

   <%
       string basePath = WebUtil.GetWebRootPath();
    %>
   <script>

       function DoComment(id) {

           var div = $("<div></div>");
           div.addClass("box");      
           var url = "<%=basePath %>/MemberAdmin/Order/DivMemberCommentEdit"
           
           div.empty().load(url, { orderId: id }, function() {
               div.dialog({
                   title: "订单点评:" + id
                 , modal: true
                 , resizable: false
                 
                 , width:500
               });
           })

       }

       $(document).ready(function() {
           SetPreUrl();
           $(".tabs").tabs();
           /*
           var url = "<%=basePath %>/MemberAdmin/Order/TableMemberOrder";
           $(".divOrderList").each(function() {
           var status = $(this).attr("status");
           $(this).load(url, { orderStatus: status });

           });
           */


           $("input[name=startDate],input[name=endDate]").datepicker({
               numberOfMonths: 2
              , showOn: "both"
             , buttonImage: _basePath + "/image/icon.calendar.gif"
             , buttonImageOnly: true
           });

           $(".spanShowRemark").each(function(i) {

               var context = $(this).parent().find(".divRemark").html();
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
           
       });
    
   </script>
   
   <style>
     .liCondition
     {
     	 float:left;
     	  margin-right:20px;
     	}
   </style>

</asp:Content>
