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
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var book1 = Book.Create(
                "Title1",
                "Isbn1",
                "Publisher1",
                2021,
                "Description1",
                1,
                1,
                new Uri("https://www.google.com/")
            );
            var book2 = Book.Create(
                "Title2",
                "Isbn2",
                "Publisher2",
                2022,
                "Description2",
                2,
                2,
                new Uri("https://www.google.com/")
            );
            var book3 = Book.Create(
                "Title3",
                "Isbn3",
                "Publisher3",
                2023,
                "Description3",
                3,
                3,
                new Uri("https://www.google.com/")
            );
            var books = new List<Book> { book1, book2, book3 };
            
            unitOfWorkMock.Setup(unitOfWork => unitOfWork.Book
                .GetAllBooksWithPaginationAsync(
                    It.IsAny<int>(), 
                    It.IsAny<int>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>())
            ).ReturnsAsync(books);
            
            var getAllBooksQuery = new GetAllBooksWithPaginationQuery(PageNumber: 1, PageSize: 3, SortOrder: "desc", SearchTerm: "abc");
            var getAllBooksQueryHandler = new GetAllBooksWithPaginationQueryHandler(unitOfWorkMock.Object);

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

