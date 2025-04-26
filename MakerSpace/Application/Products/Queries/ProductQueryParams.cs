namespace MakerSpace.Application.Products.Queries;

public class ProductQueryParams {
   
   // Filters
   public Guid? CategoryId { get; set; }
   public decimal? MinPrice { get; set; }
   public decimal? MaxPrice { get; set; }
   public string? SearchQuery { get; set; }
   public bool? InStockOnly { get; set; }
   
   // Sorting
   public string? SortBy { get; set; }
   public bool Descending { get; set; }
}