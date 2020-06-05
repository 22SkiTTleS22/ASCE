using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASCE.Controllers
{
    public class PersonalAccountController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "PersonalAccount");
        }
        
        public ActionResult List()
        {
            ViewBag.Message = "Список все лицевых счетов";

            return View();
        }
    }
}