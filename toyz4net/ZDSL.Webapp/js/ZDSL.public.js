
function doFav() {
    if (document.all) {
        try {
            window.external.addFavorite(window.location.href, document.title);
        } catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }

    } else if (window.sidebar) {
        window.sidebar.addPanel(document.title, window.location.href, "");
    } else {
        alert("加入收藏失败，请使用Ctrl+D进行添加");
    }  


} 


function ScrollTop() {
    window.scrollTo(0, 0);
}

function ToUrl(url, params) {
   // for (var key in params) {
    //    alert(key+":"+params[key]);
   // }
    var param = $.param(params);
   // alert(param);
    document.location.href = url + "?" + param;
}



function UrlQuery() {
    var aa = "dd";

    var name, value, i;

    var str = location.href;

    var num = str.indexOf("?")

    str = str.substr(num + 1);

    var arrtmp = str.split("&");

    for (i = 0; i < arrtmp.length; i++) {

        num = arrtmp[i].indexOf("=");

        if (num > 0) {
            name = arrtmp[i].substring(0, num);
            value = arrtmp[i].substr(num + 1);
            this[name] = decodeURIComponent(value);
        }

    }

}



function FormatNullOrBlank(value, defaultValue) {

    if (value == null || value.length == 0) {
        return defaultValue;
    } else {
    return value;
    }
}

/*
function SetCookie(name, value)//两个参数，一个是cookie的名子，一个是值
{
    var Days = 20; //此 cookie 将被保存 30 天
    var exp = new Date();    //new Date("December 31, 9998");
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}


function GetCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;

}
function DelCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = GetCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
*/
var _basePath;
var cityPicker=null;
function GetHotelCondition() {

    var condition = {};
    var urlQuery = new UrlQuery();
        var valCheckInDate = $.datepicker.formatDate("yy-mm-dd", Date.today().addDays(1));
        var valCheckOutDate = $.datepicker.formatDate("yy-mm-dd", Date.today().addDays(2));
        //$("#tableHotelQuickSearch").find("input[name=checkInDate]").val(valToday);
        //$("#tableHotelQuickSearch").find("input[name=checkOutDate]").val(valTomorrow);
        condition["checkInDate"] = FormatNullOrBlank(urlQuery["checkInDate"], valCheckInDate);
        condition["checkOutDate"] = FormatNullOrBlank(urlQuery["checkOutDate"], valCheckOutDate);
        condition["cityName"] = FormatNullOrBlank(urlQuery["cityName"], "广州");
        condition["star"] = FormatNullOrBlank(urlQuery["star"], "");
        condition["priceRegexp"] = FormatNullOrBlank(urlQuery["priceRegexp"], "");
        condition["keyword"] = FormatNullOrBlank(urlQuery["keyword"], "");
        condition["geoClId"] = FormatNullOrBlank(urlQuery["geoClId"], "");
        condition["geoDId"] = FormatNullOrBlank(urlQuery["geoDId"], "");
        condition["geoLlId"] = FormatNullOrBlank(urlQuery["geoLlId"], "");
        condition["brandName"] = FormatNullOrBlank(urlQuery["brandName"], "");
        condition["orderbyCode"] = FormatNullOrBlank(urlQuery["orderbyCode"], "");
        condition["orderbyType"] = FormatNullOrBlank(urlQuery["orderbyType"], "");
        //alert(condition["checkInDate"]);
    return condition;
    
}

/*
function SetHotelCondition(condition) {
    if (condition == null) return;
    for (var key in condition) {
        DelCookie(key);
        SetCookie(key, condition[key]);
    }
    
}
*/

function SetHotelConditionForm(parentRegexp) {
    var condition = _condition;
    $(parentRegexp).find("input[name=checkInDate]").val(condition["checkInDate"]);
    $(parentRegexp).find("input[name=checkOutDate]").val(condition["checkOutDate"]);
    $(parentRegexp).find("input[name=checkInDate]").datepicker({
        defaultDate: "0"
             , minDate: "0"
             , maxDate: "20"
             , changeMonth: false
             , numberOfMonths: 2
             , showOn: "both"
             , buttonImage: _basePath + "/image/icon.calendar.gif"
             , buttonImageOnly: true
             , onSelect: function(selectedDate) {
                 var _minDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, selectedDate).addDays(1);
                 var _maxDate = $.datepicker.parseDate($.datepicker._defaults.dateFormat, selectedDate).addDays(20);
                 var option = {
                     minDate: _minDate
                     , maxDate: _maxDate

                 };
                 $(parentRegexp).find("input[name=checkOutDate]").datepicker("option", option);
                 //$(parentRegexp).find("input[name=checkOutDate]").focus();

             }

    });

    $(parentRegexp).find("input[name=checkOutDate]").datepicker({
        defaultDate: "1"
              , minDate: "1"
               , changeMonth: false
               , numberOfMonths: 2
                            , showOn: "both"
             , buttonImage: _basePath + "/image/icon.calendar.gif"
             , buttonImageOnly: true
    });

    if (cityPicker == null) {
        cityPicker = new CityPicker({ layid: "inputCityPicker", inputName: "cityName", inputValue: condition["cityName"] });
        cityPicker.begin();
    }

    $(parentRegexp).find("input[name=star]").attr("value",condition["star"]);


    $(parentRegexp).find("input[name=priceRegexp]").val(condition["priceRegexp"]);

    $(parentRegexp).find("input[name=geoDId]").val(condition["geoDId"]);
    $(parentRegexp).find("input[name=geoClId]").val(condition["geoClId"]);




    $(parentRegexp).find("#inputGeoLocation").qtip({
        content: {
            url: _basePath + "/Public/Geo/DivGeoLoctaionTip",
            data: { cityName: condition["cityName"] },
            method: 'post'
        }
        , position: {
            corner: {
                target: 'bottomLeft'
            }
        }
        , style: {
            width: 400
        }
        , show: "click"
        , hide: {
        when: { event: 'unfocus'
            }
        }

        , api: {
            beforeShow: function() {
                var url = _basePath + "/Public/Geo/DivGeoLoctaionTip";
                var cityName = $(parentRegexp).find("input[name=cityName]").val();

                $.post(url, { cityName: cityName }, function(context) {
                    $(".qtip-content").html(context);
                    
                });

            }
        }


    });

/*
    $(parentRegexp).find("#divSearchLocation").click(function() {
        var url = _basePath + "/Public/Geo/DivGeoLoctaionTip";
        var cityName = $(parentRegexp).find("input[name=cityName]").val();
        alert(cityName);
        $.post(url, { cityName: cityName }, function(context) {
            $(parentRegexp).find(".spanSearchLocation").qtip({
                content: context
            , style: {
                width: 400
            }
            });
            $(parentRegexp).find(".spanSearchLocation").qtip("show");
        });
    })
*/
    
    
}



function GetDayOfWeekCn(dt) {

    var dow = dt.getDay();
    var dowCn = "";
    switch (dow) {
        case 0: dowCn = "周日"; break;
        case 1: dowCn = "周一"; break;
        case 2: dowCn = "周二"; break;
        case 3: dowCn = "周三"; break;
        case 4: dowCn = "周四"; break;
        case 5: dowCn = "周五"; break;
        case 6: dowCn = "周六"; break;
        default: { }
    }
    return dowCn;

}

var _condition = GetHotelCondition();


var _basePath = document.getElementById("base").href;

        function StartPageWating(title) {
             $("#divWaiting").dialog({
             title:title
			 ,modal: true
			 , draggable: false
			 , resizable: false
			 , height:65
			 ,width:228
		     });
         }
         
         function EndPageWating(){
             $("#divWaiting").dialog("destroy");
         }
         
         
         function SetPreUrl() {

          var url = document.location.href;
          $.post(_basePath+"/Public/Home/DoSetPreUrl", {preUrl:url});
          }



         $(document).ready(function() {

             $("button").button();
             $(".checkboxs,.radios").buttonset();
             $(".combobox").combobox();
             $(".divTabs").tabs(
              { event: "mouseover" }
             );
             $(".accordion").accordion();

             var href = document.location.href;
             //var subPath = href;

             $("#ulNav").find("li").each(function(index) {
                 if (href == $(this).find("a").attr("href")) {

                     $(this).addClass("active");
                     $(this).html($(this).find("a").html());
                 }
             });


             $.getJSON(_basePath+"/Public/Home/CheckLogin", function(json) {

                 if (json["code"] == 0) {
                     var member = json["attrs"]["member"];

                     $("#spanLoginStaue").empty().append("当前用户:" + member["id"]);
                     var a = $("<a>退出登录</a>");
                     a.attr("href", _basePath + "/Public/Home/DoLogout");
                     $("#spanLoginStaue").append("&nbsp;").append(a);
                 } else {
                     var a = $("<a>[请登录]</a>");
                     a.attr("href", _basePath + "/Public/Home/Login");
                     var a1 = $("<a>[免费注册]</a>");
                     a1.attr("href", _basePath + "/MemberAdmin/Reg/Index");
                     $("#spanLoginStaue").empty().append(a).append("&nbsp;").append(a1);
                 }

             });

             $("img").error(function(){
             $(this).attr("src", _basePath + "/image/img.null.jpeg");
             });


         });  //$(document)