using SneakersCollection.Domain.ValueObjects;

namespace SneakersCollection.Domain.Entities
{
    public class Brand
    {
        private readonly List<Sneaker> sneakers = new List<Sneaker>();

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public IReadOnlyList<Sneaker> Sneakers => sneakers;

        public Brand(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public void AddSneaker(Guid brandId, string name, Money price, Size sizeUS, int year)
        {
            if (!IsSizeWithinRange(sizeUS))
            {
                throw new ApplicationException("The size is outside the allowed range for this brand.");
            }

            var sneaker = new Sneaker(name, brandId, price, sizeUS, year);
            sneakers.Add(sneaker);
        }

        private bool IsSizeWithinRange(Size size)
        {
            // Only sizes between 5 and 13 myust be allowed
            return size.NumericSize >= 5 && size.NumericSize <= 13;
        }
    }
}
