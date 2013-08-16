using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Model.Admin;
using System.Collections;
using JSONSharp;
using JSONSharp.Collections;
using JSONSharp.Values;
using ZDSL.Model.Data;
using ZDSL.Model.Cache;

namespace ZDSL.Biz
{
    public class CacheBiz:BaseZdBiz
    {

        private CacheBiz(string pathBase) {

            this.pathBase = pathBase;
            this.pathDictCache = this.pathBase + "/" + BaseZdBiz.GetPathJsCache()+"DictModel.js";
            this.pathGeoCache = this.pathBase + "/" + BaseZdBiz.GetPathJsCache() + "GeoModel.js";
            this.pathHotelCache = this.pathBase + "/" + BaseZdBiz.GetPathJsCache() + "HotelModel.js";
            this.pathBrandCache = this.pathBase + "/" + BaseZdBiz.GetPathJsCache() + "BrandModel.js";
            this.pathRoleCache = this.pathBase + "/" + BaseZdBiz.GetPathJsCache() + "RoleModel.js";
        }

        private string pathBase="";
        public string pathDictCache { get; private set; }
        public string pathGeoCache { get; private set; }
        public string pathHotelCache { get; private set; }
        public string pathBrandCache { get; private set; }
        public string pathRoleCache { get; private set; }
   
        private static CacheBiz Instant;
        private static readonly object lockHelper = new object();

        public static CacheBiz GetInstant(string pathBase)
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new CacheBiz(pathBase);
                    }
                }

            }
            return Instant;
        }

        public bool reflashDictCache() {
            bool isSuccess = false;

            

            JSONObjectCollection jsonGraph = new JSONObjectCollection();

            JSONObjectCollection jarr = new JSONObjectCollection();

            ICriteria icr = CreateCriteria<DictTypeModel>();
            IList<DictTypeModel> dictTypes=icr.List<DictTypeModel>();
            icr=CreateCriteria<DictModel>();
            IList<DictModel> dicts=icr.List<DictModel>();
            foreach (DictTypeModel dictType in dictTypes) {
                IList<DictModel> tempDicts=new List<DictModel>();
                foreach (DictModel dict in dicts) {
                    if (dict.type != dictType.id) { continue; }
                    tempDicts.Add(dict);
                }
                jsonGraph.Add(new JSONStringValue(dictType.id),DatagridObject.ToDatagridObject<DictModel>(tempDicts).toJSONObjectCollection());
            }

            StringBuilder sbr = new StringBuilder("");
            sbr
            .Append("Class.forName(\"Toyz4js.cache\"); \n\n")
            .Append("Toyz4js[\"cache\"][\"DictModel\"]=").Append(jsonGraph.ToString()).Append(";  \n\n \n\n")
            .Append("Toyz4js[\"cache\"][\"DictTypeModel\"]=").Append(DatagridObject.ToDatagridObject<DictTypeModel>(dictTypes).toJSONObjectCollection()).Append(";  \n\n \n\n")
            ;
            try
            {
                FileUtil.WriteFile(this.pathDictCache, sbr.ToString());
            }
            catch(Exception ex) {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool reflashBrandCache() {
            bool isSucess = false;
            JSONObjectCollection jsonCraph = new JSONObjectCollection();

            StringBuilder sbr = new StringBuilder("");
            sbr
            .Append("Class.forName(\"Toyz4js.cache\"); \n\n");



            JSONObjectCollection jsonBrand = new JSONObjectCollection();
  
            IList<BrandModel> brands = CreateCriteria<BrandModel>().List<BrandModel>();

            sbr
            .Append("Toyz4js[\"cache\"][\"BrandModel\"][\"brands\"]=")
            .Append(DatagridObject.ToDatagridObject<BrandModel>(brands).toJSONObjectCollection().ToString())
            .Append("\n\n")
            ;

            isSucess=  FileUtil.WriteFile(pathBrandCache, sbr.ToString());

            return isSucess;
      
        }

        public bool reflashHotelCache()
        {
            bool isSuccess = false;

            ICriteria icr = CreateCriteria<HotelModel>();
            icr.Add(Restrictions.Eq("isreserve",HotelModel.RESERVE_OK));
            IList hotels = icr.List();
            foreach (HotelModel hotel in hotels) {
                hotel.status = null;
                hotel.isreserve = null;
            }
            DatagridObject datagrid = DatagridObject.ToDatagridObject(hotels);
            JSONObjectCollection json = datagrid.toJSONObjectCollection();
            StringBuilder sbr = new StringBuilder("");
            sbr
            .Append("Class.forName(\"Toyz4js.cache\"); \n\n")
            .Append("Toyz4js[\"cache\"][\"HotelModel\"]=").Append(json.ToString()).Append(";  \n\n \n\n");
            ;
            try
            {
                FileUtil.WriteFile(this.pathHotelCache, sbr.ToString());
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool reflashGeoCache() {

            bool isSuccess = false;

            ICriteria icr = CreateCriteria<GeoModel>();
            //icr.Add(Restrictions.Eq("isreserve", HotelModel.RESERVE_OK));
            IList geos = icr.List();
            foreach (GeoModel geo in geos) {
                geo.url = null;
                geo.status = null;
            }
            DatagridObject datagrid = DatagridObject.ToDatagridObject(geos);
            JSONObjectCollection json = datagrid.toJSONObjectCollection();
            StringBuilder sbr = new StringBuilder("");
            sbr
            .Append("Class.forName(\"Toyz4js.cache\"); \n\n")
            .Append("Toyz4js[\"cache\"][\"GeoModel\"]=").Append(json.ToString()).Append(";  \n\n \n\n");
            ;
            try
            {
                FileUtil.WriteFile(this.pathGeoCache, sbr.ToString());
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }


        public bool reflashRoleCache() {
            bool isSuccess = false;

            ICriteria icr = CreateCriteria<RoleModel>();
            //icr.Add(Restrictions.Eq("isreserve", HotelModel.RESERVE_OK));
            IList list = icr.List();
            DatagridObject datagrid = DatagridObject.ToDatagridObject(list);
            JSONObjectCollection json = datagrid.toJSONObjectCollection();
            StringBuilder sbr = new StringBuilder("");
            sbr
            .Append("Class.forName(\"Toyz4js.cache\"); \n\n")
            .Append("Toyz4js[\"cache\"][\"RoleModel\"]=").Append(json.ToString()).Append(";  \n\n \n\n");
            ;
            try
            {
                FileUtil.WriteFile(this.pathRoleCache, sbr.ToString());
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return isSuccess;
        }


        public static OrderCountModel GetCurrOrderCount() {
            ICriteria icr = CreateCriteria<OrderModel>();
            OrderCountModel orderCount = new OrderCountModel();
            orderCount.id = "system";
            orderCount.allCount=Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus",OrderModel.ORDER_STATUS_CANCEL));
            orderCount.cancelCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_COMMENTED));
            orderCount.commentedCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_DEAL));
            orderCount.dealCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_ELONG_SUCCESS));
            orderCount.elongSuccessCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_FAIL));
            orderCount.failCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_NEW));
            orderCount.newCount = Count(icr, "id");

            icr = CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("orderStatus", OrderModel.ORDER_STATUS_ZD_SUCCESS));
            orderCount.zdSuccessCount = Count(icr, "id");

            return orderCount;
        }


        public static RewardCountModel GetCurrRewardCount()
        {
            ICriteria icr = CreateCriteria<MemberRewardModel>();
            RewardCountModel count = new RewardCountModel();
            count.id = "system";
            count.allCount = Count(icr, "id");

            icr = CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("rebateStatus", MemberRewardModel.REBATE_STATUS_CANCEL));
            count.cancelCount = Count(icr, "id");

            icr = CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("rebateStatus", MemberRewardModel.REBATE_STATUS_REBATED));
            count.rebatedCount = Count(icr, "id");

            icr = CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("rebateStatus", MemberRewardModel.REBATE_STATUS_REQUIRE));
            count.requireCount = Count(icr, "id");

            icr = CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("rebateStatus", MemberRewardModel.REBATE_STATUS_UNREBATED));
            count.unrebateCount = Count(icr, "id");


            return count;
        }

    }
}
