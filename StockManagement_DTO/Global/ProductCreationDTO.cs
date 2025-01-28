using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StockManagement_DTO.Global
{
    /// <summary>
    /// DTO for creating a product
    /// </summary>
    public class ProductCreationDTO
    {
        [Required]
        [MinLength(1, ErrorMessage = "Ce champ doit contenir au moins un caractère")]
        [MaxLength(255, ErrorMessage = "Ce champ ne doit pas dépasser 255 caractères")]
        public string Name { get; set; } = null!;

        [Required]
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        public int? MinQuantity { get; set; } = 0;

        [Required]
        public int IdStorage { get; set; }

        [MinLength(1, ErrorMessage = "Ce champ doit contenir au moins un caractère")]
        [MaxLength(255, ErrorMessage = "Ce champ ne doit pas dépasser 255 caractères")]
        public string? Description { get; set; }
    }
}
