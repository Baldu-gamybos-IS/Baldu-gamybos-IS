using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.EntityFrameworkCore;
using Baldu_Gamybos_IS.Models.ViewModel.DistributorView;

namespace mvc_auth_test.Controllers
{
    public class DistributorController : Controller
    {
        private readonly ILogger<DistributorController> _logger;
        private readonly furnitureContext Context;

        public DistributorController(ILogger<DistributorController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Distributors()
        {
            if(TempData["SuccessState"]!=null){
                ViewData["SuccessState"]=true;
            }
            
            var distributors = Context.Distributors.Select(r => new Distributor{         
                Id = r.Id,
                Name = r.Name         
            });
            return View(distributors);
        }

        public IActionResult NewDistributor() {
            return this.View("NewDistributor",new Distributor());
        }
        public IActionResult CreateDistributor(Distributor newDis) {
            //Validate perhaps

            this.Context.Distributors.Add(newDis);
            this.Context.SaveChanges();
            TempData["SuccessState"]=true;
            return this.RedirectToAction("Distributors", "Distributor");
        
        }
    }
}
