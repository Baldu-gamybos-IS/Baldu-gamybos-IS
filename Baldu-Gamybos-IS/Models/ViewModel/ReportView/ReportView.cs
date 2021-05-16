
using System.Collections.Generic;

namespace Baldu_Gamybos_IS.Models.ViewModel.ReportView {
	public class ReportView {
		public IEnumerable<GenericOrder> Orders { get; set; }
		public IEnumerable<OrderStatus> Statuses { get; set; }

		public ReportView() { }
	}
}
