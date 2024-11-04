using ImageGenerator.Api.Model;
using ImageGenerator.Api.Repository;
using ImageGenerator.Api.Service;
using ImageGenerator.Tests.Handler;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ImageGenerator.Tests
{
    public class AnimalPictureServiceTests : IDisposable
    {
        private readonly AnimalPictureDbContext _inMemoryContext;
        private readonly HttpClient _mockHttpClient;

        public AnimalPictureServiceTests()
        {
            // Set up the in-memory database context
            var options = new DbContextOptionsBuilder<AnimalPictureDbContext>()
                .UseInMemoryDatabase(databaseName: "AnimalPictureTestDb_" + Guid.NewGuid())
                .Options;
            _inMemoryContext = new AnimalPictureDbContext(options);

            // Set up a mock HttpClient
            var handler = new MockHttpMessageHandler(request =>
                Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Mock Image Content")
                }));
            _mockHttpClient = new HttpClient(handler);
        }

        [Fact]
        public async Task FetchAndSavePictures_ShouldSaveAnimalPictures()
        {
            // Arrange
            var service = new AnimalPictureService(_inMemoryContext, _mockHttpClient);
            var animalType = "cat";
            var count = 2;
            //_inMemoryContext.Database.EnsureDeleted();

            // Act
            var result = await service.FetchAndSavePictures(animalType, count);

            // Assert
            Assert.Equal(count, result.Count);
            Assert.All(result, r => Assert.Equal(animalType, r.AnimalType));
            Assert.All(result, r => Assert.Equal(DateTimeKind.Utc, r.CreatedDateTimeUtc.Kind));
            var savedPictures = await _inMemoryContext.AnimalPictures.ToListAsync();
            Assert.Equal(count, savedPictures.Count); // Check that records were saved in the in-memory db
        }

        [Fact]
        public async Task FetchAndSavePictures_InvalidAnimalType()
        {
            //Arrange
            var service = new AnimalPictureService(_inMemoryContext, _mockHttpClient);
            var animalType = "tiger";
            var count = 2;


            //Act & assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.FetchAndSavePictures(animalType, count));
        }

        [Theory]
        [InlineData("cat")]
        public async Task GetLastSavedPicture_ReturnPicture(string animalType)
        {
            // Arrange
            var service = new AnimalPictureService(_inMemoryContext, _mockHttpClient);
            LoadAnimalPictureToContext();

            //Act
            var result = await service.GetLastSavedPicture(animalType);

            //Assert
            Assert.NotNull(result);

        }

        [Theory]
        [InlineData("rat")]
        public async Task GetLastSavedPicture_ReturnNoPicture(string animalType)
        {
            // Arrange
            var service = new AnimalPictureService(_inMemoryContext, _mockHttpClient);
            LoadAnimalPictureToContext();

            //Act
            var result = await service.GetLastSavedPicture(animalType);

            Assert.Null(result);
        }

        private void LoadAnimalPictureToContext()
        {
            var animalPicture = new AnimalPicture
            {
                AnimalType = "cat",
                ImageUrl = "test.jpeg",
                Id = 1,
                CreatedDateTimeUtc = DateTime.UtcNow
            };
            _inMemoryContext.Add(animalPicture);
            _ = _inMemoryContext.SaveChangesAsync();

        }

        public void Dispose()
        {
            _inMemoryContext?.Dispose();
        }
    }
}