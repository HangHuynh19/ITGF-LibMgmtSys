using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByUserIdQuery
{
    public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, ErrorOr<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        public async Task<ErrorOr<Customer>> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByUserIdAsync(request.UserId);
            
            if (customer is null)
            {
                return Errors.Customer.CustomerNotFound;
            }
            
            return customer;
        }
    }
}

