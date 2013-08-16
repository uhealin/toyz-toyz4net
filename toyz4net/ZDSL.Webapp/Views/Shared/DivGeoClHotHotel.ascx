<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<%@ Import Namespace="ZDSL.Model.Data" %>
<%@ Import Namespace="Toyz4net.Core.Util" %>
<%@ Import Namespace="ZDSL.Biz" %>
<%@ Import Namespace="ZDSL.Webapp.Controllers" %>
<% 
    string basePath =WebUtil.GetWebRootPath();
    Dictionary<string, IList<HotelDetailModel>> dictCityHotel = ViewData[BaseZdController.VD_KEY_CITY_HOT_HOTEL_DICT] as Dictionary<string, IList<HotelDetailModel>>;
    GeoBiz geoBiz=GeoBiz.GetInstant();
 %>