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

<form method="post" action="<%=basePath %>/Public/Exhi/Index">
  <table  id="tableExhiSearch" class="tableCondition"    >
  
      <tr>
         <th >城市：</th>
         <td ><div id="inputExhiCityPicker" ></div></td>
         <th >展会名：</th>
         <td><input name="keyword"  style=" width:200px" /></td>
         <td><button type="submit">搜索展会</button></td>
      </tr>
   </table>   
   
</form>

<script type="text/javascript">
    var exhiCityPicker;
    $(document).ready(function() {

    //SetHotelConditionForm("#tableExhiSearch");
    exhiCityPicker = new CityPicker({ layid: "inputExhiCityPicker", inputName: "cityName", inputValue: _condition["cityName"] });
    exhiCityPicker.begin(); 
    });

</script>