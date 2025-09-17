using Microsoft.EntityFrameworkCore;
using SmartInventoryAPI.Models; // EF Core base classes

namespace SmartInventory.Data  // we keep all DB related code in Data folder
{
    // AppDbContext = our bridge between C# and SQL
    public class AppDbContext : DbContext
    {
        // constructor: DbContext needs options (like which DB to connect to)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Example tables (will connect to SQL as tables)
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
