<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath(); %>
<form method="get" action="<%=basePath %>/Public/Hotel/Search">
 
  <table  id="tableHotelQuickSearch" class="tableCondition" style=" width:100%">
  
      <tr>
         <th>城市：</th>
         <th><div id="inputCityPicker" ></div></th>
      </tr>
      
      <tr>
         <th>入店日期：</th>
         <th><input name="checkInDate" style=" width:100px" readonly /></th>
      </tr>
      
      <tr>
         <th>离店日期：</th>
         <th><input name="checkOutDate" style=" width:100px" readonly /></th>
      </tr>
      
      <tr>
         <th>关键词：</th>
         <th><input name="keyword" /></th>
      </tr>
      
      <tr>
         <th colspan="2">
         <a href="<%=basePath %>/Public/Geo/Index">从地图中搜索</a>
           <button>搜索</button>
         </th>
      </tr>

  
  </table>
   
   <script type="text/javascript">

       $(document).ready(function() {


       SetHotelConditionForm("#tableHotelQuickSearch");
     });
     
   </script>  
 

</form>


