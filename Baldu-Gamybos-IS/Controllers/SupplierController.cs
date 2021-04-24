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
            var suppliers = Context.Suppliers.Where(s => s.Remove == false).Select(s => new Supplier(s)).ToList();
            return View(suppliers);
        } 

        public IActionResult RemoveSupplier(int id)
        {
            var supplier = Context.Suppliers.FirstOrDefault(s => s.Remove == false);
            supplier.Remove = !supplier.Remove;
            Context.Suppliers.Update(supplier);
            Context.SaveChanges();
            return RedirectToAction("Supplier");
        }   
    }
}
