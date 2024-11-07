using Alfa.CarRental.Infrastructure;
using Alfa.CarRental.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Alfa.CarRental.WebApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            { 
                IServiceProvider service = scope.ServiceProvider;
                ILoggerFactory loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try 
                {
                    ApplicationDbContext context = service.GetRequiredService<ApplicationDbContext>();

                    context.Database.Migrate();
                }
                catch(Exception ex) 
                {
                    ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

                    logger.LogError(ex, "Migration error");
                }
            }
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
