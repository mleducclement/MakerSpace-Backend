using System.ComponentModel.DataAnnotations;

namespace MakerSpace.Entities;

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
   [Range(1, 1000)]
   public decimal Price { get; set; }
   
   [Url]
   [Required]
   [StringLength(250)]
   public required string ImageUri { get; set; }
}