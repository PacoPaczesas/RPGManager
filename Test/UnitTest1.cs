using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Threading.Tasks;
using Xunit;

namespace RPGManager.WarstwaWprowadzania.Test
{
    public class CountryServiceTests
    {
        [Fact]
        public async Task Test1()
        {
            //Arrange
            var validatorMock = new Mock<IValidator<Country>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<Country>()))
                .Returns(() => new Result<Country> { IsSuccessful = true });

            var dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(context => context.Countries.AddAsync(It.IsAny<Country>(), It.IsAny<CancellationToken>()))
                .Returns(() => new ValueTask<EntityEntry<Country>>());

            var service = new CountryService(dataContextMock.Object, validatorMock.Object);

            //Act
            var result = await service.AddCountryAsync(new CountryDto()
            {
                Name = "test",
                Capital = "TestCapital"
            });

            //Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeTrue();
            dataContextMock.Verify(context => context.SaveChanges(), Times.Exactly(1));
        }

        //czy walidacja nie przejdzie dla kraju bez nazwy
        [Fact]
        public async Task AddCountryWithEmptyName()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<Country>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<Country>()))
                .Returns(() => new Result<Country> { IsSuccessful = false, Message = "Brak nazwy kraju" });

            var dataContextMock = new Mock<IDataContext>();
            var service = new CountryService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AddCountryAsync(new CountryDto
            {
                Name = "",
                Capital = "TestCapital"
            });

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("Brak nazwy kraju");
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never());
        }

        //czy walidacja nie przejdzie dla kraju bez nazwy
        [Fact]
        public async Task AddCountryWithEmptyCapital()
        {
            // Arrange
            var validatorMock = new Mock<IValidator<Country>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<Country>()))
                .Returns(() => new Result<Country> { IsSuccessful = false, Message = "Brak wprowadzonej nazwy stolicy kraju" });

            var dataContextMock = new Mock<IDataContext>();
            var service = new CountryService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AddCountryAsync(new CountryDto
            {
                Name = "CountryName",
                Capital = ""         
            });

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful.Should().BeFalse();
            result.Message.Should().Be("Brak wprowadzonej nazwy stolicy kraju");
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never());
        }




        //czy metoda usuniêcia kraju dzia³a prawid³owo
        [Fact]
        public async Task DeleteCountry()
        {
            // Arrange
            var country = new Country { Id = 1, Name = "TestCountry" };

            var dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(context => context.Countries.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(country);

            var service = new CountryService(dataContextMock.Object, Mock.Of<IValidator<Country>>());

            // Act
            var result = await service.DeleteCountryAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            dataContextMock.Verify(context => context.Countries.Remove(It.IsAny<Country>()), Times.Once());
            dataContextMock.Verify(context => context.SaveChanges(), Times.Once());
        }

        // czy usuniêcie kraju, który nie istnieje, zwraca null
        [Fact]
        public async Task DeleteNotExistingCountry()
        {
            // Arrange
            var dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(context => context.Countries.FindAsync(It.IsAny<int>()))
                .ReturnsAsync((Country)null); // Zwracamy null, gdy kraj nie istnieje

            var service = new CountryService(dataContextMock.Object, Mock.Of<IValidator<Country>>());

            // Act
            var result = await service.DeleteCountryAsync(999); // U¿ywamy nieistniej¹cego ID

            // Assert
            result.Should().BeNull();
            dataContextMock.Verify(context => context.Countries.Remove(It.IsAny<Country>()), Times.Never());
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never());
        }

    }
}