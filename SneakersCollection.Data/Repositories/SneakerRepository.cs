using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SneakersCollection.Data.Contexts;
using SneakersCollection.Domain.Entities;
using SneakersCollection.Domain.Interfaces.Repositories;
using DomainEntities = SneakersCollection.Domain.Entities;

namespace SneakersCollection.Data.Repositories
{
    public class SneakerRepository : ISneakerRepository
    {
        private readonly SneakersCollectionContext _context;
        private readonly IMapper _mapper;

        public SneakerRepository(SneakersCollectionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Sneaker> Create(Sneaker sneaker)
        {
            var dataSneaker = _mapper.Map<Entities.Sneaker>(sneaker);
            _context.Sneakers.Add(dataSneaker);
            await _context.SaveChangesAsync();
            var createdSneaker = _mapper.Map<Sneaker>(dataSneaker);
            return createdSneaker;
        }

        public async Task<Sneaker> GetById(Guid id)
        {
            var dataSneaker = _context.Sneakers.AsNoTracking().FirstOrDefault(s => s.Id == id);
            return _mapper.Map<Sneaker>(dataSneaker);
        }

        public async Task<IEnumerable<Sneaker>> GetAll()
        {
            var sneakers = _context.Sneakers.ToList();
            return _mapper.Map<IEnumerable<Sneaker>>(sneakers);
        }

        public async Task<Sneaker> Update(Sneaker sneaker)
        {
            var dataSneaker = _mapper.Map<Entities.Sneaker>(sneaker);
            _context.Sneakers.Update(dataSneaker);
            await _context.SaveChangesAsync();
            return _mapper.Map<Sneaker>(dataSneaker);
        }

        public void Delete(int id)
        {
            var sneaker = _context.Sneakers.Find(id);
            if (sneaker != null)
            {
                _context.Sneakers.Remove(sneaker);
                _context.SaveChanges();
            }
        }
    }
}
