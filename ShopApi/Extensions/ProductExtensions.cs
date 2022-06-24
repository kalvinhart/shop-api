using ShopApi.Models;

namespace ShopApi.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string sort)
        {
            if (string.IsNullOrWhiteSpace(sort)) return query.OrderByDescending(p => p.AmountSold);

            query = sort switch
            {
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                "nameDesc" => query.OrderByDescending(p => p.Name),
                "date" => query.OrderByDescending(p => p.CreatedAt),
                "amountDesc" => query.OrderByDescending(p => p.AmountSold),
                _ => query.OrderByDescending(p => p.AmountSold)
            };

            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return query;

            return query.Where(p => p.Name.ToLower().Contains(name.Trim().ToLower()));
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brands, string colors, string category)
        {
            var brandList = new List<string>();
            var colorsList = new List<string>();

            if (!string.IsNullOrEmpty(brands)) brandList.AddRange(brands.ToLower().Split(",").ToList());
            if (!string.IsNullOrEmpty(colors)) colorsList.AddRange(colors.ToLower().Split(",").ToList());

            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.Brand.ToLower()));
            query = query.Where(p => colorsList.Count == 0 || colorsList.Contains(p.Color!.ToLower()));

            if (!string.IsNullOrEmpty(category)) query = query.Where(p => p.ProductCategories.Any(pc => pc.Name == category));

            return query;
        }
    }
}
