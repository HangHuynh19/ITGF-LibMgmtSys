using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Users.Queries.CheckUserAdminStatusQuery;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.UserAggregate
{
    public class CheckUserAdminStatusQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly CheckUserAdminStatusQueryHandler _handler;
        
        public CheckUserAdminStatusQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new CheckUserAdminStatusQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_UserIsNotAdmin_ReturnsFalse()
        {
            var userId = Guid.NewGuid();
            var existingUser = User.CreateCustomer
            (
                "John",
                "Doe",
                "john@mail.com",
                "12345678");

            var query = new CheckUserAdminStatusQuery(userId);

            _unitOfWorkMock.Setup(r => r.User.GetUserByIdAsync(UserId.Create(userId)))
                .ReturnsAsync(existingUser);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.False(result.Value);
        }

        [Fact]
        public async Task Handle_UserIsAdmin_ReturnsTrue()
        {
            var userId = Guid.NewGuid();
            var existingUser = User.CreateAdmin
            (
                "Jane",
                "Smith",
                "jane@mail.com",
                "12345678");

            var query = new CheckUserAdminStatusQuery(userId);

            _unitOfWorkMock.Setup(r => r.User.GetUserByIdAsync(UserId.Create(userId)))
                .ReturnsAsync(existingUser);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.True(result.Value);
        }
        
        [Fact]
        public async Task Handle_UserNotFound_ReturnsUserNotFound()
        {
            var userId = Guid.NewGuid();
            var query = new CheckUserAdminStatusQuery(userId);

            _unitOfWorkMock.Setup(r => r.User.GetUserByIdAsync(UserId.Create(userId)))
                .ReturnsAsync((User)null);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.Equal(Errors.User.UserNotFound, result.Errors[0]);
        }
    }
}