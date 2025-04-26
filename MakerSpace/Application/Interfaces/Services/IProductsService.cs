using MakerSpace.Application.Products.Dtos;
using MakerSpace.Application.Products.Queries;
using MakerSpace.Application.Products.Results;
using MakerSpace.Domain.Models;

namespace MakerSpace.Application.Interfaces.Services;

public interface IProductsService {
   Task<IReadOnlyList<ProductDto>> GetAllAsync();
   Task<IReadOnlyList<ProductDto>> GetProductsByQueryAsync(ProductQueryParams queryParams);
   Task<ProductDto?> GetByIdAsync(Guid id);
   Task<OperationResult<ProductDto>> CreateAsync(ProductMutateDto dto);
   Task<OperationResult<List<ProductDto>>> CreateManyAsync(IReadOnlyList<ProductMutateDto> dtos);
   Task UpdateAsync(Product entity);
   Task DeleteAsync(Guid id);
}