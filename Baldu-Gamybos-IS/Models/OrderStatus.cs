using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            GenericOrders = new HashSet<GenericOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenericOrder> GenericOrders { get; set; }
    }
}
