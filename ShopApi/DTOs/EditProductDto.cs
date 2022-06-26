using static ShopApi.Models.Product;

namespace ShopApi.DTOs
{
    public class EditProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public decimal? Price { get; set; }
        public Sizes? Size { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? StockQty { get; set; }
        public int? AmountSold { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
