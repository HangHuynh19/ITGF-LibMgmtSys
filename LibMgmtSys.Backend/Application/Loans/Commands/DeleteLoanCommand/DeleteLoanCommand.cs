using LibMgmtSys.Backend.Domain.LoanAggregate;
using LibMgmtSys.Backend.Domain.LoanAggregate.ValueObjects;
using MediatR;
using ErrorOr;

namespace LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand
{
    public record DeleteLoanCommand(LoanId LoanId) : IRequest<ErrorOr<Loan>>;
}