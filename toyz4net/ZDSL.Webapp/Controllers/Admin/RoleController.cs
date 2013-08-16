using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyz4net.Core.Util;
using NHibernate;
using NHibernate.Criterion;
using ZDSL.Biz;
using ZDSL.Model.Data;
using Toyz4net.Core.Model;
using ZDSL.Model.Admin;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class RoleController : BaseZdController
    {
        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Datagrid() {

            DatagridObject datagrid = this.createDatagrid();
            return  JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }


        protected DatagridObject createDatagrid() {
            ICriteria icr = BaseZdBiz.CreateCriteria<RoleModel>();
            IList<RoleModel> list = icr.List<RoleModel>();
            PageList<RoleModel> pageList = new PageList<RoleModel>(list,this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject<RoleModel>(pageList);
            return datagrid;
        }


        public ActionResult Save() {

            RoleModel e = new RoleModel();
            e = ObjectUtil.Eval<RoleModel>(e, Request.Params, "", "");
            JsResultObject re= BaseZdBiz.SaveOrUpdate(e,"角色");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject re = BaseZdBiz.Remove<RoleModel>(arrayIds);
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

    }
}
