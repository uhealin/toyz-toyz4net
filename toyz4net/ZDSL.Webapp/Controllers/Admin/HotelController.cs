using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ZDSL.Biz;
using ZDSL.Model.Data;
using ZDSL.Model.Cache;
using NHibernate;
using NHibernate.Criterion;
using Toyz4net.Core.Util;
using Toyz4net.Core.Model;
using NHibernate.Mapping;
using System.Collections;

namespace ZDSL.Webapp.Controllers.Admin
{
    
    public class HotelController : BaseZdController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListRecommend() {
            return View();
        }


        public ActionResult SingleDetail(string hotelId) {
            HotelDetailModel hotelDetail =null; 
            if(string.IsNullOrEmpty(hotelId)){
               hotelDetail=new HotelDetailModel();
            }else{
               hotelDetail=BaseZdBiz.Load<HotelDetailModel>(hotelId);
            }
            if (hotelDetail == null) {
                return View("error");
            }
            ViewData[typeof(HotelDetailModel).Name] =hotelDetail;
            return View();
        }

        public ActionResult Datagrid(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_ACTIVATE, HotelModel.RESERVE_OK, qtHotelName, qvHotelName);

            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        //参数名必须与表单字段名一样
        public ActionResult Datagrid_act_normal(string qtHotelName, string qvHotelName)
        {

            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_ACTIVATE,HotelModel.RESERVE_OK, qtHotelName, qvHotelName);
            
            return JsonText(datagrid,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_close(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_ACTIVATE, HotelModel.RESERVE_CLOSED, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Datagrid_act_collapse(string qtHotelName, string qvHotelName)
        {
            DatagridObject datagrid = createHotelDatagrid(BaseModel.STATUS_ACTIVATE, HotelModel.RESERVE_DELETED, qtHotelName, qvHotelName);
            return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DatagridRecommend() {

            DatagridObject datagrid = this.createDatagridRecommend();
            return JsonText(datagrid,JsonRequestBehavior.AllowGet);
        }


        public ActionResult DatagridHotelDetailSubInfo(string modelType,string hotelFk) {
        
             DatagridObject datagrid=null;
             if (modelType == "HotelRoom")
             {
                 datagrid = createDatagridHotelSubDetail<HotelRoomModel>(hotelFk);
             }
             else if (modelType == "HotelImage")
             {
                 datagrid = createDatagridHotelSubDetail<HotelImageModel>(hotelFk);
             }
             else if (modelType == "HotelTrafficInfo")
             {
                 datagrid = createDatagridHotelSubDetail<HotelTrafficInfoModel>(hotelFk);
             }
             else if (modelType == "HotelSurroundingCommerce")
             {
                 datagrid = createDatagridHotelSubDetail<HotelSurroundingCommerceModel>(hotelFk);
             }
             else if (modelType == "HotelSurroundingRestaurant")
             {
                 datagrid = createDatagridHotelSubDetail<HotelSurroundingRestaurantModel>(hotelFk);
             }
             else if (modelType == "HotelSurroundingAttraction")
             {
                 datagrid = createDatagridHotelSubDetail<HotelSurroundingAttractionModel>(hotelFk);
             }
             else if (modelType == "HotelSurroundingShop")
             {
                 datagrid = createDatagridHotelSubDetail<HotelSurroundingShopModel>(hotelFk);
             }
             else if (modelType == "HotelLandMark")
             {
                 datagrid = createDatagridHotelSubDetail<HotelLandMarkModel>(hotelFk);
             }



             else {
                 datagrid = DatagridObject.ToDatagridObject(new ArrayList());
             }
             return JsonText(datagrid, JsonRequestBehavior.AllowGet);
        }

        private DatagridObject createHotelDatagrid(string status, string isreserve, string qHotelNameType, string qHotelNameVal)
        {

            DatagridObject datagrid = null;
            ICriteria icr=null;
            IList<HotelModel> listHotel;
            bool isQuery = !string.IsNullOrEmpty(qHotelNameVal) && !string.IsNullOrEmpty(qHotelNameType);

            if (isQuery)
            {
                icr = BaseZdBiz.CreateCriteria<HotelModel>();
            }
            else {
                icr = BaseZdBiz.CreateCriteria<HotelModel>(this.getPager());
            }

            icr.Add(Restrictions.And(Restrictions.Eq("isreserve", isreserve),Restrictions.Eq("status",status)));

            if (isQuery)  
            {
                //带条件查询使用集合切割策略进行分页
                
                if (qHotelNameType == "cn")
                {
                    icr.Add(Restrictions.Like("hotelName", "%" + qHotelNameVal + "%"));

                }
                else if (qHotelNameType == "en")
                {
                    icr.Add((Restrictions.Like("hotelNameEn", "%" + qHotelNameVal + "%")));
                }
                else {
                    return DatagridObject.NewIntanst();
                }
                
                //icr.Add(Restrictions.Eq("isreserve", status));
                new HotelModel().setOrderBy(ref icr);
 
                listHotel = icr.List<HotelModel>();
                PageList<HotelModel> pagerList = new PageList<HotelModel>(listHotel, this.getPager());
                datagrid = DatagridObject.ToDatagridObject<HotelModel>(pagerList);
            }
            else
            {
                //不带条件查询使用分页缓存进行分页
                
                
                new HotelModel().setOrderBy(ref icr);

                listHotel = icr.List<HotelModel>();
                AdminBiz adminBiz = AdminBiz.GetInstant();
                TableCountModel tableCount = adminBiz.getTableCount();
                int total = 0;
                if (isreserve == HotelModel.RESERVE_OK) {
                    total = tableCount.hotelOk;
                }
                else if (isreserve == HotelModel.RESERVE_CLOSED) {
                    total = tableCount.hotelClosed;
                }
                else if (isreserve == HotelModel.RESERVE_DELETED) {
                    total = tableCount.hotelDel;
                }
                datagrid = DatagridObject.ToDatagridObject<HotelModel>(listHotel, total);
            }
            return datagrid;
        }



        public DatagridObject createDatagridRecommend() {
            ICriteria icr = BaseZdBiz.CreateCriteria<HotelRecommendModel>();
            IList<HotelRecommendModel> list = icr.List<HotelRecommendModel>();
            PageList<HotelRecommendModel> pagerList = new PageList<HotelRecommendModel>(list,this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject<HotelRecommendModel>(pagerList);
            return datagrid;

        }

        public DatagridObject createDatagridHotelSubDetail<T>(string hotelFk) {
            ICriteria icr = BaseZdBiz.CreateCriteria<T>();
            icr.Add(Restrictions.Eq("hotelFk", hotelFk));
            IList<T> list = icr.List<T>();
            PageList<T> pagerList = new PageList<T>(list, this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject<T>(pagerList);
            return datagrid;
        }


        [HttpPost]
        public ActionResult Save() {
            HotelModel hotel = new HotelModel();
            hotel = ObjectUtil.Eval(hotel,Request.Params,"","");
            if (string.IsNullOrEmpty(hotel.hotelId)) {
                hotel.hotelId = hotel.createPk().ToString();
            }
            JsResultObject result= BaseZdBiz.SaveOrUpdate(hotel,"酒店");
            return JsonText(result,JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public ActionResult SaveRecommend()
        {
            HotelRecommendModel hotel = new HotelRecommendModel();
            hotel = ObjectUtil.Eval(hotel, Request.Params, "", "");
            if (string.IsNullOrEmpty(hotel.hotelId))
            {
                hotel.hotelId = hotel.createPk().ToString();
            }
            JsResultObject result = BaseZdBiz.SaveOrUpdate(hotel, "酒店推荐");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveRecommend(string ids)
        {
            string[] arrayIds = ids.Split(',');
            JsResultObject result = BaseZdBiz.Remove<HotelRecommendModel>(arrayIds, "酒店推荐");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DoRecommend(string hotelId) {
            
            HotelModel hotel = BaseZdBiz.Load<HotelModel>(hotelId);
            DataBiz dataBiz= DataBiz.GetInstant();
            JsResultObject result = dataBiz.recommendHotel(hotel,0,"");
            return JsonText(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveHotelDetail(string id) {
            HotelDetailModel e = BaseZdBiz.Load<HotelDetailModel>(id);
            e = ObjectUtil.Eval<HotelDetailModel>(e, Request.Params, "", "");
            JsResultObject re = BaseZdBiz.SaveOrUpdate(e,"酒店详细信息");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DoImportFromElong(string hotelId) {
            JsResultObject re = new JsResultObject();
            DataBiz dataBiz = DataBiz.GetInstant();
            re= dataBiz.ImportHotelDetail(hotelId,true );
            if (re.rowNum >= 1)
            {
                re.title = "操作成功";
                re.msg = string.Format("成功从eLong导入酒店:{0}", hotelId);
                
            }
            else {
                re.title = "操作失败";
                re.code = JsResultObject.CODE_ERROR;
            }
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DoUpdateImgUrl(string hotelId, string imgUrl) {

            JsResultObject re = new JsResultObject();
            HotelDetailModel hotelDetail = BaseZdBiz.Load<HotelDetailModel>(hotelId);
            hotelDetail.imgUrl = imgUrl;
            re = BaseZdBiz.Update(hotelDetail,"");
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

    }
}
