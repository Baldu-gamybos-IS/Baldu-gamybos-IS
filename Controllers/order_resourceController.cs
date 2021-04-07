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
    public class order_resourceController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: order_resource
        public ActionResult Index()
        {
            var order_resource = db.order_resource.Include(o => o.generic_order).Include(o => o.resource);
            return View(order_resource.ToList());
        }

        // GET: order_resource/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_resource order_resource = db.order_resource.Find(id);
            if (order_resource == null)
            {
                return HttpNotFound();
            }
            return View(order_resource);
        }

        // GET: order_resource/Create
        public ActionResult Create()
        {
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment");
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id");
            return View();
        }

        // POST: order_resource/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,amount,fk_resource,fk_order")] order_resource order_resource)
        {
            if (ModelState.IsValid)
            {
                db.order_resource.Add(order_resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", order_resource.fk_order);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", order_resource.fk_resource);
            return View(order_resource);
        }

        // GET: order_resource/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_resource order_resource = db.order_resource.Find(id);
            if (order_resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", order_resource.fk_order);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", order_resource.fk_resource);
            return View(order_resource);
        }

        // POST: order_resource/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,amount,fk_resource,fk_order")] order_resource order_resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", order_resource.fk_order);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", order_resource.fk_resource);
            return View(order_resource);
        }

        // GET: order_resource/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_resource order_resource = db.order_resource.Find(id);
            if (order_resource == null)
            {
                return HttpNotFound();
            }
            return View(order_resource);
        }

        // POST: order_resource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_resource order_resource = db.order_resource.Find(id);
            db.order_resource.Remove(order_resource);
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
