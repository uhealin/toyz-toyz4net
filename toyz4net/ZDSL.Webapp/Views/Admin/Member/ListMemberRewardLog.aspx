<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListMemberCommentRewordLog
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div region="center">
      <table id="tableDG"></table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <%
      string basePath = WebUtil.GetWebRootPath();

      /*
                public string id{get;set;}
         public string exchangeType{get;set;}
         public string memberFk{get;set;}
         public DateTime createDate{get;set;}
         public string remark{get;set;}
         public string moblie{get;set;}
         public string bank{get;set;}
         public string exchangeStatus{get;set;}
         public string userFk{get;set;}
         public string rewardFkArrays{get;set;}
         public int amount{get;set;}
       */ 
   %>
   
   
      <script type="text/javascript">
         
          var colId = { title: "", filed: "id", checkbox: true };
          var colExchangeType = { title: "返还类型", field: "exchangeType", width: 200 };
          var colMemberFk = { title: "会员", field: "memberFk", width: 120 };
          var colCreateDate = { title: "创建日期", field: "createDate", width: 120,formatter:DateFormatter, editor: { type: "datebox"} };
          var colRemark = { title: "备注", field: "remark", width: 120, editor: { type: "textarea"} };
          var colMoblie = { title: "手机号码", field: "moblie", width: 180 };
          var colAmount = { title: "返还金额", field: "amount", width: 120, editor: { type: "numberspinner", options: { precision: 2, min: 0, increment: 10}} };
         

          var editors = [ colMoblie,colAmount,colCreateDate,colRemark ];

          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Member/SaveMemberRewardLog";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Member/RemoveMemberRewardLog";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();
          toolbar[0]["text"] = "新建点评规则";
          toolbar[1]["text"] = "修改点评规则";
          toolbar[2]["text"] = "删除点评规则";


          $(document).ready(function() {

              $("#tableDG").datagrid({
                  url: "<%= basePath%>/Admin/Member/DatagridMemberRewardLog"
       , columns: [[
         colId, colMemberFk, colMoblie, colAmount, colCreateDate, colRemark 
       ]]
       , toolbar: toolbar
       ,onDblClickRow:editRow
              });

          });     //$(document).ready(function() { 
   
   </script>

</asp:Content>
