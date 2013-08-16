<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="ZDSL.Webapp.admin" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
捷达商旅后台管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

   <% string basePath = WebUtil.GetWebRootPath();
      string loginMsg = "";
      if (Session[ZDSL.Biz.AdminBiz.SESSION_KEY_LOGIN_MSG] != null)
      {
          loginMsg = Session[ZDSL.Biz.AdminBiz.SESSION_KEY_LOGIN_MSG].ToString();
      }   
     %>
   
  <script type="text/javascript">
      $(document).ready(function() {

          var iframe = "<iframe class='iframeTab' frameborder='0' src='<%=basePath %>/Admin/System/Frompage' />";
          $("#mainTabs").tabs('add', {
              title: "起始页",
              closable: false
	      , content: iframe
          });


          $('#MenuTreeNode').tree({
              url: "<%=basePath %>/Admin/Menu/Tree?parentId=root",
              animate: true,
              onClick: function(node) {
                  if (node["attributes"]["url"].length < 1)
                  { return; }
                  var src = "<%=basePath%>/" + node["attributes"]["url"] + "?" + node["attributes"]["parma"] || "";
                  document.getElementById("iframeContent").src = src;
                  $("#spanTitle").text(node["text"]);
              }
          }); 
          $.messager.show({ title: "登陆成功", msg: "<%=loginMsg %>" ,timeout:6000});

      });           //
     
     

  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<%
    string basePath = WebUtil.GetWebRootPath();
     %>

		<div region="west" iconCls="icon-reload" id="MenuTreeNode" split="true" style="width:300px;padding: 5%"  title="菜单导航">
	
	
		</div>

		<div region="center"  >
			<iframe  style=" width:99%;height:99%" frameborder="0" scrolling="yes" id="iframeContent"></iframe>
		</div>

        <div region="north" style=" text-align:right; background-color:Yellow">
            <span id="spanTitle">&nbsp;</span>
        </div>
        
        <div region="east" style="width:200px" split="true" title="酒店快速查询">
            <iframe  style=" width:99%;height:99%" frameborder="0" scrolling="yes"
             src="<%=basePath %>/Admin/System/QueryHelper?act"
            ></iframe>
        </div>
        
        <div region="south" style=" height:100px" split="true">
              <iframe  style=" width:99%;height:99%" frameborder="0" scrolling="yes"
             src="<%=basePath %>/Admin/System/CacheHelper"
            ></iframe>
        </div>

</asp:Content>
