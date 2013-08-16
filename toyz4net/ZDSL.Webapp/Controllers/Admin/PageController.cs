using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyz4net.Core.Util;
using ZDSL.Biz;
using ZDSL.Model.Data;
using ZDSL.Model.Public;
using NHibernate;
using NHibernate.Criterion;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class PageController : BaseZdController
    {
        //
        // GET: /Page/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ConfigHotBrand() {
            return View();
        }

        public ActionResult ConfigFrontPage() {
            FrontPageModel frontPage = PublicBiz.GetInstant().getCurFrontPage();
            ViewData[typeof(FrontPageModel).Name] = frontPage;
            return View();
        }

        public ActionResult ConfigPageSeo()
        {
            ViewData[typeof(PageSeoModel).Name] = PublicBiz.getCurPageSeo();
            return View();
        }


        public ActionResult ListAdSider() {

            return View();
         
        }

        public ActionResult ListOuterLink() {
            return View();
        }

        public ActionResult DoSaveFrontPage() {

            FrontPageModel frontPage = PublicBiz.GetInstant().getCurFrontPage();
            frontPage = WebUtil.Eval<FrontPageModel>(frontPage, "", "");
            JsResultObject re = BaseZdBiz.Update(frontPage,"首页配置");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridHotBrand() {

            DatagridObject datagrid = null;
            ICriteria icr = BaseZdBiz.CreateCriteria<HotBrandModel>();
            IList<HotBrandModel> hotBrands = icr.List<HotBrandModel>();
            PageList<HotBrandModel> pagerList = new PageList<HotBrandModel>(hotBrands, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<HotBrandModel>(pagerList);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridAdSider()
        {

            DatagridObject datagrid = null;
            ICriteria icr = BaseZdBiz.CreateCriteria<AdSiderModel>();
            IList<AdSiderModel> hotBrands = icr.List<AdSiderModel>();
            PageList<AdSiderModel> pagerList = new PageList<AdSiderModel>(hotBrands, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<AdSiderModel>(pagerList);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridOuterLink()
        {

            DatagridObject datagrid = null;
            ICriteria icr = BaseZdBiz.CreateCriteria<OuterLinkModel>();
            icr.AddOrder(Order.Desc("recLevel"));
            IList<OuterLinkModel> hotBrands = icr.List<OuterLinkModel>();
            PageList<OuterLinkModel> pagerList = new PageList<OuterLinkModel>(hotBrands, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<OuterLinkModel>(pagerList);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveHotBrand(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<HotBrandModel>(arrayIds, "热门品牌");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult RemoveAdSider(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<AdSiderModel>(arrayIds, "广告");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveOuterLink(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<OuterLinkModel>(arrayIds, "外部链接");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveHotBrand(string ids)
        {
            HotBrandModel e = new HotBrandModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "热门品牌");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveAdSider()
        {
            AdSiderModel e = new AdSiderModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "广告");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveOuterLink(string id)
        {
            OuterLinkModel e = new OuterLinkModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "外部链接");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DoSetHotBrand(string brandId)
        {

            BrandModel e = BaseZdBiz.Load<BrandModel>(brandId);
            DataBiz dataBiz = DataBiz.GetInstant();
            JsResultObject result = dataBiz.setHotBrand(e, 0, "");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DoSavePageSeo()
        {
            PageSeoModel seo = new PageSeoModel();
            seo = WebUtil.Eval<PageSeoModel>(seo, "", "");
            JsResultObject re = BaseZdBiz.Update(seo, "Seo配置");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

    }
}
