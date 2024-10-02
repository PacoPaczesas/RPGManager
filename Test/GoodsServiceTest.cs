using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
using Moq;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services;
using RPGManager.WarstwaWprowadzania.Validators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

/*
CZY TO S¥ TESTY JEDNOSKOWE?
MOCK?
*/

namespace RPGManager.WarstwaWprowadzania.Test
{
    public class GoodsServiceTest
    {
        /*        [Fact]
                public async Task IsAddCountryWorkingRight()
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
                [Theory]
                [InlineData(null)]
                [InlineData("")]
                [InlineData(" ")]

                public async Task AddCountryWithEmptyName(string nameToTest)
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
                        Name = nameToTest,
                        Capital = "TestCapital"
                    });

                    // Assert
                    result.Should().NotBeNull();
                    result.IsSuccessful.Should().BeFalse();
                    result.Message.Should().Be("Brak nazwy kraju");
                    dataContextMock.Verify(context => context.SaveChanges(), Times.Never());
                }

                //czy walidacja nie przejdzie dla kraju bez nazwy stolicy
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
                }*/

        //1111111111111111111111111111111111111111111111111111111111111111111111111111

        /*        [Fact]      
                public async Task AssignGoodsToCountryAsyncWhenCountryIsNull()
                {
                    // Arrange
                    int CountryId = 1;
                    int goodId = 1;

                    var dataContextMock = new Mock<IDataContext>();
                    // setup tak, by dataContextMock zawiera³ zarówno jakieœ country oraz goods. To nie mog¹ byæ nulle -> rezultat jest true
                    // je¿eli rezultat testu mia³ by byæ false to wtedy jedno z dwóch powy¿szych mo¿e byæ np. null.
                    dataContextMock.Setup(context => context.Countries.FindAsync(It.IsAny<int>()))
                         .ReturnsAsync(country);
                    var validatorMock = new Mock<IValidator<Goods>>();

                    var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

                    // Act
                    var result = await service.AssignGoodsToCountryAsync(CountryId, goodId);

                    // Assert
                    result.Should().BeTrue();

                }
        */


        // !!!!!!!!!!!!!!!!!!!
        // AddNewGoodsAsync()
        // !!!!!!!!!!!!!!!!!!!

        // dla AddNewGoodsAsync powuinny byæ tytlko dwie œcierzki. IsSuccessful true oraz flase. Wszystko inne sprawdzane bêdzie przy validatorze



        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task AddNewGoodsAsync_WhenNameIsInvalid(string myData)
        {
            // Arrange
            var goodsDto = new GoodsDto
            {
                Name = myData,
                Price = 100.0
            };

            var validatorMock = new Mock<IValidator<Goods>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<Goods>()))
                .Returns(new Result<Goods> { IsSuccessful = false, Message = "Nazwa nie mo¿e byæ pusta" }); // Walidacja nieudana

            var dataContextMock = new Mock<IDataContext>();

            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AddNewGoodsAsync(goodsDto);

            // Assert
            result.IsSuccessful.Should().BeFalse(); // Walidacja powinna siê nie udaæ
            result.Message.Should().Be("Nazwa nie mo¿e byæ pusta"); // Powinno zwróciæ odpowiedni komunikat
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never()); // Zapis nie powinien zostaæ wywo³any
        }


        [Fact]
        public async Task AddNewGoodsAsync_WhenValidationFails()
        {
            // Arrange
            var goodsDto = new GoodsDto
            {
                Name = "TestGoods",
                Price = 100.0
            };

            var validatorMock = new Mock<IValidator<Goods>>();
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<Goods>()))
                .Returns(new Result<Goods> { IsSuccessful = false}); // Walidacja nieudana

            var dataContextMock = new Mock<IDataContext>();

            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AddNewGoodsAsync(goodsDto);

            // Assert
            result.IsSuccessful.Should().BeFalse(); // Wynik walidacji powinien byæ negatywny
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never()); // Zapis do bazy nie powinien zostaæ wywo³any
        }



        [Fact]
        public async Task AddNewGoodsAsync_WhenPriceIsNegative()
        {
            // Arrange
            var goodsDto = new GoodsDto
            {
                Name = "TestGoods",
                Price = -10.0
            };

            var validatorMock = new Mock<IValidator<Goods>>();
            validatorMock
                //TODO: PO CO JEST SETUP?
                .Setup(validator => validator.Validate(It.IsAny<Goods>()))
                .Returns(new Result<Goods> { IsSuccessful = false, Message = "Ujemna cena" });

            var dataContextMock = new Mock<IDataContext>();

            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AddNewGoodsAsync(goodsDto);

            // Assert
            result.IsSuccessful.Should().BeFalse(); // Walidacja powinna siê nie udaæ
            result.Message.Should().Be("Ujemna cena"); // Powinno zwróciæ odpowiedni komunikat
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never()); // Zapis do bazy nie powinien zostaæ wywo³any
        }



        // !!!!!!!!!!!!!!!!!!!
        // GetGoodsAsync()
        // !!!!!!!!!!!!!!!!!!!
        // TODO: Czy tutaj coœ wiêcej robiæ?





        // !!!!!!!!!!!!!!!!!!!
        // AssignGoodsToCountryAsync()
        // !!!!!!!!!!!!!!!!!!!

        [Fact]
        public async Task AssignGoodsToCountryAsync_WhenGoodIsNull()
        {
            // Arrange
            int countryId = 1;
            int goodId = 1;

            var country = new Country { Id = countryId, Name = "NazwaKraju", Capital = "Nazwa Stolicy" };
            var goods = new Goods { Id = goodId, Name = "TestGoods", Price = 10.0 };

            var dataContextMock = new Mock<IDataContext>();

            // Symuluj sytuacjê, gdy kraj istnieje, ale goods nie istnieje (np. null)
            dataContextMock.Setup(context => context.Countries.FindAsync(countryId))
                .ReturnsAsync(country); // Kraj istnieje

            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync((Goods)null); // Towar nie istnieje (null)

            var validatorMock = new Mock<IValidator<Goods>>();

            //pozwala przywo³aœ service poni¿ej.
            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AssignGoodsToCountryAsync(countryId, goodId);

            // Assert
            result.Should().BeFalse(); // Poniewa¿ towary s¹ null, wynik powinien byæ False
        }

        [Fact]
        public async Task AssignGoodsToCountryAsync_WhenCountryIsNull()
        {
            // Arrange
            int countryId = 1;
            int goodId = 1;

            var country = new Country { Id = countryId, Name = "NazwaKraju", Capital = "Nazwa Stolicy" };
            var goods = new Goods { Id = goodId, Name = "TestGoods", Price = 10.0 };

            var dataContextMock = new Mock<IDataContext>();

            // Symuluj sytuacjê, gdy kraj istnieje, ale goods nie istnieje (np. null)
            dataContextMock.Setup(context => context.Countries.FindAsync(countryId))
                .ReturnsAsync(country); // Kraj istnieje

            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync((Goods)null); // Towar nie istnieje (null)

            var validatorMock = new Mock<IValidator<Goods>>();

            //pozwala przywo³aœ service poni¿ej.
            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AssignGoodsToCountryAsync(countryId, goodId);

            // Assert
            result.Should().BeFalse(); // Poniewa¿ towary s¹ null, wynik powinien byæ False
        }

        public async Task AssignGoodsToCountryAsync_WhenCountryAndGoodAreNull()
        {
            // Arrange
            int countryId = 1;
            int goodId = 1;

            var country = new Country { Id = countryId, Name = "NazwaKraju", Capital = "Nazwa Stolicy" };
            var goods = new Goods { Id = goodId, Name = "TestGoods", Price = 10.0 };

            var dataContextMock = new Mock<IDataContext>();

            // Symuluj sytuacjê, gdy kraj nie istnieje, ale goods  istnieje (np. null)
            dataContextMock.Setup(context => context.Countries.FindAsync(countryId))
                .ReturnsAsync((Country)null); // Kraj istnieje

            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync((Goods)null); // Towar nie istnieje (null)

            var validatorMock = new Mock<IValidator<Goods>>();

            //pozwala przywo³aœ service poni¿ej.
            var service = new GoodsService(dataContextMock.Object, validatorMock.Object);

            // Act
            var result = await service.AssignGoodsToCountryAsync(countryId, goodId);

            // Assert
            result.Should().BeFalse(); // Poniewa¿ towary s¹ null, wynik powinien byæ False
        }


        [Fact]
        public async Task AssignGoodsToCountryAsync_WhenCountryAndGoodAreValid()
        {
            // Arrange
            int countryId = 1;
            int goodId = 1;

            var country = new Country { Id = countryId, Name = "NazwaKraju", Capital = "Nazwa Stolicy" };
            var goods = new Goods { Id = goodId, Name = "TestGoods", Price = 10.0 };

            var dataContextMock = new Mock<IDataContext>();

            // zak³adamy, ¿e kraj istnieje
            dataContextMock.Setup(context => context.Countries.FindAsync(countryId))
                .ReturnsAsync(country);
            // zak³adamy, ¿e towar istnieje
            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync(goods);
            // Mockowanie dodania `CountryGoods`
            dataContextMock.Setup(context => context.CountryGoods.Add(It.IsAny<CountryGoods>()));

            // Tworzymy serwis
            var service = new GoodsService(dataContextMock.Object, Mock.Of<IValidator<Goods>>());


            // Act
            var result = await service.AssignGoodsToCountryAsync(countryId, goodId);



            // Assert
            result.Should().BeTrue(); // Oczekujemy, ¿e wynik bêdzie True
            // Sprawdzamy, czy metoda Add zosta³a wywo³ana dok³adnie raz
            dataContextMock.Verify(context => context.CountryGoods.Add(It.IsAny<CountryGoods>()), Times.Once());
            // Sprawdzamy, czy metoda SaveChanges zosta³a wywo³ana dok³adnie raz
            dataContextMock.Verify(context => context.SaveChanges(), Times.Once());
        }




        // !!!!!!!!!!!!!!!!!!!
        // DeleteGoodsAsync()
        // !!!!!!!!!!!!!!!!!!!

        [Fact]
        public async Task DeleteGoodsAsync_WhenGoodsExists()
        {
            // Arrange
            int goodId = 1;
            var goods = new Goods { Id = goodId, Name = "TestGoods", Price = 100.0 };

            var dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync(goods);

            var service = new GoodsService(dataContextMock.Object, Mock.Of<IValidator<Goods>>());

            // Act
            var result = await service.DeleteGoodsAsync(goodId);

            // Assert
            result.Id.Should().Be(goodId); // Sprawdzamy, czy zwrócony towar ma oczekiwane ID
            dataContextMock.Verify(context => context.Goods.Remove(goods), Times.Once()); // Powinien usun¹æ towar
            dataContextMock.Verify(context => context.SaveChanges(), Times.Once()); // Powinien zapisaæ zmiany
        }

        [Fact]
        public async Task DeleteGoodsAsync_WhenGoodNotExist()
        {
            // Arrange
            int goodId = 1;

            var dataContextMock = new Mock<IDataContext>();
            dataContextMock.Setup(context => context.Goods.FindAsync(goodId))
                .ReturnsAsync((Goods)null);

            var service = new GoodsService(dataContextMock.Object, Mock.Of<IValidator<Goods>>());

            // Act
            var result = await service.DeleteGoodsAsync(goodId);

            // Assert
            result.Should().BeNull(); // Powinno zwróciæ null
            dataContextMock.Verify(context => context.Goods.Remove(It.IsAny<Goods>()), Times.Never()); // Nie powinno nic usun¹æ
            dataContextMock.Verify(context => context.SaveChanges(), Times.Never()); // Nie powinno zapisywaæ zmian
        }



        // !!!!!!!!!!!!!!!!!!!
        // RemoveGoodsFromCountryAsync()
        // !!!!!!!!!!!!!!!!!!!



        [Fact]
        public async Task RemoveGoodsFromCountryAsync_ShouldReturnFalse_WhenCountryGoodsNotFound()
        {
            // Arrange
            int countryId = 1;
            int goodsId = 1;



            var mockContext = new Mock<IDataContext>();
            //var mockCountryGoods = new Mock<DbSet<CountryGoods>>();

            // Mocking FirstOrDefaultAsync to return null (CountryGoods not found)
            mockContext.Setup(context => context.CountryGoods.FirstOrDefaultAsync(cg => cg.CountryId == countryId && cg.GoodsId == goodsId, default))
                .ReturnsAsync((CountryGoods?)null);

            // Mocking DbSet
            //mockContext.Setup(c => c.CountryGoods).Returns(mockCountryGoods.Object);

            var service = new GoodsService(mockContext.Object, Mock.Of<IValidator<Goods>>());

            // Act
            var result = await service.RemoveGoodsFromCountryAsync(countryId, goodsId);

            // Assert
            Assert.False(result);
            mockContext.Verify(c => c.CountryGoods.Remove(It.IsAny<CountryGoods>()), Times.Never);
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }
        [Fact]
        public async Task RemoveGoodsFromCountryAsync_ShouldReturnFalse_WhenCountryGoodsNotFound2()
        {
            // Arrange
            int countryId = 1;
            int goodsId = 1;

            // Tworzymy pust¹ listê dla DbSet<CountryGoods>
            var mockCountryGoodsData = new List<CountryGoods>().AsQueryable();

            // Tworzymy mocka DbSet<CountryGoods>
            var mockCountryGoodsSet = new Mock<DbSet<CountryGoods>>();

            // Konfiguracja mocka DbSet, aby zachowywa³ siê jak IQueryable
            mockCountryGoodsSet.As<IQueryable<CountryGoods>>().Setup(m => m.Provider).Returns(mockCountryGoodsData.Provider);
            mockCountryGoodsSet.As<IQueryable<CountryGoods>>().Setup(m => m.Expression).Returns(mockCountryGoodsData.Expression);
            mockCountryGoodsSet.As<IQueryable<CountryGoods>>().Setup(m => m.ElementType).Returns(mockCountryGoodsData.ElementType);
            mockCountryGoodsSet.As<IQueryable<CountryGoods>>().Setup(m => m.GetEnumerator()).Returns(mockCountryGoodsData.GetEnumerator());

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.CountryGoods).Returns(mockCountryGoodsSet.Object);

            var service = new GoodsService(mockContext.Object, Mock.Of<IValidator<Goods>>());

            // Act
            var result = await service.RemoveGoodsFromCountryAsync(countryId, goodsId);

            // Assert
            Assert.False(result);
            mockContext.Verify(c => c.CountryGoods.Remove(It.IsAny<CountryGoods>()), Times.Never);
            mockContext.Verify(c => c.SaveChanges(), Times.Never);
        }


    }
}


