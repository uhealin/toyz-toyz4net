<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>
		
       <div region="south" style=" overflow:hidden" split="true" title="帐户记录">
          <iframe id="frameAccount" style=" width:100%; height:200px" frameborder="0" scrolling="no">          
          </iframe>
       </div>
       
       <div id="divRebate" style=" height:0.1px; overflow:hidden">
          
           <form id="formRebate" method="post">
           <input type="hidden" name="memberId" />
              <table style=" width:100%; "  class="tableSimple">
                  <tr>
                     <th>返还手机号码：</th>
                     <td><input name="rebateMoblie" readonly /></td>
                  </tr>
                  <tr>
                    <th>返还金额：</th>
                    <td><input name="rebateAmount" class="easyui-numberspinner" min="0" increment="50" /></td>
                  </tr>
                 <tr>
                    <th>备注：</th>
                    <td>
                      <textarea name="remark"></textarea>
                    </td>
                  </tr>
              </table>
           </form>
       </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string pathDatagrid = string.Format("{0}/Admin/Member/Datagrid_{1}", basePath, status);
          
       %>
      <script type="text/javascript">

          function qq(value, name) {
              var qType = name;
              var qVal = value || "";
              var opts = $("#tableDG").datagrid("options");
              //alert(opts["url"]);
              opts["queryParams"] = { qtHotelName: qType, qvHotelName: qVal };
              //$("#tableDG").datagrid(opts);
              $("#tableDG").datagrid("reload");
              $("#tableDG").datagrid("clearSelections");
          }


          function isreserveFormatter(value, rowData, rowIndex) {
              var returnVal = "无效";
              if (value == 0) {
                  returnVal = "激活";
              } else if (value == -1) {
                  returnVal = "未激活";
              } else if (value == -2) {
                  returnVal = "已删除";
              }
              return returnVal;
          }

          function doRebate(data) {
              $("#divRebate").find("input[name=memberId]").val(data["id"]);
              $("#divRebate").find("input[name=rebateMoblie]").val(data["rebateMoblie"]);
              var title = "处理 " + data["realName"] + " 返还申请";
              $("#divRebate").dialog({ title: title
               ,buttons:[btnRebate]
               });
          }

          var rebateFormatter = ToyzOperaFormatter({
          handlerName: "doRebate"
                    ,iconCls:"eidt"
                });

           var btnRebate = { text: "确认返还", iconCls: "icon-ok", handler: function() {
                    $("#formRebate").form("submit", {
                        url: "<%=basePath %>/Admin/Member/DoRebate"
                       , success: function(str) { $.messager.progress("close"); str.messager(); $("#tableDG").datagrid("reload"); }
                    });
                }
                };

          var colCb = { title: "", field: "xx", checkbox: true };
          var colId = { title: "会员", field: "id", width: 120 };
          var colRealName = { title: "真实姓名", field: "realName", width: 100 };
          var colMoblie = { title: "手机", field: "moblie", width: 150 };
          var colRebateMoblie = { title: "充值手机", field: "rebateMoblie", width: 150 };
          var colEmail = { title: "email", field: "email", width: 150 };
          var colPwd = { title: "密码", field: "pwd", width: 150 };
          var colCurAmount = { title: "当前奖励", field: "curAmount", width: 120, editor: {type:"numberspinner"} };
          var colTotalAmount = { title: "总奖励", field: "totalAmount", width: 120, editor: { type: "numberspinner"} };
          var colRebateInd = { title: "申请返还", field: "rebateInd", width: 80, formatter: IndFormatter, editor: GetIndEditor() };
          var colOperaRebate = { tite: "处理返还", field: "o1", width: 80, formatter: rebateFormatter };

          var status = { title: "状态", field: "status", width: 100, formatter: isreserveFormatter };

          var editors = [colId, colPwd, colRealName, colMoblie,colRebateMoblie,colEmail,colRebateInd ];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Member/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Member/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow = crud.getEditRowHandler();

          toolbar[0]["text"] = "新建会员信息";
          toolbar[1]["text"] = "修改会员信息";
          toolbar[2]["text"] = "删除会员信息";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"id"
          , columns: [
               [colCb, colId, colRealName, colMoblie, colEmail, colRebateInd,colRebateMoblie,colOperaRebate]
            ]
          , toolbar: toolbar
          , onDblClickRow: editRow
            , onClickRow: function(rowIndex, rowData) {
                var src = "<%=basePath %>/Admin/Member/ListMemberAccount?memberId=" + rowData["id"];
                $("#frameAccount").attr("src", src);
            }
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>


