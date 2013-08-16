using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Toyz4net.Core.NorthBoundAPIService;
using ZDSL.Biz;
using Toyz4net.Core.Util;

namespace ZDSL.Webapp.Controllers.Admin
{
    public class ELongController : BaseZdController
    {
        //
        // GET: /ELong/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListHotelOrder() {
            return View();
        }

        public ActionResult FormImportStaticData() {
            return View();
        }


        public ActionResult DatagridHotelOrder() {

            ELongApiBiz elongBiz = ELongApiBiz.GetInstant();
            GetHotelOrderCondition condition = new GetHotelOrderCondition();
            HotelOrderPartialForGetHotelOrderList[] hotelArray= elongBiz.queryHotelOrderList(condition);
            
            IList<HotelOrderPartialForGetHotelOrderList> hotelList = hotelArray.ToList();

            PageList<HotelOrderPartialForGetHotelOrderList> hotelPageList = new PageList<HotelOrderPartialForGetHotelOrderList>(hotelList,this.getPager());
            DatagridObject datagrid = DatagridObject.ToDatagridObject(hotelPageList);
            return JsonText(datagrid ,JsonRequestBehavior.AllowGet);
        }



        public ActionResult DoImportStaticData(string type) {

            JsResultObject re = new JsResultObject();
            DataBiz dataBiz=DataBiz.GetInstant();
            int num = 0;
            if (type == "Hotel") {
                 dataBiz.ImportHotelList();
                 num = 1;
            }
            else if (type == "Geo")
            {
                dataBiz.ImportGeoList();
                dataBiz.ImportGeoLocationList();
                num = 1;
            }
            else if (type == "Brand")
            {
                dataBiz.ImportBrandList();
                num = 1;
            }
            else if (type == "Dict")
            {
                dataBiz.ImportDictList();
                num = 1;
            }
            re.code = JsResultObject.CODE_SUCCESS;
            re.msg =string.Format( "{0}:成功导入",DateTime.Now.ToLongTimeString());
            return JsonText(re, JsonRequestBehavior.AllowGet);
        }

    }
}
