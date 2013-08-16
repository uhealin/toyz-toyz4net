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
using System.Collections;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class MemberController : BaseZdController
    {
        //
        // GET: /Member/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListMemberCommentReward() {
            return View();
        }

        public ActionResult ListMemberCommentRewardRule() {
            return View();
        }


        public ActionResult ListMemberRewardLog()
        {
            return View();
        }

        public ActionResult ListMemberAccount(string memberId) {
            MemberModel member = BaseZdBiz.Load<MemberModel>(memberId);
            ViewData[typeof(MemberModel).Name] = member;
            return View();
        }

        

        public ActionResult Datagrid_act()
        {


            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del()
        {
            
            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DatagridMemberCommentRewardRule() {
            DatagridObject datagrid = createDatagridMemberCommentRewardRule(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridMemberCommentReward_act()
        {
            DatagridObject datagrid = createDatagridMemberCommentReward(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridMemberCommentReward_del()
        {
            DatagridObject datagrid = createDatagridMemberCommentReward(BaseModel.STATUS_DELETE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DatagridMemberRewardLog() {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardLogModel>();
            icr.AddOrder(Order.Desc("createDate"));
            IList<MemberRewardLogModel> list = icr.List<MemberRewardLogModel>();
            DatagridObject datagrid = DatagridObject.ToDatagridObject<MemberRewardLogModel>(list);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridMemberAccount(string memberId) {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberAccountModel>();
            icr.Add(Restrictions.Eq("memberFk",memberId));
            icr.AddOrder(Order.Desc("createDate"));
            IList<MemberAccountModel> accounts = icr.List<MemberAccountModel>();
            DatagridObject datagrid = DatagridObject.ToDatagridObject<MemberAccountModel>(accounts);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        
        }

        private DatagridObject createDatagrid(string status)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<MemberModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("status", status));
            new MemberModel().setOrderBy(ref icr);
            listHotel = icr.List<MemberModel>();
            PageList<MemberModel> pagerList = new PageList<MemberModel>(listHotel, this.getPager());
            datagrid =  DatagridObject.ToDatagridObject<MemberModel>(pagerList);
            return datagrid;
        }


        private DatagridObject createDatagridMemberCommentRewardRule(string status) {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardRuleModel>();
            icr.Add(Restrictions.Eq("status", status));
            IList<MemberRewardRuleModel> list = icr.List<MemberRewardRuleModel>();
            PageList<MemberRewardRuleModel> pagerList = new PageList<MemberRewardRuleModel>(list, this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject<MemberRewardRuleModel>(pagerList);
            return datagrid;
        }


        private DatagridObject createDatagridMemberCommentReward(string status)
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberRewardModel>();
            icr.Add(Restrictions.Eq("status", status));
            IList<MemberRewardModel> list = icr.List<MemberRewardModel>();
            PageList<MemberRewardModel> pagerList = new PageList<MemberRewardModel>(list, this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject<MemberRewardModel>(pagerList);
            return datagrid;
        }

        public ActionResult Save()
        {
            MemberModel e = new MemberModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "会员");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<MemberModel>(arrayIds, "会员");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveMemberCommentRewardRule() {
            MemberRewardRuleModel e = new MemberRewardRuleModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "会员点评规则");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveMemberCommentRewardRule(string ids) {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<MemberRewardRuleModel>(arrayIds , "会员点评规则");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveMemberCommentReward()
        {
            MemberRewardModel e = new MemberRewardModel();
            e = ObjectUtil.Eval(e, Request.Params, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.id = e.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(e, "会员点评奖励");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveMemberCommentReward(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<MemberRewardModel>(arrayIds , "会员点评奖励");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DoRebate(string memberId,int rebateAmount,string remark) {
            JsResultObject re = new JsResultObject();
            MemberModel member = BaseZdBiz.Load<MemberModel>(memberId);
            DataBiz dataBiz = DataBiz.GetInstant();
            re = dataBiz.rebateReward(member, rebateAmount, remark);
            if (re.code == JsResultObject.CODE_SUCCESS)
            {
                member.rebateInd = BaseModel.IND_N;
                re = BaseZdBiz.Update(member, "会员");
            }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }
    }
}
