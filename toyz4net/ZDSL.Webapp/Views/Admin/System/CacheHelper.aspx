<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"  MasterPageFile="~/Views/Shared/Admin.Master" %>
<%@ Import Namespace="ZDSL.Model.Cache" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <%
    string basePath = WebUtil.GetWebRootPath();
    OrderCountModel orderCount = CacheBiz.GetCurrOrderCount();
    RewardCountModel rewardCount = CacheBiz.GetCurrRewardCount();

    ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
    icr.Add(Restrictions.Eq("rebateInd", MemberModel.IND_Y));
    int unRebateCount = BaseZdBiz.Count(icr, "id");
    %>
     <div region="center">
       <div style=" float:left">
                 <table class="tableSimple" cellpadding="15" cellspacing="5">
        
         <tr>
           <th>全部订单:</th>
           <td><%=orderCount.allCount %> </td>

           <th>新订单:</th>
           <td><%=orderCount.newCount %> </td>

           <th>elong处理成功:</th>
           <td><%=orderCount.elongSuccessCount %> </td>
  
           <th>内部处理成功:</th>
           <td><%=orderCount.zdSuccessCount %> </td>
 
           <th>处理失败:</th>
           <td><%=orderCount.failCount %> </td>

           <th>已取消:</th>
           <td><%=orderCount.cancelCount %> </td>
 
           <th>已点评:</th>
           <td><%=orderCount.commentedCount %> </td>
           <th>已成交:</th>
           <td><%=orderCount.dealCount %> </td>
           
    
         </tr>
          <tr>
           <th>返还申请:</th>
           <td><%= unRebateCount %> </td>
           </tr>
         </table>
      </div>
      
      <div style=" float:left; text-align:left;">
      <a class="easyui-linkbutton" iconCls="icon-reload"  onclick="document.location.reload();" >刷新</a>
      <br />
      <a  href="<%=basePath %>/Admin/System/DoLogout" target="_top" >退出登陆</a>
      </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
