using Microsoft.EntityFrameworkCore;
using System.Security;

namespace DoAn.Models
{
    public class EGAFurnitureContext : DbContext
    {
        public EGAFurnitureContext(DbContextOptions<EGAFurnitureContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


    }
}
