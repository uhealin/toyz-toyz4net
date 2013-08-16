<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div region="center">
      
             <form id="formUser"  method="post">
     <input type="hidden"  id="cRolestring" value="${CRolestring}" />
     <input type="hidden" name="Id" value="${Id}" />
       <table class="editTable" cellspacing="0">
         <tbody>
           <tr>
             <th>角色代码</th>
             <td style="width: 35%">
               <input name="id" type="text"  
                />
             </td>
            <th>角色名称</th>
             <td style="width: 35%">
               <input name="name" type="text"  
               />
             </td>
           </tr>
           <tr>
              <th>角色权限</th> 
             <td>
               <input  name="menuFkArray" />
             </td>
           </tr>
           

                     
           <tr>
             <td colspan="4" style="text-align: right">
             <a class="easyui-linkbutton" iconCls="icon-save"
                onclick="clickSave()" 
             >保存</a>
           </td>
           </tr>
         </tbody>
       </table>
      </form>
    
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <%
      string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
      string adminRolePath = basePath + "/Admin/Role/Save";   
      string jsonStrMenus= ViewData["jsonStrMenus"].ToString();
    %>


   <script type="text/javascript">
   
      function clickSave(){
			   $("#formUser").form('submit',{
				   url:"<%=adminRolePath%>"
			   });
 }




 $(document).ready(function() {

 $(":input[name=menuFkArray]").combotree({
     
     multiple:true
     ,cascadeCheck:false
     , url: "<%=basePath %>/Admin/Menu/Tree"
     , textField: "text"
     , valueField:"value"
     });

 });    //$(document).ready 
    
   </script>
  
</asp:Content>
