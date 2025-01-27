using System.ComponentModel.DataAnnotations;

namespace StockManagement.Models
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
        public bool HasProducts { get; set; }
    }
}
