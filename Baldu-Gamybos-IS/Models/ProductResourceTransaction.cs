using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ProductResourceTransaction
    {
        public int Id { get; set; }
        public float? InitialAmount { get; set; }
        public int? FkProdRes { get; set; }
        public int? FkProdTrans { get; set; }

        public virtual Product FkProdResNavigation { get; set; }
        public virtual ProductResource FkProdTransNavigation { get; set; }
    }
}
