
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