using Moq;
using LibMgmtSys.Backend.Application.Books.Commands.UpdateBookCommand;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;

namespace Tests.Application.UnitTests.BookAggregate
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateBookCommandHandler _handler;
        
        public UpdateBookCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateBookCommandHandler(_unitOfWorkMock.Object);
        }
        
        [Fact]
        public async Task Handle_BookFound_ReturnsBook()
        {
            var command = new UpdateBookCommand
            (
                "693312cd-8ab8-42c2-84c3-5996e087c334",
                "Updated Title",
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
                10,
                10,
                new Uri("https://www.google.com")
            );
            
            _unitOfWorkMock.Setup(r => r.Book.GetBookByIdAsync(BookId.Create(Guid.Parse(command.Id))))
                .ReturnsAsync(existingBook);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _unitOfWorkMock.Verify(r => r.Book.UpdateBookAsync(existingBook), Times.Once);
            Assert.Equal("Updated Title", result.Value.Title);
        }
        
        [Fact]
        public async Task Handle_BookNotFound_ReturnsBookNotFound()
        {
            var command = new UpdateBookCommand
            (
                "badfcbfe-e7e4-4a05-a256-140794ffb3ef",
                "Title",
                "Isbn",
                "Publisher",
                2021,
                "Description",
                new Uri("https://www.google.com"),
                10,
                10
            );
            
            _unitOfWorkMock.Setup(r => r.Book.GetBookByIdAsync(BookId.Create(Guid.Parse(command.Id))))
                .ReturnsAsync((Book)null);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _unitOfWorkMock.Verify(r => r.Book.UpdateBookAsync(It.IsAny<Book>()), Times.Never);
            Assert.Equal(Errors.Book.BookNotFound, result.Errors[0]);
        }
    }
}
