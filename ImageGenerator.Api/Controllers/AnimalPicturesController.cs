using ImageGenerator.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageGenerator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalPicturesController : ControllerBase
    {
        public IAnimalPictureService service { get; set; }

        public AnimalPicturesController(IAnimalPictureService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetLastAnimalPicture(string animalType)
        {
            var picture = await service.GetLastSavedPicture(animalType);
            if (picture == null)
                return NotFound();

            return Ok(picture);
        }

        // POST /api/animalpictures/cat?count=3
        [HttpPost("{animalType}")]
        public async Task<IActionResult> SaveAnimalPictures(string animalType, int count)
        {
            var savedPictures = await service.FetchAndSavePictures(animalType, count);
            return Ok(savedPictures);
        }
    }
}
