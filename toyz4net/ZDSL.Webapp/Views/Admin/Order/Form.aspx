<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Form
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div region="center" >
    <form method="post" id="formOrder">
         <input type="hidden" name="guestsIdArray" id="guestsIdArray"  value=""/>
         <input type="hidden" name="contacterIdArray" id="contacterIdArray"  value=""/>
         <input type="hidden" name="roomAmount" value="1" />
         
         
         <fieldset>
             <legend>订单信息</legend>
             <table style=" width:100%; " class="tableSimple">
             <tr>
               <td>捷达订单号</td>
               <td>elong订单号</td>
               <td>订单上传日期</td>
               <td>elong订单状态</td>
             </tr>
          </table>
         </fieldset>

        
        
         
         <fieldset>
            <legend>酒店房间价格查询</legend>
              <table style=" width:100%; "  class="tableSimple">
            <tr>
              <td colspan="4">

              酒店:
                <div id="searchboxHotel" style="width:120px">
		          <div name="cn" iconCls=" icon-ok">中文名查询</div>
		          <div name="en">英文名查询</div>
	            </div> &nbsp;
	            	     <input id="ss" class="easyui-searchbox"
			              searcher="searcherHotel"
			              prompt="Please Input Value" menu="#searchboxHotel" style="width:250px"></input>
	            <input type="text" id="inputHotelId" name="hotelId" />
	           &nbsp; 
                  
              </td>
            
            </tr>
      
             <tr>
               <td>入住日期:<input type="text" class="easyui-datebox" name="checkInDate" id="inputCheckInDate" /></td>
               <td>离开日期:<input type="text" class="easyui-datebox" name="checkOutDate" id="inputCheckOutDate" /></td>
              <!-- <td>最早到达时间:<input type="text" class="easyui-timespinner" name="arraivalEarlyTime"  /></td> -->
               <td>最晚到达时间:
                  <select class="easyui-combobox" name="arraivalLastTime" editable="false">
                    <%   for (int i = 6; i < 24; i++) 
                         { %>
                     <option value="<%=i %>:00"><%=i %>:00</option>                     
                     <%} %>

                  </select>
              </td>
             </tr>
             
             <tr>
               <td>房型:<input type="text" id="inputRoomTypeId" name="roomTypeId" /></td>
               <td> 产品类型:<input type="text" id="inputRatePlanId" name="ratePlanId" /></td>
               <td>总价格:<input  name="totalPrice" id="inputTotalPrice"  readonly /></td>

             </tr>
             
             <tr>
               <td colspan="4" style=" text-align:right">
                  <a class="easyui-linkbutton" iconCls="icon-search" onclick="btnQueryRatePlanClick()">价格查询</a>
                </td>
             </tr>
             </table>
         </fieldset>
        
          <fieldset>
             <legend>酒店房间预订</legend>
             <table style=" width:100%; "  class="tableSimple">
               <tr>
             	 
	            <td>宾客类型:<input type="text" id="guestTypeCode" name="guestTypeCode" /></td>
                <td>支付方式:<input id="paymentTypeCode" name="paymentTypeCode" /></td>
                <td>货币类型:<input type="text"  name="currencyCode" id="currencyCode" /></td>
                

             </tr>
             
              <tr>
                <td>订单确认方式:<input name="confirmTypeCode" id="confirmTypeCode" /></td>
                <td>订单确认语言:<input name="confirmLanguageCode"  id="confirmLanguageCode" /></td>
                <td>酒店备注:<textarea name="noteToHotel"></textarea></td>
               <td>elong备注:<textarea name="noteToElong"></textarea></td>

              </tr>
              
              <tr>
                 <td colspan="4">
                     <a class="easyui-linkbutton" iconCls="icon-save" id="btnSubmitOrder" onclick="btnSubmitOrderClick()" />提交订单</a>
                 </td>
              </tr>
              
             </table>
               <div style="  width:100%; height:180px">
            <table id="tableContacterList"></table>
         </div>
         <div style="  width:100%; height:180px" >
            <table id="tableGuestList"></table>
         </div>
          </fieldset>
             

  
        


      </form>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

 <% string basePath = Toyz4net.Core.Util.WebUtil.GetWebRootPath();
    string controllerPath = basePath + "/Admin/Order";
    
       %>
     <script  type="text/javascript">

         var _urlContacterList = "<%=controllerPath %>/DatagridGuest";

         var _valGuestsIdArray ;
         var _valContacterIdArray ;
         var _arrayGuests ;
         var _arrayContacter;
         var _hotelId;
         var _roomTypeId;
        
         var colIdNumber = { title: "身份证号码", field: "idNumber", width: 80, checkbox: true };
         var colIdTypeCode = { title: "身份证类型", field: "colIdTypeCode", width: 120 };
         var colGenderCode = { title: "", field: "genderCode", width: 120 };
         var colEmail = { title: "电子邮箱", field: "email", width: 120 };
         var colMobile = { title: "", field: "mobile", width: 120 };
         var colNationality = { title: "", field: "nationality", width: 120 };
         var colPhoneInterCode = { title: "", field: "phoneInterCode", width: 120 };
         var colPhoneAreaCode = { title: "", field: "phoneAreaCode", width: 120 };
         var colPhoneNumber = { title: "", field: "phoneNumber", width: 120 };
         var colPhoneExt = { title: "", field: "phoneExt", width: 120 };
         var colFaxInterCode = { title: "", field: "faxInterCode", width: 120 };
         var colFaxAreaCode = { title: "", field: "faxAreaCode", width: 120 };
         var colFaxNumber = { title: "", field: "faxNumber", width: 120 };
         var colFaxExt = { title: "", field: "faxExt", width: 120 };
         var colParameterString1 = { title: "", field: "parameterString1", width: 120 };
         var colParameterString2 = { title: "", field: "parameterString2", width: 120 };
         var colParameterString3 = { title: "", field: "parameterString3", width: 120 };
         var colParameterInt1 = { title: "", field: "parameterInt1", width: 120 };
         var colParameterInt2 = { title: "", field: "parameterInt2", width: 120 };
         var colParameterInt3 = { title: "", field: "parameterInt3", width: 120 };
         var colNameCn = { title: "中文名", field: "nameCn", width: 120 };
         var colNameEn = { title: "英文名", field: "nameEn", width: 120 };
         
         
          var colHotelId = { title: "", field: "hotelId", width: 80 };
          var colHotelName = { title: "酒店中文名", field: "hotelName", width: 250 };
          var colHotelNameEn = { title: "酒店英文名", field: "hotelNameEn", width: 300 };

          var tbAddGuest = {
              text: "添加住客"
            , iconCls: "icon-add"
            , handler: function() {
               $.messager.prompt("查询", "请输入身份证号", function(val) {

               $.getJSON(_urlContacterList, { ids: val }, function(json) {
                      if (json["total"] == 0) {
                          $.messager.alert("", "身份证号码:" + val + "不存在");
                      } else {
                      var rowData = json["rows"][0];
                          _arrayGuests.push(val);
                          _arrayGuests = $.unique(_arrayGuests);
                          _valGuestsIdArray = _arrayGuests.getUnionStr(",");
                         // $.each(_arrayGuests, function(i, v) {
                         //   _valGuestsIdArray += v + ",";
                       //   });
                          var opts = $("#tableGuestList").datagrid("options");
                          opts["queryParams"] = { ids: _valGuestsIdArray };
                          $("#tableGuestList").datagrid("reload");
                      }
                  });   // $.getJSON
              });  //$.messager
            }
          }

          var tbAddContacter = {
              text: "添加联系人"
            , iconCls: "icon-add"
            , handler: function() {
                $.messager.prompt("查询", "请输入身份证号", function(val) {

                    $.getJSON(_urlContacterList, { ids: val }, function(json) {
                        if (json["total"] == 0) {
                            $.messager.alert("", "身份证号码:" + val + "不存在");
                        } else {
                            var rowData = json["rows"][0];
                            _arrayContacter.push(val);
                            _arrayContacter = $.unique(_arrayContacter);
                            _valContacterIdArray = _arrayContacter.getUnionStr(",");
                           // $.each(_arrayContacter, function(i, v) {
                            //    _valContacterIdArray += v + ",";
                          //  });
                            var opts = $("#tableContacterList").datagrid("options");
                            opts["queryParams"] = { ids: _valContacterIdArray };
                            $("#tableContacterList").datagrid("reload");
                        }
                    });   // $.getJSON
                });  //$.messager
            }
          }


        var tbRemoveGuest = {
            text: "删除住客"
            , iconCls: "icon-add"
            , handler: function() {
            var selectRows = $("#tableGuestList").datagrid("getSelections");
            $.each(selectRows, function(i, rowData) {
                    _arrayGuests = $.grep(_arrayGuests, function(n, i) {
                        return n == rowData["idNumber"];
                    }, true);
                });
                _valGuestsIdArray = _arrayGuests.getUnionStr(",");
              //  $.each(_arrayGuests, function(i, v) {
              //      _valGuestsIdArray += v + ",";
              //  });
                var opts = $("#tableGuestList").datagrid("options");
                opts["queryParams"] = { ids: _valGuestsIdArray };
                $("#tableGuestList").datagrid("reload");
            }
        }

        var tbRemoveContacter = {
            text: "删除联系人"
            , iconCls: "icon-add"
            , handler: function() {
                var selectRows = $("#tableContacterList").datagrid("getSelections");
                $.each(selectRows, function(i, rowData) {
                    _arrayContacter = $.grep(_arrayContacter, function(n, i) {
                      return n == rowData["idNumber"];
                    },true);
                });
                _valContacterIdArray = _arrayContacter.getUnionStr(",");
                //$.each(_arrayContacter, function(i, v) {
                //    _valContacterIdArray += v + ",";
                //});
                var opts = $("#tableContacterList").datagrid("options");
                opts["queryParams"] = { ids: _valContacterIdArray };
                $("#tableContacterList").datagrid("reload");
            }
        }

         function btnSearchHotelClick() {
             $.messager.prompt("查询", "请输入酒店名", function(val) {
                 if (val == "") { $.messager.alert("", "查询字符不能为空"); return; }
                 var grid = $("#hotelId").combogrid("grid");
                 var opts = grid.datagrid("options");
                 opts["url"] = "<%=controllerPath %>/DatagridHotel?qvHotelName="+val;
                 //opts["queryParams"] = { qvHotelName: val };
                 grid.datagrid("reload");
                 $("#inputHotelId").combogrid ("showPanel");
             });             //$.messager.prompt("查询", "请输入酒店名", function(val) {
         }

         function searcherHotel(value, type) {
             var grid = $("#inputHotelId").combogrid("grid");
             var opts = grid.datagrid("options");
             var params = $.param({ qtHotelName: type, qvHotelName: value })
             opts["url"] = "<%=basePath %>/Admin/Hotel/Datagrid_act_normal?" + params;
             grid.datagrid("reload");
             $("#inputHotelId").combogrid("showPanel");
         }
         

         function btnSubmitOrderClick() {
             $("#formOrder").form("submit", {
                 url: "<%=controllerPath %>/DoSubmitOrder"
             , onSubmit: function() {
                 $("#contacterIdArray").val(_valContacterIdArray);
                 $("#guestsIdArray").val(_valGuestsIdArray);
             }
             });
         }

         function btnQueryRatePlanClick() {
             var valCheckInDate = $("#inputCheckInDate").datebox("getValue");
             var valCheckOutDate = $("#inputCheckOutDate").datebox("getValue");
             var valRatePlanId = $("#inputRatePlanId").combobox("getValue");

             var param = $.param({ qvHotelId: _hotelId, qvRoomTypeId: _roomTypeId
                     , qvCheckInDate: valCheckInDate, qvCheckOutDate: valCheckOutDate,qvRatePlanId:valRatePlanId
         });
         var url = "<%=controllerPath %>/DoGetOneRatePlan"
         $.post(url, { qvHotelId: _hotelId, qvRoomTypeId: _roomTypeId
                     , qvCheckInDate: valCheckInDate, qvCheckOutDate: valCheckOutDate, qvRatePlanId: valRatePlanId
         }
            , function(str) {
                var json = str.toJson();
                if (json.code == 0) {
                    var ratePlan = json["attrs"]["ratePlan"];
                    $("#inputTotalPrice").val(ratePlan["Rates"]["TotalPrice"]);
                } else if (json.code == 1) {
                    str.messager();
                }
            }
         );
             
         }

         $(document).ready(function() {
             _valGuestsIdArray = $("#guestsIdArray").val() || "";
             _valContacterIdArray = $("#contacterIdArray").val() || "";
             _arrayGuests = _valGuestsIdArray.split(',');
             _arrayContacter = _valContacterIdArray.split(',');


             $("#tableContacterList").datagrid({
                 title: "联系人列表"
             , id: "idNumber"
            , url: _urlContacterList
            , columns: [
               [colIdNumber, colNameCn, colNameEn]
            ]
            , toolbar: [tbAddContacter, tbRemoveContacter]
             });


             $("#tableGuestList").datagrid({
                 title: "住客列表"
                 , id: "idNumber"
             , url: _urlContacterList
             , columns: [
               [colIdNumber, colNameCn, colNameEn]
            ]
            , toolbar: [tbAddGuest, tbRemoveGuest]
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

             $("#inputHotelId").combogrid({
                 textField: "hotelName"
               , idField: "hotelId"
               , columns: [[colHotelId, colHotelName, colHotelNameEn]]
               , panelWidth: 500
               , width: 300
               , editable: false
               , onChange: function(nv, ov) {
                   var opts = $("#inputRoomTypeId").combobox("options");
                   _hotelId = nv;
                   var param = $.param({ qvHotelId: nv });
                   opts["url"] = "<%=controllerPath %>/ComboboxHotelRoom?" + param;
                   //opts["queryParams"] = { qvHotelId: val };
                   $("#inputRoomTypeId").combobox("reload");
               }
             });

             $("#inputRoomTypeId").combobox({
                 textField: "roomName"
                 , valueField: "roomTypeId"
                , editable: false
               , onChange: function(nv, ov) {
                   _roomTypeId = nv;
                   var opts = $("#inputRatePlanId").combobox("options");
                   var valCheckInDate = $("#inputCheckInDate").datebox("getValue");
                   var valCheckOutDate = $("#inputCheckOutDate").datebox("getValue");
                   var param = $.param({ qvHotelId: _hotelId, qvRoomTypeId: _roomTypeId
                     ,qvCheckInDate:valCheckInDate,qvCheckOutDate:valCheckOutDate
                    });
                   opts["url"] = "<%=controllerPath %>/ComboboxRatePlan?" + param;
                   //opts["queryParams"] = { qvHotelId: val };
                   $("#inputRatePlanId").combobox("reload");
               }
             });

             $("#inputRatePlanId").combobox({
                 textField: "RatePlanName"
                 , valueField: "RatePlanID"
                , editable: false
             });


         });                                 //$(document).ready(function() { 
     
     </script>
</asp:Content>
