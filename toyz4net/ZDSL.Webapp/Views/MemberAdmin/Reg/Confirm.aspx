<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网会员注册确认
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%     string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       MemberModel member = ViewData[typeof(MemberModel).Name] as MemberModel;
       string homeUrl = HomeController.GetPathIndex();
%>

         <div class="box b" style=" margin-bottom:3px">
          
                                  
          
           <ul id="ulNav">
              <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="会员注册" href="<%=Request.Url.ToString() %>/" >会员注册</a>  </li>
           </ul>
     </div>


    <div class="left b" style=" width:70%" >
    <h3>注册信息检验无误</h3>
    <div  style=" padding:15%;  font-size:1.2em;">
    <form id="formConfirm" action="<%=basePath %>/Public/Reg/DoConfirm" method="post">
    <input type="hidden" name="email" value="<%=member.email %>" />
    <input type="hidden" name="nickName" value="<%=member.nickName %>" />
    <input type="hidden" name="moblie" value="<%=member.moblie %>" />    
            <table >
                <tr>
                    
                    <td style="width:100px;">帐号：</td>
                    <td style="width:auto">
                         <%= member.email %>	       
                    </td>
                </tr>
		        <tr>
                    
                    <td >昵称：</td>
                    <td >
                       <%=member.nickName %>
                    </td>
                </tr>
                
                <tr>
                    
                    <td >手机：</td>
                    <td >
                       <%=member.moblie %>
                    </td>
                </tr>

                <tr>
                  <td></td>
                  <td>
               <button type="button" onclick="document.getElementById('formConfirm').action='<%=basePath %>/Public/Reg/Index';document.getElementById('formConfirm').submit();">返回修改</button>
                   <button type="submit" >确认注册会员</button>
                  </td>
                </tr>

	        </table>
       
        
        
        

           
         </form>
    </div>
</div>


        <div class="right b" style=" width:29%" >
         <h3>快速查询</h3>
         <div id="divQuickSearch" class="box">
           <% Html.RenderPartial("HotelQuickSearchBox"); %>
         </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">


  <script type="text/javascript">

      $(document).ready(function() {

          SetHotelConditionForm("#divQuickSearch");
      });  //$(document)
      
   </script>

</asp:Content>
