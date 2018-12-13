using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vestforsyningen.Models;

namespace Vestforsyningen.Controllers
{
    public class HomeController : Controller
    {
        private VestforsyningEntities1 db = new VestforsyningEntities1();

        // GET: Home
        public ActionResult Index(string SearchString)
        {
            var opgavers = db.Opgavers.Include(o => o.Kategori).Include(o => o.status);
            if (!String.IsNullOrEmpty(SearchString))
            {
                var search = opgavers.Where(s => s.addresse.Contains(SearchString));
                return View(search.ToList());
            }
            else
            {
                return View(opgavers.ToList().OrderByDescending(x => x.dato));
            }
            
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgaver opgaver = db.Opgavers.Find(id);
            if (opgaver == null)
            {
                return HttpNotFound();
            }
            return View(opgaver);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.fk_kategori = new SelectList(db.Kategoris, "katid", "kategori1");
            ViewBag.fk_stauts = new SelectList(db.status, "statusid", "status1");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,addresse,dato,beskrivelse,udføres,jobnr,fk_stauts,fk_kategori")] Opgaver opgaver)
        {
            if (ModelState.IsValid)
            {
                db.Opgavers.Add(opgaver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_kategori = new SelectList(db.Kategoris, "katid", "kategori1", opgaver.fk_kategori);
            ViewBag.fk_stauts = new SelectList(db.status, "statusid", "status1", opgaver.fk_stauts);
            return View(opgaver);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgaver opgaver = db.Opgavers.Find(id);
            if (opgaver == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_kategori = new SelectList(db.Kategoris, "katid", "kategori1", opgaver.fk_kategori);
            ViewBag.fk_stauts = new SelectList(db.status, "statusid", "status1", opgaver.fk_stauts);
            return View(opgaver);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,addresse,dato,beskrivelse,udføres,jobnr,fk_stauts,fk_kategori")] Opgaver opgaver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opgaver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_kategori = new SelectList(db.Kategoris, "katid", "kategori1", opgaver.fk_kategori);
            ViewBag.fk_stauts = new SelectList(db.status, "statusid", "status1", opgaver.fk_stauts);
            return View(opgaver);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opgaver opgaver = db.Opgavers.Find(id);
            if (opgaver == null)
            {
                return HttpNotFound();
            }
            return View(opgaver);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opgaver opgaver = db.Opgavers.Find(id);
            db.Opgavers.Remove(opgaver);
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
