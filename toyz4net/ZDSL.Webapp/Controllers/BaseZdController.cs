using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Biz;
using Toyz4net.Core.Model;
using Toyz4net.Core.Util;
using NHibernate;
using log4net;
using ZDSL.Model.Admin;
using ZDSL.Model.Data;
using NHibernate.Criterion;

namespace ZDSL.Webapp.Controllers
{
    public abstract class BaseZdController : Controller
    {
        
        private ELongApiBiz elongBiz;
        private AdminBiz adminBiz;

        private static ILog Logger;

        private static void CreateLooger(){
             if(Logger==null){
               Logger=LogManager.GetLogger("");
             }
        }

        protected PagerObject getPager() {

            int page = ObjectUtil.ParseInt(this.Request.Params["page"], 1);
            int rows = ObjectUtil.ParseInt(this.Request.Params["rows"], Int32.MaxValue);
            return new PagerObject(page,rows);
        }

        //
        // GET: /Base/

        protected string getPreUrl() {
            string preUrl = ObjectUtil.Parse(this.Request.Params["preUrl"],"");
            if(preUrl!=""){
               WebUtil.SetSessionAttr("preUrl", preUrl);
            }
            else if (this.Session["preUrl"] != null)
            {
                preUrl = this.Session["preUrl"].ToString();
            }
            else {
                preUrl = WebUtil.GetWebRootPath()+"/index.html";
            }
            return preUrl;
            
        }

        protected void setPreUrl(string url) {
            WebUtil.SetSessionAttr("preUrl", url);
        }


        protected void setOrderby(ref ICriteria icr) {
            string orderbyName = WebUtil.GetParamAttr("orderbyName",null);
            if (orderbyName == null) return;
            string orderbyType = WebUtil.GetParamAttr("orderbyType", "desc");
            if (orderbyType == "desc") {
                icr.AddOrder(Order.Desc(orderbyName));
            }
            else if (orderbyType == "asc")
            {
                icr.AddOrder(Order.Asc(orderbyName));
            }
            else {
                icr.AddOrder(Order.Desc(orderbyName));
            }
        }

        public  static string GetAppKey(string prefix) {
          return System.Configuration.ConfigurationManager.AppSettings[prefix+".appKey"];
        }

        public  static string GetAppSecret(string prefix)
        {
            return System.Configuration.ConfigurationManager.AppSettings[prefix+".appSecret"];
        }

        public ActionResult List() {

           
            
        return View(); 
        
        }

        //
        // GET: /Base/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Base/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Base/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }




        protected ActionResult JsonText(object obj,JsonRequestBehavior jrb) {
            return Json(obj,"text/html",jrb);
        }

        protected void info(string msg)
        {
            CreateLooger();
            string val = string.Format("url:{0} ; msg:{1}", Request.Url, msg);
            Logger.Info(val);
        }

        protected void debug(string msg) {
            CreateLooger();
            string val = string.Format("url:{0} ; msg:{1}",Request.Url,msg);
            Logger.Debug(val);
        }

        protected void error(string msg) {
            CreateLooger();
            string val = string.Format("url:{0} ; msg:{1}", Request.Url, msg);
            Logger.Error(val);
        }



        protected bool reflashDictCache (){ 
            string path = Server.MapPath("");
            CacheBiz cacheBiz = CacheBiz.GetInstant(path);
            return cacheBiz.reflashDictCache();
        }

        protected UserModel getAuthUser() {
            UserModel  user = WebUtil.GetSessionAttr<UserModel>(AdminBiz.SESSION_KEY_USER);
            if(user==null){

                Response.Redirect(WebUtil.GetWebRootPath() + "/Admin/System/Login");
            }
            return user;
            
        }

        protected MemberModel getAuthMember()
        {
            MemberModel member = WebUtil.GetSessionAttr<MemberModel>(typeof(MemberModel).Name);
            return member;

        }


        public static string GetTopNormalImgUrl(string hotelId)
        {
            return WebUtil.GetWebRootPath() + "/image/hotel/" + hotelId + "/top.normal.jpg";

        }

        public static string GetTopThumImgUrl(string hotelId)
        {
            return WebUtil.GetWebRootPath() + "/image/hotel/" + hotelId + "/top.thum.jpg";

        }


        public void SetResult(int code, string msg,bool isOnce) {

            JsResultObject re = new JsResultObject();
            re.code = code;
            re.msg = msg;
            SetResult(re,isOnce);
        }

        public void SetResult(JsResultObject re,bool isOnce) {
            if (!isOnce)
            {
                WebUtil.SetSessionAttr(typeof(JsResultObject).Name, re);
               
            }
                 ViewData[typeof(JsResultObject).Name] = re;
            
            
            
        }

        public static bool IsDebug() {
            //string debug = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.debug"];
            AdminBiz adminBiz = AdminBiz.GetInstant();
            return AdminBiz.GetCurrConfig().staticInd != BaseModel.IND_Y;
        }


        public static string VD_KEY_HOT_NEWS_LIST = "vdHotNewsList";
        public static string VD_KEY_HOT_BRAND_LIST = "vdHotBrandList";
        public static string VD_KEY_CITY_HOT_HOTEL_DICT = "vdCityHotHotelDict";
        public static string VD_KEY_CITY_HOT_GEO_CLS_DICT = "vdCityHotGeoClsDict";
        public static string VD_KEY_HOT_COMMENT_HOTEL_LIST = "vdHotCommentHotelList";
        public static string VD_KEY_HOT_WEEK_BOOKING_HOTEL_LIST = "vdHotWeekBookingHotelList";
        public static string VD_KEY_HOT_MONTH_BOOKING_HOTEL_LIST = "vdHotMonthBookingHotelList";
        public static string VD_KEY_GEO = "vdGeo";
        public static string VD_KEY_GEO_LLS = "vdGeoLls";
        public static string VD_KEY_GEO_DS = "vdGeoDs";
        public static string VD_KEY_GEO_CLS = "vdGeoCls";
        public static string VD_KEY_HOT_CITY = "vdHotCity";

        protected void VdHotNews(int rows) {
            ICriteria icr = BaseZdBiz.CreateCriteria<NewsModel>(new PagerObject(1, rows));
            IList<NewsModel> listNews = icr.List<NewsModel>();
            ViewData[VD_KEY_HOT_NEWS_LIST] = listNews;
        }

        protected void VdGeoLocation(GeoModel geo) {
            GeoBiz geoBiz = GeoBiz.GetInstant();
            ViewData[VD_KEY_GEO] = geo;
            IList<GeoCommercialLocationModel> geoCls = geoBiz.GetGeoCls(geo.id);
            ViewData[VD_KEY_GEO_CLS] = geoCls;


            IList<GeoDistrictsModel> geoDs = geoBiz.GetGeoDs(geo.id);
            ViewData[VD_KEY_GEO_DS] = geoDs;


            IList<GeoLandmarkLocationModel> geoLLs = geoBiz.GetGeoLls(geo.id);
            ViewData[VD_KEY_GEO_LLS] = geoLLs;

        }

        protected void VdHotBookingHotel(int rows) {
            
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>(new PagerObject(1,rows));
            icr.AddOrder(Order.Desc("monthBookedCount"));
            IList<HotelModel> hotels = icr.List<HotelModel>();
            ViewData[VD_KEY_HOT_MONTH_BOOKING_HOTEL_LIST] = hotels;
            /*
            IList<string> monthIds = new List<string>();
            foreach (HotelModel hotel in hotels)
            {
                monthIds.Add(hotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
            icr.Add(Restrictions.In("id", monthIds.ToArray()));
             IList<HotelDetailModel> monthDetails = icr.List<HotelDetailModel>();
            ViewData[VD_KEY_HOT_MONTH_BOOKING_HOTEL_LIST] = monthDetails;
             * */

            icr = BaseZdBiz.CreateCriteria<HotelModel>(new PagerObject(1, rows));
            icr.AddOrder(Order.Desc("weekBookedCount"));
             hotels = icr.List<HotelModel>();
             ViewData[VD_KEY_HOT_WEEK_BOOKING_HOTEL_LIST] = hotels;
            /*
            IList<string> weekIds = new List<string>();
            foreach (HotelModel hotel in hotels)
            {
                weekIds.Add(hotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
            icr.Add(Restrictions.In("id", weekIds.ToArray()));
            IList<HotelDetailModel> weekDetails = icr.List<HotelDetailModel>();
             
            ViewData[VD_KEY_HOT_WEEK_BOOKING_HOTEL_LIST] = weekDetails;
             * */
        }

        protected void VdHotelComment() { 
        
        }

        protected void VdHotelBaseInfo() { 
        
        }

        protected void VdHotBrand(int rows) {

            ICriteria icr = BaseZdBiz.CreateCriteria<BrandModel>(new PagerObject(1,rows));
            icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
            icr.Add(Restrictions.Eq("type", BrandModel.TYPE_CLASS));
            icr.AddOrder(Order.Desc("recLevel"));
            IList<BrandModel> list1 = icr.List<BrandModel>();

            icr = BaseZdBiz.CreateCriteria<BrandModel>(new PagerObject(1, rows));
            icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
            icr.Add(Restrictions.Eq("type", BrandModel.TYPE_ECONMY));
            icr.AddOrder(Order.Desc("recLevel"));
            IList<BrandModel> list2 = icr.List<BrandModel>();

            IList<BrandModel> listHotBrand = new List<BrandModel>();
            foreach (BrandModel brand in list1) {
                listHotBrand.Add(brand);
            }
            foreach (BrandModel brand in list2)
            {
                listHotBrand.Add(brand);
            }
            ViewData[VD_KEY_HOT_BRAND_LIST] = listHotBrand;
        }

        protected void VdCityTopHotel(int rows,string[] cityNames) {
            Dictionary<string, IList<HotelDetailModel>> dictCityHotels = new Dictionary<string, IList<HotelDetailModel>>();
            Dictionary<string, IList<GeoCommercialLocationModel>> dictCityGeoCls = new Dictionary<string, IList<GeoCommercialLocationModel>>();
            ICriteria icr = null;
            GeoBiz geoBiz = GeoBiz.GetInstant();
            DataBiz dataBiz = DataBiz.GetInstant();
        
            IList<HotelDetailModel> hotelDetails =dataBiz.getHotHotelDetail();

            foreach (string cityName in cityNames)
            {
                IList<HotelDetailModel> tempHotelDetails = new List<HotelDetailModel>();
                GeoModel geo=geoBiz.GetGeoByCityName(cityName);
                string cityId = geo.cityCode;
                
                foreach (HotelDetailModel hotelDetail in hotelDetails) {
                    if (tempHotelDetails.Count == rows) {
                        break;
                    }
                    if (hotelDetail.city == cityId) {
                        tempHotelDetails.Add(hotelDetail);
                    }
                
                }

                dictCityHotels.Add(cityName, tempHotelDetails);
                IList<GeoCommercialLocationModel> geoCls = geoBiz.GetGeoCls(geo.id);
                dictCityGeoCls.Add(cityName, geoCls);
            }

            ViewData[VD_KEY_CITY_HOT_HOTEL_DICT] = dictCityHotels;
            ViewData[VD_KEY_CITY_HOT_GEO_CLS_DICT] = dictCityGeoCls;
        }



        protected void VdHotCommentHotel(int rows) {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>(new PagerObject(1, rows));
            icr.AddOrder(Order.Desc("goodCommentCount"));
            IList<HotelModel> hotels = icr.List<HotelModel>();
            /*
            IList<string> monthIds = new List<string>();
            foreach (HotelModel hotel in hotels)
            {
                monthIds.Add(hotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
            icr.Add(Restrictions.In("id", monthIds.ToArray()));
             
            IList<HotelDetailModel> details = icr.List<HotelDetailModel>();
            */
            ViewData[VD_KEY_HOT_COMMENT_HOTEL_LIST] = hotels;
        }


        protected void VdHotCity(int rows) {
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoModel>(new PagerObject(1, rows));
            icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
            icr.AddOrder(Order.Desc("recLevel"));
            IList<GeoModel> geos = icr.List<GeoModel>();
            ViewData[VD_KEY_HOT_CITY] = geos;
        }


        protected void setPageDesc(string val) {
            ViewData["pageDesc"] = val;
        }

        protected void setPageKeyWords(string val) {
            ViewData["pageKeywords"] = val;
        }

        protected DateTime getDefaultCheckInDate() {
            return DateTime.Now.AddDays(1);
        }

        protected DateTime getDefaultCheckOutDate()
        {
            return DateTime.Now.AddDays(2);
        }
    }
}
