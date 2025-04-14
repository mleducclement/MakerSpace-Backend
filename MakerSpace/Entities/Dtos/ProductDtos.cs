using System.ComponentModel.DataAnnotations;

namespace MakerSpace.Entities.Dtos;

public record ProductDto {
   public required Guid Id { get; init; }
   public required string Name { get; init; }
   public required string Description { get; init; }
   public decimal Price { get; init; } 
   public required Category Category { get; init; }
   public required string ImageUri { get; init; }
}

public record ProductMutateDto(
   [Required][StringLength(50)] string Name,
   [Required][StringLength(250)] string Description,
   [Required][Range(1, 1000)] decimal Price,
   [Required] Category Category,
   [Url][Required] string ImageUri
);