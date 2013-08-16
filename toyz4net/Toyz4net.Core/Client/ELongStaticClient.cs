using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.XPath;
using Toyz4net.Core.Model;
using Toyz4net.Core.Util;

namespace Toyz4net.Core.Client
{
    public class ELongStaticClient:BaseClient
    {
        public static string BASE_PATH = "http://114-svc.elong.com/xml";

        public static string PATH_HOTEL_LIST = BASE_PATH + "/hotellist.xml";

        public static string PATH_HOTEL_ID_CN = BASE_PATH + "/perhotelcn/{0}.xml";

        public static string PATH_HOTEL_ID_EN = BASE_PATH + "/perhotelen/{0}.xml";

        public static string PATH_BRAND_CN = BASE_PATH + "/brand_cn.xml";

        public static string PATH_BRAND_EN = BASE_PATH + "/brand_en.xml";

        public static string PATH_GEO_CN = BASE_PATH + "/geo_cn.xml";

        public static string PATH_GEO_EN = BASE_PATH + "/geo_en.xml";

        public static string PATH_BASE_CODE_CN = BASE_PATH + "/hotelbaseinfocodes_cn.xml";

        public static string PATH_BASE_CODE_EN = BASE_PATH + "/hotelbaseinfocodes_en.xml";


        public static string GEO_LOCATION_DISTRICTS="districts";
        public static string GEO_LOCATION_COMMERCIAL="commercialLocations";
        public static string GEO_LOCATION_LANDMARK="LandmarkLocations";


        public static string PATTERN_DATETIME = "yyyy-MM-ddTHH:mm:ss";

        

        public static DateTime ParseDateTime(string val) {
            return DateTimeUtil.ParseDateTime(val, PATTERN_DATETIME);
        }




        public static IList<ELongHotelAdapter> GetHotelList() {
            IList<ELongHotelAdapter> listHotel = new List<ELongHotelAdapter>();
            XmlDocument document = GetDoc(PATH_HOTEL_LIST);
            XmlNodeList nodes = document.GetElementsByTagName("HotelInfoForIndex");
            foreach( XmlNode node in nodes){
               // XmlNode n = node.SelectSingleNode("/HotelInfoForIndex");
                ELongHotelAdapter hotel = new ELongHotelAdapter();
                hotel.from(node, null, "", "");
               listHotel.Add(hotel);
            }
            return listHotel;
        }




        public static IList<ELongBrandAdapter> GetBrandList() {
            IList<ELongBrandAdapter> list = new List<ELongBrandAdapter>();
            XmlDocument doc = GetDoc(PATH_BRAND_CN);
            XmlNodeList nodes = doc.GetElementsByTagName("brand");
            foreach (XmlNode node in nodes) {
                ELongBrandAdapter brand = new ELongBrandAdapter();
                brand.from(node, null, "", "");
                list.Add(brand);
            }
            return list;
        }  

        public static IList<ELongGeoAdapter> GetGeoList(){
            IList<ELongGeoAdapter> list = new List<ELongGeoAdapter>();
            XmlDocument doc = GetDoc(PATH_GEO_CN);
            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNode root=doc.SelectSingleNode("q1:HotelGeoList",xnm);
            XmlNodeList nodes = root.SelectNodes("q1:HotelGeo",xnm);
            foreach (XmlNode node in nodes) {
                ELongGeoAdapter geo = new ELongGeoAdapter();
                geo.from(node, xnm, "q1:", "");
                list.Add(geo);
            }
            return list;
        }

        public static IList<ELongBaseCodeAdapter> GetBaseCodeList() {
            IList<ELongBaseCodeAdapter> list = new List<ELongBaseCodeAdapter>();
            XmlDocument doc = GetDoc(PATH_BASE_CODE_CN);
            string[] codeTypes = new string[] { "star", "guaranteeType", "orderStatus", "guestType", "paymentType", "currencyId", "confirmType", "language", "gender", "idType", "Cancel", "Transportation", "Typology", "Inventory", "Business", "BookingRuleType", "DRRType", "CancelRuleType" };
            foreach (string codeType in codeTypes) {
                XmlNodeList nodes = doc.GetElementsByTagName(codeType);
                foreach (XmlNode node in nodes) {
                    ELongBaseCodeAdapter basecode = new ELongBaseCodeAdapter();
                    if (node.Attributes["value"] == null)
                    {
                        basecode.value = node.Attributes["vallue"].Value;
                    }
                    else {
                        basecode.value = node.Attributes["value"].Value;
                    }

                    basecode.text = node.InnerText;
                    basecode.type = codeType;
                    list.Add(basecode);
                }
            }
            return list;
        }


        public static IList<ELongGeoLocationAdapter> GetGeoLocationList() {
            IList<ELongGeoLocationAdapter> list = new List<ELongGeoLocationAdapter>();
            XmlDocument doc = GetDoc(PATH_GEO_CN);  
            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNode root = doc.SelectSingleNode("q1:HotelGeoList", xnm);
            XmlNodeList nodes = root.SelectNodes("q1:HotelGeo", xnm);
            string[] localTages = new string[] { GEO_LOCATION_DISTRICTS,GEO_LOCATION_COMMERCIAL, GEO_LOCATION_LANDMARK };
            string geoId = "";
            foreach (XmlNode node in nodes)
            {
                geoId = node.SelectSingleNode("q1:id", xnm).InnerText;
                foreach (string tag in localTages) {
                    XmlNode localTypeNode = node.SelectSingleNode("q1:"+tag,xnm);
                    XmlNodeList localNodeList= localTypeNode.ChildNodes;
                    foreach (XmlNode localNode in localNodeList) {
                        ELongGeoLocationAdapter adapter = new ELongGeoLocationAdapter();
                        adapter.from(localNode, xnm, "q1:", "");
                        adapter.exType = tag;
                        adapter.exGeoId = geoId;
                        list.Add(adapter);
                    }
                }
            }
            return list;
        }


        public static IList<ELongHotelImageAdapter> GetHotelImageList(string hotelId)
        {
            IList<ELongHotelImageAdapter> list = new List<ELongHotelImageAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);
            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodesImage = doc.SelectNodes("q1:HotelDetail/q1:images/q1:image", xnm);
            foreach (XmlNode nodeImage in nodesImage) {
                ELongHotelImageAdapter image = new ELongHotelImageAdapter();
                image.from(nodeImage, xnm, "q1:", "");
                image.exHotelId = hotelId;
                list.Add(image);
            }
            return list;
        }


        public static IList<ELongHotelRoomAdapter> GetHotelRoomList(string hotelId)
        {
            IList<ELongHotelRoomAdapter> list = new List<ELongHotelRoomAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);
           
            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:roomInfo/q1:room", xnm);
            foreach (XmlNode node in nodes)
            {
                ELongHotelRoomAdapter room = new ELongHotelRoomAdapter();
                room.from(node, xnm, "q1:", "");
                room.exHotelId = hotelId;
                list.Add(room);
            }
            return list;
        }


        public static ELongHotelFeatureInfoAdapter GetHotelFeatureInfo(string hotelId)
        {
            ELongHotelFeatureInfoAdapter adapter = new ELongHotelFeatureInfoAdapter();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNode node = doc.SelectSingleNode("q1:HotelDetail/q1:featureInfo", xnm);
            adapter.from(node, xnm, "q1:", "");
            return adapter;
        }

        public static IList<ELongHotelLandMarkAdapter> GetHotelLandMarkList(string hotelId)
        {
            IList<ELongHotelLandMarkAdapter> list = new List<ELongHotelLandMarkAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodesImage = doc.SelectNodes("q1:HotelDetail/q1:Landmarks/q1:HotelLandMark", xnm);
            foreach (XmlNode nodeImage in nodesImage)
            {
                ELongHotelLandMarkAdapter adapter = new ELongHotelLandMarkAdapter();
                adapter.from(nodeImage, xnm, "q1:", "");
                adapter.HotelID = hotelId;
                list.Add(adapter);
            }
            return list;
        }

        public static IList<ElongHotelSurroundingAttractionAdapter> GetHotelSurroundingAttractionList(string hotelId)
        {
            IList<ElongHotelSurroundingAttractionAdapter> list = new List<ElongHotelSurroundingAttractionAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:surroundingAttractions/q1:surroundingAttraction", xnm);
            foreach (XmlNode node in nodes)
            {
                ElongHotelSurroundingAttractionAdapter adapter = new ElongHotelSurroundingAttractionAdapter();
                adapter.from(node, xnm, "q1:", "");
                adapter.exHotelId = hotelId;
                list.Add(adapter);
            }
            return list;
        }

        public static IList<ELongHotelSurroundingCommerceAdapter> GetHotelSurroundingCommerceList(string hotelId)
        {
            IList<ELongHotelSurroundingCommerceAdapter> list = new List<ELongHotelSurroundingCommerceAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:surroundingCommerces/q1:surroundingCommerce", xnm);
            foreach (XmlNode node in nodes)
            {
                ELongHotelSurroundingCommerceAdapter adapter = new ELongHotelSurroundingCommerceAdapter();
                adapter.from(node, xnm, "q1:", "");
                adapter.exHotelId = hotelId;
                list.Add(adapter);
            }
            return list;
        }

        public static IList<ElongHotelSurroundingRestaurantAdapter> GetHotelSurroundingRestaurantList(string hotelId)
        {
            IList<ElongHotelSurroundingRestaurantAdapter> list = new List<ElongHotelSurroundingRestaurantAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:surroundingRestaurants/q1:surroundingRestaurant", xnm);
            foreach (XmlNode node in nodes)
            {
                ElongHotelSurroundingRestaurantAdapter adapter = new ElongHotelSurroundingRestaurantAdapter();
                adapter.from(node, xnm, "q1:", "");
                adapter.exHotelId = hotelId;
                list.Add(adapter);
            }
            return list;
        }

        public static IList<ElongHotelSurroundingShopAdapter> GetHotelSurroundingShopList(string hotelId)
        {
            IList<ElongHotelSurroundingShopAdapter> list = new List<ElongHotelSurroundingShopAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:surroundingShops/q1:surroundingShop", xnm);
            foreach (XmlNode node in nodes)
            {
                ElongHotelSurroundingShopAdapter adapter = new ElongHotelSurroundingShopAdapter();
                adapter.from(node, xnm, "q1:", "");
                adapter.exHotelId = hotelId;
                list.Add(adapter);
            }
            return list;
        }


        public static IList<ElongHotelTrafficInfoAdapter> GetHotelTrafficInfoList(string hotelId)
        {
            IList<ElongHotelTrafficInfoAdapter> list = new List<ElongHotelTrafficInfoAdapter>();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path);

            XmlNamespaceManager xnm = GetXnmQ1();
            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:trafficAndAroundInformations/q1:TrafficAndAroundInformationList/q1:HotelTrafficAndAroundInformation", xnm);
            foreach (XmlNode node in nodes)
            {
                ElongHotelTrafficInfoAdapter adapter = new ElongHotelTrafficInfoAdapter();
                adapter.from(node, xnm, "q1:", "");
                adapter.exHotelId = hotelId;
                list.Add(adapter);
            }
            return list;
        }





        private static XmlNamespaceManager GetXnmQ1() {
            XmlNamespaceManager xnm = new XmlNamespaceManager(new XmlDocument().NameTable);
            xnm.AddNamespace("q1", "http://api.elong.com/staticInfo/");
            return xnm;
        }


        public static ELongHotelDetailAdapter GetHotelDetail(string hotelId)
        {
            ELongHotelDetailAdapter adapter = new ELongHotelDetailAdapter();
            string path = string.Format(PATH_HOTEL_ID_CN, hotelId);
            XmlDocument doc = GetDoc(path); 
            XmlNamespaceManager xnm = new XmlNamespaceManager(new XmlDocument().NameTable);
            xnm.AddNamespace("q1", "http://api.elong.com/staticInfo/");
            XmlNode node = doc.SelectSingleNode("q1:HotelDetail", xnm);
            adapter.from(node, xnm, "q1:", "");

            node = doc.SelectSingleNode("q1:HotelDetail/q1:trafficAndAroundInformations/q1:Overview",xnm);
            adapter.exTrafficOverview = node.InnerText;

            node = doc.SelectSingleNode("q1:HotelDetail/q1:trafficAndAroundInformations/q1:TrafficGuide",xnm);
            adapter.exTrafficGuide = node.InnerText;

            node = doc.SelectSingleNode("q1:HotelDetail/q1:generalAmenities/q1:Overview", xnm);
            adapter.exGeneralOverview = node.InnerText;

            XmlNodeList nodes = doc.SelectNodes("q1:HotelDetail/q1:generalAmenities/q1:AmenitySimpleList/q1:string", xnm);
            adapter.exGeneranArray = "";
            foreach (XmlNode n in nodes) {
                adapter.exGeneranArray += n.InnerText+",";
            }
            adapter.exGeneranArray = adapter.exGeneranArray.Trim(',');

            node = doc.SelectSingleNode("q1:HotelDetail/q1:roomAmenities/q1:Overview", xnm);
            adapter.exRoomOverview = node.InnerText;

            nodes = doc.SelectNodes("q1:HotelDetail/q1:roomAmenities/q1:AmenitySimpleList/q1:string", xnm);
            adapter.exRoomArray = "";
            foreach (XmlNode n in nodes)
            {
                adapter.exRoomArray += n.InnerText + ",";
            }
            adapter.exRoomArray = adapter.exRoomArray.Trim(',');

            node = doc.SelectSingleNode("q1:HotelDetail/q1:diningAmenities/q1:Overview", xnm);
            adapter.exDinnerOverview = node.InnerText;

            nodes = doc.SelectNodes("q1:HotelDetail/q1:diningAmenities/q1:AmenitySimpleList/q1:string", xnm);
            adapter.exDinnerArray = "";
            foreach (XmlNode n in nodes)
            {
                adapter.exDinnerArray += n.InnerText + ",";
            }
            adapter.exDinnerArray = adapter.exDinnerArray.Trim(',');



            node = doc.SelectSingleNode("q1:HotelDetail/q1:recreationAmenities/q1:Overview", xnm);
            adapter.exRecreationOverview = node.InnerText;

            nodes = doc.SelectNodes("q1:HotelDetail/q1:recreationAmenities/q1:AmenitySimpleList/q1:string", xnm);
            adapter.exRecreationArray = "";
            foreach (XmlNode n in nodes)
            {
                adapter.exRecreationArray += n.InnerText + ",";
            }
            adapter.exRecreationArray = adapter.exRecreationArray.Trim(',');

            node = doc.SelectSingleNode("q1:HotelDetail/q1:conferenceAmenities/q1:Overview", xnm);
            adapter.exConferenceOverview = node.InnerText;

            nodes = doc.SelectNodes("q1:HotelDetail/q1:conferenceAmenities/q1:AmenitySimpleList/q1:string", xnm);
            adapter.exConferenceArray = "";
            foreach (XmlNode n in nodes)
            {
                adapter.exConferenceArray += n.InnerText + ",";
            }
            adapter.exConferenceArray = adapter.exConferenceArray.Trim(',');

            return adapter;
        } 
 
    }

    public class ELongHotelAdapter: BaseAdapter
    { 
        
        public string Hotel_id{ get;set;}
        public string Hotel_name { get; set; }
        public string Hotel_name_en { get; set; }
        public string Isreserve { get; set; }
        public string Addtime { get;set;}
        public string DelTime {get;set;}
        public string Modifytime { get; set; }

    }

    public class ELongHotelDetailAdapter:BaseAdapter
    { 
    
        public string id{ get;set;}	//酒店id，即HotelID
        public string dateUpdated{ get;set;}	//酒店最新更新时间
        public string name{ get;set;}	  //酒店名称
        public string address{ get;set;}//	酒店地址
        public string zip{ get;set;}	  //	酒店所在地邮编
        public string category{ get;set;}	  //	酒店星级（此处酒店星级是艺龙推荐星级，而非酒店挂牌星级）
        public string typology{ get;set;}	  //	酒店类别
        public string roomNumber{ get;set;}	  //	酒店总房间数量
        public string availPolicy{ get;set;}	  //	酒店特殊信息提示
        public string activationDate{ get;set;}	  //	暂时不用
        public string usersRating{ get;set;}	  //	用户评分,目前暂时不用
        public string elongRanking{ get;set;}	  //	酒店在艺龙的排序，目前排序为动态排序，暂时不用
        public string templateType{ get;set;}	  //	暂时不用
        public string translations{ get;set;}	  //	暂时不用
        public string doublePriceMin { get;set;}	  //	暂时不用
        public string doublePriceMax{ get;set;}	  //	暂时不用
        public string currency{ get;set;}	  //	暂时不用
        public string  lat{ get;set;}	  //	酒店所在位置的纬度
        public string  lon{ get;set;}	  //	酒店所在位置的经度
        public string country{ get;set;}	  //	酒店所在国家
        public string  region{ get;set;}	  //	酒店区域
        public string province{ get;set;}	  //	酒店所在省份 
        public string city{ get;set;}	  //	酒店所在城市
        public string businessZone{ get;set;}	  //	酒店所在商业区
        public string district { get;set;}	  //	酒店所在行政区
        public string propertyUrl{ get;set;}	  //	酒店在www.elong.com网站中的详细页URL
        public string introEditor{ get;set;}	  //	酒店介绍信息
        public string ccAccepted{ get;set;}	  //	可支持的信用卡
        public string description{ get;set;}	  //	酒店描述
        public string Phone{ get;set;}	  //	酒店电话(前台)
        public string Fax{ get;set;}	  //	酒店传真(前台)
        public string OpeningDate{ get;set;}	  //	酒店开业日期
        public string RenovationDate{ get;set;}	  //	酒店装修日期
        public string star{ get;set;}	  //	酒店挂牌星级
        public string brandID{ get;set;}	  //	酒店所属连锁品牌ID
        public string iseconomic{ get;set;}	  //	是否是经济型酒店
        public string Isapartment{ get;set;}	  //	是否是酒店式公寓

        public string exTrafficOverview { get; set; }
        public string exTrafficGuide { get; set; }
        public string exGeneralOverview { get; set; }
        public string exGeneranArray { get; set; }
        public string exRoomOverview { get; set; }
        public string exRoomArray { get; set; }
        public string exRecreationOverview { get; set; }
        public string exRecreationArray { get; set; }
        public string exConferenceOverview { get; set; }
        public string exConferenceArray { get; set; }
        public string exDinnerOverview { get; set; }
        public string exDinnerArray { get; set; }
    }


    public class ELongBrandAdapter:BaseAdapter
    { 
        public string  brandID{ get;set; }	//连锁品牌ID
        public string  groupId{ get;set; }	//	连锁品牌所属的酒店集团ID
        public string brandName{ get;set; }	//	连锁酒店品牌中文简称
        public string brandNameLong{ get;set; }	//	连锁酒店品牌中文全称
        public string brandFirstletter { get; set; }	//	连锁品牌名称首字母
        public string brandPinYin { get; set; }
        public string picURL { get; set; }	//	连锁品牌图片地址的URL
        public string brandURL{ get;set; }	//	连锁品牌网站的URL
        public string hotelCount{ get;set; }	//	连锁品牌包含酒店数量
        public string lastChangetime { get; set; }	//	最后的修改时间
    }


    public class ELongGeoAdapter : BaseAdapter
    { 
        public string id{ get;set; }	//	Geoid,从1开始，依次加1
        public string country{ get;set; }	//	国家
        public string provinceName{ get;set; }	//	省份名称
        public string provinceId{ get;set; }	//	省份ID
        public string cityName{ get;set; }	//	城市名称
        public string cityCode{ get;set; }	//	城市ID
        public string properties{ get;set; }	//	该城市下的有效酒店个数 
        public string url { get; set; }	//	该酒店在www.elong.com网站上的搜索列表URL
    }


    public class ELongGeoLocationAdapter : BaseAdapter {

        public string id { get; set; }	//ID
        public string name { get; set; }	 //名称
        public string exType { get; set; }   //自定义属性
        public string exGeoId { get; set; }
    }

    public class ELongBaseCodeAdapter :BaseAdapter
    {
        public string text { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }


    public class ELongHotelImageAdapter : BaseAdapter { 
      public string imgUrl{ get; set; }//图片URL地址
      public string imgType{ get; set; }	//图片类型
      public string  title{ get; set; }  //	图片标题
      public string imgNum { get; set; }
      public string exHotelId { get; set; }
    }

    public class ELongHotelRoomAdapter : BaseAdapter { 
     
     public string  roomTypeId{get;set;}	//房型id
    public string  roomName{get;set;}	//	房型名称
    public string  roomTypeNum{get;set;}	//	房型数量
    public string  area{get;set;}	//	房间面积
    public string  floor{get;set;}	//	房型所在的楼层
    public string  hasBroadnet{get;set;}	//	是否有宽带
    public string  broadnetFee{get;set;}	//	宽带是否收费  
    public string  bedDescription{get;set;}	//	房间描述
    public string  bedType{get;set;}	//	床型描述信息
    public string note { get; set; }	//	备注
    public string exHotelId { get; set; }

    }


    public class ELongHotelLandMarkAdapter : BaseAdapter { 
     public string HotelID{get;set;}
     public string LandMarkID{get;set;}
     public string LandMarkName{get;set;}
     public string LandMarkNameEn { get; set; }
    
    }

    public class ELongHotelFeatureInfoAdapter: BaseAdapter{
      public string HotelID{get;set;}
      public string DrivingGuide{get;set;}
      public string PropertyOtherHightlights{get;set;}
      public string PropertyAmenitiesHightlights{get;set;}
      public string LocationHighlights{get;set;}
      public string Overview { get; set; }
    }


    public abstract class ELongHotelSurroundingAdapter : BaseAdapter {

        public string  Distances { get; set; }
        public string Name { get; set; }
        public string exHotelId { get; set; }
    }


    public class ELongHotelSurroundingCommerceAdapter : ELongHotelSurroundingAdapter {
        public string TimeTaking { get; set; }
        public string Note { get; set; }
        public string TransportFee { get; set; }
        public string TransportationsFk { get; set; }
        
    }

    public class ElongHotelSurroundingRestaurantAdapter : ELongHotelSurroundingAdapter
    {
        public string Description { get; set; }
    }

    public class ElongHotelSurroundingAttractionAdapter : ELongHotelSurroundingAdapter
    {

    }

    public class ElongHotelSurroundingShopAdapter : ELongHotelSurroundingAdapter
    {
        public string Description { get; set; }
    }


    public class ElongHotelTrafficInfoAdapter : BaseAdapter
    {
        public string Distances { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string TimeTaking { get; set; }
        public string TransportFee { get; set; }
        public string Transportations { get; set; }
        public string exHotelId { get; set; }
    }

}
