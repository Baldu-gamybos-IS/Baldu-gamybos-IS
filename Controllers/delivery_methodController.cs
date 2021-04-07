using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using furniture.Models;

namespace furniture.Controllers
{
    public class delivery_methodController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: delivery_method
        public ActionResult Index()
        {
            var delivery_method = db.delivery_method.Include(d => d.generic_order);
            return View(delivery_method.ToList());
        }

        // GET: delivery_method/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivery_method delivery_method = db.delivery_method.Find(id);
            if (delivery_method == null)
            {
                return HttpNotFound();
            }
            return View(delivery_method);
        }

        // GET: delivery_method/Create
        public ActionResult Create()
        {
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment");
            return View();
        }

        // POST: delivery_method/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,volume,fk_order")] delivery_method delivery_method)
        {
            if (ModelState.IsValid)
            {
                db.delivery_method.Add(delivery_method);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", delivery_method.fk_order);
            return View(delivery_method);
        }

        // GET: delivery_method/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivery_method delivery_method = db.delivery_method.Find(id);
            if (delivery_method == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", delivery_method.fk_order);
            return View(delivery_method);
        }

        // POST: delivery_method/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,volume,fk_order")] delivery_method delivery_method)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery_method).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", delivery_method.fk_order);
            return View(delivery_method);
        }

        // GET: delivery_method/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            delivery_method delivery_method = db.delivery_method.Find(id);
            if (delivery_method == null)
            {
                return HttpNotFound();
            }
            return View(delivery_method);
        }

        // POST: delivery_method/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            delivery_method delivery_method = db.delivery_method.Find(id);
            db.delivery_method.Remove(delivery_method);
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
