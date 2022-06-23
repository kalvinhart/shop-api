using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dbContext;

        public ProductsController(DataContext context)
        {
            _dbContext = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dbContext.Products.Include(c => c.ProductCategories).ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSingleProduct(int id)
        {
            var product = await _dbContext.Products.Include(c => c.ProductCategories).FirstAsync(p => p.Id == id);

            if (product == null) return NotFound();
            
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            Product newProduct = new Product
            {
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color,
                Size = product.Size,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                ProductCategories = product.ProductCategories,
                StockQty = product.StockQty,
            };

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> EditProduct(Product product)
        {
            var oldProduct = await _dbContext.Products.FindAsync(product.Id);
            if (oldProduct == null) return NotFound();


        }
    }
}
