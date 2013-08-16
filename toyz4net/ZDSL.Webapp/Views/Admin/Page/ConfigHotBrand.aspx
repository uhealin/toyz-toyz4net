<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListHotBrand
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
          string pathDatagrid = string.Format("{0}/Admin/Page/DatagridHotBrand", basePath);


     
       %>
      <script type="text/javascript">
      
          var listType = [];
          
           <%         foreach (string key in BrandModel.GetDictType().Keys) {
              string text = BrandModel.GetDictType()[key];
           %>
              listType.push({text:"<%=text %>",value:"<%=key %>"});
           <%   
          }
          %> 

          function qq(value, name) {
              var qType = name;
              var qVal = value || "";
              var opts = $("#tableDG").datagrid("options");
              //alert(opts["url"]);
              opts["queryParams"] = { qtName: qType, qvName: qVal };
              //$("#tableDG").datagrid(opts);
              $("#tableDG").datagrid("reload");
              $("#tableDG").datagrid("clearSelections");
          }


          function doRecommend(hotelId) {
              var url = "<%=basePath %>/Admin/Hotel/DoRecommend";
              $.get(url, { hotelId: hotelId }, function(str) { str.messager(); });
          }

          function hotelRecommendFormatter(value, rowData, rowIndex) {
              var a = "<a onclick=\"doRecommend('" + value + "')\"  class='toyz-icon flag-green' ></a>";
              return a;
          }




          var colCb = { title: "", field: "xx", width: 80, checkbox: true };
          var colBrandID = { title: "连锁品牌ID", field: "id", width: 100 };
          var colBrandName = { title: "中文简称", field: "name", width: 250 };
          var colPriorityLevel = { title: "优先级", field: "priorityLevel",width:100,editor:{type:"numberspinner",options:{max:10,min:0,increment:1}}};
          var colRemark={title:"备注",field:"remark",width:200,editor:{type:"textarea"}};
 

          var editors = [  colPriorityLevel, colRemark];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Page/SaveHotBrand";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Page/RemoveHotBrand";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var tempToolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();

          tempToolbar[1]["text"] = "修改热门品牌";
          tempToolbar[2]["text"] = "删除热门品牌";
          var toolbar = [];
          toolbar[0] = tempToolbar[1];
          toolbar[1] = tempToolbar[2];
          toolbar[2] = tempToolbar[3];


          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colCb, colBrandID, colBrandName, colPriorityLevel, colRemark]
            ]
          , toolbar: toolbar
          ,onDblClickRow:editRow
              });

          });       // $(document).ready(function() { 
    
  </script>


</asp:Content>
