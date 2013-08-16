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





