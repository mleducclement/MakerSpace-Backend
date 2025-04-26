using System.ComponentModel.DataAnnotations;
using MakerSpace.Domain.Common;

namespace MakerSpace.Domain.Models;

public class Product : IEntity {
   
   public Guid Id { get; set; }
   
   [Required]
   [StringLength(30)]
   public required string Sku { get; set; }
   
   [Required]
   [StringLength(50)]
   public required string Name { get; set; }
   
   [Required]
   [StringLength(250)]
   public required string Description { get; set; }
   
   [Required]
   public Guid CategoryId { get; set; }
   
   [Required]
   public required Category Category { get; set; }
   
   [Required]
   [Range(1, 1000, MaximumIsExclusive = true)]
   public decimal Price { get; set; }
   
   [Url]
   [Required]
   [StringLength(500)]
   public required string ImageUri { get; set; }
   
   [Required]
   [Range(0, 5)]
   public double Rating { get; set; }
   
   [Required]
   [Range(0, 100)]
   public int PromoRate { get; set; }
   
   [Required]
   public int Stock { get; set; }
}