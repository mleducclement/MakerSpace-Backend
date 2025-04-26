using System.ComponentModel.DataAnnotations;

namespace MakerSpace.Entities.Dtos;

public class CategoryDto {
   public Guid Id { get; set; }
   public required string Slug { get; init; }
   public required string Name { get; init; }
   public required int Heat { get; init; }
}

public record CategoryMutateDto(
   [Required] [StringLength(30)] string Name,
   [Required] [StringLength(30)] string Slug,
   [Required] [Range(1, 100)] int Heat
);