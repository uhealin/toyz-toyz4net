using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Service;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Toyz4net.Core.NorthBoundAPIService;
using ZDSL.Model.Data;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Util;

namespace ZDSL.Biz
{
    public class ELongApiBiz:BaseZdBiz
    {

        
        private ELongApiService elongService;
        private DateTime loginTokenExpiredTime;

        private RequestHead requestHead;
        private bool hasLogin = false;
   

        private ELongApiBiz() {

            elongService = ELongApiService.GetPublicInstant();
;
        }


        private static ELongApiBiz Instant;
        private static readonly object lockHelper = new object();


        private static JsResultObject LoginSucessHandler(string userName)
        {
            JsResultObject re = new JsResultObject();
            re.title = "操作成功";
            re.msg = string.Format("登录成功", userName);
            return re;
        }

        private static JsResultObject LoginFailHandler(string userName)
        {
            JsResultObject re = new JsResultObject();
            re.title = "操作成功";
            re.msg = string.Format("登录失败", userName);
            return re;
        }

        private static JsResultObject SubmitHotelOrderHandler(SubmitHotelOrderResponse res) {
            JsResultObject re = new JsResultObject();
            if(res.ResponseHead.ResultCode==ELongApiService.RESULT_CODE_SUCCESS){
                re.title = "订单提交成功";
                re.msg = string.Format("订单ID:{0} ,最晚取消时间:{1}, 担保金额:{2}"
                    ,res.SubmitHotelOrderResult.HotelOrderID
                    , res.SubmitHotelOrderResult.CancelDeadline.ToShortTimeString()
                    ,res.SubmitHotelOrderResult.GuaranteeMoney
                );
            }else{
              re.title = "订单提交失败";
              re.code = JsResultObject.CODE_ERROR;
              re.msg=string.Format("失败原因:{0}", res.ResponseHead.ResultMessage);
            }
            return re;
        }

        private static JsResultObject InstantConfirmHandler(GetInstantConfirmResponse res) {
            JsResultObject re = new JsResultObject();
            if (res.ResponseHead.ResultCode == ELongApiService.RESULT_CODE_SUCCESS)
            {
                re.title = "订单状态查询成功";
                re.msg = string.Format("订单状态:{0} ,订单即时确认信息:{1}"
                    , res.InstantConfirmInfo.OrderState
                    , res.InstantConfirmInfo.InstantConfirm
                );
            }
            else
            {
                re.title = "订单状态查询失败";
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("失败原因:{0}", res.ResponseHead.ResultMessage);
            }
            return re;
        }


        private static JsResultObject CancelHotelOrderByIdHandler(CancelHotelOrderByIdResponse res) {

            JsResultObject re = new JsResultObject();
            if (res.ResponseHead.ResultCode == ELongApiService.RESULT_CODE_SUCCESS)
            {
                re.title = "订单取消请求发送成功";
                re.msg = "订单取消成功";
            }
            else
            {
                re.title = "订单状态查询失败";
                re.code = JsResultObject.CODE_ERROR;
                re.msg = string.Format("失败原因:{0}", res.ResponseHead.ResultMessage);
            }
            return re;
        }


        public static ELongApiBiz GetInstant()
        {

            if (Instant == null)
            {
                lock (lockHelper)
                {
                    if (Instant == null)
                    {
                        Instant = new ELongApiBiz();
                    }
                }

            }
            return Instant;
        }

        public bool isTimeout (){
            return DateTime.Now.Millisecond > this.loginTokenExpiredTime.Millisecond;
        }


        public bool login() {
            if (this.hasLogin && !this.isTimeout()) {
                return true;
            }
            LoginRequest req = new LoginRequest();
            req.UserName = ELongApiService.USER_PUBLIC;
            req.Password = ELongApiService.PWD_PUBLIC;
            LoginResponse res = elongService.Login(req);
            string rc = res.ResponseHead.ResultCode;
            this.loginTokenExpiredTime = res.LoginTokenExpiredTime;
            this.hasLogin= (rc== ELongApiService.RESULT_CODE_SUCCESS);
            this.requestHead = new RequestHead();
            this.requestHead.LoginToken = res.LoginToken.ToString();
            this.requestHead.Language = "CN";
            return this.hasLogin;
        }

        public JsResultObject submitHotelOrder(ref OrderModel orderModel)
        {
            if (!this.login())
            {
                return LoginFailHandler("");
            }
            SubmitHotelOrderRequest req = new SubmitHotelOrderRequest();
            req.RequestHead = this.requestHead;
            req.RequestHead.GUID = Guid.NewGuid().ToString();
            HotelOrderForSubmitHotelOrder hotelOrder=new HotelOrderForSubmitHotelOrder();
            RoomForSubmitHotelOrder roomOrder = this.toRoomForSubmitHotelOrder(orderModel);
            hotelOrder.RoomGroups = new RoomForSubmitHotelOrder[] {roomOrder};
            req.HotelOrder = hotelOrder;
            SubmitHotelOrderResponse res = elongService.SubmitHotelOrder(req);
            if (res.ResponseHead.ResultCode == ELongApiService.RESULT_CODE_SUCCESS)
            {
                orderModel.elongOrderId = res.SubmitHotelOrderResult.HotelOrderID;
                orderModel.orderStatus = OrderModel.ORDER_STATUS_ZD_SUCCESS;
            }
            else {
                orderModel.remark = res.ResponseHead.ResultMessage;
            }

            return SubmitHotelOrderHandler(res);
        }


        public HotelOrderPartialForGetHotelOrderList[] queryHotelOrderList(GetHotelOrderCondition condition) {
            if (!this.login())
            {
                return new HotelOrderPartialForGetHotelOrderList[] { };
            }
            HotelOrderPartialForGetHotelOrderList[] hotelOrderList=new HotelOrderPartialForGetHotelOrderList[]{};
            GetHotelOrderListRequest req = new GetHotelOrderListRequest();
            req.GetHotelOrderCondition = condition;
            req.RequestHead = this.requestHead;
            GetHotelOrderListResponse res= elongService.GetHotelOrderList(req);
            if (res.ResponseHead.ResultCode == ELongApiService.RESULT_CODE_SUCCESS) {
                hotelOrderList = res.HotelOrderPartials;
                
            }
            return hotelOrderList;
        }

        public IList<OrderModel> queryHotelOrderListById(string ids) {
           
            return null;
        }



        public JsResultObject cancelHotelOrderById(int orderId,string cancelCode,string cancelReason) {
            if (!this.login())
            {
                return LoginFailHandler("");
            }
            
            CancelHotelOrderByIdRequest req = new CancelHotelOrderByIdRequest();
            req.RequestHead = this.requestHead;
            req.CancelCode = cancelCode;
            req.CancelReason = cancelReason;
            req.HotelOrderId = orderId;
            CancelHotelOrderByIdResponse res = elongService.CancelHotelOrderById(req);
            return CancelHotelOrderByIdHandler(res);
        }

        public JsResultObject InstantConfirm(int orderId) {
            if (!this.login())
            {
                return LoginFailHandler("");
            }
            GetInstantConfirmRequest req = new GetInstantConfirmRequest();
            req.RequestHead = this.requestHead;
            req.OrderId = orderId;
            GetInstantConfirmResponse res=elongService.InstantConfirm(req);
            return InstantConfirmHandler(res);
        }

        public IList<HotelDetailModel> queryHotelList(HotelDetailModel hotel) {
            if (!this.login())
            {
                return new List<HotelDetailModel>();
            }
            GetHotelListRequest req = new GetHotelListRequest();
            req.RequestHead = this.requestHead;
            req.GetHotelCondition.HotelId = hotel.id;
            req.GetHotelCondition.RoomTypeID = "";
           
            GetHotelListResponse res= elongService.GetHotelList(req);
            
            return null;
        }

        public PageList<HotelForGetHotelList> getHotelList(GetHotelConditionForGetHotelList condition) {
            
            PageList<HotelForGetHotelList> pagerList = null;
            if (!this.login())
            {
                return pagerList;
            }
            GetHotelListRequest req = new GetHotelListRequest();
            req.RequestHead = this.requestHead;
            req.GetHotelCondition = condition;
            GetHotelListResponse res = elongService.GetHotelList(req);
            HotelForGetHotelList[] hotels = res.Hotels;
            int total = res.HotelCount;
            IList<HotelForGetHotelList> listHotels = new List<HotelForGetHotelList>(hotels);
            pagerList = new PageList<HotelForGetHotelList>(listHotels, total);
            return pagerList;
        }

        private RoomForSubmitHotelOrder toRoomForSubmitHotelOrder(OrderModel orderModel)
        {
            RoomForSubmitHotelOrder hotelOrder = orderModel.toRoomForSubmitHotelOrder();
            try
            {
                this.OpenSession();
                CreditCardModel creditCard = this.session.Load<CreditCardModel>(ELongApiService.CREDIT_NUMBER_TEST);
                hotelOrder.CreditCard = creditCard.toCreditCardForSubmitHotelOrder();
               

                RoomForGetHotelList room = this.getRoomForGetHotelList(orderModel.hotelId, orderModel.roomTypeId
                    ,orderModel.checkInDate,orderModel.checkOutDate,0
                    );
                


              //  ICriteria icr = this.session.CreateCriteria<GuestModel>();
              //  icr.Add(Restrictions.In("idNumber",orderModel.guestsIdArray.Split(',')));
              //  IList<GuestModel> guests = icr.List<GuestModel>();
              //  LinkedList<GuestForSubmitHotelOrder> orderGuests = new LinkedList<GuestForSubmitHotelOrder>();
              //  foreach (GuestModel guest in guests) {
              //      orderGuests.AddLast(guest.toGuestForSubmitHotelOrder());
               // }
                string[] guestNames = orderModel.guestsNameArray.Split(',');
                hotelOrder.Guests = this.createGuestForSubmitHotelOrderList(guestNames).ToArray();
                hotelOrder.GuestAmount = guestNames.Length;
                hotelOrder.RoomAmount = orderModel.roomAmount;

                //icr.Add(Restrictions.In("idNumber", orderModel.contacterIdArray.Split(',')));
                //guests = icr.List<GuestModel>();
                //LinkedList<ContacterForSubmitHotelOrder> orderContact = new LinkedList<ContacterForSubmitHotelOrder>();
                //foreach (GuestModel guest in guests)
                //{
                //    orderContact.AddLast(guest.toContacterForSubmitHotelOrder());
                //}
                ContacterForSubmitHotelOrder contacter = new ContacterForSubmitHotelOrder();
                contacter.Email = orderModel.email;
                contacter.Mobile = orderModel.mobile;
                contacter.Name = string.IsNullOrEmpty(orderModel.contacterName)?hotelOrder.Guests[0].Name:orderModel.contacterName;
                contacter.GenderCode = "0";
                hotelOrder.Contacters = new ContacterForSubmitHotelOrder[] { contacter };

                hotelOrder.TotalPrice =Convert.ToDecimal(orderModel.totalPrice);

            }
            catch (Exception ex) { 
             
            }
            return hotelOrder;

        }


        public RoomForGetHotelList getRoomForGetHotelList(string hotelId, string roomTypeId
              , DateTime checkInDate,DateTime checkOutDate,int ratePlanId)
        {
            RoomForGetHotelList room = new RoomForGetHotelList();
            if (!this.login())
            {
                return room;
            }
            GetHotelListRequest req = new GetHotelListRequest();
            req.RequestHead = this.requestHead;
            GetHotelConditionForGetHotelList conditon = new GetHotelConditionForGetHotelList();
            conditon.HotelId = hotelId;
            conditon.RoomTypeID = roomTypeId;
            conditon.CheckInDate = checkInDate;
            conditon.CheckOutDate = checkOutDate;
            if (ratePlanId != 0)
            {
                conditon.RatePlanID = ratePlanId;
            }
            req.GetHotelCondition = conditon;
            GetHotelListResponse res = elongService.GetHotelList(req);
            if (res.HotelCount == 1) {
                if (res.Hotels[0].Rooms.Length == 1) {
                    room = res.Hotels[0].Rooms[0];
                }
            }
            return room;
        }


        public PriceHotel[] getHotelRoomPrice(string hotelId, DateTime startDate, DateTime endDate)
        {
            if (!this.login())
            {
               return new PriceHotel[]{};
            }
            GetHotelRoomPriceRequest req = new GetHotelRoomPriceRequest();
            req.RequestHead = this.requestHead;
            req.HotelId = hotelId;
            req.StartDate=startDate;
            req.EndDate = endDate;
           
            GetHotelRoomPriceResponse res = elongService.GetHotelRoomPriceInfo(req);
       
            return  res.Hotels;
        }


        public IList<GuestForSubmitHotelOrder> createGuestForSubmitHotelOrderList(string[] guestNames)
        {
            IList<GuestForSubmitHotelOrder> guests = new List<GuestForSubmitHotelOrder>();
            foreach(string guestName in guestNames){
                GuestForSubmitHotelOrder guest = new GuestForSubmitHotelOrder();
                guest.Name = guestName;
                guest.GenderCode = "2";
                guests.Add(guest);
               
            }
            return guests;
        } 

    }
}
