using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Baldu_Gamybos_IS.Models.ViewModel.ContractView {
	public class ContractView {
		public readonly IEnumerable<Profile> profiles;
		public readonly Contract contract = new();
		public List<IFormFile> Files { get; set; }
		public String ContractType { get; set; }
		public String Profile { get; set; }

		public ContractView() {}

		public ContractView(IEnumerable<Profile> profiles) {
			this.profiles = profiles;
		}
	}
}
