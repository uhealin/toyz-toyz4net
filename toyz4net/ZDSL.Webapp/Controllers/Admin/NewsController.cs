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

namespace ZDSL.Webapp.Controllers.Admin
{
   
    public class NewsController : BaseZdController 
    {

        public ActionResult ListNewsRefHotel() {

            return View();
        }

        public ActionResult Datagrid_act_deploy()
        {
           
            
            DatagridObject datagrid = createNewsDatagrid(BaseModel.STATUS_ACTIVATE,NewsModel.NEWS_STATUS_DEPLOY);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_undeploy()
        {


            DatagridObject datagrid = createNewsDatagrid(BaseModel.STATUS_ACTIVATE, NewsModel.NEWS_STATUS_UNDEPLOY);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del_all()
        {


            DatagridObject datagrid = createNewsDatagrid(BaseModel.STATUS_DELETE, NewsModel.NEWS_STATUS_UNDEPLOY);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createNewsDatagrid(string status,string newsStatus)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<NewsModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<NewsModel>();
            if (string.IsNullOrEmpty(newsStatus))
            {
                icr.Add(Restrictions.Eq("status", status));
            }
            else {
                icr.Add(Restrictions.And(Restrictions.Eq("status", status), Restrictions.Eq("newsStatus", newsStatus)));

                
            }
                new NewsModel().setOrderBy(ref icr);
            listHotel = icr.List<NewsModel>();
            PageList<NewsModel> pagerList = new PageList<NewsModel>(listHotel, this.getPager());
            datagrid =  DatagridObject.ToDatagridObject <NewsModel>(pagerList);
            return datagrid;
        }


        [HttpPost]
        public ActionResult Save()
        {
            NewsModel news = new NewsModel();
            news.status = BaseModel.STATUS_ACTIVATE;
           
            news = ObjectUtil.Eval(news, Request.Params, "", "");
            if (string.IsNullOrEmpty(news.id))
            {
                news.id = news.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(news, "新闻");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<NewsModel>(arrayIds , "新闻");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridNewsRefHotel(string newsId) {

            ICriteria icr = BaseZdBiz.CreateCriteria<NewsRefHotelModel>();
            icr.Add(Restrictions.Eq("newsId", newsId));
            IList<NewsRefHotelModel> refHotels=icr.List<NewsRefHotelModel>();
            IList<string> ids = new List<string>();
            foreach (NewsRefHotelModel refHotel in refHotels) {
                ids.Add(refHotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelModel>();
            icr.Add(Restrictions.In("hotelId", ids.ToArray()));
            IList<HotelModel> hotels=icr.List<HotelModel>();
            DatagridObject datagrid = DatagridObject.ToDatagridObject<HotelModel>(hotels);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNewsRefHotel() {
            NewsRefHotelModel e = new NewsRefHotelModel();
            e = WebUtil.Eval<NewsRefHotelModel>(e, "", "");
            if (string.IsNullOrEmpty(e.id)) {
                e.setPk(e.createPk());
            }
            JsResultObject re = BaseZdBiz.SaveOrUpdate(e,"相关酒店");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveNewsRefHotel(string ids, string newsId)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject re = new JsResultObject();
            ICriteria icr = BaseZdBiz.CreateCriteria<NewsRefHotelModel>();
            icr.Add(Restrictions.Eq( "newsId", newsId));
            icr.Add(Restrictions.In("hotelId", arrayIds));
            IList<NewsRefHotelModel> refHotels = icr.List<NewsRefHotelModel>();
            foreach (NewsRefHotelModel refHotel in refHotels) {


                re.rowNum += BaseZdBiz.Delete(refHotel, "相关酒店").rowNum;

            }
            re.msg = string.Format("成功删除{0}条记录", re.rowNum);

            return JsonText(re, JsonRequestBehavior.AllowGet);
        }
    }
}
