using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Genres.Queries;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.GenreAggregate
{
    public class GetAllGenresQueryHandlerTests
    {
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly GetAllGenresQueryHandler _getAllGenresQueryHandler;
        
        public GetAllGenresQueryHandlerTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _getAllGenresQueryHandler = new GetAllGenresQueryHandler(_genreRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_ShouldReturnListOfGenres_WhenRequestIsValid()
        {
            // Arrange
            var request = new GetAllGenresQuery();
            var expectedGenres = new List<Genre>
            {
                Genre.Create("Genre 1"),
                Genre.Create("Genre 2"),
                Genre.Create("Genre 3"),
            };
            _genreRepositoryMock
                .Setup(genreRepository => genreRepository.GetAllGenresAsync())
                .ReturnsAsync(expectedGenres);
            
            // Act
            var result = await _getAllGenresQueryHandler.Handle(request, CancellationToken.None);
            
            // Assert
            Assert.Equal(3, result.Value.Count);
            Assert.Equal(expectedGenres, result.Value);
        }
    }
}

