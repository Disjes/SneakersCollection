using AutoBogus;
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
    public class SneakerControllerTests
    {
        [Fact]
        public void GetSneakerById_WithValidId_ShouldReturnSneaker()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>())).Returns(Task.FromResult(sneakerData));

            var controller = new SneakerController(mockService.Object, null);
            var validId = sneakerData.Id;

            // Act
            var result = controller.GetSneakerById(validId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeEquivalentTo(sneakerData);
        }

        [Fact]
        public void GetSneakerById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, null);
            var invalidId = Guid.NewGuid();

            // Act
            var result = controller.GetSneakerById(invalidId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void CreateSneaker_WithValidData_ShouldReturnCreatedSneaker()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<SneakerViewModel>();
            var createdSneaker = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.AddSneaker(It.IsAny<SneakersCollection.Domain.Entities.Sneaker>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(createdSneaker));

            var controller = new SneakerController(mockService.Object, null);

            // Act
            var result = await controller.CreateSneaker(sneakerData);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            result.Value.Should().BeEquivalentTo(createdSneaker);
        }

        [Fact]
        public void UpdateSneaker_WithValidData_ShouldReturnNoContent()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();
            var updatedSneaker = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();
            var mockSneakerParam = AutoFaker.Generate<SneakerViewModel>();

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(sneakerData));

            var controller = new SneakerController(mockService.Object, null);

            // Act
            var result = controller.UpdateSneaker(sneakerData.Id, mockSneakerParam);

            // Assert
            mockService.Verify(service => service.UpdateSneaker(updatedSneaker), Times.Once);
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateSneaker_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var updatedSneaker = AutoFaker.Generate<SneakerViewModel>();

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, null);


            // Act
            var result = controller.UpdateSneaker(updatedSneaker.Id, updatedSneaker);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteSneaker_WithValidId_ShouldReturnNoContent()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<SneakersCollection.Domain.Entities.Sneaker>();

            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(sneakerData));

            var controller = new SneakerController(mockService.Object, null);

            // Act
            var result = controller.DeleteSneaker(sneakerData.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteSneaker_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var mockService = new Mock<ISneakerService>();
            mockService.Setup(service => service.GetSneakerById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<SneakersCollection.Domain.Entities.Sneaker>(null));

            var controller = new SneakerController(mockService.Object, null);

            // Act
            var result = controller.DeleteSneaker(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
