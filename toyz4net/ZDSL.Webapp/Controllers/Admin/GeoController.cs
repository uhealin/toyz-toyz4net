using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Admin;
using Toyz4net.Core.Util;
using ZDSL.Model.Cache;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using Toyz4net.Core.Model;
using System.Collections;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class GeoController : BaseZdController
    {
        //
        // GET: /Geo/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListLandmarkLocations() {
            return View();
        }

        public ActionResult ListCommercialLocations()
        {
            return View();
        }

        public ActionResult ListDistricts()
        {
            return View();
        }

        public ActionResult Datagrid(string qt,string qv) {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<GeoModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<GeoModel>();
           // icr.Add(Restrictions.Eq("status", status));
            if (!string.IsNullOrEmpty(qv) && !string.IsNullOrEmpty(qt))
            {
                icr.Add(Restrictions.Like(qt, "%"+qv+"%"));
            }
            icr.AddOrder(Order.Desc("recLevel"));
            listHotel = icr.List<GeoModel>();
            PageList<GeoModel> pagerList = new PageList<GeoModel>(listHotel, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<GeoModel>(pagerList);
            return JsonText(datagrid,JsonRequestBehavior.AllowGet);
        
        }

        public ActionResult Datagrid_act()
        {
   
           
            DatagridObject datagrid = createGeoDatagrid(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_dct()
        {
          
           
            DatagridObject datagrid = createGeoDatagrid(BaseModel.STATUS_DEACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del()
        {
            GeoModel e = new GeoModel();
            
            DatagridObject datagrid = createGeoDatagrid(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridLandmarkLocations(string qvGeoFk)
        {
            DatagridObject datagrid = this.createLocationDatagrid<GeoLandmarkLocationModel>(qvGeoFk);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridDistricts(string qvGeoFk)
        {
            DatagridObject datagrid = this.createLocationDatagrid<GeoDistrictsModel>(qvGeoFk);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridCommercialLocation(string qvGeoFk)
        {
            DatagridObject datagrid = this.createLocationDatagrid<GeoCommercialLocationModel>(qvGeoFk);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createLocationDatagrid<T>(string geoFk) where T:GeoLocationModel {
            bool isQuery = !string.IsNullOrEmpty(geoFk);

            ICriteria icr = null;
            DatagridObject datagrid = null;
            if (isQuery)
            {
                icr = BaseZdBiz.CreateCriteria<T>();
            }
            else {
                icr = BaseZdBiz.CreateCriteria<T>(this.getPager());
            }

            if (isQuery)
            {
                icr.Add(Restrictions.Eq("geoFk", geoFk));
                IList<T> list = icr.List<T>();
                PageList<T> pagerList = new PageList<T>(list, this.getPager());
                datagrid = DatagridObject.ToDatagridObject<T>(pagerList);
            }
            else {
                IList<T> list = icr.List<T>();
                datagrid = DatagridObject.ToDatagridObject<T>(list,3000);
            }
            
            
            
            
            return datagrid;
        }



        private DatagridObject createGeoDatagrid(string  status)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<GeoModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<GeoModel>();
            icr.Add(Restrictions.Eq("status", status));
            icr.AddOrder(Order.Desc("recLevel"));
            listHotel = icr.List<GeoModel>();
            PageList<GeoModel> pagerList = new PageList<GeoModel>(listHotel, this.getPager());
            datagrid =  DatagridObject.ToDatagridObject <GeoModel>(pagerList);
            return datagrid;
        }



        [HttpPost]
        public ActionResult Save()
        {
            GeoModel geo = new GeoModel();
            geo = ObjectUtil.Eval(geo, Request.Params, "", "");
            if (string.IsNullOrEmpty(geo.id))
            {
                geo.id = geo.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(geo, "地理信息");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<GeoModel>(arrayIds , "地理信息");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }
    }
}
