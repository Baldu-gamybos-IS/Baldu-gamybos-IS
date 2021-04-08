using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? PriceMultiplier { get; set; }

        public virtual Product Product { get; set; }
    }
}
