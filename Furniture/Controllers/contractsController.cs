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
    public class contractsController : Controller
    {
        private furnitureEntities db = new furnitureEntities();

        // GET: contracts
        public ActionResult Index()
        {
            var contracts = db.contracts.Include(c => c.profile).Include(c => c.contract_type);
            return View(contracts.ToList());
        }

        // GET: contracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // GET: contracts/Create
        public ActionResult Create()
        {
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username");
            ViewBag.fk_contract_type = new SelectList(db.contract_type, "id", "name");
            return View();
        }

        // POST: contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,path,start_date,end_date,init_date,fk_contract_type,fk_profile")] contract contract)
        {
            if (ModelState.IsValid)
            {
                db.contracts.Add(contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", contract.fk_profile);
            ViewBag.fk_contract_type = new SelectList(db.contract_type, "id", "name", contract.fk_contract_type);
            return View(contract);
        }

        // GET: contracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", contract.fk_profile);
            ViewBag.fk_contract_type = new SelectList(db.contract_type, "id", "name", contract.fk_contract_type);
            return View(contract);
        }

        // POST: contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,path,start_date,end_date,init_date,fk_contract_type,fk_profile")] contract contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_profile = new SelectList(db.profiles, "id", "username", contract.fk_profile);
            ViewBag.fk_contract_type = new SelectList(db.contract_type, "id", "name", contract.fk_contract_type);
            return View(contract);
        }

        // GET: contracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contract contract = db.contracts.Find(id);
            if (contract == null)
            {
                return HttpNotFound();
            }
            return View(contract);
        }

        // POST: contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contract contract = db.contracts.Find(id);
            db.contracts.Remove(contract);
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
