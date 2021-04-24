using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            GenericOrders = new HashSet<GenericOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenericOrder> GenericOrders { get; set; }

        public Supplier(Supplier supplier)
        {
            Id = supplier.Id;
            Name = supplier.Name;
        }
    }
}
