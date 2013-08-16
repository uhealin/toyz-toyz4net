<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

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
        //  string status = Toyz4net.Core.Util.ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Role/Datagrid", basePath);
          
     
   
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



         // var colCheckbox = { title: "", field: "xx", checkbox: true };
          var colId = { title: "角色代码", field: "id", checkbox:true };
          var colName = { title: "角色名", field: "name", width: 250 };
          var colMenuFkArray = { title: "菜单序列", field: "menuFkArray", width: 500,
          editor: { type: "combotree", options: { multiple: true, cascadeCheck: true     , url: "<%=basePath %>/Admin/Menu/ComboTree?parentId=root" , textField: "text" , valueField: "value",width:250}}
          };


          var editors = [colId, colName, colMenuFkArray];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Role/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Role/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRowHandler = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建角色";
          toolbar[1]["text"] = "修改角色";
          toolbar[2]["text"] = "删除角色";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colId, colName, colMenuFkArray]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRowHandler
              });
          
          });       // $(document).ready(function() { 
    
  </script>

</asp:Content>
