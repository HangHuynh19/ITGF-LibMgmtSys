using LibMgmtSys.Backend.Application.Books.Commands.CreateBookCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.AuthorAggregate;
using Moq;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.GenreAggregate;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class CreateBookCommandHandlerTests
    {
        private readonly CreateBookCommandHandler _handler;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;

        public CreateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _handler = new CreateBookCommandHandler(
                _bookRepositoryMock.Object, 
                _authorRepositoryMock.Object, 
                _genreRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenAuthorIdsAreValid_ShouldReturnBook()
        {
            // Arrange
            var authorIds = new List<AuthorId> { AuthorId.CreateUnique(), AuthorId.CreateUnique() };
            var genreIds = new List<GenreId> { GenreId.CreateUnique(), GenreId.CreateUnique() };
            var request = new CreateBookCommand("Title", "Description", "Publisher", authorIds, 1998, genreIds,"Description", new Uri("https://www.google.com/"), 14, 1);
            var authors = new List<Author>
            {
                Author.Create("Test Name1", "Test Biography1"),
                Author.Create("Test Name2", "Test Biography2")
            };
            
            _authorRepositoryMock.Setup(r => r.GetAuthorsByIdsAsync(authorIds)).ReturnsAsync(authors);
            _genreRepositoryMock.Setup(r => r.GetGenresByIdsAsync(genreIds)).ReturnsAsync(new List<Genre> { Genre.Create("Test Genre1"), Genre.Create("Test Genre2") });
            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError is false);
            Assert.Equal("Title", result.Value.Title);
            Assert.Equal("Description", result.Value.Description);
            Assert.Equal("Publisher", result.Value.Publisher);
            Assert.Equal(1998, result.Value.Year);
            Assert.Equal("Description", result.Value.Description);
            Assert.Equal(new Uri("https://www.google.com/"), result.Value.Image);
            Assert.Equal(2, result.Value.Authors.Count);
            Assert.Collection(result.Value.Authors,
                author => Assert.Equal("Test Name1", author.Name),
                author => Assert.Equal("Test Name2", author.Name));
        }

        [Fact]
        public async Task Handle_WhenAuthorIdsAreInvalid_ShouldReturnAuthorNotFound()
        {
            var command = new CreateBookCommand(
                "Title",
                "Description",
                "Publisher",
                new List<AuthorId> { AuthorId.CreateUnique() },
                1998,
                new List<GenreId> { GenreId.CreateUnique() },
                "Description",
                new Uri("https://www.google.com/"),
                14,
                1);
            var foundAuthor = Author.Create(
                "Test Name",
                "Test Biography");
            
            _authorRepositoryMock.Setup(r => r.GetAuthorsByIdsAsync(command.AuthorIds))
                .ReturnsAsync(new List<Author> { foundAuthor });
            _genreRepositoryMock.Setup(r => r.GetGenresByIdsAsync(command.GenreIds))
                .ReturnsAsync(new List<Genre> { Genre.Create("Test Genre") });
            
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Single(result.Errors);
        }
    }
}

