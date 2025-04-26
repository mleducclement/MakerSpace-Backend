using System.ComponentModel.DataAnnotations;
using MakerSpace.Models;

namespace MakerSpace.Entities.Dtos;

public record ProductDto {
   public required Guid Id { get; init; }
   public required string Sku { get; init; }
   public required string Name { get; init; }
   public required string Description { get; init; }
   public required Category Category { get; init; }
   public required decimal Price { get; init; } 
   public required string ImageUri { get; init; }
   public required double Rating { get; init; }
   public required int PromoRate { get; init; }
   public required int Stock { get; init; }
}

public record ProductMutateDto(
   string Sku,
   string Name,
   string Description,
   string CategorySlug,
   decimal Price,
   string ImageUri,
   double Rating,
   int PromoRate,
   int Stock
);