namespace StockManagement_Persistance.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int MinQuantity { get; set; }
        public Storage Storage { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<Alert>? Alerts { get; set; }
        public bool IsEssential { get; set; } = false;
        public Category Category { get; set; } = null!;
    }
}
