using Application;
using Application.Exceptions;
using AutoFixture;
using Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CinemaTests
{
    public class SessionServiceTests
    {
        [Fact]
        public async Task DeleteSessionAsync_ById_ExceptionResult()
        {
            // Arrange
            var fixture = new Fixture();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindByIdAsync(It.IsAny<int>()));

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);
            int sessionIdToDelete = fixture.Create<int>();

            // Act
            Func<Task> deleteSessionFunc = () => sessionService.DeleteSessionAsync(sessionIdToDelete);

            // Assert
            await Assert.ThrowsAsync<BusinessRuleValidationException>(deleteSessionFunc);
        }

        [Fact]
        public async Task DeleteSessionAsync_ById_ExpectedBehaviour()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionToDelete = fixture.Create<Session>();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindByIdAsync(sessionToDelete.Id))
                .ReturnsAsync(sessionToDelete);

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);

            // Act
            await sessionService.DeleteSessionAsync(sessionToDelete.Id);

            // Assert 
            sessionRepoMock.Verify(r => r.Delete(sessionToDelete), Times.Once);
        }

        [Fact]
        public async Task AddSessionAsync_AlreadyExistResult()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionToAdd = fixture.Create<Session>();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindSessionByParametersAsync(sessionToAdd.SessionTime, sessionToAdd.CinemaId, sessionToAdd.FilmId))
                .ReturnsAsync(fixture.Create<List<Session>>());

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);

            // Act
            Func<Task> addSessionFunc = () => sessionService.AddSessionAsync(sessionToAdd);

            // Assert
            await Assert.ThrowsAsync<BusinessRuleValidationException>(addSessionFunc);
        }

        [Fact]
        public async Task AddSessionAsync_ById_SuccessResult()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionToAdd = fixture.Create<Session>();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindSessionByParametersAsync(sessionToAdd.SessionTime, sessionToAdd.CinemaId, sessionToAdd.FilmId));

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);
            int sessionIdToDelete = fixture.Create<int>();

            // Act
            await sessionService.AddSessionAsync(sessionToAdd);

            // Assert 
            sessionRepoMock.Verify(r => r.AddAsync(sessionToAdd), Times.Once);
        }

        [Fact]
        public async Task UpdateSessionAsync_SessionNotFound()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionToAdd = fixture.Create<Session>();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindByIdAsync(sessionToAdd.Id));
                

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);
            int sessionIdToDelete = fixture.Create<int>();

            // Act
            Func<Task> addSessionFunc = () => sessionService.UpdateSessionAsync(sessionToAdd.Id, sessionToAdd);

            // Assert
            await Assert.ThrowsAsync<BusinessRuleValidationException>(addSessionFunc);
        }

        [Fact]
        public async Task UpdateSessionAsync_Success()
        {
            // Arrange
            var fixture = new Fixture();
            
            var curSession = fixture.Create<Session>();

            var newSession = new Session
            {
                Id = curSession.Id,
                CinemaId = fixture.Create<int>(),
                FilmId = fixture.Create<int>(),
                SessionTime = fixture.Create<DateTime>()
            };

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindByIdAsync(newSession.Id))
                .ReturnsAsync(curSession);

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);
            int sessionIdToDelete = fixture.Create<int>();

            // Act
            await sessionService.UpdateSessionAsync(newSession.Id, newSession);

            // Assert 
            sessionRepoMock.Verify(r 
                => r.Update(It.Is<Session>(x => 
                x.Id == curSession.Id && 
                x.CinemaId == newSession.CinemaId && 
                x.FilmId == newSession.FilmId && 
                x.SessionTime == newSession.SessionTime)), Times.Once);
        }

        [Fact]
        public async Task GetSessionAsync_ById_NullResult()
        {
            // Arrange
            var fixture = new Fixture();

            var sessionRepoMock = new Mock<ISessionRepository>();
            sessionRepoMock
                .Setup((r) => r.FindByIdAsync(It.IsAny<int>()));

            var uowMock = new Mock<IUnitOfWork>();
            uowMock
                .Setup(x => x.Sessions)
                .Returns(sessionRepoMock.Object);

            var sessionService = new SessionService(uowMock.Object);
            var sessionId = fixture.Create<int>();

            // Act
            var session = await sessionService.GetByIdAsync(sessionId);

            // Assert
            Assert.Null(session);
        }
    }
}
