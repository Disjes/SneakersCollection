using Microsoft.EntityFrameworkCore;
using SneakersCollection.Data.Entities;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sneaker>()
                .HasOne(s => s.Brand)
                .WithMany(b => b.Sneakers)
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            var nikeId = Guid.NewGuid();
            var adidasId = Guid.NewGuid();

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = nikeId, Name = "Nike" },
                new Brand { Id = adidasId, Name = "Adidas" }
            );

            modelBuilder.Entity<Sneaker>().HasData(
                new Sneaker
                {
                    Id = Guid.NewGuid(),
                    Name = "Air Max",
                    BrandId = nikeId,
                    Price = 120.00m,
                    SizeUS = 10.5m,
                    Year = 2022
                },
                new Sneaker
                {
                    Id = Guid.NewGuid(),
                    Name = "Superstar",
                    BrandId = adidasId,
                    Price = 80.00m,
                    SizeUS = 9.0m,
                    Year = 2021
                }
            );
        }
    }
}
