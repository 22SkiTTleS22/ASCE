using ASCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASCE.Controllers
{
    public class RequestController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("List", "Request");
        }

        public ActionResult List()
        {
            ViewBag.Message = "Список всех заявок";

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Models.Request());
        }
    }
}