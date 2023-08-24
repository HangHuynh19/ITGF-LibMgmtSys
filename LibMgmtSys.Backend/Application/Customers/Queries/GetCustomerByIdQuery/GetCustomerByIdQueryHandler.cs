using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.Common.DomainErrors;
using LibMgmtSys.Backend.Domain.CustomerAggregate;

namespace LibMgmtSys.Backend.Application.Customers.Queries.GetCustomerByIdQuery
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<Customer>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<Customer>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customer.GetCustomerByIdAsync(request.CustomerId);
            
            if (customer is null)
            {
                return Errors.Customer.CustomerNotFound;
            }
            
            return customer;
        }
    }
}