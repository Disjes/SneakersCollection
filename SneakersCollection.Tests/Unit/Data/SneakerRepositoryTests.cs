using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SneakersCollection.Data.Contexts;
using SneakersCollection.Data.Entities;
using SneakersCollection.Data.Repositories;

namespace SneakersCollection.Tests.Unit.Data
{
    public class SneakerRepositoryTests
    {
        [Fact]
        public void GetById_ValidId_ReturnsSneaker()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var expectedSneaker = AutoFaker.Generate<Sneaker>();
            var sneakerData = new List<Sneaker> { expectedSneaker }.AsQueryable();

            var dbSetMock = new Mock<DbSet<Sneaker>>();
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.Provider).Returns(sneakerData.Provider);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.Expression).Returns(sneakerData.Expression);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.ElementType).Returns(sneakerData.ElementType);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.GetEnumerator()).Returns(() => sneakerData.GetEnumerator());

            dbContextMock.Setup(c => c.Sneakers).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetById(expectedSneaker.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedSneaker, options => options.ExcludingMissingMembers());
            // Add more assertions based on your actual implementation
        }

        [Fact]
        public void GetById_InvalidId_ReturnsNull()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var dbSetMock = new Mock<DbSet<Sneaker>>();
            dbContextMock.Setup(c => c.Sneakers).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetById(Guid.Empty);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetAll_ReturnsAllSneakers()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var expectedSneakers = AutoFaker.Generate<List<Sneaker>>();
            var sneakerData = expectedSneakers.AsQueryable();

            var dbSetMock = new Mock<DbSet<Sneaker>>();
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.Provider).Returns(sneakerData.Provider);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.Expression).Returns(sneakerData.Expression);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.ElementType).Returns(sneakerData.ElementType);
            dbSetMock.As<IQueryable<Sneaker>>().Setup(m => m.GetEnumerator()).Returns(() => sneakerData.GetEnumerator());

            dbContextMock.Setup(c => c.Sneakers).Returns(dbSetMock.Object);

            // Act
            var result = repository.GetAll();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedSneakers, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public void Add_NewSneaker_SneakerAddedSuccessfully()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var newSneaker = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();

            // Act
            repository.Create(newSneaker);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ExistingSneaker_SneakerUpdatedSuccessfully()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var existingSneaker = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();

            // Act
            repository.Update(existingSneaker);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ExistingSneaker_SneakerDeletedSuccessfully()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var sneakerIdToDelete = AutoFaker.Generate<int>();

            // Act
            repository.Delete(sneakerIdToDelete);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once); 
        }

        [Fact]
        public void Delete_NonExistingSneaker_DoesNothing()
        {
            // Arrange
            var dbContextMock = new Mock<SneakersCollectionContext>();
            var mapper = new Mock<IMapper>();
            var repository = new SneakerRepository(dbContextMock.Object, mapper.Object);

            var nonExistingSneakerId = AutoFaker.Generate<int>();

            // Act
            repository.Delete(nonExistingSneakerId);

            // Assert
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
        }
    }
}
