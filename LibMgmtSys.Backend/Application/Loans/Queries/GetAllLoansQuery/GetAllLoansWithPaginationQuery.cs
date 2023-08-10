using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetAllLoansQuery
{
    public record GetAllLoansWithPaginationQuery(
        int PageNumber,
        int PageSize
        ) : IRequest<ErrorOr<List<Loan>>>;
}
