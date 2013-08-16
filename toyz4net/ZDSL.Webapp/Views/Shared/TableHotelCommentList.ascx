<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<%
    string basePath = WebUtil.GetWebRootPath();
    IList<HotelCommentModel> hotelComments = ViewData[typeof(HotelCommentModel).Name] as IList<HotelCommentModel>;
    
    
 %>


<ul>
     
      <% foreach (HotelCommentModel hotelComment in hotelComments)
      { 
            string rcCode=ObjectUtil.Parse(hotelComment.recommendInd,"Y");
             string rcName=rcCode=="Y"?"推荐":"不推荐";
             string url = HotelController.GetPathDetail(hotelComment.hotelFk);
        %>
         <li class="bottomline"> <span class="spanRc<%=rcCode %>"><%=rcName %></span> &nbsp;
          <%=hotelComment.memberNickName %> &nbsp; 发表于 <%= hotelComment.createDate.ToShortDateString() %> &nbsp; 
          对 &nbsp;<a href="<%=url %>" ><%= hotelComment.hotelName %> </a> &nbsp;的评论：&nbsp;
           <%=hotelComment.context %>
   </li>
         
         
      <%} %>
</ul>