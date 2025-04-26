using MakerSpace.Application.Interfaces.Repositories;
using MakerSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Infrastructure.Data.Repositories;

public class EfCoreCategoriesRepository : ICategoriesRepository {
   private readonly AppDbContext _context;

   public EfCoreCategoriesRepository(AppDbContext context) {
      _context = context;
   }
   
   public async Task<IReadOnlyList<Category>> GetAllAsync() {
      return await _context.Categories
         .AsNoTracking()
         .ToListAsync();
   }
   
   public async Task<Category?> GetByIdAsync(Guid id) {
      return await _context.Categories
         .AsNoTracking()
         .FirstOrDefaultAsync(c => c.Id == id);
   }
   
   public async Task<List<Category>> GetManyByIdsAsync(List<Guid> ids) {
      return await _context.Categories.Where(c => ids.Contains(c.Id)).ToListAsync();
   }
   
   public async Task<Category> GetRequiredByIdAsync(Guid id) {
      var category = await _context.Categories.FindAsync(id);
      if (category == null) {
         throw new InvalidOperationException("Category not found");
      }

      return category;
   }
   
   public Task<Category> CreateAsync(Category entity) {
      throw new NotImplementedException();
   }
   
   public Task<IReadOnlyList<Category>> CreateManyAsync(IReadOnlyList<Category> categories) {
      throw new NotImplementedException();
   }
   
   public Task UpdateAsync(Category entity) {
      throw new NotImplementedException();
   }
   
   public Task DeleteAsync(Guid id) {
      throw new NotImplementedException();
   }

}