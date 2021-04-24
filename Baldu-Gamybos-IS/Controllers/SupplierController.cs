using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Baldu_Gamybos_IS.Models.ViewModel.WarehouseView;
using Microsoft.EntityFrameworkCore;

namespace mvc_auth_test.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly furnitureContext Context;

        public SupplierController(ILogger<WarehouseController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Supplier()
        {
            if(TempData["Success"]!=null){
                ViewData["Success"]=TempData["Success"];
            }
            var suppliers = Context.Suppliers.Where(s => s.Remove == false).Select(s => new Supplier(s)).ToList();
            return View(suppliers);
        } 

        public IActionResult RemoveSupplier(int id)
        {
            var supplier = Context.Suppliers.FirstOrDefault(s => s.Id == id);
            supplier.Remove = !supplier.Remove;
            Context.Suppliers.Update(supplier);
            Context.SaveChanges();
            TempData["Success"]="Tiekėjas pašalintas!";
            return RedirectToAction("Supplier");
        }   
        public IActionResult EditSupplier(int id)
        {
            var supplier = Context.Suppliers.FirstOrDefault(s => s.Id == id);
            return View("SupplierManagementWindowEdit", supplier);
        }

        public IActionResult ValidateInput(Supplier supplier)
        {
            if(supplier.Name == null)
            {
                return  View("SupplierManagementWindowEdit", supplier);                
            }
            var name = Context.Suppliers.SingleOrDefault(s => s.Name == supplier.Name);
            if(name != null && name.Name == supplier.Name){
                ModelState.AddModelError("Name", "Toks tiekėjas jau yra!");
                return  View("SupplierManagementWindowEdit", supplier);
            }
            Context.Suppliers.Update(supplier);
            Context.SaveChanges();
            ViewData["Success2"] = true;
            return  View("SupplierManagementWindowEdit", supplier);
        }

        public IActionResult SupplierManagementWindowCreate()
        {
            var supplier = new Supplier();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if(supplier.Name == null)
            {
                return View("SupplierManagementWindowCreate", supplier);
            }
            var sp = Context.Suppliers.SingleOrDefault(s => s.Name == supplier.Name);
            if(sp != null && sp.Name == supplier.Name && sp.Remove == true)
            {
                if(sp.Name == supplier.Name && sp.Remove == false)
                {
                    ModelState.AddModelError("Name", "Toks tiekėjas jau yra sukurtas!");
                    return  View("SupplierManagementWindowCreate", supplier);
                }
                else if(sp.Remove == true)
                {
                    sp.Remove = false;
                    Context.Suppliers.Update(sp);
                    Context.SaveChanges();
                    TempData["Success"]="Naujas tiekėjas sukurtas!";
                    return RedirectToAction("Supplier");
                }
            }
            Context.Suppliers.Add(supplier);
            Context.SaveChanges();
            TempData["Success"]="Naujas tiekėjas sukurtas!";
            return RedirectToAction("Supplier");
        }
    }
}
