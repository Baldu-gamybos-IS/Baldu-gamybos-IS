using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.EntityFrameworkCore;
using Baldu_Gamybos_IS.Models.ViewModel.OrderView;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public IActionResult Orders()
        {
            if(TempData["SuccessState"]!=null){
                ViewData["SuccessState"]=true;
            }
            if (TempData["SuccessfullyCreatedOrder"] != null) {
                ViewData["SuccessfullyCreatedOrder"] = true;
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
        [Authorize]
        public IActionResult ShowOrder(int id){
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
        [Authorize]
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
        [Authorize]
        public IActionResult ChangeState(int id,int state){
            var obj=Context.GenericOrders.Where(r=>r.Id==id).Single();
            obj.FkStatus=state;
            TempData["SuccessState"]=true;

            Context.GenericOrders.Update(obj);
            Context.SaveChanges();
            return RedirectToAction("Orders","Orders");
        }
        [Authorize]
        public IActionResult Order() {
            return this.View("Order", new OrderView());
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(OrderView view) {
            view.Order.Direction = false;
            view.Order.PayedAmount = 0.0f;
            view.Order.FkStatusNavigation = this.Context.OrderStatuses.Find(1);
            view.Order.InitDate = DateTime.UtcNow;
            //view.Files?.ForEach(file => view.Order.Files.Add(new(file, view.Order)));

            this.TempData["SuccessfullyCreatedOrder"] = true;
            //this.Context.Files.AddRange(view.Order.Files);
            this.Context.GenericOrders.Add(view.Order);
            this.Context.SaveChanges();
            return this.RedirectToAction("Orders", "Orders");
        }
    }
}
