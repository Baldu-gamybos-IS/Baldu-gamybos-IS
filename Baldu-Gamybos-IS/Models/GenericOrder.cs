using System;
using System.Collections.Generic;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class GenericOrder
    {
        public GenericOrder()
        {
            DeliveryMethods = new HashSet<DeliveryMethod>();
            Files = new HashSet<File>();
            OrderResources = new HashSet<OrderResource>();
            Payments = new HashSet<Payment>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public float? Price { get; set; }
        public float? PayedAmount { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? Direction { get; set; }
        public string Comment { get; set; }
        public DateTime? InitDate { get; set; }
        public string DestAddr { get; set; }
        public string DestZipcode { get; set; }
        public int? FkStatus { get; set; }
        public int? FkProfile { get; set; }
        public int? FkDistributor { get; set; }
        public int? FkSupplier { get; set; }

        public virtual Distributor FkDistributorNavigation { get; set; }
        public virtual Profile FkProfileNavigation { get; set; }
        public virtual OrderStatus FkStatusNavigation { get; set; }
        public virtual Supplier FkSupplierNavigation { get; set; }
        public virtual ICollection<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<OrderResource> OrderResources { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Product> Products { get; set; }


        public GenericOrder(GenericOrder r)
        {
            Id = r.Id;
            Price = r.Price;
            PayedAmount=r.PayedAmount;
            Deadline =r.Deadline;
            Direction =r.Direction;
            Comment =r.Comment;
            InitDate =r.InitDate;
            DestAddr =r.DestAddr;
            DestZipcode =r.DestZipcode;
            FkStatus=r.FkStatus;
            FkProfile=r.FkProfile;
            FkDistributor=r.FkDistributor;
            FkSupplier =r.FkSupplier;
        }

        public Product GetProduct() {
            IEnumerator<Product> enumerator = this.Products.GetEnumerator();
            if (enumerator.MoveNext()) {
                return enumerator.Current;
            } else {
                this.Products.Add(new Product());
                return this.GetProduct();
            }
        }
    }
}
