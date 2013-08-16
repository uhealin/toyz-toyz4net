using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;
using ZDSL.Model.Public;

namespace ZDSL.Webapp.Controllers.Public
{
    public class ExhiController : BaseZdController
    {

        public static string GetPathIndex()
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Exhi.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Exhi/Index");
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }

        public static string GetPathExhiHotel(ExhiModel exhi)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Exhi.ExhiHotel"];
            filePath = string.Format(filePath, exhi.id);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Exhi/ExhiHotel?exhiId=" + exhi.id);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }


        //
        // GET: /Exhi/

        public ActionResult Index(string cityName,string keyword)
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<ExhiModel>(new PagerObject(1,20));
            GeoBiz geoBiz = GeoBiz.GetInstant();
            if (WebUtil.IsPost()) {
                GeoModel geo = geoBiz.GetGeoByCityName(cityName);
                icr.Add(Restrictions.Eq("geoFk", geo.id));
                icr.Add(Restrictions.Like("name", "%" + keyword + "%"));
            }
            icr.AddOrder(Order.Desc("startDate"));
            IList<ExhiModel> exhis = icr.List<ExhiModel>();
            
            ViewData[typeof(ExhiModel).Name] = exhis;
            this.VdHotBookingHotel(10);
            this.VdHotBrand(12);
            this.VdHotCity(15);

            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(seo.exhiIndexDesc);
            this.setPageKeyWords(seo.exhiIndexKeywords);
            return View();
        }


        public ActionResult ExhiHotel(string exhiId) {

            ExhiModel exhi = BaseZdBiz.Load<ExhiModel>(exhiId);
            ICriteria icr = BaseZdBiz.CreateCriteria<ExhiRefHotelModel>();
            icr.Add(Restrictions.Eq("exhiId", exhiId));
            IList<ExhiRefHotelModel> refHotels = icr.List<ExhiRefHotelModel>();
            IList<string> ids = new List<string>();
            if (refHotels.Count > 0)
            {
                 ids = ObjectUtil.GetProList(refHotels, "hotelId");
            }
            else {
                icr = BaseZdBiz.CreateCriteria<HotelModel>(new PagerObject(1, 10));
                icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
                IList<HotelModel> hotels = icr.List<HotelModel>();
                ids = ObjectUtil.GetProList(hotels, "hotelId");
            }
            
            exhi.hotelIdArray = StringUtil.UnionArray(ids.ToArray(), ',');
            icr = BaseZdBiz.CreateCriteria<ExhiModel>();
            icr.Add(Restrictions.Eq("geoFk", exhi.geoFk));
            icr.Add(Restrictions.Not(Restrictions.Eq("id", exhi.id)));
            icr.AddOrder(Order.Desc("startDate"));
            IList<ExhiModel> exhis = icr.List<ExhiModel>();
            ViewData["refExhis"] = exhis;
            ViewData[typeof(ExhiModel).Name] = exhi;
            this.VdHotCity(15);

            PageSeoModel seo = PublicBiz.getCurPageSeo();
            //{0}展会code,{1}:展会名,{2}:展会地址, {3}:展会时间 ,{4}:行业,{5}:内容
            this.setPageDesc(string.Format( seo.exhiHotelDesc,exhi.id, exhi.name, exhi.address, exhi.startDate.ToShortDateString(), exhi.busName,exhi.msg).Substring(0,228));
            this.setPageKeyWords(string.Format( seo.exhiHotelKeywords,exhi.id, exhi.name, exhi.address, exhi.startDate.ToShortDateString(), exhi.busName,exhi.msg));
            return View();
        }

    }
}
