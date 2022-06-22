namespace ShopApi.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Subcategory> Subcategories { get; set; } = new List<Subcategory> { };

        public class Subcategory
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
        }
    }
}
