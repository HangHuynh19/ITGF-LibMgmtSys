using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByUserIdQuery
{
    public class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, ErrorOr<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetCustomerByUserIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Customer>> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customer.GetCustomerByUserIdAsync(request.UserId);
            
            if (customer is null)
            {
                return Errors.Customer.CustomerNotFound;
            }
            
            return customer;
        }
    }
}

