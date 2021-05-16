using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Baldu_Gamybos_IS.Models.ViewModel.ContractView {
	public class ContractView {
		public Contract Contract { get; set; }
		public IEnumerable<Profile> Profiles { get; set; }
		public List<IFormFile> Files { get; set; }
		public String ContractType { get; set; }
		public String Profile { get; set; }

		public ContractView() { }
	}
}
