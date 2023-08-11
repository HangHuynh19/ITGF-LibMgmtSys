using Moq;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Users.Commands.UpdateUserCommand;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;

namespace Tests.Application.UnitTests.UserAggregate
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UpdateUserCommandHandler _handler;
        
        public UpdateUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new UpdateUserCommandHandler(
                _userRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_UserFound_ReturnsUser()
        {
            var userId = Guid.NewGuid();
            var existingUser = User.CreateCustomer
            (
                "John",
                "Doe",
                "john@mail.com",
                "12345678");
            
            var command = new UpdateUserCommand(
                userId,
                "John2",
                "Doe",
                "john2@mail.com",
                "12345678");
            
            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(UserId.Create(userId))).ReturnsAsync(existingUser);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            _userRepositoryMock.Verify(r => r.UpdateUserAsync(existingUser), Times.Once);
            Assert.Equal("John2", result.Value.FirstName);
            Assert.Equal("john2@mail.com", result.Value.Email);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsUserNotFound()
        {
            var userId = Guid.NewGuid();
            var command = new UpdateUserCommand(
                userId,
                "John2",
                "Doe",
                "john2@mail.com",
                "12345678");

            _userRepositoryMock.Setup(r => r.GetUserByIdAsync(UserId.Create(userId))).ReturnsAsync((User)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            _userRepositoryMock.Verify(r => r.UpdateUserAsync(It.IsAny<User>()), Times.Never);
            Assert.Equal(Errors.User.UserNotFound, result.Errors[0]);
        }
    }
}
