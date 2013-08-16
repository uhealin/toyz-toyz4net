<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%
        string basePath = WebUtil.GetWebRootPath();
        JsResultObject re = ViewData[typeof(JsResultObject).Name] as JsResultObject;
        OrderModel order = re.attrs[typeof(OrderModel).Name] as OrderModel;
	%>
	订单<%=order.id %>提交结果
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <%  
       string basePath = WebUtil.GetWebRootPath();
       JsResultObject re = ViewData[typeof(JsResultObject).Name] as JsResultObject;
       OrderModel order = re.attrs[typeof(OrderModel).Name] as OrderModel;
       HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(order.hotelId);
       GeoBiz geoBiz = GeoBiz.GetInstant();
       GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
       GeoDistrictsModel geoD = BaseZdBiz.Load<GeoDistrictsModel>(Restrictions.Eq("geoFk",geo.id),Restrictions.Eq("locationId",hotelDetail.district));
       GeoCommercialLocationModel geoCl = BaseZdBiz.Load<GeoCommercialLocationModel>(Restrictions.Eq("geoFk", geo.id), Restrictions.Eq("locationId", hotelDetail.businessZone));
       string hotelUrl = HotelController.GetPathDetail(hotelDetail.id);
       MemberModel member = WebUtil.GetSessionAttr<MemberModel>(typeof(MemberModel).Name);

       string homeUrl = HomeController.GetPathIndex();
       string cityUrl = HotelController.GetPathCityHotel(geo);
       HotelRoomModel hotelRoom = ViewData[typeof(HotelRoomModel).Name] as HotelRoomModel;
    %>
    

      
     <div class="left b" style=" width:70%;" >
     
                     <div class="box">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="<%=geo.cityName %>酒店预订" href="<%=cityUrl %>" ><%=geo.cityName %>酒店</a> >> </li>
               <li><a title="<%=hotelDetail.name %>酒店预订" href="<%=hotelUrl %>" ><%=hotelDetail.name %>酒店</a> >> </li>
             <li><a title="<%=hotelDetail.name+hotelRoom.roomName %>预订" href="<%=basePath %>/Public/Hotel/OrderEdit?hotelId=<%=hotelDetail.id  %>&roomTypeId=<%=order.roomTypeId %>&ratePlanId=<%=order.ratePlanId %>" ><%=hotelRoom.roomName %> </a> 预订结果  </li>
           </ul>
          
        </div>
     
            <h3>订单提交结果</h3>
            <div  style="padding:10%">
                      <%if (re.code == JsResultObject.CODE_SUCCESS)
         { %>
         <div class="ui-state-highlight ui-corner-all" id="divMsg" style=" padding:0.7em">
         <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
           
           <span>提交成功，订单号：<%=order.id%> 我们将已最快速度为你处理</span>
             <% if (member == null)
                {
                    WebUtil.SetSessionAttr(typeof(OrderModel).Name, order);
             %> 
           <br/><span>马上&nbsp;<a href="<%=basePath %>/Public/Home/Login">登录</a>&nbsp;或&nbsp;<a  href="<%=basePath %>/MemberAdmin/Reg/Index" >注册会员</a>，入住后可获得本订单的奖励</span>  
             <%} %>
            
           </div>
       <%}
         else
         {  %>
         <div class="ui-state-error ui-corner-all" id="div1" style=" padding:0.7em">
         <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
           <span>提交失败，失败原因：<%=re.msg %></span> 
           </div>
       <%} %>
       <div>
         <ol type="1">
 
           <li>
                  查看 &nbsp;<a href="<%=hotelUrl %>"><%= hotelDetail.name %> </a>&nbsp; 的更多房间
           </li>
           <li>
            	    查看&nbsp;<a href="<%=basePath %>/Public/Hotel/Search?cityName=<%=geo.cityName %>" ><%= geo.cityName %></a>&nbsp;
	             <a href="<%=basePath %>/Public/Hotel/Search?cityName=<%=geo.cityName %>&geoDId=<%=geoD.locationId %>"><%= geoD.name %></a> &nbsp; 
	             <a href="<%=basePath %>/Public/Hotel/Search?cityName=<%=geo.cityName %>&geoClId=<%=geoCl.locationId %>"><%= geoCl.name %></a>&nbsp; 的更多酒店
           </li>

         </ol>
       </div>
            </div>
     </div>
      
       <div class="right b"  style=" width:29%">
       <h3>快速搜索</h3>
       <div class="box">
       <% Html.RenderPartial("HotelQuickSearchBox"); %>
       </div>
       
    </div>
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <style>
   .divAlertMsg{
     display:none;
  }
 </style>

</asp:Content>
