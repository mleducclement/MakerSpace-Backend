using MakerSpace.Application.Products.Dtos;
using MakerSpace.Domain.Models;

namespace MakerSpace.Application.Products.Mappings;

public static class ProductMappings {
   public static ProductDto ToDto(this Product product) {
      return new ProductDto() {
         Id = product.Id,
         Name = product.Name,
         Sku = product.Sku,
         Description = product.Description,
         Price = product.Price,
         Category = product.Category, // TODO: Extract to own DTO later on to expose ONLY used data ?
         ImageUri = product.ImageUri,
         PromoRate = product.PromoRate,
         Rating = product.Rating,
         Stock = product.Stock,
      };
   }

   public static Product ToProduct(this ProductMutateDto dto, Category category) {
      return new Product() {
         Id = Guid.NewGuid(),
         Sku = dto.Sku,
         Name = dto.Name,
         Description = dto.Description,
         Category = category, 
         Price = dto.Price,
         ImageUri = dto.ImageUri,
         Rating = dto.Rating,
         PromoRate = dto.PromoRate,
         Stock = dto.Stock
      };
   }
}