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
    public class contract_typeController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: contract_type
        public ActionResult Index()
        {
            return View(db.contract_type.ToList());
        }

        // GET: contract_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract_type contract_type = db.contract_type.Find(id);
            if (contract_type == null)
            {
                return HttpNotFound();
            }
            return View(contract_type);
        }

        // GET: contract_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: contract_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] contract_type contract_type)
        {
            if (ModelState.IsValid)
            {
                db.contract_type.Add(contract_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contract_type);
        }

        // GET: contract_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract_type contract_type = db.contract_type.Find(id);
            if (contract_type == null)
            {
                return HttpNotFound();
            }
            return View(contract_type);
        }

        // POST: contract_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] contract_type contract_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contract_type);
        }

        // GET: contract_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract_type contract_type = db.contract_type.Find(id);
            if (contract_type == null)
            {
                return HttpNotFound();
            }
            return View(contract_type);
        }

        // POST: contract_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contract_type contract_type = db.contract_type.Find(id);
            db.contract_type.Remove(contract_type);
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
