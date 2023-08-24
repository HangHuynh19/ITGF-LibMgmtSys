using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Genres.Commands.CreateGenreCommand;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using Moq;

namespace Tests.Application.UnitTests.GenreAggregate
{
    public class CreateGenreCommandTests
    {
        private readonly CreateGenreCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateGenreCommandTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateGenreCommandHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_WhenBookIdsAreValid_ShouldReturnGenre()
        {
            // Arrange
            var mockGenreRepository = new Mock<IGenreRepository>();
            _unitOfWorkMock.Setup(uow => uow.Genre).Returns(mockGenreRepository.Object);
            var request = new CreateGenreCommand("Test Name");
            
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError is false);
            Assert.Equal("Test Name", result.Value.Name);
            mockGenreRepository.Verify(r => r.AddGenreAsync(It.IsAny<Genre>()), Times.Once);
        }
    }
}

