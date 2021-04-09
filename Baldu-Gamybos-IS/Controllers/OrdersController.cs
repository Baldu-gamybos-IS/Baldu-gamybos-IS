using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;

namespace mvc_auth_test.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly furnitureContext Context;

        public OrdersController(ILogger<OrdersController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Orders()
        {
            var orders = Context.GenericOrders.Select(r => new GenericOrder{         
                Id = r.Id,
                Price = r.Price,
                PayedAmount=r.PayedAmount,
                Deadline =r.Deadline,
                Direction =r.Direction,
                Comment =r.Comment,
                InitDate =r.InitDate,
                DestAddr =r.DestAddr,
                DestZipcode =r.DestZipcode,
                FkStatus=r.FkStatus,
                FkProfile=r.FkProfile,
                FkDistributor=r.FkDistributor,
                FkSupplier =r.FkSupplier,
                FkStatusNavigation=r.FkStatusNavigation
            });
            return View(orders);
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
