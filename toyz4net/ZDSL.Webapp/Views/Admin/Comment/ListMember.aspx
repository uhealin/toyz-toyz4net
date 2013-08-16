<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     </div>
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Comment/DatagridMember_{1}", basePath, status);
          
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




          var colId = { title: "", field: "id", width: 80, checkbox: true };
          var colContext = { title: "正文", field: "context", width: 250, editor: {type:"textarea"} };
          var colTitle = { title: "主题", field: "title", width: 100 };
          var colCreateDate = { title: "创建日期", field: "createDate", width: 200, formatter: DateFormatter, editor: { type: "timestampDatebox"} };
          var colOrderFk = { title: "订单号", field: "orderFk", width: 200 };
          var colServiceFk = { title: "服务评级", field: "serviceLevel", width: 200 };
         
          var status = { title: "状态", field: "status", width: 100 };

          var editors = [colTitle, colCreateDate, colServiceFk, colContext];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Comment/SaveOrder";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Comment/RemoveOrder";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();

          toolbar[0]["text"] = "新建评论";
          toolbar[1]["text"] = "修改评论";
          toolbar[2]["text"] = "删除评论";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colId, colTitle, colContext,colServiceFk, colCreateDate, colOrderFk]
            ]
          ,toolbar:toolbar
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
