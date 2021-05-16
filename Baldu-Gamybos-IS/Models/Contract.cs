using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models {
	public partial class Contract {
		public Contract() {
			this.Files = new HashSet<File>();
		}

		public Contract(Contract c) {
			this.Id = c.Id;
			this.Number = c.Number;
			this.Name = c.Name;
			this.StartDate = c.StartDate;
			this.EndDate = c.EndDate;
			this.InitDate = c.InitDate;
			this.Files = c.Files;
			this.FkProfileNavigation = c.FkProfileNavigation;
			this.FkContractTypeNavigation = c.FkContractTypeNavigation;
			this.FkContractType = c.FkContractType;
			this.FkProfile = c.FkProfile;
		}

		public int Id { get; set; }
		public string Number { get; set; }
		public string Name { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? InitDate { get; set; }
		public int? FkContractType { get; set; }
		public int? FkProfile { get; set; }

		public virtual ContractType FkContractTypeNavigation { get; set; }
		public virtual Profile FkProfileNavigation { get; set; }
		public virtual ICollection<File> Files { get; set; }
	}
}
