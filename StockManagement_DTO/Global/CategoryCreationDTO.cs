using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class CategoryCreationDTO
    {
        [Required]
        [Display(Name = "Catégorie")]
        [MinLength(2, ErrorMessage = "Le nom de la catégorie doit contenir au moins 3 caractères")]
        [MaxLength(254, ErrorMessage = "Le nom de la catégorie doit contenir au maximum 254 caractères")]
        public string Name { get; set; } = null!;
    }
}
