﻿using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class CategoryListingDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
    }
}
