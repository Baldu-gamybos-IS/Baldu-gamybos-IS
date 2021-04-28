using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Resource
    {
        public Resource()
        {
            OrderResources = new HashSet<OrderResource>();
            ProductResources = new HashSet<ProductResource>();
            ResourceTransactions = new HashSet<ResourceTransaction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Neigiamas kiekis negalimas!!!")]
        public float? LeftAmount { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Neigiama kaina negalima!!!")]
        public float? UnitPrice { get; set; }
        public int? FkMUnit { get; set; }

        public virtual MUnit FkMUnitNavigation { get; set; }
        public virtual ICollection<OrderResource> OrderResources { get; set; }
        public virtual ICollection<ProductResource> ProductResources { get; set; }
        public virtual ICollection<ResourceTransaction> ResourceTransactions { get; set; }

        public Resource(Resource r)
        {
            Id = r.Id;
            Name = r.Name;
            LeftAmount = r.LeftAmount;
            UnitPrice = r.UnitPrice;
        }
    }

    public class EstResource : Resource
    { 
        public EstResource(Resource r, double est) : base(r)
        {
            this.Est = est;
        }
        public double Est { get; set; }
    }
}
