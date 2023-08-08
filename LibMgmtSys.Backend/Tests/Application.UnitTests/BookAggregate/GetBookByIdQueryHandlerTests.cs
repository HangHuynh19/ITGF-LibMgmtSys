using LibMgmtSys.Backend.Application.Books.Queries.GetBookByIdQuery;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using Moq;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class GetBookByIdQueryHandlerTests
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly GetBookByIdQueryHandler _handler;
        
        public GetBookByIdQueryHandlerTests()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            _handler = new GetBookByIdQueryHandler(_mockBookRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ShouldReturnBook()
        {
            // Arrange
            var expectedBookId = BookId.CreateUnique();
            var expectedBook = Book.Create(
                "Title",
                "Isbn",
                "Publisher",
                2021,
                "Description",
                new Uri("https://www.google.com/"),
                1,
                1
            );
            var getBookByIdQuery = new GetBookByIdQuery(expectedBookId);
            
            _mockBookRepository.Setup(bookRepository => bookRepository.GetBookByIdAsync(expectedBookId))
                .ReturnsAsync(expectedBook);
            
            // Act
            var result = await _handler.Handle(getBookByIdQuery, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsError);
            Assert.Equal(expectedBook, result.Value);
        }
        
        [Fact]
        public async Task Handle_InvalidId_ShouldReturnBookNotFound()
        {
            // Arrange
            var invalidBookId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var getBookByIdQuery = new GetBookByIdQuery(BookId.Create(invalidBookId));
            
            _mockBookRepository.Setup(bookRepository => bookRepository.GetBookByIdAsync(getBookByIdQuery.Id))
                .ReturnsAsync((Book)null);
            
            // Act
            var result = await _handler.Handle(getBookByIdQuery, CancellationToken.None);
            
            // Assert
            Assert.True(result.IsError);
            Assert.Equal(Errors.Book.BookNotFound, result.Errors[0]);
        }
    }
}

