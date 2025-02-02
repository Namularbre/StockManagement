namespace StockManagement_DTO.Global
{
    public class CategoryListingDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public bool HasProducts { get; set; }
    }
}
