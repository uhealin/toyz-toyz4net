using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Model.Data;
using Toyz4net.Core.Util;
using ZDSL.Biz;
using NHibernate;
using NHibernate.Criterion;

namespace ZDSL.Webapp.Controllers.Public
{
    public class RegController : BaseZdController
    {
        //
        // GET: /Reg/

        public ActionResult Index() {
            ViewData[typeof(MemberModel).Name] = new MemberModel();
            return View();
        }

        public ActionResult DoSubmit() {

            MemberModel member = new MemberModel();
            member = WebUtil.Eval<MemberModel>(member, "", "");

            ViewData[typeof(MemberModel).Name] = member;
            DataBiz dataBiz = DataBiz.GetInstant();
            JsResultObject re= dataBiz.checkMemberRegInfo(member);
            if (re.code == JsResultObject.CODE_SUCCESS) {

                re = dataBiz.submitMemberRegInfo(member);
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    /*
                    MemberRewardModel reward = new MemberRewardModel();
                    reward.busType = MemberRewardModel.BUS_TYPE_NEW_MEMBER;
                    reward.amount = 10;
                    reward.remark = string.Format("{0}新会员注册成功",DateTime.Now.ToShortDateString());
                    reward.memberFk = member.id;
                    reward.createDate = DateTime.Now;
                    reward.setPk(reward.createPk());
                    reward.rebateStatus = MemberRewardModel.REBATE_STATUS_UNREBATED;
                    re= BaseZdBiz.Save(reward);
                     */
                    member.pwd = "";
                    WebUtil.SetSessionAttr(typeof(MemberModel).Name, member);
                    OrderModel order = WebUtil.GetSessionAttr<OrderModel>(typeof(OrderModel).Name);
                    if (order != null) {
                        order = BaseZdBiz.Load<OrderModel>(order.id);
                        order.memberFk = member.id;
                        BaseZdBiz.Update(order,"");
                        WebUtil.SetSessionAttr(typeof(OrderModel).Name, null);
                    }
                    return View("RequestVaildate");
                }
                else
                {
                    return View("Error");
                }
                
            }
            ViewData[typeof(JsResultObject).Name] = re;
            return View("Index");
        }

        public ActionResult DoModityReg() {
            return View("Index");
        }


        public ActionResult DoConfirm() {
            MemberModel member = new MemberModel();
            member = WebUtil.Eval<MemberModel>(member, "", "");
            member.setPk(member.createPk());
            JsResultObject re=BaseZdBiz.Save(member);
            if (re.code == JsResultObject.CODE_SUCCESS)
            {
                bool isSended = EMailUtil.SendMail("捷达商旅会员信息确认", "会员号", new string[] { member.email });
                ViewData[typeof(MemberModel).Name] = member;
                return View("RequestVaildate");
            }
            else {
                return View("Error");
            }
        }


        public ActionResult DoWeiboAuth()
        {
            string url = string.Format("https://api.t.sina.com.cn/oauth2/authorize?client_id={0}&redirect_uri={1}&response_type=token"
                , GetAppKey("weibo")
                , WebUtil.GetWebRootPath() + "/MemberAdmin/Reg/WeiboAuthResponse"
                );
            return Redirect(url);
        }



        //access_token=2.00R9OGxBLdkETB0b080b9ec3l6bsME&expires_in=86400&uid=1789422193
        public ActionResult WeiboAuthResponse(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return View();
            }
            
            if (this.getAuthMember() != null) {
                MemberModel m = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
                m.weiboUid = uid;
                JsResultObject re = BaseZdBiz.Update(m,"");
                if (re.code == JsResultObject.CODE_SUCCESS) {
                    this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的微博帐号：{0}已绑定成功，以后可以直接使用新浪微博进行登录", uid), true);
                    WebUtil.SetSessionAttr(typeof(MemberModel).Name, m);
                }else{
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的微博帐号：{0}已绑定失败，可能已有相同的微博帐号与其他帐号绑定了", uid), true);
                }
                return RedirectToAction("Edit","Account");
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("weiboUid", uid));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1)
            {
                MemberModel member = members[0];
                member.pwd = "";
                WebUtil.SetSessionAttr(typeof(MemberModel).Name, members[0]);
                return Redirect(this.getPreUrl());
            }
            else
            {
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("你的微博帐号：{0}已绑定成功，请保完以下基本资料完成登陆", uid), true);
                MemberModel member = new MemberModel();
                member.weiboUid = uid;
                ViewData[typeof(MemberModel).Name] = member;
                return View("Index");
                //return Redirect(WebUtil.GetWebRootPath()+"/MemberAdmin/Reg/Index");
            }
        }

        public ActionResult DoQQAuth()
        {
            //https://graph.qq.com/oauth2.0/authorize
            string url = string.Format("https://graph.qq.com/oauth2.0/authorize?client_id={0}&redirect_uri={1}&response_type=token"
            , GetAppKey("qq")
            , WebUtil.GetWebRootPath() + "/MemberAdmin/Reg/QQAuthResponse"
            );
            return Redirect(url);
        }

        public ActionResult QQAuthResponse(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return View();
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("QQ", uid));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1)
            {
                MemberModel member = members[0];
                member.pwd = "";
                WebUtil.SetSessionAttr(typeof(MemberModel).Name, members[0]);
                return Redirect(this.getPreUrl());
            }
            else
            {
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("你的QQ号：{0}已绑定成功，请保完以下基本资料完成登陆", uid), true);
                MemberModel member = new MemberModel();
                member.weiboUid = uid;
                ViewData[typeof(MemberModel).Name] = member;
                return View("Index");
                //return Redirect(WebUtil.GetWebRootPath()+"/MemberAdmin/Reg/Index");
            }
        }


        public ActionResult DoTencentWeiboAuth()
        {
            //https://graph.qq.com/oauth2.0/authorize
            string url = string.Format("https://open.t.qq.com/cgi-bin/request_token?oauth_consumer_key={0}&oauth_callback={1}&response_type=token"
            , GetAppKey("tencentweibo")
            , WebUtil.GetWebRootPath() + "/MemberAdmin/Reg/TencentWeiboAuthResponse"
            );
            return Redirect(url);
        }

        public ActionResult TencentWeiboAuthResponse(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return View();
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("QQ", uid));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1)
            {
                MemberModel member = members[0];
                member.pwd = "";
                WebUtil.SetSessionAttr(typeof(MemberModel).Name, members[0]);
                return Redirect(this.getPreUrl());
            }
            else
            {
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("你的QQ号：{0}已绑定成功，请保完以下基本资料完成登陆", uid), true);
                MemberModel member = new MemberModel();
                member.weiboUid = uid;
                ViewData[typeof(MemberModel).Name] = member;
                return View("Index");
                //return Redirect(WebUtil.GetWebRootPath()+"/MemberAdmin/Reg/Index");
            }
        }


        public ActionResult DoRenrenAuth()
        {
            //https://graph.qq.com/oauth2.0/authorize
            string url = string.Format("https://graph.renren.com/oauth/authorize?client_id={0}&response_type=token&redirect_uri={1}"
            , GetAppKey("renren")
            , WebUtil.GetWebRootPath() + "/MemberAdmin/Reg/RenrenAuthResponse"
            );
            return Redirect(url);
        }

        public ActionResult RenrenAuthResponse(string access_token)
        {
            if (string.IsNullOrEmpty(access_token))
            {
                return View();
            }
            string renrenUid = access_token.Split('-')[1];

            if (this.getAuthMember() != null)
            {
                MemberModel m = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
                m.renrenUid = renrenUid;
                JsResultObject re = BaseZdBiz.Update(m, "");
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的人人网帐号：{0}已绑定成功，以后可以直接使用人人网帐号进行登录", renrenUid), true);
                    WebUtil.SetSessionAttr(typeof(MemberModel).Name, m);
                }
                else
                {
                    this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的人人网帐号：{0}已绑定失败，可能已有相同的人人网帐号与其他帐号绑定了", renrenUid), true);
                }
                return RedirectToAction("Edit", "Account");
            }

            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            icr.Add(Restrictions.Eq("renrenUid", renrenUid));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1)
            {
                MemberModel member = members[0];
                member.pwd = "";
                WebUtil.SetSessionAttr(typeof(MemberModel).Name, members[0]);
                return Redirect(this.getPreUrl());
            }
            else
            {
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("你的人人网帐号：{0}已绑定成功，请保完以下基本资料完成登陆", renrenUid), true);
                MemberModel member = new MemberModel();
                member.renrenUid = renrenUid;
                ViewData[typeof(MemberModel).Name] = member;
                return View("Index");
                //return Redirect(WebUtil.GetWebRootPath()+"/MemberAdmin/Reg/Index");
            }
        }


        public ActionResult DoKaixinAuth()
        {
            //https://graph.qq.com/oauth2.0/authorize
            string url = string.Format("http://api.kaixin001.com/oauth2/authorize?response_type=token&client_id={0}&redirect_uri={1}"
            , GetAppKey("kaixin")
            , WebUtil.GetWebRootPath() + "/MemberAdmin/Reg/KaixinAuthResponse"
            );
            return Redirect(url);
        }

        public ActionResult KaixinAuthResponse(string access_token)
        {
            if (string.IsNullOrEmpty(access_token))
            {
                return View();
            }
            ICriteria icr = BaseZdBiz.CreateCriteria<MemberModel>();
            //icr.Add(Restrictions.Eq("QQ", uid));
            string uid = access_token.Split('_')[0];

            if (this.getAuthMember() != null)
            {
                MemberModel m = BaseZdBiz.Load<MemberModel>(this.getAuthMember().id);
                m.kaixinUid = uid;
                JsResultObject re = BaseZdBiz.Update(m, "");
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的开心网帐号：{0}已绑定成功，以后可以直接使用开心网帐号进行登录", uid), true);
                    WebUtil.SetSessionAttr(typeof(MemberModel).Name, m);
                }
                else
                {
                    this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的开心网帐号：{0}已绑定失败，可能已有相同的开心网帐号与其他帐号绑定了", uid), true);
                }
                return RedirectToAction("Edit", "Account");
            }

            icr.Add(Restrictions.Eq("kaixinUid", uid));
            IList<MemberModel> members = icr.List<MemberModel>();
            if (members.Count == 1)
            {
                MemberModel member = members[0];
                member.pwd = "";
                WebUtil.SetSessionAttr(typeof(MemberModel).Name, members[0]);
                return Redirect(this.getPreUrl());
            }
            else
            {
                this.SetResult(JsResultObject.CODE_ALERT, string.Format("你的开心网帐号：{0}已绑定成功，请保完以下基本资料完成登陆", uid), true);
                MemberModel member = new MemberModel();
               // member.weiboUid = uid;
                member.kaixinUid = uid;
                ViewData[typeof(MemberModel).Name] = member;
                return View("Index");
                //return Redirect(WebUtil.GetWebRootPath()+"/MemberAdmin/Reg/Index");
            }
        }
    }
}
