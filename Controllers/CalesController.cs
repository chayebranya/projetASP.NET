using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VegeMoteur.App.Database;

namespace VegeMoteur.App.Controllers
{
    public class CalesController : Controller
    {
        private VegeMoteurDBEntities db = new VegeMoteurDBEntities();

        // GET: Cales
        public ActionResult Index()
        {
            var cales = db.Cales.Include(c => c.Norme);
            return View(cales.ToList());
        }

        // GET: Cales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cale cale = db.Cales.Find(id);
            if (cale == null)
            {
                return HttpNotFound();
            }
            return View(cale);
        }

        // GET: Cales/Create
        public ActionResult Create()
        {
            ViewBag.NormeId = new SelectList(db.Normes, "Id", "CodeNorme");
            return View();
        }

        // POST: Cales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,NormeId")] Cale cale)
        {
            if (ModelState.IsValid)
            {
                db.Cales.Add(cale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NormeId = new SelectList(db.Normes, "Id", "CodeNorme", cale.NormeId);
            return View(cale);
        }

        // GET: Cales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cale cale = db.Cales.Find(id);
            if (cale == null)
            {
                return HttpNotFound();
            }
            ViewBag.NormeId = new SelectList(db.Normes, "Id", "CodeNorme", cale.NormeId);
            return View(cale);
        }

        // POST: Cales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,NormeId")] Cale cale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NormeId = new SelectList(db.Normes, "Id", "CodeNorme", cale.NormeId);
            return View(cale);
        }

        // GET: Cales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cale cale = db.Cales.Find(id);
            if (cale == null)
            {
                return HttpNotFound();
            }
            return View(cale);
        }

        // POST: Cales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cale cale = db.Cales.Find(id);
            db.Cales.Remove(cale);
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
