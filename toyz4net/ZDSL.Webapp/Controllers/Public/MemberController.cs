using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZDSL.Webapp.Controllers.Public
{
    public class MemberController : BaseZdController
    {
        //
        // GET: /Member/

        public ActionResult Login() {
            return View();
        }
    }
}
