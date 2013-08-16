<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<%
    IList<BrandModel> listBrand = ViewData[BaseZdController.VD_KEY_HOT_BRAND_LIST] as IList<BrandModel>;
    string basePath = WebUtil.GetWebRootPath();
    
 %>
       <div class="divTabs">
        <ul>
        <% 
            foreach (string key in BrandModel.GetDictType().Keys)
            {
                string name = BrandModel.GetDictType()[key];
         %>
             <li><a href="#tab-<%=key %>"><%=name %>连锁酒店</a></li>
         <%} %>
         </ul>
         
         
       <ul>
        <% 
            foreach (string key in BrandModel.GetDictType().Keys)
            {
                string name = BrandModel.GetDictType()[key];
         %>
             <div id="tab-<%=key %>" style=" overflow:auto">
             
             <%
                 foreach (BrandModel brand in listBrand)
                 {
                     if (brand.type!=key) continue;
                     string brandUrl = BrandController.GetPathBrandHotel(brand.brandID);
               %>
             <div class="divBrand" style=" padding:1%; text-align:center" >
          <a href="<%=brandUrl %>"  title="<%=brand.brandNameLong%>" > <img title="<%=brand.brandNameLong%>" class="imgBrand" alt="<%=brand.brandNameLong%>" src="<%=brand.picURL %>" /><br/>
              <span class="spanBrandName"><%=brand.brandNameLong%> </span> </a>  
            </div>
             
             <%} %>
             </div>
         <%} %>
         </ul>
         
         
      </div>
	
			        
		             