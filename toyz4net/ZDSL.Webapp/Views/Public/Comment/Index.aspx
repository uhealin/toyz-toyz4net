<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= PublicBiz.getCurPageSeo().commentIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <%
        string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
        IList<HotelCommentModel> hotelComments = ViewData[typeof(HotelCommentModel).Name] as IList<HotelCommentModel>;
        string homeUrl = HomeController.GetPathIndex();
        string commentUrl = CommentController.GetPathIndex();
    %>
 
    <div class="left" style=" width:70%">
    
                   <div class="box b" style=" margin-bottom:3px">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="酒店点评" href="<%=commentUrl %>" >酒店点评</a> </li>

           </ul>
          
        </div>
   
    
       	 <div class="b">
       	 <h3>最新酒店评论</h3>
         <% Html.RenderPartial("TableHotelCommentList", hotelComments);%>
         </div>
    </div>
    
    <div>
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
          <div>
          <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
      </div>
      
                           <div >
                 
                 <% Html.RenderPartial("DivHotBrand"); %>
               </div>
      
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

   <style type="text/css">
   
         .divBrand
{
	 width:29%;
	}
  
  </style>

</asp:Content>
