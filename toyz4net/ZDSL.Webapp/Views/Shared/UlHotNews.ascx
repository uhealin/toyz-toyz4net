<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<% 
    string basePath = WebUtil.GetWebRootPath();
    IList<NewsModel> listNews = ViewData[BaseZdController.VD_KEY_HOT_NEWS_LIST] as IList<NewsModel>;
%>


			        <ul>
			          <%foreach (NewsModel news in listNews)
                      {
                          string url = NewsController.GetPathNewsHotel(news);
                   %>
                       <li class="ignore"><a title="<%=news.title %>" href="<%=url %>"><%=news.title %></a></li>
			          <%} %>
			        </ul>