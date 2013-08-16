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

        IList<MemberRewardModel> allRewards = ViewData[typeof(MemberRewardModel).Name] as IList<MemberRewardModel>;
        Dictionary<string, string[]> dictRewardStatue = MemberRewardModel.GetDictRewardStatus();
        Dictionary<string, IList<MemberRewardModel>> dictRewards = new Dictionary<string, IList<MemberRewardModel>>();

        foreach (string key in dictRewardStatue.Keys) {
            string[] values=dictRewardStatue[key];
            IList<MemberRewardModel> tempRewards = new List<MemberRewardModel>();
            foreach (string value in values) {

                foreach (MemberRewardModel reward in allRewards) {
                    if (reward.rebateStatuts == value) {
                        tempRewards.Add(reward);
                    }
                }
                 
            }
            dictRewards.Add(key, tempRewards);
        
        }

        dictRewards.Add("全部", allRewards);
        
    %>


        <div class="left" style=" width:69%">
        
                <div  class="box b">
           <ul>
              <li class="liCondition"><a href="?dateRegexp=_week">本周</a></li>
              <li class="liCondition"><a href="?dateRegexp=_month">本月</a></li>
              <li class="liCondition"><a href="?dateRegexp=_season">本季</a></li>
              <li><input name="startDate" />~<input name="endDate" /></li>
           </ul>
          
           
        </div>
        
        <div class="divTabs">
        
          
          <ul>

             <%  int i=0;
                 foreach (string key in dictRewards.Keys)
                 { %>
                  <li><a href="#tab<%=i++ %>"><%=key %>(<%=dictRewards[key].Count() %>)</a></li>
              <%} %>
            
          </ul>
          
           <%   i=0;
                 foreach (string key in dictRewards.Keys)
                 { %>
                  <div class="divRewardList" id="tab<%=i++ %>">
                  
                  <table style=" width:100%">

                        <tr>
                        <th class="order_th" align="center">订单号</th>
                        <th align="center">提交时间</th>
                        <th>酒店</th>
                        <th align="center">订单金额</th>
                        <th align="center">奖励金额</th>
       
                        </tr>
                  <% foreach (MemberRewardModel reward in dictRewards[key])
                       { 
                         MemberCommentModel comment=BaseZdBiz.Load<MemberCommentModel>(reward.comFk);
                         if (comment == null) {
                             comment = new MemberCommentModel();
                         }
                         OrderModel order=BaseZdBiz.Load<OrderModel>(comment.orderFk);
                         if (order == null) {
                             order = new OrderModel();
                         }
                          %>
                    <tr>
                    <td><%=order.id %></td>
                    <td><%=reward.createDate.ToShortDateString() %></td>
                    <td><%=order.hotelName %></td>
                    <td><%=order.totalPrice %></td>
                    <td><%=NumberUtil.Format(reward.amount, 0) %></td>


                    </tr>
                    <tr>
                      <td colspan="5">
                        <% if (!string.IsNullOrEmpty(reward.remark))
                           { %>
                           <div class="ui-state-highlight ui-corner-all"  style=" padding:2%; font-weight:bold">
                              备注:<%=reward.remark %>
                           </div>
                        <%} %>
                      </td>
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
       
       });
         
   </script>

</asp:Content>
