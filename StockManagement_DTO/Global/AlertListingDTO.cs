using Microsoft.AspNetCore.Identity;
using StockManagement_Persistance.Entities;
using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class AlertListingDTO
    {
        [Display(Name = "Produits Manquants")]
        public List<Product> Products { get; set; } = new List<Product>();

        [Display(Name = "Traitée")]
        public bool Treated { get; set; }

        [Display(Name = "Créée le")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Mis à jour le")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
