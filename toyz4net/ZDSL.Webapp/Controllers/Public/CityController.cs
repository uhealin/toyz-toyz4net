using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZDSL.Webapp.Controllers.Public
{
    public class CityController : Controller
    {
        //
        // GET: /City/

        public ActionResult Index(string cityId, string cityPinyin)
        {
            return View();
        }

    }
}
