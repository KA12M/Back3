﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Back3.Models.Data
{
    public partial class CoffeeV2Context : DbContext
    {
        public CoffeeV2Context()
        {
        }

        public CoffeeV2Context(DbContextOptions<CoffeeV2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<ManageStock> ManageStock { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductDetail> ProductDetail { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<ReviewDetail> ReviewDetail { get; set; }
        public virtual DbSet<TransportDetail> TransportDetail { get; set; }
        public virtual DbSet<TypeProduct> TypeProduct { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("Data Source=10.0.0.228,1443;Initial Catalog=CoffeeV2;Persist Security Info=True;User ID=student;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Address");

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubDistrict)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("ZIPcode");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Address_User1");
            });

            modelBuilder.Entity<ManageStock>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Detail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.ProdcutId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("prodcut_id");

                entity.Property(e => e.TypeManage)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("typeManage")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Prodcut)
                    .WithMany(p => p.ManageStock)
                    .HasForeignKey(d => d.ProdcutId)
                    .HasConstraintName("FK_ManageStock_Product1");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .HasColumnName("isused")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransportCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("transportCode");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Order_Address1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("order_id");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("order_id");

                entity.Property(e => e.PayImg)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Payment_Order");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Detail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .HasColumnName("isused")
                    .IsFixedLength(true);

                entity.Property(e => e.LevelCoffee)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeSeed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Product_TypeProduct");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Detail).HasColumnType("text");

                entity.Property(e => e.Img)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetail)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductDetail_Product");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comments).HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Review_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Review_User1");
            });

            modelBuilder.Entity<ReviewDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Img)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.ReviewDetail)
                    .HasForeignKey(d => d.ReviewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReviewDetail_Review");
            });

            modelBuilder.Entity<TransportDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Detail).HasColumnType("text");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("orderId");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TransportDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_TransportDetail_Order");
            });

            modelBuilder.Entity<TypeProduct>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateReg).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Isused)
                    .HasMaxLength(1)
                    .HasColumnName("isused")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}