using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SneakersCollection.Api.Controllers;
using SneakersCollection.Api.ViewModels;
using SneakersCollection.Data.Entities;
using SneakersCollection.Domain.Interfaces.Repositories;
using SneakersCollection.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakersCollection.Tests.Unit.Api
{
    public class SneakerControllerTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public SneakerControllerTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void GetSneakerById_WithValidId_ShouldReturnSneaker()
        {
            // Arrange
            var sneaker = new AutoFaker<Sneaker>()
                .RuleFor(fake => fake.SizeUS, fake => fake.Random.Decimal(5, 13))
                .Generate();
            var domainSneaker = _fixture.Mapper.Map<Domain.Entities.Sneaker>(sneaker);
            var sneakerViewModel = _fixture.Mapper.Map<SneakerViewModel>(domainSneaker);

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>())).Returns(Task.FromResult(domainSneaker));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);
            var validId = domainSneaker.Id;

            // Act
            var result = await controller.GetSneakerById(validId);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            (result.Result as OkObjectResult).Value.Should().BeEquivalentTo(sneakerViewModel);
        }

        [Fact]
        public async void GetSneakerById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, null);
            var invalidId = Guid.NewGuid();

            // Act
            var result = await controller.GetSneakerById(invalidId);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void CreateSneaker_WithValidData_ShouldReturnCreatedSneaker()
        {
            // Arrange
            var createdSneaker = new AutoFaker<Sneaker>()
                .RuleFor(fake => fake.SizeUS, fake => fake.Random.Decimal(5, 13))
                .Generate();
            var createdDomainSneaker = _fixture.Mapper.Map<Domain.Entities.Sneaker>(createdSneaker);
            var sneakerViewModel = _fixture.Mapper.Map<SneakerViewModel>(createdDomainSneaker);

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.AddSneaker(It.IsAny<SneakersCollection.Domain.Entities.Sneaker>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(createdDomainSneaker));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);

            // Act
            var result = await controller.CreateSneaker(sneakerViewModel);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            (result.Result as CreatedAtActionResult).Value.Should().BeEquivalentTo(sneakerViewModel);
        }

        [Fact]
        public async void UpdateSneaker_WithValidData_ShouldReturnNoContent()
        {
            // Arrange
            var updatedSneaker = new AutoFaker<Sneaker>()
                .RuleFor(fake => fake.SizeUS, fake => fake.Random.Decimal(5, 13))
                .Generate();
            var domainSneaker = _fixture.Mapper.Map<Domain.Entities.Sneaker>(updatedSneaker);
            var sneakerViewModel = _fixture.Mapper.Map<SneakerViewModel>(domainSneaker);

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(domainSneaker));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);

            // Act
            var result = await controller.UpdateSneaker(domainSneaker.Id, sneakerViewModel);

            // Assert
            mockService.Verify(service => service.UpdateSneaker(domainSneaker), Times.Once);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void UpdateSneaker_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var updatedSneaker = new AutoFaker<Sneaker>()
                .RuleFor(fake => fake.SizeUS, fake => fake.Random.Decimal(5, 13))
                .Generate();
            var domainSneaker = _fixture.Mapper.Map<Domain.Entities.Sneaker>(updatedSneaker);
            var sneakerViewModel = _fixture.Mapper.Map<SneakerViewModel>(domainSneaker);

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);


            // Act
            var result = await controller.UpdateSneaker(updatedSneaker.Id, sneakerViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void DeleteSneaker_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            var sneakerData = new AutoFaker<Sneaker>()
                .RuleFor(fake => fake.SizeUS, fake => fake.Random.Decimal(5, 13))
                .Generate();
            var domainSneaker = _fixture.Mapper.Map<Domain.Entities.Sneaker>(sneakerData);

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(domainSneaker));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);

            // Act
            var result = await controller.DeleteSneaker(sneakerData.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async void DeleteSneaker_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, _fixture.Mapper);

            // Act
            var result = await controller.DeleteSneaker(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
