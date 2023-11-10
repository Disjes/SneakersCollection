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
        // Create a new sneaker.
        Sneaker Create(Sneaker sneaker);

        // Retrieve a sneaker by its unique ID.
        Sneaker GetById(int id);

        // Retrieve a list of all sneakers.
        IEnumerable<Sneaker> GetAll();

        // Update an existing sneaker.
        Sneaker Update(Sneaker sneaker);

        // Delete a sneaker by its unique ID.
        void Delete(int id);
    }
}
