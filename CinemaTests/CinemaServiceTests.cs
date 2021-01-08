using Application;
using AutoFixture;
using Core;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CinemaTests
{
    public class CinemaServiceTests
    {
        [Fact]
        public async Task GetAllCinemas_ExpectedBehaviour()
        {
            // Arrange
            var fixture = new Fixture();
            var expectedCinemas = fixture.Create<List<Cinema>>();

            var cinemaRepoMock = new Mock<ICinemaRepository>();
            cinemaRepoMock
                .Setup((r) => r.GetAllAsync())
                .ReturnsAsync(() => expectedCinemas);

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Cinemas)
                .Returns(cinemaRepoMock.Object);

            var cinemaService = new CinemaService(uowMock.Object);

            // Act
            var actualCinemas = await cinemaService.GetAllAsync();

            // Assert
            Assert.Equal(actualCinemas, expectedCinemas);
        }

        [Fact]
        public async Task GetAllCinemasAsync_EmptyResult()
        {
            // Arrange
            var cinemaRepoMock = new Mock<ICinemaRepository>();
            cinemaRepoMock
                .Setup((r) => r.GetAllAsync())
                .ReturnsAsync(() => new List<Cinema>());

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Cinemas)
                .Returns(cinemaRepoMock.Object);

            var cinemaService = new CinemaService(uowMock.Object);

            // Act
            var cinemas = await cinemaService.GetAllAsync();

            // Assert
            Assert.Empty(cinemas);
        }

        [Fact]
        public async Task GetCinemaAsync_ById_NullResult()
        {
            // Arrange
            var fixture = new Fixture();

            var cinemaRepoMock = new Mock<ICinemaRepository>();
            cinemaRepoMock
                .Setup((r) => r.FindByIdAsync(It.IsAny<int>()));

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Cinemas)
                .Returns(cinemaRepoMock.Object);

            var cinemaService = new CinemaService(uowMock.Object);
            var cinemaId = fixture.Create<int>();

            // Act
            var cinema = await cinemaService.GetByIdAsync(cinemaId);

            // Assert
            Assert.Null(cinema);
        }
    }
}
