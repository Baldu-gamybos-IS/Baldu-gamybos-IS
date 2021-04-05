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
    public class resource_transactionController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: resource_transaction
        public ActionResult Index()
        {
            return View(db.resource_transaction.ToList());
        }

        // GET: resource_transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource_transaction resource_transaction = db.resource_transaction.Find(id);
            if (resource_transaction == null)
            {
                return HttpNotFound();
            }
            return View(resource_transaction);
        }

        // GET: resource_transaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: resource_transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,direction,amount,initial_amount,time")] resource_transaction resource_transaction)
        {
            if (ModelState.IsValid)
            {
                db.resource_transaction.Add(resource_transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resource_transaction);
        }

        // GET: resource_transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource_transaction resource_transaction = db.resource_transaction.Find(id);
            if (resource_transaction == null)
            {
                return HttpNotFound();
            }
            return View(resource_transaction);
        }

        // POST: resource_transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,direction,amount,initial_amount,time")] resource_transaction resource_transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource_transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource_transaction);
        }

        // GET: resource_transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource_transaction resource_transaction = db.resource_transaction.Find(id);
            if (resource_transaction == null)
            {
                return HttpNotFound();
            }
            return View(resource_transaction);
        }

        // POST: resource_transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            resource_transaction resource_transaction = db.resource_transaction.Find(id);
            db.resource_transaction.Remove(resource_transaction);
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
