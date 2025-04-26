using MakerSpace.Application.Interfaces.Repositories;
using MakerSpace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace.Infrastructure.Data.Extensions;

public static class DataExtensions {
   public static async Task InitializeDbAsync(this IServiceProvider serviceProvider) {
      using var scope = serviceProvider.CreateScope();
      var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      await dbContext.Database.MigrateAsync();
   }

   public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration) {
      var connString = configuration.GetConnectionString("DefaultConnection");
      
      services.AddSqlServer<AppDbContext>(connString)
         .AddScoped<IProductRepository, EfCoreProductRepository>()
         .AddScoped<ICategoriesRepository, EfCoreCategoriesRepository>();
      
      return services;
   }
}