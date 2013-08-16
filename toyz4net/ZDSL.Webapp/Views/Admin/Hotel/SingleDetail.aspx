<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SingleDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <% 
       
        HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
        
        string basePath = WebUtil.GetWebRootPath();
        string hotelId = ObjectUtil.Parse(hotelDetail.id, "");
        string keyword = ObjectUtil.Parse(hotelDetail.name,"广州");
    %>

    
    <div region="north" style="padding:5px" >
               酒店:
                <div id="searchboxHotel" style="width:120px">
		          <div name="cn" iconCls=" icon-ok">中文名查询</div>
		          <div name="en">英文名查询</div>
	            </div> &nbsp;
	            	     <input id="ss" class="easyui-searchbox"
			              searcher="searcherHotel" value="<%=keyword %>"
			              prompt="输入酒店名" menu="#searchboxHotel" style="width:250px"></input>
	            <input type="text" id="inputHotelId" name="hotelId" value="<%=hotelId %>" />
	            <a class="easyui-linkbutton" iconCls="icon-search"  onclick="clickLoadHotelHandler()"></a>&nbsp;&nbsp;&nbsp;
	            <a class="easyui-linkbutton" icon="icon-save" onclick="$('#formHotelDetail').form('submit');" >保存</a>
	            <a class="easyui-linkbutton" icon="icon-save" onclick="clickDoImportFromElong()" >从elong更新</a>
    </div>
    <div region="center" >
      <form id="formHotelDetail" method="post" action="<%=basePath %>/Admin/Hotel/SaveHotelDetail">
        <!--
        public string id { get; set; }	//酒店id，即HotelID
        public DateTime dateUpdated { get; set; }	//酒店最新更新时间
        public string name { get; set; }	  //酒店名称
        public string address { get; set; }//	酒店地址
        public string zip { get; set; }	  //	酒店所在地邮编
        public int category { get; set; }	  //	酒店星级（此处酒店星级是艺龙推荐星级，而非酒店挂牌星级）
        public string typology { get; set; }	  //	酒店类别
        public int roomNumber { get; set; }	  //	酒店总房间数量
        public string availPolicy { get; set; }	  //	酒店特殊信息提示
        public DateTime activationDate { get; set; }	  //	暂时不用
        public int usersRating { get; set; }	  //	用户评分,目前暂时不用
        public int elongRanking { get; set; }	  //	酒店在艺龙的排序，目前排序为动态排序，暂时不用
        public string templateType { get; set; }	  //	暂时不用
        public string translations { get; set; }	  //	暂时不用
        public Double doublePriceMin { get; set; }	  //	暂时不用
        public Double doublePriceMax { get; set; }	  //	暂时不用
        public string currency { get; set; }	  //	暂时不用
        public Double lat { get; set; }	  //	酒店所在位置的纬度
        public Double lon { get; set; }	  //	酒店所在位置的经度
        public string country { get; set; }	  //	酒店所在国家
        public string region { get; set; }	  //	酒店区域
        public string province { get; set; }	  //	酒店所在省份 
        public string city { get; set; }	  //	酒店所在城市
        public string businessZone { get; set; }	  //	酒店所在商业区
        public string district { get; set; }	  //	酒店所在行政区
        public string propertyUrl { get; set; }	  //	酒店在www.elong.com网站中的详细页URL
        public string introEditor { get; set; }	  //	酒店介绍信息
        public string ccAccepted { get; set; }	  //	可支持的信用卡
        public string description { get; set; }	  //	酒店描述
        public string phone { get; set; }	  //	酒店电话(前台)
        public string fax { get; set; }	  //	酒店传真(前台)
        public DateTime openingDate { get; set; }	  //	酒店开业日期
        public DateTime renovationDate { get; set; }	  //	酒店装修日期
        public int star { get; set; }	  //	酒店挂牌星级
        public string brandId { get; set; }	  //	酒店所属连锁品牌ID
        public string iseconomic { get; set; }	  //	是否是经济型酒店
        public string isapartment { get; set; }	  //	是否是酒店式公寓
        -->
        <fieldset>
           <label>基础信息</label>
          <table class="tableSimple" style="width:100%"   >
           <tr>
             <th>酒店id:<input name="id"  class=""  value="<%=hotelDetail.id %>" /></th>
             <th>酒店名称:<input name="name" class=""  value="<%=hotelDetail.name %>" /></th>
             <th>酒店星级:<input name="category" class=""  value="<%=hotelDetail.category %>" /></th>
           </tr>
           
           <tr>
             <th>酒店类别:<input name="typology" class="" value="<%=hotelDetail.typology %>" /> </th>
             <th>星级<input name="star" class="" value="<%=hotelDetail.star %>"/> </th>
             <th>酒店总房间数量:<input name="roomNumber" class="" value="<%=hotelDetail.roomNumber %>" /> </th>
           </tr>
           
           <tr>
             <th>经济型酒店:<select name="iseconomic" class="easyui-combobox" value="<%=hotelDetail.iseconomic %>" >
                 <option value="0">否</option>
                 <option value="1">是</option>
               </select> </th>
             <th>酒店式公寓<select name="isapartment" class="easyui-combobox" value="<%=hotelDetail.isapartment %>" >
                 <option value="0">否</option>
                 <option value="1">是</option>
             </select> </th>
             <th>所属连锁品牌:<input name="brandId" class="" value="<%=hotelDetail.brandId %>" /></th>
           </tr>
           
             <tr>
             
             <th colspan="2">酒店特殊信息提示:<input name="availPolicy" class="" value="<%=hotelDetail.availPolicy %>" style=" width:600px" /></th>
             <th></th>
           </tr>
           
           <tr>
             <th>酒店描述:<textarea name="description" class=""  rows="5" cols="40" ><%=hotelDetail.description %></textarea></th>
             <th colspan="2">酒店介绍信息:<textarea name="introEditor" class=""  rows="5" cols="80" ><%=hotelDetail.introEditor %></textarea></th>
             
             
           </tr>
           
           </table>
        </fieldset>


        <fieldset>
             <legend>地理位置信息</legend>
             <table class="tableSimple" style="width:100%"   >
               <tr>
             <th>纬度<input name="lat" class="" value="<%=hotelDetail.lat %>"/></th>
             <th>经度<input name="lon" class="" value="<%=hotelDetail.lon %>"/></th>
             <th>酒店地址:<input name="address" class="" value="<%=hotelDetail.address %>" /></th>
           </tr>
           
           <tr>
             <th>所在国家:<input name="country" class="" value="<%=hotelDetail.country %>"/></th>
             <th>所在省份:<input name="province" class="" value="<%=hotelDetail.province %>"/></th>
             <th>所在城市:<input name="" class="" value="<%=hotelDetail.city %>"/></th>
           </tr>
           
           <tr>
             <th>所在商业区:<input name="country" class="" value="<%=hotelDetail.businessZone %>"/></th>
             <th>所在行政区:<input name="province" class="" value="<%=hotelDetail.district %>"/></th>
            
           </tr>
             </table>
        </fieldset>
           
        <fieldset>
            <legend>联系方式</legend>
                     <table class="tableSimple" style="width:100%"   >  
        <tr>
             <th>电话:<input name="phone" class="" value="<%=hotelDetail.phone %>"/></th>
             <th>传真:<input name="fax" class="" value="<%=hotelDetail.fax %>"/></th>
             <th>url连接:<input name="" class="" value="<%=hotelDetail.propertyUrl %>"/></th>
           </tr>
        <tr>
           <td ></td>
        </tr>
       </table>
        </fieldset>

       
       </form>
    </div>
    <div region="south" style="height:220px;">
        <div  id="divTabs">
            <div title="房间" id="HotelRoom"></div>
            <div title="图片" id="HotelImage"></div>
            <div title="交通点" id="HotelTrafficInfo"></div>
            <div title="商业区" id="HotelSurroundingCommerce"></div>
            <div title="餐厅" id="HotelSurroundingRestaurant"></div>
            <div title="特色建筑" id="HotelSurroundingAttraction"></div>
            <div title="店铺" id="HotelSurroundingShop"></div>
            <div title="标志物" id="HotelLandMark"></div>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

   
    <% 
       
        HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;
        
        string basePath = WebUtil.GetWebRootPath(); 
        
    %>
    
    

   <script type="text/javascript">


       function searcherHotel(value, type) {
           var grid = $("#inputHotelId").combogrid("grid");
           var opts = grid.datagrid("options");
           var params = $.param({ qtHotelName: type, qvHotelName: value })
           opts["url"] = "<%=basePath %>/Admin/Hotel/Datagrid_act_normal?" + params;
           grid.datagrid("reload");
           $("#inputHotelId").combogrid("showPanel");
       }


       function clickLoadHotelHandler() {
           var valHotelId = $("#inputHotelId").combogrid("getValue");
           if (valHotelId == null || valHotelId == "") {
               $.messager.alert("操作错误", "请选择需要编辑的酒店");return;
           }

           document.location.search = "hotelId=" + valHotelId;
       }


       function clickDoImportFromElong() {
           var valHotelId = $("input[name=id]").val();
           if (valHotelId == null || valHotelId == "") {
               $.messager.alert("操作错误", "请选择需要编辑的酒店"); return;
           }
           var url = "<%=basePath %>/Admin/Hotel/DoImportFromElong";
           $.get(url, { hotelId: valHotelId }, function(str) {
               var re = str.toJson();
               if (re["code"] == 0) {
                   document.location.reload();
               } else {
                   str.messager();
               }
           });
       }
       
       
       function clickSetImgUrlHander(rowData){
        
            var hotelId=rowData["hotelFk"]||"";
            var imgUrl=rowData["imgUrl"]||"";
            var url="<%=basePath %>/Admin/Hotel/DoUpdateImgUrl";
            $.post(url,{hotelId:hotelId,imgUrl:imgUrl},function(str){str.messager()});
       }
       
       var operaSetImgUrlFormatter=ToyzOperaFormatter({iconCls:"icon-edit",handlerName:"clickSetImgUrlHander"});

       var mapColumns = {};
/*
               public string id { get; set; }
        public string roomTypeId { get; set; }	//房型id
        public string roomName { get; set; }	//	房型名称
        public int roomTypeNum { get; set; }	//	房型数量
        public double area { get; set; }	//	房间面积
        public string floor { get; set; }	//	房型所在的楼层
        public string hasBroadnet { get; set; }	//	是否有宽带
        public string broadnetFee { get; set; }	//	宽带是否收费  
        public string bedDescription { get; set; }	//	房间描述
        public string bedType { get; set; }	//	床型描述信息
        public string note { get; set; }	//	备注
        
        
        public string imgUrl { get; set; }//图片URL地址
        public string imgType { get; set; }	//图片类型
        public string title { get; set; }  //	图片标题
        public int imgNum { get; set; } 
        
        
        
*/

       var colHotelId = { title: "", field: "hotelId", width: 80 };
       var colHotelName = { title: "酒店中文名", field: "hotelName", width: 250 };
       var colHotelNameEn = { title: "酒店英文名", field: "hotelNameEn", width: 300 };

      var colCheckbox={title:"",filed:"cb",checkbox:true};
      var colRoomName = { title: "房型名称", field: "roomName", width: 200 };
      var colRoomTypeNum = { title: "房型数量", field: "roomTypeNum", width: 200 };
      var colArea = { title: "房间面积", field: "area", width: 200 };
      var colFloor = { title: "房型所在的楼层", field: "floor", width: 200 };
      var colHasBroadnet = { title: "是否有宽带", field: "hasBroadnet", width: 200 };
      var colBroadnetFee = { title: "宽带是否收费", field: "broadnetFee", width: 200 };
      var colBedDescription = { title: "房间描述", field: "bedDescription", width: 200 };
      var colBedType = { title: "床型描述信息", field: "bedType", width: 200 };
      var colNote = { title: "备注", field: "note", width: 200 };


      var colImgUrl = { title: "图片URL地址", field: "imgUrl", width: 200 };
      var colImgType = { title: "图片类型", field: "imgType", width: 200 };
      var colTitle = { title: "图片标题", field: "title", width: 200 };
      var colImgNum = { title: "图片数", field: "imgNum", width: 200 };

      var colName = { title: "名称", field: "name", width: 250 };
      var colDistances = { title: "距离", field: "distances", width: 120 };
      var colDescp = { title: "描述", field: "descp", width: 250 };
      var colTimeTaking = { title: "所需时间", field: "timeTaking", width: 80 };
      var colNote = { title: "备注", field: "note", width: 300 };
      var colTransportFee = { title: "交通费用", field: "transportFee", width: 100 };
      var colTransportationsFk = { title: "交通工具", field: "transportationsFk", width: 120 };

      var colLandmarkId = { title: "ID", field: "landmarkId", width: 120 };
      var colLandmarkName = { title: "中文名", field: "landmarkName", width: 120 };
      var colLandmarkNameEn = { title: "英文名", field: "landmarkNameEn", width: 120 };
      var colOperaDoUpdateImgUrl = {title:"设置酒店图片",field:"o1",width:80,formatter:operaSetImgUrlFormatter}


      mapColumns["HotelRoom"] = [colCheckbox, colRoomName, colRoomTypeNum, colArea, colFloor, colHasBroadnet, colHasBroadnet, colBedDescription, colBedType, colNote];
      mapColumns["HotelImage"] = [colCheckbox, colTitle, colImgType, colImgNum, colImgUrl,colOperaDoUpdateImgUrl];

      mapColumns["HotelTrafficInfo"] = [colCheckbox, colName, colDistances, colTimeTaking, colTransportFee, colTransportationsFk, colNote];
      mapColumns["HotelSurroundingCommerce"] = [colCheckbox, colName, colDistances, colTimeTaking, colTransportFee, colTransportationsFk, colNote];
      mapColumns["HotelSurroundingRestaurant"] = [colCheckbox, colName, colDistances, colDescp];
      mapColumns["HotelSurroundingAttraction"] = [colCheckbox,colName, colDistances];
      mapColumns["HotelSurroundingShop"] = [colCheckbox, colName, colDistances, colDescp];
      mapColumns["HotelLandMark"] = [colCheckbox, colLandmarkId, colLandmarkName, colLandmarkNameEn];



      var mapEditors = {};
      mapEditors["HotelRoom"] = [ colRoomName, colRoomTypeNum, colArea, colFloor, colHasBroadnet, colHasBroadnet, colBedDescription, colBedType, colNote];
      mapEditors["HotelImage"] = [ colTitle, colImgType, colImgNum, colImgUrl,colOperaDoUpdateImgUrl];

      mapEditors["HotelTrafficInfo"] = [colName, colDistances, colTimeTaking, colTransportFee, colTransportationsFk, colNote];
      mapEditors["HotelSurroundingCommerce"] = [ colName, colDistances, colTimeTaking, colTransportFee, colTransportationsFk, colNote];
      mapEditors["HotelSurroundingRestaurant"] = [ colName, colDistances, colDescp];
      mapEditors["HotelSurroundingAttraction"] = [ colName, colDistances];
      mapEditors["HotelSurroundingShop"] = [ colName, colDistances, colDescp];
      mapEditors["HotelLandMark"] = [ colLandmarkId, colLandmarkName, colLandmarkNameEn];


      $(document).ready(function() {

          $("#divTabs").tabs({
              fit: true
            , onSelect: function(title) {
                var divTab = $("#divTabs").tabs("getTab", title);
                var tabId = divTab.attr("id");
                if (divTab.html() != "") { return; }
                var tableDG = $("<table></table>");
                tableDG.attr("id", "dg" + tabId);
                divTab.append(tableDG);

                var opts = {};
                opts["regexp"] = "#dg" + tabId;
                opts["id"] = "id";
                opts["editors"] = mapEditors[tabId];
                opts["addUrl"] = "<%=basePath %>/Admin/Hotel/SaveHotelSubInfo?modelType=" + tabId + "&hotelFk=<%=hotelDetail.id %>";
                opts["updateUrl"] = "<%=basePath %>/Admin/Hotel/SaveHotelSubInfo?modelType=" + tabId;
                opts["removeUrl"] = "<%=basePath %>/Admin/Hotel/RemoveHotelSubInfo?modelType=" + tabId;

                var crud = new CrudDatagrid(opts);
                var toolbar = crud.getToolbar();
                var dblClickHandler = crud.getEditRowHandler();

                tableDG.datagrid({
                    url: "<%=basePath %>/Admin/Hotel/DatagridHotelDetailSubInfo?modelType=" + tabId + "&hotelFk=<%=hotelDetail.id %>"
                   , columns: [mapColumns[tabId]]
                   , idField: "id"
                   , toolbar: toolbar
                   , onDblClickRow: dblClickHandler
                });

            }
          });




          $("#inputHotelId").combogrid({
              textField: "hotelName"
               , idField: "hotelId"
               , columns: [[colHotelId, colHotelName, colHotelNameEn]]
               , panelWidth: 500
               , width: 300
               , editable: false

          });


      });                         //$(document).ready
   
   </script>

</asp:Content>
