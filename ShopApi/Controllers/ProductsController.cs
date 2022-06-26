using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.DTOs;
using ShopApi.Extensions;
using ShopApi.Models;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsController(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts(string? category, string? brands, string? colors, string? name, string? sort)
        {
            var query = _dbContext.Products.Include(c => c.ProductCategories)
                .Search(name!)
                .Filter(brands!, colors!, category!)
                .Sort(sort!)
                .AsQueryable();            

            var products = await query.ToListAsync();

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
        public async Task<IActionResult> AddProduct(AddProductDto productDto)
        {
            var newProduct = _mapper.Map<Product>(productDto);

            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPatch]
        public async Task<IActionResult> EditProduct(EditProductDto productDto)
        {
            var productToEdit = await _dbContext.Products.FindAsync(productDto.Id);
            if (productToEdit == null) return NotFound();

            _mapper.Map(productDto, productToEdit);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
