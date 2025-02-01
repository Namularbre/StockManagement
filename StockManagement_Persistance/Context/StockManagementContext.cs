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
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Snacks"},
                new Category { Id = 2, Name = "Produit d'entretien" },
                new Category { Id = 3, Name = "Produit sanitaire" },
                new Category { Id = 4, Name = "Produit chat" },
                new Category { Id = 5, Name = "Repas midi" },
                new Category { Id = 6, Name = "Repas soir" },
                new Category { Id = 7, Name = "Petit dej" }
            );

            builder.Entity<Storage>().HasData(
                new Storage { Id = 1, Name = "Cuisine" },
                new Storage { Id = 2, Name = "Salle de bain" },
                new Storage { Id = 3, Name = "Sellier" }
            );

            base.OnModelCreating(builder);
        }
    }
}
