using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Genres.Commands.CreateGenreCommand;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.GenreAggregate
{
    public class CreateGenreCommandTests
    {
        private readonly CreateGenreCommandHandler _handler;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;

        public CreateGenreCommandTests()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _handler = new CreateGenreCommandHandler(_genreRepositoryMock.Object, _bookRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WhenBookIdsAreValid_ShouldReturnGenre()
        {
            // Arrange
            var bookIds = new List<BookId> { BookId.CreateUnique(), BookId.CreateUnique() };
            var request = new CreateGenreCommand("Test Name", bookIds);
            var books = new List<Book>
        {
            Book.Create(
                "Title1", 
                "Description1", 
                "Publisher1", 
                1991, 
                "Description1", 
                14, 
                1,
                new Uri("https://www.google.com/")), 
            Book.Create(
                "Title2", 
                "Description2", 
                "Publisher2", 
                1992, 
                "Description2", 
                14, 
                1,
                new Uri("https://www.google.com/")) 
        };
            _bookRepositoryMock.Setup(r => r.GetBooksByIdsAsync(bookIds)).ReturnsAsync(books);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsError is false);
            Assert.Equal("Test Name", result.Value.Name);
            Assert.Equal(2, result.Value.Books.Count);
            Assert.Collection(result.Value.Books,
                book => Assert.Equal("Title1", book.Title),
                book => Assert.Equal("Title2", book.Title));
        }

        [Fact]
        public async Task Handle_WhenBookIdsAreInvalid_ShouldReturnBookNotFound()
        {
            var command = new CreateGenreCommand(
                "Test Name",
                new List<BookId> { BookId.CreateUnique() });
            var foundBook = Book.Create(
                "Title1",
                "Description1",
                "Publisher1",
                1991,
                "Description1",
                14,
                1,
                new Uri("https://www.google.com/"));
            var books = new List<Book> { foundBook };
            _bookRepositoryMock.Setup(r => r.GetBooksByIdsAsync(command.BookIds)).ReturnsAsync(books);
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Single(result.Errors);
        }
    }
}

