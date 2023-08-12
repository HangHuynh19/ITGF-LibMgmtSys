using LibMgmtSys.Backend.Application.Books.Queries.GetAllBooksQuery;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using Moq;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class GetAllBooksQueryHandlerTests
    {
        [Fact]
        public async Task GetAllBooksQueryHandler_ShouldReturnAllBooks()
        {
            // Arrange
            var bookRepositoryMock = new Mock<IBookRepository>();
            var book1 = Book.Create(
                "Title1",
                "Isbn1",
                "Publisher1",
                2021,
                "Description1",
                new Uri("https://www.google.com/"),
                1,
                1
            );
            var book2 = Book.Create(
                "Title2",
                "Isbn2",
                "Publisher2",
                2022,
                "Description2",
                new Uri("https://www.google.com/"),
                2,
                2
            );
            var book3 = Book.Create(
                "Title3",
                "Isbn3",
                "Publisher3",
                2023,
                "Description3",
                new Uri("https://www.google.com/"),
                3,
                3
            );
            var books = new List<Book> { book1, book2, book3 };
            
            bookRepositoryMock.Setup(bookRepository => bookRepository
                .GetAllBooksWithPaginationAsync(
                    It.IsAny<int>(), 
                    It.IsAny<int>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>())
            ).ReturnsAsync(books);
            
            var getAllBooksQuery = new GetAllBooksWithPaginationQuery(PageNumber: 1, PageSize: 3, SortOrder: "desc", SearchTerm: "abc");
            var getAllBooksQueryHandler = new GetAllBooksWithPaginationQueryHandler(
                bookRepositoryMock.Object
            );

            // Act
            var result = await getAllBooksQueryHandler.Handle(getAllBooksQuery, CancellationToken.None);

            // Assert
            Assert.Equal(3, result.Value.Count);
            Assert.Equal(book1.Title, result.Value[0].Title);
            Assert.Equal(book2.Title, result.Value[1].Title);
            Assert.Equal(book3.Title, result.Value[2].Title);
        }
    }
}

