using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ProductTransaction
    {
        public ProductTransaction()
        {
            ProductResourceTransactions = new HashSet<ProductResourceTransaction>();
        }

        public int Id { get; set; }
        public bool? Direction { get; set; }
        public DateTime? Time { get; set; }
        public int? FkProduct { get; set; }

        public virtual Product FkProductNavigation { get; set; }
        public virtual ICollection<ProductResourceTransaction> ProductResourceTransactions { get; set; }
    }
}
