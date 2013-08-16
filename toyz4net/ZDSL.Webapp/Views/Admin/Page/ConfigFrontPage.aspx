<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Public" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ConfigHotCity
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
   <% 
       string basePath = WebUtil.GetWebRootPath();
       FrontPageModel frontPage = ViewData[typeof(FrontPageModel).Name] as FrontPageModel;
    %>
    
    <form id="form1" method="post">
        <table class="tableSimple" style=" width:100%">
          <tr>
            <th style=" width:200px;" >首页推荐城市：</th>
            <td><textarea name="homeHotCityNameArray" cols="70" rows="5" ><%=frontPage.homeHotCityNameArray %></textarea></td>
          </tr>
          <tr>
            <th>酒店预订页面推荐城市：</th>
            <td><textarea name="hotelHotCityNameArray" cols="70" rows="5"><%=frontPage.hotelHotCityNameArray %></textarea></td>
          </tr>
          <!--
          <tr>
            <th>热门品牌推荐：</th>
            <td><textarea name="homeHotBrandNameArray" cols="70" rows="5"><%= frontPage.homeHotBrandNameArray %></textarea></td>
          </tr>
          -->
          <tr>
            <th>品牌搜索字：</th>
            <td><textarea name="searchBrandNameArray" cols="70" rows="5"><%= frontPage.searchBrandNameArray %></textarea></td>
          </tr>
          
          <tr>
             <th></th>
             <td><a class="easyui-linkbutton" iconCls="icon-edit" onclick="doSubmit()" >修改</a></td>
          </tr>
        </table>
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
   
   <%
    
       string basePath = WebUtil.GetWebRootPath();
            
    %>
   
    <script type="text/javascript">


        function doSubmit() {

            $("#form1").form("submit", {
                
                url:"<%=basePath %>/Admin/Page/DoSaveFrontPage"
 
            });
        
        }
    
    </script>

</asp:Content>
