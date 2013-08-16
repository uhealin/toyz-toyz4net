using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZDSL.Biz;
using Toyz4net.Core.Util;
using ZDSL.Model.Admin;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class MenuController : BaseZdController
    {
        //
        // GET: /Menu/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Tree(string parentId) {
            AdminBiz adminBiz = AdminBiz.GetInstant();
            UserModel user = WebUtil.GetSessionAttr<UserModel>(AdminBiz.SESSION_KEY_USER);
            if (user == null)
            {
                //return new List<TreeNodeObject>();
            }

            RoleModel role =BaseZdBiz.Load<RoleModel>(user.roleFk);
            if (role == null)
            {
                //return new List<TreeNodeObject>();
            }

            string[] arrayMenuFk = role.getArrayMenuFk();
            ICriteria icr = BaseZdBiz.CreateCriteria<MenuModel>();
            icr.Add(Restrictions.In("id", arrayMenuFk));
            icr.Add(Restrictions.Eq("status",BaseModel.STATUS_ACTIVATE));
            IList<MenuModel> menus = icr.List<MenuModel>();
            IList<TreeNodeObject> nodes = adminBiz.createTree(menus,parentId);
            return JsonText(nodes,JsonRequestBehavior.AllowGet);             
        }

        public ActionResult ComboTree(string parentId)
        {
            AdminBiz adminBiz = AdminBiz.GetInstant();
            ICriteria icr = BaseZdBiz.CreateCriteria<MenuModel>();
            icr.Add(Restrictions.Eq("status", BaseModel.STATUS_ACTIVATE));
            IList<MenuModel> menus = icr.List<MenuModel>();
            IList<TreeNodeObject> nodes = adminBiz.createTree(menus,parentId);
            return JsonText(nodes, JsonRequestBehavior.AllowGet);
        }

    }
}
