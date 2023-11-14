namespace SneakersCollection.Api.ViewModels
{
    public class SneakerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BrandId { get; set; }
        public decimal Price { get; set; }
        public decimal SizeUS { get; set; }
        public int Year { get; set; }
    }
}
