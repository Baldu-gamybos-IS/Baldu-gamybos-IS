using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class DeliveryMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Volume { get; set; }
        public int? FkOrder { get; set; }

        public virtual GenericOrder FkOrderNavigation { get; set; }
    }
}
