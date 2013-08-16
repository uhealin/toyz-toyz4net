<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListOrder
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          
          string pathDatagrid = string.Format("{0}/Admin/ELong/DatagridHotelOrder", basePath);
          
       %>

<script type="text/javascript">


    var currencyFormatter = DictFormatter("currencyId");
    var orderStatusFormatter = DictFormatter("orderStatus");

    var colHotelOrderId = { title: "订单号", field: "HotelOrderId", width: 120 };
    var colOrderStatusCode = { title: "状态", field: "OrderStatusCode", width: 120, formatter: orderStatusFormatter };
    var colOrderTotalPrice = { title: "总价格", field: "OrderTotalPrice", width: 120 };
    var colCurrencyCode = { title: "货币类型", field: "CurrencyCode", width: 120 ,formatter:currencyFormatter};


    $(document).ready(function() {

    $("#tableDG").datagrid({
      url: "<%=pathDatagrid %>"
     , columns: [
     [colHotelOrderId, colOrderStatusCode, colOrderTotalPrice, colCurrencyCode]
     ]
        });

    });    //   $(document).ready(function() { 


</script>

</asp:Content>
