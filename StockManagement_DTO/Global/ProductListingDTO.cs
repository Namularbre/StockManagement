using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class ProductListingDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;

        [Display(Name = "Quantité")]
        public int Quantity { get; set; }

        [Display(Name = "Quantité minimale")]
        public int MinQuantity { get; set; }

        [Display(Name = "Nom du stockage")]
        public string StorageName { get; set; } = null!;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        public int IdStorage { get; set; }

        [Display(Name = "Est essentiel")]
        public bool IsEssential {  get; set; }
    }
}
