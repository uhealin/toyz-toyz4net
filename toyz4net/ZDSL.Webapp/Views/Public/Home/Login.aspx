<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	中国商旅网会员登陆
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
   JsResultObject re = ViewData[typeof(JsResultObject).Name] as JsResultObject;     
    string homeUrl = HomeController.GetPathIndex();
    
     %>
     
            <div class="box b" style=" margin-bottom:3px">
          
                                  
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="会员登陆" href="<%=Request.Url.ToString() %>" >会员登陆</a>  </li>
           </ul>
     </div>
 

    <div class="left b"  style=" width:49.5%; height:300px;">
       <h3>会员登陆</h3>
       
       <div  style=" padding:15% ; font-size:1.2em;">
       <% Html.RenderPartial("FormLogin"); %>
          
       </div>
    </div>
    
    <div class="right b"  style=" width:49.5%;height:300px;">
      <h3>新会员注册</h3>
      <div  style=" padding:15% ">
        <span style="color: #FF6600;font-family:黑体; font-size: 16px;">注册捷达商旅网会员即能享受：</span> 
        <ol  type="1"> 
  	     <li>免费注册，不收取任何费用！</li>
        <li>预订酒店或机票获得积分，兑换超值礼品！ </li>
        <li>享受更加体贴的个性化服务！</li>
        </ol>
        
        <a href="<%=basePath %>/MemberAdmin/Reg/Index" ><img  src="<%=basePath %>/image/btn_reg.gif" /></a>
      
      </div>

    </div>
 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); %>
  <link type="text/css" rel="stylesheet" href="http://tjs.sjs.sinajs.cn/t3/style/css/common/card.css" />
    <script type="text/javascript" src="http://tjs.sjs.sinajs.cn/t3/platform/js/api/wb.js"></script>
  <script type="text/javascript">


      $(document).ready(function() {

        //  $(".divLogin").dialog({draggable:false});
              WB.core.load(['connect', 'client', 'widget.base'], function() {
                  var cfg = {
                      key: '238190106',
                      xdpath: '<%=basePath %>/weibo/xd.html'
                  };
                  WB.connect.init(cfg);
                  WB.client.init(cfg);

                  // WB.widget.base.followButton(1084691853, document.getElementById("btnWeibo"));
                  // WB.widget.base.followButton(1733363742, document.getElementById("wb_follow_btn2"));
                  WB.widget.base.connectButton(document.getElementById("btnWeibo"));

              });
      });
  </script>
</asp:Content>
