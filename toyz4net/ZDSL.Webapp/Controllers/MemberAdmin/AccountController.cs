using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Data;
using NHibernate;
using ZDSL.Biz;
using Toyz4net.Core.Util;
using NHibernate.Criterion;
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.MemberAdmin
{
    public class AccountController : BaseZdController
    {

        public ActionResult Index(string dateRegexp,DateTime? startDate,DateTime? endDate)
        {
            MemberModel member = this.getAuthMember();
            if (member == null)
            {
                this.SetResult(JsResultObject.CODE_ALERT, "进入点评奖励，请用户先进行登陆", false);
                return RedirectToAction("Login", "Home");
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberAccountModel>();
            icr.Add(Restrictions.Eq("memberFk", member.id));

            if (WebUtil.IsPost())
            {
                ToyzRangeDateObject range = new ToyzRangeDateObject(startDate ?? DateTime.Now ,endDate ?? DateTime.Now.AddDays(1));
                icr.Add(range.createCriterion("createDate"));
                ViewData["startDate"] = range.getFirstDay().ToShortDateString();
                ViewData["endDate"] = range.getLastDay().ToShortDateString();
            }
            else {

                if (string.IsNullOrEmpty(dateRegexp)|| dateRegexp == "_week")
                {
                    ToyzWeekObject week = new ToyzWeekObject(DateTime.Now);
                    icr.Add(week.createCriterion("createDate"));
                    ViewData["startDate"] = week.sun.ToShortDateString();
                    ViewData["endDate"] = week.sat.ToShortDateString();
                }
                else if (dateRegexp == "_month")
                {
                    ToyzMonthObject month = new ToyzMonthObject(DateTime.Now);
                    icr.Add(month.createCriterion("createDate"));
                    ViewData["startDate"] = month.getFirstDay().ToShortDateString();
                    ViewData["endDate"] = month.getLastDay().ToShortDateString();
                }
                else if (dateRegexp == "_season")
                {

                    ToyzSeasonObject season = new ToyzSeasonObject(DateTime.Now);
                }

            }

            icr.AddOrder(Order.Desc("createDate"));

            IList<MemberAccountModel> accounts = icr.List<MemberAccountModel>();
            ViewData[typeof(MemberAccountModel).Name] = accounts;
            return View();
        }

        //
        // GET: /Account/

        public ActionResult Edit()
        {
            MemberModel member = this.getAuthMember(); 
            if (member == null) {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }





        [HttpPost]
        public ActionResult DoEdit(string id)
        {
            MemberModel member = BaseZdBiz.CreateSession().Load<MemberModel>(id);
            member = WebUtil.Eval(member,"","");
           JsResultObject re=  BaseZdBiz.Update(member,"");
            member.pwd = null;
            WebUtil.SetSessionAttr(typeof(MemberModel).Name, member);
            if (re.code == JsResultObject.CODE_SUCCESS) {
                this.SetResult(JsResultObject.CODE_ALERT, "会员信息保存成功", true);
            }
            return View("Edit");
        }

        [HttpPost]
        public ActionResult DoResetPwd(string pwd, string newPwd)
        {
            MemberModel member = this.getAuthMember();
            member = BaseZdBiz.CreateSession().Load<MemberModel>(member.id);
            JsResultObject re = BaseZdBiz.Save(member);
            if (pwd != member.pwd)
            {
                this.SetResult(JsResultObject.CODE_ERROR, "密码与会员密码不一致", true);
            }
            else
            {
                member.pwd = newPwd;
                re = BaseZdBiz.Update(member,"");
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    this.SetResult(JsResultObject.CODE_ALERT, "会员密码修改成功", true);
                }
            }
            return View("Edit");
        }

        [HttpPost]
        public ActionResult DoRequestRebate( string remark,string moblie) {

            MemberModel member = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
            DataBiz dataBiz=DataBiz.GetInstant();
            /*
            MemberRewardLogModel rewardLog = new MemberRewardLogModel();
            rewardLog.memberFk = member.id;
            rewardLog.remark = remark;
            rewardLog.moblie = moblie; 
            rewardLog.exchangeType = exchangeType;
            rewardLog.exchangeStatus = MemberRewardLogModel.EXCHANGE_STATUS_UNEXCHANGE;
            rewardLog.setPk(rewardLog.createPk());
            rewardLog.amount = dataBiz.getMemberCurAmount(member);
            rewardLog.createDate = DateTime.Now;
             * */
            member.rebateInd=BaseModel.IND_Y;
            member.rebateMoblie = moblie;
            JsResultObject re = BaseZdBiz.Update(member,"");


                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    this.SetResult(JsResultObject.CODE_ALERT, "兑换请求发送成功，请耐心等待工作人员处理", true);
                }
                else {
                    this.SetResult(JsResultObject.CODE_ALERT, "兑换请求发送失败", true);
                }
         
            return Redirect(this.getPreUrl());
          
        
        }

        [HttpPost]
        public ActionResult DoCancelRebate( string remark) { 
            MemberModel member = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
            DataBiz dataBiz=DataBiz.GetInstant();
            /*
            MemberRewardLogModel rewardLog = new MemberRewardLogModel();
            rewardLog.memberFk = member.id;
            rewardLog.remark = remark;
            rewardLog.moblie = moblie; 
            rewardLog.exchangeType = exchangeType;
            rewardLog.exchangeStatus = MemberRewardLogModel.EXCHANGE_STATUS_UNEXCHANGE;
            rewardLog.setPk(rewardLog.createPk());
            rewardLog.amount = dataBiz.getMemberCurAmount(member);
            rewardLog.createDate = DateTime.Now;
             * */
            member.rebateInd=BaseModel.IND_N;
            JsResultObject re = BaseZdBiz.Update(member,"");


                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    this.SetResult(JsResultObject.CODE_ALERT, "兑换请求已取消，欢迎随时申请兑换", true);
                }
                else {
                    this.SetResult(JsResultObject.CODE_ALERT, "兑换取消失败", true);
                }
    
            return Redirect(this.getPreUrl());
        }


        [HttpPost]
        public ActionResult DoUnBind(string t) {
            MemberModel member = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
            string tName = "";
            if (t == "weibo") {
                tName = "新浪微博";
                member.weiboUid = string.Empty;
            }
            else if (t == "renren")
            {
                tName = "人人网";
                member.renrenUid = string.Empty;
            }
            else if (t == "kaixin")
            {
                tName = "开心网";
                member.kaixinUid = string.Empty;
            }
            BaseZdBiz.Update(member,"");
            WebUtil.SetSessionAttr(typeof(MemberModel).Name,member);
            this.SetResult(JsResultObject.CODE_ALERT,string.Format("成功解除与{0}的绑定",tName),true);
            return View("Edit");
        }

    }
}
