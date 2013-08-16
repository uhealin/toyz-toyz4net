<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListReward
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">


   <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = Toyz4net.Core.Util.ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Member/DatagridMemberCommentReward_{1}", basePath, status);

       %>
       
      <script type="text/javascript">

       var colId = { title: "", filed: "id", checkbox: true };
       var colComFk = { title: "点评Id", field: "comFk", width: 120 };
       var colCreateDate = { title: "记录创建日期", field: "createDate", width: 120,formatter:DateTimeFormatter, editor: { type: "timestampDatebox"}};
       var colMemberFk = { title: "会员帐号", field: "memberFk", width: 120,  };
       var colRebateStatuts = { title: "返还状态", field: "rebateStatuts", width: 120 };
       var colAmount = { title: "返还金额", field: "amount", width: 120, editor: { type: "numberspinner", options: { precision: 2, min: 0, increment: 10}} };
       var colRebateMethod = { title: "返还方式", field: "rebateMethod", width: 120 };
       var colRemark = { title: "备注", field: "remark", width: 120, editor: { type: "textarea"} };

       var colUserFk = { title: "审核人帐号", field: "userFk", width: 120, };


       var editors = [
       colAmount,colRebateMethod,colCreateDate,colRemark
          ];

       var opts = {};
       opts["regexp"] = "#tableDG";
       opts["id"] = "id";
       opts["urlUpdate"] = "<%=basePath %>/Admin/Member/SaveMemberCommentReward";
       opts["urlAdd"] = opts["urlUpdate"];
       opts["urlRemove"] = "<%=basePath %>/Admin/Member/RemoveMemberCommentReward";
       opts["editors"] = editors;

       var crud = new CrudDatagrid(opts);
       var toolbar = crud.getToolbar();
       toolbar[0]["text"] = "新建点评";
       toolbar[1]["text"] = "修改点评";
       toolbar[2]["text"] = "删除点评";


       $(document).ready(function() {

       $("#tableDG").datagrid({
       url: "<%= pathDatagrid%>"
       , columns: [[
          colComFk,colCreateDate,colMemberFk,colRebateStatuts,colAmount,colRebateMethod,colRemark,colUserFk
        ]]
       ,toolbar:toolbar
        });

       });     //$(document).ready(function() { 
   
   </script>

</asp:Content>
