<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Public" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListAdSider
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
  
   <%
       string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       string pathDatagrid = basePath + "/Admin/Page/DatagridAdSider";
   %>
   
   <script type="text/javascript">


       var colCb = { title: "", field: "id", width: 80, checkbox: true };
       var colSrc = { title: "src", field: "src", width: 250,formatter:ImgFormatter("height:80px") };
       var colAlt = { title: "alt", field: "alt", width: 200 };
       var colTitle = { title: "title", field: "title", width: 200 };
       var colHref = { title: "href", field: "href", width: 200, formatter: HrefFormatter() };
       var colRecLevel = { title: "推荐排序", field: "recLevel", width: 100, editor: {type:"numberspinner"} };


       var editors = [colSrc, colAlt, colTitle, colHref, colRecLevel];

       var opts = {};
       opts["regexp"] = "#tableDG";
       opts["id"] = "id";
       opts["urlUpdate"] = "<%=basePath %>/Admin/Page/SaveAdSider";
       opts["urlAdd"] = opts["urlUpdate"];
       opts["urlRemove"] = "<%=basePath %>/Admin/Page/RemoveAdSider";
       opts["editors"] = editors;

       var crud = new CrudDatagrid(opts);
       var toolbar = crud.getToolbar();
       var editRow = crud.getEditRowHandler();


       $(document).ready(function() {

           $("#tableDG").datagrid({
               url: "<%=pathDatagrid %>"
             , idField: "id"
          , columns: [
               [colCb, colSrc, colAlt, colTitle,colHref,colRecLevel]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRow
           });

       });       // $(document).ready(function() { 
        
   </script>

</asp:Content>
