using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public float? Amount { get; set; }
        public DateTime? Time { get; set; }
        public bool? Direction { get; set; }
        public int? FkType { get; set; }
        public int? FkOrder { get; set; }

        public virtual GenericOrder FkOrderNavigation { get; set; }
        public virtual PaymentType FkTypeNavigation { get; set; }
    }
}
