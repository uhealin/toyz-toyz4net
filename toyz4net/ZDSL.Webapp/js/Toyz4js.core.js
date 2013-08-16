
//require jquery easyui






// 声明一个全局对象Namespace，用来注册命名空间
Class = new Object();

// 全局对象仅仅存在register函数，参数为名称空间全路径，如"Grandsoft.GEA"
Class.forName = function(fullNS)
{
    // 将命名空间切成N部分, 比如Grandsoft、GEA等
    var nsArray = fullNS.split('.');
    var sEval = "";
    var sNS = "";
    for (var i = 0; i < nsArray.length; i++)
    {
        if (i != 0) sNS += ".";
        sNS += nsArray[i];
        // 依次创建构造命名空间对象（假如不存在的话）的语句
        // 比如先创建Grandsoft，然后创建Grandsoft.GEA，依次下去
        sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = new Object();"
    }
    if (sEval != "") eval(sEval);
}

Class.forName("Toyz4js.util");

Toyz4js.util = {
    isDateFormat: function(pattern, val) {
        if (pattern.length != val.length) return false;
        var reg = pattern;
        reg = reg.replace(/yyyy/, "[0-9]{4}");
        reg = reg.replace(/yy/, "[0-9]{2}");
        reg = reg.replace(/MM/, "((0[1-9])|1[0-2])");
        reg = reg.replace(/M/, "(([1-9])|1[0-2])");
        reg = reg.replace(/dd/, "((0[1-9])|([1-2][0-9])|30|31)");
        reg = reg.replace(/d/, "([1-9]|[1-2][0-9]|30|31))");
        reg = reg.replace(/HH/, "(([0-1][0-9])|20|21|22|23)");
        reg = reg.replace(/H/, "([0-9]|1[0-9]|20|21|22|23)");
        reg = reg.replace(/mm/, "([0-5][0-9])");
        reg = reg.replace(/m/, "([0-9]|([1-5][0-9]))");
        reg = reg.replace(/ss/, "([0-5][0-9])");
        reg = reg.replace(/s/, "([0-9]|([1-5][0-9]))");
        var regexp = new RegExp("^" + reg + "$");
        return regexp.test(val);
    }
,
    parseDateTime: function(pattern, str) {
        if (pattern.length != str.length) return null;
        var yearIndex = pattern.indexOf("yyyy");
        var monthIndex = pattern.indexOf("MM");
        var dayIndex = pattern.indexOf("dd");
        var hourIndex = pattern.indexOf("HH");
        var minuteIndex = pattern.indexOf("mm");
        var secondIndex = pattern.indexOf("ss");
        var d = new Date();
        try {
            //	alert(pattern+":"+str+":"+yearIndex+":"+monthIndex+":"+dayIndex);
            if (yearIndex == -1 || monthIndex == -1 || dayIndex == -1) return null;
            var year = parseInt(str.substr(yearIndex, 4));
            var month = parseInt(str.substr(monthIndex, 2));
            var day = parseInt(str.substr(dayIndex, 2));
            d.setFullYear(year, month - 1, day);
            if (hourIndex != -1) { var hour = parseInt(str.substr(hourIndex, 2)); d.setHours(hour); }
            if (minuteIndex != -1) { var minute = parseInt(str.substr(minute, 2)); d.setMinutes(minute); }
            if (secondIndex != -1) { var second = parseInt(str.substr(secondIndex, 2)); d.setSeconds(second); }
            return d;
        } catch (ex) { return null; }
    }
,
    strToJson: function(jsonStr) {
        return eval('(' + jsonStr + ')');
    }
,
    handleJsonResult: function(json, handler) {
        var msg = json["msg"] || "";
        var title = json["title"] || "操作";
        var icon = json["icon"] || "info";
        var action = json["action"] || "show";
        var result = false;
        if (action == "show") {
            $.messager.show({ title: title
			                , msg: msg
			                , timeout: 5000
			                , showType: "slide"
			                , showSpeed: 500
            });
        } else if (action == "alert") {
            $.messager.alert(title, msg, icon, handler == null ? function() { return true; } : handler);
        } else if (action == "confirm") {
            $.messager.confirm(title, msg, handler == null ? function(y) { return y; } : handler);
        } else if (action == "prompt") {
            $.messager.prompt(title, msg, handler == null ? function(val) { return val; } : handler);
        }
    }

,
    getIframeContext: function(src) {
        return "<iframe frameborder='0' src='" + src + "' style='width:100%;height:100%;overflow:hidden;'></iframe>";
    }

, jsonToStr: function(obj) {
    switch (typeof (obj)) {
        case 'object':
            var ret = [];
            if (obj instanceof Array) {
                for (var i = 0, len = obj.length; i < len; i++) {
                    ret.push(Toyz4js.util.jsonToStr(obj[i]));
                }
                return '[' + ret.join(',') + ']';
            }
            else if (obj instanceof RegExp) {
                return obj.toString();
            }
            else {
                for (var a in obj) {
                    ret.push(a + ':' + Toyz4js.util.jsonToStr(obj[a]));
                }
                return '{' + ret.join(',') + '}';
            }
        case 'function':
            return 'function() {}';
        case 'number':
            return obj.toString();
        case 'string':
            return "\"" + obj.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g, function(a) { return ("\n" == a) ? "\\n" : ("\r" == a) ? "\\r" : ("\t" == a) ? "\\t" : ""; }) + "\"";
        case 'boolean':
            return obj.toString();
        default:
            return obj.toString();

    }

}  //Toyz4js.util={




}
 


 


 
 
 



 
