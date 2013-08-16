<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

            <div region="north" style=" height:40px;">
            <div id="mm" style="width:120px">
		         <div name="cn" iconCls=" icon-ok">中文名查询</div>
		         <div name="en">英文名查询</div>
		         <div name="idcard">身份证号码查询</div>
	        </div>
	
	     <input id="ss" class="easyui-searchbox"
			searcher="qq"
			prompt="Please Input Value" menu="#mm" style="width:300px"></input>
        </div>
       <div region="center"  >
		  <table id="tableDG"></table>
		</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
       <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = Toyz4net.Core.Util.ObjectUtil.Parse(Request.Params["status"], "");
          string type = Toyz4net.Core.Util.ObjectUtil.Parse(Request.Params["guestType"], "");
          string pathDatagrid = string.Format("{0}/Admin/Guest/Datagrid_{1}_{2}", basePath, status,type);
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


         /*
                 public string idNumber { get; set; }
        public string idTypeCode { get; set; }
        public string genderCode { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string nationality { get; set; }
        public int phoneInterCode { get; set; }
        public int phoneAreaCode { get; set; }
        public int phoneNumber { get; set; }
        public int phoneExt { get; set; }
        public int faxInterCode { get; set; }
        public int faxAreaCode { get; set; }
        public int faxNumber { get; set; }
        public int faxExt { get; set; }
        public string parameterString1 { get; set; }
        public string parameterString2 { get; set; }
        public string parameterString3 { get; set; }
        public int parameterInt1 { get; set; }
        public int parameterInt2 { get; set; }
        public int parameterInt3 { get; set; }
        public string nameCn { get; set; }
        public string nameEn { get; set; }
         */

          var colIdNumber = { title: "身份证号码", field: "idNumber", width: 80, checkbox: true };
          var colIdTypeCode = { title: "身份证类型", field: "idTypeCode", width: 120, editor: { type: "combobox", options: { data: Toyz4js["cache"]["DictModel"]["idType"]["rows"]}} };
          var colGenderCode = { title: "性别", field: "genderCode", width: 50, editor: { type: "combobox", options: { data: Toyz4js["cache"]["DictModel"]["gender"]["rows"]}} };
          var colEmail = { title: "电子邮箱", field: "email", width: 120 };
          var colMobile = { title: "手机", field: "mobile", width: 120 };
          var colNationality = { title: "国籍", field: "nationality", width: 120 };
          var colPhoneInterCode = { title: "电话国际区号", field: "phoneInterCode", width: 120 };
          var colPhoneAreaCode = { title: "电话国内区号", field: "phoneAreaCode", width: 120 };
          var colPhoneNumber = { title: "电话号码", field: "phoneNumber", width: 120 };
          var colPhoneExt = { title: "电话分机号码", field: "phoneExt", width: 120 };
          var colFaxInterCode = { title: "传真国际区号", field: "faxInterCode", width: 120 };
          var colFaxAreaCode = { title: "传真国内区号", field: "faxAreaCode", width: 120 };
          var colFaxNumber = { title: "传真号码", field: "faxNumber", width: 120 };
          var colFaxExt = { title: "传真分机号码", field: "faxExt", width: 120 };
          var colParameterString1 = { title: "备用字符1", field: "parameterString1", width: 120 };
          var colParameterString2 = { title: "备用字符2", field: "parameterString2", width: 120 };
          var colParameterString3 = { title: "备用字符3", field: "parameterString3", width: 120 };
          var colParameterInt1 = { title: "备用数字1", field: "parameterInt1", width: 120 };
          var colParameterInt2 = { title: "备用数字2", field: "parameterInt2", width: 120 };
          var colParameterInt3 = { title: "备用数字3", field: "parameterInt3", width: 120 };
          var colNameCn = { title: "中文名", field: "nameCn", width: 120 };
          var colNameEn = { title: "英文名", field: "nameEn", width: 120 };

          var editors = [colIdNumber, colIdTypeCode, colNameCn, colNameEn, colGenderCode, colMobile, colEmail
           , colNationality, colPhoneInterCode, colPhoneAreaCode, colPhoneNumber, colPhoneExt
           , colFaxInterCode, colFaxAreaCode, colFaxNumber, colFaxExt, colParameterInt1, colParameterInt2, colParameterInt3
           ,colParameterString1,colParameterInt2,colParameterInt3
          ];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "hotelId";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Guest/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Guest/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();

          toolbar[0]["text"] = "新建会员点评规则";
          toolbar[1]["text"] = "修改会员点评规则";
          toolbar[2]["text"] = "删除会员点评规则";

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"idNumber"
          , columns: [
               [colIdNumber, colNameCn, colNameEn, colNationality, colMobile, colEmail]
            ]
          ,toolbar:toolbar
              });

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>
