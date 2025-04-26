using System.ComponentModel.DataAnnotations;
using MakerSpace.Domain.Common;

namespace MakerSpace.Domain.Models;

public class Category : IEntity {
   
   public Guid Id { get; set; }
   
   [Required]
   [StringLength(30)]
   public required string Slug { get; set; }
   
   [Required]
   [StringLength(30)]
   public required string Name { get; set; }
   
   [Required]
   [Range(1, 100)]
   public int Heat { get; set; }
}