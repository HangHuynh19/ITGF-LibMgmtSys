using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery
{
    public class GetAllLoansWithPaginationQueryHandler 
        : IRequestHandler<GetAllLoansWithPaginationQuery, ErrorOr<List<Loan>>>
    {
        private readonly ILoanRepository _loanRepository;
        
        public GetAllLoansWithPaginationQueryHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<ErrorOr<List<Loan>>> Handle(GetAllLoansWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            return await _loanRepository.GetAllLoansWithPaginationAsync(request.PageNumber, request.PageSize);
        }
    }
}

