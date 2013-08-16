using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZDSL.Biz;
using Toyz4net.Core.Util;

namespace ZDSL.Webapp
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Server.MapPath("");
            DataBiz dataBiz = DataBiz.GetInstant();
            
            //dataBiz.ImportBrandList();
            AdminBiz adminBiz = AdminBiz.GetInstant();
            if (!adminBiz.isAuthority())
            {
                Response.Redirect(WebUtil.GetWebRootPath() + "/Admin/System/Login");
            }

            CacheBiz cacheBiz = CacheBiz.GetInstant(path);
            cacheBiz.reflashDictCache();
            cacheBiz.reflashGeoCache();
            //cacheBiz.reflashRoleCache();
            //cacheBiz.reflashHotelCache();
            System.Configuration.ConfigurationManager.AppSettings["ZDSL.Biz.auto.order"]="Y";

        }
    }
}
