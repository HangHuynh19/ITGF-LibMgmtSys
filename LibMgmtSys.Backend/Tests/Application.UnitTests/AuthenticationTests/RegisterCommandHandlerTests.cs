using LibMgmtSys.Backend.Application.Authentication.Commands.Register;
using LibMgmtSys.Backend.Application.Authentication.Common;
using LibMgmtSys.Backend.Application.Common.Interfaces.Authentication;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.AuthenticationTests;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _handler;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    
    public RegisterCommandHandlerTests()
    {
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _handler = new RegisterCommandHandler(
            _jwtTokenGeneratorMock.Object, 
            _userRepositoryMock.Object,
            _customerRepositoryMock.Object
            );
    }

    /*[Fact]
    public async Task Handle_WhenUserIsNotRegistered_ShouldReturnAuthenticationResult()
    {
        // Arrange
        var command = new RegisterCommand("John", "Doe", "john@mail.com", "password123");
        var newUser = User.CreateCustomer(command.FirstName, command.LastName, command.Email, command.Password);
        var newCustomer = Customer.Create(newUser.FirstName, newUser.LastName, newUser.Id);
        var authenticationResult = new AuthenticationResult(newUser, "token");
        
        _jwtTokenGeneratorMock.Setup(g => g.GenerateToken(It.IsAny<User>()))
            .Returns("token");
        _userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email))
            .ReturnsAsync((User)null);
        _userRepositoryMock.Setup(r => r.AddUserAsync(newUser)).ReturnsAsync(newUser);
        _customerRepositoryMock.Setup(r => r.AddCustomerAsync(newCustomer))
            .ReturnsAsync(newCustomer);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        //Assert.False(result.IsError);
        //Assert.NotNull(result.Value);
        Assert.Equal(authenticationResult.User, result.Value.User);
        //Assert.Equal(authenticationResult.Token, result.Value.Token);
        
        /*_jwtTokenGeneratorMock.Verify(g => g.GenerateToken(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(r => r.GetUserByEmailAsync(command.Email), Times.Once);
        _userRepositoryMock.Verify(r => r.AddUserAsync(newUser), Times.Once);
        _customerRepositoryMock.Verify(r => r.AddCustomerAsync(newCustomer), Times.Once);#1#
    }*/

    [Fact]
    public async Task Handle_DuplicateEmail_ShouldReturnDuplicateEmailError()
    {
        // Arrange
        var jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var customerRepositoryMock = new Mock<ICustomerRepository>();
        var handler = new RegisterCommandHandler(
            jwtTokenGeneratorMock.Object, 
            userRepositoryMock.Object,
            customerRepositoryMock.Object
        );
        var command = new RegisterCommand("John", "Doe", "john@mail.com", "password123");
        var existingUser = User.CreateCustomer("Existing", "User", command.Email, "password123");

        userRepositoryMock.Setup(r => r.GetUserByEmailAsync(command.Email))
            .ReturnsAsync(existingUser);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsError);
        Assert.Equal(Errors.User.DuplicateEmail, result.Errors[0]);
    }
}