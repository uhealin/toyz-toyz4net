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
    public class UserController : BaseZdController
    {

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/
        public ActionResult Datagrid_act()
        {
            
            
            DatagridObject datagrid = createUserDatagrid(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_dct()
        {
           

            DatagridObject datagrid = createUserDatagrid(BaseModel.STATUS_ACTIVATE);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

       

        private DatagridObject createUserDatagrid(string  status)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<UserModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<UserModel>();
            icr.Add(Restrictions.Eq("status", status));
            new UserModel().setOrderBy(ref icr);
            listHotel = icr.List<UserModel>();
            PageList<UserModel> pagerList = new PageList<UserModel>(listHotel, this.getPager());
            datagrid =DatagridObject.ToDatagridObject<UserModel>(pagerList);
            return datagrid;
        }


        public ActionResult Save()
        {

            UserModel e = new UserModel();
            e = ObjectUtil.Eval<UserModel>(e, Request.Params, "", "");
            JsResultObject re = BaseZdBiz.Save(e);
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject re = BaseZdBiz.Remove<UserModel>(arrayIds);
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

    }
}
