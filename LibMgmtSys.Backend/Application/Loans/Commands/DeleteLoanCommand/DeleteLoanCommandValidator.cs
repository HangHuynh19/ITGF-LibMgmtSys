using FluentValidation;
using FluentValidation.AspNetCore;

namespace LibMgmtSys.Backend.Application.Loans.Commands.DeleteBookCommand
{
    public class DeleteLoanCommandValidator : AbstractValidator<DeleteLoanCommand>
    {
        public DeleteLoanCommandValidator()
        {
            RuleFor(deleteLoanCommand => deleteLoanCommand.LoanId)
                .NotEmpty();
        }
    }
}
