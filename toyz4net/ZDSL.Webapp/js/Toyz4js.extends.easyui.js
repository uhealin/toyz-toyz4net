
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

