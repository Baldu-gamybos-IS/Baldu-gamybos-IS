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
    public class WarehouseController : Controller
    {
        private readonly ILogger<WarehouseController> _logger;
        private readonly furnitureContext Context;

        public WarehouseController(ILogger<WarehouseController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Warehouse()
        {
            var resource = Context.Resources.Select(r => new Resource(r)).ToList();
            var products = Context.Products.Select(p => new ProductView(p, Context.ProductTypes.Where(t => t.Id == p.Id).FirstOrDefault().Name)).ToList();
            // Context.GenericOrders.Where(t => t.Id == p.Id).FirstOrDefault().Id)).ToList();
            var warehouse = new WarehouseView(resource, products);
            return View(warehouse);
        }

        public ActionResult ChangeWarehouse(int id)
        {
            var resource = Context.Resources.Select(t => new Resource());
            ViewData["error"] = 0;
            var res = Context.Resources.Where(c => c.Id == id).FirstOrDefault();
            return View(res);
        }

        [HttpPost]
        public IActionResult ChangeValue(Resource resource){
            ViewData["error"] = 0;

            if(resource.LeftAmount == null)
            {
                ViewData["error"] = 1;
                return  View("ChangeWarehouse", resource);
            }else if(resource.LeftAmount == null){
                ViewData["error"] = 2;
                return  View("ChangeWarehouse", resource);
            }

            if(resource.LeftAmount < 0 && resource.UnitPrice < 0){
                ViewData["error"] = 3;
                return  View("ChangeWarehouse", resource);
            }  
            else if(resource.LeftAmount < 0){            
                ViewData["error"] = 1;
                return  View("ChangeWarehouse", resource);
            }
            else if (resource.UnitPrice < 0)
            {
                ViewData["error"] = 2;
                return  View("ChangeWarehouse", resource);
            }
            ViewData["Success2"] = true;
            Context.Resources.Update(resource);
            Context.SaveChanges(); 
            return  View("ChangeWarehouse", resource);
        }
    }
}
