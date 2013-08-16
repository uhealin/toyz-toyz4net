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
using ZDSL.Model.Public;

namespace ZDSL.Webapp.Controllers.Public
{
    public class BrandController : BaseZdController
    {



        public static string GetPathIndex()
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Brand.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Brand/Index");
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }

        public static string GetPathBrandHotel(string brandId)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Brand.BrandHotel"];
            filePath = string.Format(filePath,brandId);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Brand/BrandHotel?brandId="+brandId);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }





        //
        // GET: /Brand/

        public ActionResult Index()
        {

            ICriteria icr = BaseZdBiz.CreateCriteria<BrandModel>();
            icr.Add(Restrictions.IsNotNull("picURL"));
            //IList<BrandModel> brands = icr.List<BrandModel>();
           // IList<BrandModel> tempBrands = new List<BrandModel>();
            //foreach (BrandModel brand in brands) {
            //    if (string.IsNullOrEmpty(brand.picURL)) {
             //       continue;
             //   }
             //   tempBrands.Add(brand);
            //}
           // ViewData[VD_KEY_HOT_BRAND_LIST] = tempBrands;

            this.VdHotBookingHotel(10);
            this.VdHotBrand(500);
            this.VdHotCity(15);
            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(seo.brandIndexDesc);
            this.setPageKeyWords(seo.brandIndexKeywords);
            

            return View();
        }



        public ActionResult BrandHotel(string brandId) {

            ICriteria icr = BaseZdBiz.CreateCriteria<HotelDetailModel>();
            icr.Add(Restrictions.Eq("brandId",brandId));
            IList<HotelDetailModel> hotels=icr.List<HotelDetailModel>();
            ViewData[typeof(HotelDetailModel).Name] = hotels;

            BrandModel brand = BaseZdBiz.Load<BrandModel>(brandId);
            ViewData[typeof(BrandModel).Name] = brand;
            this.VdHotBookingHotel(10);
            this.VdHotCity(15);
            PageSeoModel seo = PublicBiz.getCurPageSeo();

            //{0}品牌code,{1}:品牌长名,{2}:品牌短名, {3}:拼音
            this.setPageDesc(string.Format(seo.brandHotelDesc,brand.brandID,brand.brandNameLong,brand.brandName,brand.brandPinYin));
            this.setPageKeyWords(string.Format(seo.brandHotelKeywords, brand.brandID, brand.brandNameLong, brand.brandName, brand.brandPinYin));
            return View();
        
        }

    }
}
