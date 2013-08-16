<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<%
  
    string basePath = WebUtil.GetWebRootPath();
    HotelDetailModel hotelDetail = ViewData[typeof(HotelDetailModel).Name] as HotelDetailModel;    
 %>
 
 <div id="divHotelCommentEdit">
    <h3>发表评论</h3>
    <div id="divNoLoginAlert" class="ui-state-highlight ui-corner-all" style=" display:none"  >
    <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
  匿名或未登录用户不能发表！
    <div class="right">
    <a href="<%=basePath %>/Public/Home/Login"> 登录 </a>| <a href="<%=basePath %>/MemberAdmin/Reg/Index">注册</a>
    </div>
   </div>
    <textarea id="context" name="context" style="width:100%;height:200px;"></textarea>
	               <input type="radio" name="recommendInd" value="Y"  checked="checked" onclick="_recommendInd=this.value;" />
	               <span class="spanRcY" >推荐</span>
	               <input type="radio" name="recommendInd" value="N"  onclick="_recommendInd=this.value;" />
	               <span class="spanRcN" >不推荐</span>
	               <br/>
	               	           <span>验证码:</span><img id="imgVcode" src="<%=basePath %>/Public/Home/ImgValidateCode" onclick="this.src=this.src+'?xx=f'"  />
	            <input name="vCode"  id="inputVCode" style=" width:60px" />(看不清楚？点击更换)
	            &nbsp;
	            
	            <button type="button" id="btnSubmitHotelCommnet">提交</button>
     <div class="b">
       <ol  type="1">
       

          <li>本站接受善意的、客观的建议及点评，有权拒绝恶意的或欺骗性的评论</li>
          <li>请对你的言行负责，自觉遵守中华人民共和国的有关法律法规，遵守网上道德</li>
       </ol>
     </div>
</div>
	      

	       
	       
	       
	       <script type="text/javascript">

	           var _recommendInd = "Y";
	           $(document).ready(function() {





	               $("#btnSubmitHotelCommnet").click(function() {


	                   var context = $("#context").val();
	                   var valVCode = $("#inputVCode").val();


	                   var url = "<%=basePath %>/Public/Comment/DoSubmitHotelComment";
	                   $.post(url,
                     { hotelFk: "<%=hotelDetail.id %>"
                     , hotelName: "<%=hotelDetail.name %>"
                     , context: context
                     , recommendInd: _recommendInd
                         //, serviceScore: valServiceScore
                         //, facilityScore: valFacilityScore
                         //, hygieneScore: valHygieneScore
                         // , priceScore: valPriceScore
                     , vCode: valVCode
                     }
                       , function(str) {
                           //updateHotelComment();
                           var json = eval("(" + str + ")");
                           var div = $("<div></div>");
                           div.text(json["msg"]);
                           div.dialog();
                           updateHotelComment();
                           $("#inputVCode").val("");
                           $("#context").val("");
                           $("#imgVcode").click();
                       })

	               });


	               $.getJSON("<%=basePath %>/Public/Home/CheckLogin", function(json) {
	                   var code = json["code"];
	                   if (code != 0) {
	                       // $("#divHotelCommentEdit").hide();
	                       $("#divNoLoginAlert").show();

	                       //您需要登录后才可以点评酒店 登录 | 注册
	                   }

	               });


	           });
	       
	       </script>