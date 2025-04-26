using System.ComponentModel.DataAnnotations;
using MakerSpace.Domain.Models;

namespace MakerSpace.Models;

public class Product {
   
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
   public required double Rating { get; set; }
   
   [Required]
   [Range(0, 100)]
   public required int PromoRate { get; set; }
   
   [Required]
   public required int Stock { get; set; }
}