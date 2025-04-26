namespace MakerSpace.Application.Extensions;

public static class SwaggerExtensions {
   public static IServiceCollection AddMakerSpaceSwagger(this IServiceCollection services) {
      services.AddSwaggerGen(c => {
         c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
            Title = "MakerSpace API",
            Version = "v1",
            Description = "API for MakerSpace application"
         });
      });
         
      return services;
   }

   public static IApplicationBuilder UseMakerSpaceSwagger(this IApplicationBuilder app) {
      app.UseSwagger();
      app.UseSwaggerUI(c => {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "MakerSpace API V1");
         c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
      });

      return app;
   }
}