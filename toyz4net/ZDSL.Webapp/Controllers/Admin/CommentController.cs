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
    public class CommentController : BaseZdController
    {
        //
        // GET: /Comment/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListMember() {
            return View();
        }

        public ActionResult ListNews()
        {
            return View();
        }

        public ActionResult ListHotel()
        {
            return View();
        }
        public ActionResult ListExhi()
        {
            return View();
        }


        public ActionResult DatagridMember_act()
        {

            DatagridObject datagrid = createDatagrid<MemberCommentModel>(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridMember_del()
        {

            DatagridObject datagrid = createDatagrid<MemberCommentModel>(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridNews_act()
        {

            DatagridObject datagrid = createDatagrid<NewsCommentModel>(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridNews_del()
        {

            DatagridObject datagrid = createDatagrid<NewsCommentModel>(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DatagridExhi_act()
        {

            DatagridObject datagrid = createDatagrid<ExhiCommentModel>(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridExhi_del()
        {

            DatagridObject datagrid = createDatagrid<ExhiCommentModel>(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridHotel_act()
        {

            DatagridObject datagrid = createDatagrid<HotelCommentModel>(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridHotel_del()
        {

            DatagridObject datagrid = createDatagrid<HotelCommentModel>(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        private DatagridObject createDatagrid<T>(string  status)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<T> listHotel;
            icr = BaseZdBiz.CreateCriteria<T>();
            icr.Add(Restrictions.Eq("status", status));
           // new CommentModel().setOrderBy(ref icr);
            listHotel = icr.List<T>();
            PageList<T> pagerList = new PageList<T>(listHotel, this.getPager());
            datagrid = DatagridObject.ToDatagridObject <T>(pagerList);
            return datagrid;
        }



        [HttpPost]
        public ActionResult SaveOrder()
        {
            MemberCommentModel e = new MemberCommentModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "订单点评");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveOrder(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<MemberCommentModel>(arrayIds , "订单点评");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult SaveNews()
        {
            NewsCommentModel e = new NewsCommentModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "新闻评论");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveNews(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<NewsCommentModel>(arrayIds , "新闻评论");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult SaveExhi()
        {
            ExhiCommentModel e = new ExhiCommentModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "展会评论");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveExhi(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<ExhiCommentModel>(arrayIds , "展会评论");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveHotel()
        {
            HotelCommentModel e = new HotelCommentModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "酒店点评");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveHotel(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<HotelCommentModel>(arrayIds , "酒店点评");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }





    }
}
