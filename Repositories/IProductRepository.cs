using SmartInventoryAPI.Models;

namespace SmartInventoryAPI.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}
