using System.ComponentModel.DataAnnotations;

namespace MakerSpace.Entities;

public class Category {
   
   public Guid Id { get; set; }
   
   [Required]
   [StringLength(30)]
   public required string Name { get; set; }
}