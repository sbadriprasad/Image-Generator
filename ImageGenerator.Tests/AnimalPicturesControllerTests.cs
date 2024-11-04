using ImageGenerator.Api.Controllers;
using ImageGenerator.Api.Interfaces;
using ImageGenerator.Api.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ImageGenerator.Tests
{
    public class AnimalPicturesControllerTests
    {
        private readonly AnimalPicturesController controller;
        private readonly Mock<IAnimalPictureService> mockService;

        public AnimalPicturesControllerTests()
        {
            mockService = new Mock<IAnimalPictureService>();
            controller = new AnimalPicturesController(mockService.Object);
        }

        [Fact]
        public async Task GetLastAnimalPicture_ReturnsOkResult_WhenPictureExists()
        {
            // Arrange
            var animalType = "cat";
            var picture = new AnimalPicture { AnimalType = animalType, ImageUrl = "http://abc.com/cat.jpeg" };
            mockService.Setup(service => service.GetLastSavedPicture(animalType))
                        .ReturnsAsync(picture);

            // Act
            var result = await controller.GetLastAnimalPicture(animalType);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(picture, okResult.Value);
        }

        [Fact]
        public async Task SaveAnimalPictures_ReturnsOkResult_WithSavedPictures()
        {
            // Arrange
            var animalType = "dog";
            var count = 2;
            var savedPictures = new List<AnimalPicture>
            {
                new() { AnimalType = animalType, ImageUrl = "http://abc.com/dog.jpeg" },
                new() { AnimalType = animalType, ImageUrl = "http://abc.com/dog.jpeg" }
            };

            mockService.Setup(service => service.FetchAndSavePictures(animalType, count))
                        .ReturnsAsync(savedPictures);

            // Act
            var result = await controller.SaveAnimalPictures(animalType, count);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(savedPictures, okResult.Value);
        }
    }
}
