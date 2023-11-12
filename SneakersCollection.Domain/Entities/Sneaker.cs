using SneakersCollection.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Domain.Entities
{
    public class Sneaker
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid BrandId { get; private set; }
        public Money Price { get; private set; }
        public Size SizeUS { get; private set; }
        public int Year { get; private set; }

        public Sneaker(string name, Guid brandId, Money price, Size sizeUS, int year)
        {
            Name = name;
            BrandId = brandId;
            Price = price;
            SizeUS = sizeUS;
            Year = year;
        }
    }
}
