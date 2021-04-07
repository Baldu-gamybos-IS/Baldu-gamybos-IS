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
    public class resourcesController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: resources
        public ActionResult Index()
        {
            var resources = db.resources.Include(r => r.m_unit).Include(r => r.resource_transaction);
            return View(resources.ToList());
        }

        // GET: resources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource resource = db.resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: resources/Create
        public ActionResult Create()
        {
            ViewBag.fk_m_unit = new SelectList(db.m_unit, "id", "unit_name");
            ViewBag.fk_res_trans = new SelectList(db.resource_transaction, "id", "id");
            return View();
        }

        // POST: resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,left_amount,unit_price,fk_res_trans,fk_m_unit")] resource resource)
        {
            if (ModelState.IsValid)
            {
                db.resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_m_unit = new SelectList(db.m_unit, "id", "unit_name", resource.fk_m_unit);
            ViewBag.fk_res_trans = new SelectList(db.resource_transaction, "id", "id", resource.fk_res_trans);
            return View(resource);
        }

        // GET: resources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource resource = db.resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_m_unit = new SelectList(db.m_unit, "id", "unit_name", resource.fk_m_unit);
            ViewBag.fk_res_trans = new SelectList(db.resource_transaction, "id", "id", resource.fk_res_trans);
            return View(resource);
        }

        // POST: resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,left_amount,unit_price,fk_res_trans,fk_m_unit")] resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_m_unit = new SelectList(db.m_unit, "id", "unit_name", resource.fk_m_unit);
            ViewBag.fk_res_trans = new SelectList(db.resource_transaction, "id", "id", resource.fk_res_trans);
            return View(resource);
        }

        // GET: resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resource resource = db.resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            resource resource = db.resources.Find(id);
            db.resources.Remove(resource);
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
