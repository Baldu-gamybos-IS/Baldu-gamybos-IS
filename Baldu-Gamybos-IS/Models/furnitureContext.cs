using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Baldu_Gamybos_IS.Models
{
    public partial class furnitureContext : DbContext
    {
        public furnitureContext()
        {
        }

        public furnitureContext(DbContextOptions<furnitureContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractType> ContractTypes { get; set; }
        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<GenericOrder> GenericOrders { get; set; }
        public virtual DbSet<MUnit> MUnits { get; set; }
        public virtual DbSet<OrderResource> OrderResources { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductResource> ProductResources { get; set; }
        public virtual DbSet<ProductResourceTransaction> ProductResourceTransactions { get; set; }
        public virtual DbSet<ProductTransaction> ProductTransactions { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<ResourceTransaction> ResourceTransactions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("contract");

                entity.HasIndex(e => e.FkProfile, "contract_ibfk_2");

                entity.HasIndex(e => e.FkContractType, "fk_contract_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.FkContractType)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_contract_type");

                entity.Property(e => e.FkProfile)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_profile");

                entity.Property(e => e.InitDate)
                    .HasColumnType("date")
                    .HasColumnName("init_date");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Number)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("number")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.HasOne(d => d.FkContractTypeNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.FkContractType)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("contract_ibfk_3");

                entity.HasOne(d => d.FkProfileNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.FkProfile)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("contract_ibfk_2");
            });

            modelBuilder.Entity<ContractType>(entity =>
            {
                entity.ToTable("contract_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<DeliveryMethod>(entity =>
            {
                entity.ToTable("delivery_method");

                entity.HasIndex(e => e.FkOrder, "fk_orderid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FkOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_order");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.DeliveryMethods)
                    .HasForeignKey(d => d.FkOrder)
                    .HasConstraintName("delivery_method_ibfk_1");
            });

            modelBuilder.Entity<Distributor>(entity =>
            {
                entity.ToTable("distributor");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("file");

                entity.HasIndex(e => e.FkContract, "fk_contractid");

                entity.HasIndex(e => e.FkOrder, "fk_orderid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FkContract)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_contract");

                entity.Property(e => e.FkOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_order");

                entity.Property(e => e.Mime)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("mime")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Path)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("path")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Size)
                    .HasPrecision(20)
                    .HasColumnName("size");

                entity.HasOne(d => d.FkContractNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FkContract)
                    .HasConstraintName("file_ibfk_2");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.FkOrder)
                    .HasConstraintName("file_ibfk_1");
            });

            modelBuilder.Entity<GenericOrder>(entity =>
            {
                entity.ToTable("generic_order");

                entity.HasIndex(e => new { e.FkDistributor, e.FkSupplier }, "fk_distributorid");

                entity.HasIndex(e => e.FkSupplier, "fk_supplierid");

                entity.HasIndex(e => e.FkProfile, "multiple");

                entity.HasIndex(e => e.FkStatus, "status");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("comment")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Deadline)
                    .HasColumnType("date")
                    .HasColumnName("deadline");

                entity.Property(e => e.DestAddr)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("dest_addr")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.DestZipcode)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("dest_zipcode")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.FkDistributor)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_distributor");

                entity.Property(e => e.FkProfile)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_profile");

                entity.Property(e => e.FkStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_status");

                entity.Property(e => e.FkSupplier)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_supplier");

                entity.Property(e => e.InitDate)
                    .HasColumnType("date")
                    .HasColumnName("init_date");

                entity.Property(e => e.PayedAmount).HasColumnName("payed_amount");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.FkDistributorNavigation)
                    .WithMany(p => p.GenericOrders)
                    .HasForeignKey(d => d.FkDistributor)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("generic_order_ibfk_3");

                entity.HasOne(d => d.FkProfileNavigation)
                    .WithMany(p => p.GenericOrders)
                    .HasForeignKey(d => d.FkProfile)
                    .HasConstraintName("multiple");

                entity.HasOne(d => d.FkStatusNavigation)
                    .WithMany(p => p.GenericOrders)
                    .HasForeignKey(d => d.FkStatus)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("generic_order_ibfk_4");

                entity.HasOne(d => d.FkSupplierNavigation)
                    .WithMany(p => p.GenericOrders)
                    .HasForeignKey(d => d.FkSupplier)
                    .HasConstraintName("generic_order_ibfk_2");
            });

            modelBuilder.Entity<MUnit>(entity =>
            {
                entity.ToTable("m_unit");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Unit)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("unit")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UnitName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("unit_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<OrderResource>(entity =>
            {
                entity.ToTable("order_resource");

                entity.HasIndex(e => e.FkOrder, "fk_orderid");

                entity.HasIndex(e => e.FkResource, "fk_resourceid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.FkOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_order");

                entity.Property(e => e.FkResource)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_resource");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.OrderResources)
                    .HasForeignKey(d => d.FkOrder)
                    .HasConstraintName("order_resource_ibfk_2");

                entity.HasOne(d => d.FkResourceNavigation)
                    .WithMany(p => p.OrderResources)
                    .HasForeignKey(d => d.FkResource)
                    .HasConstraintName("order_resource_ibfk_1");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.HasIndex(e => e.FkOrder, "fk_orderid");

                entity.HasIndex(e => e.FkType, "fk_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.FkOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_order");

                entity.Property(e => e.FkType)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_type");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.FkOrder)
                    .HasConstraintName("payment_ibfk_2");

                entity.HasOne(d => d.FkTypeNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.FkType)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payment_ibfk_3");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("payment_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("char(16)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.FkOrder, "fk_order");

                entity.HasIndex(e => e.FkProductType, "fk_product_typename")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Count)
                    .HasColumnType("int(11)")
                    .HasColumnName("count");

                entity.Property(e => e.EstPrimeCost).HasColumnName("est_prime_cost");

                entity.Property(e => e.FkOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_order");

                entity.Property(e => e.FkProductType)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_product_type");

                entity.Property(e => e.MadeCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("made_count")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PartCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("part_count");

                entity.Property(e => e.SinglePrice).HasColumnName("single_price");

                entity.Property(e => e.Type)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("type")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.FkOrderNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.FkOrder)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_ibfk_2");

                entity.HasOne(d => d.FkProductTypeNavigation)
                    .WithOne(p => p.Product)
                    .HasForeignKey<Product>(d => d.FkProductType)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_ibfk_1");
            });

            modelBuilder.Entity<ProductResource>(entity =>
            {
                entity.ToTable("product_resource");

                entity.HasIndex(e => e.FkProduct, "fk_productid");

                entity.HasIndex(e => e.FkResource, "fk_resourceid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.FkProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_product");

                entity.Property(e => e.FkResource)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_resource");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.ProductResources)
                    .HasForeignKey(d => d.FkProduct)
                    .HasConstraintName("product_resource_ibfk_1");

                entity.HasOne(d => d.FkResourceNavigation)
                    .WithMany(p => p.ProductResources)
                    .HasForeignKey(d => d.FkResource)
                    .HasConstraintName("product_resource_ibfk_2");
            });

            modelBuilder.Entity<ProductResourceTransaction>(entity =>
            {
                entity.ToTable("product_resource_transaction");

                entity.HasIndex(e => e.FkProdTrans, "fk_prod_trans");

                entity.HasIndex(e => e.FkProdRes, "fk_rod_res");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.FkProdRes)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_prod_res");

                entity.Property(e => e.FkProdTrans)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_prod_trans");

                entity.Property(e => e.InitialAmount).HasColumnName("initial_amount");

                entity.HasOne(d => d.FkProdResNavigation)
                    .WithMany(p => p.ProductResourceTransactions)
                    .HasForeignKey(d => d.FkProdRes)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_resource_transaction_ibfk_2");

                entity.HasOne(d => d.FkProdTransNavigation)
                    .WithMany(p => p.ProductResourceTransactions)
                    .HasForeignKey(d => d.FkProdTrans)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_resource_transaction_ibfk_1");
            });

            modelBuilder.Entity<ProductTransaction>(entity =>
            {
                entity.ToTable("product_transaction");

                entity.HasIndex(e => e.FkResource, "fk_resource");

                entity.HasIndex(e => e.FkProduct, "product_transaction_ibfk_1");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.FkProduct)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_product");

                entity.Property(e => e.FkResource)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_resource");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");

                entity.HasOne(d => d.FkProductNavigation)
                    .WithMany(p => p.ProductTransactions)
                    .HasForeignKey(d => d.FkProduct)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_transaction_ibfk_1");

                entity.HasOne(d => d.FkResourceNavigation)
                    .WithMany(p => p.ProductTransactions)
                    .HasForeignKey(d => d.FkResource)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("product_transaction_ibfk_2");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("product_type");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PriceMultiplier).HasColumnName("price_multiplier");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("profile");

                entity.HasIndex(e => e.FkRole, "roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("address")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FirstName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("first_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.FkRole)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_role");

                entity.Property(e => e.LastName)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("last_name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("password")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Phone)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("phone")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Username)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("username")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Zipcode)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("zipcode")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.FkRoleNavigation)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.FkRole)
                    .HasConstraintName("profile_ibfk_1");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("resource");

                entity.HasIndex(e => e.FkMUnit, "fk_m_unitid");

                entity.HasIndex(e => e.FkResTrans, "fk_resource_transactionid");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.FkMUnit)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_m_unit");

                entity.Property(e => e.FkResTrans)
                    .HasColumnType("int(11)")
                    .HasColumnName("fk_res_trans");

                entity.Property(e => e.LeftAmount).HasColumnName("left_amount");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

                entity.HasOne(d => d.FkMUnitNavigation)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.FkMUnit)
                    .HasConstraintName("resource_ibfk_2");

                entity.HasOne(d => d.FkResTransNavigation)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.FkResTrans)
                    .HasConstraintName("resource_ibfk_1");
            });

            modelBuilder.Entity<ResourceTransaction>(entity =>
            {
                entity.ToTable("resource_transaction");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.InitialAmount).HasColumnName("initial_amount");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("name")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
