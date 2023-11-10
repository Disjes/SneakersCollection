using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Data.Contexts
{
    public class SneakersCollectionContext : DbContext
    {
        public SneakersCollectionContext(DbContextOptions<SneakersCollectionContext> options) : base(options)
        { }

        public DbSet<Sneaker> Sneakers { get; set; }
    }
}
