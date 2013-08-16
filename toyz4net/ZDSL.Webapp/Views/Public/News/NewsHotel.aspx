<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<% 
	    NewsModel news = ViewData[typeof(NewsModel).Name] as NewsModel; 
	    %>
	   <%=string.Format(PublicBiz.getCurPageSeo().newsHotelTitle, news.id, news.title, news.deployDate.ToShortDateString())%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% 
     string basePath = WebUtil.GetWebRootPath();
     NewsModel news = ViewData[typeof(NewsModel).Name] as NewsModel;
     IList<NewsModel> refNewss = ViewData["refNews"] as IList<NewsModel>;
     string homeUrl = HomeController.GetPathIndex();
     string newsIndexUrl = NewsController.GetPathIndex();
     
    %>
    
     <div class="left" style=" width:70%">
        
        
   <div class="box b" style=" margin-bottom:3px">
   
     
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="新闻资讯" href="<%=newsIndexUrl %>" >新闻资讯</a>  </li>
             <li><%=news.title %></li>
           </ul>
          
        <br />
   
            <% Html.RenderPartial("NewsSearchBox"); %>
          </div>
        
         <div class="b">
             <h3>新闻正文</h3>
             <div class="box" style=" padding:5%">
                <h2 style=" text-align:center"><%=news.title %></h2>
                <div style=" text-align:center">发表时间：<%=news.deployDate %> &nbsp;&nbsp;<a href="<%=basePath %>/Public/News/Index">更多新闻</a></div>
                <div class="context">
                 <pre><%=news.context %></pre>
                </div>
             </div>
         </div>
         
          <div class="b" style=" margin-top:3px">
               <h3>更多新闻</h3>
             <% foreach (NewsModel refNews in refNewss)
               {
                   string url = NewsController.GetPathNewsHotel(news);         
                %>
               <div class="box bottomline">
                   <div class="left">
                      <a href="<%=url %>"><%=refNews.title%></a> 
                   </div>
                   <div class="right" style=" text-align:right">
                     <%=refNews.deployDate.ToShortDateString()%>
                   </div>
               </div>
              
            <%} %>
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
       
            <div class="b" >
         <h3>相关酒店</h3>
          <div class="box" id="divNewsHotelList">
           
          </div>
     </div>
       
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  
     <% 
     string basePath = WebUtil.GetWebRootPath();
     NewsModel news = ViewData[typeof(NewsModel).Name] as NewsModel;
       
    %>
    
    
    <script type="text/javascript">

        function reflashHotels() {

            //condition["rows"] = rows;
            _condition["viewName"] = "UlGeoHotelSearchList";
            _condition["hotelId"] = "<%=news.hotelIdArray %>";
            _condition["rows"] = 20;
            var pagenum = _condition["page"];
            var params = $.param(_condition);
            StartPageWating("酒店搜索进行中");
            $("#divNewsHotelList").load("<%=basePath %>/Public/Hotel/ViewHotelSearch?" + params, function() {
                EndPageWating();
                var total = parseInt($(this).find("#spanTotal").attr("total"));
                var p = total % _condition["rows"] > 0 ? 1 : 0;
                var pageCount = parseInt(total / _condition["rows"]) + p;

               // $("#pager").pager({ pagenumber: pagenum, pagecount: pageCount, buttonClickCallback: clickPager });
                // SetHotelCondition(condition);
                //drawConditionBox();
            });
        }


        $(document).ready(function() {
            <%if(!string.IsNullOrEmpty(news.hotelIdArray)){ %>
            reflashHotels();
            <%} %>
        });   // $(document)
    
    </script>
    
    
    <style>
     
       .divHotelPrice
       {
       	 float:left;
       	}
       	
       .divStar
       {
       	 float:right;
       	}	
    
    </style>

</asp:Content>
