using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class OrderResource
    {
        public int Id { get; set; }
        public float? Amount { get; set; }
        public int? FkResource { get; set; }
        public int? FkOrder { get; set; }

        public virtual GenericOrder FkOrderNavigation { get; set; }
        public virtual Resource FkResourceNavigation { get; set; }
    }
}
