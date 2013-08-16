<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListNewsRefHotel
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   		<div region="center"  >
		  <table id="tableDGRefHotel"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
 
<%  
    string basePath = WebUtil.GetWebRootPath();
    string newsId = WebUtil.GetParamAttr("newsId","");    
 %>

<script  type="text/javascript">


    var colCb = { title: "", field: "xx", width: 80, checkbox: true };
    var colHotelId = { title: "酒店Id", field: "hotelId", width: 100 };
    var colHotelName = { title: "酒店中文名", field: "hotelName", width: 200 };
    var colHotelNameEn = { title: "酒店英文名", field: "hotelNameEn", width: 200 };

    var colRecInd = { title: "首页推荐", field: "recInd", width: 60, formatter: IndFormatter, editor: GetIndEditor() };
    var colRecLevel = { title: "推荐等级", field: "recLevel", width: 60, editor: { type: "numberspinner"} };

    var colBookedCount = { title: "预订数", field: "bookedCount", width: 60, editor: { type: "numberspinner"} };
    var colGoodCommentCount = { title: "好评数", field: "goodCommentCount", width: 60, editor: { type: "numberspinner"} };
    var colBadCommentCount = { title: "差评数", field: "badCommentCount", width: 60, editor: { type: "numberspinner"} };

    var editors = [colHotelId];

    var opts = {};
    opts["regexp"] = "#tableDGRefHotel";
    opts["id"] = "hotelId";
    opts["urlUpdate"] = "<%=basePath %>/Admin/News/SaveNewsRefHotel?newsId=<%=newsId %>";
    opts["urlAdd"] = opts["urlUpdate"];
    opts["urlRemove"] = "<%=basePath %>/Admin/News/RemoveNewsRefHotel?newsId=<%=newsId %>";
    opts["editors"] = editors;

    var crud = new CrudDatagrid(opts);
    var toolbar = crud.getToolbar();
    var editRow = crud.getEditRowHandler();


    $(document).ready(function() {


    $("#tableDGRefHotel").datagrid({
    url: "<%=basePath %>/Admin/News/DatagridNewsRefHotel?newsId=<%=newsId %>",
    idField: "hotelId"
                  , pagination: false
             , columns: [
               [colCb, colHotelId, colHotelName]
               ]
             , toolbar: toolbar
             , onDblClickRow: editRow
    });
    
    
    });
    

</script>

</asp:Content>
