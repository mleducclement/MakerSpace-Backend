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
      var category = await _context.Categories.FirstOrDefaultAsync(p => p.Name == productDto.CategoryName);
      if (category == null) {
         return NotFound($"Category not found: {productDto.CategoryName}");
      }
      
      var product = new Product {
         Id = Guid.NewGuid(),
         Sku = productDto.Sku,
         Name = productDto.Name,
         Description = productDto.Description,
         Price = productDto.Price,
         Category = category,
         ImageUri = productDto.ImageUri
      };
      
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ProductToDto(product));
   }
   
   private static ProductDto ProductToDto(Product product) => new() {
      Id = product.Id,
      Sku = product.Sku,
      Name = product.Name,
      Description = product.Description,
      Price = product.Price,
      Category = product.Category,
      ImageUri = product.ImageUri
   };
}
   