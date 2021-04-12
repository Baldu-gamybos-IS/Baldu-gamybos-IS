using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Baldu_Gamybos_IS.Models.ViewModel.OrderView {
	public class OrderView {
		public readonly GenericOrder Order = new();
		public List<IFormFile> Files { get; set; }

		public OrderView() { }
	}
}
