using ImageGenerator.Api.Interfaces;
using ImageGenerator.Api.Model;
using ImageGenerator.Api.Repository;
using Microsoft.EntityFrameworkCore;

namespace ImageGenerator.Api.Service
{
    public class AnimalPictureService : IAnimalPictureService
    {
        private AnimalPictureDbContext Context { get; set; }
        private HttpClient HttpClient { get; set; }
        private bool IsRunningFromUnit;

        public AnimalPictureService(AnimalPictureDbContext context, HttpClient httpClient)
        {
            Context = context;
            HttpClient = httpClient;
            IsRunningFromUnit = AppDomain.CurrentDomain.GetAssemblies().Any(
            static a => a.FullName.StartsWith("xunit", StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<List<AnimalPicture>> FetchAndSavePictures(string animalType, int count)
        {
            var savedPictures = new List<AnimalPicture>();
            var apiHost = animalType switch
            {
                "cat" => "https://placekitten.com/",
                "dog" => "https://place.dog/",
                "bear" => "https://placebear.com/",
                _ => throw new ArgumentException("Invalid Animal Type")
            };

            for (int i = 0; i < count; i++)
            {
                int[] numbers = [100, 200, 300, 400, 500];
                Random rd = new();

                int height = numbers[rd.Next(numbers.Length)];
                int width = numbers[rd.Next(numbers.Length)];

                var apiUrl = apiHost + width + "/" + height;

                if (!IsRunningFromUnit)
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },
                        UseProxy = true
                    };
                    HttpClient = new HttpClient(handler);
                }

                var response = await HttpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var picture = new AnimalPicture { AnimalType = animalType, ImageUrl = apiUrl, CreatedDateTimeUtc = DateTime.UtcNow };

                    Context.AnimalPictures.Add(picture);
                    savedPictures.Add(picture);
                }
            }

            await Context.SaveChangesAsync();
            return savedPictures;
        }

        public async Task<AnimalPicture?> GetLastSavedPicture(string animalType)
        {
            //TODO
            return await Context.AnimalPictures.OrderByDescending(p => p.CreatedDateTimeUtc)
                .FirstOrDefaultAsync(p => p.AnimalType == animalType);
        }
    }

}
