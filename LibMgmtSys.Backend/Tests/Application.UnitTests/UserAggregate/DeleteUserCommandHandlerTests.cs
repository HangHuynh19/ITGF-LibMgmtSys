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
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly DeleteUserCommandHandler _handler;
        
        public DeleteUserCommandHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new DeleteUserCommandHandler(_unitOfWorkMock.Object);
        }
        
        [Fact]
        public async Task Handle_WhenUserNotFound_ReturnsUserNotFound()
        {
            // Arrange
            var userId = UserId.CreateUnique();
            var command = new DeleteUserCommand(userId);
            
            _unitOfWorkMock
                .Setup(x => x.User.GetUserByIdAsync(userId))
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
            
            _unitOfWorkMock
                .Setup(x => x.User.GetUserByIdAsync(userId))
                .ReturnsAsync(user);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.Equal(user, result.Value);
            _unitOfWorkMock.Verify(x => x.User.DeleteUserAsync(user), Times.Once);
        }
    }
}

