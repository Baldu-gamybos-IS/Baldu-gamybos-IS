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
    public class m_unitController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: m_unit
        public ActionResult Index()
        {
            return View(db.m_unit.ToList());
        }

        // GET: m_unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_unit m_unit = db.m_unit.Find(id);
            if (m_unit == null)
            {
                return HttpNotFound();
            }
            return View(m_unit);
        }

        // GET: m_unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_unit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,unit_name,unit")] m_unit m_unit)
        {
            if (ModelState.IsValid)
            {
                db.m_unit.Add(m_unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_unit);
        }

        // GET: m_unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_unit m_unit = db.m_unit.Find(id);
            if (m_unit == null)
            {
                return HttpNotFound();
            }
            return View(m_unit);
        }

        // POST: m_unit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,unit_name,unit")] m_unit m_unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_unit);
        }

        // GET: m_unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_unit m_unit = db.m_unit.Find(id);
            if (m_unit == null)
            {
                return HttpNotFound();
            }
            return View(m_unit);
        }

        // POST: m_unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_unit m_unit = db.m_unit.Find(id);
            db.m_unit.Remove(m_unit);
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
