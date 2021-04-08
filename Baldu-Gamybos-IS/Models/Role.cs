using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class Role
    {
        public Role()
        {
            Profiles = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
