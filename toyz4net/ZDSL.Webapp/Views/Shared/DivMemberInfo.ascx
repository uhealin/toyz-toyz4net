<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>

<% 
    string basePath =WebUtil.GetWebRootPath();
    MemberModel member = WebUtil.GetSessionAttr<MemberModel>(typeof(MemberModel).Name);
    member = BaseZdBiz.Load<MemberModel>(member.id);
    DataBiz dataBiz=DataBiz.GetInstant();
    MemberAccountModel account = dataBiz.getCurrMemberAccount(member);
    MemberRewardRuleModel rule = dataBiz.GetCurRewardRule();
    
%>

<table class="tableCondition">
     <tr>
       <th>会员号：</th>
       <td><%=member.id %></td>
     </tr>
     <tr>
       <th>姓名：</th>
       <td><%=member.realName %></td>
     </tr>
          <tr>
       <th>手机：</th>
       <td><%=member.moblie %></td>
     </tr>
     <tr>
       <th>邮箱：</th>
       <td><%=member.email %></td>
     </tr>

     <tr>
       <th>当前奖励：</th>
       <td><%= account.newAmount %></td>
     </tr>
     
     
</table>

    <div id="divRequireRebate"  style="  height:1px; overflow:hidden; display:none;">
       <form id="formExchangRequire" action="<%=basePath %>/MemberAdmin/Account/DoRequestRebate" method="post" >
         <input type="hidden" name="exchangeType" value="<%=MemberAccountModel.METHOD_REWARD_REBATE %>" />

         <table>
             <tr>
               <td>充值手机号:</td>
               <td><input name="moblie" value="<%=member.rebateMoblie %>" /></td>
             </tr>
             <tr>
                <td>备注</td>
                <td><textarea name="remark" cols="30" rows="5"></textarea></td>
             </tr>
              <tr>
                <td></td>
                <td><button type="submit">提交</button></td>
              </tr>
         </table>
       </form>
    </div>
    
        <div id="divCancelRebate"  style="  height:1px; overflow:hidden; display:none;">
       <form id="form1" action="<%=basePath %>/MemberAdmin/Account/DoCancelRebate" method="post"  >
        
         
         <table>
             <tr>
                <td>备注</td>
                <td><textarea name="remark" cols="30" rows="5"></textarea></td>
             </tr>
              <tr>
                <td></td>
                <td><button type="submit">提交</button></td>
              </tr>
         </table>
       </form>
    </div>

   <div class="ui-state-highlight ui-corner-all" >
   <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
    <% if(rule.rebateLimit>account.newAmount){ %>
       <%=string.Format("累计奖励达到{0}即可申请兑换，还差{1}",rule.rebateLimit,rule.rebateLimit-account.newAmount) %> 
    <%} else{ %>
        <%=string.Format("累计奖励已超过{0},可以申请兑换",rule.rebateLimit) %>
        
        <%if (member.rebateInd != MemberModel.IND_Y)
          { %>
          <button onclick="$('#divRequireRebate').dialog({title:'请填写正确兑换信息'});">申请兑换</button>
        <%}
          else { 
          %>
          <button onclick="$('#divCancelRebate').dialog({title:'请填写正确兑换信息'});">暂缓申请</button>
          <%
          } %>
         
    <%} %>
    
    </div>
    
    <script>
        SetPreUrl();
    </script>