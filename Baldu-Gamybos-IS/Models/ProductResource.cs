using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ProductResource
    {
        public ProductResource()
        {
            ProductResourceTransactions = new HashSet<ProductResourceTransaction>();
        }

        public int Id { get; set; }
        public float? Amount { get; set; }
        public int? FkProduct { get; set; }
        public int? FkResource { get; set; }

        public virtual Product FkProductNavigation { get; set; }
        public virtual Resource FkResourceNavigation { get; set; }
        public virtual ICollection<ProductResourceTransaction> ProductResourceTransactions { get; set; }
    }
}
