<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListMemberAccount
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <div region="center">
      <table id="tableDG"></table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

<%
      string basePath = WebUtil.GetWebRootPath();
      MemberModel member = ViewData[typeof(MemberModel).Name] as MemberModel;
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
          var colMethod = { title: "业务", field: "method", width: 200 };
          var colMemberFk = { title: "会员", field: "memberFk", width: 120 };
          var colCreateDate = { title: "创建日期", field: "createDate", width: 120,formatter:DateFormatter, editor: { type: "datebox"} };
          var colRemark = { title: "备注", field: "remark", width: 320, editor: { type: "textarea"} };
         
          var colOldAmount = { title: "原奖励", field: "oldAmount", width: 120, editor: { type: "numberspinner", options: { precision: 2, min: 0, increment: 10}} };
          var colNewAmount = { title: "现有奖励", field: "newAmount", width: 120, editor: { type: "numberspinner", options: { precision: 2, min: 0, increment: 10}} };

          var editors = [ colRemark ];

          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Member/SaveMemberAccount?memberId=<%=member.id %>";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Member/RemoveMemberAccount";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();
    


          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%= basePath%>/Admin/Member/DatagridMemberAccount?memberId=<%=member.id %>"
       , columns: [[
         colId, colMemberFk, colOldAmount, colNewAmount,colMethod ,colCreateDate, colRemark 
       ]]
       , toolbar: toolbar
       ,onDblClickRow:editRow
              });

          });     //$(document).ready(function() { 
   
   </script>

</asp:Content>
