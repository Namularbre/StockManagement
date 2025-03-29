using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class ProductRecentlyUpdatedDTO
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
        [Display(Name = "Date de modification")]
        public DateTime UpdatedAt { get; set; }
        [Display(Name = "Quantité")]
        public int Quantity { get; set; }
        [Display(Name = "Quantité minimale")]
        public int MinQuantity { get; set; }
        public int StorageId { get; set; }
    }
}
