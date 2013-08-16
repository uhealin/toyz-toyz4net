<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListDictType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
  <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          //string status = ObjectUtil.Parse(Request.Params["status"], "");
          //string pathDatagrid = string.Format("{0}/Admin/Dict/Datagrid", basePath, status);
          
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


          function isreserveFormatter(value, rowData, rowIndex) {
              var returnVal = "无效";
              if (value == 0) {
                  returnVal = "激活";
              } else if (value == -1) {
                  returnVal = "未激活";
              } else if (value == -2) {
                  returnVal = "已删除";
              }
              return returnVal;
          }


          var colCheckBox = { title: "id", field: "xxx", width: 80, checkbox: true };
          var colDictTypeId = { title: "字典类型Id", field: "id", width: 180 };
          var colDictTypeName = { title: "字典类型名", field: "name", width: 250 };


          var editors = [colDictTypeId, colDictTypeName];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Dict/SaveDictType";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Dict/RemoveDictType";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();

          toolbar[0]["text"] = "新建字典类型";
          toolbar[1]["text"] = "修改字典类型";
          toolbar[2]["text"] = "删除字典类型";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=basePath%>/Admin/Dict/DatagridDictType"
             ,idField:"id"
          , columns: [
               [colCheckBox, colDictTypeName, colDictTypeId]
            ]
          ,toolbar:toolbar
              });

          });       // $(document).ready(function() { 
          
          </script>
          
</asp:Content>
