namespace DapperProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopingCart : DbContext
    {
        public ShopingCart()
            : base("name=ShopingCart")
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<AppUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRole)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }
}
