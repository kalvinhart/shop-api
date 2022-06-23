using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApi.Models;
using ShopApi.Data;
using static ShopApi.Models.Category;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public CategoriesController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            List<Category> categories = await _dbContext.Categories.Include(s => s.Subcategories).ToListAsync();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category data)
        {
            List<Subcategory> subcategories = new List<Subcategory>{};

            foreach(Subcategory subcategory in data.Subcategories)
            {
                subcategories.Add(new Subcategory() { Name = subcategory.Name});
            };

            Category category = new Category
            {
                Name = data.Name,
                Subcategories = subcategories
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            
            return Ok(category);
        }

        [HttpPatch]
        public async Task<IActionResult> EditCategory(Category category)
        {
            var oldCategory = await _dbContext.Categories.FindAsync(category.Id);
            if (oldCategory == null) return NotFound();

            oldCategory.Name = category.Name;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Category category)
        {
            var categoryToDelete = await _dbContext.Categories.FindAsync(category.Id);
            if (categoryToDelete == null) return NotFound("Category not found.");


            _dbContext.Categories.Remove(categoryToDelete);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpPost]
        [Route("subcategories")]
        public async Task<IActionResult> AddSubcategory(Subcategory subcategory)
        {
            var category = await _dbContext.Categories.FindAsync(subcategory.CategoryId);

            if (category == null) return NotFound();

            category.Subcategories.Add(subcategory);
            await _dbContext.SaveChangesAsync();

            return Ok(subcategory);
        }


        [HttpDelete]
        [Route("subcategories")]
        public async Task<IActionResult> DeleteSubcategory(Subcategory subcategory)
        {
            var category = await _dbContext.Categories.Include(s => s.Subcategories).Where(p => p.Id == subcategory.CategoryId).FirstOrDefaultAsync();
            if (category == null) return NotFound("Category not found.");
            
            var subcategoryToRemove = category.Subcategories.Find(s => s.Id == subcategory.Id);
            if (subcategoryToRemove == null) return NotFound("Subcategory not found.");

            category.Subcategories.Remove(subcategoryToRemove);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
