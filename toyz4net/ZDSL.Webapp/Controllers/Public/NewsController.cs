using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using ZDSL.Model.Public;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.Public
{
    public class NewsController : BaseZdController
    {
        //
        // GET: /News/


        public static string GetPathIndex()
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.News.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/News/Index");
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }


        public static string GetPathNewsHotel(NewsModel News)
        {
            string pattern = "{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.News.NewsHotel"];
            filePath = string.Format(filePath, News.id);
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/News/NewsHotel?newsId=" + News.id);
            }
            else
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }


        public ActionResult Index(string keyword)
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<NewsModel>();
            icr.AddOrder(Order.Desc("deployDate"));
            if (WebUtil.IsPost())
            {
   
                icr.Add(Restrictions.Like("title", "%" + keyword + "%"));
            }
            IList<NewsModel> newss = icr.List<NewsModel>();
            ViewData[typeof(NewsModel).Name] = newss;
            this.VdHotBookingHotel(10);
            this.VdHotBrand(12);
            this.VdHotCity(15);
            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(seo.newsIndexDesc);
            this.setPageKeyWords(seo.newsIndexKeywords);
            return View();
        }


        public ActionResult NewsHotel(string newsId)
        {

            NewsModel news = BaseZdBiz.Load<NewsModel>(newsId);
            
            ICriteria icr=BaseZdBiz.CreateCriteria<NewsRefHotelModel>();
            icr.Add(Restrictions.Eq("newsId", newsId));
            IList<NewsRefHotelModel> refHotels = icr.List<NewsRefHotelModel>();
            IList<string> ids = new List<string>();
            if (refHotels.Count > 0)
            {
                ids = ObjectUtil.GetProList(refHotels, "hotelId");
            }
            else
            {
                icr = BaseZdBiz.CreateCriteria<HotelModel>(new PagerObject(1, 10));
                icr.Add(Restrictions.Eq("recInd", BaseModel.IND_Y));
                IList<HotelModel> hotels = icr.List<HotelModel>();
                ids = ObjectUtil.GetProList(hotels, "hotelId");
            }
            news.hotelIdArray = StringUtil.UnionArray(ids.ToArray(),',');

            icr = BaseZdBiz.CreateCriteria<NewsModel>(new PagerObject(1, 5));
            icr.Add(Restrictions.Not(Restrictions.Eq("id",newsId)));
            icr.AddOrder(Order.Desc("deployDate"));
            IList<NewsModel> refNews = icr.List<NewsModel>();

            ViewData["refNews"] = refNews;
            ViewData[typeof(NewsModel).Name] = news;
            this.VdHotCity(15);

            //{0}新闻code,{1}:新闻主题,{2}:新闻日期 ,{3}:新闻内容
            PageSeoModel seo = PublicBiz.getCurPageSeo();
            this.setPageDesc(string.Format(seo.newsHotelDesc,news.id,news.title,news.deployDate.ToShortDateString(),news.context).Substring(0,228));
            this.setPageKeyWords(string.Format(seo.newsHotelKeywords, news.id, news.title, news.deployDate.ToShortDateString(), news.context));
            return View();
        }

    }
}
