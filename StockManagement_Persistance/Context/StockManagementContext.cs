using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagement_Persistance .Entities;

namespace StockManagement_Persistance.Context
{
    public class StockManagementContext : IdentityDbContext
    {
        public StockManagementContext(DbContextOptions<StockManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Storage> Storages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Alert> Alerts { get; set; }
    }
}
