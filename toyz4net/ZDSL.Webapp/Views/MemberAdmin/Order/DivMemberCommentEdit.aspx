<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>


<%
  
    string basePath = WebUtil.GetWebRootPath();
    OrderModel order = ViewData[typeof(OrderModel).Name] as OrderModel;
    %>

<form  method="post" action="<%=basePath %>/MemberAdmin/Order/DoSubmitMemberComment">
<input type="hidden" value="<%=order.id %>" name="orderFk" />
<textarea id="context" name="context" style="width:100%;height:300px;"></textarea>
	               <input type="radio" name="recommendInd" value="Y"  checked="checked" onclick="_recommendInd=this.value;" />
	               <span class="spanRcY" >推荐</span>
	               <input type="radio" name="recommendInd" value="N"  onclick="_recommendInd=this.value;" />
	               <span class="spanRcN" >不推荐</span> &nbsp;
	               	           <span>验证码:</span><img src="<%=basePath %>/Public/Home/ImgValidateCode" onclick="this.src=this.src+'?xx=f'"  />
	            <input name="vCode"  id="inputVCode"/>
	            &nbsp;
	            <button type="submit" id="btnSubmitHotelCommnet">提交</button>
</form>