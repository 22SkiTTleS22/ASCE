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
    public class CountersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Counters
        public ActionResult Index()
        {
            var counters = db.Counters.Include(c => c.PersonalAccount);
            return View(counters.ToList());
        }

        // GET: Counters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }
            return View(counter);
        }

        // GET: Counters/Create
        public ActionResult Create()
        {
            ViewBag.PersonalAccountId = new SelectList(db.PersonalAccounts, "Id", "Address");
            return View();
        }

        // POST: Counters/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonalAccountId,SerialNumber,Model,Manufacturer,DateCreate,DateVerification,DateInstall,SealNumber,InstallPlace")] Counter counter)
        {
            if (ModelState.IsValid)
            {
                db.Counters.Add(counter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonalAccountId = new SelectList(db.PersonalAccounts, "Id", "Address", counter.PersonalAccountId);
            return View(counter);
        }

        // GET: Counters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonalAccountId = new SelectList(db.PersonalAccounts, "Id", "Address", counter.PersonalAccountId);
            return View(counter);
        }

        // POST: Counters/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonalAccountId,SerialNumber,Model,Manufacturer,DateCreate,DateVerification,DateInstall,SealNumber,InstallPlace")] Counter counter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(counter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonalAccountId = new SelectList(db.PersonalAccounts, "Id", "Address", counter.PersonalAccountId);
            return View(counter);
        }

        // GET: Counters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter counter = db.Counters.Find(id);
            if (counter == null)
            {
                return HttpNotFound();
            }
            return View(counter);
        }

        // POST: Counters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Counter counter = db.Counters.Find(id);
            db.Counters.Remove(counter);
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
