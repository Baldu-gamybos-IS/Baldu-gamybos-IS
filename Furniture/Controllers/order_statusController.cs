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
    public class order_statusController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: order_status
        public ActionResult Index()
        {
            return View(db.order_status.ToList());
        }

        // GET: order_status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_status order_status = db.order_status.Find(id);
            if (order_status == null)
            {
                return HttpNotFound();
            }
            return View(order_status);
        }

        // GET: order_status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: order_status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] order_status order_status)
        {
            if (ModelState.IsValid)
            {
                db.order_status.Add(order_status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_status);
        }

        // GET: order_status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_status order_status = db.order_status.Find(id);
            if (order_status == null)
            {
                return HttpNotFound();
            }
            return View(order_status);
        }

        // POST: order_status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] order_status order_status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_status);
        }

        // GET: order_status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_status order_status = db.order_status.Find(id);
            if (order_status == null)
            {
                return HttpNotFound();
            }
            return View(order_status);
        }

        // POST: order_status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_status order_status = db.order_status.Find(id);
            db.order_status.Remove(order_status);
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
