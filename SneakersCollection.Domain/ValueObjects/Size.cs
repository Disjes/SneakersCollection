using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Domain.ValueObjects
{
    public class Size
    {
        public decimal NumericSize { get; private set; }
        public string SizeSystem { get; private set; }

        private static readonly List<string> AcceptedSizeSystems = new List<string>
        {
            "US",
            "EU",
            "UK"
        };

        private static readonly Dictionary<string, (decimal Min, decimal Max)> SizeLimits = new Dictionary<string, (decimal, decimal)>
        {
            { "US", (1.0m, 20.0m) },
            { "EU", (1.0m, 20.0m) },
            { "UK", (1.0m, 20.0m) }
        };

        public Size(decimal numericSize, string sizeSystem)
        {
            if (!AcceptedSizeSystems.Contains(sizeSystem))
            {
                throw new ArgumentException("Invalid size system.");
            }

            if (numericSize < SizeLimits[sizeSystem].Min || numericSize > SizeLimits[sizeSystem].Max)
            {
                throw new ArgumentException($"Size must be between {SizeLimits[sizeSystem].Min} and {SizeLimits[sizeSystem].Max} for {sizeSystem} system.");
            }

            NumericSize = numericSize;
            SizeSystem = sizeSystem;
        }
    }
}
