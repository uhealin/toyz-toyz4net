<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	捷达商旅后台登陆
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div region="center">
 
     <div id="divLogin">
       <form id="formLogin" method="post">
          <table>
             <tr>
                <th>帐号</th>
                <td><input name="id" class="easyui-validatebox" required="true"  /></td>
             </tr>
             <tr>
                <th>密码</th>
                <td ><input name="pwd" type="password" class="easyui-validatebox" required="true"   /></td>
             </tr>
             
             <tr>
                 <td colspan="2" style=" text-align:right;">
                    <a class="easyui-linkbutton" iconCls="icon-ok"  onclick="clickLogin()">登陆</a>
                    <a class="easyui-linkbutton" onclick="clickClear()" >清空</a>
                 </td>
             </tr>
          </table>
       </form>
     </div>
</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

   <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); %>
   
   <script type="text/javascript">

       function clickLogin() {
           $('#formLogin').form('submit', {
               url: "<%=basePath %>/Admin/System/DoLogin"
             , success: function(str) {
                 var re = str.toJson();
                 if (re.code == 0) {
                     document.location.href = "<%=basePath %>/admin.aspx";
                 } else {
                     str.messager();
                 }
             }
           });
       }

       function clickClear() {
           $('#formLogin').form('clear');
       }

       $(document).ready(function() {

       $("#divLogin").dialog({
              title:"捷达商旅后台登陆"
           });
       });
     
   </script>

</asp:Content>
