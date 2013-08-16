var defaultView = {
	render: function(target, container, frozen){
		var opts = $.data(target, 'datagrid').options;
		var rows = $.data(target, 'datagrid').data.rows;
		var fields = $(target).datagrid('getColumnFields', frozen);
		
		if (frozen){
			if (!(opts.rownumbers || (opts.frozenColumns && opts.frozenColumns.length))){
				return;
			}
		}
		
		var table = ['<table cellspacing="0" cellpadding="0" border="0"><tbody>'];
		for(var i=0; i<rows.length; i++) {
			// get the class and style attributes for this row
			var cls = (i % 2 && opts.striped) ? 'class="datagrid-row-alt"' : '';
			var styleValue = opts.rowStyler ? opts.rowStyler.call(target, i, rows[i]) : '';
			var style = styleValue ? 'style="' + styleValue + '"' : '';
			
			table.push('<tr datagrid-row-index="' + i + '" ' + cls + ' ' + style + '>');
			table.push(this.renderRow.call(this, target, fields, frozen, i, rows[i]));
			table.push('</tr>');
		}
		table.push('</tbody></table>');
		
		$(container).html(table.join(''));
	},
	
	renderFooter: function(target, container, frozen){
		var opts = $.data(target, 'datagrid').options;
		var rows = $.data(target, 'datagrid').data.footer || [];
		var fields = $(target).datagrid('getColumnFields', frozen);
		var table = ['<table cellspacing="0" cellpadding="0" border="0"><tbody>'];
		
		for(var i=0; i<rows.length; i++){
			table.push('<tr datagrid-row-index="' + i + '">');
			table.push(this.renderRow.call(this, target, fields, frozen, i, rows[i]));
			table.push('</tr>');
		}
		
		table.push('</tbody></table>');
		$(container).html(table.join(''));
	},
	
	renderRow: function(target, fields, frozen, rowIndex, rowData){
		var opts = $.data(target, 'datagrid').options;
		
		var cc = [];
		if (frozen && opts.rownumbers){
			var rownumber = rowIndex + 1;
			if (opts.pagination){
				rownumber += (opts.pageNumber-1)*opts.pageSize;
			}
			cc.push('<td class="datagrid-td-rownumber"><div class="datagrid-cell-rownumber">'+rownumber+'</div></td>');
		}
		for(var i=0; i<fields.length; i++){
			var field = fields[i];
			var col = $(target).datagrid('getColumnOption', field);
			if (col){
				// get the cell style attribute
				var styleValue = col.styler ? (col.styler(rowData[field], rowData, rowIndex)||'') : '';
				var style = col.hidden ? 'style="display:none;' + styleValue + '"' : (styleValue ? 'style="' + styleValue + '"' : '');
				
				cc.push('<td field="' + field + '" ' + style + '>');
				
				var style = 'width:' + (col.boxWidth) + 'px;';
				style += 'text-align:' + (col.align || 'left') + ';';
				style += opts.nowrap == false ? 'white-space:normal;' : '';
				
				cc.push('<div style="' + style + '" ');
				if (col.checkbox){
					cc.push('class="datagrid-cell-check ');
				} else {
					cc.push('class="datagrid-cell ');
				}
				cc.push('">');
				
				if (col.checkbox){
					cc.push('<input type="checkbox"/>');
				} else if (col.formatter){
					cc.push(col.formatter(rowData[field], rowData, rowIndex));
				} else {
					cc.push(rowData[field]);
				}
				
				cc.push('</div>');
				cc.push('</td>');
			}
		}
		return cc.join('');
	},
	
	refreshRow: function(target, rowIndex){
		var opts = $.data(target, 'datagrid').options;
		var panel = $(target).datagrid('getPanel');
		var rows = $(target).datagrid('getRows');
		
		var styleValue = opts.rowStyler ? opts.rowStyler.call(target, rowIndex, rows[rowIndex]) : '';
		var tr = panel.find('div.datagrid-body tr[datagrid-row-index=' + rowIndex + ']');
		tr.attr('style', styleValue || '');
		tr.children('td').each(function(){
			var td = $(this);
			var cell = td.find('div.datagrid-cell');
			var field = td.attr('field');
			var col = $(target).datagrid('getColumnOption', field);
			if (col){
				var styleValue = col.styler ? col.styler(rows[rowIndex][field], rows[rowIndex], rowIndex) : '';
				td.attr('style', styleValue || '');
				if (col.hidden){
					td.hide();
				}
				
				if (col.formatter){
					cell.html(col.formatter(rows[rowIndex][field], rows[rowIndex], rowIndex));
				} else {
					cell.html(rows[rowIndex][field]);
				}
			}
		});
		$(target).datagrid('fixRowHeight', rowIndex);
	},
	
	onBeforeRender: function(target, rows){},
	onAfterRender: function(target){
		var opts = $.data(target, 'datagrid').options;
		if (opts.showFooter){
			var footer = $(target).datagrid('getPanel').find('div.datagrid-footer');
			footer.find('div.datagrid-cell-rownumber,div.datagrid-cell-check').css('visibility', 'hidden');
		}
	}
};
