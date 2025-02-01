using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Le nom doit contenir au moins 3 caractères")]
        [MaxLength(255, ErrorMessage = "Le nom ne doit pas dépasser 254 caractères")]
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;

        [MinLength(3, ErrorMessage = "La description doit contenir au moins 3 caractères")]
        [MaxLength(255, ErrorMessage = "La description ne doit pas dépasser 254 caractères")]
        public string? Description { get; set; }

        [Required]
        public int IdStorage { get; set; }

        [Required]
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        [Display(Name = "Quantité")]
        public int Quantity { get; set; } = 1;

        [DefaultValue(0)]
        [Range(0, int.MaxValue)]
        [Display(Name = "Quantité minimale")]
        public int? MinQuantity { get; set; } = 0;

        [DefaultValue(false)]
        [Display(Name = "Est essentiel")]
        public bool IsEssential { get; set; } = false;
    }
}
