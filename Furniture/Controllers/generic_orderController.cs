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
    public class generic_orderController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: generic_order
        public ActionResult Index()
        {
            var generic_order = db.generic_order.Include(g => g.distributor).Include(g => g.supplier).Include(g => g.order_status).Include(g => g.profile);
            return View(generic_order.ToList());
        }

        // GET: generic_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            generic_order generic_order = db.generic_order.Find(id);
            if (generic_order == null)
            {
                return HttpNotFound();
            }
            return View(generic_order);
        }

        // GET: generic_order/Create
        public ActionResult Create()
        {
            ViewBag.fk_distributor = new SelectList(db.distributors, "id", "name");
            ViewBag.fk_supplier = new SelectList(db.suppliers, "id", "name");
            ViewBag.fk_status = new SelectList(db.order_status, "id", "name");
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username");
            return View();
        }

        // POST: generic_order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,price,payed_amount,deadline,direction,comment,init_date,dest_addr,dest_zipcode,fk_status,fk_profile,fk_distributor,fk_supplier")] generic_order generic_order)
        {
            if (ModelState.IsValid)
            {
                db.generic_order.Add(generic_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_distributor = new SelectList(db.distributors, "id", "name", generic_order.fk_distributor);
            ViewBag.fk_supplier = new SelectList(db.suppliers, "id", "name", generic_order.fk_supplier);
            ViewBag.fk_status = new SelectList(db.order_status, "id", "name", generic_order.fk_status);
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", generic_order.fk_profile);
            return View(generic_order);
        }

        // GET: generic_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            generic_order generic_order = db.generic_order.Find(id);
            if (generic_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_distributor = new SelectList(db.distributors, "id", "name", generic_order.fk_distributor);
            ViewBag.fk_supplier = new SelectList(db.suppliers, "id", "name", generic_order.fk_supplier);
            ViewBag.fk_status = new SelectList(db.order_status, "id", "name", generic_order.fk_status);
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", generic_order.fk_profile);
            return View(generic_order);
        }

        // POST: generic_order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,price,payed_amount,deadline,direction,comment,init_date,dest_addr,dest_zipcode,fk_status,fk_profile,fk_distributor,fk_supplier")] generic_order generic_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generic_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_distributor = new SelectList(db.distributors, "id", "name", generic_order.fk_distributor);
            ViewBag.fk_supplier = new SelectList(db.suppliers, "id", "name", generic_order.fk_supplier);
            ViewBag.fk_status = new SelectList(db.order_status, "id", "name", generic_order.fk_status);
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", generic_order.fk_profile);
            return View(generic_order);
        }

        // GET: generic_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            generic_order generic_order = db.generic_order.Find(id);
            if (generic_order == null)
            {
                return HttpNotFound();
            }
            return View(generic_order);
        }

        // POST: generic_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            generic_order generic_order = db.generic_order.Find(id);
            db.generic_order.Remove(generic_order);
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
