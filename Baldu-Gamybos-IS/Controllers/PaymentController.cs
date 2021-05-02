using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.EntityFrameworkCore;

namespace mvc_auth_test.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly furnitureContext Context;

        public PaymentController(ILogger<PaymentController> logger, furnitureContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Payments()
        {
            if(TempData["SuccessState"]!=null){
                ViewData["SuccessState"]=true;
            }
            
            var payments = Context.Payments.Include(h=> h.FkTypeNavigation).Include(l=>l.FkOrderNavigation).Select(r => new Payment{         
                Id = r.Id,
                Amount = r.Amount,
                Direction=r.Direction,
                Time=r.Time,
                FkTypeNavigation=r.FkTypeNavigation,
                FkOrderNavigation=r.FkOrderNavigation
            });
             return View(payments);
        }

        
        public IActionResult NewPayment(Payment making) {
            if(TempData["SuccessState"]!=null){
                switch(TempData["SuccessState"]){
                    case 1:
                        ViewData["ErrorMSG"]="Neegzistuoja toks užsakymas";
                        break;
                    case 2:
                        ViewData["ErrorMSG"]="Nepasirinkote krypties";
                        break;   
                    case 3:
                        ViewData["ErrorMSG"]="Nepasirinkote tipo";
                        break;      
                    default:
                        ViewData["ErrorMSG"]="Unknown problem";
                        break;
                };
                
            }
            if(making.Id==null){
                making=new Payment();
            }
            return this.View("NewPayment",making);
        }
        public IActionResult CreatePayment(Payment newPay,int type) {
            //Validate perhaps
            if (this.Context.GenericOrders.Any(h=>h.Id==newPay.FkOrder)){
                if(type==0){
                    TempData["SuccessState"]=3;
                    return this.RedirectToAction("NewPayment","Payment",newPay);
                }
                var order=this.Context.GenericOrders.Where(h=>h.Id==newPay.FkOrder).Single();
                order.PayedAmount+=newPay.Amount;
                this.Context.GenericOrders.Update(order);
                newPay.Time=DateTime.UtcNow;
                newPay.Direction=order.Direction;
                newPay.FkType=type;
                this.Context.Payments.Add(newPay);
                this.Context.SaveChanges();
                TempData["SuccessState"]=0;
                return this.RedirectToAction("Payments", "Payment");

            }
            else{
                TempData["SuccessState"]=1;
                return this.RedirectToAction("NewPayment","Payment",newPay);
            }       
        }
    }
}
