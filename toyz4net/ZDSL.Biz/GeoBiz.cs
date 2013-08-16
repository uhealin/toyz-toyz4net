using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZDSL.Model.Data;
using NHibernate;
using NHibernate.Criterion;

namespace ZDSL.Biz
{
    public  class GeoBiz: BaseZdBiz
    {


        private GeoBiz() { }

        private static GeoBiz Instant;
        private static readonly object lockHelper = new object();

        private static Dictionary<string, GeoModel> CACHE_GEOS_BY_CITY_NAME;
        private static Dictionary<string, GeoModel> CACHE_GEOS_BY_CITY_ID;
        private static Dictionary<string, IList<GeoCommercialLocationModel>> CACHE_GEO_CLS_BY_GEO_FK;
        private static Dictionary<string, IList<GeoDistrictsModel>> CACHE_GEO_DS_BY_GEO_FK;
        private static Dictionary<string, IList<GeoLandmarkLocationModel>> CACHE_GEO_LLS_BY_GEO_FK;


        public static GeoBiz GetInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new GeoBiz();
                    }
                }

            }
            return Instant;
        }


        public GeoModel GetGeoByCityName(string cityName) {
            if (CACHE_GEOS_BY_CITY_NAME == null) {
                CACHE_GEOS_BY_CITY_NAME = new Dictionary<string, GeoModel>();
            }
            GeoModel geo = null;
            if (CACHE_GEOS_BY_CITY_NAME.ContainsKey(cityName)) {
                return CACHE_GEOS_BY_CITY_NAME[cityName];
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoModel>();
            icr.Add(Restrictions.Like("cityName","%"+cityName+"%"));
            IList<GeoModel> geos = icr.List<GeoModel>();
            if (geos.Count > 0)
            {
               geo= geos[0];
            }
            CACHE_GEOS_BY_CITY_NAME.Add(cityName, geo);
            return geo;
        }


        public GeoModel GetGeoByCityId(string cityId)
        {
            if (CACHE_GEOS_BY_CITY_ID == null)
            {
                CACHE_GEOS_BY_CITY_ID = new Dictionary<string, GeoModel>();
            }
            GeoModel geo = null;
            if (CACHE_GEOS_BY_CITY_ID.ContainsKey(cityId))
            {
                return CACHE_GEOS_BY_CITY_ID[cityId];
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoModel>();
            icr.Add(Restrictions.Eq("cityCode", cityId));
            IList<GeoModel> geos = icr.List<GeoModel>();
            if (geos.Count > 0)
            {
                geo = geos[0];
            }
            CACHE_GEOS_BY_CITY_ID.Add(cityId, geo);
            return geo;
        }

        public IList<GeoCommercialLocationModel> GetGeoCls(string geoFk)
        {
            if (CACHE_GEO_CLS_BY_GEO_FK == null)
            {
                CACHE_GEO_CLS_BY_GEO_FK = new Dictionary<string, IList<GeoCommercialLocationModel>>();
            }
            IList<GeoCommercialLocationModel> localtions = null;
            if (CACHE_GEO_CLS_BY_GEO_FK.ContainsKey(geoFk))
            {
                return CACHE_GEO_CLS_BY_GEO_FK[geoFk];
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoCommercialLocationModel>();
            icr.Add(Restrictions.Eq("geoFk", geoFk));
            localtions = icr.List<GeoCommercialLocationModel>();

            CACHE_GEO_CLS_BY_GEO_FK.Add(geoFk, localtions);
            return localtions;
        }

        public IList<GeoDistrictsModel> GetGeoDs(string geoFk)
        {
            if (CACHE_GEO_DS_BY_GEO_FK == null)
            {
                CACHE_GEO_DS_BY_GEO_FK = new Dictionary<string, IList<GeoDistrictsModel>>();
            }
            IList<GeoDistrictsModel> localtions = null;
            if (CACHE_GEO_DS_BY_GEO_FK.ContainsKey(geoFk))
            {
                return CACHE_GEO_DS_BY_GEO_FK[geoFk];
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoDistrictsModel>();
            icr.Add(Restrictions.Eq("geoFk", geoFk));
            localtions = icr.List<GeoDistrictsModel>();

            CACHE_GEO_DS_BY_GEO_FK.Add(geoFk, localtions);
            return localtions;
        }

        public IList<GeoLandmarkLocationModel> GetGeoLls(string geoFk)
        {
            if (CACHE_GEO_LLS_BY_GEO_FK == null)
            {
                CACHE_GEO_LLS_BY_GEO_FK = new Dictionary<string, IList<GeoLandmarkLocationModel>>();
            }
            IList<GeoLandmarkLocationModel> localtions = null;
            if (CACHE_GEO_LLS_BY_GEO_FK.ContainsKey(geoFk))
            {
                return CACHE_GEO_LLS_BY_GEO_FK[geoFk];
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoLandmarkLocationModel>();
            icr.Add(Restrictions.Eq("geoFk", geoFk));
            localtions = icr.List<GeoLandmarkLocationModel>();

            CACHE_GEO_LLS_BY_GEO_FK.Add(geoFk, localtions);
            return localtions;
        }



        public Dictionary<string, IList<GeoModel>> GeoProvinceCitys() {
            Dictionary<string, IList<GeoModel>> dict = new Dictionary<string, IList<GeoModel>>();
            ICriteria icr = BaseZdBiz.CreateCriteria<GeoModel>();
            IList<GeoModel> allCitys=icr.List<GeoModel>();
            foreach (GeoModel geo in allCitys) {
                string provinceId = geo.provinceId;
                if (!dict.ContainsKey(provinceId)) { 
                   dict.Add(provinceId,new List<GeoModel>());
                }
                dict[provinceId].Add(geo);
            }
            return dict;
        }


    }
}
