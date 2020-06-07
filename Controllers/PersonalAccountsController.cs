using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASCE.Models;

namespace ASCE.Controllers
{
    [Authorize]
    public class PersonalAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonalAccounts
        public ActionResult Index()
        {
            return View(db.PersonalAccounts.ToList());
        }

        // GET: PersonalAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalAccounts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountNumber,ApplicationUserId,Address,DateOpen")] PersonalAccount personalAccount)
        {
            if (ModelState.IsValid)
            {
                db.PersonalAccounts.Add(personalAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalAccount);
        }

        // GET: PersonalAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalAccount);
        }

        // POST: PersonalAccounts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountNumber,ApplicationUserId,Address,DateOpen")] PersonalAccount personalAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalAccount);
        }

        // GET: PersonalAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            if (personalAccount == null)
            {
                return HttpNotFound();
            }
            return View(personalAccount);
        }

        // POST: PersonalAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalAccount personalAccount = db.PersonalAccounts.Find(id);
            db.PersonalAccounts.Remove(personalAccount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
