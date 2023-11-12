using AutoBogus;
using FluentAssertions;
using SneakersCollection.Domain.Entities;
using SneakersCollection.Domain.ValueObjects;

namespace SneakersCollection.Tests.Unit.Domain
{
    public class SneakerEntityTests
    {
        [Fact]
        public void CreateSneaker_WithValidData_ShouldCreateSneaker()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<Sneaker>();
            Money price = new Money(sneakerData.Price.Amount, sneakerData.Price.Currency);
            Size sizeUS = new Size(sneakerData.SizeUS.NumericSize, sneakerData.SizeUS.SizeSystem);

            // Act
            var sneaker = new Sneaker(sneakerData.Name, sneakerData.BrandId, price, sizeUS, sneakerData.Year);

            // Assert
            sneaker.Name.Should().Be(sneakerData.Name);
            sneaker.BrandId.Should().Be(sneakerData.BrandId);
            sneaker.Price.Should().Be(price);
            sneaker.SizeUS.Should().Be(sizeUS);
            sneaker.Year.Should().Be(sneakerData.Year);
        }

        [Fact]
        public void CreateSneaker_WithInvalidPrice_ShouldThrowException()
        {
            // Arrange
            string invalidCurrency = "InvalidCurrency";

            // Act & Assert
            Action action = () => new Money(100.0M, invalidCurrency);
            action.Should().Throw<ArgumentException>().WithMessage("Invalid currency.");
        }

        [Fact]
        public void CreateSneaker_WithInvalidSizeUS_ShouldThrowException()
        {
            // Arrange
            string invalidUnit = "InvalidUnit";

            // Act & Assert
            Action action = () => new Size(10.5M, invalidUnit);
            action.Should().Throw<ArgumentException>().WithMessage("Invalid unit.");
        }

        [Fact]
        public void UpdateSneaker_WithNullName_ShouldThrowException()
        {
            // Arrange
            var sneakerData = AutoFaker.Generate<Sneaker>();
            var sneaker = new Sneaker(sneakerData.Name, sneakerData.BrandId, new Money(50.0M, "USD"), new Size(9.5M, "US"), 2022);

            // Act & Assert
            //Action action = () => sneaker.Upda(null, sneakerData.BrandId, new Money(75.0M, "USD"), new Size(10.0M, "US"), 2023);
            //action.Should().Throw<ArgumentNullException>().WithMessage("Name cannot be null.");
        }
    }
}