using System.ComponentModel.DataAnnotations;

namespace StockManagementDTO.Global
{
    public class StorageDTO
    {
        [Display(Name = "#")]
        public int Id { get; set; }
        [Display(Name = "Nom")]
        public string Name { get; set; } = null!;
    }
}
