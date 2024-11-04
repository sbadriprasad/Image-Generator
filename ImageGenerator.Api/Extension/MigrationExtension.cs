using ImageGenerator.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace ImageGenerator.Api.Extension
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AnimalPictureDbContext dbContext = scope.ServiceProvider.GetRequiredService<AnimalPictureDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
