using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DatabaseContext.Models
{
    public partial class MarketContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public MarketContext()
        {
        }

        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<EntryItems> EntryItems { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        //public virtual DbSet<IdentityUser> IdentityUsers { get; set; } //??
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductEntry> ProductEntry { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=DESKTOP-AFN70IH\SQLEXPRESS;database= Market;Trusted_Connection=True; 
Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd().UseIdentityColumn(1, 1); ;
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50);
                entity.Property(e => e.IsVip);

                entity.HasKey("Id");
            });

            modelBuilder.Entity<EntryItems>(entity =>
            {
                entity.HasIndex(e => e.ProductEntryId);

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ProductEntryId).HasColumnName("productEntryID");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ProductEntry)
                    .WithMany(p => p.EntryItems)
                    .HasForeignKey(d => d.ProductEntryId)
                    .HasConstraintName("FK_EntryItems_ProductEntry");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.EntryItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_EntryItems_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerId)
                .IsRequired()
                .HasColumnName("customerID");

                entity.Property(e => e.DeliveryAddress)
                    .HasColumnName("deliveryAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryPrice)
                    .HasColumnName("deliveryPrice")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(4)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TotalPrice)
                    .IsRequired()
                    .HasColumnName("totalPrice")
                    .HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });


            modelBuilder.Entity<OrderItems>(entity =>
            {
                entity.HasIndex(e => e.OrderId);

                entity.HasIndex(e => e.ProductId);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PriceTest).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItems_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderItems_Product");
            });

            //modelBuilder.Entity<Person>(entity =>
            //{
            //    entity.Property(e => e.Id)
            //        .HasColumnName("id")
            //        .ValueGeneratedNever();

            //    entity.Property(e => e.Address)
            //        .HasColumnName("address")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.EmailAdress)
            //        .HasColumnName("emailAdress")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Name)
            //        .HasColumnName("name")
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PhoneNumber).HasColumnName("phoneNumber");
            //});

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AlertQuantity).HasColumnName("alertQuantity");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                .IsRequired()
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Quantity)
                .HasColumnName("quantity");
                entity.Property(e => e.Description).HasColumnName("description");
            });


            modelBuilder.Entity<ProductEntry>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.SupplierId)
                .IsRequired()
                .HasColumnName("supplierID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ProductEntry)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductEntry_Supplier");

            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
                entity.HasKey("Id");
            });
            //modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");


            base.OnModelCreating(modelBuilder);
        }

        private void HasAnnotation(string v, RangeAttribute rangeAttribute)
        {
            throw new NotImplementedException();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
