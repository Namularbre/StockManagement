using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class StorageViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
    }
}
