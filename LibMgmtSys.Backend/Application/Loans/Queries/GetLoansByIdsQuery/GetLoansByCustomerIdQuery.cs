using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using MediatR;
using ErrorOr;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;

namespace LibMgmtSys.Backend.Application.Loans.Queries.GetLoansByIdsQuery
{
    public record GetLoansByCustomerIdQuery(CustomerId CustomerId) : IRequest<ErrorOr<List<Loan>>>;
}

