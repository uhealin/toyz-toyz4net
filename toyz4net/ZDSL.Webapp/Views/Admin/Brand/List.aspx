<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
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
          string pathDatagrid = string.Format("{0}/Admin/Brand/Datagrid_{1}", basePath, status);


     
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


          function doSetHotBrand(e) {
              var brandId=e["brandID"];
              var url = "<%=basePath %>/Admin/Page/DoSetHotBrand";
              $.get(url, { brandId: brandId }, function(str) { str.messager(); });
          }






          var colCb = { title: "", field: "xx", width: 80, checkbox: true };
          var colBrandID = { title: "连锁品牌ID", field: "brandID", width: 100 };
          var colBrandName = { title: "中文简称", field: "brandName", width: 250 };
          var colBrandNameLong = { title: "连锁酒店品牌中文全称", field: "brandNameLong", width: 200 };
          var colBrandPinYin = { title: "拼音", field: "brandPinYin", width: 100 };
          var colPicURL = { title: "图片地址", field: "picURL", width: 100,formatter:ImgFormatter("width:30px")};
          var colHotelCount = { title: "酒店数量", field: "hotelCount", width: 100,editor:{type:"numberspinner"} };
          var colLastChangetime = { title: "修改时间", field: "lastChangetime", width: 100,formatter:DateFormatter , editor: {type:"datebox"} };
          var colType = { title: "类型", field: "type", width: 100,editor:{type:"combobox",options:{data:listType}} };
          var colRecInd = { title: "首页推荐", field: "recInd", width: 60,formatter:IndFormatter,editor:GetIndEditor() };
          var colRecLevel = { title: "推荐等级", field: "recLevel", width: 60, editor: {type:"numberspinner"} };
          
          var colHotBrandOpera={title:"设置热门",field:"o1",width:80,formatter:ToyzOperaFormatter({handlerName:"doSetHotBrand",iconCls:"icon-ok"}) };

          var editors = [colBrandName, colBrandNameLong, colBrandPinYin, colPicURL,colHotelCount,colType,colRecInd,colRecLevel];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "brandID";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Brand/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Brand/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建连锁品牌";
          toolbar[1]["text"] = "修改连锁品牌";
          toolbar[2]["text"] = "删除连锁品牌";


          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"brandId"
          , columns: [
               [colPicURL, colBrandName, colBrandNameLong, colBrandPinYin, colHotelCount, colType,colRecInd,colRecLevel]
            ]
          , toolbar: toolbar
          ,onDblClickRow:editRow
              });

          });       // $(document).ready(function() { 
    
  </script>

</asp:Content>
