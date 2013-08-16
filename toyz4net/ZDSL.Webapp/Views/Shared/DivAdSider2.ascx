<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Model.Public" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="NHibernate" %>
<%@ Import Namespace="NHibernate.Criterion" %>

<%
    string basePath = WebUtil.GetWebRootPath();
    ICriteria icr = BaseZdBiz.CreateCriteria<AdSiderModel>();
    icr.AddOrder(Order.Desc("recLevel"));
    IList<AdSiderModel> adSiders = icr.List<AdSiderModel>();

    string bcastr_file = "";
    string bcastr_link = "";
    string bcastr_title = "";
    string flashvars = "";

    foreach (AdSiderModel adSider in adSiders) {  
        bcastr_file += adSider.src + "|";
        bcastr_link += adSider.href + "|";
        bcastr_title += adSider.title + "|";
        
    }
    flashvars = string.Format("bcastr_file={0}&bcastr_link={1}&bcastr_title={2}"
        ,bcastr_file.Trim('|')
        ,bcastr_link.Trim('|')
        , bcastr_title.Trim('|')
        );
 %>

<object width="319" height="125"  codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000">
<param value="<%=basePath %>/flash/bcastr3.swf" name="movie">
<param value="high" name="quality">
<param value="false" name="menu">
<param value="opaque" name="wmode">
<param value="<%=flashvars %>" name="FlashVars">
<embed width="319" height="125" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" quality="high" false"="" flashvars="<%=flashvars  %>&menu=" wmode="opaque" src="<%=basePath %>/flash/bcastr3.swf">
</object>