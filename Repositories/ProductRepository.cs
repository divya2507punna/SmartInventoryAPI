using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SmartInventory.Data;
using SmartInventoryAPI.Models;

namespace SmartInventoryAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;       // EF Core
        private readonly IConfiguration _config;      // ADO.NET (connection string)

        // Constructor Dependency Injection
        public ProductRepository(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ---------------------- EF CORE METHODS ----------------------

        // Get all products (with category info)
        public IEnumerable<Product> GetAllProducts()
        {
            // EF translates LINQ to SQL behind the scenes
            return _context.Products
                           .Include(p => p.Category) // eager load related category
                           .ToList();
        }

        // Get product by ID
        public Product? GetProductById(int id)
        {
            // Single query with optional include
            return _context.Products
                           .Include(p => p.Category)
                           .FirstOrDefault(p => p.Id == id);
        }

        // Add new product
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);   // tracked in memory
            _context.SaveChanges();           // commit to DB
        }

        // Update product
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        // Delete product
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id); // tries cache first, then DB
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // ---------------------- ADO.NET METHOD ----------------------

        // Get products by category using raw SQL
        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = new List<Product>();

            // Fetch connection string from appsettings.json
            using (SqlConnection conn = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                conn.Open();

                // Parameterized query (protects against SQL Injection)
                string sql = "SELECT Id, Name, Price, CategoryId FROM Products WHERE CategoryId = @CategoryId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    // Execute reader → iterate results row by row
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],                          // cast to int
                                Name = reader["Name"].ToString()!,               // safe string
                                Price = (decimal)reader["Price"],                // cast to decimal
                                CategoryId = (int)reader["CategoryId"]
                            });
                        }
                    }
                }
            }

            return products;
        }
    }
}
