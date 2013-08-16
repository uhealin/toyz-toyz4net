
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


