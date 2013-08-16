<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<%  
    string basePath = WebUtil.GetWebRootPath();
    PageList<MemberCommentRewardModel> rewards = ViewData[typeof(MemberCommentRewardModel).Name] as PageList<MemberCommentRewardModel>;
%>
<table style=" width:100%">

<tr>
<th class="order_th" align="center">订单号</th>
<th align="center">提交时间</th>
<th align="center">总金额</th>
<th align="center">总积分</th>
<th align="center">订单状态</th>
</tr>



<% foreach (MemberCommentRewardModel reward in rewards)
   {  %>
<tr>
<td><%=reward.comFk %></td>
<td><%=reward.createDate %></td>
<td><%=reward.amount %></td>
<td></td>
<td></td>


</tr>


<%} %>


</table>
