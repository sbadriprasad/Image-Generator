using ImageGenerator.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

