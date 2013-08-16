<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>
		
       <div region="south" style=" overflow:hidden" split="true" title="相关酒店列表">
          <iframe id="frameRefHotel" style=" width:100%; height:200px" frameborder="0" scrolling="no">          
          </iframe>
       </div>
		

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string newsStatus = ObjectUtil.Parse(Request.Params["newsStatus"], "");
          string pathDatagrid = string.Format("{0}/Admin/News/Datagrid_{1}_{2}", basePath, status, newsStatus);
          
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



          var colId = { title: "", field: "id", width: 80, checkbox: true };
          var colTitle = { title: "主题", field: "title", width: 200 };
          var colContext = { title: "正文", field: "context", width: 100, editor: {type:"textarea"} };
          var colDeployDate = { title: "发布日期", field: "deployDate", width: 200, formatter: DateFormatter, editor: {type:"datebox"} };
          var colModifyDate = { title: "修改日期", field: "modifyDate", width: 200, formatter: DateFormatter, editor: { type: "datebox"} };
          var colDeployerId = { title: "发布人", field: "deployerId", width: 200 };
          var colType = { title: "类型", field: "type", width: 200 };
          var colNewsStatus = { title: "新闻状态", field: "newsStatus", width: 100, editor: { type: "combobox", options: { textField: "text", valueField: "value", data: Toyz4js["cache"]["DictModel"]["newsStatus"]["rows"]}} };
          var colExhiHotelIdArray = { title: "相关酒店", field: "hotelIdArray", width: 100 };

          var editors = [colTitle, colDeployDate, colModifyDate, colDeployerId, colNewsStatus, colContext];

          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/News/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/News/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRowHandler = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建新闻";
          toolbar[1]["text"] = "修改新闻";
          toolbar[2]["text"] = "删除新闻";









          $(document).ready(function() {

              $("#tableDG").datagrid({
                  url: "<%=pathDatagrid %>"
             , idField: "id"
          , columns: [
               [colId, colTitle, colDeployDate, colModifyDate, colDeployerId, colType]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRowHandler
          , onClickRow: function(rowIndex, rowData) {
              var src = "<%=basePath %>/Admin/News/ListNewsRefHotel?newsId=" + rowData["id"];
              $("#frameRefHotel").attr("src", src);
          }
              });



          });               // $(document).ready(function() { 
    
  </script>
</asp:Content>

