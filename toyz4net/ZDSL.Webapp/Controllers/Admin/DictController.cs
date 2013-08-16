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
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class DictController : BaseZdController 
    {
        //
        // GET: /Dict/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Datagrid_act(string qtHotelName, string qvHotelName)
        {
            
            
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_ACTIVATE, qtHotelName, qvHotelName);
            
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_dct(string qtHotelName, string qvHotelName)
        {
           
           
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_DEACTIVATE, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del(string qtHotelName, string qvHotelName)
        {
           
           
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_DELETE, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createHotelDatagrid(string  status, string qHotelNameType, string qHotelNameVal)
        {

            DatagridObject datagrid = null;
            ICriteria icr;
            IList<DictModel> listHotel;


            if (!string.IsNullOrEmpty(qHotelNameVal) && !string.IsNullOrEmpty(qHotelNameType))
            {
                //带条件查询使用集合切割策略进行分页
                icr = BaseZdBiz.CreateCriteria<DictModel>();
                if (qHotelNameType == "text")
                {
                    icr.Add(Restrictions.Like("text", "%" + qHotelNameVal + "%"));

                }
                else if (qHotelNameType == "value")
                {
                    icr.Add((Restrictions.Like("value", "%" + qHotelNameVal + "%")));
                }
                else
                {
                    return DatagridObject.NewIntanst();
                }

                icr.Add(Restrictions.Eq("status", status));
                icr.AddOrder(Order.Asc("type"));
               
                listHotel = icr.List<DictModel>();
                PageList<DictModel> pagerList = new PageList<DictModel>(listHotel, this.getPager());
                datagrid =  DatagridObject.ToDatagridObject<DictModel>(pagerList);
            }
            else
            {
                //不带条件查询使用分页缓存进行分页
                icr = BaseZdBiz.CreateCriteria<DictModel>();
                icr.Add(Restrictions.Eq("status", status));
                icr.AddOrder(Order.Asc("type"));
              
                listHotel = icr.List<DictModel>();
                PageList<DictModel> pagerList = new PageList<DictModel>(listHotel, this.getPager());
                datagrid =  DatagridObject.ToDatagridObject <DictModel>(pagerList);
               // AdminBiz adminBiz = AdminBiz.GetInstant();
              //  TableCountModel tableCount = adminBiz.getTableCount();
              //  datagrid = new DatagridObject<DictModel>(listHotel, tableCount.hotelOk);
            }
            return datagrid;
        }

        [HttpPost]
        public ActionResult Save()
        {
            DictModel dict = new DictModel();
            dict = ObjectUtil.Eval(dict, Request.Params, "", "");
            if (string.IsNullOrEmpty(dict.id)) {
                dict.id = dict.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(dict, "字典");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<DictModel>(arrayIds , "字典");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListDictType() {

            return View();
        }

        public ActionResult DatagridDictType()
        {

            DatagridObject datagrid = createDatagridDictType();
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createDatagridDictType()

        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<DictTypeModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<DictTypeModel>();
            //icr.Add(Restrictions.Eq("status", status));
            new DictTypeModel().setOrderBy(ref icr);
            listHotel = icr.List<DictTypeModel>();
            PageList<DictTypeModel> pagerList = new PageList<DictTypeModel>(listHotel, this.getPager());
            datagrid =  DatagridObject.ToDatagridObject <DictTypeModel>(pagerList);
            return datagrid;
        }

        [HttpPost]
        public ActionResult SaveDictType()
        {
            DictTypeModel dictType = new DictTypeModel();
            dictType = ObjectUtil.Eval(dictType, Request.Params, "", "");
            JsResultObject result = BaseZdBiz.SaveOrUpdate(dictType, "字典类型");
            this.reflashDictCache();
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveDictType(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<DictTypeModel>(arrayIds , "字典类型");
            this.reflashDictCache();
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }
    }
}
