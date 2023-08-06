using LibMgmtSys.Backend.Application.Authentication.Queries.Login;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.UserAggregate;
using Moq;

namespace Tests.Application.UnitTests.AuthenticationTests
{
    public class LoginQueryHandlerTests
    {
        private readonly LoginQueryHandler _handler;
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        
        public LoginQueryHandlerTests()
        {
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new LoginQueryHandler(_jwtTokenGeneratorMock.Object, _userRepositoryMock.Object);
        }
        
        [Fact]
        public async Task Handle_ValidCredentials_ReturnsAuthenticationResult()
        {
            // Arrange
            var email = "test@mail.com";
            var password = "password123";
            var user = User.CreateCustomer("John", "Doe", email, password);
            var query = new LoginQuery(email, password);
            
            _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(email))
                .ReturnsAsync(user);
            _jwtTokenGeneratorMock.Setup(g => g.GenerateToken(user))
                .Returns("token");
            
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
            Assert.Equal(user, result.Value.User);
            Assert.Equal("token", result.Value.Token);
        }

        [Fact]
        public async Task Handle_InvalidCredentials_ReturnsInvalidCredentialsError()
        {
            // Arrange
            var email = "test@mail.com";
            var password = "incorrectPassword";
            var user = User.CreateCustomer("John", "Doe", email, "correctPassword");
            var query = new LoginQuery(email, password);
            
            _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(email))
                .ReturnsAsync(user);
            
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.True(result.IsError);
            Assert.Equal(Errors.Authentication.InvalidCredentials, result.Errors[0]);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsUserNotFoundError()
        {
            // Arrange
            var email = "non-existent-user@mail.com";
            var password = "password123";
            var query = new LoginQuery(email, password);
            
            _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(email))
                .ReturnsAsync((User)null);
            
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.True(result.IsError);
            Assert.Equal(Errors.User.UserNotFound, result.Errors[0]);
        }
    }
}

