<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FormImportStaticData
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div region="center">
        <table class="tableSimple"  style=" width:100%"  >
           <tr>
              <th style="width:30%;  padding-top:30px;"><a class="easyui-linkbutton import" id="Hotel" iconCls="icon-ok">导入酒店基础数据</a></th>
              <td style="width:60%" class="tdHotel"></td>
           </tr>
           
           <tr>
              <th><a class="easyui-linkbutton import" id="Brand" iconCls="icon-ok">导入连锁品牌数据</a></th>
              <td class="tdBrand"></td>
           </tr>
           
           <tr>
              <th><a class="easyui-linkbutton import" id="Geo" iconCls="icon-ok">导入地理位置数据</a></th>
              <td class="tdGeo"></td>
           </tr>
           
            <tr>
              <th><a class="easyui-linkbutton import" id="Dict" iconCls="icon-ok">导入字典数据</a></th>
              <td class="tdDict"></td>
           </tr>
        </table>
          
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

  <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); %>
  
  <script type="text/javascript">


      $(document).ready(function() {


          $(".import").click(function() {
              var type = $(this).attr("id");
              var url = "<%=basePath %>/Admin/Elong/DoImportStaticData";
              $.messager.progress({msg:"请求处理中，请稍候",text:""});
              $.get(url, { type: type }, function(str) {
                  $.messager.progress("close");
                  var re = str.toJson();
                  if (re["code"] == 0) {
                      
                      var td = $(".td" + type);
                      td.html(re["msg"]);
                  } else {
                      str.messager();
                  }
              });

          });

      });      // $(document).ready(function()
  
  
  </script>
</asp:Content>
