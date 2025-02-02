using System.ComponentModel.DataAnnotations;

namespace StockManagement_DTO.Global
{
    public class StorageCreationDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Ce champ doit contenir au moin 3 lettres")]
        [MaxLength(255, ErrorMessage = "Ce champ doit contenir moins de 255 lettres")]
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
    }
}
