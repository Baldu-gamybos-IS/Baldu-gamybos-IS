using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Distributor
    {
        public Distributor()
        {
            GenericOrders = new HashSet<GenericOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<GenericOrder> GenericOrders { get; set; }
    }
}
