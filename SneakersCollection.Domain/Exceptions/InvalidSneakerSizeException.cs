using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Domain.Exceptions
{
    public class InvalidSneakerSizeException : ArgumentException
    {
        public InvalidSneakerSizeException(decimal size)
            : base($"Invalid sneaker size. Size must be between 1 and 20. Actual size: {size}")
        {
        }
    }
}
