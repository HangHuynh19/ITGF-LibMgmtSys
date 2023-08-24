using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand;
using LibMgmtSys.Backend.Domain.BookAggregate;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.LoanAggregate
{
    public class CreateLoanCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CreateLoanCommandHandler _handler;

        public CreateLoanCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CreateLoanCommandHandler(_unitOfWorkMock.Object);
        }
        
        [Fact]
        public async Task Handle_ValidRequest_ReturnsBook()
        {
            var bookId1 = Guid.NewGuid().ToString();
            var bookId2 = Guid.NewGuid().ToString();
            var customer = Customer.Create(
                "John",
                "Doe",
                "john@mail.com",
                UserId.CreateUnique());
            var books = new List<Book>
            {
                Book.Create(
                    "Book 1", 
                    "1234567890", 
                    "Publisher 1", 
                    2023, 
                    "Description 1", 
                    7, 
                    10,
                    new Uri("http://google.com")), 
                Book.Create(
                    "Book 2", 
                    "0987654321", 
                    "Publisher 2", 
                    2023, 
                    "Description 2", 
                    7, 
                    10,
                    new Uri("http://google.com")) 
            };
            
            _unitOfWorkMock.Setup(r => r.Book.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(books);
            _unitOfWorkMock.Setup(r => r.Customer.GetCustomerByUserIdAsync(customer.UserId))
                .ReturnsAsync(customer);
            _unitOfWorkMock.Setup(r => r.Loan.AddLoanAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan loan) => loan);
            
            var command = new CreateLoanCommand(
                new List<string> { bookId1, bookId2 }, 
                DateTime.UtcNow, 
                customer.UserId.Value);
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.False(result.IsError);
            Assert.Equal(2, result.Value.Count);
        }

        [Fact]
        public async Task Handle_BookNotFound_ReturnsError()
        {
            var bookId1 = Guid.NewGuid().ToString();
            var bookId2 = Guid.NewGuid().ToString();
            var customer = Customer.Create(
                "Jane",
                "Smith",
                "jane@mail.com",
                UserId.CreateUnique());
            var foundBooks = new List<Book>
            {
                Book.Create(
                    "Book 1", 
                    "1234567890", 
                    "Publisher 1", 
                    2023, 
                    "Description 1", 
                    7, 
                    10,
                    new Uri("http://google.com"))
            };

            _unitOfWorkMock.Setup(r => r.Book.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(foundBooks);
            _unitOfWorkMock.Setup(r => r.Customer.GetCustomerByIdAsync(It.IsAny<CustomerId>()))
                .ReturnsAsync(customer);
            _unitOfWorkMock.Setup(r => r.Loan.AddLoanAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan loan) => loan);

            var command =
                new CreateLoanCommand(new List<string> { bookId1, bookId2 }, DateTime.UtcNow, customer.Id.Value);
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(Errors.Book.BookNotFound, result.Errors[0]);
        }

        [Fact]
        public async Task Handle_CustomerNotFound_ReturnsError()
        {
            var bookId1 = Guid.NewGuid().ToString();
            var bookId2 = Guid.NewGuid().ToString();
            var customer = Customer.Create(
                "Hanna",
                "Doe",
                "hanna@mail.com",
                UserId.CreateUnique());
            var books = new List<Book>
            {
                Book.Create(
                    "Book 1", 
                    "1234567890", 
                    "Publisher 1", 
                    2023, 
                    "Description 1", 
                    7, 
                    10,
                    new Uri("http://google.com")),
                Book.Create(
                    "Book 2", 
                    "0987654321", 
                    "Publisher 2", 
                    2023, 
                    "Description 2", 
                    7, 
                    10,
                    new Uri("http://google.com"))
            };

            _unitOfWorkMock.Setup(r => r.Book.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(books);
            _unitOfWorkMock.Setup(r => r.Customer.GetCustomerByIdAsync(It.IsAny<CustomerId>()))
                .ReturnsAsync((Customer)null);

            var command =
                new CreateLoanCommand(new List<string> { bookId1, bookId2 }, DateTime.UtcNow, customer.Id.Value);
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(Errors.Customer.CustomerNotFound, result.Errors[0]);
        }
    }
}

