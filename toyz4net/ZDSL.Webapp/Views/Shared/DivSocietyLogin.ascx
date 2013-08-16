<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<%
  
    string basePath = WebUtil.GetWebRootPath();
%>

<div class="box" style=" text-align:center">

      <span>你也可以使用合作网站帐号登陆</span><br/>
      <ul>
        <li style=" border-width:0px" ><a href="<%=basePath %>/MemberAdmin/Reg/DoWeiboAuth"><img src="<%=basePath %>/image/btn.weibo.login.png"  style=" height:28px; width:135px" /></a> </li>
        <li style=" border-width:0px"><a href="<%=basePath %>/MemberAdmin/Reg/DoRenrenAuth"><img src="<%=basePath %>/image/btn.renren.login.png"  style=" height:28px; width:135px" /></a> </li>
        <li style=" border-width:0px"><a href="<%=basePath %>/MemberAdmin/Reg/DoKaixinAuth"><img src="<%=basePath %>/image/btn.kaixin.login.png"  style=" height:28px; width:135px" /></a> </li>
         <li style=" border-width:0px"><a href="<%=basePath %>/MemberAdmin/Reg/DoQQAuth"><img src="<%=basePath %>/image/btn.qq.login.png"  style=" height:28px; width:135px" /></a> </li>
        <li style=" border-width:0px"><a href="<%=basePath %>/MemberAdmin/Reg/DoTencentWeiboAuth"><img src="<%=basePath %>/image/btn.tweibo.login.png"  style=" height:28px; width:135px" /></a> </li>
      </ul>
</div>