using LibMgmtSys.Backend.Application.Books.Queries.GetBooksByIdsQuery;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class GetBooksByIdsQueryHandlerTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly GetBooksByIdsQueryHandler _handler;
        
        public GetBooksByIdsQueryHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _handler = new GetBooksByIdsQueryHandler(_bookRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_ValidIds_ShouldReturnBooks()
        {
            // Arrange
            var bookIds = new List<BookId>
            {
                BookId.CreateUnique(),
                BookId.CreateUnique(),
                BookId.CreateUnique()
            };
            var expectedBooks = new List<Book>
            {
                Book.Create(
                    "Title",
                    "Isbn",
                    "Publisher",
                    2021,
                    "Description",
                    new Uri("https://www.google.com/"),
                    1,
                    1
                ),
                Book.Create(
                    "Title",
                    "Isbn",
                    "Publisher",
                    2021,
                    "Description",
                    new Uri("https://www.google.com/"),
                    1,
                    1
                ),
                Book.Create(
                    "Title",
                    "Isbn",
                    "Publisher",
                    2021,
                    "Description",
                    new Uri("https://www.google.com/"),
                    1,
                    1
                )
            };
            var getBooksByIdsQuery = new GetBooksByIdsQuery(bookIds);
            
            _bookRepositoryMock.Setup(r => r.GetBooksByIdsAsync(bookIds))
                .ReturnsAsync(expectedBooks);
            
            // Act
            var result = await _handler.Handle(getBooksByIdsQuery, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsError);
            Assert.Equal(expectedBooks, result.Value);
        }
    }
}

