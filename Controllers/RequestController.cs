using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASCE.Controllers
{
    public class RequestController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "Request");
        }

        public ActionResult List()
        {
            ViewBag.Message = "Список всех заявок";

            return View();
        }
    }
}