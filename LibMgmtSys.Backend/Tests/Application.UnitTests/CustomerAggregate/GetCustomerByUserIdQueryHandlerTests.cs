using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByUserIdQuery;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using LibMgmtSys.Backend.Domain.UserAggregate.ValueObjects;
using Moq;

namespace Tests.Application.UnitTests.CustomerAggregate
{
    public class GetCustomerByUserIdQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly GetCustomerByUserIdQueryHandler _handler;
        
        public GetCustomerByUserIdQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new GetCustomerByUserIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ValidUserId_ShouldReturnCustomer()
        {
            var expectedUserId = UserId.CreateUnique();
            var expectedCustomer = Customer.Create("John", "Doe", "john@mail.com", UserId.CreateUnique());

            _unitOfWorkMock.Setup(r => r.Customer.GetCustomerByUserIdAsync(expectedUserId))
                .ReturnsAsync(expectedCustomer);

            var getCustomerByUserIdQuery = new GetCustomerByUserIdQuery(expectedUserId);
            var result = await _handler.Handle(getCustomerByUserIdQuery, CancellationToken.None);

            Assert.False(result.IsError);
            Assert.Equal(expectedCustomer, result.Value);
        }
        
        [Fact]
        public async Task Handle_InvalidUserId_ShouldReturnCustomerNotFound()
        {
            var invalidUserId = Guid.Parse("00000000-0000-0000-0000-000000000000");
            var getCustomerByUserIdQuery = new GetCustomerByUserIdQuery(UserId.Create(invalidUserId));

            _unitOfWorkMock.Setup(r => r.Customer.GetCustomerByUserIdAsync(getCustomerByUserIdQuery.UserId))
                .ReturnsAsync((Customer)null);

            var result = await _handler.Handle(getCustomerByUserIdQuery, CancellationToken.None);
            
            Assert.True(result.IsError);
            Assert.Equal(Errors.Customer.CustomerNotFound, result.Errors[0]);
        }
    }
}

