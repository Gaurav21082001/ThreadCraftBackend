using Microsoft.EntityFrameworkCore;

namespace ProjectFirst.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public  DbSet<User> Users { get; set; }

        public  DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Order> Orders { get; set; }

       

        public DbSet<Order_Items> Order_Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(b => b.Role)
                .HasDefaultValue("Customer");

            modelBuilder.Entity<Order>()
                .Property(b => b.Status)
                .HasDefaultValue("Pending");
        }


    }
}
