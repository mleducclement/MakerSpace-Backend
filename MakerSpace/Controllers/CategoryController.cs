using MakerSpace.Data;
using MakerSpace.Entities;
using MakerSpace.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Controllers;

[Route("/categories")]
[ApiController]
public class CategoryController : ControllerBase {
   private readonly AppDbContext _context;

   public CategoryController(AppDbContext context) {
      _context = context;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories() {
      return await _context.Categories
         .Select(x => CategoryToDto(x))
         .ToListAsync();
   }

   [HttpGet("{id:guid}")]
   public async Task<ActionResult<CategoryDto>> GetCategory(Guid id) {
      var category = await _context.Categories.FindAsync(id);

      if (category == null) {
         return NotFound();
      }
      
      return CategoryToDto(category);
   }

   [HttpPost]
   public async Task<ActionResult<CategoryDto>> PostCategory(CategoryMutateDto categoryDto) {
      var category = new Category {
         Id = Guid.NewGuid(),
         Name = categoryDto.Name
      };
      
      _context.Categories.Add(category);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, CategoryToDto(category));
   }

   private static CategoryDto CategoryToDto(Category category) => new() {
      Id = category.Id,
      Name = category.Name
   };
}