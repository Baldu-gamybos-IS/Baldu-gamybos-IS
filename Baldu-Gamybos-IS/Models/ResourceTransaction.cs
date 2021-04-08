using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class ResourceTransaction
    {
        public ResourceTransaction()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public bool? Direction { get; set; }
        public float? Amount { get; set; }
        public float? InitialAmount { get; set; }
        public DateTime? Time { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
