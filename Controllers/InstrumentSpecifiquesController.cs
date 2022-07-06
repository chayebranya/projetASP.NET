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
    public class InstrumentSpecifiquesController : Controller
    {
        private VegeMoteurDBEntities db = new VegeMoteurDBEntities();

        // GET: InstrumentSpecifiques
        public ActionResult Index()
        {
            var instrumentSpecifiques = db.InstrumentSpecifiques.Include(i => i.InstrumentGeneral).Include(i => i.Marque).Include(i => i.PosteTravail);
            return View(instrumentSpecifiques.ToList());
        }

        // GET: InstrumentSpecifiques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentSpecifique instrumentSpecifique = db.InstrumentSpecifiques.Find(id);
            if (instrumentSpecifique == null)
            {
                return HttpNotFound();
            }
            return View(instrumentSpecifique);
        }

        // GET: InstrumentSpecifiques/Create
        public ActionResult Create()
        {
            ViewBag.InstumentsGenraux = db.InstrumentGenerals.ToList();
            ViewBag.InstrumentGeneralId = new SelectList(db.InstrumentGenerals, "Id", "Designation");
            ViewBag.MarqueId = new SelectList(db.Marques, "Id", "Nom");
            ViewBag.NormeId = new SelectList(db.Normes, "Id", "CodeNorme");
            ViewBag.PostTravailId = new SelectList(db.PosteTravails, "Id", "Nom");
            return View();
        }

        // POST: InstrumentSpecifiques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Precision,Type,EtenduMax,EtenduMin,Unite,Caractere,Resolution,FrequenceCalibrage,FrequenceEtallonage,DateMiseEnService,NormeId,PostTravailId,InstrumentGeneralId,MarqueId")] InstrumentSpecifique instrumentSpecifique)
        {
            ViewBag.InstumentsGenraux = db.InstrumentGenerals.ToList();
            if (ModelState.IsValid)
            {
                db.InstrumentSpecifiques.Add(instrumentSpecifique);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstrumentGeneralId = new SelectList(db.InstrumentGenerals, "Id", "Designation", instrumentSpecifique.InstrumentGeneralId);
            ViewBag.MarqueId = new SelectList(db.Marques, "Id", "Nom", instrumentSpecifique.MarqueId);           
            ViewBag.PostTravailId = new SelectList(db.PosteTravails, "Id", "Nom", instrumentSpecifique.PostTravailId);
            return View(instrumentSpecifique);
        }

        // GET: InstrumentSpecifiques/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.InstumentsGenraux = db.InstrumentGenerals.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentSpecifique instrumentSpecifique = db.InstrumentSpecifiques.Find(id);
            if (instrumentSpecifique == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstrumentGeneralId = new SelectList(db.InstrumentGenerals, "Id", "Designation", instrumentSpecifique.InstrumentGeneralId);
            ViewBag.MarqueId = new SelectList(db.Marques, "Id", "Nom", instrumentSpecifique.MarqueId);
            ViewBag.PostTravailId = new SelectList(db.PosteTravails, "Id", "Nom", instrumentSpecifique.PostTravailId);
            return View(instrumentSpecifique);
        }

        // POST: InstrumentSpecifiques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Precision,Type,EtenduMax,EtenduMin,Unite,Caractere,Resolution,FrequenceCalibrage,FrequenceEtallonage,DateMiseEnService,NormeId,PostTravailId,InstrumentGeneralId,MarqueId")] InstrumentSpecifique instrumentSpecifique)
        {
            ViewBag.InstumentsGenraux = db.InstrumentGenerals.ToList();
            if (ModelState.IsValid)
            {
                db.Entry(instrumentSpecifique).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstrumentGeneralId = new SelectList(db.InstrumentGenerals, "Id", "Designation", instrumentSpecifique.InstrumentGeneralId);
            ViewBag.MarqueId = new SelectList(db.Marques, "Id", "Nom", instrumentSpecifique.MarqueId);
            ViewBag.PostTravailId = new SelectList(db.PosteTravails, "Id", "Nom", instrumentSpecifique.PostTravailId);
            return View(instrumentSpecifique);
        }

        // GET: InstrumentSpecifiques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentSpecifique instrumentSpecifique = db.InstrumentSpecifiques.Find(id);
            if (instrumentSpecifique == null)
            {
                return HttpNotFound();
            }
            return View(instrumentSpecifique);
        }

        // POST: InstrumentSpecifiques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstrumentSpecifique instrumentSpecifique = db.InstrumentSpecifiques.Find(id);
            db.InstrumentSpecifiques.Remove(instrumentSpecifique);
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
