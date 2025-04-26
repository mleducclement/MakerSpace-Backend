using MakerSpace.Application.Interfaces.Repositories;
using MakerSpace.Application.Products.Queries;
using MakerSpace.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Infrastructure.Data.Repositories;

public class EfCoreProductRepository : IProductRepository {
   private readonly AppDbContext _context;

   public EfCoreProductRepository(AppDbContext context) {
      _context = context;
   }
   
   public async Task<IReadOnlyList<Product>> GetAllAsync() {
      return await _context.Products
         .AsNoTracking()
         .Include(p => p.Category)
         .ToListAsync();
   }
   
   public async Task<IReadOnlyList<Product>> GetProductsByQueryAsync(ProductQueryParams queryParams) {
      return await _context.Products
         .AsNoTracking()
         .Include(p => p.Category)
         .ApplyFiltering(queryParams)
         .ApplySorting(queryParams)
         .ToListAsync();
   }
   
   public async Task<Product?> GetByIdAsync(Guid id) {
      return await _context.Products
         .AsNoTracking()
         .Include(p => p.Category)
         .FirstOrDefaultAsync(p => p.Id == id);
   }
   
   public async Task<List<Product>> GetManyByIdsAsync(List<Guid> ids) {
      return await _context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
   }
   
   public async Task<Product> GetRequiredByIdAsync(Guid id) {
      var product = await _context.Products.FindAsync(id);
      if (product == null) {
         throw new InvalidOperationException("Product not found");
      }

      return product;
   }

   public async Task<Product> CreateAsync(Product entity) {
      _context.Products.Add(entity);
      await _context.SaveChangesAsync();
      return entity;
   }
   
   public async Task<IReadOnlyList<Product>> CreateManyAsync(IReadOnlyList<Product> products) {
      _context.Products.AddRange(products);
      await _context.SaveChangesAsync();
      return products;
   }
   
   public async Task UpdateAsync(Product product) {
      _context.Products.Update(product);
      await _context.SaveChangesAsync();
   }
   
   public async Task DeleteAsync(Guid id) {
      await _context.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
   }
}