using SmartInventoryAPI.Models;
using SmartInventoryAPI.Repositories;

namespace SmartInventoryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Constructor injection → gets repository instance
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            // Wrap synchronous repository call into a Task
            return await Task.Run(() => _productRepository.GetAllProducts());
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await Task.Run(() => _productRepository.GetProductById(id));
        }

        public async Task AddProductAsync(Product product)
        {
            // Example business rule
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty.");

            await Task.Run(() => _productRepository.AddProduct(product));
        }

        public async Task UpdateProductAsync(Product product)
        {
            await Task.Run(() => _productRepository.UpdateProduct(product));
        }

        public async Task DeleteProductAsync(int id)
        {
            await Task.Run(() => _productRepository.DeleteProduct(id));
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await Task.Run(() => _productRepository.GetProductsByCategoryId(categoryId));
        }
    }
}
