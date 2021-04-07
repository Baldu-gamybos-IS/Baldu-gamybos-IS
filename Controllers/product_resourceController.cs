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
    public class product_resourceController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: product_resource
        public ActionResult Index()
        {
            var product_resource = db.product_resource.Include(p => p.product).Include(p => p.resource);
            return View(product_resource.ToList());
        }

        // GET: product_resource/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource product_resource = db.product_resource.Find(id);
            if (product_resource == null)
            {
                return HttpNotFound();
            }
            return View(product_resource);
        }

        // GET: product_resource/Create
        public ActionResult Create()
        {
            ViewBag.fk_product = new SelectList(db.products, "id", "id");
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id");
            return View();
        }

        // POST: product_resource/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,amount,fk_product,fk_resource")] product_resource product_resource)
        {
            if (ModelState.IsValid)
            {
                db.product_resource.Add(product_resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_resource.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_resource.fk_resource);
            return View(product_resource);
        }

        // GET: product_resource/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource product_resource = db.product_resource.Find(id);
            if (product_resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_resource.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_resource.fk_resource);
            return View(product_resource);
        }

        // POST: product_resource/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,amount,fk_product,fk_resource")] product_resource product_resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_resource.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_resource.fk_resource);
            return View(product_resource);
        }

        // GET: product_resource/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource product_resource = db.product_resource.Find(id);
            if (product_resource == null)
            {
                return HttpNotFound();
            }
            return View(product_resource);
        }

        // POST: product_resource/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_resource product_resource = db.product_resource.Find(id);
            db.product_resource.Remove(product_resource);
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
