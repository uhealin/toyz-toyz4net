<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import  Namespace="Toyz4net.Core.Util"%>
<%@ Import Namespace="ZDSL.Model.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>

<%  
    string basePath = WebUtil.GetWebRootPath();
    DateTime today = DateTime.Now;
    DateTime tomorrow = today.AddDays(1);
    string strCheckInDate = WebUtil.GetParamAttr("checkInDate",today.ToString("yyyy-MM-dd"));
    string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", tomorrow.ToString("yyyy-MM-dd"));
    string strCityName = WebUtil.GetParamAttr("cityName", "广州");
    string strKeyWord = WebUtil.GetParamAttr("keyword","");
    string strStar = WebUtil.GetParamAttr("star","");
    string strPriceRegexp = WebUtil.GetParamAttr("priceRegexp","");
    PublicBiz publicBiz=PublicBiz.GetInstant();
    HotKeywordModel hotKeyword = publicBiz.getCurrKeyWord();
%>

<form method="get" action="<%=basePath %>/Public/Geo/Index">
 
  <table  id="tableHotelSearch" class="tableCondition"  style=" width:99.5%;" >
  
      <tr>
         <th>城市：</th>
         <td><div id="inputCityPicker" ></div>
         
         </td>
         <th>入店日期：</th>
         <td><input name="checkInDate" value="<%=strCheckInDate %>"   style=" width:80px" readonly /></td>
         <th>离店日期：</th>
         <td style=" text-align:right"><input name="checkOutDate" style=" width:80px" value="<%=strCheckOutDate %>" readonly /></td>
         

      </tr>
      

      
      <tr>
         <th>价格范围：</th>
         <td>
                         <select  id="inputPriceRegexp"  name="priceRegexp">
                <option  value="">不限</option>
                <option value="gt@(700)">700以上</option>
                <option value="between@(500),(700)">500-700</option>
                <option value="between@(300),(500)">300-500</option>
                <option value="between@(200),(300)">200-300</option>
                <option value="lt@(200)">200以下</option>
                </select>
         </td>
         <th>酒店星级：</th>
         <td>
             <select id="inputStar"  name="star" >
                <option  value="">不限</option>
                <option value="0">经济型</option>
                <option value="1">一星级</option>
                <option value="2">二星级</option>
                <option value="3">三星级</option>
                <option value="4">四星级</option>
                <option value="5">五星级</option>
                
              </select>
         </td>
         <td rowspan="2" colspan="2" style="text-align:right">
           <button type="submit" style=" height:45px;">搜索酒店</button>
         </th>
         </td>

      </tr>

       <tr>
       <th>关键词：</th>
         <td colspan="2" ><input name="keyword" style=" width:250px"  value="<%=strKeyWord %>" /></td>
         <td colspan="2"><div style=" width:80%; float:right" class="ui-state-highlight ui-corner-all ignore " id="divSearchLocation"><span class="spanSearchLocation pointer ">点击查找区域</span></div></td>
       </tr>
   
      
      


  
  </table>
   
   
    


   
   
   <script type="text/javascript">

       $(document).ready(function() {


           SetHotelConditionForm("#tableHotelSearch");

   


       });
   </script>
 

</form>



