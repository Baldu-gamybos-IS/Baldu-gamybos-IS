using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public class Profile
    {
        public Profile()
        {
            Contracts = new HashSet<Contract>();
            GenericOrders = new HashSet<GenericOrder>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public int? FkRole { get; set; }

        public virtual Role FkRoleNavigation { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<GenericOrder> GenericOrders { get; set; }
    }
}
