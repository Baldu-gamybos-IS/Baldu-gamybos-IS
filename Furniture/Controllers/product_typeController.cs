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
    public class product_typeController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: product_type
        public ActionResult Index()
        {
            return View(db.product_type.ToList());
        }

        // GET: product_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            return View(product_type);
        }

        // GET: product_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: product_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,price_multiplier")] product_type product_type)
        {
            if (ModelState.IsValid)
            {
                db.product_type.Add(product_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_type);
        }

        // GET: product_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            return View(product_type);
        }

        // POST: product_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,price_multiplier")] product_type product_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_type);
        }

        // GET: product_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product_type product_type = db.product_type.Find(id);
            if (product_type == null)
            {
                return HttpNotFound();
            }
            return View(product_type);
        }

        // POST: product_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product_type product_type = db.product_type.Find(id);
            db.product_type.Remove(product_type);
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
