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
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly CreateLoanCommandHandler _handler;

        public CreateLoanCommandHandlerTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _handler = new CreateLoanCommandHandler(
                _bookRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _loanRepositoryMock.Object
            );
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
                Book.Create("Book 1", "1234567890", "Publisher 1", 2023, "Description 1", new Uri("http://google.com"), 7, 10),
                Book.Create("Book 2", "0987654321", "Publisher 2", 2023, "Description 2", new Uri("http://google.com"), 7, 10)
            };
            
            _bookRepositoryMock
                .Setup(x => x.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(books);
            _customerRepositoryMock.Setup(r => r.GetCustomerByIdAsync(It.IsAny<CustomerId>()))
                .ReturnsAsync(customer);
            _loanRepositoryMock.Setup(r => r.AddLoanAsync(It.IsAny<Loan>()))
                .ReturnsAsync((Loan loan) => loan);
            
            var command = new CreateLoanCommand(new List<string> { bookId1, bookId2 }, DateTime.UtcNow, customer.Id.Value);
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
                Book.Create("Book 1", "1234567890", "Publisher 1", 2023, "Description 1", new Uri("http://google.com"),
                    7, 10),
            };

            _bookRepositoryMock
                .Setup(x => x.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(foundBooks);
            _customerRepositoryMock.Setup(r => r.GetCustomerByIdAsync(It.IsAny<CustomerId>()))
                .ReturnsAsync(customer);
            _loanRepositoryMock.Setup(r => r.AddLoanAsync(It.IsAny<Loan>()))
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
                Book.Create("Book 1", "1234567890", "Publisher 1", 2023, "Description 1", new Uri("http://google.com"),
                    7, 10),
                Book.Create("Book 2", "0987654321", "Publisher 2", 2023, "Description 2", new Uri("http://google.com"),
                    7, 10)
            };

            _bookRepositoryMock
                .Setup(x => x.GetBooksByIdsAsync(It.IsAny<List<BookId>>()))
                .ReturnsAsync(books);
            _customerRepositoryMock.Setup(r => r.GetCustomerByIdAsync(It.IsAny<CustomerId>()))
                .ReturnsAsync((Customer)null);

            var command =
                new CreateLoanCommand(new List<string> { bookId1, bookId2 }, DateTime.UtcNow, customer.Id.Value);
            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(Errors.Customer.CustomerNotFound, result.Errors[0]);
        }
    }
}

