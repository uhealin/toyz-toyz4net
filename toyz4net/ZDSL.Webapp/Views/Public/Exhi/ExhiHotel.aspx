<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <% 
     string basePath = WebUtil.GetWebRootPath();
     ExhiModel exhi = ViewData[typeof(ExhiModel).Name] as ExhiModel;
    %>
	<%=string.Format(PublicBiz.getCurPageSeo().exhiHotelTitle,exhi.id, exhi.name, exhi.address, exhi.startDate.ToShortDateString(), exhi.busName) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% 
     string basePath = WebUtil.GetWebRootPath();
     ExhiModel exhi = ViewData[typeof(ExhiModel).Name] as ExhiModel;
     IList<ExhiModel> refExhis = ViewData["refExhis"] as IList<ExhiModel>;
     GeoModel geo = BaseZdBiz.Load<GeoModel>(exhi.geoFk);
     string homeUrl = HomeController.GetPathIndex();
     string exhiUrl = ExhiController.GetPathIndex();
    %>
    
     <div class="left" style=" width:70%">
           <div class="box b" style=" margin-bottom:3px">
           
                        
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="展会信息" href="<%=exhiUrl %>" >展会信息</a> >> </li>
              <li><%=exhi.name %></li>
           </ul>
            <br />
            <% Html.RenderPartial("ExhiSearchBox"); %>
          </div>
          <div class="b">
             <h3>展会信息</h3>
             <div class="box" style=" padding:5%">
                
                <h2 style=" text-align:center"><%=exhi.name%></h2>
        <br/>
                <div style=" text-align:center; padding-left :10%" >
                   <table cellSpacing=0 cellPadding=0  style=" width:80%" class="ui-state-highlight ui-corner-all">
                      
                      <tr>

                        <th>所在城市：</th>
                        <td><a href="<%=basePath %>/Public/Geo/Index?cityName=<%=geo.cityName %>" ><%=geo.cityName %></a></td>
                        <th >举办场所：</th>
                        <td><%=exhi.siteName %> </td>
                      </tr>
                      <tr>
                        <th >开始日期：</th>

                        <td><%=exhi.startDate.ToShortDateString() %></td>
                        <th >结束日期：</th>
                        <td><%= exhi.endDate.ToShortDateString() %></td>
                      </tr>
                      <tr>
                        <th >所属行业：</th>
                        <td><%=exhi.busName %></td>

                        <th>联系地址：</th>
                        <td><%=exhi.address %> </td>
                      </tr>
                     
                        </table>
                        </div>
                <div class="context">
                 <pre><%=exhi.msg %></pre>
                </div>
                </div>
             </div>
      
            <div class="b" style=" margin-top:3px">
               <h3>更多展会</h3>
                  
                   <% foreach (ExhiModel refExhi in refExhis)
                      {
                          string url = ExhiController.GetPathExhiHotel(exhi); 
                          %>
                      <div class="box bottomline">
                     <div class="left">
                        <a href="<%=url %>"><%= refExhi.name %></a>
                      </div>
                      <div class="right" style=" text-align:right">
                        <%=refExhi.startDate.ToShortDateString() %>~<%=refExhi.endDate.ToShortDateString() %>
                      </div>
                    </div>
                   <%} %>
               
            </div>
         
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
     
     <div class="b" style=" margin-top:3px">
         <h3>相关酒店</h3>
          <div class="box" id="divExhiHotelList">
           
          </div>
     </div>
     
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

   <% 
     string basePath = WebUtil.GetWebRootPath();
     ExhiModel exhi = ViewData[typeof(ExhiModel).Name] as ExhiModel;
       
    %>
    
    
    <script type="text/javascript">

        function reflashHotels() {

            //condition["rows"] = rows;
            _condition["viewName"] = "UlGeoHotelSearchList";
            _condition["hotelId"] = "<%=exhi.hotelIdArray %>";
            _condition["rows"] = 20;
            var pagenum = _condition["page"];
            var params = $.param(_condition);
            StartPageWating("酒店搜索进行中");
            $("#divExhiHotelList").load("<%=basePath %>/Public/Hotel/ViewHotelSearch?" + params, function() {
                EndPageWating();
                var total = parseInt($(this).find("#spanTotal").attr("total"));
                var p = total % _condition["rows"] > 0 ? 1 : 0;
                var pageCount = parseInt(total / _condition["rows"]) + p;

               // $("#pager").pager({ pagenumber: pagenum, pagecount: pageCount, buttonClickCallback: clickPager });
                // SetHotelCondition(condition);
                //drawConditionBox();
            });
        }


        $(document).ready(function() {
           
            reflashHotels();

        });    // $(document)
    
    </script>


    <style>
     
       .divHotelPrice
       {
       	 float:left;
       	}
       	
       .divStar
       {
       	 float:right;
       	}	
    
    </style>

</asp:Content>
