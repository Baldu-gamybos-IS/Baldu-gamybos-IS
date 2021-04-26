using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class MUnit
    {
        public MUnit()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string UnitName { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
