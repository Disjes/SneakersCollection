using SneakersCollection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Domain.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Brand Create(Brand brand);
        Task<Brand> GetById(Guid id);
        IEnumerable<Brand> GetAll();
        Brand Update(Brand brand);
        void Delete(Guid id);
    }
}
