using MakerSpace.Application.Interfaces.Repositories;
using MakerSpace.Application.Interfaces.Services;
using MakerSpace.Application.Products.Dtos;
using MakerSpace.Application.Products.Mappings;
using MakerSpace.Application.Products.Queries;
using MakerSpace.Application.Products.Results;
using MakerSpace.Domain.Models;
using Microsoft.JSInterop.Infrastructure;

namespace MakerSpace.Application.Products.Services;

public class ProductsService : IProductsService {
   private readonly IProductRepository _productsRepo;
   private readonly ICategoriesRepository _categoriesRepo;

   public ProductsService(IProductRepository productsRepo, ICategoriesRepository categoriesRepo) {
      _productsRepo = productsRepo;
      _categoriesRepo = categoriesRepo;
   }

   public async Task<IReadOnlyList<ProductDto>> GetAllAsync() {
      var products = await _productsRepo.GetAllAsync();
      return ConvertToDtos(products);
   }

   public async Task<IReadOnlyList<ProductDto>> GetProductsByQueryAsync(ProductQueryParams queryParams) {
      var products = await _productsRepo.GetProductsByQueryAsync(queryParams);
      return ConvertToDtos(products);
   }

   public async Task<ProductDto?> GetByIdAsync(Guid id) {
      var product = await _productsRepo.GetByIdAsync(id);
      return product?.ToDto();
   }

   public async Task<OperationResult<ProductDto>> CreateAsync(ProductMutateDto dto) {
      var category = await _categoriesRepo.GetRequiredByIdAsync(dto.CategoryId);
      var newProduct = dto.ToProduct(category);
      var createdProduct = await _productsRepo.CreateAsync(newProduct);

      return OperationResult<ProductDto>.Success(createdProduct.ToDto());
   }

   public async Task<OperationResult<List<ProductDto>>> CreateManyAsync(IReadOnlyList<ProductMutateDto> dtos) {
      var categoryIds = dtos.Select(dto => dto.CategoryId).Distinct().ToList();
      var categories = await _categoriesRepo.GetManyByIdsAsync(categoryIds);

      var categoryMap = categories.ToDictionary(c => c.Id);

      var mapResult = MapProducts(dtos, categoryMap);
      if (!mapResult.IsSuccess) {
         return OperationResult<List<ProductDto>>.Failure(mapResult.Error!);
      }

      var createdProducts = await _productsRepo.CreateManyAsync(mapResult.Data!);
      var returnedDtos = createdProducts.Select(p => p.ToDto()).ToList();

      return OperationResult<List<ProductDto>>.Success(returnedDtos);
   }
   
   public Task UpdateAsync(Product entity) {
      throw new NotImplementedException();
   }

   public Task DeleteAsync(Guid id) {
      throw new NotImplementedException();
   }

   private static IReadOnlyList<ProductDto> ConvertToDtos(IReadOnlyList<Product> products) {
      return products
         .Select(p => p.ToDto())
         .ToList();
   }

   private static OperationResult<List<Product>> MapProducts(
      IReadOnlyList<ProductMutateDto> dtos,
      Dictionary<Guid, Category> categoryMap) {
      var products = new List<Product>();

      foreach (var dto in dtos) {
         if (!categoryMap.TryGetValue(dto.CategoryId, out var category)) {
            return OperationResult<List<Product>>.Failure($"Category {dto.CategoryId} not found");
         }

         products.Add(dto.ToProduct(category));
      }

      return OperationResult<List<Product>>.Success(products);
   }
}