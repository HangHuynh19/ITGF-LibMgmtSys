using LibMgmtSys.Backend.Application.Books.Commands.DeleteBookCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using Moq;

namespace Tests.Application.UnitTests.BookAggregate;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteBookCommandHandler _deleteBookCommandHandler;
    
    public DeleteBookCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _deleteBookCommandHandler = new DeleteBookCommandHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    private async Task Handle_ValidBookId_ReturnsBook()
    {
        var book = Book.Create(
            "title",
            "isbn",
            "publisher",
            2021,
            "description",
            30,
            10,
            new Uri("https://www.google.com"));
        
        _unitOfWorkMock.Setup(x => x.Book.GetBookByIdAsync(book.Id)).ReturnsAsync(book);
        var result = await _deleteBookCommandHandler.Handle(new DeleteBookCommand(book.Id), CancellationToken.None);
        
        Assert.False(result.IsError);
        Assert.Equal(book, result.Value);
        _unitOfWorkMock.Verify(r => r.Book.DeleteBook(book), Times.Once);
    }

    [Fact]
    private async Task Handle_InvalidBookId_ReturnsBookNotFound()
    {
        var bookId = BookId.CreateUnique();
        
        _unitOfWorkMock.Setup(x => x.Book.GetBookByIdAsync(bookId)).ReturnsAsync((Book)null);
        
        var result = await _deleteBookCommandHandler.Handle(new DeleteBookCommand(bookId), CancellationToken.None);
        
        Assert.True(result.IsError);
        Assert.Equal(Errors.Book.BookNotFound, result.Errors[0]);
    }
}