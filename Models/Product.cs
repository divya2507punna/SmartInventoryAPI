namespace SmartInventoryAPI.Models
{
    public class Product
    {
        public int Id { get; set; }               // Primary Key
        public string Name { get; set; } = "";    // Product Name
        public decimal Price { get; set; }        // Price
        public int Stock { get; set; }            // Quantity available

        // Foreign Key → CategoryId links product to a category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }   // Navigation property
    }
}
