using MakerSpace.Data;
using MakerSpace.Entities;
using MakerSpace.Entities.Dtos;
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
      var product = new Product {
         Id = Guid.NewGuid(),
         Name = productDto.Name,
         Description = productDto.Description,
         Price = productDto.Price,
         Category = productDto.Category,
         ImageUri = productDto.ImageUri
      };
      
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ProductToDto(product));
   }
   
   private static ProductDto ProductToDto(Product product) => new() {
      Id = product.Id,
      Name = product.Name,
      Description = product.Description,
      Price = product.Price,
      Category = product.Category,
      ImageUri = product.ImageUri
   };
}
   