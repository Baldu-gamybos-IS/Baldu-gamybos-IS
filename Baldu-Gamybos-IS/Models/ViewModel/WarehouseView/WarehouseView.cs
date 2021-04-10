using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models.ViewModel.WarehouseView
{
    public partial class WarehouseView
    {

        public ICollection<Resource> Resources { get; set; }
        public ICollection<ProductView> Products { get; set; }
        public WarehouseView(ICollection<Resource> resources, ICollection<ProductView> products)
        {
            Resources = resources;
            Products = products;
        }
    }
}
