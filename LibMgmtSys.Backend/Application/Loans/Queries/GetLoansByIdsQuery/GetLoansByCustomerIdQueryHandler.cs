using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetLoansByIdsQuery
{
    public class GetLoansByCustomerIdQueryHandler : IRequestHandler<GetLoansByCustomerIdQuery, ErrorOr<List<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetLoansByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<List<Loan>>> Handle(GetLoansByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Loan.GetLoansByCustomerIdAsync(query.CustomerId);
        }
    }
}

