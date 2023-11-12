using SneakersCollection.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Domain.Interfaces.Repositories
{
    public interface ISneakerRepository
    {
        Task<Sneaker> Create(Sneaker sneaker);
        Task<Sneaker> GetById(Guid id);
        Task<IEnumerable<Sneaker>> GetAll();
        Task<Sneaker> Update(Sneaker sneaker);
        void Delete(int id);
    }
}
