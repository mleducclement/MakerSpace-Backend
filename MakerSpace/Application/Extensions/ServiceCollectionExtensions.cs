using FluentValidation;
using MakerSpace.Application.Interfaces.Services;
using MakerSpace.Application.Products.Dtos;
using MakerSpace.Application.Products.Services;
using MakerSpace.Application.Products.Validators;

namespace MakerSpace.Application.Extensions;

public static class ServiceCollectionExtensions {
   
   // Register all application layer services here (NO INFRA)
   public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
      services
         .AddScoped<IValidator<ProductMutateDto>, ProductMutateDtoValidator>()
         .AddScoped<IProductsService, ProductsService>();
      return services;
   }
}