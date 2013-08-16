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
    
%>
<div class="left" style=" width:74%">

  <input type="hidden" name="geoClId" />
  <input type="hidden" name="geoDId" />
  <table  id="tableHotelSearch" class="tableCondition"  style=" width:100%"  >
  
      <tr>
         <th style=" width:90px">城市：</th>
         <td style=" width:100px"><div id="inputCityPicker" ></div></td>
         <th style=" width:90px">入店日期：</th>
         <td><input name="checkInDate"    style=" width:80px" readonly /></td>
         
         
      </tr>
      
      <tr>
        <th>区域：</th>
         <td><input id="inputGeoLocation"  readonly /></td>
         <th >离店日期：</th>
         <td ><input name="checkOutDate" style=" width:80px"  readonly /></td>
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
                <option value="between@(0),(200)">200以下</option>
                </select>
         </td>
         <th>酒店星级：</th>
         <td>
             <select id="inputStar"  name="star" >
                <option  value="">不限</option>
                <option value="1,2">经济型</option>
                <option value="3">三星级</option>
                <option value="4">四星级</option>
                <option value="5">五星级</option>
                
              </select>
         </td>

        

      </tr>

       <tr>
       <th>酒店名称：</th>
         <td  ><input name="keyword"   value="<%=strKeyWord %>" /> </td>
         <td colspan="2">
         &nbsp; &nbsp;&nbsp;<button type="submit" >搜索酒店</button> &nbsp;
         <a href="<%=basePath %>/Public/Geo/Index">在地图中查找</a>
         
         </td>
         
         
         
        
       </tr>
   


  
  </table>
   
   </div>
   <div class="right" width="25%">
      <img src="<%=basePath %>/image/ad.zcyctf148x120.jpg" />
   </div>
    


   
   
   <script type="text/javascript">

       $(document).ready(function() {


           SetHotelConditionForm("#tableHotelSearch");






       });
   </script>
 

</form>



