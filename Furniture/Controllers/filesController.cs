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
    public class filesController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: files
        public ActionResult Index()
        {
            var files = db.files.Include(f => f.contract).Include(f => f.generic_order);
            return View(files.ToList());
        }

        // GET: files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: files/Create
        public ActionResult Create()
        {
            ViewBag.fk_contract = new SelectList(db.contracts, "id", "name");
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment");
            return View();
        }

        // POST: files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,path,name,mime,size,fk_order,fk_contract")] file file)
        {
            if (ModelState.IsValid)
            {
                db.files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_contract = new SelectList(db.contracts, "id", "name", file.fk_contract);
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", file.fk_order);
            return View(file);
        }

        // GET: files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_contract = new SelectList(db.contracts, "id", "name", file.fk_contract);
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", file.fk_order);
            return View(file);
        }

        // POST: files/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,path,name,mime,size,fk_order,fk_contract")] file file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_contract = new SelectList(db.contracts, "id", "name", file.fk_contract);
            ViewBag.fk_order = new SelectList(db.generic_order, "id", "comment", file.fk_order);
            return View(file);
        }

        // GET: files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            file file = db.files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            file file = db.files.Find(id);
            db.files.Remove(file);
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
