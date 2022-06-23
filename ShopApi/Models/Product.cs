using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Brand { get; set; } = null!;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public Sizes? Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; } = "/images/no-image.jpg";
        public List<Category> ProductCategories { get; set; } = new List<Category> { };
        public int StockQty { get; set; } = 1;
        public int AmountSold { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public enum Sizes
        {
            XS, S, M, L, XL
        }

        public class ProductCategory
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public int ProductId { get; set; }
        }
    }
}
