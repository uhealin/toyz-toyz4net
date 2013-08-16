using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Biz;
using ZDSL.Model.Data;
using Toyz4net.Core.Util;
using ZDSL.Model.Public;
using NHibernate.Criterion;

namespace ZDSL.Webapp.Controllers.Public
{
    public class GeoController : BaseZdController
    {
        //
        // GET: /Geo/

        public ActionResult Index(string cityName, string geoClId, string geoDId)
        {

            GeoBiz geoBiz = GeoBiz.GetInstant();
            cityName = ObjectUtil.Parse(cityName, "广州");
            GeoModel geo = geoBiz.GetGeoByCityName(cityName);
            if (geo == null) {
                geo = geoBiz.GetGeoByCityName("广州");
            }

            GeoLocationModel geoLocation = null;
            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(string.Format(seo.cityDesc, geo.cityCode, geo.cityName));
            this.setPageKeyWords(string.Format(seo.cityKeywords, geo.cityCode, geo.cityName));
            if (!string.IsNullOrEmpty(geoClId))
            {
                geoLocation = BaseZdBiz.Load<GeoCommercialLocationModel>(Restrictions.Eq("geoFk", geo.id), Restrictions.Eq("locationId", geoClId));
                this.setPageDesc(string.Format(seo.cityClDesc, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
                this.setPageKeyWords(string.Format(seo.cityClKeywords, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
            }
            else if (!string.IsNullOrEmpty(geoDId))
            {
                geoLocation = BaseZdBiz.Load<GeoDistrictsModel>(Restrictions.Eq("geoFk", geo.id), Restrictions.Eq("locationId", geoDId));
                this.setPageDesc(string.Format(seo.cityDDesc, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
                this.setPageKeyWords(string.Format(seo.cityDKeywords, geo.cityCode, geo.cityName, geoLocation.locationId, geoLocation.name));
            }

            ViewData[typeof(GeoLocationModel).Name] = geoLocation;
            ViewData[typeof(GeoModel).Name] = geo;
            return View();
        }


        public ActionResult DivGeoLoctaionTip(string cityName, string cityId) {
            GeoBiz geoBiz = GeoBiz.GetInstant();
            GeoModel geo = null;
            if (!string.IsNullOrEmpty(cityName)) {
                geo = geoBiz.GetGeoByCityName(cityName);
            }else if(!string.IsNullOrEmpty(cityId)){
                geo = geoBiz.GetGeoByCityId(cityId);
            }else{
                geo = geoBiz.GetGeoByCityName("广州");
            }
            VdGeoLocation(geo);
            return View();
        }
    }
}
