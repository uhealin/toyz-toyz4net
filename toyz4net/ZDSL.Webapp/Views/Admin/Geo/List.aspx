<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div region="north" style=" height:40px;">
            <div id="mm" style="width:120px">
		         <div name="cityName" iconCls=" icon-ok">城市名</div>
		         <div name="provinceName">省份</div>
	        </div>
	        	     <input id="ss" class="easyui-searchbox"
			searcher="qq"
			prompt="Please Input Value" menu="#mm" style="width:300px"></input>
	    </div>
     
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>
		
		<div region="south" style="height:220px;">
		  <div  id="divTabs" class="easyui-tabs" fit="true">
		    <div title="行政区" >
		      <table id="tableGeoDistricts"></table>
		    </div>
		    <div title="标志物">
		      <table id="tableGeoLandmarkLocation"></table>
		    </div>
		    <div title="商业区" >
		      <table id="tableGeoCommercialLocation"></table>
		    </div>
		   </div>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Geo/Datagrid", basePath, status);
          
       %>
      <script type="text/javascript">

          function qq(value, name) {
              var qType = name;
              var qVal = value || "";
              var opts = $("#tableDG").datagrid("options");
              //alert(opts["url"]);
              opts["queryParams"] = { qt: qType, qv: qVal };
              //$("#tableDG").datagrid(opts);
              $("#tableDG").datagrid("reload");
              $("#tableDG").datagrid("clearSelections");
          }


          var statusFormatter = DictFormatter("dbStatus");

          var colId = { title: "", field: "id", width: 80, checkbox: true };
          var colCountry = { title: "国家", field: "country", width: 150 };
          var colProvinceName = { title: "省名", field: "provinceName", width: 150 };
          var colProvinceId = { title: "省代码", field: "provinceId", width: 150 };
          var colCityName = { title: "城市名", field: "cityName", width: 150 };
          var colCityCode = { title: "城市代码", field: "cityCode", width: 150 };
          var colProperties = { title: "酒店数目", field: "properties", width: 150, editor: { type: "numberbox"} };
          var colUrl = { title: "elong连接", field: "url", width: 150 };
          var colRecInd = { title: "首页推荐", field: "recInd", width: 60, formatter: IndFormatter, editor: GetIndEditor() };
          var colRecLevel = { title: "推荐等级", field: "recLevel", width: 60, editor: { type: "numberspinner"} };

          var colName = { title: "商业区名", field: "name", width: 300 };

          var colStatus = { title: "状态", field: "status", width: 100, formatter: statusFormatter };

          var editors = [ colProvinceId,colProvinceName, colCityCode,colCityName,colCountry ,colProperties, colUrl,colRecInd,colRecLevel];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Geo/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Geo/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建地理信息";
          toolbar[1]["text"] = "修改地理信息";
          toolbar[2]["text"] = "删除展会地理信息";

          $(document).ready(function() {
             

              $("#tableDG").datagrid({
                  url: "<%=pathDatagrid %>"
             , idField: "id"
          , columns: [
               [colId, colCountry, colProvinceName, colCityName, colProperties, colRecInd, colRecLevel]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRow
          , onClickRow: function(rowIndex, rowData) {
              var geoFk = rowData["id"];
              $("#tableGeoDistricts").datagrid({
              url: "<%=basePath %>/Admin/Geo/DatagridDistricts?qvGeoFk=" + geoFk
                     , columns: [[
                        colId, colName
                     ]]
              });
              $("#tableGeoLandmarkLocation").datagrid({
              url: "<%=basePath %>/Admin/Geo/DatagridLandmarkLocations?qvGeoFk=" + geoFk
                     , columns: [[
                        colId, colName
                     ]]
              });
              $("#tableGeoCommercialLocation").datagrid({
              url: "<%=basePath %>/Admin/Geo/DatagridCommercialLocation?qvGeoFk=" + geoFk
                     , columns: [[
                        colId, colName
                     ]]
              });
          }
              });

          });          // $(document).ready(function() { 
    
  </script>
</asp:Content>

