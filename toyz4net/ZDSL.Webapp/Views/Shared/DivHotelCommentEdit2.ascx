<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<%
  
    string basePath = WebUtil.GetWebRootPath();
        
 %>
 
 
 
  <div class="left" style=" width:60%; ">
<textarea id="context" name="context" style="width:100%;height:200px;visibility:hidden;"></textarea>
	        </div>
	       
	       <div class="right" style=" width:39% ;">
                
           <fieldset >
	          <legend>评分</legend>
				<ul>
				 <li><span>服务</span><div id="divServiceScore" style=" display:inline"></div></li>
				 <li><span>卫生</span><div id="divHygieneScore" style=" display:inline"></div></li>
				 <li><span>设施</span><div id="divFacilityScore" style=" display:inline"></div></li>
				 <li><span>价格</span><div id="divPriceScore" style=" display:inline"> </div></li>
			   </ul>
	       </fieldset>
	       <div>
	       <ul>
	         <li>
	           <span>验证码:</span><img src="<%=basePath %>/Public/Home/ImgValidateCode" onclick="this.src=this.src+'?xx=f'"  />
	            <input name="vCode"  id="inputVCode"/>
	         </li>
	         <li>
	           <button type="button" id="btnSubmitHotelCommnet">提交评论</button>
	         </li>
	       </ul>
	       
	       
	       </div>
	       </div>
	       
	       <script type="text/javascript">

	           var valServiceScore = 0;
	           var valHygieneScore = 0;
	           var valFacilityScore = 0;
	           var valPriceScore = 0;

	           $(document).ready(function() {

	                   KE.show({
	                       id: 'context',
	                       resizeMode: 1,
	                       allowPreviewEmoticons: false,
	                       allowUpload: false,
	                       items: []
	                   });



	           $("#btnSubmitHotelCommnet").click(function() {


	               var context = KE.html("context");
	               var valVCode = $("#inputVCode").val();
	               var url = "<%=basePath %>/Public/Comment/DoSubmitHotelComment";
	               $.post(url,
                     { hotelFk: hotelId
                     , context: context
                     , serviceScore: valServiceScore
                     , facilityScore: valFacilityScore
                     , hygieneScore: valHygieneScore
                     , priceScore: valPriceScore
                     , vCode: valVCode
                     }
                       , function(str) {
                           updateHotelComment();
                           var json = eval("(" + str + ")");
                           var div = $("<div></div>");
                           div.text(json["msg"]);
                           div.dialog();

                       })

	           });


	           $("#divServiceScore").raty({
	               path: "<%= basePath  %>/js3rd/jquery.raty-0.5/img/",
	               onClick: function(score) {
	                   valServiceScore = score;
	               }
	           });

	           $("#divFacilityScore").raty({
	               path: "<%= basePath  %>/js3rd/jquery.raty-0.5/img/",
	               onClick: function(score) {
	                   valFacilityScore = score;
	               }
	           });

	           $("#divHygieneScore").raty({
	               path: "<%= basePath  %>/js3rd/jquery.raty-0.5/img/",
	               onClick: function(score) {
	                   valHygieneScore = score;
	               }
	           });

	           $("#divPriceScore").raty({
	               path: "<%= basePath  %>/js3rd/jquery.raty-0.5/img/",
	               onClick: function(score) {
	                   valPriceScore = score;
	               }
	           });
	           
	           
	           });
	       
	       </script>