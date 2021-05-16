using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Cryptography;
using Baldu_Gamybos_IS.Models;

namespace mvc_auth_test.Controllers
{
    public class ProfileController : Controller
    {
        const string SIsLoggedIn = "_Name";
        private readonly ILogger<ProfileController> _logger;
        private readonly furnitureContext Context;
        public ProfileController(ILogger<ProfileController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Profile()
        {
            var Profile = Context.Profiles.Select(r => new Profile{         
                Id = r.Id,
                Username = r.Username,
                Password=r.Password,
                FirstName =r.FirstName,
                LastName =r.LastName,
                Email =r.Email,
                Phone =r.Phone,
                Address =r.Address,
                Zipcode =r.Zipcode,
                FkRole =r.FkRole,
            });
            return View(Profile);
        }
        

        private string Sha256(string source)
        {
            using (SHA256 sha = SHA256.Create()){
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(source));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++){
                    sBuilder.Append(data[i].ToString("x2"));
                }
                string hash = sBuilder.ToString();
                return hash;
            }
        }

        [Authorize]
        public ActionResult SignIn()
        {
            return View();
        } 
  
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        } 

        [HttpPost("login")]
        public async Task<IActionResult> ValidateData(string username, string password)
        {
            string hash = Sha256(password);
            IQueryable<Baldu_Gamybos_IS.Models.Profile> profiles = Context.Profiles.Where(s => s.Username == username);
            if(profiles.Any() && profiles.First() != null && profiles.First().Password == hash){
                var role = Context.Roles.Where(s => s.Id==profiles.First().FkRole).First();
                var claims = new List<Claim>();
                claims.Add(new Claim("username", username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                claims.Add(new Claim(ClaimTypes.Role, role != null ? role.Name : "pirkėjas"));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect("Profile/SignIn");
            }
            TempData["Error"] = "Neteisingas vartotojo vardas arba slaptažodis";
            return View("login");
        }

    
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("SignIn");
        }

        [HttpGet("register")]
        public ActionResult Register(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        } 
    
        [HttpPost("register")]
        public async Task<IActionResult> RegisterValidate(
            string firstName, string lastName, string username, string email,
             string phone, string password1, string password2)
        {

            if(firstName != null && firstName.Length > 255){
                TempData["Error"] = "Vardas yra per ilgas";
                return View("register");
            }
            if(firstName != null && lastName.Length > 255){
                TempData["Error"] = "Pavardė yra per ilga";
                return View("register");
            }
            if(username == null || username.Length > 255){
                TempData["Error"] = "Vartotojo vardas yra per ilgas";
                return View("register");
            }
            bool profile_exists = Context.Profiles.Any(s => s.Username == username);
            if(profile_exists){
                TempData["Error"] = "Vartotojas tokiu vartotojo vardu egzistuoja";
                return View("register");
            }
            profile_exists = Context.Profiles.Any(s => s.Email == email);
            if(profile_exists){
                TempData["Error"] = "Vartotojas su tokiu el. paštu egzistuoja";
                return View("register");
            }
            if(email == null || email.Length > 255){
                TempData["Error"] = "El. paštas yra per ilgas";
                return View("register");
            }
            if(password1 == null || password2 == null || password1.Length > 255 || password2.Length > 255){
                TempData["Error"] = "Slaptažodis per ilgas";
                return View("register");
            }
            if(password1 != password2){
                TempData["Error"] = "Slaptažodžiai nesutampa";
                return View("register");
            }
            if(password1.Length < 8){
                TempData["Error"] = "Slaptažodis per trumpas";
                return View("register");
            }
            int capCout = Regex.Matches(password1, "[A-Z]").Count;
            int lowCout = Regex.Matches(password1, "[a-z]").Count;
            int numCout = Regex.Matches(password1, "[0-9]").Count;
            int charCout = Regex.Matches(password1, "[~`!@#$%^&*()_+|\\=;':\"?.>,<]").Count;
            if(capCout == 0 || lowCout == 0 || numCout == 0 || charCout == 0){
                TempData["Error"] = "Į slaptažodį turi įeiti didžiosios, mažosios raidės, skaičiai ir meta simboliai(~`!@#$%^&*()_+|\\=;':\"?.>,<)";
                return View("register");
            }
            Profile profile = new Profile();
            profile.FirstName = firstName;
            profile.LastName = lastName;
            profile.Username = username;
            profile.Password = Sha256(password1);
            profile.Email = email;
            profile.Phone = phone;

            Context.Profiles.Add(profile);
            Context.SaveChanges();

            var claims = new List<Claim>();
            claims.Add(new Claim("username", username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return Redirect("login");
        }


    }
}
