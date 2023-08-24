using Moq;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace Tests.Application.UnitTests.UserAggregate
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly UpdateUserCommandHandler _handler;
        
        public UpdateUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateUserCommandHandler(
                _unitOfWorkMock.Object);
        }
        
        [Fact]
        public async Task Handle_UserFound_ReturnsUser()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var userId = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var existingUser = User.CreateCustomer
            (
                "John",
                "Doe",
                "john@mail.com",
                "12345678");
            var existingCustomer = Customer.Create(
                "John",
                "Doe",
                "john@mail.com",
                UserId.Create(userId),
                new Uri("http://google.com"));
            var command = new UpdateUserCommand(
                userId,
                "John2",
                "Doe",
                "john2@mail.com",
                "12345678");
            
            _unitOfWorkMock
                .Setup(uow => uow.Customer).Returns(customerRepositoryMock.Object);
            _unitOfWorkMock
                .Setup(r => r.User.GetUserByIdAsync(UserId.Create(userId))).ReturnsAsync(existingUser);
            _unitOfWorkMock
                .Setup(r => r.Customer.GetCustomerByUserIdAsync(UserId.Create(userId))).ReturnsAsync(existingCustomer);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _unitOfWorkMock.Verify(r => r.User.UpdateUser(existingUser), Times.Once);
            _unitOfWorkMock.Verify(r => r.Customer.UpdateCustomer(existingCustomer), Times.Once);
            Assert.Equal("John2", result.Value.FirstName);
            Assert.Equal("john2@mail.com", result.Value.Email);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsUserNotFound()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var userId = Guid.NewGuid();
            var command = new UpdateUserCommand(
                userId,
                "John2",
                "Doe",
                "john2@mail.com",
                "12345678");
            
            _unitOfWorkMock
                .Setup(uow => uow.Customer).Returns(customerRepositoryMock.Object);
            _unitOfWorkMock
                .Setup(r => r.User.GetUserByIdAsync(UserId.Create(userId))).ReturnsAsync((User)null);

            var result = await _handler.Handle(command, CancellationToken.None);
            
            _unitOfWorkMock.Verify(r => r.User.UpdateUser(It.IsAny<User>()), Times.Never);
            Assert.Equal(Errors.User.UserNotFound, result.Errors[0]);
        }
    }
}
