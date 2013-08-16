<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Public.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import  Namespace="Toyz4net.Core.Util"%>
<%@ Import  Namespace="ZDSL.Model.Data"%>
<%@ Import  Namespace="ZDSL.Model.Public"%>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers.Public" %>
<%@ Import  Namespace="ZDSL.Biz"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%
        GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel;
        PageSeoModel seo = PublicBiz.getCurPageSeo();
        string title = string.Format(seo.cityTitle, geo.cityCode, geo.cityName);
	    string strGeoClId = WebUtil.GetParamAttr("geoClId","");
         string strGeoDId = WebUtil.GetParamAttr("geoDId", "");
         string strKeyWord = WebUtil.GetParamAttr("keyword", "");
         string strCheckInDate = WebUtil.GetParamAttr("checkInDate",DateTime.Now.ToShortDateString());
         GeoLocationModel geoLocation = ViewData[typeof(GeoLocationModel).Name] as GeoLocationModel;
  
	        
         if (!string.IsNullOrEmpty(strGeoClId)) {
             title = string.Format( seo.cityClTitle,geo.cityCode,geo.cityName,geoLocation.locationId,geoLocation.name);

         }
         else if (!string.IsNullOrEmpty(strGeoDId))
         {
             title = string.Format(seo.cityDTitle, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name);

         }else if (!string.IsNullOrEmpty(strKeyWord))
         {
             title = string.Format( seo.hotelSearchTitle,geo.cityName,strKeyWord,strCheckInDate);
         }
	          
	        %>
	 <%=title %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%  
    string basePath = WebUtil.GetWebRootPath();
    DateTime today = DateTime.Now;
    DateTime tomorrow = today.AddDays(1);
    string strCheckInDate = WebUtil.GetParamAttr("checkInDate",today.ToString("yyyy-MM-dd"));
    string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", tomorrow.ToString("yyyy-MM-dd"));
    string strCityName = WebUtil.GetParamAttr("cityName", "广州");
    string strKeyWord = WebUtil.GetParamAttr("keyword","");
    string strStar = WebUtil.GetParamAttr("star","");
    string strPirceRegexp = WebUtil.GetParamAttr("priceRegexp","");
    string strGeoClId = WebUtil.GetParamAttr("geoClId","");
    string strGeoDId = WebUtil.GetParamAttr("geoDId", "");
    string strGeoLlld = WebUtil.GetParamAttr("geoLlId", "");
    string strLocationName = WebUtil.GetParamAttr("locationName", "");

    GeoModel geo = ViewData[BaseZdController.VD_KEY_GEO] as GeoModel;
    IList<GeoCommercialLocationModel> geoCls = ViewData[BaseZdController.VD_KEY_GEO_CLS] as IList<GeoCommercialLocationModel>;
    IList<GeoDistrictsModel> geoDs = ViewData[BaseZdController.VD_KEY_GEO_DS ] as IList<GeoDistrictsModel>;
    IList<GeoLandmarkLocationModel> geoLls = ViewData[BaseZdController.VD_KEY_GEO_LLS] as IList<GeoLandmarkLocationModel>;
    IList<BrandModel> brands = ViewData[typeof(BrandModel).Name] as IList<BrandModel>;

 
    GeoLocationModel geoLocation = null;
    string locationUrl = "";
    string geoLocationType = "";
    if (!string.IsNullOrEmpty(strGeoDId)) {
        foreach (GeoDistrictsModel geoD in geoDs) {
            if (geoD.locationId == strGeoDId) {
                geoLocation = geoD;
                geoLocationType = "行政区";
                locationUrl = HotelController.GetPathDHotel(geo,geoD);
                break;
            }
            
        }
    }

    if (!string.IsNullOrEmpty(strGeoClId))
    {
        foreach (GeoCommercialLocationModel geoCl in geoCls)
        {
            if (geoCl.locationId == strGeoClId)
            {
                geoLocation = geoCl;
                geoLocationType = "商业区";
                locationUrl = HotelController.GetPathClHotel(geo, geoCl);
                break;
            }
        }
    }

    if (!string.IsNullOrEmpty(strGeoLlld))
    {
        foreach (GeoLandmarkLocationModel geoLl in geoLls)
        {
            if (geoLl.locationId == strGeoLlld)
            {
                geoLocation = geoLl;
                geoLocationType = "地标";
                break;
            }
        }
    }
    string homeUrl = HomeController.GetPathIndex();
    string cityUrl = HotelController.GetPathCityHotel(geo);

    string lastNav = "";

    if (!string.IsNullOrEmpty(strStar)) {
        if (strStar == "1,2") {
            lastNav += "经济型";
        }
        else if (strStar == "3") {
            lastNav += "三星级";
        }
        else if (strStar == "4")
        {
            lastNav += "四星级";
        }
        else if (strStar == "5")
        {
            lastNav += "五星级";
        }
    }

    if (!string.IsNullOrEmpty(strPirceRegexp))
    {
        ToyzNumberRangeObject priceRange = NumberUtil.ParseToyz(strPirceRegexp) as ToyzNumberRangeObject;
        lastNav += string.Format(" {0}-{1} ", priceRange.min, priceRange.max);
    }
    
    if (!string.IsNullOrEmpty(strKeyWord))
    {
        lastNav += " "+strKeyWord + " ";
    }
    
    
   
%>
    <div class="right" style=" width:74%">

        <div class="box b" style=" margin-bottom:3px" >
          
           <ul id="ulNav">
           <li>当前位置:</li>
              <li><a title="酒店预订" href="<%=homeUrl %>" >酒店预订</a> >> </li>
              <li><a title="<%=geo.cityName %>酒店预订" href="<%=cityUrl %>" ><%=geo.cityName %>酒店</a> >> </li>
              <%if (geoLocation != null)
                { %>
              <li><a title="<%=geo.cityName+geoLocation.name %>酒店预订" href="<%=locationUrl %>" ><%=geoLocation.name %>酒店</a> >> </li>
              <%} %>
              <li><%=lastNav %>&nbsp;  酒店搜索</li>
           </ul>
          
        </div>
     
        <div class="b">
        <h3><%=strCityName%> &nbsp; <%= strLocationName %>  酒店
        </h3>
        
        <div  class="box b">
             <div class="left">查询条件:</div><div class="divGeoLoction left "></div><div class="divConditionList left "></div>
             <a href="?cityName=<%=strCityName %>&checkInDate=<%=strCheckInDate %>&checkOutDate=<%=strCheckOutDate %>">&nbsp;&nbsp;清空搜索条件</a>
        </div>
        <div class="divCondition" style=" padding:15px">
  

         
         <ul>
           <li>房价：</li>


            <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="">不限</span> </li>
           <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="between@(0),(200)">200元以下</span> </li>
           <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="between@(200),(300)">200-300</span></li>
           <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="between@(300),(500)">300-500</span></li>
           <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="between@(500),(700)">500-700</span></li>
           <li class="liCondition" ><span class="spanRoomPrice" qn="priceRegexp" code="gt@(700)">700元以上</span></li>
         </ul>
         
         <ul>
           <li>星级：</li>
           <li class="liCondition" ><span class="spanStar" qn="star" code="">不限</span> </li>
           <li class="liCondition" ><span class="spanStar" qn="star" code="1,2">经济型</span> </li>
           <li class="liCondition" ><span class="spanStar" qn="star" code="3">三星级</span></li>
           <li class="liCondition" ><span class="spanStar" qn="star" code="4">四星级</span></li>
           <li class="liCondition" ><span class="spanStar" qn="star" code="5">五星级</span></li>
         </ul>
         

       
         
         <ul>
           <li>品牌：</li>
           <li class="liCondition" ><span class="spanBrand" qn="brandName" code="">不限</span> </li>
           <% foreach (BrandModel brand in brands)
              { %>
           <li class="liCondition" ><span class="spanBrand" qn="brandName" code="<%=brand.brandName %>"><%= brand.brandName %></span> </li>
           <%} %>
           
         </ul>
        
        
        
         <ul>
           <li>排序：</li>

           <li class="liCondition" ><span class="spanOrderbyCode" qn="orderbyCode" code="">不限</span> </li>
           <li class="liCondition" ><span class="spanOrderbyCode" qn="orderbyCode" code="star">按星级</span> </li>
           <li class="liCondition" ><span class="spanOrderbyCode" qn="orderbyCode" code="price">按价格</span></li>
           <li class="liCondition" >
                 <select  onchange="condition['orderbyType']=this.value;">
                 <option value="desc">降序</option>
                 <option value="asc">升序</option>
                 </select> 
           </li>
         </ul>
         
        </div>
        
        
        <div id="divHotels" class="box"> 
        </div>
        <div id="pager" ></div>
        
        </div>
        
    </div>

    <div class="left" style=" width:25%">
                <div id="divHotelSearchBox" class="b" style=" margin-bottom:3px">
          <h3>快速查询</h3>
          <% Html.RenderPartial("HotelQuickSearchBox"); %>
        </div>
                <div class="b" style="margin-top:3px">
        		      <h3 >热门城市</h3>
			      <div id="divHotCity" class="box">
                  <% Html.RenderPartial("UlHotCity"); %>
                  </div>
        </div>
    
        <div class="divCondition" style=" margin-top:3px" >
        
         <% Html.RenderPartial("DivGeoLocation"); %> 

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<%  
    string basePath = WebUtil.GetWebRootPath();
    DateTime today = DateTime.Now;
    DateTime tomorrow = today.AddDays(1);
    string strCheckInDate = WebUtil.GetParamAttr("checkInDate",today.ToString("yyyy-MM-dd"));
    string strCheckOutDate = WebUtil.GetParamAttr("checkOutDate", tomorrow.ToString("yyyy-MM-dd"));
    string strCityName = WebUtil.GetParamAttr("cityName", "广州");
    string strKeyWord = WebUtil.GetParamAttr("keyword","");
     string strStar = WebUtil.GetParamAttr("star","");
     string strPirceRegexp = WebUtil.GetParamAttr("priceRegexp", "");
     string strGeoClId = WebUtil.GetParamAttr("geoClId", "");
     string strGeoDId = WebUtil.GetParamAttr("geoDId", "");
     string strGeoLlld = WebUtil.GetParamAttr("geoLlId", "");

     IList<GeoCommercialLocationModel> geoCls = ViewData[BaseZdController.VD_KEY_GEO_CLS] as IList<GeoCommercialLocationModel>;
     IList<GeoDistrictsModel> geoDs = ViewData[BaseZdController.VD_KEY_GEO_DS] as IList<GeoDistrictsModel>;
     IList<GeoLandmarkLocationModel> geoLls = ViewData[BaseZdController.VD_KEY_GEO_LLS] as IList<GeoLandmarkLocationModel>;
     IList<BrandModel> brands = ViewData[typeof(BrandModel).Name] as IList<BrandModel>;

     GeoLocationModel geoLocation = null;
     string geoLocationType = "";
     if (!string.IsNullOrEmpty(strGeoDId))
     {
         foreach (GeoDistrictsModel geoD in geoDs)
         {
             if (geoD.locationId == strGeoDId)
             {
                 geoLocation = geoD;
                 geoLocationType = "行政区";
                 break;
             }

         }
     }

     if (!string.IsNullOrEmpty(strGeoClId))
     {
         foreach (GeoCommercialLocationModel geoCl in geoCls)
         {
             if (geoCl.locationId == strGeoClId)
             {
                 geoLocation = geoCl;
                 geoLocationType = "商业区";
                 break;
             }
         }
     }

     if (!string.IsNullOrEmpty(strGeoLlld))
     {
         foreach (GeoLandmarkLocationModel geoLl in geoLls)
         {
             if (geoLl.locationId == strGeoLlld)
             {
                 geoLocation = geoLl;
                 geoLocationType = "地标";
                 break;
             }
         }
     }
    
   
   %>


    <script type="text/javascript">

       
        

/*
        condition["checkInDate"] = "<%=strCheckInDate %>";
        condition["checkOutDate"] = "<%=strCheckOutDate %>";
        condition["cityName"] = "<%=strCityName %>";
        condition["star"] = "<%=strStar %>";
        condition["priceRegexp"] = "<%=strPirceRegexp %>";
        condition["keyword"]="<%=strKeyWord %>";
        condition["geoClId"]="<%=strGeoClId %>";
        condition["geoDId"]="<%=strGeoDId %>";
        condition["geoLlId"] = "<%=strGeoLlld %>";
        condition["rows"] = 15;
        condition["page"] = 1;
      */  
        

        function reflashHotels() {

            //condition["rows"] = rows;
            _condition["viewName"] = "TableHotelSearchList";
            var pagenum = _condition["page"];
            var params = $.param(_condition);
            StartPageWating("酒店搜索进行中");
            $("#divHotels").load("<%=basePath %>/Public/Hotel/ViewHotelSearch?" + params, function() {
                EndPageWating();
                var total = parseInt($(this).find("#spanTotal").attr("total"));
                var p = total % _condition["rows"] > 0 ? 1 : 0;
                var pageCount = parseInt(total / _condition["rows"])+p;
                
                $("#pager").pager({ pagenumber: pagenum, pagecount: pageCount, buttonClickCallback: clickPager });
                // SetHotelCondition(condition);
                drawConditionBox();
            });
        }

        function clickPager(pagenum) {
            _condition["page"] = pagenum;
            reflashHotels();
        }

        function drawConditionBox() {
            $(".spanRoomPrice").each(function() {
            var code = $(this).attr("code");
               $(this).removeClass("selected");
                if (code == _condition["priceRegexp"]) {
                    $(this).addClass("selected");
                }
            });

            $(".spanStar").each(function() {
                var code = $(this).attr("code");
                $(this).removeClass("selected");
                if (code == _condition["star"]) {
                    $(this).addClass("selected");
                }
            });

            $(".spanOrderbyCode").each(function() {
                var code = $(this).attr("code");
                $(this).removeClass("selected");
                if (code == _condition["orderbyCode"]) {
                    $(this).addClass("selected");
                }
            });

            $(".spanBrand").each(function() {
                var code = $(this).attr("code");
                $(this).removeClass("selected");
                if (code == _condition["brandName"]) {
                    $(this).addClass("selected");
                }
            });

            $(".divConditionList").empty();
            


            
            $(".divCondition").find("span.selected").each(function() {
            var text = $.trim($(this).text());
                if (text == "不限") return true;
                var code = $(this).attr("code");
                var qn = $(this).attr("qn");
                var span = $("<span class='ui-state-highlight ui-corner-all'></span>");
                span.text(text);
                var aDelete = $("<a href='javascript:void();'><img src='<%=basePath %>/image/icon.cancel_round.png' /></a>");
                aDelete.click(function() {
                    _condition[qn] = "";
                    _condition["page"] = 1;
                    reflashHotels();
                });
                span.append("&nbsp;").append(aDelete);
                $(".divConditionList").append(span);
            });
            

            
        }


        $(document).ready(function() {
        _condition["rows"] = 8;
        _condition["page"] = 1;
            reflashHotels();
          
                        <%if (geoLocation != null){ %>
  
               var geoLoctionId= "<%=geoLocation.locationId %>";
               var geoLoctionName="<%=geoLocation.name %>";
               var spanGeo = $("<span class='ui-state-highlight ui-corner-all'></span>");
               spanGeo.text(geoLoctionName);
               var aDeleteGeo = $("<a href='javascript:void();'><img src='<%=basePath %>/image/icon.cancel_round.png' /></a>");
               aDeleteGeo.click(function() {
                    _condition["geoClId"] = "";
                    _condition["geoDId"] = "";
                    _condition["geoLlId"] = "";
                    _condition["page"] = 1;
                    spanGeo.remove();
                    reflashHotels();
                    
                });
                spanGeo.append("&nbsp;").append(aDeleteGeo);
                 $(".divGeoLoction").append(spanGeo);
              
            <%} %>


            $(".spanRoomPrice").click(function() {
                var text = $(this).text();
                var code = $(this).attr("code");
                _condition["priceRegexp"] = code;
                //appendConditionBox(text, "priceRegexp");
                reflashHotels();
                //   reflashHotels({ priceRegexp: code });
            });

            $(".spanStar").click(function() {
                var text = $(this).text();
                var code = $(this).attr("code");
                _condition["star"] = code;
                //appendConditionBox(text, "star");
                reflashHotels();
                //  reflashHotels({ star: code });
            });

            $(".spanBrand").click(function() {
                var code = $(this).attr("code");
                _condition["brandName"] = code;
                reflashHotels();
                //    reflashHotels({ brandId: code });
            });

            $(".spanOrderbyCode").click(function() {
            var code = $(this).attr("code");
            
                _condition["orderbyCode"] = code;
                reflashHotels();
                //    reflashHotels({ brandId: code });
            });



        });
    
    </script>
</asp:Content>
