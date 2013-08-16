<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Cache" %>
<%@ Import Namespace="ZDSL.Model.Admin" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	GobalConfig
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% string basePath = WebUtil.GetWebRootPath();
       ConfigModel config = AdminBiz.GetCurrConfig();
        %>
    <div region="center">
       <form id="form1" method="post">
       <input type="hidden" name="id" value="<%=config.id %>" />
        <table class="tableSimple" style=" width:100%">
          <tr>
            <th style=" width:200px;" >订单自动提交：</th>
            <td><input id="inputAutoOrderInd"  name="autoOrderInd"  value="<%=config.autoOrderInd %>" /></td>
          </tr>
         
          <tr>
            <th style=" width:200px;" >启用静态页面：</th>
            <td><input id="inputStaticInd"  name="staticInd"  value="<%=config.staticInd %>" /></td>
          </tr>
         
          <tr>
            <th></th>
            <td><a class="easyui-linkbutton" iconCls="icon-edit" onclick="doSubmit()" >保存</a></td>
          </tr>
        </table>
    </form>
 </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
  
      <%
    
       string basePath = WebUtil.GetWebRootPath();
            
    %>
   
    <script type="text/javascript">


        function doSubmit() {

            $("#form1").form("submit", {

                url: "<%=basePath %>/Admin/System/DoSaveConfig"

            });

        }

        $(document).ready(function() {

            var indOpt = GetIndEditor()["options"];

            $("#inputAutoOrderInd,#inputStaticInd").combobox(indOpt);
        });
    
    </script>

</asp:Content>
