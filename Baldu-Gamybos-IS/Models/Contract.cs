using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Contract
    {
        public Contract()
        {
            Files = new HashSet<File>();
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
