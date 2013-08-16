<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网账户管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        string basePath = WebUtil.GetWebRootPath();
        string startDate = ViewData["startDate"] as string;
        string endDate = ViewData["endDate"] as string;
        IList<MemberAccountModel> allAccounts = ViewData[typeof(MemberAccountModel).Name] as IList<MemberAccountModel>;
        Dictionary<string, string[]> dictMethods = new Dictionary<string, string[]>();
        dictMethods.Add("会员奖励", new string[] {MemberAccountModel.METHOD_ACCOUNT_CREATE });
        dictMethods.Add("点评奖励", new string[] {MemberAccountModel.METHOD_COMMENT_REWARD });
        dictMethods.Add("返回奖励", new string[] { MemberAccountModel.METHOD_REWARD_REBATE });
        dictMethods.Add("全部", new string[] { });
        Dictionary<string, IList<MemberAccountModel>> dictAccounts = new Dictionary<string, IList<MemberAccountModel>>();

        foreach (string key in dictMethods.Keys)
        {
            string[] values = dictMethods[key];
            if (values.Length == 0) {
                dictAccounts.Add(key, allAccounts);
                continue;
            }
            IList<MemberAccountModel> tempAccounts = new List<MemberAccountModel>();
            foreach (string value in values) {

                foreach (MemberAccountModel account in allAccounts)
                {
                    if (account.method == value) {
                        tempAccounts.Add(account);
                    }
                }
                 
            }
            dictAccounts.Add(key, tempAccounts);
        
        }
        string homeUrl = HomeController.GetPathIndex();
       
        
    %>


        <div class="left" style=" width:69%">
        
                <div  class="box b">
           <ul id="ulNav">
           
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="账户管理" href="<%= Request.Url.ToString() %>" >账户管理</a>  </li>
           </ul>      
                  <br />
         <form action="<%=basePath %>/MemberAdmin/Account/Index"  method="post">
           <ul>
              <li class="liCondition"><a href="?dateRegexp=_week">本周</a></li>
              <li class="liCondition"><a href="?dateRegexp=_month">本月</a></li>
             
               <li><input name="startDate" value="<%=startDate %>" />&nbsp;~&nbsp;<input name="endDate" value="<%=endDate %>" />&nbsp;&nbsp;<button type="submit">查询订单</button></li>
               
           </ul>
          </form>
           
        </div>
        
        <div class="divTabs">
        
          
          <ul>

             <%  int i=0;
                 foreach (string key in dictMethods.Keys)
                 { %>
                  <li><a href="#tab<%=i++ %>"><%=key %>(<%=dictAccounts[key].Count()%>)</a></li>
              <%} %>
          </ul>
          
           <%   i=0;
                foreach (string key in dictMethods.Keys)
                 { %>
                  <div class="divRewardList" id="tab<%=i++ %>" style="  height:600px; overflow-y:scroll">
                  
                  <table style=" width:100%">

                        <tr class="bottomline">
                        <td >记录时间</td>
                        <td >业务类型</td>
                        <td >原奖励</td>
                        <td >现有奖励</td>
       
                        </tr>
                  <% foreach (MemberAccountModel account in dictAccounts[key])
                       {
                          %>
                    <tr>
                    <td><%=account.createDate.ToShortDateString() %>&nbsp;<span class="spanShowRemark pointer" href="javascript:void();" >详情</span><div class="divRemark" style=" display:none"><%=account.remark %></div></td>
                    <td><%=MemberAccountModel.GetDictMethods()[account.method] %></td>
                    <td><%=account.oldAmount %></td>
                    <td><%=account.newAmount%></td>
            


                    </tr>
   

                    <%} %>
                  
                  </table>
                  </div>
              <%} %>
          

        
        </div>
    </div>


    <div class="right b"  style=" width:29%">
           <h3>会员信息</h3>
       <div class="box">
          <% Html.RenderPartial("DivMemberInfo"); %>
       </div>
       <h3>快速搜索</h3>
       <div class="box">
       <% Html.RenderPartial("HotelQuickSearchBox"); %>
       </div>
       
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">


   <%
       string basePath = WebUtil.GetWebRootPath();
    %>
      <style>
     .liCondition
     {
     	 float:left;
     	  margin-right:20px;
     	}
   </style>
   
     <script type="text/javascript">

         $(document).ready(function() {

             $("input[name=startDate],input[name=endDate]").datepicker({
                 numberOfMonths: 2
              , showOn: "both"
             , buttonImage: _basePath + "/image/icon.calendar.gif"
             , buttonImageOnly: true
             });

             $(".spanShowRemark").each(function(i) {

                 var context = $(this).parent().find(".divRemark").html();
                 $(this).qtip({
                 content: context,
                 show: 'mouseover',
                 hide: 'mouseout',
                 position: {
                     corner: {
                         target: 'bottomLeft'
                         //,tooltip: 'bottomLeft'
                     }
                  }
                 });

             });

         });
         
   </script>

</asp:Content>
