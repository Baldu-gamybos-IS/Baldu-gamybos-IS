using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ResourceTransaction
    {
        public int Id { get; set; }
        public bool? Direction { get; set; }
        public float? Amount { get; set; }
        public float? InitialAmount { get; set; }
        public DateTime? Time { get; set; }
        public int? FkResource { get; set; }

        public virtual Resource FkResourceNavigation { get; set; }
    }
}
