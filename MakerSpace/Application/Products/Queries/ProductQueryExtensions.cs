using MakerSpace.Domain.Models;

namespace MakerSpace.Application.Products.Queries;

public static class ProductQueryExtensions {

   public static IQueryable<Product> ApplyFiltering(this IQueryable<Product> query, ProductQueryParams queryParams) {
      if (!string.IsNullOrWhiteSpace(queryParams.SearchQuery)) {
         var normalizedQuery = queryParams.SearchQuery.Trim().ToLowerInvariant();
         query = query.Where(p => p.Name.ToLower().Contains(normalizedQuery));
      }

      if (queryParams.CategoryId.HasValue) {
         query = query.Where(p => p.CategoryId == queryParams.CategoryId.Value);
      }

      if (queryParams.MinPrice.HasValue) {
         query = query.Where(p => p.Price >= queryParams.MinPrice.Value);
      }

      if (queryParams.MaxPrice.HasValue) {
         query = query.Where(p => p.Price <= queryParams.MaxPrice.Value);
      }

      if (queryParams.InStockOnly.HasValue) {
         query = query.Where(p => p.Stock > 0);
      }
      
      return query;
   }
   
   public static IQueryable<Product> ApplySorting(this IQueryable<Product> query, ProductQueryParams queryParams) {
      query = queryParams.SortBy?.ToLower() switch {
         "name" => queryParams.Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
         "price" => queryParams.Descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
         "rating" => queryParams.Descending ? query.OrderByDescending(p => p.Rating) : query.OrderBy(p => p.Rating),
         _ => query
      };
      
      return query;
   }
}