<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div region="north" style=" height:40px;">
            <div id="mm" style="width:120px">
		         <div name="text" iconCls=" icon-ok">字典名查询</div>
		         <div name="value">字典值查询</div>
	        </div>
	
	     <input id="ss" class="easyui-searchbox"
			searcher="qq"
			prompt="Please Input Value" menu="#mm" style="width:300px"></input>
        </div>
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>
	
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
       <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Dict/Datagrid_{1}", basePath, status);
          
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

          var colDictId = { title: "", field: "id", width: 80, checkbox: true };
          var colDictText = { title: "字典名", field: "text", width: 250 };
          var colDictValue = { title: "字典值", field: "value", width: 300 };
          var colDictType = { title: "字典类型", field: "type", width: 300, editor: { type: "combobox", options: {data:Toyz4js["cache"]["DictTypeModel"]["rows"],textField:"name",valueField:"id",width:200}} };
          var colIsreserve = { title: "有效性", field: "status", width: 100, formatter: isreserveFormatter };

          var editors = [colDictText, colDictValue, colDictType];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Dict/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Dict/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();

          toolbar[0]["text"] = "新建字典";
          toolbar[1]["text"] = "修改字典";
          toolbar[2]["text"] = "删除字典";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colDictId, colDictText, colDictValue, colDictType]
            ]
          ,toolbar:toolbar
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
