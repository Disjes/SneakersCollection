using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Data.Entities
{
    public class Brand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public virtual ICollection<Sneaker> Sneakers { get; set; }
    }
}
