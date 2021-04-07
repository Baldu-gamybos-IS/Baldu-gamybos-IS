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
    public class product_transactionController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: product_transaction
        public ActionResult Index()
        {
            var product_transaction = db.product_transaction.Include(p => p.product).Include(p => p.resource);
            return View(product_transaction.ToList());
        }

        // GET: product_transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_transaction product_transaction = db.product_transaction.Find(id);
            if (product_transaction == null)
            {
                return HttpNotFound();
            }
            return View(product_transaction);
        }

        // GET: product_transaction/Create
        public ActionResult Create()
        {
            ViewBag.fk_product = new SelectList(db.products, "id", "id");
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id");
            return View();
        }

        // POST: product_transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,direction,fk_product,fk_resource")] product_transaction product_transaction)
        {
            if (ModelState.IsValid)
            {
                db.product_transaction.Add(product_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_transaction.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_transaction.fk_resource);
            return View(product_transaction);
        }

        // GET: product_transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_transaction product_transaction = db.product_transaction.Find(id);
            if (product_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_transaction.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_transaction.fk_resource);
            return View(product_transaction);
        }

        // POST: product_transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,direction,fk_product,fk_resource")] product_transaction product_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_product = new SelectList(db.products, "id", "id", product_transaction.fk_product);
            ViewBag.fk_resource = new SelectList(db.resources, "id", "id", product_transaction.fk_resource);
            return View(product_transaction);
        }

        // GET: product_transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_transaction product_transaction = db.product_transaction.Find(id);
            if (product_transaction == null)
            {
                return HttpNotFound();
            }
            return View(product_transaction);
        }

        // POST: product_transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_transaction product_transaction = db.product_transaction.Find(id);
            db.product_transaction.Remove(product_transaction);
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
