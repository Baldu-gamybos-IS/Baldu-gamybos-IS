using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.AspNetCore.Authorization;
using Baldu_Gamybos_IS.Models.ViewModel.ContractView;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace mvc_auth_test.Controllers {
	[Authorize(Roles = "vadybininkas")]
	public class ContractController : Controller {
		private readonly ILogger<ContractController> _logger;
		private readonly furnitureContext Context;

		public ContractController(ILogger<ContractController> logger, furnitureContext context) {
			this._logger = logger;
			this.Context = context;
		}

		[Authorize(Roles = "vadybininkas")]
		public IActionResult Contracts() {
			if (this.TempData["Success"] != null) {
				this.ViewData["Success"] = true;
			}
			return this.View("Contracts", this.Context.Contracts.Include(p => p.FkProfileNavigation).Include(p => p.FkContractTypeNavigation));
		}

		[Authorize(Roles = "vadybininkas")]
		public IActionResult Contract() {
			return this.View("Contract", new ContractView{ Profiles = this.Context.Profiles });
		}

		[Authorize(Roles = "vadybininkas")]
		[HttpPost]
		public IActionResult CreateContract(ContractView view) {
			view.Contract.InitDate = DateTime.UtcNow;
			view.Contract.FkProfile = Int32.Parse(view.Profile);

			ContractType type = this.Context.ContractTypes.FirstOrDefault(t => t.Name == view.ContractType);
			if (type == null) {
				this.Context.ContractTypes.Add(new ContractType{ Name = view.ContractType });
				this.Context.SaveChanges();
				view.Contract.FkContractType = this.Context.ContractTypes.Single(t => t.Name == view.ContractType).Id;
			} else view.Contract.FkContractType = type.Id;
			

			this.Context.Contracts.Add(view.Contract);
			this.Context.SaveChanges();
			this.TempData["Success"] = true;
			return this.RedirectToAction("Contracts", "Contract");
		}
	}
}
