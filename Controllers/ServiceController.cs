using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASCE.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Service");
        }

        public ActionResult List()
        {
            ViewBag.Message = "Список всех возможны услуг";

            return View();
        }
    }
}