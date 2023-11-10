using AutoMapper;
using SneakersCollection.Data.Contexts;
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

        public DomainEntities.Sneaker Create(DomainEntities.Sneaker sneaker)
        {
            var dataSneaker = _mapper.Map<Sneaker>(sneaker);
            _context.Sneakers.Add(dataSneaker);
            _context.SaveChanges();
            return _mapper.Map<DomainEntities.Sneaker>(dataSneaker); ;
        }

        public DomainEntities.Sneaker GetById(int id)
        {
            var dataSneaker = _context.Sneakers.Find(id);
            return _mapper.Map<DomainEntities.Sneaker>(dataSneaker);
        }

        public IEnumerable<DomainEntities.Sneaker> GetAll()
        {
            var dataSneakers = _context.Sneakers.ToList();
            return _mapper.Map<IEnumerable<DomainEntities.Sneaker>>(dataSneakers);
        }

        public DomainEntities.Sneaker Update(DomainEntities.Sneaker sneaker)
        {
            var dataSneaker = _mapper.Map<Sneaker>(sneaker);
            _context.Sneakers.Update(dataSneaker);
            _context.SaveChanges();
            return _mapper.Map<DomainEntities.Sneaker>(dataSneaker);
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
