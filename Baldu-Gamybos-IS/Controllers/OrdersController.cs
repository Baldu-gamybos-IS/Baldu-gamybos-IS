using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.EntityFrameworkCore;

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
            if(TempData["SuccessState"]!=null){
                ViewData["SuccessState"]=true;
            }
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
            var checking=Context.GenericOrders.Include(h=> h.OrderResources).
            ThenInclude(g=>g.FkResourceNavigation).
            Include(b=>b.Products).
            ThenInclude(c=>c.ProductResources).ThenInclude(d=>d.FkResourceNavigation).Select(r=> new GenericOrder{
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
           
            var ourOrder=checking.Where(r => r.Id==id).Single();
            return View(ourOrder);
        }
        public IActionResult OrderState(int id){
            var checking=Context.GenericOrders.Include(h=> h.OrderResources).
            ThenInclude(g=>g.FkResourceNavigation).
            Include(b=>b.Products).
            ThenInclude(c=>c.ProductResources).ThenInclude(d=>d.FkResourceNavigation).Select(r=> new GenericOrder{
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
           
            var ourOrder=checking.Where(r => r.Id==id).Single();
            return View(ourOrder);
        }
        public IActionResult ChangeState(int id,int state){
            var obj=Context.GenericOrders.Where(r=>r.Id==id).Single();
            obj.FkStatus=state;
            TempData["SuccessState"]=true;

            Context.GenericOrders.Update(obj);
            Context.SaveChanges();
            return RedirectToAction("Orders","Orders");
        }

        public IActionResult Order() {
            return this.View("Order", new GenericOrder());
        }

        [HttpPost]
        public IActionResult CreateOrder(GenericOrder order) {
            order.Direction = true;
            order.PayedAmount = 0.0f;
            order.FkStatusNavigation = this.Context.OrderStatuses.Find(1);
            order.InitDate = DateTime.UtcNow;

            this.TempData["SuccessState"] = true;
            this.Context.GenericOrders.Add(order);
            this.Context.SaveChanges();
            return this.RedirectToAction("Orders", "Orders");
        }
    }
}
