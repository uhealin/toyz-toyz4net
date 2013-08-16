<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div region="north" style=" height:40px;">
            <div id="mm" style="width:120px">
		         <div name="cn" iconCls=" icon-ok">中文名查询</div>
		         <div name="en">英文名查询</div>
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
          string isreserve = ObjectUtil.Parse(Request.Params["isreserve"], "");
          string pathDatagrid = string.Format("{0}/Admin/Hotel/Datagrid", basePath, status, isreserve);
          string reserveData= EasyuiUtil.ToComboboxData(HotelModel.GetDictReserve());
       %>
      <script type="text/javascript">

          var _conditon = {};

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


          function doRecommend(hotelId) {
              var url = "<%=basePath %>/Admin/Hotel/DoRecommend";
              $.get(url, { hotelId: hotelId }, function(str) { str.messager(); });
          }

          function hotelRecommendFormatter(value, rowData, rowIndex) {
              var a = "<a onclick=\"doRecommend('" + rowData["hotelId"] + "')\"  class='toyz-icon flag-green' ></a>";
              return a;
          }

          function isreserveFormatter(value, rowData, rowIndex) {

              if (value == 0) {
                  return "正常营业";
              } else if (value == 1) {
                  return "暂停营业";
              } else if (value == 2) {
                  return "已删除";
              } else {
              return "未知状态";
              }
          }

          var colCb = { title: "", field: "xx", width: 80, checkbox: true };
          var colHotelId = { title: "id", field: "hotelId", width: 100 };
          var colHotelName = { title: "酒店中文名", field: "hotelName", width: 200 };
          var colHotelNameEn = { title: "酒店英文名", field: "hotelNameEn", width: 200 };
          var colIsreserve = { title: "有效性", field: "isreserve", width: 100, formatter: isreserveFormatter, editor: { type: "combobox", options: {data:[<%=reserveData %>]} }};
          var colRecInd = { title: "首页推荐", field: "recInd", width: 60,formatter:IndFormatter,editor:GetIndEditor() };
          var colRecLevel = { title: "推荐等级", field: "recLevel", width: 60, editor: {type:"numberspinner"} };
          var colRecommendOpera = { title: "酒店推荐", field: "o1", formatter: hotelRecommendFormatter, width: 100 };
          var colBookedCount = { title: "预订数", field: "bookedCount", width: 60, editor: {type:"numberspinner"} };
          var colGoodCommentCount = { title: "好评数", field: "goodCommentCount", width: 60, editor: {type:"numberspinner"} };
          var colBadCommentCount = { title: "差评数", field: "badCommentCount", width: 60, editor: {type:"numberspinner"} };

          var editors = [colHotelName, colHotelNameEn, colRecInd,colRecLevel, colIsreserve,colBookedCount,colGoodCommentCount,colBadCommentCount];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "hotelId";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Hotel/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Hotel/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建酒店";
          toolbar[1]["text"] = "修改酒店";
          toolbar[2]["text"] = "删除酒店";


          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"hotelId"
          , columns: [
               [ colHotelId, colHotelName]
            ]
        
          
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
