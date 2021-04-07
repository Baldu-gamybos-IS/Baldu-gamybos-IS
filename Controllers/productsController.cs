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
    public class productsController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.generic_order).Include(p => p.product_type);
            return View(products.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment");
            ViewBag.fk_product_type = new SelectList(db.product_type, "id", "name");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,single_price,count,part_count,est_single_cost,fk_order,fk_product_type")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", product.fk_order);
            ViewBag.fk_product_type = new SelectList(db.product_type, "id", "name", product.fk_product_type);
            return View(product);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", product.fk_order);
            ViewBag.fk_product_type = new SelectList(db.product_type, "id", "name", product.fk_product_type);
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,single_price,count,part_count,est_single_cost,fk_order,fk_product_type")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", product.fk_order);
            ViewBag.fk_product_type = new SelectList(db.product_type, "id", "name", product.fk_product_type);
            return View(product);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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
