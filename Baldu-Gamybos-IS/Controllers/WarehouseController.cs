﻿using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Baldu_Gamybos_IS.Models.ViewModel.WarehouseView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace mvc_auth_test.Controllers
{
    [Authorize(Roles = "darbuotojas,vadybininkas")]
    public class WarehouseController : Controller
    {

        private class TrEntry
        {
            public double Amount { get; set; }
            public long Time { get; set; }
            public TrEntry(double amount, DateTime time)
            {

                this.Time = ConvertDatetimeToUnixTimeStamp(time);
                this.Amount = amount;
            }

        }
        private class Vector
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double LastX { get; set; }
            public double LastY { get; set; }
            public bool LastDesc { get; set; }
            
            public Vector()
            {
                this.X = 0.0;
                this.Y = 0.0;
                this.LastX = 0.0;
                this.LastY = 0.0;
            }
            
            public void addDesc(double x, double y)
            {
                if (this.LastY < y) {
                    LastY = y;
                    LastX = x;
                } else {
                    double vectX = x;
                    double vectY = y;
                    vectorize(ref vectX, ref vectY);
                    LastY = y;
                    LastX = x;
                    add(vectX, vectY);
                }
            }
            public void add(double x, double y)
            {
                this.X += x;
                this.Y += y;
            }
            public void vectorize(ref double x, ref double y)
            {
                x -= this.LastX;
                y -= this.LastY;
            }

        }
        private readonly ILogger<WarehouseController> _logger;
        private readonly furnitureContext Context;

        public WarehouseController(ILogger<WarehouseController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        [Authorize]
        public IActionResult Warehouse()
        {
            var resource = Context.Resources.Select(r => new Resource(r)).ToList();
            List<EstResource> estResource = new List<EstResource>();
            foreach(var res in resource){
                double est = ApproximateResources(res.Id);
                EstResource er = new EstResource(res, est);
                estResource.Add(er);
            }
            var products = Context.Products.Include(t => t.FkProductTypeNavigation).Select(p => new ProductView(p, p.FkProductTypeNavigation.Name)).ToList();
            var warehouse = new WarehouseView(estResource, products);
            return View(warehouse);
        }
        [Authorize]
        public ActionResult ChangeWarehouseResource(int id)
        {
            ViewData["error"] = 0;
            var res = Context.Resources.Where(c => c.Id == id).FirstOrDefault();
            return View(res);
        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangeValueResource(Resource resource){
            ViewData["error"] = 0;

            if(resource.LeftAmount == null)
            {
                ViewData["error"] = 1;
                return  View("ChangeWarehouseResource", resource);
            }else if(resource.LeftAmount == null){
                ViewData["error"] = 2;
                return  View("ChangeWarehouseResource", resource);
            }

            if(resource.LeftAmount < 0 && resource.UnitPrice < 0){
                ViewData["error"] = 3;
                return  View("ChangeWarehouseResource", resource);
            }  
            else if(resource.LeftAmount < 0){            
                ViewData["error"] = 1;
                return  View("ChangeWarehouseResource", resource);
            }
            else if (resource.UnitPrice < 0)
            {
                ViewData["error"] = 2;
                return  View("ChangeWarehouseResource", resource);
            }
            ViewData["Success2"] = true;
            Context.Resources.Update(resource);
            Context.SaveChanges(); 
            return  View("ChangeWarehouseResource", resource);
        }

       [Authorize]
        public ActionResult ChangeWarehouseProduct(int id)
        {
            ViewData["error"] = 0;
            var product = Context.Products.Where(t => t.Id == id)
                                          .Include(t => t.FkProductTypeNavigation)
                                          .Select(p => new ProductView(p, p.FkProductTypeNavigation.Name))
                                          .FirstOrDefault();
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeValueProduct(ProductView product){
            ViewData["error"] = 0;
            if(product.Count == null)
            {
                ViewData["error"] = 1;
                return  View("ChangeWarehouseProduct", product);
            }else if(product.Count == null){
                ViewData["error"] = 2;
                return  View("ChangeWarehouseProduct", product);
            }

            if(product.Count < 0 && product.SinglePrice < 0){
                ViewData["error"] = 3;
                return  View("ChangeWarehouseProduct", product);
            }  
            else if(product.Count < 0){            
                ViewData["error"] = 1;
                return  View("ChangeWarehouseProduct", product);
            }
            else if (product.SinglePrice < 0)
            {
                ViewData["error"] = 2;
                return  View("ChangeWarehouseProduct", product);
            }
            ViewData["Success2"] = true;
            var prod = Context.Products.FirstOrDefault(p => p.Id == product.Id);
            prod.Count = product.Count;
            prod.SinglePrice = product.SinglePrice;
            Context.Products.Update(prod);
            Context.SaveChanges();

            return  View("ChangeWarehouseProduct", product);
        }
        
        public static long ConvertDatetimeToUnixTimeStamp(DateTime date)
        {
            var dateTimeOffset = new DateTimeOffset(date);
            var unixDateTime = dateTimeOffset.ToUnixTimeSeconds();
            return unixDateTime;
        }
        public Double ApproximateResources(int id)
        {
            long day = 86400; //day in seconds
            long days30 = day * 30; //30 days in seconds

            var r = Context.Resources.Where(d => d.Id == id).FirstOrDefault();
            var rt = Context.ResourceTransactions.Where(d => d.FkResource == r.Id).Select(d => new TrEntry((double)d.InitialAmount, (System.DateTime)d.Time)).ToList();
            var pr = Context.ProductResources.Where(d => d.FkResource == r.Id).ToList();
            var p_idx = pr.Select(d => d.FkProduct).ToList();
            var pt = Context.ProductTransactions.Where(d =>  p_idx.Contains(d.FkProduct)).ToList();
            foreach(var _pr in pr) {
                foreach(var _pt in pt) {
                    var loc_prt = Context.ProductResourceTransactions.Where(d => d.FkProdRes == _pt.Id && d.FkProdTrans == _pr.Id).ToList(); 
                    foreach (var _prt in loc_prt) {
                        rt.Add(new TrEntry((double)_prt.InitialAmount, (System.DateTime)_pt.Time));
                    }
                }
            }
            long days30Ago = ConvertDatetimeToUnixTimeStamp(DateTime.Now) - days30;
            rt = rt.Where(d => d.Time > days30Ago).ToList();
            Vector v = new Vector();
            foreach(var d in rt){
                v.addDesc((double)d.Time, d.Amount);
            }
            double left = Math.Round(-v.LastY / (v.Y /v.X) / day, 2);
            return left;
        }

        [Authorize(Roles = "vadybininkas")]
        public IActionResult NewResource() {
            return this.View("NewResource", new Resource());
        }

        [Authorize(Roles = "vadybininkas")]
        [HttpPost]
        public IActionResult CreateResource(Resource resource) {
            this.TempData["SuccessfullyCreatedResource"] = true;

            this.Context.Resources.Add(resource);
            this.Context.SaveChanges();
            return this.RedirectToAction("Warehouse", "Warehouse");
        }
    }
}
