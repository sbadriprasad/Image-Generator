using ImageGenerator.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace ImageGenerator.Api.Repository
{
    public class AnimalPictureDbContext : DbContext
    {
        public AnimalPictureDbContext(DbContextOptions<AnimalPictureDbContext> options) : base(options)
        {
        }

        public DbSet<AnimalPicture> AnimalPictures { get; set; }
    }
}

