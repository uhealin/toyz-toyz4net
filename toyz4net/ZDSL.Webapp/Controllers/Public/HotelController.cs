using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Data;
using ZDSL.Biz;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.NorthBoundAPIService;
using Toyz4net.Core.Util;
using System.Collections;
using ZDSL.Model.Public;

namespace ZDSL.Webapp.Controllers.Public
{
    public class HotelController : BaseZdController
    {


        public static string GetPathIndex()
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Hotel/Index");
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(),filePath);
            }
        }

        public static string GetPathDetail(string hotelId)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Detail"];
            filePath = string.Format(filePath, hotelId);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Hotel/Detail?hotelId="+hotelId);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }


        public static string GetPathCityHotel(GeoModel geo) {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.City"];
            filePath = string.Format(filePath,geo.cityName, geo.cityCode);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Hotel/Search?cityName=" + geo.cityName );
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }

        public static string GetPathDHotel(GeoModel geo,GeoLocationModel geoD)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.D"];
            filePath = string.Format(filePath, geo.cityCode,geoD.locationId);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Hotel/Search?cityName=" + geo.cityName + "&geoDId="+ geoD.locationId);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath );
            }
        }

        public static string GetPathClHotel(GeoModel geo, GeoLocationModel geoCl)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Hotel.Cl"];
            filePath = string.Format(filePath, geo.cityCode,geoCl.locationId);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Hotel/Search?cityName=" + geo.cityName + "&geoClId=" + geoCl.locationId);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath );
            }
        }

        public ActionResult Index()
        {

            PublicBiz publicBiz = PublicBiz.GetInstant();
            FrontPageModel frontPage = publicBiz.getCurFrontPage();


            string[] cityNames = frontPage.homeHotCityNameArray.Split(',');
            GeoBiz geoBiz = GeoBiz.GetInstant();
            IList<GeoModel> geos = new List<GeoModel>();
            foreach (string cityName in cityNames)
            {
                GeoModel geo = geoBiz.GetGeoByCityName(cityName);
                geo.geoCls = geoBiz.GetGeoCls(geo.id);
                geo.geoDs = geoBiz.GetGeoDs(geo.id);
                geo.geoLls = geoBiz.GetGeoLls(geo.id);
                geos.Add(geo);

            }
            ViewData[typeof(IList<GeoModel>).Name] = geos;
            this.VdCityTopHotel(20,cityNames);
            this.VdHotBookingHotel(10);
            this.VdHotBrand(12);
            this.VdHotCity(15);

            this.setPageDesc(PublicBiz.getCurPageSeo().hotelIndexDesc);
            this.setPageKeyWords(PublicBiz.getCurPageSeo().hotelIndexKeywords);

            return View();
        }

        //
        // GET: /Hotel/

        public ActionResult Detail(string hotelId)
        {
            HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotelId);
            ViewData[typeof(HotelDetailModel).Name] = hotelDetail;

            HotelFeatrueInfoModel hotelFi = BaseZdBiz.Load<HotelFeatrueInfoModel>(hotelId);
            ViewData[typeof(HotelFeatrueInfoModel).Name] = hotelFi;

            HotelModel hotel = BaseZdBiz.Load<HotelModel>(hotelId);
            ViewData[typeof(HotelModel).Name] = hotel;

            ICriteria icr = BaseZdBiz.CreateCriteria<HotelImageModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelImageModel> images = icr.List<HotelImageModel>();
            ViewData[typeof(HotelImageModel).Name] = images;

            GeoBiz geoBiz = GeoBiz.GetInstant();
            GeoModel geo = geoBiz.GetGeoByCityId(hotelDetail.city);
            this.VdGeoLocation(geo);
          

            icr = BaseZdBiz.CreateCriteria<HotelLandMarkModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelLandMarkModel> hotelLls=icr.List<HotelLandMarkModel>();
            ViewData[typeof(HotelLandMarkModel).Name] = hotelLls;

            icr = BaseZdBiz.CreateCriteria<HotelSurroundingAttractionModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelSurroundingAttractionModel> hotelSas = icr.List<HotelSurroundingAttractionModel>();
            ViewData[typeof(HotelSurroundingAttractionModel).Name] = hotelSas;

            icr = BaseZdBiz.CreateCriteria<HotelSurroundingCommerceModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelSurroundingCommerceModel> hotelScs = icr.List<HotelSurroundingCommerceModel>();
            ViewData[typeof(HotelSurroundingCommerceModel).Name] = hotelScs;

            icr = BaseZdBiz.CreateCriteria<HotelSurroundingRestaurantModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelSurroundingRestaurantModel> hotelSrs = icr.List<HotelSurroundingRestaurantModel>();
            ViewData[typeof(HotelSurroundingRestaurantModel).Name] = hotelSrs;

            icr = BaseZdBiz.CreateCriteria<HotelSurroundingShopModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelSurroundingShopModel> hotelSss = icr.List<HotelSurroundingShopModel>();
            ViewData[typeof(HotelSurroundingShopModel).Name] = hotelSss;

            icr = BaseZdBiz.CreateCriteria<HotelTrafficInfoModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            IList<HotelTrafficInfoModel> hotelTrs = icr.List<HotelTrafficInfoModel>();
            ViewData[typeof(HotelTrafficInfoModel).Name] = hotelTrs;


            icr = BaseZdBiz.CreateCriteria<HotelCommentModel>(new PagerObject(1, 5));
            icr.Add(Restrictions.Eq("hotelFk", hotelDetail.id));
            icr.AddOrder(Order.Desc("createDate"));
            IList<HotelCommentModel> hotelComments = icr.List<HotelCommentModel>();
            ViewData[typeof(HotelCommentModel).Name] = hotelComments;
            PageSeoModel seo=PublicBiz.getCurPageSeo();
            this.setPageDesc(string.Format(seo.hotelDetailDesc, hotel.hotelId, hotel.hotelName,hotelDetail.address,hotelDetail.description, geo.cityCode, geo.cityName));
            this.setPageKeyWords(string.Format(seo.hotelDetailKeywords, hotel.hotelId, hotel.hotelName, hotelDetail.address, hotelDetail.description, geo.cityCode, geo.cityName));
            this.VdHotCity(15);
            return View();
        }

        [HttpPost]
        public ActionResult QuickSearch(string cityName,DateTime checkInDate,DateTime checkOutDate,string keyword) {
            ViewData["cityName"] = cityName;
            ViewData["checkInDate"] = checkInDate;
            ViewData["checkOutDate"] = checkOutDate;
            ViewData["keyword"] = keyword;
            return View();
        }


        public ActionResult TableHotelRoomPriceInfo(string hotelId, DateTime checkInDate, DateTime checkOutDate) {

            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            PriceHotel[] priceInfo= elongBiz.getHotelRoomPrice(hotelId, checkInDate, checkOutDate);
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelRoomModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelRoomModel> rooms = icr.List<HotelRoomModel>();
            ViewData[typeof(HotelRoomModel).Name] = rooms;
            ViewData[typeof(PriceHotel).Name] = priceInfo;
            ViewData["checkInDate"] = checkInDate.ToString(DateTimeUtil.PATTERN_DB_DATE);
            ViewData["checkOutDate"] = checkOutDate.ToString(DateTimeUtil.PATTERN_DB_DATE);
            return View();
        }


        public ActionResult TableHotelRoomPriceInfo2(string hotelId, DateTime checkInDate, DateTime checkOutDate)
        {

            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            //PriceHotel[] priceInfo = elongBiz.getHotelRoomPrice(hotelId, checkInDate, checkOutDate);
            GetHotelConditionForGetHotelList condition = new GetHotelConditionForGetHotelList();
            condition.CheckInDate = checkInDate;
            condition.CheckOutDate = checkOutDate;
            condition.HotelId = hotelId;

            HotelForGetHotelList hotel= elongBiz.getHotelList(condition).ElementAt<HotelForGetHotelList>(0);
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelRoomModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelRoomModel> rooms = icr.List<HotelRoomModel>();
            ViewData[typeof(HotelRoomModel).Name] = rooms;
            ViewData[typeof(HotelForGetHotelList).Name] =hotel;
            ViewData["checkInDate"] = checkInDate.ToString(DateTimeUtil.PATTERN_DB_DATE);
            ViewData["checkOutDate"] = checkOutDate.ToString(DateTimeUtil.PATTERN_DB_DATE);
            return View();
        }


        public ActionResult ViewHotelSearch(string viewName,string cityName, DateTime? checkInDate, DateTime? checkOutDate, string keyword,string hotelId
            , string star, string priceRegexp, string geoLlId, string geoClId,string geoDid,string brandName,string orderbyCode,string orderbyType
            ,decimal? startLng,decimal? endLng,decimal? startLat,decimal?  endLat,string geoMode,int? radius
            ) {
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            GeoBiz geoBiz=GeoBiz.GetInstant();
          
            string cityId = geoBiz.GetGeoByCityName(cityName).cityCode;
             ToyzNumberRangeObject priceRange=null;
            GetHotelConditionForGetHotelList condition = new GetHotelConditionForGetHotelList();
            condition.CheckInDate = checkInDate ?? this.getDefaultCheckInDate();
            condition.CheckOutDate = checkOutDate ?? this.getDefaultCheckOutDate();
            condition.CityId = cityId;
            //condition.HotelName = keyword;
            condition.MaxRows = this.getPager().size;
            condition.PageIndex = this.getPager().page;

            if (string.IsNullOrEmpty(geoMode)||geoMode=="0")
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    condition.HotelName = keyword;
                }

                if (!string.IsNullOrEmpty(hotelId)) {

                    condition.HotelId = hotelId;
                    condition.CityId = null;
                }

                if (!string.IsNullOrEmpty(brandName))
                {
                    condition.HotelName = brandName;
                }
                if (!string.IsNullOrEmpty(star))
                {
                    condition.StarCode = star;
                }
   
                if (!string.IsNullOrEmpty(priceRegexp))
                {
                    priceRegexp = HttpUtility.UrlDecode(priceRegexp);
                    IToyzObjcet toyzPrice = NumberUtil.ParseToyz(priceRegexp);
                    priceRange = toyzPrice as ToyzNumberRangeObject;
                    if (priceRange != null)
                    {
                        condition.LowestRate = priceRange.min;
                        condition.HighestRate = priceRange.max;
                    }
                }

                if (!string.IsNullOrEmpty(geoLlId))
                {
                    condition.LandmarkLocationID = geoLlId;
                }
                else if (!string.IsNullOrEmpty(geoClId))
                {
                    condition.CommercialLocationId = geoClId;
                }
                else if (!string.IsNullOrEmpty(geoDid))
                {
                    condition.DistrictId = geoDid;
                }

            }
            else if(geoMode=="1") {

                condition.StartLatitude = startLat ??0;
                condition.StartLongitude = startLng??0;
                condition.EndLatitude = endLat??0;
                condition.EndLongitude = endLng??0;

                condition.PositionModeCode = geoMode;

            }
            else if (geoMode == "2") {
                condition.StartLatitude = startLat??0;
                condition.StartLongitude = startLng??0;
                condition.EndLatitude = condition.StartLatitude;
                condition.EndLongitude = condition.StartLongitude;
                condition.PositionModeCode = geoMode;
                condition.Radius = radius ??0;
            }



            if (!string.IsNullOrEmpty(orderbyCode))
            {
                condition.OrderByCode = orderbyCode;
                condition.OrderTypeCode = orderbyType;
            }
            else {
                condition.OrderTypeCode = "desc";
            }
            
            
            
           
            PageList<HotelForGetHotelList> hotels= elongBiz.getHotelList(condition);
            
            /*
            if(priceRegexp!=null){
                foreach (HotelForGetHotelList hotel in hotels) {
                    foreach()
                    RoomForGetHotelList[] rooms = hotel.Rooms;
                }    
             */
            
             
            

            IList<string> hotelIds=new List<string>();
            foreach (HotelForGetHotelList hotel in hotels) {
                hotelIds.Add( hotel.HotelId);
            }


            ICriteria icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
  
            icr.Add(Restrictions.In("id", hotelIds.ToArray()));
         
            IList<HotelDetailModel> hotelDetails = icr.List<HotelDetailModel>();

            icr = BaseZdBiz.CreateCriteria<HotelFeatrueInfoModel>();
            icr.Add(Restrictions.In("hotelId", hotelIds.ToArray()));
            IList<HotelFeatrueInfoModel> hotelFeats = icr.List<HotelFeatrueInfoModel>();
            
            Dictionary<string,HotelDetailModel> dictHotelDetails= hotelDetails.ToDictionary(p=>p.id);
            Dictionary<string, HotelFeatrueInfoModel> dictHotelFeat = hotelFeats.ToDictionary(p => p.hotelId);
            ViewData[typeof(PageList<HotelForGetHotelList>).Name] = hotels;
            ViewData[typeof(Dictionary<string, HotelDetailModel>).Name] = dictHotelDetails;
            ViewData[typeof(HotelFeatrueInfoModel).Name] = dictHotelFeat;
            ViewData[typeof(ToyzNumberRangeObject).Name] = priceRange;
            return View(viewName);                                   
        }

        public ActionResult ULGeoLocationList(string cityName, string type) {
            GeoBiz geoBiz=GeoBiz.GetInstant();
            GeoModel geo = geoBiz.GetGeoByCityName(cityName);
            IList<GeoLocationModel> locations = new List<GeoLocationModel>();
            string typeName = "";
            if (type == "geoDs") {
                typeName = "行政区";
                locations = geoBiz.GetGeoDs(geo.id) as IList<GeoLocationModel>;
            }
            else if (type == "geoCls") {
                typeName = "商业区";
               locations=geoBiz.GetGeoCls(geo.id) as IList<GeoLocationModel>;
            }
            else if (type == "geoLLs")
            {
                typeName = "标志物";
                locations = geoBiz.GetGeoLls(geo.id) as IList<GeoLocationModel>;
            }
            ViewData[typeof(GeoLocationModel).Name] = locations;
            ViewData["typeName"] = typeName;
            return View();
        }

        public ActionResult IframeHotelImage(string hotelId) {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelImageModel>();
            icr.Add(Restrictions.Eq("hotelFk", hotelId));
            IList<HotelImageModel> images = icr.List<HotelImageModel>();
            ViewData[typeof(HotelImageModel).Name] = images;
            return View();
        }

        public ActionResult TableGeoLocationList(string cityName) {
            GeoBiz geoBiz = GeoBiz.GetInstant();
            GeoModel geo = geoBiz.GetGeoByCityName(cityName);
            IList<GeoCommercialLocationModel> geoCls = geoBiz.GetGeoCls(geo.id);
            ViewData[typeof(GeoCommercialLocationModel).Name] = geoCls;

            //icr = BaseZdBiz.CreateCriteria<GeoDistrictsModel>(pager);
            //icr.Add(Restrictions.Eq("geoFk", geo.id));
            IList<GeoDistrictsModel> geoDs = geoBiz.GetGeoDs(geo.id);
            ViewData[typeof(GeoDistrictsModel).Name] = geoDs;

            //icr = BaseZdBiz.CreateCriteria<GeoLandmarkLocationModel>(pager);
            //icr.Add(Restrictions.Eq("geoFk", geo.id));
            IList<GeoLandmarkLocationModel> geoLLs = geoBiz.GetGeoLls(geo.id);
            ViewData[typeof(GeoLandmarkLocationModel).Name] = geoLLs;
            return View();
        }

        public ActionResult OrderEdit(string hotelId, string roomTypeId, int ratePlanId, DateTime? checkInDate, DateTime? checkOutDate)
        {
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            RoomForGetHotelList roomForGetHotelList = elongBiz.getRoomForGetHotelList(hotelId, roomTypeId, checkInDate ?? this.getDefaultCheckInDate(), checkOutDate ?? this.getDefaultCheckOutDate(), ratePlanId);
            HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotelId);
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelRoomModel>();
            icr.Add(Restrictions.And(Restrictions.Eq("hotelFk", hotelId),Restrictions.Eq("roomTypeId",roomTypeId)));
            IList<HotelRoomModel> rooms = icr.List<HotelRoomModel>();
            if (rooms.Count == 1) {
                ViewData[typeof(HotelRoomModel).Name] = rooms[0];
            } 
            ViewData["checkInDate"] = (checkInDate ?? this.getDefaultCheckInDate()).ToString(DateTimeUtil.PATTERN_DB_DATE);
            ViewData["checkOutDate"] = (checkOutDate ?? this.getDefaultCheckOutDate()).ToString(DateTimeUtil.PATTERN_DB_DATE);
            ViewData[typeof(RatePlanForGetHotelList).Name] = roomForGetHotelList.RatePlans[0];
            ViewData[typeof(HotelDetailModel).Name] = hotelDetail;
            return View();
        }

        public ActionResult DoSubmitOrder(string arraivalLastTimeType)
        {
            JsResultObject re = new JsResultObject();
            MemberModel member=this.getAuthMember();


            OrderModel order = new OrderModel();
            DateTimeUtil.FixMSSQLNullDateTime(ref order);
            order.createDate = DateTime.Now;
           
            order = WebUtil.Eval(order, "", "");

            if (member != null)
            {
                order.memberFk = member.id;
                // re.code = JsResultObject.CODE_ALERT;
                // re.msg = "未登录用户不能进行酒店预定";

                // WebUtil.SetSessionAttr(typeof(JsResultObject).Name,re);
                // return RedirectToAction("Login","Home");
                //WebUtil.SetSessionAttr(typeof(OrderModel).Name, order);
            }
            if (arraivalLastTimeType == "a") {
                order.arraivalLateTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,18,0,0);
            }
            else if (arraivalLastTimeType == "b") {
                DateTime dt =DateTime.Now.AddDays(1);
                order.arraivalLateTime = new DateTime(dt.Year, dt.Month, dt.Day, 6, 0, 0);
            }

            DataBiz dataBiz = DataBiz.GetInstant();
            re = dataBiz.createNewOrder(order);
            re.attrs.Add(typeof(OrderModel).Name, order);
            ViewData[typeof(JsResultObject).Name] = re;

            HotelRoomModel room = BaseZdBiz.Load<HotelRoomModel>(Restrictions.Eq("hotelFk", order.hotelId), Restrictions.Eq("roomTypeId", order.roomTypeId));
            ViewData[typeof(HotelRoomModel).Name] = room;
            return View("OrderFeedback");
        }


        public ActionResult Search(string cityName, string keyWord, string geoClId, string geoDId,DateTime? checkInDate)
        {
            PublicBiz publicBiz = PublicBiz.GetInstant();
            FrontPageModel frontPage = publicBiz.getCurFrontPage();
            GeoBiz geoBiz = GeoBiz.GetInstant();
            GeoModel geo=geoBiz.GetGeoByCityName(cityName);
            this.VdGeoLocation(geo);
            GeoLocationModel geoLocation = null;
            PageSeoModel seo=PublicBiz.getCurPageSeo();

            this.setPageDesc(string.Format( seo.cityDesc, geo.cityCode, geo.cityName));
            this.setPageKeyWords(string.Format(seo.cityKeywords, geo.cityCode, geo.cityName));
            if (!string.IsNullOrEmpty(geoClId)) {
                geoLocation = BaseZdBiz.Load<GeoCommercialLocationModel>(Restrictions.Eq("geoFk",geo.id),Restrictions.Eq("locationId",geoClId));
                this.setPageDesc(string.Format(seo.cityClDesc,geo.cityCode,geo.cityName,geoLocation.locationId,geoLocation.name));
                this.setPageKeyWords(string.Format(seo.cityClKeywords, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
            }
            else if (!string.IsNullOrEmpty(geoDId)) {
                geoLocation = BaseZdBiz.Load<GeoDistrictsModel>(Restrictions.Eq("geoFk", geo.id), Restrictions.Eq("locationId", geoDId));
                this.setPageDesc(string.Format(seo.cityDDesc, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
                this.setPageKeyWords(string.Format(seo.cityDKeywords, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
            }
            else if (!string.IsNullOrEmpty(keyWord)) {

                this.setPageDesc(string.Format(seo.hotelSearchDesc, cityName, keyWord, (checkInDate ?? DateTime.Now).ToShortDateString()));

                this.setPageKeyWords(string.Format(seo.hotelSearchKeywords,cityName,keyWord,(checkInDate?? DateTime.Now).ToShortDateString()));
            }
            ViewData[typeof(GeoLocationModel).Name] = geoLocation;

            ICriteria icr = BaseZdBiz.CreateCriteria<BrandModel>();
            string[] brandNames = frontPage.searchBrandNameArray.Split(',');
            icr.Add(Restrictions.In("brandName", brandNames));
            IList<BrandModel> brands = icr.List<BrandModel>();
            ViewData[typeof(BrandModel).Name] = brands;
            this.VdHotCity(15);
            return View();
        }

        public ActionResult DoSearch() { 
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            GetHotelConditionForGetHotelList condition=new GetHotelConditionForGetHotelList();
            condition=WebUtil.Eval(condition,"","");
            PageList<HotelForGetHotelList> hotels= elongBiz.getHotelList(condition);
            return JsonText(hotels, JsonRequestBehavior.AllowGet);
        }
    }
}
