using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;
using ZDSL.Model.Data;
using NHibernate;
using ZDSL.Biz;
using NHibernate.Criterion;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class BrandController : BaseZdController 
    {
        //
        // GET: /Brand/

        public ActionResult Datagrid_act(string qtName, string qvName)
        {


            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, qtName, qvName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_undeploy(string qtName, string qvName)
        {


            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, qtName, qvName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del_all(string qtName, string qvName)
        {


            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_DELETE, qtName, qvName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createDatagrid(string status, string qNameType, string qNameVal)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<BrandModel> listHotel;
            
            icr = BaseZdBiz.CreateCriteria<BrandModel>();
            bool isQuery = !string.IsNullOrEmpty(qNameType) && !string.IsNullOrEmpty(qNameVal);
            icr.Add(Restrictions.Eq("status", status));

            if (isQuery) {
                icr.Add(Restrictions.Or(Restrictions.Like("brandNameLong", "%" + qNameVal + "%"), Restrictions.Like("brandName", "%" + qNameVal + "%")));
            }
     
            new BrandModel().setOrderBy(ref icr);
            listHotel = icr.List<BrandModel>();
            PageList<BrandModel> pagerList = new PageList<BrandModel>(listHotel, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<BrandModel>(pagerList);
            return datagrid;
        }


        [HttpPost]
        public ActionResult Save()
        {
            BrandModel brand = new BrandModel();
            brand.status = BaseModel.STATUS_ACTIVATE;

            brand = ObjectUtil.Eval(brand, Request.Params, "", "");
            if (string.IsNullOrEmpty(brand.brandID))
            {
                brand.brandID = brand.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(brand, "连锁品牌");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<BrandModel>(arrayIds, "连锁品牌");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }




    }
}
