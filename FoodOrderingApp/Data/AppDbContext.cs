using FoodOrderingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<DeliveryPartner> DeliveryPartners { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole { RoleID = 1, Role = Role.ADMIN },
                new UserRole { RoleID = 2, Role = Role.CUSTOMER },
                new UserRole { RoleID = 3, Role = Role.DELIVERY_PARTNER});

            modelBuilder.Entity<User>()
               .HasDiscriminator<string>("UserType")
               .HasValue<User>("User")
               .HasValue<DeliveryPartner>("DeliveryPartner");
        }


    }
}
