using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyz4net.Core.Util;
using ZDSL.Model.Data;
using NHibernate;
using ZDSL.Biz;
using NHibernate.Criterion;
using ZDSL.Model.Cache;
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class GuestController : BaseZdController
    {
        public ActionResult Index()
        {
            return View();
        }

        //参数名必须与表单字段名一样
        public ActionResult Datagrid_act_vip(string qtHotelName, string qvHotelName)
        {

            DatagridObject datagrid = createGuestDatagrid(BaseModel.STATUS_ACTIVATE, GuestModel.TYPT_VIP, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_blacklist(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createGuestDatagrid(BaseModel.STATUS_ACTIVATE, GuestModel.TYPE_BLACKLIST, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_normal(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createGuestDatagrid(BaseModel.STATUS_ACTIVATE, GuestModel.TYPE_NORMAL, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del_all(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createGuestDatagrid(BaseModel.STATUS_DELETE,"", qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createGuestDatagrid(string status,string type, string qtGuestName, string qvGuestName)
        {

            DatagridObject datagrid = null;
            ICriteria icr;
            IList<GuestModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<GuestModel>();


            if (type == "")
            {
                icr.Add(Restrictions.Eq("status", status));
                
            }
            else {
                icr.Add(Restrictions.And(Restrictions.Eq("status", status),Restrictions.Eq("type", type)));
            }

            if (!string.IsNullOrEmpty(qvGuestName) && !string.IsNullOrEmpty(qtGuestName))
            {
                //带条件查询使用集合切割策略进行分页
                if (qtGuestName == "cn")
                {
                    icr.Add(Restrictions.Like("nameCn", "%" + qvGuestName + "%"));

                }
                else if (qtGuestName == "en")
                {
                    icr.Add(Restrictions.Like("nameEn", "%" + qvGuestName + "%"));
                }
                else if (qtGuestName == "idcard") {
                    icr.Add(Restrictions.Eq("idNumber",qvGuestName));
                }
                else
                {
                    return  DatagridObject.NewIntanst();
                }

                new GuestModel().setOrderBy(ref icr);
                listHotel = icr.List<GuestModel>();
                PageList<GuestModel> pagerList = new PageList<GuestModel>(listHotel, this.getPager());
                datagrid =  DatagridObject.ToDatagridObject <GuestModel>(pagerList);
            }
            else
            {
                //不带条件查询使用分页缓存进行分页
                icr = BaseZdBiz.CreateCriteria<GuestModel>(this.getPager());
                if (type == "")
                {
                    icr.Add(Restrictions.Eq("status", status));

                }
                else
                {
                    icr.Add(Restrictions.And(Restrictions.Eq("status", status), Restrictions.Eq("type", type)));
                }
               // icr.Add(Restrictions.Eq("status", status));
                new GuestModel().setOrderBy(ref icr);
                listHotel = icr.List<GuestModel>();
                AdminBiz adminBiz = AdminBiz.GetInstant();
                TableCountModel tableCount = adminBiz.getTableCount();
                datagrid =  DatagridObject.ToDatagridObject <GuestModel>(listHotel, tableCount.hotelOk);
            }
            return datagrid;
        }

        [HttpPost]
        public ActionResult Save()
        {
            GuestModel guest = new GuestModel();
            guest = ObjectUtil.Eval(guest, Request.Params, "", "");
            JsResultObject result = BaseZdBiz.SaveOrUpdate(guest, "住客");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<GuestModel>(arrayIds , "住客");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

    }
}
