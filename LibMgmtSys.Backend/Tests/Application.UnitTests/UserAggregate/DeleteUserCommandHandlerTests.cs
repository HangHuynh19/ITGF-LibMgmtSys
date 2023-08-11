using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Users.Commands.DeleteUserCommand;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.UserAggregate
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly DeleteUserCommandHandler _handler;
        
        public DeleteUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new DeleteUserCommandHandler(_userRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_WhenUserNotFound_ReturnsUserNotFound()
        {
            // Arrange
            var userId = UserId.CreateUnique();
            var command = new DeleteUserCommand(userId);
            
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync((User)null);
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.Equal(Errors.User.UserNotFound, result.Errors[0]);
        }

        [Fact]
        public async Task Handle_UserFound_ReturnsUser()
        {
            var userId = UserId.CreateUnique();
            var command = new DeleteUserCommand(userId);
            var user = User.CreateCustomer("John", "Doe", "john@mail.com", "12345678");
            
            _userRepositoryMock
                .Setup(x => x.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(user, result.Value);
            _userRepositoryMock.Verify(x => x.DeleteUserAsync(user), Times.Once);
        }
    }
}

