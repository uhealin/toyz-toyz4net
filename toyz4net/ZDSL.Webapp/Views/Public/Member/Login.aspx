<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网会员登陆
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    string homeUrl = HomeController.GetPathIndex();
    
     %>
     
            <div class="box b" style=" margin-bottom:3px">
          
                                  
          
           <ul id="ulNav">
              <li><a title="中国商旅酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="中国商旅会员登陆" href="<%=Request.Url.ToString() %>/" >会员登陆</a>  </li>
           </ul>
     </div>
  <div id="f1">
    <div id="f1_1" class="divLogin"  >
        <form>
           <table class="tableLogin">
              <tr>
                <th>用户帐号或Email</th>
                <td><input  /></td>
              </tr>
              
              <tr>
                <th>密码</th>
                <td><input  type="password" /></td>
              </tr>
              
              <tr>
                <td colspan="2" style=" text-align:right"><button>登陆</button></td>
              </tr>
           </table>
         </form>
     </div>
   </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <script type="text/javascript">


      $(document).ready(function() {

        //  $(".divLogin").dialog({draggable:false});

      });
  </script>
</asp:Content>
