using ASCE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASCE.Controllers
{
    public class PersonalAccountController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return RedirectToAction("List", "PersonalAccount");
        }
        
        public ActionResult List()
        {
            return View(db.PersonalAccounts);
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}