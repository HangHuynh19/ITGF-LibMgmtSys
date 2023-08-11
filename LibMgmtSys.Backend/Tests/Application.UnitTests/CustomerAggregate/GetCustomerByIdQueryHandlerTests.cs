using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByIdQuery;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.CustomerAggregate
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly GetCustomerByIdQueryHandler _handler;

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _handler = new GetCustomerByIdQueryHandler(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCustomerId_ShouldReturnCustomer()
        {
            var expectedCustomerId = CustomerId.CreateUnique();
            var expectedCustomer = Customer.Create("John", "Doe", "john@mail.com", UserId.CreateUnique());

            _customerRepositoryMock.Setup(r => r.GetCustomerByIdAsync(expectedCustomerId))
                .ReturnsAsync(expectedCustomer);

            var getCustomerByIdQuery = new GetCustomerByIdQuery(expectedCustomerId);
            var result = await _handler.Handle(getCustomerByIdQuery, CancellationToken.None);
            
            Assert.False(result.IsError);
            Assert.Equal(expectedCustomer, result.Value);
        }

        [Fact]
        public async Task Handle_InvalidCustomerId_ShouldReturnCustomerNotFound()
        {
            var invalidCustomerId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var getCustomerByIdQuery = new GetCustomerByIdQuery(CustomerId.Create(invalidCustomerId));

            _customerRepositoryMock.Setup(r => r.GetCustomerByIdAsync(getCustomerByIdQuery.CustomerId))
                .ReturnsAsync((Customer)null);

            var result = await _handler.Handle(getCustomerByIdQuery, CancellationToken.None);
            
            Assert.True(result.IsError);
            Assert.Equal(Errors.Customer.CustomerNotFound, result.Errors[0]);
        }
    }
}

