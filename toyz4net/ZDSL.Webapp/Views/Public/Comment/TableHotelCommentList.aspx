<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>


<%
    string basePath = WebUtil.GetWebRootPath();
   
    
    
 %>
 <% Html.RenderPartial("TableHotelCommentList");%>

 
