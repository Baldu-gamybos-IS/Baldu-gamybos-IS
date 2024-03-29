using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Baldu_Gamybos_IS.Models;

#nullable disable

namespace Baldu_Gamybos_IS.Models.ViewModel.WarehouseView
{
    public partial class ProductView
    {

        public int Id { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Neigiama kaina negalima!!!")]
        public float? SinglePrice { get; set; }
        
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Neigiamas kiekis negalimas!!!")]
        public int? Count { get; set; }
        public string Type { get; set; }
        public int? OrderId { get; set; }
        public string ProductName { get; set; }
        public ProductView(Product product, string name)
        {
            Id = product.Id;
            SinglePrice = product.SinglePrice;
            Count = product.Count;
            Type = product.Type;
            OrderId = product.FkOrder;
            ProductName = name;
        }
        public ProductView()
        {
            
        }
    }
}
