using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Data;
using ZDSL.Biz;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Util;

namespace ZDSL.Webapp.Controllers.MemberAdmin
{
    public class RewardController : BaseZdController
    {
        //
        // GET: /Reward/

        public ActionResult Index(string dateRegexp)
        {
            MemberModel member = this.getAuthMember();
            if (member == null)
            {
                this.SetResult(JsResultObject.CODE_ALERT, "进入点评奖励，请用户先进行登陆",false);
                return RedirectToAction("Login", "Home");
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("memberFk", member.id));

            if (dateRegexp == "_week")
            {
                ToyzWeekObject week = new ToyzWeekObject(DateTime.Now);
                icr.Add(week.createCriterion("createDate"));
            }
            else if (dateRegexp == "_month")
            {
                ToyzMonthObject month = new ToyzMonthObject(DateTime.Now);
                icr.Add(month.createCriterion("createDate"));

            }
            else if (dateRegexp == "_season")
            {

                ToyzSeasonObject season = new ToyzSeasonObject(DateTime.Now);
            }

            icr.AddOrder(Order.Desc("createDate"));

            IList<MemberRewardModel> rewards = icr.List<MemberRewardModel>();
            ViewData[typeof(MemberRewardModel).Name] = rewards;
            return View();
        }



        public ActionResult TableMemberReward(string statue)
        {

            MemberModel member = this.getAuthMember();

            ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardModel>();
            if (string.IsNullOrEmpty(statue))
            {
                icr.Add(Restrictions.Eq("memberFk", member.id));
            }
            else
            {
                string[] states = statue.Split(',');
                icr.Add(Restrictions.And(Restrictions.Eq("memberFk", member.id), Restrictions.In("orderStatus", states)));
            }

            IList<MemberRewardModel> orders = icr.List<MemberRewardModel>();
            PageList<MemberRewardModel> pageList = new PageList<MemberRewardModel>(orders, this.getPager());
            ViewData[typeof(MemberRewardModel).Name] = pageList;
            return View();
        }

    }
}
