namespace StockManagement_Persistance.Entities
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Product>? Products { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
