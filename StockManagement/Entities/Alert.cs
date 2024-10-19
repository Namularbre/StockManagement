﻿using Microsoft.AspNetCore.Identity;

namespace StockManagement.Entities
{
    public class Alert
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = null!;
        public IdentityUser Author { get; set; } = null!;
        public bool Treated { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}