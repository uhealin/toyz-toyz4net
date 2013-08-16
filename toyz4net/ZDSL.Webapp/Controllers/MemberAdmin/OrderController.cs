using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Data;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using Toyz4net.Core.Util;

namespace ZDSL.Webapp.Controllers.MemberAdmin
{
    public class OrderController : BaseZdController
    {
        //
        // GET: /Order/

        public ActionResult Index(string dateRegexp, DateTime? startDate, DateTime? endDate)
        {
            MemberModel member = this.getAuthMember();
            if (member == null)
            {
                this.SetResult(JsResultObject.CODE_ALERT,"进入订单管理，请用户先进行登陆",false);

                return RedirectToAction("Login", "Home");
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<OrderModel>();
            icr.Add(Restrictions.Eq("memberFk", member.id));


            if (WebUtil.IsPost())
            {
                
                ToyzRangeDateObject range = new ToyzRangeDateObject(startDate ?? DateTime.Now, endDate ?? DateTime.Now.AddDays(1));
                icr.Add(range.createCriterion("createDate"));
                ViewData["startDate"] = range.getFirstDay().ToShortDateString();
                ViewData["endDate"] = range.getLastDay().ToShortDateString();
            }
            else
            {
                if (string.IsNullOrEmpty(dateRegexp)|| dateRegexp == "_week")
                {
                    ToyzWeekObject week = new ToyzWeekObject(DateTime.Now);
                    icr.Add(week.createCriterion("checkInDate"));
                    ViewData["startDate"] = week.sun.ToShortDateString();
                    ViewData["endDate"] = week.sat.ToShortDateString();
                }
                else if (dateRegexp == "_month")
                {
                    ToyzMonthObject month = new ToyzMonthObject(DateTime.Now);
                    icr.Add(month.createCriterion("checkInDate"));
                    ViewData["startDate"] = month.getFirstDay().ToShortDateString();
                    ViewData["endDate"] = month.getLastDay().ToShortDateString();

                }
                else if (dateRegexp == "_season")
                {

                    ToyzSeasonObject season = new ToyzSeasonObject(DateTime.Now);
                }
            }
            icr.AddOrder(Order.Desc("checkInDate"));

            IList<OrderModel> orders = icr.List<OrderModel>();
            ViewData[typeof(OrderModel).Name] = orders;
            return View();
        }



        public ActionResult TableMemberOrder(string orderStatus) {

            MemberModel member = this.getAuthMember();

            ICriteria icr = BaseZdBiz.CreateCriteria<OrderModel>();
            if (string.IsNullOrEmpty(orderStatus))
            {
                icr.Add(Restrictions.Eq("memberFk", member.id));
            }
            else {
                string[] states = orderStatus.Split(',');
                icr.Add(Restrictions.And(Restrictions.Eq("memberFk", member.id), Restrictions.In("orderStatus",states)));
            }
            
            IList<OrderModel> orders = icr.List<OrderModel>();
            PageList<OrderModel> pageList = new PageList<OrderModel>(orders, this.getPager());
            ViewData[typeof(OrderModel).Name] = pageList;
            return View();
        }


        public ActionResult DoCancel(int orderId) {
            MemberModel member = this.getAuthMember();
            DataBiz dataBiz = DataBiz.GetInstant();
            OrderModel order=BaseZdBiz.Load<OrderModel>(orderId);
            JsResultObject re= dataBiz.cancelOrder(order,"","");
            this.SetResult(re,true);
            IList<OrderModel> orders = BaseZdBiz.List<OrderModel>(Restrictions.Eq("memberFk", member.id));
            ViewData[typeof(OrderModel).Name] = orders;
            return View("Index");
        }

        public ActionResult DoRenew(int orderId)
        {
            MemberModel member = this.getAuthMember();
            DataBiz dataBiz = DataBiz.GetInstant();
            OrderModel order = BaseZdBiz.Load<OrderModel>(orderId);
            JsResultObject re = dataBiz.renewOrder(order);
            this.SetResult(re,true);
            IList<OrderModel> orders = BaseZdBiz.List<OrderModel>(Restrictions.Eq("memberFk", member.id));
            ViewData[typeof(OrderModel).Name] = orders;
            return View("Index");
        }

        [HttpPost]
        public ActionResult DoSubmitMemberComment() {
            MemberModel member = this.getAuthMember();
            MemberCommentModel comment = new MemberCommentModel();
            comment = WebUtil.Eval<MemberCommentModel>(comment, "", "");
            comment.memberFk = member.id;
            DataBiz dataBiz = DataBiz.GetInstant();
            JsResultObject re = dataBiz.submitComment(comment);
            this.SetResult(re, true);
            IList<OrderModel> orders = BaseZdBiz.List<OrderModel>(Restrictions.Eq("memberFk", member.id));
            ViewData[typeof(OrderModel).Name] = orders;
            return View("Index");
        }

        public ActionResult DivMemberCommentEdit(int orderId) {

            OrderModel order = BaseZdBiz.Load<OrderModel>(orderId);
            HotelDetailModel hotel = BaseZdBiz.Load<HotelDetailModel>(order.hotelId);
            ViewData[typeof(OrderModel).Name] = order;
            ViewData[typeof(HotelDetailModel).Name] = hotel;
            return View();
        
        }
    }
}
