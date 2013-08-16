<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
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
              <li><a title="中国商旅酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="中国商旅会员注册" href="<%=basePath %>/MemberAdmin/Reg/Index" >会员注册</a>  </li>
           </ul>
     </div>
     
     <div class="left b" style=" width:70%;" >
            <h3>会员注册信息</h3>
            <div  style="padding:10%">
			<div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em; font-size:1.3em; text-indent:2em; line-height:2.5em"> 
				<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
				尊敬的会员: <%=member.id %>, 恭喜你成功注册为新会员,10元红包奖励已存入你的帐号！ <a href="<%=basePath %>/MemberAdmin/Account/Index">马上查看</a></p>
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
</asp:Content>
