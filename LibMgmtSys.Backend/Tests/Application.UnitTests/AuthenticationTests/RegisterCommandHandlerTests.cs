using System.Text.RegularExpressions;
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
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    
    public RegisterCommandHandlerTests()
    {
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RegisterCommandHandler(
            _unitOfWorkMock.Object,
            _jwtTokenGeneratorMock.Object);
    }
    
    private static bool IsValidEmail(string email)
    {
        const string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }

    [Fact]
    public async Task Handle_WhenUserIsNotRegistered_ShouldReturnAuthenticationResult()
    {
        _unitOfWorkMock.Setup(r => r.User.GetUserByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) =>
            {
                if (IsValidEmail(email))
                {
                    return null;
                }
                
                return User.CreateCustomer("John", "Doe", email, "password123");
            });
        var command = new RegisterCommand("Jane", "Doe", "jane@mail.com", "password123");
        var user = User.CreateCustomer(command.FirstName, command.LastName, command.Email, command.Password);
        var customer = Customer.Create(command.FirstName, command.LastName, command.Email, user.Id);
        
        _unitOfWorkMock.Setup(r => r.User.AddUserAsync(It.IsAny<User>()))
            .ReturnsAsync(user);
        _unitOfWorkMock.Setup(r => r.Customer.AddCustomerAsync(It.IsAny<Customer>()))
            .ReturnsAsync(customer);
        _jwtTokenGeneratorMock.Setup(g => g.GenerateToken(It.IsAny<User>()))
            .Returns("token");
        
        var result = await _handler.Handle(command, CancellationToken.None);
        
        Assert.False(result.IsError);
        Assert.Equal("token", result.Value.Token);
    }

    [Fact]
    public async Task Handle_DuplicateEmail_ShouldReturnDuplicateEmailError()
    {
        // Arrange
        var command = new RegisterCommand("John", "Doe", "john@mail.com", "password123");
        var existingUser = User.CreateCustomer("Existing", "User", command.Email, "password123");
        
        _unitOfWorkMock.Setup(r => r.User.GetUserByEmailAsync(command.Email))
            .ReturnsAsync(existingUser);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsError);
        Assert.Equal(Errors.User.DuplicateEmail, result.Errors[0]);
    }
}