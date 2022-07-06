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
    public class NormesController : Controller
    {
        private VegeMoteurDBEntities db = new VegeMoteurDBEntities();

        // GET: Normes
        public ActionResult Index()
        {
            return View(db.Normes.ToList());
        }

        // GET: Normes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Norme norme = db.Normes.Find(id);
            if (norme == null)
            {
                return HttpNotFound();
            }
            return View(norme);
        }

        // GET: Normes/Create
        public ActionResult Create()
        {
            ViewBag.InstrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            return View();
        }

        // POST: Normes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CodeNorme, InstrumentSpecifiqueId, NombrePosition,Actuelle")] Norme norme)
        {
            ViewBag.InstrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            if (ModelState.IsValid)
            { 
                foreach(var item in db.Normes.Where(n=>n.InstrumentSpecifiqueId == norme.InstrumentSpecifiqueId))
                {
                    item.Actuelle = false;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }

                db.Normes.Add(norme);
                db.SaveChanges();

                for(int i=0; i<norme.NombrePosition; i++)
                {
                    // get value of each cale from the form
                    string key = "Valeur" + i;
                    var value = Request.Form[key];
                    Cale cale = new Cale()
                    {
                        NormeId = norme.Id,
                        Valeur = int.Parse(value)
                    };
                    db.Cales.Add(cale);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(norme);
        }

        // GET: Normes/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.InstrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Norme norme = db.Normes.Find(id);
            if (norme == null)
            {
                return HttpNotFound();
            }
            return View(norme);
        }

        // POST: Normes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CodeNorme,InstrumentSpecifiqueId,NombrePosition,Actuelle")] Norme norme)
        {
            ViewBag.InstrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            if (ModelState.IsValid)
            {
                db.Entry(norme).State = EntityState.Modified;
                db.SaveChanges();
                
                // delete cales and add them again
                foreach(var cale in db.Cales.Where(c=>c.NormeId == norme.Id))
                {
                    db.Cales.Remove(cale);                    
                }
                db.SaveChanges();
                for (int i = 0; i < norme.NombrePosition; i++)
                {
                    // get value of each cale from the form
                    string key = "Valeur" + i;
                    var value = Request.Form[key];
                    Cale cale = new Cale()
                    {
                        NormeId = norme.Id,
                        Valeur = int.Parse(value)
                    };
                    db.Cales.Add(cale);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(norme);
        }

        // GET: Normes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Norme norme = db.Normes.Find(id);
            if (norme == null)
            {
                return HttpNotFound();
            }
            return View(norme);
        }

        // POST: Normes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Norme norme = db.Normes.Find(id);
            db.Normes.Remove(norme);
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
