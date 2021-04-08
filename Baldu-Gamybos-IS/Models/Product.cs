using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductResourceTransactions = new HashSet<ProductResourceTransaction>();
            ProductResources = new HashSet<ProductResource>();
            ProductTransactions = new HashSet<ProductTransaction>();
        }

        public int Id { get; set; }
        public float? SinglePrice { get; set; }
        public int? Count { get; set; }
        public int? MadeCount { get; set; }
        public string Type { get; set; }
        public int PartCount { get; set; }
        public float EstPrimeCost { get; set; }
        public int? FkOrder { get; set; }
        public int? FkProductType { get; set; }

        public virtual GenericOrder FkOrderNavigation { get; set; }
        public virtual ProductType FkProductTypeNavigation { get; set; }
        public virtual ICollection<ProductResourceTransaction> ProductResourceTransactions { get; set; }
        public virtual ICollection<ProductResource> ProductResources { get; set; }
        public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }
    }
}
