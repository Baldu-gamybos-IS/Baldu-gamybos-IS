using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Baldu_Gamybos_IS.Models;
using Microsoft.AspNetCore.Authorization;
using Baldu_Gamybos_IS.Models.ViewModel.ContractView;
using System;

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
			return this.View("Contracts", this.Context.Contracts);
		}

		[Authorize(Roles = "vadybininkas")]
		public IActionResult Contract() {
			return this.View("Contract", new ContractView(this.Context.Profiles));
		}

		[Authorize(Roles = "vadybininkas")]
		[HttpPost]
		public IActionResult CreateContract(ContractView view) {
			view.Contract.InitDate = DateTime.UtcNow;
			if(view.Profile != null) view.Contract.FkProfile = Int32.Parse(view.Profile);

			this.Context.Contracts.Add(view.Contract);
			this.Context.SaveChanges();
			this.TempData["Success"] = true;
			return this.RedirectToAction("Contracts", "Contract");
		}
	}
}
