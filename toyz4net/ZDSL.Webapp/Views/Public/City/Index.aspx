<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	City
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div style=" width:100%; height:250px">
        <% Html.RenderPartial("HotelQuickSearchBox"); %>
     </div>

     <div style=" width:100%; height:330px">
       <div class="title">酒店地图</div>
       <div id="divBMap" style="width:69.8%; height:300px; float:left; ">       </div>
       <div id="divBMapResultList" style=" width:30%;height:270px; float:right">
       </div>
     </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

<script type="text/javascript">
    // 新创建地图
    var map;
    $(document).ready(function() {
        /*
        var map = new BMap.Map("map_container");
        map.centerAndZoom(new BMap.Point(116.404, 39.915), 12);

    // 创建CityList对象，并放在city_container节点内
        var myCl = new BMapLib.CityList({ "container": "city_container", "map": map });

    // 给城市点击时，添加切换地图视野的操作
        myCl.addEventListener("cityclick", function(e) {
        alert("当前城市是" + e.name);
        // 由于此时传入了map对象，所以点击的时候会自动帮助地图定位，不需要下面的地图视野切换语句
        // map.centerAndZoom(e.center, e.level);
        });
        */

         map = new BMap.Map("divBMap");
        map.centerAndZoom(new BMap.Point(116.404, 39.915), 15);
        map.enableScrollWheelZoom();
        var local = new BMap.LocalSearch(map, {
            renderOptions: { map: map, panel: "divBMapResultList" }
        });
        local.search("天安门");


    });     // $(document).ready(function() { 

</script>
  
</asp:Content>
