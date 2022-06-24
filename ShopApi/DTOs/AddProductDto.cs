using static ShopApi.Models.Product;

namespace ShopApi.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image.jpg";
        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory> { };
        public int StockQty { get; set; }
    }
}
