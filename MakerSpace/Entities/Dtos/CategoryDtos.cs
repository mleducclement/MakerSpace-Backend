using System.ComponentModel.DataAnnotations;

namespace MakerSpace.Entities.Dtos;

public class CategoryDto {
   public Guid Id { get; set; }
   public required string Name { get; init; }
}

public record CategoryMutateDto(
   [Required][StringLength(30)] string Name
);