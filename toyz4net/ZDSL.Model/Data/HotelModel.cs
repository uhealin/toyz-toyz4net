using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;
using Toyz4net.Core.Client;
using NHibernate;
using NHibernate.Criterion;

namespace ZDSL.Model.Data
{
    public class HotelModel: BaseModel
    {

        public static string RESERVE_OK = "0";
        public static string RESERVE_CLOSED = "1";
        public static string RESERVE_DELETED = "2";

        private static Dictionary<string, string> DictReserve;

        public static Dictionary<string, string> GetDictReserve() {

            if (DictReserve == null)
            {
                DictReserve = new Dictionary<string, string>();
                DictReserve.Add(RESERVE_OK,"正常营业");
                DictReserve.Add(RESERVE_CLOSED, "暂关闭");
                DictReserve.Add(RESERVE_DELETED, "已删除");
            }
            return DictReserve;
        }

        public string hotelId { get; set; }
        public string hotelName { get; set; }
        public string hotelNameEn { get; set; }
        public DateTime addTime { get; set; }
        public DateTime delTime { get; set; }
        public DateTime modifyTime { get; set; }
        public string isreserve { get; set; }
        public string recInd { get; set; }
        public int recLevel { get; set; }
        public int monthBookedCount { get; set; }
        public int weekBookedCount { get; set; }
        public int goodCommentCount { get; set; }
        public int badCommentCount { get; set; }


        public int newsId { get; set; }

        public override object getPk()
        {
            return this.hotelId;
        }

        public override void setPk(object pk)
        {
            this.hotelId = pk.ToString();
        }

        public void setOrderBy(ref ICriteria icr) {
            icr.AddOrder(new Order("recInd", false));
            icr.AddOrder(new Order("recLevel", false));
            icr.AddOrder((new Order("hotelName",true)));

        }

        public void fromYiLong(ELongHotelAdapter hotel)
        {
            this.hotelId = hotel.Hotel_id;
            this.hotelName = hotel.Hotel_name;
            this.hotelNameEn = hotel.Hotel_name_en;
            this.addTime = ELongStaticClient.ParseDateTime(hotel.Addtime);
            this.delTime = ELongStaticClient.ParseDateTime(hotel.DelTime);
            this.modifyTime = ELongStaticClient.ParseDateTime(hotel.Modifytime);
            this.isreserve = ObjectUtil.Parse(hotel.Isreserve, RESERVE_DELETED);
        }
    }


    public class HotelDetailModel : BaseModel
    {

        public string id { get; set; }	//酒店id，即HotelID
        public DateTime dateUpdated { get; set; }	//酒店最新更新时间
        public string name { get; set; }	  //酒店名称
        public string address { get; set; }//	酒店地址
        public string zip { get; set; }	  //	酒店所在地邮编
        public int category { get; set; }	  //	酒店星级（此处酒店星级是艺龙推荐星级，而非酒店挂牌星级）
        public string typology { get; set; }	  //	酒店类别
        public int roomNumber { get; set; }	  //	酒店总房间数量
        public string availPolicy { get; set; }	  //	酒店特殊信息提示
        public DateTime activationDate { get; set; }	  //	暂时不用
        public int usersRating { get; set; }	  //	用户评分,目前暂时不用
        public int elongRanking { get; set; }	  //	酒店在艺龙的排序，目前排序为动态排序，暂时不用
        public string templateType { get; set; }	  //	暂时不用
        public string translations { get; set; }	  //	暂时不用
        public Double doublePriceMin { get; set; }	  //	暂时不用
        public Double doublePriceMax { get; set; }	  //	暂时不用
        public string currency { get; set; }	  //	暂时不用
        public Double lat { get; set; }	  //	酒店所在位置的纬度
        public Double lon { get; set; }	  //	酒店所在位置的经度
        public string country { get; set; }	  //	酒店所在国家
        public string region { get; set; }	  //	酒店区域
        public string province { get; set; }	  //	酒店所在省份 
        public string city { get; set; }	  //	酒店所在城市
        public string businessZone { get; set; }	  //	酒店所在商业区
        public string district { get; set; }	  //	酒店所在行政区
        public string propertyUrl { get; set; }	  //	酒店在www.elong.com网站中的详细页URL
        public string introEditor { get; set; }	  //	酒店介绍信息
        public string ccAccepted { get; set; }	  //	可支持的信用卡
        public string description { get; set; }	  //	酒店描述
        public string phone { get; set; }	  //	酒店电话(前台)
        public string fax { get; set; }	  //	酒店传真(前台)
        public DateTime openingDate { get; set; }	  //	酒店开业日期
        public DateTime renovationDate { get; set; }	  //	酒店装修日期
        public int star { get; set; }	  //	酒店挂牌星级
        public string brandId { get; set; }	  //	酒店所属连锁品牌ID
        public string iseconomic { get; set; }	  //	是否是经济型酒店
        public string isapartment { get; set; }	  //	是否是酒店式公寓
        public string imgUrl { get; set; }
        public string trafficOverview { get; set; }
        public string trafficGuide { get; set; }
        public string generalOverview { get; set; }
        public string roomOverview { get; set; }
        public string recreationOverview { get; set; }
        public string conferenceOverview { get; set; }
        public string dinnerOverview { get; set; }
        public string generalArray { get; set; }
        public string roomArray { get; set; }
        public string recreationArray { get; set; }
        public string conferenceArray { get; set; }
        public string dinnerArray { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }



        public void from(ELongHotelDetailAdapter adapter) {
            this.id = adapter.id;
            this.dateUpdated = ELongStaticClient.ParseDateTime(adapter.dateUpdated);
            this.name = adapter.name;
            this.address = adapter.address;
            this.zip = adapter.zip;
            this.category = ObjectUtil.ParseInt(adapter.category,0);
            this.typology = adapter.typology;
            this.roomNumber = ObjectUtil.ParseInt(adapter.roomNumber,0);
            this.availPolicy = adapter.availPolicy;
            this.activationDate = ELongStaticClient.ParseDateTime(adapter.activationDate);
            this.usersRating = ObjectUtil.ParseInt(adapter.usersRating,0);
            this.elongRanking = ObjectUtil.ParseInt(adapter.elongRanking, 0);
            this.templateType = adapter.templateType;
            this.translations = adapter.translations;
            this.doublePriceMin = ObjectUtil.ParseDouble(adapter.doublePriceMin, 0);
            this.doublePriceMax = ObjectUtil.ParseDouble(adapter.doublePriceMax, 0);
            this.currency = adapter.currency;
            this.lat = ObjectUtil.ParseDouble(adapter.lat, 0);
            this.lon = ObjectUtil.ParseDouble(adapter.lon, 0);
            this.country = adapter.country;
            this.region = adapter.region;
            this.city = adapter.city;
            this.province = adapter.province;
            this.businessZone = adapter.businessZone;
            this.district = adapter.district;
            this.propertyUrl = adapter.propertyUrl;
            this.introEditor = adapter.introEditor;
            this.ccAccepted = adapter.ccAccepted;
            this.description = adapter.description;
            this.phone = adapter.Phone;
            this.fax = adapter.Fax;
            this.openingDate = ELongStaticClient.ParseDateTime(adapter.OpeningDate);
            this.renovationDate = ELongStaticClient.ParseDateTime(adapter.RenovationDate);
            this.star = ObjectUtil.ParseInt(adapter.star, 0);
            this.brandId = adapter.brandID;
            this.iseconomic = ObjectUtil.Parse(adapter.iseconomic, "0");
            this.isapartment = ObjectUtil.Parse(adapter.Isapartment, "0");
            this.trafficGuide = adapter.exTrafficGuide;
            this.trafficOverview = adapter.exTrafficOverview;
            this.generalOverview = adapter.exGeneralOverview;
            this.recreationOverview = adapter.exRecreationOverview;
            this.roomOverview = adapter.exRoomOverview;
            this.conferenceOverview = adapter.exConferenceOverview;
            this.dinnerOverview = adapter.exDinnerOverview;
            
            this.generalArray = adapter.exGeneranArray;
            this.recreationArray = adapter.exRecreationArray;
            this.roomArray = adapter.exRoomArray;
            this.conferenceArray = adapter.exConferenceArray;
            this.dinnerArray = adapter.exDinnerArray;
        }

    }

    public class HotelImageModel : BaseModel
    {
        /*
         * 0-展示图；1-餐厅；2-休闲室；3-会议室 ；4-服务；5-酒店外观 ；6-大堂/接待台；7-酒店介绍；8-房型；9-背景图；10-其他
         */

        private static Dictionary<string, string> DictImgType;

        public static Dictionary<string, string> GetDictImgType() {

            if (DictImgType == null) {
                DictImgType = new Dictionary<string, string>();
                DictImgType.Add("0", "展示图");
                DictImgType.Add("1", "餐厅");
                DictImgType.Add("2", "休闲室");
                DictImgType.Add("3", "会议室");
                DictImgType.Add("4", "服务");
                DictImgType.Add("5", "酒店外观");
                DictImgType.Add("6", "大堂/接待台");
                DictImgType.Add("7", "酒店介绍");
                DictImgType.Add("8", "房型");
                DictImgType.Add("9", "背景图");
                DictImgType.Add("10", "其他");
            }



            return DictImgType;
        
        }

        public string imgUrl { get; set; }//图片URL地址
        public string imgType { get; set; }	//图片类型
        public string title { get; set; }  //	图片标题
        public int imgNum { get; set; } 
        public string hotelFk { get; set; }
        public string imgSizeCode { get; set; }


        public override object getPk()
        {
            return this.imgUrl;
        }

        public override void setPk(object pk)
        {
            this.imgUrl = pk.ToString();
        }

        public void from(ELongHotelImageAdapter adapter) {
            this.imgUrl = adapter.imgUrl;
            this.imgType = adapter.imgType;
            this.title = adapter.title;
            this.imgNum = ObjectUtil.ParseInt(adapter.imgNum,0);
            this.hotelFk = adapter.exHotelId;

            try
            {
                string size = this.imgUrl.Split('_')[1];
                this.imgSizeCode = size;
            }
            catch (Exception ex) { }


            
            //http://static.elong.com/images/hotels/hotelimages/1/52001001_1_1_0_1.jpg
        }


        public bool isNormal() {
            try{
                string size = this.imgUrl.Split('_')[1];
                return size == "0";
            }
            catch (Exception ex) {
                return false;
            }
        }

        public bool isThum() {
            try
            {
                string size = this.imgUrl.Split('_')[1];
                return size == "1" || size == "3";
            }
            catch(Exception ex) {
                return false;
            }
        }

        public bool isAround() {
            try 
            {
                string size = this.imgUrl.Split('_')[1];
                return size == "2";
            }
            catch (Exception ex) {
                return false;
            }
        }
    }


    public class HotelRoomModel : BaseModel
    {
        public string id { get; set; }
        public string roomTypeId { get; set; }	//房型id
        public string roomName { get; set; }	//	房型名称
        public int roomTypeNum { get; set; }	//	房型数量
        public double area { get; set; }	//	房间面积
        public string floor { get; set; }	//	房型所在的楼层
        public string hasBroadnet { get; set; }	//	是否有宽带
        public string broadnetFee { get; set; }	//	宽带是否收费  
        public string bedDescription { get; set; }	//	房间描述
        public string bedType { get; set; }	//	床型描述信息
        public string note { get; set; }	//	备注
        public string hotelFk { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object pk)
        {
            this.id = pk.ToString();
        }

        public void from(ELongHotelRoomAdapter adapter) {
            this.id = string.Format("{0}::{1}", adapter.exHotelId, adapter.roomTypeId);
            this.roomTypeId = adapter.roomTypeId;
            this.roomName = adapter.roomName;
            this.roomTypeNum = ObjectUtil.ParseInt(adapter.roomTypeNum, 0);
            this.area = ObjectUtil.ParseDouble(adapter.area, 0);
            this.floor = adapter.floor;
            this.hasBroadnet = ObjectUtil.Parse(adapter.hasBroadnet,"0");
            this.broadnetFee = ObjectUtil.Parse(adapter.broadnetFee,"0");
            this.bedDescription = adapter.bedDescription;
            this.bedType = adapter.bedType;
            this.note = adapter.note;
            this.hotelFk = adapter.exHotelId;
        }
    }


    public class HotelRecommendModel : BaseModel
    {

        public string hotelId { get; set; }
        public int priorityLevel { get; set; }
        public string remark { get; set; }
        public string hotelName { get; set; }



        public override object getPk()
        {
            return this.hotelId;
        }

        public override void setPk(object obj)
        {
            this.hotelId = obj.ToString();
        }
    }


    public class HotelFeatrueInfoModel : BaseModel
    {

        public string hotelId { get; set; }
        public string drivingGuide { get; set; }
        public string propertyOtherHightlights { get; set; }
        public string propertyAmenitiesHightlights { get; set; }
        public string locationHighlights { get; set; }
        public string overview { get; set; }


        public override object getPk()
        {
            return this.hotelId;
        }

        public override void setPk(object obj)
        {
            this.hotelId = obj.ToString();
        }

        public void from(ELongHotelFeatureInfoAdapter adapter) {
            this.hotelId = adapter.HotelID;
            this.drivingGuide = adapter.DrivingGuide;
            this.propertyOtherHightlights = adapter.PropertyOtherHightlights;
            this.propertyAmenitiesHightlights = adapter.PropertyAmenitiesHightlights;
            this.locationHighlights = adapter.PropertyOtherHightlights;
            this.overview = adapter.Overview;
        }
    }

    public class HotelLandMarkModel : BaseModel {

        public string id { get; set; }
        public string hotelFk { get; set; }
        public string landmarkId { get; set; }
        public string landmarkName { get; set; }
        public string landmarkNameEn { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }

        public void from(ELongHotelLandMarkAdapter adapter) {
            this.hotelFk = adapter.HotelID;
            this.landmarkId = adapter.LandMarkID;
            this.landmarkName = adapter.LandMarkName;
            this.landmarkNameEn = adapter.LandMarkNameEn;
        }
    
    }

    public  abstract  class HotelSurroundingModel: BaseModel{

        public string id { get; set; }
        public string hotelFk { get; set; }
        public float distances { get; set; }
        public string name { get; set; }

        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }

        protected void fromBase(ELongHotelSurroundingAdapter adapter){
            this.hotelFk=adapter.exHotelId;
            this.name = adapter.Name;
            this.distances=ObjectUtil.ParseFloat(adapter.Distances,-1);
        }

    }


    public class HotelSurroundingCommerceModel : HotelSurroundingModel {
        public int timeTaking { get; set; }
        public string note { get; set; }
        public float transportFee { get; set; }
        public string transportationsFk { get; set; }

        public void from(ELongHotelSurroundingCommerceAdapter adapter) {
           this.fromBase(adapter);
           this.timeTaking = ObjectUtil.ParseInt(adapter.TimeTaking, -1);
           this.note = adapter.Note;
           this.transportFee = ObjectUtil.ParseFloat(adapter.TransportFee, 0);
           this.transportationsFk = adapter.TransportationsFk;
        }

    }

    public class HotelSurroundingRestaurantModel : HotelSurroundingModel {
        public string descp { get; set; }

        public void from(ElongHotelSurroundingRestaurantAdapter adapter) {
            this.fromBase(adapter);
            this.descp = adapter.Description;
        }
    }

    public class HotelSurroundingAttractionModel : HotelSurroundingModel {

        public void from(ElongHotelSurroundingAttractionAdapter adapter) {
            this.fromBase(adapter);
        }
    }

    public class HotelSurroundingShopModel : HotelSurroundingModel
    {
        public string descp { get; set; }

        public void from(ElongHotelSurroundingShopAdapter adapter) {

            this.fromBase(adapter);
            this.descp = adapter.Description;
        }
    }


    public class HotelTrafficInfoModel : BaseModel {

        public string id { get; set; }
        public string hotelFk { get; set; }
        public float distances { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public int timeTaking { get; set; }
        public float transportFee { get; set; }
        public string transportationsFk { get; set; }


        public override object getPk()
        {
            return this.id;
        }

        public override void setPk(object obj)
        {
            this.id = obj.ToString();
        }

        public void from(ElongHotelTrafficInfoAdapter adapter) {
            this.hotelFk = adapter.exHotelId;
            this.distances = ObjectUtil.ParseFloat(adapter.Distances,-1);
            this.name = adapter.Name;
            this.note = adapter.Note;
            this.timeTaking = ObjectUtil.ParseInt(adapter.TimeTaking,-1);
            this.transportFee = ObjectUtil.ParseFloat(adapter.TransportFee, -1);
            this.transportationsFk = adapter.Transportations;
        }
    }

}
