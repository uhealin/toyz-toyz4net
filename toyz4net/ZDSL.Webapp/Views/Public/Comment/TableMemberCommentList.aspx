<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>


<%
    string basePath = WebUtil.GetWebRootPath();
    IList<MemberCommentModel> memberComments = ViewData[typeof(MemberCommentModel).Name] as IList<MemberCommentModel>;
    
    
 %>
 
 <table style=" width:100%">
     
      <% foreach (MemberCommentModel memberComment in memberComments)
      { %>
         <tr>
           
           <td><%= memberComment.createDate.ToShortDateString()%></td>
           
         </tr>
         
         <tr>
           <td><%= memberComment.context%></td>
         </tr>
      <%} %>
 </table>