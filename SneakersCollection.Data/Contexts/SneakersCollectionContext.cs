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

        public DbSet<Entities.Brand> Brands { get; set; }
        public DbSet<Entities.Sneaker> Sneakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Sneaker>()
                .HasOne(s => s.Brand)
                .WithMany(b => b.Sneakers)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
