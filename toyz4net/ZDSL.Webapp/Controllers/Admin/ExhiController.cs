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
    public class ExhiController : BaseZdController 
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListExhiRefHotel()
        {

            return View();
        }


        //
        // GET: /Exhi/

        public ActionResult Datagrid_act_deploy()
        {
            
            DatagridObject datagrid = createExhiDatagrid(BaseModel.STATUS_ACTIVATE,ExhiModel.EXHI_STATUS_DEPLOY);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_undeploy()
        {
 
            DatagridObject datagrid = createExhiDatagrid(BaseModel.STATUS_ACTIVATE, ExhiModel.EXHI_STATUS_UNDEPLOY);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del_all()
        {
            ExhiModel e = new ExhiModel();
            
            DatagridObject datagrid = createExhiDatagrid(BaseModel.STATUS_DELETE,"");
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createExhiDatagrid(string  status,string exhiStatus)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<ExhiModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<ExhiModel>();
            if (string.IsNullOrEmpty(exhiStatus))
            {
                icr.Add(Restrictions.Eq("status", status));
            }
            else
            {
                icr.Add(Restrictions.And(Restrictions.Eq("status", status), Restrictions.Eq("exhiStatus", exhiStatus)));
            }
            new ExhiModel().setOrderBy(ref icr);
            listHotel = icr.List<ExhiModel>();
            PageList<ExhiModel> pagerList = new PageList<ExhiModel>(listHotel, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<ExhiModel>(pagerList);
            return datagrid;
        }

        [HttpPost]
        public ActionResult Save()
        {
            ExhiModel exhi = new ExhiModel();
            exhi.status = BaseModel.STATUS_ACTIVATE;
            exhi = ObjectUtil.Eval(exhi, Request.Params, "", "");
            if (string.IsNullOrEmpty(exhi.id))
            {
                exhi.id = exhi.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(exhi, "展会");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<ExhiModel>(arrayIds, "展会");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult DatagridExhiRefHotel(string exhiId)
        {

            ICriteria icr = BaseZdBiz.CreateCriteria<ExhiRefHotelModel>();
            icr.Add(Restrictions.Eq("exhiId", exhiId));
            IList<ExhiRefHotelModel> refHotels = icr.List<ExhiRefHotelModel>();
            IList<string> ids = new List<string>();
            foreach (ExhiRefHotelModel refHotel in refHotels)
            {
                ids.Add(refHotel.hotelId);
            }
            icr = BaseZdBiz.CreateCriteria<HotelModel>();
            icr.Add(Restrictions.In("hotelId", ids.ToArray()));
            IList<HotelModel> hotels = icr.List<HotelModel>();
            DatagridObject datagrid = DatagridObject.ToDatagridObject<HotelModel>(hotels);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveExhiRefHotel()
        {
            ExhiRefHotelModel e = new ExhiRefHotelModel();
            e = WebUtil.Eval<ExhiRefHotelModel>(e, "", "");
            if (string.IsNullOrEmpty(e.id))
            {
                e.setPk(e.createPk());
            }
            JsResultObject re = BaseZdBiz.SaveOrUpdate(e, "相关酒店");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveExhiRefHotel(string ids,string exhiId)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject re = new JsResultObject();
            ICriteria icr = BaseZdBiz.CreateCriteria<ExhiRefHotelModel>();
            icr.Add(Restrictions.Eq("exhiId", exhiId));
            icr.Add(Restrictions.In("hotelId", arrayIds));
            IList<ExhiRefHotelModel> refHotels = icr.List<ExhiRefHotelModel>();
            foreach (ExhiRefHotelModel refHotel in refHotels)
            {


                re.rowNum += BaseZdBiz.Delete(refHotel, "相关酒店").rowNum;

            }
            re.msg = string.Format("成功删除{0}条记录", re.rowNum);

            return JsonText(re, JsonRequestBehavior.AllowGet);
        }


    }
}
