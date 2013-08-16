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
using System.Collections;
using Toyz4net.Core.NorthBoundAPIService;
using Toyz4net.Core.Model;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class OrderController : BaseZdController
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Datagrid_act_new()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, OrderModel.ORDER_STATUS_NEW);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_zdSuccess()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE,OrderModel.ORDER_STATUS_ZD_SUCCESS);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_elongSuccess()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, OrderModel.ORDER_STATUS_ELONG_SUCCESS);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Datagrid_act_fail()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE,OrderModel.ORDER_STATUS_FAIL);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_cancel()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE,OrderModel.ORDER_STATUS_CANCEL);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_deal()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, OrderModel.ORDER_STATUS_DEAL);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_commented()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_ACTIVATE, OrderModel.ORDER_STATUS_COMMENTED);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_del_all()
        {

            DatagridObject datagrid = createDatagrid(BaseModel.STATUS_DELETE, "");
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid(string orderId, string orderStatus)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<OrderModel> listHotel;
            

            if (ObjectUtil.IsAllNullOrEmpty(new string[] { orderId, orderStatus })==ObjectUtil.RESULT_ALL_NULL)
            {
                icr = BaseZdBiz.CreateCriteria<OrderModel>(this.getPager());
                listHotel = icr.List<OrderModel>();
                int count=BaseZdBiz.CountDistinct(icr,"id");
                datagrid = DatagridObject.ToDatagridObject(listHotel, count);
            }
            else {
                icr = BaseZdBiz.CreateCriteria<OrderModel>();
                if (!string.IsNullOrEmpty(orderStatus))
                {
                    icr.Add(Restrictions.Eq("orderStatus", orderStatus));
                }
                if (!string.IsNullOrEmpty(orderId))
                {
                    icr.Add(Restrictions.Eq("id", Convert.ToInt32(orderId)));
                }
                listHotel = icr.List<OrderModel>();
                PageList<OrderModel> pagerList = new PageList<OrderModel>(listHotel, this.getPager());
                datagrid = DatagridObject.ToDatagridObject<OrderModel>(pagerList);
            
            }

            return  JsonText(datagrid,JsonRequestBehavior.AllowGet);

            
            

          
        }
 
        public ActionResult DatagridGuest(string ids) {
            string[] arrayId = ObjectUtil.Parse(ids, "").Split(','); 
            ICriteria icr = BaseZdBiz.CreateCriteria<GuestModel>();
            icr.Add(Restrictions.In("idNumber", arrayId));
            IList<GuestModel> guests = icr.List<GuestModel>();
           
            DatagridObject datagrid = DatagridObject.ToDatagridObject<GuestModel>(guests);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DatagridHotel(string qvHotelName) {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelModel>();
            icr.Add(Restrictions.Or(Restrictions.Like("hotelName", "%" + qvHotelName + "%"),
                Restrictions.Like("hotelNameEn", "%" + qvHotelName + "%")
                ));
               
            IList<HotelModel> hotels=icr.List<HotelModel>();
            DatagridObject datagrid = DatagridObject.ToDatagridObject<HotelModel>(hotels);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ComboboxHotelRoom(string qvHotelId)
        {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelRoomModel>();
            icr.Add(Restrictions.Eq("hotelFk",qvHotelId));
            IList<HotelRoomModel> hotels = icr.List<HotelRoomModel>();
            //DatagridObject<HotelRoomModel> datagrid = new DatagridObject<HotelRoomModel>(hotels);
            return JsonText(hotels, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ComboboxRatePlan(string qvHotelId, string qvRoomTypeId, DateTime qvCheckInDate, DateTime qvCheckOutDate)
        { 
            ELongApiBiz elongBiz=ELongApiBiz.GetInstant();
            RoomForGetHotelList room = elongBiz.getRoomForGetHotelList(qvHotelId,qvRoomTypeId,qvCheckInDate,qvCheckOutDate,0);
            return JsonText(room.RatePlans,JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoGetOneRatePlan(string qvHotelId, string qvRoomTypeId, DateTime qvCheckInDate, DateTime qvCheckOutDate,int qvRatePlanId)
        {
            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            RoomForGetHotelList room = elongBiz.getRoomForGetHotelList(qvHotelId, qvRoomTypeId, qvCheckInDate, qvCheckOutDate,qvRatePlanId);
            RatePlanForGetHotelList ratePlan=null;
            JsResultObject re=new JsResultObject();
            if (room.RatePlans.Length == 1)
            {
                re.code = JsResultObject.CODE_SUCCESS;
                ratePlan = room.RatePlans[0];
                re.attrs.Add("ratePlan", ratePlan);
            }
            else {
                re.code = JsResultObject.CODE_ERROR;
                re.title = "查询失败";
                re.msg = "没有查到符合条件的价格信息";
                re.action = JsResultObject.ACTION_ALERT;
            }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoSubmitOrder() {
            OrderModel order = new OrderModel();
            order = ObjectUtil.Eval<OrderModel>(order, Request.Params, "", "");


            DateTime checkInDate=order.checkInDate;
            DateTime arraivalLateTime = new DateTime(
                checkInDate.Year,checkInDate.Month,checkInDate.Day
                ,order.arraivalLateTime.Hour,0,0
             );
            order.arraivalLateTime = arraivalLateTime;
            order.arraivalEarlyTime = arraivalLateTime.AddHours(-3);

            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            JsResultObject re= elongBiz.submitHotelOrder(ref order);
            BaseZdBiz.SaveOrUpdate(order,"");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoSubmitToElong(int id)
        {
            OrderModel order = BaseZdBiz.Load<OrderModel>(id);
            //order = WebUtil.Eval(order, "", "");
            JsResultObject re = new JsResultObject();
            DataBiz dataBiz = DataBiz.GetInstant();
            if ( StringUtil.IsIn(order.orderStatus,new string[]{OrderModel.ORDER_STATUS_COMMENTED,OrderModel.ORDER_STATUS_DEAL}))
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("订单{0} 状态为 {1},不能再处理"
                    , order.id
                    , OrderModel.GetDictOrderStatus()[order.orderStatus]
                    );
            }
            else
            {
                re = dataBiz.submitOrder(order, true);
            }
            return JsonText(re,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoSubmitToZDSL(int id) {
            OrderModel order = BaseZdBiz.Load<OrderModel>(id);
            DataBiz dataBiz = DataBiz.GetInstant();
            JsResultObject re = new JsResultObject();
            if (StringUtil.IsIn(order.orderStatus, new string[] {OrderModel.ORDER_STATUS_ELONG_SUCCESS,OrderModel.ORDER_STATUS_ZD_SUCCESS,OrderModel.ORDER_STATUS_COMMENTED, OrderModel.ORDER_STATUS_DEAL }))
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("订单{0} 状态为 {1},不能再处理"
                    , order.id
                    , OrderModel.GetDictOrderStatus()[order.orderStatus]
                    );
            }
            else
            {
                order.orderStatus = OrderModel.ORDER_STATUS_ZD_SUCCESS;
                re = BaseZdBiz.Update(order, "");
                if (re.code == JsResultObject.CODE_SUCCESS)
                {
                    re.title = "订单处理成功";
                    re.msg = string.Format("订单:{0} 处理完成", id);
                }
                else
                {
                    re.title = "订单处理失败";

                }

            }

            return JsonText(re, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save() {
            OrderModel order = new OrderModel();
            order = WebUtil.Eval<OrderModel>(order, "", "");
            JsResultObject re = new JsResultObject();
            if (StringUtil.IsIn(order.orderStatus, new string[] { OrderModel.ORDER_STATUS_COMMENTED, OrderModel.ORDER_STATUS_DEAL }))
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("订单{0} 状态为 {1},不能再修改"
                    ,order.id
                    ,OrderModel.GetDictOrderStatus()[order.orderStatus]
                    );
            }
            else
            {
                re = BaseZdBiz.SaveOrUpdate(order, "订单");
            }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Remove(string ids)
        {
            int[] arrayIds =StringUtil.ToIntArray(ids.Split(',')).ToArray();
            JsResultObject result = BaseZdBiz.Remove<OrderModel>(arrayIds, "订单");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoDeal(int id)
        {
            OrderModel order = BaseZdBiz.Load<OrderModel>(id);
            DataBiz dataBiz = DataBiz.GetInstant();

                        JsResultObject re = new JsResultObject();
                        if (StringUtil.IsIn(order.orderStatus, new string[] { OrderModel.ORDER_STATUS_COMMENTED, OrderModel.ORDER_STATUS_DEAL,OrderModel.ORDER_STATUS_CANCEL }))
                        {
                            re.code = JsResultObject.CODE_ERROR;
                            re.msg = string.Format("订单{0} 状态为 {1},不能取消"
                                , order.id
                                , OrderModel.GetDictOrderStatus()[order.orderStatus]
                                );
                        }
                        else
                        {

                            order.orderStatus = OrderModel.ORDER_STATUS_DEAL;
                             re = BaseZdBiz.Update(order, "");
                            if (re.code == JsResultObject.CODE_SUCCESS)
                            {
                                re.title = "订单处理成功";
                                re.msg = string.Format("订单:{0} 成交完成", id);
                            }
                            else
                            {
                                re.title = "订单处理失败";

                            }
                        }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoCancelOrder(int cancelOrderId,string cancelCode,string cancelReason) {

            DataBiz  dataBiz=DataBiz.GetInstant();
            OrderModel order = BaseZdBiz.Load<OrderModel>(cancelOrderId);
            JsResultObject re = new JsResultObject();
            if (StringUtil.IsIn(order.orderStatus, new string[] { OrderModel.ORDER_STATUS_COMMENTED, OrderModel.ORDER_STATUS_DEAL }))
            {
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("订单{0} 状态为 {1},不能取消"
                    ,order.id
                    ,OrderModel.GetDictOrderStatus()[order.orderStatus]
                    );
            }
            else
            {
             re = dataBiz.cancelOrder(order,cancelCode,cancelReason);
            }
            return JsonText(re,JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createDatagrid(string  status,string orderStatus)
        {
            DatagridObject datagrid = null;
            ICriteria icr;
            IList<OrderModel> listHotel;
            icr = BaseZdBiz.CreateCriteria<OrderModel>();
            if (string.IsNullOrEmpty(orderStatus))
            {
                icr.Add(Restrictions.Eq("status", status));
            }
            else {
                icr.Add(Restrictions.And(Restrictions.Eq("status", status), Restrictions.Eq("orderStatus", orderStatus)));
            }
            
            new OrderModel().setOrderBy(ref icr);
            listHotel = icr.List<OrderModel>();
            PageList<OrderModel> pagerList = new PageList<OrderModel>(listHotel, this.getPager());
            datagrid = DatagridObject.ToDatagridObject<OrderModel>(pagerList);
            return datagrid;
        }

       
        public ActionResult Form() {
            return View();
        }

    }
}
