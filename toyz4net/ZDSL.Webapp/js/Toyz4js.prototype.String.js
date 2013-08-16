
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