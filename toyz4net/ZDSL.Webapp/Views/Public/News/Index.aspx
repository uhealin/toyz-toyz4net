<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= PublicBiz.getCurPageSeo().newsIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <% 
     string basePath = WebUtil.GetWebRootPath();
     IList<NewsModel> newss = ViewData[typeof(NewsModel).Name] as IList<NewsModel>;
     string homeUrl = HomeController.GetPathIndex();
     string newsUrl = NewsController.GetPathIndex();
    %>

    <div class="left" style=" width:70%">
    

    
        <div class="box b" style=" margin-bottom:3px">
          
                       
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="新闻资讯" href="<%=newsUrl %>" >新闻资讯</a>  </li>
           </ul>
         <br />
            <% Html.RenderPartial("NewsSearchBox"); %>
          </div>
      <div  class="b">
         <h3>酒店新闻</h3>
         <div class="box">
           
            <% foreach (NewsModel news in newss)
               {
                   string url = NewsController.GetPathNewsHotel(news);       
            %>
               <div class="box bottomline">
                   <div class="left">
                      <a href="<%=url %>"><%=news.title %></a> 
                   </div>
                   <div class="right" style=" text-align:right">
                     <%=news.deployDate.ToShortDateString() %>
                   </div>
               </div>
              
            <%} %>
        
         </div>
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
      
      <div>
          <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
      </div>
      
                     <div >
                 
                 <% Html.RenderPartial("DivHotBrand"); %>
               </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <style>
   
         .divBrand
{
	 width:29%;
	}
  
  </style>

</asp:Content>
