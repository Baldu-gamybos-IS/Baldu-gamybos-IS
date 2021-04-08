using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class File
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Mime { get; set; }
        public decimal? Size { get; set; }
        public int? FkOrder { get; set; }
        public int? FkContract { get; set; }

        public virtual Contract FkContractNavigation { get; set; }
        public virtual GenericOrder FkOrderNavigation { get; set; }
    }
}
