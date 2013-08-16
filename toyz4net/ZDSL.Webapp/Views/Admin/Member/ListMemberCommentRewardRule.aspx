<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListMemberCommentRewardRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div region="center">
      <table id="tableDG"></table>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = Toyz4net.Core.Util.ObjectUtil.Parse(Request.Params["status"], "");

          string datagridPath = basePath + "/Admin/Member/DatagridMemberCommentRewardRule";

   %>
   
   <script type="text/javascript">

       var colId = { title: "", filed: "id", checkbox: true };
       var colName = { title: "规则名", field: "name", width: 200 };
       var colRebateScale = { title: "返还比例", field: "rebateScale", width: 120, editor: { type: "numberspinner", options: { precision: 4, min: 0, max: 1, increment: 0.01}} };
       var colRebateAmount = { title: "返还固定金额", field: "rebateAmount", width: 120, editor: { type: "numberspinner", options: { precision: 0, min: 0, increment: 10}} };
       var colRebateScaleAmount = { title: "返还比例下限", field: "rebateScaleAmount", width: 120, editor: { type: "numberspinner", options: { precision: 0, min: 1, increment: 10}} };
       var colComValidityDays = { title: "有效点评日数", field: "comValidityDays", width: 120, editor: { type: "numberspinner", options: { precision: 0, min: 1, increment: 1}} };
       var colRebateLimit = { title: "返还上限", field: "rebateLimit", width: 120, editor: { type: "numberspinner", options: { precision: 0, min: 0, increment: 10}} };
       var colRebateValidityMonths = { title: "返还有效月数", field: "rebateValidityMonths", width: 120, editor: { type: "numberspinner", optoins: { precision: 0, min: 1, increment: 1}} };

       var editors = [colName, colRebateScale, colRebateAmount, colRebateScaleAmount, colComValidityDays, colRebateLimit, colRebateValidityMonths
          ];

       var opts = {};
       opts["regexp"] = "#tableDG";
       opts["id"] = "id";
       opts["urlUpdate"] = "<%=basePath %>/Admin/Member/SaveMemberCommentRewardRule";
       opts["urlAdd"] = opts["urlUpdate"];
       opts["urlRemove"] = "<%=basePath %>/Admin/Member/RemoveMemberCommentRewardRule";
       opts["editors"] = editors;

       var crud = new CrudDatagrid(opts);
       var toolbar = crud.getToolbar();
       var editRow = crud.getEditRowHandler();
       toolbar[0]["text"] = "新建点评规则";
       toolbar[1]["text"] = "修改点评规则";
       toolbar[2]["text"] = "删除点评规则";


       $(document).ready(function() {

       $("#tableDG").datagrid({
       url: "<%= datagridPath%>"
       , columns: [[
         colId,colName, colRebateScale, colRebateAmount, colRebateScaleAmount, colComValidityDays, colRebateLimit, colRebateValidityMonths
       ]]
       , toolbar: toolbar
       ,onDblClickRow:editRow
        });

       });     //$(document).ready(function() { 
   
   </script>

</asp:Content>
