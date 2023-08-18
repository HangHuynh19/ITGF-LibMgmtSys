using ErrorOr;
using LibMgmtSys.Backend.Domain.BookAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.CustomerAggregate.ValueObjects;
using LibMgmtSys.Backend.Domain.LoanAggregate;
using MediatR;

namespace LibMgmtSys.Backend.Application.Loans.Commands.CreateLoanCommand
{
    public record CreateLoanCommand(
        List<string> BookIds,
        DateTime LoanedAt,
        Guid CustomerId
    ) : IRequest<ErrorOr<List<Loan>>>;
}

