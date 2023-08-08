using LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.AuthorAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.GenreAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly Mock<IGenreRepository> _genreRepositoryMock;
        private readonly UpdateBookCommandHandler _handler;
        
        public UpdateBookCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _handler = new UpdateBookCommandHandler(
                _bookRepositoryMock.Object, 
                _authorRepositoryMock.Object, 
                _genreRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_BookFound_ReturnsBook()
        {
            var command = new UpdateBookCommand
            (
                BookId.CreateUnique(),
                "Title",
                "Isbn",
                "Publisher",
                2021,
                "Description",
                new Uri("https://www.google.com"),
                10,
                10
            );
            var existingBook = Book.Create
            (
                "Title",
                "Isbn",
                "Publisher",
                2021,
                "Description",
                new Uri("https://www.google.com"),
                10,
                10
            );
            
            _bookRepositoryMock.Setup(r => r.GetBookByIdAsync(command.Id)).ReturnsAsync(existingBook);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _bookRepositoryMock.Verify(r => r.UpdateBookAsync(existingBook), Times.Once);
            Assert.Equal(existingBook, result.Value);
        }
        
        [Fact]
        public async Task Handle_BookNotFound_ReturnsBookNotFound()
        {
            var command = new UpdateBookCommand
            (
                BookId.CreateUnique(),
                "Title",
                "Isbn",
                "Publisher",
                2021,
                "Description",
                new Uri("https://www.google.com"),
                10,
                10
            );
            
            _bookRepositoryMock.Setup(r => r.GetBookByIdAsync(command.Id)).ReturnsAsync((Book)null);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _bookRepositoryMock.Verify(r => r.UpdateBookAsync(It.IsAny<Book>()), Times.Never);
            Assert.Equal(Errors.Book.BookNotFound, result.Errors[0]);
        }
    }
}
