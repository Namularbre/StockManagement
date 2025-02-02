using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class ProductSearchDTO
    {
        [Display(Name = "Nom")]
        public string? Name { get; set; }
        [Display(Name = "Catégorie")]
        public int? IdCategory { get; set; }
        [Display(Name = "Stockage")]
        public int? IdStorage { get; set; }
    }
}
