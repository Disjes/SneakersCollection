using SneakersCollection.Domain.Entities;
using SneakersCollection.Domain.Interfaces.Repositories;
using SneakersCollection.Domain.Interfaces.Services;
using SneakersCollection.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Application.Services
{
    public class SneakerService : ISneakerService
    {
        private readonly IBrandRepository brandRepository;
        private readonly ISneakerRepository sneakerRepository;

        public SneakerService(IBrandRepository brandRepository, ISneakerRepository sneakerRepository)
        {
            this.brandRepository = brandRepository;
            this.sneakerRepository = sneakerRepository;
        }

        // Commands
        public async Task<Sneaker> AddSneaker(Sneaker sneaker)
        {
            var brand = await brandRepository.GetById(sneaker.BrandId);

            if (brand == null)
            {
                throw new ApplicationException("Brand not found.");
            }

            brand.AddSneaker(sneaker.BrandId, sneaker.Name, sneaker.Price, sneaker.SizeUS, sneaker.Year);

            var createdSneaker = await sneakerRepository.Create(sneaker);

            return createdSneaker;
        }

        public async void UpdateSneaker(Sneaker sneaker)
        {
            var brand = await brandRepository.GetById(sneaker.BrandId);

            if (brand == null)
            {
                throw new ApplicationException("Brand not found");
            }

            var sneakerToBeUpdated = brand.Sneakers.FirstOrDefault(s => s.Id == sneaker.Id);

            if (sneaker == null)
            {
                throw new ApplicationException("Sneaker not found");
            }

            //TODO: Call Brand.UpdateSneaker();
            // Save changes to the repository
            await sneakerRepository.Update(sneaker);
        }

        public async void RemoveSneaker(Guid brandId, Guid sneakerId)
        {
            var brand = await brandRepository.GetById(brandId);

            if (brand == null)
            {
                // Handle the case where the brand is not found
                throw new ApplicationException("Brand not found");
            }

            var sneaker = brand.Sneakers.FirstOrDefault(s => s.Id == sneakerId);

            if (sneaker == null)
            {
                // Handle the case where the sneaker is not found
                throw new ApplicationException("Sneaker not found");
            }

            // Remove the sneaker from the brand
            //brand.RemoveSneaker(sneakerId);

            // Save changes to the repository
            brandRepository.Delete(brand.Id);
        }

        // Queries
        public async Task<IEnumerable<Sneaker>> GetAllSneakers()
        {
            var sneakersList = await sneakerRepository.GetAll();
            return sneakersList;
        }

        public async Task<Sneaker> GetSneakerById(Guid sneakerId)
        {
            var sneaker = await sneakerRepository.GetById(sneakerId);
            return sneaker;
        }
    }
}
