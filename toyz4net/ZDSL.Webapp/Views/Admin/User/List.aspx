<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/User/Datagrid_{1}", basePath, status);
          
     
   
       %>
      <script type="text/javascript">

          function qq(value, name) {
              var qType = name;
              var qVal = value || "";
              var opts = $("#tableDG").datagrid("options");
              //alert(opts["url"]);
              opts["queryParams"] = { qtHotelName: qType, qvHotelName: qVal };
              //$("#tableDG").datagrid(opts);
              $("#tableDG").datagrid("reload");
              $("#tableDG").datagrid("clearSelections");
          }

          var roleFormatter = RoleFormatter();

          var colCheckbox = { title: "", field: "xx", checkbox: true };
          var colId = { title: "帐号", field: "id", width: 180 };
          var colPwd = { title: "密码", field: "pwd", width: 250 };
          var colName = { title: "姓名", field: "name", width: 300 };
          var colRoleFk = { title: "角色", field: "roleFk", width: 300, formatter: roleFormatter, editor: { type: "combobox", options: {textField:"name",valueField:"id",data:Toyz4js["cache"]["RoleModel"]["rows"]}} };
          var colEmail = { title: "email", field: "email", width: 100 };

          var editors = [colId, colPwd, colName, colRoleFk, colEmail];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/User/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/User/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();

          toolbar[0]["text"] = "新建用户";
          toolbar[1]["text"] = "修改用户";
          toolbar[2]["text"] = "删除用户";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colCheckbox,colId, colPwd, colName, colRoleFk, colEmail]
            ]
          ,toolbar:toolbar
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
