namespace ImageGenerator.Api.Model
{
    public class AnimalPicture
    {
        public int Id { get; set; }

        public required string AnimalType { get; set; }

        public required string ImageUrl { get; set; }

        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
