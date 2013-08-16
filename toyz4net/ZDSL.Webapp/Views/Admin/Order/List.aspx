<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <div region="north">
           
           <table id="tableSearch">
              <tr>
                 <td>订单号:</td>
                 <td><input name="orderId" /></td>
                 <td><a class="easyui-linkbutton" onclick="doSearch();">查询</a></td>
              </tr>
           </table>

       </div>

       <div region="center"  >
		  <table id="tableDG"></table>
		  
		  	<div id="divCancel"  style=" height:0.1px; overflow:hidden" >
		  	    <form id="formCancel" method="post"  >
               <input id="inputOrderId" name="cancelOrderId" type="hidden"  />
		       <table class="tableSimple">

		         <tr>
    		        
		           <th>取消类型：</th>
		           <td><input id="inputCancelCode" name="cancelCode" style=" width:400px" required="true" /></td>
		         </tr>
		         <tr>
		           <th>取消原因：</th>
		           <td><textarea  id="inputCancelReason" name="cancelReason" cols="50" rows="4"  ></textarea></td>
		         </tr>
		          </table>
		       </form>
		   </div>
		  
		  
		  <div id="divElongSubmit" style=" height:0.1px; overflow:hidden">
		       <form id="formElongSubmit" method="post"  >
		       <input  name="id" type="hidden"  />
		                   <table style=" width:100%; "  class="tableSimple">
               <tr>
             	 
	            <td>宾客类型:<input type="text" id="guestTypeCode" name="guestTypeCode" value="1" /></td>
                <td>支付方式:<input id="paymentTypeCode" name="paymentTypeCode" value="0" /></td>
                <td>货币类型:<input type="text"  name="currencyCode" id="currencyCode" value="RMB" /></td>
                

             </tr>
             
              <tr>
                <td>订单确认方式:<input name="confirmTypeCode" id="confirmTypeCode" value="sms" /></td>
                <td>订单确认语言:<input name="confirmLanguageCode"  id="confirmLanguageCode" value="CN" /></td>

              </tr>
              
              <tr>
                               <td>酒店备注:<textarea name="noteToHotel"></textarea></td>
               <td>elong备注:<textarea name="noteToElong"></textarea></td>
              </tr>             
   
             </table>
             </form>
		  </div>
		  
		</div>
	

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
          string status = ObjectUtil.Parse(Request.Params["status"], "");
          string orderStatus = ObjectUtil.Parse(Request.Params["orderStatus"], "");
          string pathDatagrid = string.Format("{0}/Admin/Order/Datagrid?orderStatus={1}", basePath,orderStatus);
          
       %>
     
      <script type="text/javascript">


         function doSearch(){
              var orderId=$("#tableSearch").find("input[name=orderId]").val();
              
              var opts = $("#tableDG").datagrid("options");
              opts["queryParams"]={orderId:orderId};
              $("#tableDG").datagrid(opts);
             
              $("#tableDG").datagrid("clearSelections");
         }

          function qq(value, name) {
              var qType = name;
              var qVal = value || "";
              var opts = $("#tableDG").datagrid("options");
              //alert(opts["url"]);
              opts["queryParams"] = { qtHotelName: qType, qvHotelName: qVal };
              //$("#tableDG").datagrid(opts);
              $("#tableDG").datagrid("reload");
              $("#tableDG").datagrid("clearSelections");
          }
          
          function clickCancelOrderHander(){
                        
                    var rows=$("#tableDG").datagrid("getSelections");
                    if(rows.length==0){
                      $.messager.alert("","没有记录被选中");return;
                    }else{

						        var ids=rows.getUnionAttrStr("hotelId",",");
						        var cancelCode=$("#inputCancelCode").val()||0;
						        var cancelReason=$("#inputCancelReason").val()||"";
						        var url="<%=basePath %>/Admin/Order/DoCancelOrder";
						        $.post(url,{ids:ids,cancelCode:cancelCode,cancelReason:cancelReason},function(str){
						           //var json=str.toJson();
						           str.messager();
						        });
      
                    }  //else
          
          }
          
          function doCancelOrder(rowData){
             var orderId=rowData["id"];
             $("#inputOrderId").val(orderId);
             $("#divCancel").dialog({
                 title:"取消订单:"+orderId
                 ,buttons:[btnCancel]
             });
          }
          
          function doSubmitToElong(rowData){
             var orderId=rowData["id"];
              $("#formElongSubmit").find("input[name=id]").val(orderId);
           ///  $("#divElongSubmit").dialog({
            //     title:"请填写订单补充信息:"+orderId
            //     ,buttons:[btnElongSubmit]
           //  });
             var url="<%=basePath %>/Admin/Order/DoSubmitToElong";
             $.post(url,{id:orderId},function(str){str.messager();$("#tableDG").datagrid("reload");});
             //$.post(url,{id:orderId},function(str){str.messager();$("#tableDG").datagrid("reload");})
          }
          
          function doSubmitToZDSL(rowData){
             var orderId=rowData["id"];
             var url="<%=basePath %>/Admin/Order/DoSubmitToZDSL";
             $.post(url,{id:orderId},function(str){str.messager();$("#tableDG").datagrid("reload");});
             
          }
          
          function doDeal(rowData){
             var orderId=rowData["id"];
             var url="<%=basePath %>/Admin/Order/DoDeal";
             $.post(url,{id:orderId},function(str){str.messager();$("#tableDG").datagrid("reload");})
             
          }

          var btnCancel={ text:"取消订单",iconCls:"icon-cancel",handler:function(){ 
              $("#formCancel").form("submit",{
                       url:"<%=basePath %>/Admin/Order/DoCancelOrder"
                       ,success:function(str){$.messager.progress("close");str.messager();$("#tableDG").datagrid("reload"); }
                       }); }
          } ;
          
          var btnElongSubmit={ text:"提交到elong",iconCls:"icon-ok",handler:function(){ 
                 $("#formElongSubmit").form("submit",{
                       url:"<%=basePath %>/Admin/Order/DoSubmitToElong"
                       ,success:function(str){$.messager.progress("close");str.messager();$("#tableDG").datagrid("reload"); }
                       }); }
          } ;
           
          var CancelFormatter=ToyzOperaFormatter({iconCls:"remove",handlerName:"doCancelOrder" });
          var SubmitToElongFormatter=ToyzOperaFormatter({iconCls:"ok-blue",handlerName:"doSubmitToElong" });
          var SubmitToZDSLFormatter=ToyzOperaFormatter({iconCls:"ok-green",handlerName:"doSubmitToZDSL" });
          var DealFormatter=ToyzOperaFormatter({iconCls:"ok-green",handlerName:"doDeal" });

          var colCheckbox={title:"",field:"gg",checkbox:true};
          var colId = { title: "捷达订单号", field: "id", width: 120 };
          var colElongId = { title: "eLong订单号", field: "elongOrderId", width: 150,  };
          var colHotelId = { title: "酒店", field: "hotelId", width: 250 };
          var colRoomTypeId = { title: "房间类型", field: "roomTypeId", width: 150 };
          var colRatePlanId = { title: "产品类型", field: "ratePlanId", width: 50 };
          var colRatePlanCode = { title: "产品类型代码", field: "ratePlanCode", width: 50 };
          var colCheckInDate = { title: "入住时间", field: "checkInDate", width: 120,formatter:DateFormatter,editor:{type:"datebox",options:{formatter:DateFormatter }} };
          var colCheckOutDate = { title: "离开时间", field: "checkOutDate", width: 120 ,formatter:DateFormatter,editor:{type:"datebox"}};
          var colElongCardNo = { title: "Elong卡号", field: "elongCardNo", width: 50 };
          var colGuestTypeCode = { title: "宾客类型", field: "guestTypeCode", width: 50,editor:{ type:"combobox",options:{data:Toyz4js["cache"]["DictModel"]["guestType"]["rows"]}} };
          var colGuestAmount = { title: "宾客人数", field: "guestAmount", width: 50,editor:{type:"numberspinner"} };
          var colPaymentTypeCode = { title: "支付类型", field: "paymentTypeCode", width: 50,editor:{ type:"combobox",options:{data: Toyz4js["cache"]["DictModel"]["paymentType"]["rows"]}} };
          var colArraivalEarlyTime = { title: "最早到达时间", field: "arraivalEarlyTime", width: 50 };
          var colArraivalLateTime = { title: "最晚到达时间", field: "arraivalLateTime", width: 50 };
          var colCurrencyCode = { title: "货币类型", field: "currencyCode", width: 120,editor:{ type:"combobox",options:{data: Toyz4js["cache"]["DictModel"]["currencyId"]["rows"]}} };
          var colTotalPrice = { title: "总价", field: "totalPrice", width: 100 };

          var colConfirmTypeCode = { title: "确认类型", field: "confirmTypeCode", width: 50,editor:{ type:"combobox",options:{data: Toyz4js["cache"]["DictModel"]["confirmType"]["rows"] }} };
          var colConfirmLanguageCode = { title: "确认语言", field: "confirmLanguageCode", width: 50,editor:{ type:"combobox",options:{data: Toyz4js["cache"]["DictModel"]["language"]["rows"]}} };
          var colNoteToHotel = { title: "酒店备注", field: "noteToHotel", width: 50 };
          var colNoteToElong = { title: "elong备注", field: "noteToElong", width: 50 };
          var colCreditCardNumber = { title: "信用卡号", field: "creditCardNumber", width: 50 };
          var colParameterString1 = { title: "备用字符1", field: "parameterString1", width: 50 };
          var colParameterString2 = { title: "备用字符2", field: "parameterString2", width: 50 };
          var colParameterString3 = { title: "备用字符3", field: "parameterString3", width: 50 };
          var colParameterInt1 = { title: "备用数字1", field: "parameterInt1", width: 50 };
          var colParameterInt2 = { title: "备用数字2", field: "parameterInt2", width: 50 };
          var colParameterInt3 = { title: "备用数字3", field: "parameterInt3", width: 50 };
          var colGuestsIdArray = { title: "住客列表", field: "guestsIdArray", width: 50 };
          var colContacterIdArray = { title: "联系人列表", field: "contacterIdArray", width: 50 };
          
          var colOrderStatus={title:"订单状态",field:"orderStatus",width:100};
          
          var colMemberFk = { title: "会员帐号", field: "memberFk", width: 200 };
          var colUserFk = { title: "处理人帐号", field: "userFk", width: 200 };
          var colHotelName = { title: "酒店名", field: "hotelName", width: 250 };
          var colCreateDate = { title: "预订时间", field: "createDate", width: 120,formatter:DateTimeFormatter };
          var colLastModify = { title: "最后修改时间", field: "lastModifyDate", width: 150,formatter:DateTimeFormatter };
          var colGuestsNameArray={ title:"住客名",field:"guestsNameArray",width:200,editor:{type:"textarea"}};
    
         var colOperaCancel={title:"取消",field:"o1",width:80,formatter:CancelFormatter};
         var colOperaSubmitToElong={title:"E龙处理",field:"o2",width:80,formatter:SubmitToElongFormatter};
         var colOperaSubmitToZDSL={title:"内部处理",field:"o3",width:80,formatter:SubmitToZDSLFormatter};
         var colOperaDeal={title:"确认成交",field:"o4",width:80,formatter:DealFormatter};
          
          var status = { title: "状态", field: "status", width: 50 };

          var editors = [colCheckInDate,colCheckOutDate,colGuestAmount,colGuestsNameArray,colGuestTypeCode,colPaymentTypeCode,colCurrencyCode,colConfirmTypeCode,colConfirmLanguageCode,colNoteToElong,colNoteToHotel];
          
          var opts = {};
          opts["regexp"] = "#tableDG";
          opts["id"] = "id";
          opts["urlUpdate"] = "<%=basePath %>/Admin/Order/Save";
          opts["urlAdd"] = opts["urlUpdate"];
          opts["urlRemove"] = "<%=basePath %>/Admin/Order/Remove";
          opts["editors"] = editors;

          var crud = new CrudDatagrid(opts);
          var toolbar = crud.getToolbar();
          var editRow=crud.getEditRowHandler();

          toolbar[0]["text"] = "新建订单";
          toolbar[1]["text"] = "修改订单";
          toolbar[2]["text"] = "删除订单";
          

          $(document).ready(function() {

              $("#tableDG").datagrid({
              url: "<%=pathDatagrid %>"
             ,idField:"orderId"
             ,toolbar:toolbar
             ,onDblClickRow:editRow
          , columns: [
               [colCheckbox,colId,colElongId,colCreateDate,colHotelName,colCheckInDate,colTotalPrice,colOperaSubmitToElong,colOperaSubmitToZDSL,colOperaDeal,colOperaCancel]
            ]
          	,			view: detailview
			,	detailFormatter: function(rowIndex, rowData){
			       // var guestTypeName=Toyz4js["cache"]["DictModel"]
					var context= "<table class='tableSimple'>"
					+"<tr><th>房型</th><td>"+rowData["roomName"]+"</td><th>产品类型</th><td>"+rowData["ratePlanName"]+"</td>"
					+"<tr><th>房间数</th><td>"+rowData["roomAmount"]+"</td><th>住客姓名</th><td>"+rowData["guestsNameArray"]+"</td>"
					+"<th>联系电话</th><td>"+rowData["mobile"]+"</td><th>Email</th><td>"+rowData["email"]+"</td></tr>"
					+"</table>";
					return context;
				}
              });

             $("#inputCancelCode").combobox({
               data:Toyz4js["cache"]["DictModel"]["Cancel"]["rows"]
               ,editable:false
             });
             
             
             
                          $("#currencyCode").combobox({
                 editable: false   //只读必须显式配置
                , data: Toyz4js["cache"]["DictModel"]["currencyId"]["rows"]  //获取DictModel.js中的集合
             });


             $("#paymentTypeCode").combobox({
                 editable: false   //只读必须显式配置
                , data: Toyz4js["cache"]["DictModel"]["paymentType"]["rows"]  //获取DictModel.js中的集合
             });

             $("#guestTypeCode").combobox({
                 editable: false   //只读必须显式配置
                , data: Toyz4js["cache"]["DictModel"]["guestType"]["rows"]  //获取DictModel.js中的集合
             });


             $("#confirmLanguageCode").combobox({
                 editable: false   //只读必须显式配置
                , data: Toyz4js["cache"]["DictModel"]["language"]["rows"]  //获取DictModel.js中的集合
             });

             $("#confirmTypeCode").combobox({
                 editable: false   //只读必须显式配置
                , data: Toyz4js["cache"]["DictModel"]["confirmType"]["rows"]  //获取DictModel.js中的集合
             });
             

          });       // $(document).ready(function() { 
    
  </script>
</asp:Content>