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
    public class product_resource_transactionController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: product_resource_transaction
        public ActionResult Index()
        {
            var product_resource_transaction = db.product_resource_transaction.Include(p => p.product).Include(p => p.product_resource);
            return View(product_resource_transaction.ToList());
        }

        // GET: product_resource_transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource_transaction product_resource_transaction = db.product_resource_transaction.Find(id);
            if (product_resource_transaction == null)
            {
                return HttpNotFound();
            }
            return View(product_resource_transaction);
        }

        // GET: product_resource_transaction/Create
        public ActionResult Create()
        {
            ViewBag.fk_prod_res = new SelectList(db.products, "id", "id");
            ViewBag.fk_prod_trans = new SelectList(db.product_resource, "id", "id");
            return View();
        }

        // POST: product_resource_transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,initial_amount,fk_prod_res,fk_prod_trans")] product_resource_transaction product_resource_transaction)
        {
            if (ModelState.IsValid)
            {
                db.product_resource_transaction.Add(product_resource_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_prod_res = new SelectList(db.products, "id", "id", product_resource_transaction.fk_prod_res);
            ViewBag.fk_prod_trans = new SelectList(db.product_resource, "id", "id", product_resource_transaction.fk_prod_trans);
            return View(product_resource_transaction);
        }

        // GET: product_resource_transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource_transaction product_resource_transaction = db.product_resource_transaction.Find(id);
            if (product_resource_transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_prod_res = new SelectList(db.products, "id", "id", product_resource_transaction.fk_prod_res);
            ViewBag.fk_prod_trans = new SelectList(db.product_resource, "id", "id", product_resource_transaction.fk_prod_trans);
            return View(product_resource_transaction);
        }

        // POST: product_resource_transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,initial_amount,fk_prod_res,fk_prod_trans")] product_resource_transaction product_resource_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_resource_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_prod_res = new SelectList(db.products, "id", "id", product_resource_transaction.fk_prod_res);
            ViewBag.fk_prod_trans = new SelectList(db.product_resource, "id", "id", product_resource_transaction.fk_prod_trans);
            return View(product_resource_transaction);
        }

        // GET: product_resource_transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_resource_transaction product_resource_transaction = db.product_resource_transaction.Find(id);
            if (product_resource_transaction == null)
            {
                return HttpNotFound();
            }
            return View(product_resource_transaction);
        }

        // POST: product_resource_transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_resource_transaction product_resource_transaction = db.product_resource_transaction.Find(id);
            db.product_resource_transaction.Remove(product_resource_transaction);
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
