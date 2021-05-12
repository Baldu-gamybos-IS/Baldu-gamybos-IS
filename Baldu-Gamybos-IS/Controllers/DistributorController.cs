using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.EntityFrameworkCore;
using Baldu_Gamybos_IS.Models.ViewModel.DistributorView;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;

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
        [Authorize]
        public IActionResult Distributors()
        {
            if(TempData["SuccessState"]!=null){
                ViewData["SuccessState"]=true;
            }
            
            var distributors = Context.Distributors.Select(r => new Distributor{         
                Id = r.Id,
                Name = r.Name,
                Email=r.Email,
                Phone=r.Phone         
            });
            return View(distributors);
        }
        [Authorize]
        public IActionResult NewDistributor() {
            return this.View("NewDistributor",new Distributor());
        }
        [Authorize]
        public IActionResult CreateDistributor(Distributor newDis) {
            //Validate perhaps
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(newDis.Email);
            if(!match.Success){
                newDis.Email=null;
            }
            if(newDis.Email==null && newDis.Phone==null){
                ViewData["SuccessState"]=1;
                return this.View("NewDistributor",newDis);
            }
            if(!newDis.Name.All(char.IsLetterOrDigit)){
                ViewData["SuccessState"]=2;
                return this.View("NewDistributor",newDis);
            }


            this.Context.Distributors.Add(newDis);
            this.Context.SaveChanges();
            TempData["SuccessState"]=true;
            return this.RedirectToAction("Distributors", "Distributor");
        
        }
    }
}
