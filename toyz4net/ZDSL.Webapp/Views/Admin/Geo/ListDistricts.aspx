<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListDistricts
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div region="north" style=" padding:5px"> 
      <form>
       请选择城市 <input id="inputGeoFk"  name="qvGeoFk"   /> 
       <a class="easyui-linkbutton" iconCls="icon-search" onclick="clickQueryGeoFk()">查询</a>
       
       </form>
    </div>
    
    <div region="center">
       <table id="tableDG"></table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">


    <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
       string pathDatagrid = basePath + "/Admin/Geo/DatagridDistricts";    
    %>
    
    
    <script type="text/javascript">

        var geoFormatter = GeoFormatter();

        var colId = { title: "", field: "id", checkbox: true };
        var colName = { title: "行政区名", field: "name", width: 300 };
        var colGeoFk = { title: "所在城市", field: "geoFk", width: 200, formatter: geoFormatter };


        function clickQueryGeoFk() {

            var valGeoFk = $(":input[name=qvGeoFk]").val();
            if (valGeoFk.isNullOrEmpty()) { return; }
            var options = $("#tableDG").datagrid("options");
            options["queryParams"] = { qvGeoFk: valGeoFk };
            $("#tableDG").datagrid("reload");
        }

        $(document).ready(function() {

            $("#tableDG").datagrid({
                url: "<%=pathDatagrid %>"
        , columns: [[
          colId, colName, colGeoFk
        ]]
            });

            $("#inputGeoFk").toyzCombogrid({
                textField: "cityName"
                 , valueField: "id"
                 , dataEval: "Toyz4js['cache']['GeoModel']"
            });
        });     // $(document).ready


        
    
    </script>
</asp:Content>
