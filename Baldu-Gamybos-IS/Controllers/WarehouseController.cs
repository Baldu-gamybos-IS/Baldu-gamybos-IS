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

        private class TrEntry
        {
            public double Amount { get; set; }
            public DateTime Time { get; set; }
            public TrEntry(double amount, DateTime time)
            {
                this.Time = time;
                this.Amount = amount;
            }

        }
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
            EstimateResource(1);
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

        public Double EstimateResource(int id)
        {
            var r = Context.Resources.Where(d => d.Id == id).FirstOrDefault();
            Console.Write("Done\n");
            var rt = Context.ResourceTransactions.Where(d => d.FkResource == r.Id).Select(d => new TrEntry((double)d.InitialAmount, (System.DateTime)d.Time)).ToList();
            Console.Write("Done\n");
            var pr = Context.ProductResources.Where(d => d.FkResource == r.Id).ToList();
            foreach(var d in pr){
                Console.Write(d.Id + " ");
            }
            Console.Write("Done\n");
            var p_idx = pr.Select(d => d.FkProduct).ToList();
            foreach(var d in p_idx){
                Console.Write(d + " ");
            }
            Console.Write("Done\n");
            var pt = Context.ProductTransactions.Where(d =>  p_idx.Contains(d.FkProduct)).ToList();
            // var pt = Context.ProductTransactions.Where(d =>  p_idx.Contains(d.FkProduResourcect) == true).ToList();
            var prt_ = Context.ProductResourceTransactions.ToList();
            foreach(var _pr in pr) {
                foreach(var _pt in pt) {
                    var loc_prt = Context.ProductResourceTransactions.Where(d => d.FkProdRes == _pt.Id && d.FkProdTrans == _pr.Id).ToList(); 
                    foreach (var _prt in loc_prt) {
                        rt.Add(new TrEntry((double)_prt.InitialAmount, (System.DateTime)_pt.Time));
                    }
                }
            }
            
            foreach(var d in rt){
                Console.Write(d.Amount + " " + d.Time);
            }
            // var data = Context.Resources.FromSqlRaw("SELECT t.* FROM (SELECT prt.initial_amount, pt.time FROM resource as r INNER JOIN product_resource as pr ON (r.id={0} AND r.id=pr.fk_resource) INNER JOIN product_resource_transaction as prt ON prt.fk_prod_res=pr.id INNER JOIN product_transaction as pt ON pt.id=prt.fk_prod_trans UNION SELECT rt2.initial_amount, rt2.time FROM resource as r2 INNER JOIN resource_transaction as rt2 ON (r2.id={1} AND r2.id=rt2.fk_resource)) as t ORDER BY t.time LIMIT 50", id, id).ToList();
            return 0;
        }

    }
}
