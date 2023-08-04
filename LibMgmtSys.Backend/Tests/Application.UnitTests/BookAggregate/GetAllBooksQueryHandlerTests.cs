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
            var authorRepositoryMock = new Mock<IAuthorRepository>();
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
            
            bookRepositoryMock.Setup(bookRepository => bookRepository.GetAllBooksAsync())
                .ReturnsAsync(books);
            
            var getAllBooksQueryHandler = new GetAllBooksQueryHandler(
                bookRepositoryMock.Object
            );
            var getAllBooksQuery = new GetAllBooksQuery();

            // Act
            var result = await getAllBooksQueryHandler.Handle(getAllBooksQuery, CancellationToken.None);

            // Assert
            Assert.Equal(books.Count, result.Value.Count);
            Assert.Equal(books[0].Title, result.Value[0].Title);
            Assert.Equal(books[1].Title, result.Value[1].Title);
            Assert.Equal(books[2].Title, result.Value[2].Title);
        }
    }
}

