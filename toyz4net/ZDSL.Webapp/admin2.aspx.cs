using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZDSL.Biz;

namespace ZDSL.Webapp
{
    public partial class admin2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Server.MapPath("");
            DataBiz dataBiz = DataBiz.GetInstant();
            
            //dataBiz.ImportBrandList();
            CacheBiz cacheBiz = CacheBiz.GetInstant(path);
            cacheBiz.reflashDictCache();
            //cacheBiz.reflashHotelCache();
            //cacheBiz.reflashBrandCache();
        }
    }
}
