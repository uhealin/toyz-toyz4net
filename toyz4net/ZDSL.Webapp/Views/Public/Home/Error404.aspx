<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= PublicBiz.getCurPageSeo().homeIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    string homeUrl = HomeController.GetPathIndex();
     %>

             <div class="box b" style=" margin-bottom:3px">
          
                                  
          
           <ul id="ulNav">
              <li>当前位置:</li>
              <li><a title="中国商旅酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li>页面不存在 </li>
           </ul>
     </div>
     
     <div class="left b" style=" width:70%;" >
            <div  style="padding:10%">
			<div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em; font-size:1.3em; text-indent:2em; line-height:2.5em"> 
				<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
				  你所访问的页面不存在，或者你可以尝试：
				  </p>
				  <ol >
				    <li>1.&nbsp;<a onclick="javascript:window.history.back();">回到之前的页面</a></li>
				    <li>2.&nbsp;<a href="<%=homeUrl %>">回到首页</a></li>
				  </ol>
				
			</div>
		    </div>
     </div>
      <div class="right b" style=" width:29%">
         <h3>快速搜索</h3>
         <div class="box">
         <%Html.RenderPartial("HotelQuickSearchBox"); %>
         </div>
     </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Status = "404 No Found";
        }
    }
</script>

</asp:Content>
