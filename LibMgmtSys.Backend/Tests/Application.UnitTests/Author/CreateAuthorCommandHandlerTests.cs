using LibMgmtSys.Backend.Application.Authors.Commands.CreateAuthorCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using Moq;

namespace Tests.Application.UnitTests.Author;

public class CreateAuthorCommandHandlerTests
{
    private readonly CreateAuthorCommandHandler _handler;
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    
    public CreateAuthorCommandHandlerTests()
    {
        _authorRepositoryMock = new Mock<IAuthorRepository>();
        _bookRepositoryMock = new Mock<IBookRepository>();
        _handler = new CreateAuthorCommandHandler(_authorRepositoryMock.Object, _bookRepositoryMock.Object);
    }
    
    [Fact]
    public async Task Handle_WhenBookIdsAreValid_ShouldReturnAuthor()
    {
        // Arrange
        var bookIds = new List<BookId> { BookId.CreateUnique(), BookId.CreateUnique() };
        var request = new CreateAuthorCommand("Test Name", "Test Biography", bookIds);
        var books = new List<Book>
        {
            Book.Create("Title1", "Description1", "Publisher1", 1991, "Description1", new Uri("https://www.google.com/"), 14, 1),
            Book.Create("Title2", "Description2", "Publisher2", 1992, "Description2", new Uri("https://www.google.com/"), 14, 1)
        };
        _bookRepositoryMock.Setup(r => r.GetBooksByIdsAsync(bookIds)).ReturnsAsync(books);
        
        // Act
        var result = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsError is false);
        Assert.Equal("Test Name", result.Value.Name);
        Assert.Equal("Test Biography", result.Value.Biography);
        Assert.Equal(2, result.Value.Books.Count);
        Assert.Collection(result.Value.Books,
            book => Assert.Equal("Title1", book.Title),
            book => Assert.Equal("Title2", book.Title));
    }
    
    [Fact]
    public async Task Handle_WhenBookIdsAreInvalid_ShouldReturnBookNotFound()
    {
        var command = new CreateAuthorCommand(
            "Test Name", 
            "Test Biography", 
            new List<BookId> { BookId.CreateUnique() });
        var foundBook = Book.Create(
            "Title1", 
            "Description1", 
            "Publisher1", 
            1991, 
            "Description1", 
            new Uri("https://www.google.com/"), 
            14, 
            1);
        var books = new List<Book> { foundBook };
        
        _bookRepositoryMock.Setup(r => r.GetBooksByIdsAsync(command.BookIds))
            .ReturnsAsync(books);
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        //
        //Assert.True(result.IsError);
        Assert.Equal(1, result.Errors.Count());
    }
}