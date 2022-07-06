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
    public class PosteTravailsController : Controller
    {
        private VegeMoteurDBEntities db = new VegeMoteurDBEntities();

        // GET: PosteTravails
        public ActionResult Index()
        {
            var posteTravails = db.PosteTravails.Include(p => p.Section);
            return View(posteTravails.ToList());
        }

        // GET: PosteTravails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosteTravail posteTravail = db.PosteTravails.Find(id);
            if (posteTravail == null)
            {
                return HttpNotFound();
            }
            return View(posteTravail);
        }

        // GET: PosteTravails/Create
        public ActionResult Create()
        {
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Nom");
            return View();
        }

        // POST: PosteTravails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,SectionId")] PosteTravail posteTravail)
        {
            if (ModelState.IsValid)
            {
                db.PosteTravails.Add(posteTravail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Nom", posteTravail.SectionId);
            return View(posteTravail);
        }

        // GET: PosteTravails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosteTravail posteTravail = db.PosteTravails.Find(id);
            if (posteTravail == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Nom", posteTravail.SectionId);
            return View(posteTravail);
        }

        // POST: PosteTravails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,SectionId")] PosteTravail posteTravail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(posteTravail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "Nom", posteTravail.SectionId);
            return View(posteTravail);
        }

        // GET: PosteTravails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PosteTravail posteTravail = db.PosteTravails.Find(id);
            if (posteTravail == null)
            {
                return HttpNotFound();
            }
            return View(posteTravail);
        }

        // POST: PosteTravails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PosteTravail posteTravail = db.PosteTravails.Find(id);
            db.PosteTravails.Remove(posteTravail);
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
