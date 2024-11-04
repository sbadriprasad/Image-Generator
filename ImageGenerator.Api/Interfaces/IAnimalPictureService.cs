using ImageGenerator.Api.Model;

namespace ImageGenerator.Api.Interfaces
{
    public interface IAnimalPictureService
    {

        Task<List<AnimalPicture>> FetchAndSavePictures(string animalType, int count);

        Task<AnimalPicture?> GetLastSavedPicture(string animalType);
    }
}