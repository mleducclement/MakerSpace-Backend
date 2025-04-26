using MakerSpace.Data;
using MakerSpace.Entities;
using MakerSpace.Entities.Dtos;
using MakerSpace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Controllers;

[Route("/products")]
[ApiController]
public class ProductController : ControllerBase {
   private readonly AppDbContext _context;

   public ProductController(AppDbContext context) {
      _context = context;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts() {
      return await _context.Products
         .Include(p => p.Category)
         .Select(x => ProductToDto(x))
         .ToListAsync();
   }

   [HttpGet("{id:guid}")]
   public async Task<ActionResult<ProductDto>> GetProduct(Guid id) {
      var product = await _context.Products.FindAsync(id);

      if (product == null) {
         return NotFound();
      }
      
      return ProductToDto(product);
   }
   
   [HttpPost]
   public async Task<ActionResult<ProductDto>> PostProduct(ProductMutateDto productDto) {
      var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == productDto.CategorySlug);
      
      if (category == null) {
         return NotFound($"Category not found: {productDto.CategorySlug}");
      }
      
      var product = new Product {
         Id = Guid.NewGuid(),
         Sku = productDto.Sku,
         Name = productDto.Name,
         Description = productDto.Description,
         Price = productDto.Price,
         Category = category,
         ImageUri = productDto.ImageUri,
         Rating = productDto.Rating,
         PromoRate = productDto.PromoRate,
         Stock = productDto.Stock
      };
      
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ProductToDto(product));
   }

   [HttpPost("bulk")]
   public async Task<ActionResult<IEnumerable<ProductDto>>> PostBulkProducts(List<ProductMutateDto> productDtos) {
      if (productDtos.Count == 0) {
         BadRequest("No product provided");
      }

      var createdProducts = new List<Product>();

      foreach (var productDto in productDtos) {
         if (!ModelState.IsValid) {
            return BadRequest();
         }
         
         var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == productDto.CategorySlug);
         if (category == null) {
            return BadRequest($"Category not found: {productDto.CategorySlug}");
         }
         
         var product = new Product {
            Id = Guid.NewGuid(),
            Sku = productDto.Sku,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Category = category,
            ImageUri = productDto.ImageUri,
            Rating = productDto.Rating,
            PromoRate = productDto.PromoRate,
            Stock = productDto.Stock
         };
         
         createdProducts.Add(product);
      }

      await using var transaction = await _context.Database.BeginTransactionAsync();
      try {
         await _context.Products.AddRangeAsync(createdProducts);
         await _context.SaveChangesAsync();
         await transaction.CommitAsync();
      }
      catch {
         await transaction.RollbackAsync();
         throw;
      }
      
      return CreatedAtAction(nameof(GetProducts),
         createdProducts.Select(ProductToDto).ToList());
   }
   
   private static ProductDto ProductToDto(Product product) => new() {
      Id = product.Id,
      Sku = product.Sku,
      Name = product.Name,
      Description = product.Description,
      Price = product.Price,
      Category = product.Category,
      ImageUri = product.ImageUri,
      Rating = product.Rating,
      PromoRate = product.PromoRate,
      Stock = product.Stock
   };
}
   