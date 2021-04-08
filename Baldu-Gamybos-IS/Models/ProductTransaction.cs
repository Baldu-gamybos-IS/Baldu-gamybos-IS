using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ProductTransaction
    {
        public int Id { get; set; }
        public bool? Direction { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? FkProduct { get; set; }
        public int? FkResource { get; set; }

        public virtual Product FkProductNavigation { get; set; }
        public virtual Resource FkResourceNavigation { get; set; }
    }
}
