using LibMgmtSys.Backend.Application.Authentication.Commands.Register;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.UserAggregate;
using Moq;

namespace Tests.Application.UnitTests.AuthenticationTests;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _handler;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    
    public RegisterCommandHandlerTests()
    {
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new RegisterCommandHandler(_jwtTokenGeneratorMock.Object, _userRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotRegistered_ShouldReturnAuthenticationResult()
    {
        // Arrange
        var command = new RegisterCommand("John", "Doe", "john@mail.com", "password123");
        
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email))
            .ReturnsAsync((User)null);
        
        var handler = new RegisterCommandHandler(_jwtTokenGeneratorMock.Object, _userRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsError is false);
        Assert.NotNull(result.Value);
        Assert.Equal(command.FirstName, result.Value.User.FirstName);
        Assert.Equal(command.LastName, result.Value.User.LastName);
        Assert.Equal(command.Email, result.Value.User.Email);
        _jwtTokenGeneratorMock.Verify(g => g.GenerateToken(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_ShouldReturnDuplicateEmailError()
    {
        // Arrange
        var command = new RegisterCommand("John", "Doe", "john@mail.com", "password123");
        var existingUser = User.CreateCustomer("Existing", "User", command.Email, "password123");

        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email))
            .ReturnsAsync(existingUser);
        
        var handler = new RegisterCommandHandler(_jwtTokenGeneratorMock.Object, _userRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsError);
        Assert.Equal("User.DuplicateEmail", result.Errors[0].Code);
        Assert.Equal("User with given email already exists.", result.Errors[0].Description);
    }
}