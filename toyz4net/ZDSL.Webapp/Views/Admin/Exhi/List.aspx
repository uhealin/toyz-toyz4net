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
          string exhiStatus = ObjectUtil.Parse(Request.Params["exhiStatus"], "");
          string pathDatagrid = string.Format("{0}/Admin/Exhi/Datagrid_{1}_{2}", basePath, status, exhiStatus);
          
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

          var colExhiId = { title: "", field: "id", width: 80, checkbox: true };
          var colExhiName = { title: "展会名", field: "name", width: 200 };
          var colExhiGeofk = { title: "所属城市", field: "geoFk", width: 100, editor: { type: "combobox", options: { textField: "cityName", valueField: "id", data: Toyz4js["cache"]["GeoModel"]["rows"] }  } };
          var colExhiMsg = { title: "发布信息", field: "msg", width: 200, editor: {type:"textarea"} };
          var colExhiStartDate = { title: "开始日期", field: "startDate", width: 200, formatter: DateFormatter, editor: {type:"datebox"} };
          var colExhiEndDate = { title: "结束日期", field: "endDate", width: 200, formatter: DateFormatter, editor: { type: "datebox"} };
          var colExhiStatus = { title: "展会状态", field: "exhiStatus", width: 100 };
          var colExhiAddress = { title: "地址", field: "address", width: 100 };
          var colExhiSiteName = { title: "场所", field: "siteName", width: 100 };
          var colExhiBusName = { title: "所属行业", field: "busName", width: 100 };
          var colExhiHotelIdArray = { title: "相关酒店", field: "hotelIdArray", width: 100 };
          var colExhiStatus = { title: "展会状态", field: "exhiStatus", width: 100, editor: { type: "combobox", options: { textField: "text", valueField: "value", data: Toyz4js["cache"]["DictModel"]["exhiStatus"]["rows"]}} };
          var editors = [colExhiName, colExhiGeofk, colExhiStartDate, colExhiEndDate, colExhiStatus,colExhiAddress,colExhiBusName,colExhiSiteName,colExhiHotelIdArray ,colExhiMsg ];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Exhi/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Exhi/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRowHandler = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建展会";
          toolbar[1]["text"] = "修改展会";
          toolbar[2]["text"] = "删除展会";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colExhiId,colExhiName, colExhiGeofk, colExhiStartDate, colExhiEndDate, colExhiStatus]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRowHandler
        , onClickRow: function(rowIndex, rowData) {
            var src = "<%=basePath %>/Admin/Exhi/ListExhiRefHotel?exhiId=" + rowData["id"];
            $("#frameRefHotel").attr("src", src);
        }
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
