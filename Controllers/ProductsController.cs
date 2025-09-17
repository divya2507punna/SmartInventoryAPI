using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventoryAPI.Models;
using SmartInventoryAPI.Services;

namespace SmartInventoryAPI.Controllers
{
    [ApiController]                                     // Marks this as an API Controller
    [Route("api/[controller]")]                         // URL → api/products
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor injection → get service
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // ------------------ CRUD Endpoints ------------------

        // GET: api/products
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);    // 200 + products
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();  // 404 if not found

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch.");

            await _productService.UpdateProductAsync(product);
            return NoContent();   // 204 (success, no response body)
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        // GET: api/products/category/3
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return Ok(products);
        }
    }
}
