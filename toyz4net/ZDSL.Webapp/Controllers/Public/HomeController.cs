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
using Toyz4net.Core.Plugin;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using ZDSL.Model.Public;


namespace ZDSL.Webapp.Controllers.Public
{
    public class HomeController : BaseZdController
    {
        //
        // GET: /Home/

        public static string GetPathIndex() {
            string pattern="{0}{1}";
            string filePath = System.Configuration.ConfigurationManager.AppSettings["ZDSL.Webapp.path.Home.Index"];
            if (IsDebug())
            {
                return string.Format(pattern, WebUtil.GetWebRootPath(), "/Public/Home/Index");
            }
            else {
                return string.Format(pattern, WebUtil.GetWebRootPath(), filePath);
            }
        }

        

        public ActionResult Login() {
            JsResultObject re=WebUtil.GetSessionAttr<JsResultObject>(typeof(JsResultObject).Name);
            
            if (re != null) {
                ViewData[typeof(JsResultObject).Name] = re;
                WebUtil.SetSessionAttr(typeof(JsResultObject).Name, null);

            }
            return View();
        }

        public ActionResult Index() {
            GeoBiz geoBiz=GeoBiz.GetInstant();
            PublicBiz publicBiz = PublicBiz.GetInstant();
            FrontPageModel frontPage = publicBiz.getCurFrontPage();


           // icr=BaseZdBiz.CreateCriteria<HotelCommentModel>(new PagerObject(1,10));
           // IList<HotelCommentModel> listComment = icr.List<HotelCommentModel>();
           // ViewData[typeof(HotelCommentModel).Name] = listComment;

            this.VdHotNews(4);
            this.VdHotBrand(16);
            string[] cityNames = frontPage.homeHotCityNameArray.Split(',');
            this.VdCityTopHotel(10,cityNames);
            this.VdHotBookingHotel(10);
            this.VdHotCommentHotel(10);
            this.VdHotCity(15);

            this.setPageDesc(PublicBiz.getCurPageSeo().homeIndexDesc);
            this.setPageKeyWords(PublicBiz.getCurPageSeo().homeIndexKeywords);
            return View();
        }





        [HttpPost]
        public ActionResult DoLogin(string key,string t,string uid) {
            MemberModel member = WebUtil.Eval(new MemberModel(), "", "");
            PublicBiz publicBiz = PublicBiz.GetInstant();
            JsResultObject re= publicBiz.login(member,key);
            if (re.code == JsResultObject.CODE_SUCCESS)
            {
                member=re.attrs[typeof(MemberModel).Name] as MemberModel;

                Session[typeof(MemberModel).Name]=member;
                OrderModel order = WebUtil.GetSessionAttr<OrderModel>(typeof(OrderModel).Name);
                if (order != null)
                {
                    order = BaseZdBiz.Load<OrderModel>(order.id);
                    order.memberFk = member.id;
                    BaseZdBiz.Update(order, "");
                    WebUtil.SetSessionAttr(typeof(OrderModel).Name, null);
                }
                if (!string.IsNullOrEmpty(t) && !string.IsNullOrEmpty(uid))
                {
                    MemberModel m = BaseZdBiz.Load<MemberModel>(member.id);
                    string tName="";
                    if (t == "weibo")
                    {
                        tName = "新浪微博";
                        m.weiboUid = uid;
                    }
                    else if (t == "renren")
                    {
                        tName = "人人网";
                        m.renrenUid = uid;
                    }
                    else if (t == "kaixin")
                    {
                        tName = "开心网";
                        m.kaixinUid = uid;
                    }
                    re= BaseZdBiz.Update(m, "");
                    m.pwd = "";
                    if (re.code == JsResultObject.CODE_SUCCESS)
                    {
                        this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的{1}帐号：{0}已绑定成功，以后可以直接使用新浪微博进行登录", uid,tName), true);
                        WebUtil.SetSessionAttr(typeof(MemberModel).Name, m);
                    }
                    else
                    {
                        this.SetResult(JsResultObject.CODE_ALERT, string.Format("当前帐号已和你的{1}帐号：{0}已绑定失败，可能已有相同的微博帐号与其他帐号绑定了", uid,tName), true);
                    }
                    return RedirectToAction("Edit","Account");
                }
                return Redirect(this.getPreUrl());
            }else{
                this.SetResult(re.code, re.msg,false);
                return RedirectToAction("Login", "Home");
            }
           
        }




        public ActionResult DoLogout() {
            Session[typeof(MemberModel).Name] = null;
            return RedirectToAction("Index","Home");
        }


        public ActionResult ImgValidateCode() {

            string vcode = "";
            byte[] bs = WebUtil.GenValidateImg (4,out vcode);
            return File(bs, @"image/jpeg");
        }


        public ActionResult CheckLogin() {
            MemberModel member = this.getAuthMember();
            JsResultObject re = new JsResultObject();
            if (member == null)
            {
                re.code = JsResultObject.CODE_ERROR;
            }
            else {
                re.code = JsResultObject.CODE_SUCCESS;
                re.attrs.Add("member", member);
            }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoSetPreUrl(string preUrl) {
            this.setPreUrl(preUrl);
            return null;
        }


        public ActionResult Error404() {
            return View();
        }

    }
}
