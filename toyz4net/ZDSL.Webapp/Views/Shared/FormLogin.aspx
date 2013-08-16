<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<%
    string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    MemberModel member = ViewData[typeof(MemberModel).Name] as MemberModel;
    if (member == null) {
        member = new MemberModel();
    }
    string t="";
    string uid="";
    if (!string.IsNullOrEmpty(member.weiboUid)) {
        t = "weibo";
        uid = member.weiboUid;
    }
    else if (!string.IsNullOrEmpty(member.renrenUid))
    {
        t = "renren";
        uid = member.renrenUid;
    }
    else if (!string.IsNullOrEmpty(member.kaixinUid))
    {
        t = "kaixin";
        uid = member.kaixinUid;
    }
     %>

       <form action="<%=basePath %>/Public/Home/DoLogin" method="post">
       
          <% if(!string.IsNullOrEmpty(t)&&!string.IsNullOrEmpty(uid)){ %>
                <input type="hidden" name="t"  value="<%=t %>" />
                <input type="hidden" name="uid" value="<%=uid %>" />
          <%} %>
          
          <table cellspacing="10">
              <tr>
                <th style=" text-align:right">用户名/邮箱/手机：</th>
                <td><input  type="text"  this.style.color='#000'" style="height: 16px; width: 159px; color: rgb(0, 0, 0);"  name="key"></td>
              </tr>
              
              <tr>
                <th style=" text-align:right">密码：</th>
                <td><input  type="password" style="height:16px; width:159px;" name="pwd"></td>
              </tr>
              
              <tr>
                 <th></th>
                 <td><button type="submit">登陆</button> </td>
              </tr>
              <!--
              <tr>
                 <th></th>
                 <td><span id="btnWeibo"></span></td>
              </tr>
              -->
          </table>
          
          </form>
