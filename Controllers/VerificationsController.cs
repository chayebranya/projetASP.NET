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
    public class VerificationsController : Controller
    {
        private VegeMoteurDBEntities db = new VegeMoteurDBEntities();

        // GET: Verifications
        public ActionResult Index()
        {

            var verifications = db.Verifications.Include(v => v.InstrumentSpecifique);
            return View(verifications.ToList());
        }

        public ActionResult Verifier()
        {

            var instrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            return View(instrumentsSpecifiques);
        }

        public ActionResult ErreurContactPleineTouche(int? id)
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

        
        public ActionResult ErreurContactPleineToucheSave()
        {
            var InstrumentSpecifiqueId = int.Parse(Request.Form["InstrumentSpecifiqueId"]);
            if (InstrumentSpecifiqueId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InstrumentSpecifique instrumentSpecifique = db.InstrumentSpecifiques.Find(InstrumentSpecifiqueId);
            if (instrumentSpecifique == null)
            {
                return HttpNotFound();
            }

            Verification verification = new Verification();
            verification.Description = Request.Form["Description"];
            verification.Date = DateTime.Now;
            verification.InstrumentSpecifiqueId = InstrumentSpecifiqueId;
            db.Verifications.Add(verification);
            db.SaveChanges();

            ErreurContactPleineTouche erreur = new ErreurContactPleineTouche();
            erreur.VerificationId = verification.Id;
            List<CalibrageCale> verificationCales = new List<CalibrageCale>();
            
            // get cales for current norme of current instrument specifique
            var norme = instrumentSpecifique.Normes.Where(n => n.Actuelle).FirstOrDefault();
            foreach(var cale in norme.Cales)
            {
                string key = "mesureA-" + cale.Id;
                float mesureA = float.Parse(Request.Form[key]);

                key = "mesureB-" + cale.Id;
                var mesureB = float.Parse(Request.Form[key]);

                key = "ecartA-" + cale.Id;
                float ecartA = float.Parse(Request.Form[key]);

                key = "ecartB-" + cale.Id;
                float ecartB = float.Parse(Request.Form[key]);

                verificationCales.Add(new CalibrageCale()
                {
                    MesureA = mesureA,
                    MesureB = mesureA,
                    EcartA = ecartA,
                    EcartB = ecartB,
                    CaleId = cale.Id
                });
            }
            erreur.CalibrageCales = verificationCales;
            erreur.Emax = float.Parse(Request.Form["Emax"]);
            erreur.CaleMax = float.Parse(Request.Form["CaleMax"]);
            erreur.Date = DateTime.Now;
            db.ErreurContactPleineTouches.Add(erreur);
            db.SaveChanges();
            return Redirect("/verifications/ErreurContactPartiel/" + erreur.Id);
        }

        public ActionResult ErreurContactPartiel(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ErreurContactPleineTouche erreurContactPleineTouche = db.ErreurContactPleineTouches.Find(id);
            if (erreurContactPleineTouche == null)
            {
                return HttpNotFound();
            }


            return View(erreurContactPleineTouche);
        }

        // GET: Verifications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verification verification = db.Verifications.Find(id);
            if (verification == null)
            {
                return HttpNotFound();
            }
            return View(verification);
        }

        // GET: Verifications/Create
        public ActionResult Create()
        {
            ViewBag.InstrumentsSpecifiques = db.InstrumentSpecifiques.ToList();
            ViewBag.InstrumentSpecifiqueId = new SelectList(db.InstrumentSpecifiques, "Id", "Code");
            return View();
        }

        // POST: Verifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,InstrumentSpecifiqueId")] Verification verification)
        {
            if (ModelState.IsValid)
            {
                db.Verifications.Add(verification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstrumentSpecifiqueId = new SelectList(db.InstrumentSpecifiques, "Id", "Code", verification.InstrumentSpecifiqueId);
            return View(verification);
        }

        // GET: Verifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verification verification = db.Verifications.Find(id);
            if (verification == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstrumentSpecifiqueId = new SelectList(db.InstrumentSpecifiques, "Id", "Code", verification.InstrumentSpecifiqueId);
            return View(verification);
        }

        // POST: Verifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,InstrumentSpecifiqueId")] Verification verification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstrumentSpecifiqueId = new SelectList(db.InstrumentSpecifiques, "Id", "Code", verification.InstrumentSpecifiqueId);
            return View(verification);
        }

        // GET: Verifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verification verification = db.Verifications.Find(id);
            if (verification == null)
            {
                return HttpNotFound();
            }
            return View(verification);
        }

        // POST: Verifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verification verification = db.Verifications.Find(id);
            db.Verifications.Remove(verification);
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
