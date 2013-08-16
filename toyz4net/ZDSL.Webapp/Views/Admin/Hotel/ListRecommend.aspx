<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListRecommend
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div region="center">
      <table id="tableDG"></table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

    <%  string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();

        string datagridPath = basePath + "/Admin/Hotel/DatagridRecommend";
        %>

    <script type="text/javascript">


        var colCheckbox = { title: "", field: "hotelId", checkbox: true };
        var colHotelId = { title: "酒店代码", field: "hotelId", width: 250 };
        var colHotelName = { title: "酒店名", field: "hotelName", width: 250 };
        var colPriorityLevel = { title: "优先级", field: "priorityLevel",width:100,editor:{type:"numberspinner",options:{max:10,min:0,increment:1}}};
        var colRemark={title:"备注",field:"remark",width:200,editor:{type:"textarea"}};
    
    
          var editors = [  colPriorityLevel, colRemark];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "hotelId";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Hotel/SaveRecommend";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Hotel/RemoveRecommend";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var tempToolbar = crud.getToolbar();

          tempToolbar[1]["text"] = "修改酒店推荐";
          tempToolbar[2]["text"] = "删除酒店推荐";
          var toolbar = [];
          toolbar[0] = tempToolbar[1];
          toolbar[1] = tempToolbar[2];
          toolbar[2] = tempToolbar[3];

          var dblClickHander = crud.getEditRowHandler();
    
        $(document).ready(function(){
        
          $("#tableDG").datagrid({
              url:"<%=datagridPath %>",
              columns:[
              [colCheckbox, colHotelName, colPriorityLevel, colRemark]
              ]

              , idField: "hotelId"
              , toolbar: toolbar
              , onDblClickRow: dblClickHander
              
          });
        
        });
      
    </script>

</asp:Content>
