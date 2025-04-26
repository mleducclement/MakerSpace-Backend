using MakerSpace.Application.Products.Queries;
using MakerSpace.Domain.Models;
using MakerSpace.Infrastructure.Data.Repositories.Interfaces;

namespace MakerSpace.Application.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product> {
   Task<IReadOnlyList<Product>> GetProductsByQueryAsync(ProductQueryParams queryParams);
}