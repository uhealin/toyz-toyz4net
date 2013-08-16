using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;
using Toyz4net.Core.Client;
using Toyz4net.Core.NorthBoundAPIService;
using NHibernate;
using NHibernate.Criterion;

namespace ZDSL.Model.Data
{
    public class OrderModel:BaseModel
    {
        public static string ORDER_STATUS_ZD_SUCCESS = "zd_success";
        public static string ORDER_STATUS_ELONG_SUCCESS = "elong_success";
        public static string ORDER_STATUS_FAIL = "fail";
        public static string ORDER_STATUS_CANCEL = "cancel";
        public static string ORDER_STATUS_NEW = "new";
        public static string ORDER_STATUS_DEAL = "deal";
        public static string ORDER_STATUS_COMMENTED = "commented";


        private static Dictionary<string, string> DictOrderStatus;

        public static Dictionary<string, string> GetDictOrderStatus(){
            if (DictOrderStatus == null) {
                DictOrderStatus = new Dictionary<string, string>();
                DictOrderStatus.Add(ORDER_STATUS_CANCEL, "已取消");
                DictOrderStatus.Add(ORDER_STATUS_DEAL, "成交");
                DictOrderStatus.Add(ORDER_STATUS_ELONG_SUCCESS, "处理成功");
                DictOrderStatus.Add(ORDER_STATUS_FAIL, "处理失败");
                DictOrderStatus.Add(ORDER_STATUS_NEW, "新创建");
                DictOrderStatus.Add(ORDER_STATUS_ZD_SUCCESS, "处理成功");
                DictOrderStatus.Add(ORDER_STATUS_COMMENTED, "已点评");
            }
            return DictOrderStatus;
        }

        public int id { get; set; }
        public string hotelId { get; set; }
        public int elongOrderId { get; set; }
        
        public string roomTypeId { get; set; }
        public int ratePlanId { get; set; }
        public string ratePlanCode { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public string elongCardNo { get; set; }
        public string guestTypeCode{get;set;}
        public int roomAmount{get;set;}
        public int guestAmount{get;set;}
        public string paymentTypeCode{get;set;}
        public DateTime arraivalEarlyTime { get; set; }
        public DateTime arraivalLateTime { get; set; }
        public string currencyCode { get; set; }
        public float totalPrice { get; set; }
        public string confirmTypeCode { get; set; }
        public string confirmLanguageCode { get; set; }
        public string noteToHotel { get; set; }
        public string noteToElong { get; set; }
        public string creditCardNumber { get; set; }
        public string creditCardVeryfyCode { get; set; }
        public string creditCardValidyear { get; set; }
        public string creditCardValidmonth { get; set; }
        public string creditCardcardHolderName { get; set; }
        public string creditCardIdTypeCode { get; set; }
        public string creditCardIdNumber { get; set; }
        public string parameterString1{get;set;}
        public string parameterString2 { get; set; }
        public string parameterString3 { get; set; }
        public string parameterInt1 { get; set; }
        public string parameterInt2 { get; set; }
        public string parameterInt3 { get; set; }
        public string guestsIdArray { get; set; }
        public string contacterIdArray { get; set; }
        public string elongStatus { get; set; }
        public string orderStatus { get; set; }
        public DateTime createDate { get; set; }
        public DateTime lastModifyDate { get; set; }
        public string userFk { get; set; }
        public string memberFk { get; set; }
        public string hotelName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string guestsNameArray { get; set; }
        public string guestsGenderArray { get; set; }
        public string contacterName { get; set; }
        public string remark { get; set; }
        public string roomName { get; set; }
        public string ratePlanName { get; set; }

        public OrderModel() {
            this.orderStatus = ORDER_STATUS_NEW;
            this.guestTypeCode = "1";
            this.paymentTypeCode = "0";
            this.currencyCode = "RMB";
            this.confirmLanguageCode = "CN";
            this.confirmTypeCode = "sms";
        }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            //this.id = pk.ToString();
        }
        
        

        public RoomForSubmitHotelOrder toRoomForSubmitHotelOrder() {
            RoomForSubmitHotelOrder hotelOrder = new RoomForSubmitHotelOrder();
            hotelOrder.ArrivalEarlyTime = this.arraivalEarlyTime;
            hotelOrder.ArrivalLateTime = this.arraivalLateTime;
            hotelOrder.CheckInDate = this.checkInDate;
            hotelOrder.CheckOutDate = this.checkOutDate;
            hotelOrder.ConfirmLanguageCode = this.confirmLanguageCode;
            hotelOrder.ConfirmTypeCode = this.confirmTypeCode;
            hotelOrder.CurrencyCode = this.currencyCode;
            hotelOrder.GuestTypeCode = this.guestTypeCode;
            hotelOrder.GuestAmount = this.guestAmount;
            hotelOrder.HotelId = this.hotelId;
            hotelOrder.NoteToElong = this.noteToElong;
            hotelOrder.NoteToHotel = this.noteToHotel;
            hotelOrder.PaymentTypeCode = this.paymentTypeCode;
            hotelOrder.RatePlanCode = this.ratePlanCode;
            hotelOrder.RatePlanID = this.ratePlanId;
            hotelOrder.RoomAmount = this.roomAmount;
            hotelOrder.RoomTypeId = this.roomTypeId;
            hotelOrder.TotalPrice = Convert.ToDecimal(this.totalPrice);
            return hotelOrder;
        }


        public GetHotelOrderCondition toGetHotelOrderCondition() {
            GetHotelOrderCondition conditon = new GetHotelOrderCondition();
            conditon.CityId = "";
            conditon.CustomerName = "";
            conditon.EarlyArriveDate = this.arraivalEarlyTime;
            conditon.EarlyLeaveDate = this.arraivalLateTime;
            conditon.EndCreateTime = DateTime.Now;
            conditon.HotelId = this.hotelId;
            conditon.HotelName = "";

            return conditon;
        }

        public void setOrderBy(ref ICriteria icr) {
            icr.AddOrder(Order.Desc("id"));
        }
    }





    public class OrderCommentRewardModel : BaseModel {

        public string id { get; set; }
        public string comFk { get; set; }
        public DateTime createDate { get; set; }
        public string memberFk { get; set; }
        public string rebateStatuts { get; set; }
        public float amount { get; set; }
        public string rebateMethod { get; set; }
        public string remark { get; set; }
        public string userFk { get; set; }


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }


    }
    
}
