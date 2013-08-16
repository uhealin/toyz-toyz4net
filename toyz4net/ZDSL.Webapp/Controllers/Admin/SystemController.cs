using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using ZDSL.Biz;
using ZDSL.Model.Admin;
using ZDSL.Model.Cache;
using ZDSL.Model.Public;
using Toyz4net.Core.Util;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class SystemController : BaseZdController
    {
        //
        // GET: /System/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QueryHelper() {
            return View();
        }

        public ActionResult CacheHelper() {
            AdminBiz adminBiz=AdminBiz.GetInstant();
            TableCountModel tableCount = adminBiz.getTableCount();
            ViewData[typeof(TableCountModel).Name] = tableCount;
            return View();
        }

        public ActionResult Tree_Menu() {

            ISession ise = BaseZdBiz.CreateSession();
            ICriteria icr = ise.CreateCriteria<MenuModel>();
            IList<MenuModel> menus = icr.List<MenuModel>();
            IList<TreeNodeObject> nodes = MenuModel.getTreeNode(menus);
            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Frompage() {
            return View();
        }

        public ActionResult Login() {
            return View();
        }

        public ActionResult DoLogin(string id, string pwd) { 
        
            AdminBiz admin=AdminBiz.GetInstant();
            JsResultObject re = admin.login(id, pwd);
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoLogout()
        {

            Session.Clear();
            return View("Login");
        }


        public ActionResult GobalConfig() {
            ViewData[typeof(ConfigModel).Name] = AdminBiz.GetCurrConfig();
            return View();
        }

 

        public ActionResult DoSaveConfig() {
            ConfigModel config = new ConfigModel();
            config = WebUtil.Eval<ConfigModel>(config, "", "");
            JsResultObject re = BaseZdBiz.Update(config,"全局配置");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }



    }
}
