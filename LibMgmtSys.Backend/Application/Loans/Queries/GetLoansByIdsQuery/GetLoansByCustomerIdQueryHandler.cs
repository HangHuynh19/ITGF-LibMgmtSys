using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetLoansByIdsQuery
{
    public class GetLoansByCustomerIdQueryHandler : IRequestHandler<GetLoansByCustomerIdQuery, ErrorOr<List<Loan>>>
    {
        private readonly ILoanRepository _loanRepository;
        
        public GetLoansByCustomerIdQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        
        public async Task<ErrorOr<List<Loan>>> Handle(GetLoansByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            return await _loanRepository.GetLoansByCustomerIdAsync(query.CustomerId);
        }
    }
}

