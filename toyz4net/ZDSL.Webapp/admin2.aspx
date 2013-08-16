<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" CodeBehind="admin2.aspx.cs" Inherits="ZDSL.Webapp.admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
捷达商旅后台管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

   <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); %>
   
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
                  if ($("#mainTabs").tabs("exists", node["text"]) == false) {

                      var src = "<%=basePath%>/" + node["attributes"]["url"] + "?" + node["attributes"]["parma"] || "";
                      var iframe = "<iframe class='iframeTab' frameborder='0' src='" + src + "' />";
                      $("#mainTabs").tabs('add', {
                          title: node["text"],
                          closable: true
						 , content: iframe
                      });
                  } else {
                      $("#mainTabs").tabs("select", node["text"]);
                  }
              }
          });

      });       //
     
     

  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
		<div region="west" iconCls="icon-reload" id="MenuTreeNode" split="true" style="width:300px;padding: 5%"  title="菜单导航">
	
	
		</div>

		<div region="center"  >
			<div class="easyui-tabs" fit="true" id="mainTabs" border="false">	
			</div>
		</div>


</asp:Content>
