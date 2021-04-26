using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ContractType
    {
        public ContractType()
        {
            Contracts = new HashSet<Contract>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
