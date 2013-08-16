<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Query hotel
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%  
        string cityName = ViewData["cityName"].ToString();
        DateTime checkInDate = (DateTime)ViewData["checkInDate"] ;
        DateTime checkOutDate = (DateTime)ViewData["checkOutDate"];
        string keyword = ViewData["keyword"].ToString();  
     %>
     
     <table>
        <tr>
           <th>城市</th>
           <td><%=cityName %></td>
        </tr>
         <tr>
           <th>入店日期</th>
           <td><%=checkInDate.ToShortDateString() %></td>
        </tr>
                <tr>
           <th>离店日期</th>
           <td><%=checkOutDate.ToShortDateString() %></td>
        </tr>
                <tr>
           <th>关键字</th>
           <td><%=keyword %></td>
        </tr>
     </table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
