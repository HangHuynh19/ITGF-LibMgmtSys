using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Application.Common.Interfaces.Persistence;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery
{
    public class GetAllLoansWithPaginationQueryHandler 
        : IRequestHandler<GetAllLoansWithPaginationQuery, ErrorOr<List<Loan>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public GetAllLoansWithPaginationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ErrorOr<List<Loan>>> Handle(GetAllLoansWithPaginationQuery request,
            CancellationToken cancellationToken)
        {
            return await _unitOfWork.Loan.GetAllLoansWithPaginationAsync(request.PageNumber, request.PageSize);
        }
    }
}

