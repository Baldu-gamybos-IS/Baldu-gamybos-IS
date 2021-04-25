using Baldu_Gamybos_IS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Baldu_Gamybos_IS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly furnitureContext Context;

        public HomeController(ILogger<HomeController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            // Role role = Context.Roles.Where(s => s.Id == 3).FirstOrDefault();
            return Redirect("login");
            // return View(role);
        }

        // public IActionResult Privacy()
        // {
        //     return View();
        // }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
