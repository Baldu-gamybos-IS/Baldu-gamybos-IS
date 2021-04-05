using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Furniture.Models;

namespace Furniture.Controllers
{
    public class payment_typeController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: payment_type
        public ActionResult Index()
        {
            return View(db.payment_type.ToList());
        }

        // GET: payment_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_type payment_type = db.payment_type.Find(id);
            if (payment_type == null)
            {
                return HttpNotFound();
            }
            return View(payment_type);
        }

        // GET: payment_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: payment_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] payment_type payment_type)
        {
            if (ModelState.IsValid)
            {
                db.payment_type.Add(payment_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(payment_type);
        }

        // GET: payment_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_type payment_type = db.payment_type.Find(id);
            if (payment_type == null)
            {
                return HttpNotFound();
            }
            return View(payment_type);
        }

        // POST: payment_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] payment_type payment_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(payment_type);
        }

        // GET: payment_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment_type payment_type = db.payment_type.Find(id);
            if (payment_type == null)
            {
                return HttpNotFound();
            }
            return View(payment_type);
        }

        // POST: payment_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            payment_type payment_type = db.payment_type.Find(id);
            db.payment_type.Remove(payment_type);
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
