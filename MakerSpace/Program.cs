using MakerSpace.Application.Extensions;
using MakerSpace.Infrastructure.Data.Extensions;

namespace MakerSpace;

public class Program {
   public static async Task Main(string[] args) {
      var builder = WebApplication.CreateBuilder(args);

      builder.Configuration
         .AddEnvironmentVariables();

      builder.Services.AddControllers();

      builder.Services
         .AddEndpointsApiExplorer()
         .AddRepositories(builder.Configuration)
         .AddApplicationServices()
         .AddMakerSpaceSwagger();

      var app = builder.Build();

      await app.Services.InitializeDbAsync();

      if (app.Environment.IsDevelopment()) {
         app.UseMakerSpaceSwagger();
      }

      app.UseRouting();
      app.MapControllers();
      await app.RunAsync();
   }
}