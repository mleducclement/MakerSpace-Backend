using MakerSpace.Data;
using Microsoft.EntityFrameworkCore;

namespace MakerSpace;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<AppDbContext>(opt => {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            opt.UseSqlServer(connectionString);
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "MakerSpace API",
                Version = "v1",
                Description = "API for MakerSpace application"
            });
        });
        
        var app = builder.Build();

        using (var scope = app.Services.CreateScope()) {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MakerSpace API V1");
                c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
            });
        }
        
        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}
