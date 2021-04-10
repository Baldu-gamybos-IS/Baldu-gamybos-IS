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
        public IActionResult OrderInfo(int id){
            
            var orderinfo=Context.GenericOrders.Select(r=> new GenericOrder{
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
                FkStatusNavigation=r.FkStatusNavigation,
                FkDistributorNavigation=r.FkDistributorNavigation,
                FkProfileNavigation=r.FkProfileNavigation,
                FkSupplierNavigation=r.FkSupplierNavigation,
                Products=r.Products,
                OrderResources=r.OrderResources,
                

            });
            var ourOrder=orderinfo.Where(r => r.Id==id);
            return View(ourOrder);
        }


            
    }
}
