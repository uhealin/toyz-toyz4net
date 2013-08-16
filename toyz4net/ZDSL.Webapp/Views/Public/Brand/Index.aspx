<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Biz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= PublicBiz.getCurPageSeo().brandIndexTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<%
    string basePath = WebUtil.GetWebRootPath();
    IList<BrandModel> brands = ViewData[typeof(BrandModel).Name] as IList<BrandModel>;

    string homeUrl = HomeController.GetPathIndex();
    string brandUrl = BrandController.GetPathIndex();
 %>

   <div id="divBrands" class="left"  style=" width:70%;" >
               <div class="box b" style=" margin-bottom:3px">
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="连锁品牌" href="<%=brandUrl %>" >连锁品牌</a>  </li>

           </ul>
          
        </div>
   
   <% Html.RenderPartial("DivHotBrand"); %>
</div>

  <div class="right" style=" width:29%">
     <div class="b" style=" margin-bottom:3px">
    <h3>快速搜索</h3>
    <div class="box">
      <% Html.RenderPartial("HotelQuickSearchBox"); %>
    </div>
    </div>
    
    
          <div class="b" style=" margin-bottom:3px">
        		      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
        </div>
    
   <div  >
    <% Html.RenderPartial("DivHotBookingHotelTabs"); %>
    </div>
    
        
  </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">



<% string basePath = WebUtil.GetWebRootPath(); %>

<script src="<%=basePath %>/js3rd/jquery.masonry/jquery.masonry.min.js"></script>

<script type="text/javascript">

    
  


</script>

<style>
 
   .divBrand
{
   width:9.3%;  
}

</style>
</asp:Content>
