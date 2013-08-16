
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
 


 


 
 
 



 
Class.forName("Toyz4js.custom.setting");

Toyz4js.custom.setting = {
	dbDateFormat:"yyyy-MM-dd"
    ,precision:2
}
Array.prototype.getUnionAttrStr=function(filed,split){
	//if(!(filed instanceof String))return "";
	try{
	//if(!filed.isString())return "";
	var unionStr=new String();
	if(!split){
		split=",";
	}
	for(var i=0;i<this.length;i++){
		var tempVal=this[i][filed];
		//if(!tempVal.isString())continue;
		unionStr+=tempVal+split;
	}
		unionStr=unionStr.substr(0,unionStr.lastIndexOf(split));
	return unionStr;
	}catch(ex){alert(ex);}
}


Array.prototype.getUnionStr = function(split) {
    //if(!(filed instanceof String))return "";
    try {
        //if(!filed.isString())return "";
        var unionStr = new String();
        if (!split) {
            split = ",";
        }
        for (var i = 0; i < this.length; i++) {
            var tempVal = this[i].toString();
            //if(!tempVal.isString())continue;
            unionStr += tempVal + split;
        }
        unionStr = unionStr.substr(0, unionStr.lastIndexOf(split));
        return unionStr;
    } catch (ex) { alert(ex); }
}



String.prototype.toJson=function(){
	try{
		var json=eval("("+this.toString()+")");
		return json;
	}catch(ex){
		return {};
	}
};


String.prototype.messager=function(handler){
    try{
      
       var json=this.toJson();
       var msg=json["msg"]||"";
	   var title=json["title"]||"操作结果";
	   var icon=json["icon"]||"info";
	   var action=json["action"]||"show";
	   var result=false;
	   if(action=="show"){
		   $.messager.show({title:title
			                ,msg:msg
			                ,timeout:5000
			                ,showType:"slide"
			                ,showSpeed:500
		   });
	   }else if(action=="alert"){
		   $.messager.alert(title,msg,icon,handler==null?function(){ return true;}:handler);
	   }else if(action=="confirm"){
		   $.messager.confirm(title,msg,handler==null?function(y){ return y;}:handler);
	   }else if(action=="prompt"){
		   $.messager.prompt(title,msg,handler==null?function(val){return val;}:handler);
	   }
    }catch(ex){
    	alert(ex);
    	$.messager.alert("Format error","The result is not a Messager format");
    }	 
};


String.prototype.isNullOrEmpty = function() {
    return ((this == null) || (this == ""));
}

//判断字符串是否只包含英文字母
String.prototype.isAlph=function(){
	
	return (/^[A-Za-z]+$/.test(this));
	
};

//判断字符串是否只包含英文字母和数字
String.prototype.isAlphOrNum=function(){
	return (/^[A-Za-z0-9]+$/.test(this));
};

//判断字符串是否不包含任何标点符号
String.prototype.isNotSymbol=function(){
	return !(/[^0-9A-z\u4e00-\u9fa5]/.test(this));

};


//判断字符是否符合身份证格式
String.prototype.isIdNoFormat=function(){
	return (/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(this));
};

//从身份证里提出出日期
String.prototype.getIdNoBornDate=function(){
	var len=this.length;
	if(len==15){
		var year="19"+this.slice(6,8);
		var month=this.slice(8,10);
		var day=this.slice(10,12);
		return new Date(year+"/"+month+"/"+day);
	}
	
	if(len==18){
		var year=this.slice(6,10);
		var month=this.slice(10,12);
		var day=this.slice(12,14);
		return new Date(year+"/"+month+"/"+day);
	}
};
//电话
String.prototype.isPhoneFormat=function(){
	return (/^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/).test(this);
};
//手机号码
String.prototype.isMobileFormat=function(){
	return (/^((\(\d{3}\))|(\d{3}\-))?13[0-9]\d{8}|15[89]\d{8}/).test(this);


};
//邮政编码 
String.prototype.isZipCode=function(){
	return (/[1-9]\d{5}(?!\d)/.test(this));
};

//中文
String.prototype.isChinese=function(){
	return (/[\u4e00-\u9fff]+/.test(this));
};
//五位的数字
String.prototype.isFive=function(){
	return (/^\d{5,5}$/.test(this));
};
//车牌号
String.prototype.carNo=function(){
	return (/\d{5}$|[A-Z]{1}\d{4}|\d{1}[A-Z]{1}\d{3}|[A-Z]{2}\d{3}$/.test(this));
};
//IP地址
String.prototype.ip=function(){
	return (/^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$/.test(this));
}
;
jQuery.fn.extend({

    toyzLoadFields: function() {
        $(this).find("input:enabled").each(function() {

            if ($(this).hasClass("easyui-combobox")) {
                $(this).combobox();
            } else if ($(this).hasClass("toyz-intbox")) {
                $(this).toyzIntbox();
            } else if ($(this).hasClass("toyz-doublebox")) {
                $(this).toyzDoublebox();
            } else if ($(this).hasClass("toyz-combogrid")) {
                $(this).toyzCombogrid();
            } else if ($(this).hasClass("toyz-datebox")) {
                $(this).toyzDatebox();
            }
        });
        return true;

    }

 , toyzIframe: function(url) {
     $(this).html("<iframe scrolling='no' frameborder='0' src='" + url + "' style='width:99%;height:99%;overflow:hidden;'></iframe>");
 }

 , toyzAuthFields: function(authJson) {
     var authObject = $(this);
     //var authUrl=url||authObject.attr("authUrl")||null;
     if (!authJson) return false;
     authObject.find("input").each(function() {
         var fieldName = $(this).attr("name") || null;
         if (fieldName) {
             var authValue = authJson[fieldName];
             switch (authValue) {
                 case 0: $(this).addClass('invisible').attr("disabled", "disabled"); break;
                 case 1: $(this).addClass('readonly').attr("readonly", "true"); break;
                 default: $(this).addClass('editable');
             }
         }
     });
     return true;
 }



 , toyzCombogrid: function(opts) {

     var thisObj = $(this);
     var thisName = thisObj.attr('name') || "";

     var thisValueField = thisObj.attr("valueField") || "value";
     var thisTextField = thisObj.attr("textField") || "text";
     var thisUrl = thisObj.attr("url") || "";
     var thisRequired = thisObj.attr("required") || true;
     var selecterWidth = thisObj.attr("selecterWidth") || 80;
     var thisDisabled = thisObj.attr("disabled");
     var thisDataEval = thisObj.attr("dataEval") || "";


     if (opts) {
         thisValueField = opts["valueField"] || "value";
         thisTextField = opts["textField"] || "text";
         thisUrl = opts["url"] || "";
         thisRequired = opts["required"] || true;
         selecterWidth = opts["selecterWidth"] || 80;
         thisDisabled = opts["disabled"];
         thisDataEval = opts["dataEval"] || "";
     }


     var thisWidth = thisObj.width();
     var thisHeight = thisObj.height();
     thisObj.width(thisWidth - selecterWidth);
     var thisValue = thisObj.val() || "";

     //var selecterId="selecter-"+thisName;
     //alert(_comboData);
     var selecterObj = $("<input>").width(selecterWidth); //.attr('id',selecterId);
     thisObj.before(selecterObj);
     //var selecterObj=$("#"+selecterId);
     thisObj.combobox({
         hasDownArrow: false
				    	   , editable: false
				    	   , required: thisRequired
				    	   , textField: thisTextField
				    	   , valueField: thisValueField
     });
     selecterObj.combogrid({
         panelWidth: thisWidth
				    	   , disabled: thisDisabled
				    	   , idField: thisValueField
				    	   , textField: thisValueField
				    	   , value: thisValue
				    	   , columns: [[{ field: thisValueField, width: selecterWidth },
                                      { field: thisTextField, width: thisWidth}]]
                           , onChange: function(nv, ov) {
                               if (!nv || !ov || nv == ov) return;
                               thisObj.combobox("select", nv);
                           }
				    	  , onSelect: function(index, r) {
				    	      thisObj.combobox("select", r[thisValueField]);
				    	  }
     });


     var grid = selecterObj.combogrid('grid');
     if (thisDataEval != "") {
         var gridData = eval(thisDataEval);
         if (gridData) {
             grid.datagrid("loadData", gridData);
             thisObj.combobox('loadData', gridData["rows"]);
         }
     } else if (thisUrl != "") {
         $.getJSON(thisUrl, function(gridData) {
             grid.datagrid("loadData", gridData);
             thisObj.combobox('loadData', gridData["rows"]);
         });
     }


     //});  //$.getJSON(thisUrl,function(grid){
 }  // toyzComboBox:function(){

 , toyzDatebox2: function() {
     try {
         var thisObject = $(this);
         var thisValue = thisObject.attr("value") || "";
         var formatValue = "";
         if (thisValue != "") {
             //var d=new Date(thisValue);
             //formatValue=d.format(Toyz4js.custom.setting.dbDateFormat);
             var d = Date.parse(thisValue);
             formatValue = d.toString(Toyz4js.custom.setting.dbDateFormat);
             thisObject.val(formatValue);
             //alert(formatValue);
         }
         thisObject.datebox({
             onChange: Toyz4js.easyui.datebox.onChangeHandler
		 , onSelect: function(date) {
		     thisObject.datebox("setValue", date.format(Toyz4js.custom.setting.dbDateFormat));
		 }
         });
         var dateCombotext = thisObject.next().find("input:first");
         dateCombotext.blur(function() {
             if ($(this).val() == null || $(this).val() == "") return;
             //alert($(this).val());
             var re = Toyz4js.util.isDateFormat(Toyz4js.custom.setting.dbDateFormat, $(this).val());
             if (re) {
                 var cal = dateboxObj.datebox("calendar");
                 var opts = cal.calendar("options");
                 var d = opts["current"];
                 //thisObject.val(d.format(Toyz4js.custom.dbDateFormat));
                 thisObject.datebox("setValue", d.format(Toyz4js.custom.setting.dbDateFormat));
             } else {
                 $.messager.show({ title: "输入错误",
                     msg: "日期格式必须符合" + Toyz4js.custom.setting.dbDateFormat
                 }
    		  );
                 //dateboxObj.datebox("setValue",thisDateboxValue);
                 //thisObject.val(formatValue);
                 thisObject.datebox("setValue", formatValue);
             }
             //if(!isDbDateFormat(thisObject.val())){
             //  $("#"+dateboxId).datebox("setValue","");
             //$.messager.alert("输入错误","日期格式");
             //}
         });
     } catch (ex) {
         //alert(ex); 
     }
 }
 , toyzDatebox: function() {

     var thisObject = $(this);
     var thisValue = thisObject.attr("value") || "";
     var formatValue = "";
     if (thisValue != "") {
         try {
             //var d=Date.parse(thisValue,"%a %h %d %T %z %G");
             var d = new Date(thisValue);
             formatValue = d.toString(Toyz4js.custom.setting.dbDateFormat);
             thisObject.val(formatValue);
         } catch (ex) {
             //alert(ex); 
         }
     } else {
         thisObject.val("");
     }
     thisObject.datebox({
         validType: "date['yyyy-MM-dd']"
     });
 }
 , toyzDoublebox: function() {
     $(this).numberbox({
         increment: 10
            , precision: Toyz4js.setting.precision
     });
 }

  , toyzIntbox: function() {
      $(this).numberbox({
          increment: 10
            , precision: 0
      });
  }

 , toyzCrudForm: function(opts) {
     try {
         var prefix = opts["prefix"] || "";
         var colNum = opts["colNum"] || 2;
         var editors = opts["editors"] || [];
         var urlSave = opts["urlSave"] || "";
         var urlRemove = opts["urlRemove"] || "";
         var data = opts["data"] || {};
         var objDiv = $("<div></div>");
         objDiv
         //.css("width","100%")
         //.css("height","100%")
	.css("overflow", "hidden")
	;
         var objForm = $("<form method='post'></form>");
         var objTable = $("<table cellspacing='0' ></table>");
         objTable.addClass("tableSimple");
         objTable.css("table-layout", "auto");

         $.each(data, function(dataName, dataValue) {
             var hidden = $("<input type=\"hidden\" />")
             hidden.attr("name", prefix + dataName).attr("value", dataValue);
             objForm.append(hidden);

         });

         var tr;
         $.each(editors, function(index, editor) {

             if (index % colNum == 0) {
                 tr = $("<tr></tr>");
             }

             var inputName = editor["field"] || "";
             var inputTitle = editor["title"] || "";
             var pattern = "input[name='" + prefix + inputName + "']";
             objForm.find("input").remove(pattern);
             var td = $("<td>&nbsp;</td>");
             var th = $("<th>&nbsp;</th>");
             var inputEditor = editor["editor"] || { type: "text" };
             var editorType = inputEditor["type"];
             var editorOpts = inputEditor["options"] || {};
             var inputObj;
             if (editorType == "textarea") {
                 inputObj = $("<textarea rows='5' cols='50'></textarea>");
                 inputObj.html(data[inputName]);
             } else if (editorType == "checkbox") {
                 inputObj = $("<input type='checkbox' value='Y' />");
             } else {
                 inputObj = $("<input type='text' />");
                 inputObj.val(data[inputName]);
             }

             inputObj.attr("name", prefix + inputName);
             var span = $("<span></span>");
             span.html(inputTitle);
             //td.html(data[inputName]);
             td.append(inputObj);
             th.append(span).append(":");
             tr.append(th);
             tr.append(td);
             if (index % colNum == colNum - 1 || index == editors.length - 1) {
                 objTable.append(tr);
             }

             switch (editorType) {

                 case "validatebox": { inputObj.validatebox(editorOpts); break; }

                 case "numberbox": { inputObj.numberbox(editorOpts); break; }
                 case "numberspinner": { inputObj.numberspinner(editorOpts); break; }
                 case "combobox": { inputObj.combobox(editorOpts); break; }
                 case "combotree": { inputObj.combotree(editorOpts); break; }
                 case "combogrid": { inputObj.combogrid(editorOpts); break; }
                 case "toyzCombogrid": { inputObj.toyzCombogrid(editorOpts); break; }
                 case "datebox":
                     {
                         var val = inputObj.val();
                         val = DateFormatter(val);
                         inputObj.val(val);
                         inputObj.datebox(editorOpts);
                         break;
                     }
                 case "datetimebox":
                     {
                         var val = inputObj.val();
                         val = DatetimeFormatter(val);
                         inputObj.val(val);
                         inputObj.datetimebox(editorOpts);
                         break;
                     }
                 default: inputObj.validatebox(editorOpts); break;
             }

         });  //each

         var trOpera = $("<tr></tr>");
         var tdOpera = $("<th></th>");
         tdOpera.css("padding-right", "3%");
         trOpera.append(tdOpera);
         tdOpera.attr("colspan", colNum * 2);
         var aSave = $("<a></a>");
         var aClear = $("<a></a>");
         tdOpera.append(aSave);
         tdOpera.append(aClear);
         aSave.linkbutton({
             text: "保存"
    	, iconCls: "icon-save"
         });
         aClear.linkbutton({
             text: "清空"
    	, iconCls: "icon-cancel"
         });
         aSave.click(function() {
             //  $.messager.confirm("操作提示", "确认保存数据？", function(yes) {
             //      if (yes) {

             objForm.form("submit", {
                 url: urlSave
    		   , onSubmit: function() { return $(this).form("validate"); }
    	       , success: function(str) { str.messager(); $(".crud").dialog("close"); }
             });
             //      }
             //  });  //$.messager.alert

         });
         aClear.click(function() { objForm.form("clear"); });
         objTable.append(trOpera);
         objForm.append(objTable);
         objDiv.append(objForm);
         $(this).html(objDiv);

     } catch (ex) {
         //alert(ex); 
     }
 }




 , toyzUploadForm: function(opts) {
     if (!opts) {
         opts = {};
     }
     var uploadUrl = opts["uploadUrl"] || "#";
     var inputName = opts["inputName"] || "file";
     var muti = opts["muti"] || false;
     var div = $("<div></div>");
     var table = $("<table></table>");
     var ol = $("<ol type='1'></ol>");
     var span = $("<span></span>");
     var form = $("<form method='post' enctype='multipart/form-data'></form>");
     form.attr("action", uploadUrl);
     var aNew = $("<a></a>");
     var aSubmit = $("<a></a>");
     aNew.linkbutton({
         text: "新增"
    		  , iconCls: "icon-add"
     });
     aSubmit.linkbutton({
         text: "提交"
    		  , iconCls: "icon-save"
     });
     aNew.click(function() {
         var li = $("<li></li>");
         var inputFile = $("<input type='file' />");
         inputFile.attr("name", inputName);
         var btnRemove = $("<button type='button'>删除</button>");
         btnRemove.click(function() {
             li.remove();
         });
         li.append(inputFile).append("&nbsp;&nbsp;").append(btnRemove);
         ol.append(li);
     });

     aSubmit.click(function() {
         form.form("submit");
     });

     //var btns=[];

     if (muti) {
         //btns=[btnNew,btnUpload];
         span.append(aNew);
     }
     span.append(aSubmit);
     var li = $("<li></li>");
     var inputFile = $("<input type='file' />");
     inputFile.attr("name", inputName);
     li.append(inputFile);
     ol.append(li);
     div.css("padding", "1%");
     span.css("width", "100%").css("text-align", "right").css("padding-right", "5%");
     form.append(ol);
     form.append(span);
     div.append(form);
     $(this).html(div);
 }

});                 //jQuery.fn.entends({
$.extend($.fn.validatebox.defaults.rules, {

    minLength: {
                validator: function(value, param){
                              return value.length >= param[0];},
                message: 'Please enter at least {0} characters.'
    }  //minLength: {
   ,datetime:{
	            validator:function(value,param){
	                      var pattern=param[0]||Toyz4js.custom.setting.dbDateFormat;
	                      return Toyz4js.util.isDateFormat(pattern,value);
	            }
                ,message: '日期格式必须符合 {0}'
   }
   ,idCard:{
	   	        validator:function(value,param){
	                   return value.isIdNoFormat(); 
	            }
                ,message: '身份证格式错误'
   }
   ,zip:{
	   	        validator:function(value,param){
	                   return value.isZipCode(); 
	            }
                ,message: '邮政编码格式错误'
   }
   ,phone:{
	   	        validator:function(value,param){
	                   return value.isPhoneFormat(); 
	            }
                ,message: '电话号码格式错误'
   }
    ,mobile:{
	   	        validator:function(value,param){
	                   return value.isMobileFormat(); 
	            }
                ,message: '手机号码格式错误'
   }
      ,Chinese:{
	   	        validator:function(value,param){
	                   return value.isChinese(); 
	            }
                ,message: '只能是中文'
   }
    ,five:{
	   	      validator:function(value,param){
	                   return value.isFive(); 
	            }
                ,message: '输入格式错误'
   }
     ,carno:{
	   	      validator:function(value,param){
	                   return value.carNo(); 
	            }
                ,message: '车牌输入格式错误'
   }
     ,ip:{
	   	      validator:function(value,param){
	                   return value.ip(); 
	            }
                ,message: 'IP输入格式错误'
   }

});   //$.extend($.fn.validatebox.defaults.rules, {



 
 
  $.fn.panel.defaults.onMove=function(left,top){
 	     if(left<0){
            $(this).panel("move",{left:10});
    	 }
    	 if(top<0){
    		$(this).panel("move",{top:10});
    	 }
    	 var right=left+$(this).width();
    	 var buttom=top+$(this).height();
    	 var wiw=parseInt(window.innerWidth);
    	 var wih=parseInt(window.innerHeight);
    	 if(right>wiw){  
    		 var r=wiw-$(this).width()-30;
    		 $(this).panel("move",{left:r});
    	 }
    	 if(buttom>wih){  
    		  var b=wih-$(this).height()-50;
    		 $(this).panel("move",{top:b});
    	 }
 }
  
 $.fn.window.defaults.onMove= $.fn.panel.defaults.onMove;
 //$.fn.dialog.defaults.onMove= $.fn.panel.defaults.onMove;
 
 $.fn.window.defaults.shadow=false;

 $.fn.form.defaults.success=function(str){$.messager.progress("close");str.messager(null);};
 $.fn.form.defaults.onSubmit=function(){ 
	                                    var pass= $(this).form("validate");
	                                    if(pass){
	                                    	$.messager.progress({msg:"请求处理中，请稍候",text:""});
	                                    }
	                                    return pass;
                                        };
 $.fn.form.defaults.onLoadError=function(){
	 $.messager.progress("close");
	 $.messager.alert("Error","Can't access to the remote site.","error");
 };
 
 
  $.fn.datagrid.defaults.fit=true;
 $.fn.datagrid.defaults.pagination=true;
 $.fn.datagrid.defaults.nowrap=false;
 $.fn.datagrid.defaults.idField="id";
 $.fn.datagrid.defaults.rownumbers=true;
 $.fn.datagrid.defaults.pageSize=20;
 $.fn.datagrid.defaults.groupFormatter=function(value,rows){return value + ' - 共' + rows.length + ' 条';};


 $.fn.combobox.defaults.textField = "text";
 $.fn.combobox.defaults.valueField = "value";





 function DateFormatter(value, rowData, rowIndex) {
     try {
         var dv = value.substring(1, value.length - 1);
         var dt = eval("new " + dv);
         return dt.toString("yyyy-MM-dd");
     } catch (ex) {

     return "";}
 }



 function DateTimeFormatter(value, rowData, rowIndex) {
     try {
         var dv = value.substring(1, value.length - 1);
         var dt = eval("new " + dv);
         return dt.toString("yyyy-MM-dd HH:mm:ss");
     } catch (ex) {  return ""; }
 }


 function DictFormatter(type) {
     var dicts = Toyz4js["cache"]["DictModel"][type]["rows"] || [];
     var dictFormatter = function(value, rowData, rowIndex) {
         
         for (var i = 0; i < dicts.length; i++) {
             var dict = dicts[i];
             var id = dict["value"] || "";
             if (id.toString() == value.toString()) { return dict["text"] || ""; }
         }
         return value;
     };
  return dictFormatter;
}

function GeoFormatter() {

    var geos = Toyz4js["cache"]["GeoModel"]["rows"] || [];
    var geoFormatter = function(value, rowData, rowIndex) {

        for (var i = 0; i < geos.length; i++) {
            var geo = geos[i];
            var id = geo["id"] || "";
            if (id.toString() == value.toString()) { return geo["cityName"] || ""; }
        }
        return value;
    };
    return geoFormatter;
}

function RoleFormatter() {

    var roles = Toyz4js["cache"]["RoleModel"]["rows"] || [];
    var roleFormatter = function(value, rowData, rowIndex) {

      for (var i = 0; i < roles.length; i++) {
            var role = roles[i];
            var id = role["id"] || "";
            if (id.toString() == value.toString()) { return role["name"] || ""; }
        }
        return value;
    };
    return roleFormatter;
}

function IndFormatter(value, rowData, rowIndex) {

    if (value == "Y") {
        return "是";

    } else {
        return "否";
    }

}


function HotelFormatter() {

    var hotels = Toyz4js["cache"]["HotelModel"]["rows"] || [];
    var hotelFormatter = function(value, rowData, rowIndex) {

        for (var i = 0; i < hotels.length; i++) {
            var hotel = hotels[i];
            var id = hotel["hotelId"] || "";
            if (id.toString() == value.toString()) { return hotel["hotelName"] || ""; }
        }
        return value;
    };
    return hotelFormatter;
}

function ToyzOperaFormatter(opts) {

    opts = opts || {};
    var iconCls = opts["iconCls"] || "file";
    var href = opts["href"] || "";
    var handlerName = opts["handlerName"] || "";

    var operaFomatter = function(value, rowData, rowIndex) {

        var a;
        var strRowData =Toyz4js.util.jsonToStr(rowData);
        if (href == "") {
            a = "<a onclick='" + handlerName + "(" + strRowData + "," + rowIndex + ")' class='toyz-icon " + iconCls + "'></a>";
        } else {
            a = "<a href='" + href + "' class='toyz-icon " + iconCls + "'></a>";
        }

        return a;
    }
  return operaFomatter;
}





function ImgFormatter(style) {
    return function(value, rowData, rowIndex) {
        if (value.length == 0) {
            return "";
        }
        var context = "<img src='" + value + "'";
        if (style) {
            context += " style='" + style + "' ";
        }
        context += " />";
        return context;
    }

}

function HrefFormatter(style) {
    return function(value, rowData, rowIndex) {
        if (value.length == 0) {
            return "";
        }
        var context = "<a href='" + value + "' target='_blank'";
        if (style) {
            context += " style='" + style + "' ";
        }
        context += " >"+value+"</a>";
        return context;
    }

}


function NumberDescSorter(n1, n2) {
    return n1 > n2;
}

function StringDescSorter(c1, c2) {
    return c1.localeCompare(c2);
}


function GetIndEditor() {

    return {
        type: "combobox"
      , options: {
        data: [{ text: "是", value: "Y" }, { text: "否", value: "N"}]
        , textField: "text"
        , valueField:"value"
      }
    };

}


function LikeFilter(proName) {
    return function(q, rowData) {  rowData[proName].indexOf(q) != -1; }
}

/**
 require jquery,easyui
 * import core.js
 * import setting.js
 * import extends.jquery.js
 * import extends.easyui.js
 * import prototype.String.js
 */


var CrudDatagrid=function(opts){
	this._urlAdd=opts["urlAdd"]||"";
	this._urlRemove=opts["urlRemove"]||"";
	this._urlUpdate=opts["urlUpdate"]||"";
	this._urlExportExcel=opts["urlExportExcel"]||"";
	this._regexp=opts["regexp"]||"";
    this._id=opts["id"]||"Id";
    this._initRow=opts["initRow"]||function(){return {};};
    this._editors=opts["editors"];
    //alert("init");
};

CrudDatagrid.prototype.getToolbar = function(opts) {
    opts = opts || {};
    var newRow = new this._initRow();
    var regexp = this._regexp;
    var id = this._id;
    var urlRemove = this._urlRemove;
    var urlAdd = this._urlAdd;
    var urlUpdate = this._urlUpdate;
    var urlExportExcel = this._urlExportExcel;
    var formOpts = {};
    formOpts["editors"] = this._editors;
    formOpts["prefix"] = opts["prefix"] || "";
    var btnAdd = { iconCls: "icon-add", text: "新建"
	        , handler: function() {
	            formOpts["data"] = newRow;
	            formOpts["urlSave"] = urlAdd;
	            var div = $("<div></div>");
	            div.addClass("crud");
	            div.toyzCrudForm(formOpts);
	            div.dialog({
	                title: "新建记录"
    		      , modal: true
	              , onClose: function() { $(this).dialog("destroy", true); }
	             , onDestroy: function() { $(regexp).datagrid("reload"); }

	            });
	        }
    };
    var btnUpdate = { iconCls: "icon-edit", text: "修改"
	        , handler: function() {
	            var selectRow = $(regexp).datagrid("getSelected");
	            if (!selectRow) { $.messager.alert("", "没有记录被选中", "error"); return; }
	            var rowIndex = $(regexp).datagrid("getRowIndex", selectRow);
	            formOpts["data"] = selectRow;
	            formOpts["urlSave"] = urlUpdate;
	            var title = "修改记录序号:" + (rowIndex + 1);
	            var div = $("<div></div>");
	            div.addClass("crud");
	            div.toyzCrudForm(formOpts);
	            div.dialog({
	                title: title
    		      , modal: true
	                //,onBeforeOpen:function(){$(this).toyzCrudForm(formOpts);}
	             , onClose: function() { $(this).dialog("destroy", true); }
	             , onDestroy: function() { $(regexp).datagrid("reload"); }
	            });

	        }
    };
    var btnRemove = { iconCls: "icon-remove"
		        , text: "删除"
		       , handler: function() {
		           var selectsRows = $(regexp).datagrid("getSelections");
		           var deleteCount = selectsRows.length;
		           if (deleteCount == 0) { $.messager.alert("", "没有记录被选中", "error"); return; }
		           var ids = "";
		           ids = selectsRows.getUnionAttrStr(id, ",");
		           $.messager.confirm("操作提示", "是否删除" + deleteCount + "条记录", function(ok) {
		               if (ok) {
		                   //var removeRowsAction=basePath+"/common/Datagrid/do/removeRows.action?modelClass="+modelClass;
		                   $.post(urlRemove, { ids: ids }, function(re) {
		                       try {
		                           //alert(re.toString());
		                           
		                           re.toString().messager();
		                           $(regexp).datagrid("clearSelections");
		                           $(regexp).datagrid("reload");
		                       } catch (ex) { alert(ex); }
		                   });
		               } //if(ok){
		           });
		       }
    };
    var btnRefresh = { iconCls: "icon-reload"
	        , text: "刷新"
	        , handler: function() {
                $(regexp).datagrid("clearSelections");
	            $(regexp).datagrid("reload");
	        }
    };
    var btnExportExcel = {
        iconCls: "icon-help"
            	, text: "导出excel"
            	, handler: function() {
            	    if (urlExportExcel.length < 1) {
            	        $.messager.alert("操作错误", "没有配置excel路径", "error");
            	        return;
            	    }
            	    var div = $("<div></div>");
            	    var table = $("<table></table>");
            	    table.css("padding", "10px");
            	    var tr;
            	    var th;
            	    var td;
            	    var td2;
            	    var a;

            	    //导出全部记录
            	    tr = $("<tr></tr>");
            	    th = $("<th></th>");
            	    td = $("<td></td>");
            	    td2 = $("<td></td>");
            	    a = $("<a></a>");
            	    var countAll = $(regexp).datagrid("getData")["total"] || 0;
            	    th.html("全部记录数:");
            	    td.html(countAll).css("width", "100px");
            	    td2.css("width", "120px");
            	    a.attr("href", urlExportExcel);
            	    td2.append(a);
            	    a.linkbutton({
            	        text: "导出"
            	    });
            	    tr.append(th).append(td).append(td2);
            	    table.append(tr);

            	    //导出已选记录
            	    tr = $("<tr></tr>");
            	    th = $("<th></th>");
            	    td = $("<td></td>");
            	    td2 = $("<td></td>");
            	    a = $("<a></a>");
            	    var countSelect = $(regexp).datagrid("getSelections").length || 0;
            	    th.html("已选记录数:");
            	    td.html(countSelect);
            	    a.attr("href", urlExportExcel);
            	    td2.append(a);
            	    a.linkbutton({
            	        text: "导出"
            	    });
            	    tr.append(th).append(td).append(td2);
            	    table.append(tr);

            	    //导出当前页记录
            	    tr = $("<tr></tr>");
            	    th = $("<th></th>");
            	    td = $("<td></td>");
            	    td2 = $("<td></td>");
            	    a = $("<a></a>");
            	    var countPage = $(regexp).datagrid("getRows").length || 0;
            	    th.html("当前页记录数:");
            	    td.html(countPage);
            	    a.attr("href", urlExportExcel);
            	    td2.append(a);
            	    a.linkbutton({
            	        text: "导出"
            	    });
            	    tr.append(th).append(td).append(td2);
            	    table.append(tr);

            	    div.append(table);

            	    div.dialog({
            	        title: "导出excel"
            	    });
            	}
    };

    var toolbar = [btnAdd, btnUpdate, btnRemove, btnRefresh];
    return toolbar;
};




CrudDatagrid.prototype.getOperaFormatter=function(){
	try{
	var newRow=new this._initRow();
	var regexp=this._regexp;
	var id=this._id;
	var urlRemove=this._urlRemove;
	var urlAdd=this._urlAdd;
	var urlUpdate=this._urlUpdate;
    var formOpts={};
   formOpts["editors"]=this._editors;
	var o= function(value,row,index){
   var span=$("<span></span>");
   var aEdit=$("<a id='aEdit'></a>");
   var aRemove=$("<a></a>");
   var eidtOnclickContext="$('<div></div>').dialog({width:600,height:400});"
   span.append(aEdit);
   span.find("#aEdit")
	   .attr("title","编辑")
	   .attr("href","javascript:void(0)")
	   .addClass("toyz-icon")
	   .addClass("edit")
	   .attr("onclick",eidtOnclickContext)
	;
	
	   
	   alert(span.html());
     return span.html();
   
   }
	 return o;
	 
	 }catch(ex){alert(ex);}
}

CrudDatagrid.prototype.getEditRowHandler = function(opts) {
    opts = opts || {};
    var regexp = this._regexp;
    var id = this._id;
    var urlRemove = this._urlRemove;
    var urlAdd = this._urlAdd;
    var urlUpdate = this._urlUpdate;
    var formOpts = {};
    formOpts["editors"] = this._editors;
    formOpts["prefix"] = opts["prefix"] || "";
    var fun = function(rowIndex, rowData) {

        formOpts["data"] = rowData;
        formOpts["urlSave"] = urlUpdate;
        var i = parseInt(rowIndex);
        var title = "修改记录序号:" + (i + 1);
        var div = $("<div></div>");
        div.addClass("crud");
        div.toyzCrudForm(formOpts);
        div.dialog({
            title: title
    		      , modal: true
            //,onBeforeOpen:function(){$(this).toyzCrudForm(formOpts);}
	             , onClose: function() { $(this).dialog("destroy", true); }
	             , onDestroy: function() { $(regexp).datagrid("reload"); }
        });

    }
    return fun;

}
	


function dateboxFormatter(value,row,index){

    
	if(value==null||value=='')return '';
	var strFormat="";
	if(typeof(value)=="number"){
		var date=new Date();date.setTime(value);
		return date.toString('yyyy-MM-dd');
	}else{
		return value;
	}

	//alert('value:'+value+" date:"+date.toString('yyyy-MM-dd'));
	
}

function dateTimeboxFormatter(value,row,index){

	if(value==null||value=='')return '';
	var date=new Date();
	date.setTime(value);
	if(date==null){
		return value;
	}
	//alert('value:'+value+" date:"+date.toString('yyyy-MM-dd'));
	return date.toString('yyyy-MM-dd HH:mm:ss');
}





