using SneakersCollection.Domain.Entities;
using SneakersCollection.Domain.ValueObjects;

namespace SneakersCollection.Domain.Interfaces.Services
{
    public interface ISneakerService
    {
        // Commands
        Task<Sneaker> AddSneaker(Sneaker sneaker);

        void UpdateSneaker(Sneaker sneaker);

        void RemoveSneaker(Guid brandId, Guid sneakerId);

        // Queries
        Task<IEnumerable<Sneaker>> GetAllSneakers();
        Task<Sneaker> GetSneakerById(Guid sneakerId);
    }

}
