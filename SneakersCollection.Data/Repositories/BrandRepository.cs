using AutoMapper;
using SneakersCollection.Data.Contexts;
using SneakersCollection.Domain.Entities;

namespace SneakersCollection.Data.Repositories
{
    public class BrandRepository
    {
        private readonly SneakersCollectionContext dbContext;
        private readonly IMapper _mapper;

        public BrandRepository(SneakersCollectionContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public Brand Create(Brand brand)
        {
            var dataBrand = _mapper.Map<Entities.Brand>(brand);
            dbContext.Brands.Add(dataBrand);
            dbContext.SaveChanges();
            return brand;
        }

        public async Task<Brand> GetById(Guid id)
        {
            var brand = await dbContext.Brands.FindAsync(id);
            var domainBrand = _mapper.Map<Brand>(brand);
            return domainBrand;
        }

        public IEnumerable<Brand> GetAll()
        {
            var list = _mapper.Map<List<Brand>>(dbContext.Brands.ToList());
            return list;
        }

        public Brand Update(Brand brand)
        {
            var dataBrand = _mapper.Map<Entities.Brand>(brand);
            dbContext.Brands.Update(dataBrand);
            dbContext.SaveChanges();
            return brand;
        }

        public void Delete(Guid id)
        {
            var brand = dbContext.Brands.Find(id);
            if (brand != null)
            {
                dbContext.Brands.Remove(brand);
                dbContext.SaveChanges();
            }
        }
    }
}
