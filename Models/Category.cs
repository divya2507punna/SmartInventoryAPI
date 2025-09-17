namespace SmartInventoryAPI.Models
{
    public class Category
    {
        public int Id { get; set; }              // Primary Key
        public string Name { get; set; } = "";   // Category Name (e.g., Electronics)

        // Navigation property → one category has many products
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
